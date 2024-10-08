using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CPF.Cef.Common.Serialization.JsonConverters
{
    internal class BinaryJsonConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            // write byte[] with a special marker
            writer.WriteStringValue(DataMarkers.BinaryMarker + JsonSerializer.SerializeToNode(value));
        }
    }
}
