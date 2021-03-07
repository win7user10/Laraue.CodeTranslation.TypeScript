﻿using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ValueOutputType : OutputType
	{
		public override IEnumerable<OutputType> UsedTypes { get; } = Array.Empty<OutputType>();
	}
}