/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using NUnit.Framework;
using PoTTr.Format.TTML;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PoTTr.Tests.Format.TTML
{
    public class TimeExpressionTests
    {
        [TestFixture]
        public class FromTime
        {
            TimeExpression te1;
            [SetUp]
            public void Setup()
            {
                te1 = TimeExpression.FromTime(1, 28, 27, 520);
            }

            [Test] public void Seconds() => Assert.That(27, Is.EqualTo(te1.Seconds));
            [Test] public void Minutes() => Assert.That(28, Is.EqualTo(te1.Minutes));
            [Test] public void Hours() => Assert.That(1, Is.EqualTo(te1.Hours));
            [Test] public void TotalSecond() => Assert.That(5307.520, Is.EqualTo(te1.TotalSeconds));
            [Test] public void TotalMinutes() => Assert.That(5307.520 / 60, Is.EqualTo(te1.TotalMinutes));
            [Test] public void TotalHours() => Assert.That(5307.520 / 3600, Is.EqualTo(te1.TotalHours));
            [Test] public void Frames() => Assert.That(te1.Frames, Is.Null);
            [Test]
            public void SetFrame()
            {
                Assert.That(0.520 * 24, Is.EqualTo(te1.ConvertFrameRate(24).Frames));
            }
            [Test]
            public void Comprable()
            {
                var ltSc = TimeExpression.FromTime(1, 28, 20, 520);
                var ltMs = TimeExpression.FromTime(1, 28, 27, 500);
                var ltFr = TimeExpression.FromFrames(1, 28, 27, 15, 33);
                var gtSc = TimeExpression.FromTime(1, 28, 29, 520);
                var gtMs = TimeExpression.FromTime(1, 28, 27, 550);
                var gtFr = TimeExpression.FromFrames(1, 28, 27, 18, 33);

                Assert.Less(ltSc, te1);
                Assert.Less(ltMs, te1);
                Assert.Less(ltFr, te1);
                Assert.Greater(gtSc, te1);
                Assert.Greater(gtMs, te1);
                Assert.Greater(gtFr, te1);

            }

            [Test]
            public void ToStringTest()
            {
                var msString = TimeExpression.FromTime(1, 28, 27, 520);
                Assert.AreEqual("1:28:27.52", msString.ToString("0.00", null));
                Assert.AreEqual("1:28:27.52", $"{msString:0.###}");
                Assert.AreEqual("1:28:27.520", $"{msString:0.000#}");

                var frString = TimeExpression.FromFrames(1, 28, 27, 15, 33);
                Assert.AreEqual(frString.ToString("0.00", null), "1:28:27:15.00");
                Assert.AreEqual("1:28:27:15", $"{frString:0.###}");
                Assert.AreEqual("1:28:27:15.000", $"{frString:0.000#}");

            }
        }

        [TestFixture]
        public class FromSeconds
        {
            TimeExpression te1;
            [SetUp]
            public void Setup()
            {
                // 5307 seconds = 1:28:27, or 88:27
                te1 = TimeExpression.FromSeconds(5307);
            }

            [Test] public void CorrectTime() => Assert.AreEqual(5307, te1.TotalSeconds);

        }

        [TestFixture]
        public class FromSecondsMs
        {
            TimeExpression te1;
            [SetUp]
            public void Setup()
            {
                // 5307 seconds = 1:28:27, or 88:27
                te1 = TimeExpression.FromSeconds(5307, 520);
            }

            [Test] public void CorrectTime() => Assert.AreEqual(5307.520, te1.TotalSeconds);
        }

        [TestFixture]
        public class FromSecondsDbl
        {
            TimeExpression te1;
            [SetUp]
            public void Setup()
            {
                // 5307.520 seconds = 1:28:27.520, or 88:27.520
                te1 = TimeExpression.FromSeconds(5307.520);
            }

            [Test] public void CorrectTime() => Assert.AreEqual(5307.520, te1.TotalSeconds);
        }

        [TestFixture]
        public class TryParseOut
        {
            [Test]
            public void TryParseSec()
            {
                var success = TimeExpression.TryParse("1:28:27", out TimeExpression te);
                Assert.That(success, Is.True);
                Assert.That(5307, Is.EqualTo(te.TotalSeconds));
            }

            [Test]
            public void TryParseMs()
            {
                var success = TimeExpression.TryParse("1:28:27.502", out TimeExpression te);
                Assert.That(success, Is.True);
                Assert.That(5307.502, Is.EqualTo(te.TotalSeconds));
            }


            [Test]
            public void TryParseFrame()
            {
                var success = TimeExpression.TryParse("1:28:27:20", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(5307 + 2.0 / 3.0, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseOffHr()
            {
                var success = TimeExpression.TryParse("10.25h", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(3600 * 10.25, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseOffMin()
            {
                var success = TimeExpression.TryParse("10.25m", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(60 * 10.25, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseOffSec()
            {
                var success = TimeExpression.TryParse("10.25s", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(10.25, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseOffMs()
            {
                var success = TimeExpression.TryParse("10250ms", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(10.25, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseOffFrame()
            {
                var success = TimeExpression.TryParse("4515f", out TimeExpression te, 24);
                Assert.IsTrue(success);
                Assert.AreEqual(4515 / 30.0, te.ConvertFrameRate(30).TotalSeconds);
            }

            [Test]
            public void TryParseBad()
            {
                Assert.That(TimeExpression.TryParse("27", out TimeExpression te1), Is.False);
                Assert.That(TimeExpression.TryParse("27:10.2", out TimeExpression te2), Is.False);
                Assert.That(TimeExpression.TryParse(" aa 22", out TimeExpression te3), Is.False);
            }
        }

        [TestFixture]
        public class Serialize
        {
            XmlSerializer s { get; set; }
            TT simpleDocument { get; set; }
            XDocument xml { get; set; }
            XNamespace tt { get; set; }

            [SetUp]
            public void SetUp()
            {
                s = new XmlSerializer(typeof(TT));

                simpleDocument = new TT()
                {
                    Body = new TTBody()
                    {
                        Begin = TimeExpression.Parse("1:28:27.502")
                    }
                };

                tt = Namespaces.TTML;

                xml = new XDocument(new XElement(tt + "tt",
                    new XAttribute(XNamespace.Xmlns + "tt", Namespaces.TTML),
                    new XAttribute(XNamespace.Xmlns + "ttp", Namespaces.TTMLParameter),
                    new XAttribute(XNamespace.Xmlns + "tts", Namespaces.TTMLStyling),
                    new XAttribute(XNamespace.Xmlns + "ttm", Namespaces.TTMLMetadata),
                    new XAttribute(XNamespace.Xmlns + "xml", Namespaces.Xml),
                    new XElement(tt + "body", new XAttribute("begin", "1:28:27.502"))));
            }

            [Test]
            public void TestSerialize()
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    s.Serialize(textWriter, simpleDocument, Namespaces.DefaultPrefixes);
                    var xDoc = XDocument.Parse(textWriter.ToString());
                    string v1 = xDoc.Descendants(tt + "body").First().Attribute("begin").Value;
                    string v2 = xml.Descendants(tt + "body").First().Attribute("begin").Value;
                    Assert.That(v1, Is.EqualTo(v2));
                }
            }

            [Test]
            public void TestDeserialize()
            {
                using (XmlReader xr = new XmlNodeReader(xml.ToXmlDocument()))
                {
                    var a = s.Deserialize(xr);
                    var b = a as TT;
                    Assert.That(simpleDocument.Body.Begin, Is.DeepEqualTo(b.Body.Begin));
                }
            }


        }
    }

}
