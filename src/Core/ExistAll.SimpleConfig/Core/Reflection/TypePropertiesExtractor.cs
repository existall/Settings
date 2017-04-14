﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypePropertiesExtractor : ITypePropertiesExtractor
	{
		public IEnumerable<PropertyInfo> ExtractTypeProperties(Type type)
		{
			try
			{
				var info = type.GetTypeInfo();
				var properties = info.GetProperties().ToList();
				var inherited = info
					.GetInterfaces()
					.SelectMany(x => x.GetTypeInfo().GetProperties())
					.ToList();

				foreach (var property in inherited.Where(p => properties.All(x => x.Name != p.Name)))
					properties.Add(property);

				return properties;
			}
			catch (Exception e)
			{
				throw new ConfigPropertyExtractionException(type,e);
			}

		}
	}
}