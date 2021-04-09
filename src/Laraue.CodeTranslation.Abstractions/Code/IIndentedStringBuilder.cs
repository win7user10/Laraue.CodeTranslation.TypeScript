using System;
using System.Text;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Represents <see cref="StringBuilder"/> for generating result programming code.
	/// </summary>
	public interface IIndentedStringBuilder : IDisposable
	{
		/// <summary>
		/// Increase indent.
		/// </summary>
		/// <returns></returns>
		public IIndentedStringBuilder Indent();

		/// <summary>
		/// Append string value.
		/// </summary>
		/// <param name="value"></param>
		public void Append(string value);

		/// <summary>
		/// Append string value and make new indent.
		/// </summary>
		/// <param name="value"></param>
		public void AppendLine(string value);

		/// <summary>
		/// Append new indent.
		/// </summary>
		public void AppendLine();
	}
}