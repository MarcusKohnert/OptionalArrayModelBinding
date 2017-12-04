using System;
using System.Text.RegularExpressions;

namespace OptionalArrayModelBinding
{
    public class CustomIdentifier
    {
        // three lower case letters and three digits
        private static readonly Regex dataPointIdentifierFormat = new Regex("^([a-z]{3})([0-9]{3})$");

        private readonly string value;

        private CustomIdentifier(string value)
        {
            this.value = value;
        }

        public static CustomIdentifier Parse(string value)
        {
            var result = TryParse(value);
            if (result.Failed) throw new FormatException($"'{value}' is not a datapoint identifier");

            return result.Value;
        }

        public static Result<CustomIdentifier> TryParse(string value)
        {
            if (string.IsNullOrWhiteSpace(value) ||
                dataPointIdentifierFormat.IsMatch(value) == false)
            {
                return Result.Failure<CustomIdentifier>(new FormatException($"'{value}' is not a datapoint identifier"));
            }
            else
            {
                return Result.Success(new CustomIdentifier(value));
            }
        }

        public string Value => value;

        public override string ToString() => value;

        public static explicit operator CustomIdentifier(string value) => Parse(value);

        public static implicit operator string(CustomIdentifier customIdentifier) => customIdentifier.Value;
    }
}
