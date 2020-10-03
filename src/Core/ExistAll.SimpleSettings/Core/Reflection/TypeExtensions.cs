﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal static class TypeExtensions
	{
		public static string GetNormalizeInterfaceName(this Type target)
		{
			return target.Name[0] == 'I' ? $"{target.Name.Substring(1)}" : target.Name;
		}

		public static string GetSectionName(this Type settingsClass, SettingsOptions options)
		{
			var attribute = settingsClass.GetTypeInfo().GetCustomAttribute<SettingsSectionAttribute>(true);

			return !string.IsNullOrWhiteSpace(attribute?.Name)
				? attribute.Name
				: options.SectionNameFormatter(settingsClass);
		}

		public static string GetPropertyName(this PropertyInfo propertyInfo)
		{
			var attribute = propertyInfo.GetCustomAttribute<SettingsPropertyAttribute>(true);

			return !string.IsNullOrWhiteSpace(attribute?.Name)
				? attribute.Name
				: propertyInfo.Name;
		}

		public static bool IsEnumerable(this Type type)
		{
			var info = type.GetTypeInfo();

			return info.IsGenericType && info.GetGenericTypeDefinition() == typeof(IEnumerable<>);
		}
	}
}