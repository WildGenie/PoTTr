/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Collections.Generic;

namespace PoTTr.Format.PoTTr
{
    public class TranscriptProject
    {
        List<Agent> Agents { get; } = new List<Agent>();
    }
}
