using System.Reflection;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal interface ITypeConverter
	{
		object ConvertValue(object value, PropertyInfo propertyInfo, SettingsOptions options);
	}
}