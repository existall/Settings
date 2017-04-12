﻿using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core
{
	internal interface IConfigTypesExtractor
	{
		Type[] ExtractConfigTypes(AssemblyCollection assemblies, ConfigOptions options);
	}

	internal class ConfigTypesExtractor : IConfigTypesExtractor
	{
		public Type[] ExtractConfigTypes(AssemblyCollection assemblies, ConfigOptions options)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			if (options == null) throw new ArgumentNullException(nameof(options));

			return assemblies.GetTypes()
				.Where(x => x.GetTypeInfo().IsInterface && IsFromOptions(x, options))
				.ToArray();
		}

		private static bool IsFromOptions(Type type, ConfigOptions options)
		{
			try
			{
				var info = type.GetTypeInfo();

				if (info.GetCustomAttribute(options.AttributeType, true) != null)
					return true;

				if (options.InterfaceBase.GetTypeInfo().IsAssignableFrom(info))
					return true;

				if (info.Name.ToLower().EndsWith(options.ConfigSufix.Trim().ToLower()))
					return true;

				return false;
			}
			catch (Exception e)
			{
				throw new ConfigExtractionException(type,e);
			}

		}
	}
}