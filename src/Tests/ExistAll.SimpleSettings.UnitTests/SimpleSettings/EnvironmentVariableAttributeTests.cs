﻿using System;
using ExistAll.SimpleSettings.Binder;
using ExistAll.SimpleSettings.Binders;
using Xunit;

namespace ExistAll.SimpleSettings.UnitTests.SimpleSettings
{
	public class EnvironmentVariableAttributeTests
	{
		public const string EnvironmentVariable = "env-var";

		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var guid = Guid.NewGuid().ToString();

			using (new DisposableEnvironmentVariable(EnvironmentVariable, guid))
            {
                var sut = SettingsBuilder.CreateBuilder(x => x.AddEnvironmentVariable());

                var result = sut.ScanAssemblies(GetType().Assembly);

				var settings = result.GetSettings<IWithEnvironmentVariable>();

				Assert.Equal(guid, settings.EnvironmentVariable);
			}
		}
		
		[Fact]
		public void TryGetValue_WhenEnvVariableIsLast_ShouldSetValueFromEnv()
		{
			var guid = Guid.NewGuid().ToString();
			var badGuid = Guid.NewGuid().ToString();
			
            using (new DisposableEnvironmentVariable(EnvironmentVariable, guid))
            {
                var collection = new InMemoryCollection();
                collection.Add(nameof(IWithEnvironmentVariable).TrimStart('I'),
	                EnvironmentVariable, badGuid);

                var sut = SettingsBuilder.CreateBuilder(x => 
                { 
	                x.AddInMemoryCollection(collection)
	                .AddEnvironmentVariable(); 
                });

                var settings = sut.GetSettings<IWithEnvironmentVariable>();

                Assert.Equal(guid, settings.EnvironmentVariable);
            }
        }
		
		[Fact]
		public void TryGetValue_WhenMemoryCollectionIsLast_ShouldSetValueFromMemoryBinder()
		{
			var guid = Guid.NewGuid().ToString();
			var badGuid = Guid.NewGuid().ToString();
			
			using (new DisposableEnvironmentVariable(EnvironmentVariable, badGuid))
			{
				var collection = new InMemoryCollection();
				collection.Add(nameof(IWithEnvironmentVariable).TrimStart('I'),
					EnvironmentVariable, guid);

				var sut = SettingsBuilder.CreateBuilder(x => 
				{ 
					x.AddEnvironmentVariable()
						.AddInMemoryCollection(collection); 
				});

				var settings = sut.GetSettings<IWithEnvironmentVariable>();

				Assert.Equal(guid, settings.EnvironmentVariable);
			}
		}
	}
}