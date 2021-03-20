using System;
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

		public void Append(string value)
		{
			_stringBuilder.Append(IndentationString)
				.Append(value);
		}

		public void AppendLine(string value)
			=> Append(value + Environment.NewLine);

		public void AppendLine()
			=> Append(Environment.NewLine);

		public IIndentedStringBuilder Indent()
		{
			_indent += _indentSize;
			return this;
		}

		private void DecreaseIndent()
		{
			if (_indent > 0)
			{
				_indent -= _indentSize;
			}
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