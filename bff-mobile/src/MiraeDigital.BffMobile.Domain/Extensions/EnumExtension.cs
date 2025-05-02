using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum)
                return null;

            var description = Enum.GetName(typeof(TEnum), enumValue);

            var fieldInfo = typeof(TEnum).GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static IEnumerable<string> GetListDescriptions<TEnum>() where TEnum : struct, Enum
        {
            TEnum[] values = Enum.GetValues<TEnum>();

            foreach (TEnum value in values)
            {
                yield return $"{Convert.ToInt32(value)}-{value.GetDescription()}";
            }
        }

        public static string GetEnumComment<TEnum>(string description) where TEnum : struct, Enum
        {
            return $"{description} {string.Join(" | ", GetListDescriptions<TEnum>())}".Trim();
        }

        public static string GetEnumComment<TEnum>() where TEnum : struct, Enum
        {
            return GetEnumComment<TEnum>(string.Empty);
        }
    }
}
