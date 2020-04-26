/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PoTTr.Format.TTML
{
    public static class Namespaces
    {
        public const string TTML = "http://www.w3.org/ns/ttml";
        public const string TTMLParameter = "http://www.w3.org/ns/ttml#parameter";
        public const string TTMLStyling = "http://www.w3.org/ns/ttml#styling";
        public const string TTMLMetadata = "http://www.w3.org/ns/ttml#metadata";
        public const string Xml = "http://www.w3.org/XML/1998/namespace";

        static private XmlSerializerNamespaces? defaultPrefixes;
        static public XmlSerializerNamespaces DefaultPrefixes
        {
            get
            {
                if (defaultPrefixes == null)
                {
                    defaultPrefixes = new XmlSerializerNamespaces();
                    defaultPrefixes.Add("tt", TTML);
                    defaultPrefixes.Add("ttp", TTMLParameter);
                    defaultPrefixes.Add("tts", TTMLStyling);
                    defaultPrefixes.Add("ttm", TTMLMetadata);
                    defaultPrefixes.Add("xml", Xml);
                }
                return defaultPrefixes;
            }
        }
    }
}
