using System;

namespace Laraue.TypeScriptContractsGenerator.CodeGenerator
{
    /// <summary>
    /// Generated ts code with info about source <see cref="Type"/>.
    /// </summary>
    public class GeneratedType
    {
        /// <summary>
        /// Source type.
        /// </summary>
        public Type ClrType { get; }

        /// <summary>
        /// Generated code.
        /// </summary>
        public string TsCode { get; }

        /// <summary>
        /// Relative file path of this file.
        /// </summary>
        public string[] RelativeFilePathSegments { get; }

        /// <summary>
        /// Initialize new instance of <see cref="GeneratedType"/>.
        /// </summary>
        /// <param name="clrType"></param>
        /// <param name="tsCode"></param>
        public GeneratedType(Type clrType, string tsCode, string[] relativeFilePathSegments)
        {
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            TsCode = string.IsNullOrEmpty(tsCode) ? throw new ArgumentNullException(nameof(tsCode)) : tsCode;
            RelativeFilePathSegments = relativeFilePathSegments.Length <= 0 ? throw new ArgumentNullException(nameof(relativeFilePathSegments)) : relativeFilePathSegments;
        }
    }
}
