﻿using System;
using System.IO;
using System.Reflection;
using ExistAll.SimpleConfig.Binder;
using ExistAll.SimpleConfig.Binders;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests
{
	public class Class1
	{
		[Fact]
		public void Test()
		{
			var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../appSettings.json")).Build();

			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var t = new ConfigBuilder();
			t.Add(new ConfigurationBinder(configuration));


			var configCollection = t.Build(collection, new ConfigOptions());
			var config = configCollection.GetConfig<IX1>();

		}

		[Fact]
		public void Test1()
		{
			var collection = new AssemblyCollection()
				.AddFullAssemblyHolder(this.GetType().GetTypeInfo().Assembly);

			var co = new InMemoryCollection();
			co.Add("X1", "Name", "goni");

			var t = new ConfigBuilder();
			t.Add(new InMemoryBinder(co));


			var configCollection = t.Build(collection, new ConfigOptions());
			var config = configCollection.GetConfig<IX1>();
			var config1 = configCollection.GetConfig<IX2>();

		}
	}
}