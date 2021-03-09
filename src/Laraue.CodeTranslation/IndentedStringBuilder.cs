﻿using System;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Code;
using System.Text;

namespace Laraue.CodeTranslation
{
	public class IndentedStringBuilder : IIndentedStringBuilder
	{
		private readonly StringBuilder _stringBuilder;

		private int _indent = 0;

		private readonly int _indentSize;

		private string IndentationString => string.Concat(Enumerable.Range(1, _indent).Select(x => " "));

		public IndentedStringBuilder(int indentSize)
		{
			_stringBuilder = new StringBuilder(150);
			_indentSize = indentSize;
		}

		public IIndentedStringBuilder Append(string value)
		{
			_stringBuilder.Append(IndentationString)
				.Append(value);
			return this;
		}

		public IIndentedStringBuilder AppendLine(string value)
			=> Append(value + Environment.NewLine);

		public IIndentedStringBuilder AppendLine()
			=> Append(Environment.NewLine);

		public IIndentedStringBuilder Indent()
		{
			_indent++;
			return this;
		}

		public IIndentedStringBuilder DecreaseIndent()
		{
			if (_indent > 0)
			{
				_indent--;
			}

			return this;
		}

		/// <inheritdoc />
		public void Dispose()
		{
			DecreaseIndent();
		}

		public override string ToString()
		{
			return _stringBuilder.ToString();
		}
	}
}