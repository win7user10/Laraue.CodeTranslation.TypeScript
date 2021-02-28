using System;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		private readonly Func<Metadata.Metadata, OutputTypeName> _getNameFunc;

		protected OutputType(Func<Metadata.Metadata, OutputTypeName> getNameFunc)
		{
			_getNameFunc = getNameFunc;
		}

		public OutputTypeName GetName(Metadata.Metadata metadata)
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