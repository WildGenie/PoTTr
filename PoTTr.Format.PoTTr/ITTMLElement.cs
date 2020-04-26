/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Xml.Serialization;
using System.Xml;

namespace PoTTr.Format.TTML
{
    public abstract class ITTMLElement
    {
        [XmlAttribute("id", Namespace = Namespaces.Xml)] public string? XmlId { get; set; }
        [XmlAttribute("lang", Namespace = Namespaces.Xml)] public string? XmlLang { get; set; }
        [XmlElement("space", Namespace = Namespaces.Xml)] public string? XmlSpace { get; set; }
        [XmlAnyAttribute] public XmlAttribute[]? XmlAttributes { get; set; }
        [XmlAnyElement] public XmlElement[]? XmlElements { get; set; }
    }
}