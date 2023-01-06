using System;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.SignalR;

namespace API.Extentions
{
    public static class Extentions
    {
        public static long ParseToLong(this string value)
        {
            return long.Parse(value);
        }

        public static int ParseToInt(this string value)
        {
            return int.Parse(value);
        }

        public static long ParseToLong(this string value, NumberStyles numberStyles)
        {
            return long.Parse(value, numberStyles);
        }

        public static int ParseToInt(this string value, NumberStyles numberStyles)
        {
            return int.Parse(value, numberStyles);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static byte[] ToByte(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static string Hashing(this string text)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: text,
                        salt: "!P@sWord".ToByte(),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));
        }

        public static string InnerMessage(this Exception value)
        {
            var message = value.Message;
            var next = value.InnerException;
            while (next != null)
            {
                message = next.Message;
                next = next.InnerException;
            }
            return message;
        }
    }

    public static class DateTimeExtentions
    {
        public static DateTime ParseToDateTime(this string value, string dateformat)
        {
            DateTime dateParse;

            if (DateTime.TryParseExact(
                value,
                dateformat,
                new System.Globalization.CultureInfo("ID-id"),
                System.Globalization.DateTimeStyles.AssumeLocal,
                out dateParse))
            {
                return dateParse;
            }

            throw new Exception($"'{value}' Invalid date format");
        }
    }
}