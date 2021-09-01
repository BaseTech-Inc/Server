using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1).ToLower()
            };

        public static string AllFirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    {
                        var values = input.Split(' ');

                        for (var i = 0; i < values.Length; i++)
                        {
                            values[i] = values[i].FirstCharToUpper();
                        }

                        return string.Join(" ", values);
                    }
            }
        }

        private static Dictionary<string, string> acronyms =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
                {"AV.", "Avenida"},
                {"R.", "Rua"},
                {"TN.", "Tunel"},
                {"CEL", "Coronel"},
                {"PRES", "Presidente"}
            };

        public static string StreetConversion(this string street)
        {
            var streetList = street.Split(' ');
            var resultList = streetList
                .Select(word => acronyms.TryGetValue(word, out var newWord) ? newWord : word);

            var result = string.Join(" ", resultList);

            return result;
        }
    }
}
