// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using System.Text;

namespace AssignBuildingStylesWinForms
{
    internal sealed class TextBoxAppendWriter : TextWriter
    {
        private readonly TextBoxBase textBox;

        public TextBoxAppendWriter(TextBoxBase textBox)
        {
            this.textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string? value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke((string? text) => textBox.AppendText(text), value);
            }
            else
            {
                textBox.AppendText(value);
            }
        }

        public override void Write(char value)
        {
            Write(value.ToString());
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new string(buffer, index, count));
        }

        public override void Write(ReadOnlySpan<char> buffer)
        {
            Write(buffer.ToString());
        }
    }
}
