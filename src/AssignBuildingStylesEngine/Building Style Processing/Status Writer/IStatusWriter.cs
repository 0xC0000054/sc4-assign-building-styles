// Copyright (c) 2025, 2026 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesEngine
{
    public interface IStatusWriter
    {
        public int Indent { get; set; }

        public void WriteLine(string format, params object[] args);
    }
}
