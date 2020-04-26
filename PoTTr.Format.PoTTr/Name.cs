/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

namespace PoTTr.Format.PoTTr
{
    public class Name
    {
        public NameType NameType { get; }
        public string Value { get; }

        public Name(NameType type, string value)
        {
            NameType = type;
            Value = value;
        }

    }
    public enum NameType { Full, Family, Given, Alias, Other }
}