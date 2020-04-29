/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Runtime.Serialization;
using ProtoBuf;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class Name
    {
        [DataMember(Order = 1, IsRequired = true)]
        public NameType Type { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public string Value { get; set; } = null!;
    }

    public enum NameType { Full = 1, Family, Given, Alias, Other }
}