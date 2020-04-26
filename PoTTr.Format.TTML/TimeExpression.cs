/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PoTTr.Format.TTML
{
    public class TimeExpression : IComparable<TimeExpression>, IComparable, IFormattable
    {
        const int secPerHr = 60 * 60;
        const int secPerMin = 60;
        const int minPerHr = 60;
        const double msPerSec = 1000;
        public int Hours => (int)(_seconds / (double)secPerHr);
        public int Minutes => (int)(_seconds / (double)secPerMin) % minPerHr;
        public int Seconds => _seconds % secPerMin;
        public double? SubSeconds => _subUnit * SubUnitRatio;
        public double? Milliseconds => (SubSeconds * msPerSec);
        public double? FrameRate => _frameRate;
        public double? Frames => SubSeconds * _frameRate;
        public TimeExpressionSubUnit SubUnit => _subUnitType;

        public double TotalSeconds => _seconds + SubSeconds ?? 0;
        public double TotalMinutes => TotalSeconds / (double)secPerMin;
        public double TotalHours => TotalSeconds / (double)secPerHr;

        public double SubUnitRatio
        {
            get
            {
                if (SubUnit == TimeExpressionSubUnit.Millisecond)
                    return 1.0 / msPerSec;
                else if (FrameRate != null)
                    return 1.0 / FrameRate.Value;
                else throw new ArgumentException("Frame rate must be set when subunit is frame rate");
            }
        }

        public enum TimeExpressionSubUnit { Millisecond, Frame };

        private int _seconds { get; }
        private double _subUnit { get; }
        private TimeExpressionSubUnit _subUnitType { get; }
        private double? _frameRate { get; }

        private TimeExpression(int TotalSeconds, double TotalSubUnit, TimeExpressionSubUnit SubUnit, double? frameRate = null)
        {
            _seconds = TotalSeconds;
            _subUnit = TotalSubUnit;
            _subUnitType = SubUnit;
            _frameRate = frameRate;
        }

        public static TimeExpression FromSeconds(int seconds, double milliseconds = 0)
        {
            return new TimeExpression(seconds, milliseconds, TimeExpressionSubUnit.Millisecond);
        }

        public static TimeExpression FromSeconds(double seconds)
        {
            int fullSec = (int)seconds;
            return new TimeExpression(fullSec, msPerSec * (seconds - fullSec), TimeExpressionSubUnit.Millisecond);
        }

        public static TimeExpression FromTime(int hours, int minutes, int seconds, double milliseconds = 0)
        {
            return new TimeExpression(hours * secPerHr + minutes * secPerMin + seconds, milliseconds, TimeExpressionSubUnit.Millisecond);
        }

        public static TimeExpression FromFrames(int hours, int minutes, int seconds, double frames, double? frameRate = null)
        {
            return new TimeExpression(hours * secPerHr + minutes * secPerMin + seconds, frames, TimeExpressionSubUnit.Frame, frameRate);
        }

        public enum Metric { Hours, Minutes, Seconds, Milliseconds, Frames, Ticks }

        public static TimeExpression FromMetric(double offset, Metric metric)
        {
            switch (metric)
            {
                case Metric.Hours:
                    return FromSeconds(offset * secPerHr);
                case Metric.Minutes:
                    return FromSeconds(offset * secPerMin);
                case Metric.Seconds:
                    return FromSeconds(offset);
                case Metric.Milliseconds:
                    return FromSeconds(offset / msPerSec);
                case Metric.Frames:
                    return FromFrames(0, 0, 0, offset);
                case Metric.Ticks:
                    throw new NotImplementedException("Ticks not supported");
                default:
                    throw new NotImplementedException("Unsupported metric");
            }
        }

        public static TimeExpression Parse(string timeString, double? frameRate = null) => TryParse(timeString, frameRate) ?? throw new FormatException();

        public TimeExpression ConvertFrameRate(double newFrameRate)
        {
            if (_subUnitType == TimeExpressionSubUnit.Frame && _frameRate == null)
                return new TimeExpression(_seconds, _subUnit, TimeExpressionSubUnit.Frame, newFrameRate);
            else return new TimeExpression(_seconds, _subUnit * newFrameRate * SubUnitRatio, TimeExpressionSubUnit.Frame, newFrameRate);
        }

        public TimeExpression ConvertToMilliseconds() =>
            new TimeExpression(_seconds, _subUnit * 1000 / SubUnitRatio, TimeExpressionSubUnit.Millisecond);


        public static bool TryParse(string timeString, out TimeExpression timeExpression, double? frameRate = null)
        {
            var output = TryParse(timeString, frameRate);
            timeExpression = output ?? new TimeExpression(0, 0, TimeExpressionSubUnit.Millisecond);
            return output != null;
        }

        public static TimeExpression? TryParse(string? timeString, double? frameRate = null)
        {
            string pattern = @"^(?<clocktime>(?<hours>[0-9]*):(?<minutes>[0-9][0-9]):(?<seconds>([0-9][0-9])|(60)))((?<fraction>\.[0-9]*)|((:(?<frames>[0-9]*\.?[0-9]*))))?|(?<offsettime>(?<timecount>[0-9]+\.?[0-9]*))(?<metric>h|m|s|(ms)|f|t)$";
            var matches = Regex.Match(timeString, pattern, RegexOptions.IgnoreCase);
            if (matches.Groups["clocktime"].Success)
            {
                int hours = int.Parse(matches.Groups["hours"].Value);
                int minutes = int.Parse(matches.Groups["minutes"].Value); // CHECK MIN btwn 0 and 60
                int seconds = int.Parse(matches.Groups["seconds"].Value); // CHECK MIN btwn 0 and 60
                if (matches.Groups["frames"].Success)
                {
                    double frames = double.Parse(matches.Groups["frames"].Value);
                    return TimeExpression.FromFrames(hours, minutes, seconds, frames);
                }
                else
                {
                    double milliseconds;
                    if (matches.Groups["fraction"].Success)
                        milliseconds = double.Parse(matches.Groups["fraction"].Value) * msPerSec;
                    else milliseconds = 0;
                    return TimeExpression.FromTime(hours, minutes, seconds, milliseconds);
                }
            }
            else if (matches.Groups["offsettime"].Success)
            {
                return FromMetric(double.Parse(matches.Groups["timecount"].Value),
                    matches.Groups["metric"].Value.ToLower() switch
                    {
                        "h" => Metric.Hours,
                        "m" => Metric.Minutes,
                        "s" => Metric.Seconds,
                        "ms" => Metric.Milliseconds,
                        "f" => Metric.Frames,
                        "t" => Metric.Ticks,
                        _ => Metric.Seconds // Based on regex, this should never hit
                    });
            }
            return null;
        }

        public int CompareTo([AllowNull] TimeExpression other)
        {
            if (other == null) return 1;
            return this.TotalSeconds.CompareTo(other.TotalSeconds);
        }

        public static bool operator >(TimeExpression te1, TimeExpression te2) => te1.CompareTo(te2) == 1;
        public static bool operator <(TimeExpression te1, TimeExpression te2) => te1.CompareTo(te2) == -1;
        public static bool operator >=(TimeExpression te1, TimeExpression te2) => te1.CompareTo(te2) >= 0;
        public static bool operator <=(TimeExpression te1, TimeExpression te2) => te1.CompareTo(te2) <= 0;
        public int CompareTo(object? obj) => CompareTo(obj as TimeExpression);

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            // Only worry about decimal places in the format string
            if (_subUnitType == TimeExpressionSubUnit.Millisecond)
            {
                format = format ?? "0.000###";
                var sub = (Seconds + SubSeconds ?? 0).ToString(format, formatProvider);
                return $"{Hours}:{Minutes}:{sub}";
            }
            else
            {
                format = format ?? "0.000###";
                var fra = Frames?.ToString(format, formatProvider);
                return $"{Hours}:{Minutes}:{Seconds}:{fra}";
            }
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }



        //public static explicit operator TimeSpan(TimeExpression te) => te;
        //public static explicit operator TimeExpression(TimeSpan ts) => new TimeExpression(ts);
        //public readonly override string ToString() => TimeSpan.ToString();
    }
}
