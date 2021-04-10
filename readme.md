Typescript code generator for DTO classes
--------------------

[![latest version](https://img.shields.io/nuget/v/Laraue.CodeTranslation.Typescript)](https://www.nuget.org/packages/Laraue.CodeTranslation.Typescript)

The problem: Frontend and backend have some contracts for communication, describing, what types exist in a system. Manually creating these contracts is slow and dangerous, because maybe occurred situation, when BE and FE contracts are not the same.

This library generates Typescript contracts from passed C# types using reflection. Each type is translating using specified logic.

Library is setup as default to process most common types such as below:
```cs
System.String -> string?
System.Guid -> string
System.Int32 -> number
System.Int32? -> number?
IEnumerable<System.String> -> string[]?
```

And also has options to extend default mapping.

### Get started

As first necessary to create a collection of types should be translated.

```cs
var types = new TypeCollection();
```

The library contains some extensions in Laraue.CodeTranslation.Extensions namespace to fast creating this collection.
For example, code below will load all referenced assemblies which name contains "Laraue.Contracts." and add to the collection all types
contains attribute "DataContract".

```cs
var types.AddTypesFromAllReferencedAssemblies(x => x.Contains("Laraue.Contracts."), x => x.HasAttribute<DataContractAttribute>())
```

Then should be created instance of CodeTranslator, class which will translate code from C# to Typescript.
It consumes translator options that can control output code view.

```cs
var codeTranslator = TypeScriptTranslatorBuilder.Create(new TypeScriptCodeTranslatorOptions());
var typesCode = codeTranslator.GenerateTypesCode(types);
```

CodeTranslator returns a sequence of files with generated code and path, where each of file should be situated.
It can be easily stored someplace on the disk using the special extension or used as you wish.

```cs
typesCode.StoreTo("D:/tsTypes", true);
```

### Code translator options

Options contains some properties to control code will be generated.

Example of adding additional mapping: System.Net.HttpStatusCode -> number:

```cs
var options = new TypeScriptCodeTranslatorOptions()
{
    ConfigureTypeMap = (mapCollection) => mapCollection.AddMap<HttpStatusCode, Number>()
}
```

Example of camel case type naming strategy for result code:

```cs
var options = new TypeScriptCodeTranslatorOptions()
{
    TypeNamingStrategy = new CamelCaseNamingStrategy()    
}
```

Example of custom indent size for generated code:

```cs
var options = new TypeScriptCodeTranslatorOptions()
{
    IndentSize = 4    
}
```