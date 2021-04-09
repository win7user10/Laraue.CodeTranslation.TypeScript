Typescript code generator for DTO classes
--------------------

[![latest version](https://img.shields.io/nuget/v/Laraue.TypeScriptContractsGenerator)](https://www.nuget.org/packages/Laraue.TypeScriptContractsGenerator)

The problem: Frontend and backend have some contracts for communication, describing, what types are exists in a system. Manually creating of these contracts is slow and danger, because may be occured situation, when BE and FE contracts are not the same.

This library generates Typescript contracts from passed C# types using reflection. Each type is translating using specified logic.

Library is setup as default to process most common types such as below:
```cs
System.String -> string?
System.Guid -> string
System.Int32 -> number
System.Int32? -> number?
IEnumerable<System.String> -> string[]?
```

And also has options to setup default mapping.

### Get started

As first necessary to create collection of types should be translated.

```cs
var types = new TypeCollection();
```

Library contains some extensions in Laraue.CodeTranslation.Extensions namespace to fast creating this collection.

```cs
var types.AddTypesFromAllReferencedAssemblies(x => x.Contains("Laraue.Contracts."), x => x.HasAttribute<DataContractAttribute>())
```

Then can be created instance of CodeTranslator which will translate code from C# to Typescript. 
It consumes translator options which can control output code view.

Example of additional mapping: System.Net.HttpStatusCode -> number:

```cs
var options = new TypeScriptCodeTranslatorOptions()
{
    ConfigureTypeMap = (mapCollection) => mapCollection.AddMap<HttpStatusCode, Number>()
}
```

Example of camel case type naming for result code:

```cs
var options = new TypeScriptCodeTranslatorOptions()
{
    TypeNamingStrategy = new CamelCaseNamingStrategy()    
}
```

Now CodeTranslator can be creating and result code can be generated 

```cs
var codeTranslator = TypeScriptTranslatorBuilder.Create(options);
var typesCode = codeTranslator.GenerateTypesCode(types);
```

CodeTranslator returns sequence of files with generated code and path, where this file should be situated. 
It can be easy stored to some place on the disk using special extension or used as you wish.

```cs
typesCode.StoreTo("D:/tsTypes", true);
```