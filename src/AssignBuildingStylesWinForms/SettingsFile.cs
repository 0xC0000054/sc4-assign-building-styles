// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Runtime.Serialization;
using System.Xml;

namespace AssignBuildingStylesWinForms
{
    internal static class SettingsFile
    {
        private static readonly string SettingsFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, "Settings.xml");

        public static Settings Load()
        {
            Settings settings;

            using (FileStream stream = new(SettingsFilePath, FileMode.Open, FileAccess.Read))
            {
                XmlReaderSettings readerSettings = new()
                {
                    CloseInput = false,
                    IgnoreComments = true,
                    XmlResolver = null
                };

                using (XmlReader xmlReader = XmlReader.Create(stream, readerSettings))
                {
                    DataContractSerializer serializer = new(typeof(Settings));
                    settings = (Settings)(serializer.ReadObject(xmlReader) ?? throw new InvalidOperationException("Failed to load the settings."));
                    settings.Dirty = false;
                }
            }

            return settings;
        }

        public static void Save(Settings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            if (settings.Dirty)
            {
                XmlWriterSettings writerSettings = new()
                {
                    Indent = true
                };

                using (XmlWriter writer = XmlWriter.Create(SettingsFilePath, writerSettings))
                {
                    DataContractSerializer serializer = new(typeof(Settings));
                    serializer.WriteObject(writer, settings);
                }
            }
        }
    }
}
