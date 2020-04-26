/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Xml.Serialization;
using System.Xml;

namespace PoTTr.Format.TTML
{
    [XmlRoot("tt", Namespace = Namespaces.TTML)]
    public class TT : ITTMLElement
    {
        [XmlAttribute("extent", Namespace = Namespaces.TTMLStyling)] public string? Extent { get; set; }

        [XmlElement("head", Namespace = Namespaces.TTML)] public TTHead? Head { get; set; }
        [XmlElement("body", Namespace = Namespaces.TTML)] public TTBody? Body { get; set; }
    }
}
