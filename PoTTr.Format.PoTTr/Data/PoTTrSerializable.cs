/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PoTTr.Format.PoTTr.Data
{
    public enum SerializeFormat { Protobuf, Json, Xml }

    public class PoTTrSerializable<T>
    {
        public void SaveProject(Stream stream, SerializeFormat format)
        {
            switch (format)
            {
                case SerializeFormat.Protobuf:
                    ProtoBuf.Serializer.Serialize(stream, this);
                    break;
                case SerializeFormat.Json:
                    var jSerializer = new DataContractJsonSerializer(typeof(T));
                    jSerializer.WriteObject(stream, this);
                    break;
                case SerializeFormat.Xml:
                    var xSerializer = new DataContractSerializer(typeof(T));
                    xSerializer.WriteObject(stream, this);
                    break;
                default: throw new NotImplementedException();
            }
        }

        public static T Open(Stream stream, SerializeFormat format)
        {
            switch (format)
            {
                case SerializeFormat.Protobuf:
                    return ProtoBuf.Serializer.Deserialize<T>(stream);
                case SerializeFormat.Json:
                    var jSerializer = new DataContractJsonSerializer(typeof(T));
                    return (T)jSerializer.ReadObject(stream);
                case SerializeFormat.Xml:
                    var xSerializer = new DataContractSerializer(typeof(T));
                    return (T)xSerializer.ReadObject(stream);
                default: throw new ArgumentException(nameof(format));
            }
        }
        public static bool TryOpen(Stream stream, SerializeFormat format, out T results)
        {
            try
            {
                results = Open(stream, format);
                return results != null;
            }
            catch
            {
                results = default!;
                return false;
            }
        }
    }
}