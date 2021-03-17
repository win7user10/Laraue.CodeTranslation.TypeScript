using System;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	public interface IIndentedStringBuilder : IDisposable
	{
		public IIndentedStringBuilder Indent();

		public void Append(string value);

		public void AppendLine(string value);

		public void AppendLine();
	}
}