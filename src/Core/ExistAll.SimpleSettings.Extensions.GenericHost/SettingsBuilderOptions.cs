﻿using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleSettings;

namespace ExistAll.SimpleSettings.Extensions.GenericHost
{
    public class SettingsBuilderOptions : ISettingsBuilderOptions
    {
        private readonly ISettingsBuilderFactory _settingsBuilderFactory;
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        public SettingsOptions Options => _settingsBuilderFactory.Options;
        public IEnumerable<Assembly> Assemblies => _assemblies;

        public void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            _assemblies.AddRange(assemblies);
        }

        public SettingsBuilderOptions(ISettingsBuilderFactory settingsBuilderFactory)
        {
            _settingsBuilderFactory = settingsBuilderFactory;
        }

        public void AddSectionBinder(ISectionBinder sectionBinder)
        {
            _settingsBuilderFactory.AddSectionBinder(sectionBinder);
        }
    }
}
