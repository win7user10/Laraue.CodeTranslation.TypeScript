using System;
using System.Linq;
using System.Text;
using Laraue.CodeTranslation.Abstractions.Code;

namespace Laraue.CodeTranslation.Common
{
	/// <inheritdoc />
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

		/// <inheritdoc />
		public void Append(string value)
		{
			_stringBuilder.Append(IndentationString)
				.Append(value);
		}

		/// <inheritdoc />
		public void AppendLine(string value)
			=> Append(value + Environment.NewLine);

		/// <inheritdoc />
		public void AppendLine()
			=> Append(Environment.NewLine);

		/// <inheritdoc />
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

		/// <inheritdoc />
		public override string ToString()
		{
			return _stringBuilder.ToString();
		}
	}
}