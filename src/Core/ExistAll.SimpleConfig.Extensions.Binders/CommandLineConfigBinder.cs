using System;
using System.Collections.Generic;
using System.Linq;

namespace ExistAll.SimpleConfig.Binders
{
   public class CommandLineConfigBinder : ISectionBinder
   {
      private readonly CommandLineConfigBinderOptions _options;
      private readonly Dictionary<string, string> _argumentStore;

      public CommandLineConfigBinder(string[] args, CommandLineConfigBinderOptions options)
      {
         if (args == null) throw new ArgumentNullException(nameof(args));
         _options = options ?? throw new ArgumentNullException(nameof(options));

         _argumentStore =
            new Dictionary<string, string>(options.IsCaseSensitive
               ? StringComparer.Ordinal
               : StringComparer.OrdinalIgnoreCase);

         Parse(args);
      }

      public void BindPropertyConfig(BindingContext context)
      {
         var key = _options.NameFormatter != null ? _options.NameFormatter(context.Section, context.Key) : context.Key;
         
         if(_argumentStore.TryGetValue(key, out var value))
         {
            context.SetNewValue(value);
         }
      }
      
      private void Parse(string[] args)
      {
         _argumentStore.Clear();

         if (args == null) return;

         foreach (var arg in args)
         {
            var (key, value) = SplitByDelimiter(arg, _options);

            var name = key.TrimStart(_options.ArgumentPrefixes.ToArray());

            if (value != null)
            {
               _argumentStore[name] = value;
            }
         }
      }

      private static Tuple<string, string> SplitByDelimiter(string str, CommandLineConfigBinderOptions options)
      {
         if (str == null)
            return null;

         string key, value;

         if (!options.Delimiters.Any())
         {
            key = str.Trim();
            value = null;
         }
         else
         {

            var indices = options.Delimiters.Where(delimiter => delimiter != null)
               .Select(d => str.IndexOf(d))
               .Where(d => d != -1).ToList();

            if (indices.Count == 0)
            {
               key = str.Trim();
               value = null;
            }
            else
            {
               var idx = indices.OrderBy(i => i).First();
               key = str.Substring(0, idx);
               value = str.Substring(idx + 1);
            }
         }

         return new Tuple<string, string>(key, value);
      }
   }
}