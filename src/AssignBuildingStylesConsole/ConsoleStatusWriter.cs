// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using AssignBuildingStylesEngine;

namespace AssignBuildingStylesConsole
{
    internal sealed class ConsoleStatusWriter : StatusWriterBase
    {
        public ConsoleStatusWriter()
        {
        }

        public override void WriteLine(string format, params object[] args)
        {
            for (int i = 0; i < Indent; i++)
            {
                Console.Write(' ');
            }

            Console.WriteLine(format, args);
        }
    }
}
