// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

namespace AssignBuildingStylesWinForms
{
    /// <summary>
    /// Wraps a value type to provide better type safety when boxing in an object.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    internal sealed class Box<T> where T : struct
    {
        public Box(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
