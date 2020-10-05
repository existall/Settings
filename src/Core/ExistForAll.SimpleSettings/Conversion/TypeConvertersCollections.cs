﻿using System.Collections.Generic;

namespace ExistForAll.SimpleSettings.Conversion
{
	internal class TypeConvertersCollections : LinkedList<ISettingsTypeConverter>
	{
		public TypeConvertersCollections(SettingsOptions settingsOptions)
		{
			AddLast(new DateTimeTypeConverter(settingsOptions));
			AddLast(new UriTypeConvertor());
			AddLast(new ArrayTypeConverter(settingsOptions, this));
			AddLast(new EnumerableTypeConverter(settingsOptions, this));
			AddLast(new DefaultTypeConverter());
		}
	}
}