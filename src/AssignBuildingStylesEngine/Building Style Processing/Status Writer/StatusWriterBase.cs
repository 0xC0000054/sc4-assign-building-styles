// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesEngine
{
    public abstract class StatusWriterBase : IStatusWriter
    {
        protected StatusWriterBase()        
        {
            Indent = 0;
        }

        public int Indent { get; set; }

        public abstract void WriteLine(string format, params object[] args);
    }
}
