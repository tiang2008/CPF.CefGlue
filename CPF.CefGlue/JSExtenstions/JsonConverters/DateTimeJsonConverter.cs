using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CPF.Cef.Common.Serialization.JsonConverters
{
    internal class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // write date time with a special marker and the date value without quotes
            writer.WriteStringValue(DataMarkers.DateTimeMarker + JsonSerializer.SerializeToNode(value));
        }
    }
}
