﻿using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace jsonCultuerLocalizerLibrary
{
    public class jsonStringLocalizer : IStringLocalizer
    {
        private JsonSerializer serializer = new();

        public LocalizedString this[string name] => new(name, getString(name));

        public LocalizedString this[string name, params object[] arguments] =>
            this[name].ResourceNotFound
            ? this[name]
            : new(name, string.Format(this[name].Value, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader SReader = new(stream);
            using JsonTextReader reader = new(SReader);

            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName) continue;

                var key = reader.Value as string;
                reader.Read();
                var value = serializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);

            }
        }

        private string getValueFromJson(string propertyName, string filePath)
        {

            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader SReader = new(stream);
            using JsonTextReader reader = new(SReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
                {
                    reader.Read();
                    return serializer.Deserialize<string>(reader);
                }
            }

            return string.Empty;
        }

        private string getString(string key)
        {
            var filePath = $"\\jsonCultuerLocalizerLibrary\\Resources\\{Thread.CurrentThread.CurrentCulture.Name}.json";
            var path = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("\\")) + filePath;
            var fullFilePath = Path.GetFullPath(path);
            

            if (File.Exists(fullFilePath))
                return getValueFromJson(key, fullFilePath);

            return string.Empty;
        }

    }
}
