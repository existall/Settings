﻿namespace ExistForAll.SimpleSettings.Binder
{
	public interface IInMemoryCollection
	{
		void Add(string section, string key, string value);
	}
}