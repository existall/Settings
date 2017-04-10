﻿using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.Settings.Core
{
	internal interface ISettingTypesExtractor
	{
		Type[] ExtractSettingTypes(AssemblyCollection assemblies, SettingsOptions options);
	}

	internal class SettingTypesExtractor : ISettingTypesExtractor
	{
		public Type[] ExtractSettingTypes(AssemblyCollection assemblies, SettingsOptions options)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			if (options == null) throw new ArgumentNullException(nameof(options));

			return assemblies.GetTypes()
				.Where(x => x.GetTypeInfo().IsInterface && IsFromOptions(x, options))
				.ToArray();
		}

		private static bool IsFromOptions(Type type, SettingsOptions options)
		{
			try
			{
				var info = type.GetTypeInfo();

				if (info.GetCustomAttribute(options.AttributeType, true) != null)
					return true;

				if (options.InterfaceBase.GetTypeInfo().IsAssignableFrom(info))
					return true;

				if (info.Name.ToLower().EndsWith(options.SettingSufix.Trim().ToLower()))
					return true;

				return false;
			}
			catch (Exception e)
			{
				throw new SettingsExtractionException(type,e);
			}

		}
	}
}
