using System;
using System.Linq;
using System.Text;

namespace Laraue.TypeScriptContractsGenerator.CodeGenerator
{
	public class IndentedStringBuilder
	{
		private readonly StringBuilder sb;
		private string indentationString;
		private string completeIndentationString = "";
		private int indent = 0;

		public IndentedStringBuilder(int indentSize)
		{
			sb = new StringBuilder(150);
			indentationString = string.Concat(Enumerable.Range(1, indentSize).Select(x => " "));
		}

		public IndentedStringBuilder Append(string value)
		{
			sb.Append(completeIndentationString)
				.Append(value);
			return this;
		}

		public IndentedStringBuilder AppendLine(string value)
			=> Append(value + Environment.NewLine);

		public IndentedStringBuilder AppendLine()
			=> Append(Environment.NewLine);

		public string IndentationString
		{
			get { return indentationString; }
			set
			{
				indentationString = value;
				UpdateCompleteIndentationString();
			}
		}

		private void UpdateCompleteIndentationString()
		{
			completeIndentationString = "";

			for (int i = 0; i < indent; i++)
				completeIndentationString += indentationString;
		}

		public IndentedStringBuilder IncreaseIndent()
		{
			indent++;
			UpdateCompleteIndentationString();
			return this;
		}

		public IndentedStringBuilder DecreaseIndent()
		{
			if (indent > 0)
			{
				indent--;
				UpdateCompleteIndentationString();
			}
			return this;
		}

		public override string ToString()
		{
			return sb.ToString();
		}
	}
}
