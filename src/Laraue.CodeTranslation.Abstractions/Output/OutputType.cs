using System;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		private readonly Func<Metadata.Metadata, string> _getNameFunc;

		protected OutputType(Func<Metadata.Metadata, string> getNameFunc)
		{
			_getNameFunc = getNameFunc;
		}

		public string GetName(Metadata.Metadata metadata)
		{
			return _getNameFunc.Invoke(metadata);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return _getNameFunc.ToString();
		}
	}
}