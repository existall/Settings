using System;
using System.Reflection;

namespace ExistAll.SimpleSettings
{
	internal class SettingsPropertyValueException : Exception
	{
		public SettingsPropertyValueException(
			Type interfaceType,
			object value,
			PropertyInfo property,
			Exception exception)
			: base(Resources.PropertySetterExceptionMessage(interfaceType, value, property), exception)
		{}
	}
}