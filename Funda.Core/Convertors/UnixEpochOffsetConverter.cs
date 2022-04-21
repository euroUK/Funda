using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Funda.Core.Convertors
{
    /// <summary>
    /// JsonConverter class for Unix Epoch Offset
    /// https://docs.microsoft.com/en-us/dotnet/standard/datetime/system-text-json-support
    /// </summary>
    public sealed class UnixEpochOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private static readonly DateTimeOffset s_epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly Regex s_regex = new Regex("^/Date\\(([+-]*\\d+)([+-])(\\d{2})(\\d{2})\\)/$", RegexOptions.CultureInvariant);

        public override bool CanConvert(Type typeToConvert)
        {
            return base.CanConvert(typeToConvert);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var formatted = reader.GetString()!;
            var match = s_regex.Match(formatted);

            if (
                    !match.Success
                    || !long.TryParse(match.Groups[1].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out var unixTime)
                    || !int.TryParse(match.Groups[3].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out var hours)
                    || !int.TryParse(match.Groups[4].Value, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out var minutes))
            {
                throw new JsonException();
            }

            var sign = match.Groups[2].Value[0] == '+' ? 1 : -1;
            var utcOffset = new TimeSpan(hours * sign, minutes * sign, 0);

            return s_epoch.AddMilliseconds(unixTime).ToOffset(utcOffset);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var unixTime = Convert.ToInt64((value - s_epoch).TotalMilliseconds);
            var utcOffset = value.Offset;

            var formatted = FormattableString.Invariant($"/Date({unixTime}{(utcOffset >= TimeSpan.Zero ? "+" : "-")}{utcOffset:hhmm})/");
            writer.WriteStringValue(formatted);
        }
    }
}