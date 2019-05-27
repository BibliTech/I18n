using BibliTech.I18n.Fallback;
using BibliTech.I18n.InInputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BibliTech.I18n.Builder
{

    public class InBuilder
    {

        public string? FallbackLanguage { get; set; }
        public FallbackStrategy FallbackStrategy { get; set; } = FallbackStrategy.DefaultFallbackStrategy;
        public List<IInInput> Inputs { get; set; } = new List<IInInput>();

        public InBuilder AddJsonFile(string jsonFilePath, string languageCode, bool useAsFallback = false)
        {
            return this.AddJsonFile(() => File.OpenRead(jsonFilePath), languageCode, useAsFallback);
        }


        public InBuilder AddJsonResources(string pathPrefix, string prefix)
        {
            return this.AddJsonResources(Assembly.GetCallingAssembly(), pathPrefix, prefix);
        }

        public InBuilder AddJsonResources(Assembly assembly, string pathPrefix, string prefix)
        {
            var allResources = assembly.GetManifestResourceNames();
            var files = allResources
                .Where(q => q.StartsWith(pathPrefix))
                .Select(q => new
                {
                    Name = q.Substring(pathPrefix.Length),
                    FullPath = q,
                })
                .Where(q => q.Name.StartsWith(prefix))
                .ToList();

            var prefixLength = prefix.Length;
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.Name);
                var languageCode = fileName.Substring(prefixLength);

                this.AddJsonFile(() => assembly.GetManifestResourceStream(file.FullPath), languageCode);
            }

            return this;
        }


        public InBuilder AddJsonResource(string name, string languageCode, bool useAsFallback = false)
        {
            return this.AddJsonResource(Assembly.GetCallingAssembly(), name, languageCode, useAsFallback);
        }

        public InBuilder AddJsonResource(Assembly assembly, string name, string languageCode, bool useAsFallback = false)
        {
            return this.AddJsonFile(() => assembly.GetManifestResourceStream(name), languageCode, useAsFallback);
        }

        public InBuilder AddJsonFile(Stream jsonFileStream, string languageCode, bool useAsFallback = false)
        {
            return this.AddJsonFile(() => jsonFileStream, languageCode, useAsFallback);
        }

        public InBuilder AddJsonFile(Func<Stream> jsonStreamFunc, string languageCode, bool useAsFallback = false)
        {
            languageCode = languageCode ?? throw new ArgumentNullException(nameof(languageCode));

            this.Inputs.Add(new JsonInInput(languageCode, jsonStreamFunc));

            if (useAsFallback)
            {
                this.SetFallbackLanguageInternal(languageCode);
            }

            return this;
        }

        public InBuilder AddJsonFiles(string folder, string prefix)
        {
            var files = Directory.GetFiles(folder, $"{prefix}*.json");
            var prefixLength = prefix.Length;
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var languageCode = fileName.Substring(prefixLength);

                this.AddJsonFile(file, languageCode);
            }

            return this;
        }

        public InBuilder SetFirstFallbackLanguage()
        {
            if (this.Inputs.Count == 0)
            {
                throw new InvalidOperationException($"No ${nameof(Inputs)} set yet.");
            }

            this.SetFallbackLanguageInternal(this.Inputs[0].LanguageCode, true);

            return this;
        }

        public InBuilder SetFallbackLanguage(string languageCode)
        {
            this.SetFallbackLanguageInternal(this.Inputs[0].LanguageCode, true);

            return this;
        }

        public InBuilder SetFallbackStrategy(FallbackStrategy fallbackStrategy)
        {
            this.FallbackStrategy = fallbackStrategy;
            return this;
        }

        public InProvider Build()
        {
            return new InProvider(this);
        }

        private void SetFallbackLanguageInternal(string languageCode, bool force = false)
        {
            if (!force && this.FallbackLanguage != null)
            {
                throw new InvalidOperationException($"{nameof(this.FallbackLanguage)} is already set");
            }

            this.FallbackLanguage = languageCode;
        }

    }

}
