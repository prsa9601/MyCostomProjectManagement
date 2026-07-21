using BackEnd.Data.Entities.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Shared.Extentions.Enum
{
    public static class EnumExtensions
    {
        public static List<string> GetDisplayNames<TEnum>() where TEnum : System.Enum
        {
            return System.Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => e.GetDisplayName())
                       .ToList();
        }

        public static string GetDisplayName(this System.Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field == null)
                return enumValue.ToString();

            // روش اول: استفاده از GetCustomAttributes با پارامتر نوع (سازگار با تمام نسخه‌ها)
            var attr = field.GetCustomAttributes(typeof(DisplayAttribute), false)
                            .FirstOrDefault() as DisplayAttribute;
            return attr?.Name ?? enumValue.ToString();

            // یا روش دوم (قدیمی‌تر):
            // var attrs = field.GetCustomAttributes(false);
            // var displayAttr = attrs.OfType<DisplayAttribute>().FirstOrDefault();
            // return displayAttr?.Name ?? enumValue.ToString();
        }
        public static List<TEnum> GetEnumsByDisplayNames<TEnum>(List<string> displayNames) where TEnum : System.Enum
        {
            if (displayNames == null || !displayNames.Any())
                return new List<TEnum>();

            return displayNames
                .Select(name => GetEnumByDisplayName<TEnum>(name))
                .ToList();
        }

        public static TEnum GetEnumByDisplayName<TEnum>(string displayName) where TEnum : System.Enum
        {
            foreach (TEnum value in System.Enum.GetValues(typeof(TEnum)))
            {
                if (value.GetDisplayName() == displayName)
                    return value;
            }
            throw new ArgumentException($"Display name '{displayName}' not found in enum {typeof(TEnum).Name}");
        }
    }
}
