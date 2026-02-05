// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Text.Json.Serialization;

namespace AssignBuildingStylesWinForms
{
    internal sealed class BuildingStyleInfo
    {
#pragma warning disable IDE0032 // Use auto property
#pragma warning disable IDE0044 // Add readonly modifier
        [JsonInclude]
        private string name;
        [JsonInclude]
        private string author;
        [JsonInclude]
        private string description;
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

        public BuildingStyleInfo(string? name, string? author, string? description)
        {
            this.name = name ?? string.Empty;
            this.author = author ?? string.Empty;
            this.description = description ?? string.Empty;
        }

        [JsonIgnore]
        internal string Name => name;

        [JsonIgnore]
        internal string Author => author;

        [JsonIgnore]
        internal string Description => description;

        public override string ToString() => Name;
    }
}
