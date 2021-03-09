using System;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	public interface IIndentedStringBuilder : IDisposable
	{
		public IIndentedStringBuilder Indent();

		public IIndentedStringBuilder Append(string value);

		public IIndentedStringBuilder AppendLine(string value);

		public IIndentedStringBuilder AppendLine();
	}
}