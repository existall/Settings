﻿using System;

namespace ExistAll.SimpleSettings
{
	public class SettingsHolder : ISettingsHolder
	{
		public SettingsHolder(Type settingsType, object settingsImplementation)
		{
			SettingsType = settingsType;
			SettingsImplementation = settingsImplementation;
		}

		public Type SettingsType { get; }
		public object SettingsImplementation { get; }
	}
}
