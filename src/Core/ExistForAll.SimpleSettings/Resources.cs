using System;
using System.Reflection;

namespace ExistForAll.SimpleSettings
{
	internal class Resources
	{
		public const string SettingsOptionsArgumentNullMessage = @"Settings builder must have at least one type of indication to know what is a Settings, 
				the options are an attribute, Interface or type name suffix. You can use all of them or one but you need at least one. ExistForAll.SimpleSettings 
				also provide to override each of the options thus you can provide your own set of attribute or interface. allowing you to decouple this framework
				from you domains.";

		public static string SettingsOptionsArgumentMissingMessage(string argumentName) => $@"Settings argument [{argumentName}] must be set.";

		public static string GetSettingsNotFoundMessageFormatMessage(Type settingsType)
			=> $@"Settings types [{settingsType.Name}] was not found, this could be due to several reasons:
				1. The Settings type was not found in the assemblies you provided
				2. Settings indication (Attribute, Interface, Suffix) was not set correctly";

		public static string SettingsBindingExceptionMessage(ISectionBinder binder, string section,
			string key) => $@"An error has occurred while [{binder.GetType().FullName}] tried
				to bind section [{section}] with key [{key}], if this is custom binding check your implementation of ISectionBinder.
				If this is ExistForAll.Settings binder please let us know on github.";

		public static string SettingsExtractionsExceptionMessage(Type type) =>
			$@"An error occurred while trying to get the type [{type.FullName}]
				from it's assembly, we only check if this type has some of our Settings indications like attribute, an interface or suffix.
				see inner exception for more details";

		public static string SettingsClassGenerationException(Type type) =>
			$@"While trying to generate a class from interface [{type.FullName}] something went wrong.
				please see inner exception for more details";

		public static string PropertySetterExceptionMessage(Type interfaceType, object value, PropertyInfo property) =>
			$@"failed to to set the value [{value}] within the property [{property.Name}] for interface [{interfaceType.Name}].
see inner exception for more details";

		public static string SettingsPropertiesExtractionMessage(Type type) =>
			$@"An error has occurred while trying to extract
				all properties from type [{type.FullName}]";

		public static string PropertyNotAllowNullMessage(string propertyName) =>
			$@"[{propertyName}] is marked as Null not allowed, yet the value is null. please provide value via binder or attribute";

		public static string TypeIsNotInterface(string typeName) =>
			@"[{typeName}] is not an interface, SimpleSettings supports only interfaces";

        public static string SettingsOptionAttributeTypeMessage(Type type) =>
            $"SimpleSettings support Attribute indication of interfaces, the type provided [${type.FullName}] is not an attribute.";

    }
}