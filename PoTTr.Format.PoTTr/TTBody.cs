/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Xml;
using System.Xml.Serialization;

namespace PoTTr.Format.TTML
{
    public class TTBody : ITTMLElement
    {
        [XmlIgnore] public TimeExpression? Begin { get; set; }
        [XmlIgnore] public TimeExpression? Duration { get; set; }
        [XmlIgnore] public TimeExpression? End { get; set; }

        [XmlAttribute("begin", Namespace = Namespaces.TTML)]
        public string? BeginStr
        {
            get => Begin?.ToString();
            set => Begin = TimeExpression.TryParse(value);
        }
        [XmlAttribute("dur", Namespace = Namespaces.TTML)] public string? DurationStr { get; set; }
        [XmlAttribute("end", Namespace = Namespaces.TTML)] public string? EndStr { get; set; }

        [XmlAttribute("id", Namespace = Namespaces.Xml)] public string? XmlId { get; set; }
        [XmlAttribute("lang", Namespace = Namespaces.Xml)] public string? XmlLang { get; set; }
        [XmlElement("space", Namespace = Namespaces.Xml)] public string? XmlSpace { get; set; }
        [XmlAnyAttribute] public XmlAttribute[]? XmlAttributes { get; set; }
    }
}