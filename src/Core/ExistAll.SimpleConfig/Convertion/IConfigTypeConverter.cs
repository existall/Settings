﻿using System;

namespace ExistAll.SimpleConfig.Convertion
{
	public interface IConfigTypeConverter
	{
		bool CanConvert(Type configType);
		object Convert(object value, Type configType);
	}
}