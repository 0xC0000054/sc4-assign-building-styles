// Copyright (c) 2025 Nicholas Hayes
// SPDX-License-Identifier: MIT

using AssignBuildingStylesEngine;
using System.Text;

namespace AssignBuildingStylesWinForms
{
    internal sealed class TextBoxAppendStatusWriter : StatusWriterBase
    {
        private readonly TextBoxBase textBox;
        private readonly StringBuilder stringBuilder;

        public TextBoxAppendStatusWriter(TextBoxBase textBox)
        {
            this.textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            stringBuilder = new StringBuilder(256);
        }

        public override void WriteLine(string format, params object[] args)
        {
            string value;

            if (Indent == 0)
            {
                value = string.Format(format, args);
            }
            else
            {
                stringBuilder.Clear();
                stringBuilder.Append(' ', Indent);
                stringBuilder.AppendFormat(format, args);

                value = stringBuilder.ToString();
            }

            if (textBox.InvokeRequired)
            {
                textBox.Invoke((string? text) =>
                {
                    textBox.AppendText(text);
                    textBox.AppendText(Environment.NewLine);
                }, value);
            }
            else
            {
                textBox.AppendText(value);
                textBox.AppendText(Environment.NewLine);
            }
        }
    }
}
