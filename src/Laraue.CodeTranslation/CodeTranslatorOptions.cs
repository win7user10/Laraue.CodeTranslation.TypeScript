using System;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation
{
    public class CodeTranslatorOptions
    {
        public int IndentSize { get; set; } = 2;

        public Action<MapCollection> ConfigureTypeMap { get; set; }
    }
}