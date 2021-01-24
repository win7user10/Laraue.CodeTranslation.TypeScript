Typescript contracts code generator
--------------------

[![latest version](https://img.shields.io/nuget/v/Laraue.TypeScriptContractsGenerator)](https://www.nuget.org/packages/Laraue.TypeScriptContractsGenerator)

This library made to have opportunity for generating typescript contacts from C# code.

### Get started

```cs
var generator = new TypeScriptGeneratorBuilder()
    .Build();
    
generator.GenerateTsCode()
    .StoreTo("C://tsCode")
```

### How it works
Library loads all available assemblies and all existing it these assemblies types. Then it generates typescript code bases on loaded types.

##### Changing loading assemblies
```cs
generator.ConfigureAssemblies(assemblyNames => assemblyNames.MatchAnyPattern(new[] { "Laraue.Contracts.*", "Laraue.Core.DataAccess.*" }))
```

##### Changing source types
```cs
generator.ConfigureTypes(types =>
{
    var projectTypes = types.WithAttribure<DataContractAttribute>();
    return projectTypes.Concat(new[] { typeof(PaginatedRequest) });
})
```

##### Changing indent of string builder
```cs
generator.WithIndentSize(2);
```

##### Changing generation code behaviour
```cs
public class NewTsTypeGenerator : DefaultTsTypeGenerator
{
}

public class NewTsCodeGenerator : DefaultTsCodeGenerator
{
}
```

and use it's into generator

```cs
generator.UseTypeGenerator(typeGenerator);
generator.UseCodeGenerator(codeGenerator);
```

These classes contains different methods for generating code, such as class naming, type naming, should be class imported e t.c.

### Manually creating generator without loading types from assemblies
```cs
new TypeScriptGenerator(IEnumerable<Type> sourceTypes, int indentSize, TsCodeGenerator tsCodeGenerator, TsTypeGenerator tsTypeGenerator);
```

After calling GenerateTsCode() generator returns collection of GeneratedType elements.
 ```cs
public class GeneratedType
{
    public Type ClrType { get; }
    
    public string TsCode { get; }
    
    public string[] RelativeFilePathSegments { get; }
}
```

It can be saved to folder using next construction, or used as you want.
```cs
generatedCode.Store("C:/typescriptCode");
```