﻿using System;

namespace ExistAll.SimpleConfig.Conversion
{
	internal class DefaultTypeConverter : IConfigTypeConverter
	{
		public bool CanConvert(Type configType)
		{
			return true;
		}

		public object Convert(object value, Type configType)
		{
			return System.Convert.ChangeType(value, configType);
		}
	}
}