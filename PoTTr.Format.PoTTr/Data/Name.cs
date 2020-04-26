/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

namespace PoTTr.Format.PoTTr.Data
{
    public class Name : IDataTable
    {
        public NameType NameType { get; set; }
        public string Value { get; set; } = null!;
        public int Id { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; } = null!;
    }

    public enum NameType { Full, Family, Given, Alias, Other }
}