﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Ext.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get name of display attribute of an enum value
        /// </summary>
        /// <param name="enumValue">The enum value to get</param>
        /// <returns>The name property of display attribute of exist, otherwise return enum to string</returns>
        public static string ToDisplayName(this Enum enumValue)
        {
            var attr = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>();

            return attr != null ? attr.GetName() : enumValue.ToString();
        }
    }
}
