using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// https://www.youtube.com/watch?v=1u33UTdllV0
namespace jjnguy.Terraform.Net
{
  [Generator]
  public class AzureTerraformGenerator : ISourceGenerator
  {
    public void Execute(GeneratorExecutionContext context)
    {
      context.AddSource("GenAttr", SourceText.From(@"
using System;
namespace GG 
{
  public class TerraformModuleAttribute: Attribute
  {
    public string Url { get; set; }
  }
}
      
      ", Encoding.UTF8));

      if (!(context.SyntaxReceiver is MySyntaxReceiver msr)) return;

      var http = new HttpClient();
      int count = 0;
      msr.todo.ForEach(csx =>
      {
        var result = http.GetAsync("https://registry.terraform.io/v1/modules/GoogleCloudPlatform/lb-http/google").Result;
        var rawContent = result.Content.ReadAsStringAsync().Result;
        var content = JsonSerializer.Deserialize<ModuleDetail>(rawContent);

        var inputs = string.Join('\n', content.root.inputs
          .Where(i => new string[] { "string", "bool" }.Contains(i.type))
          .Select(input => $"public string {input.name} {{ get; set; }}"));

        var src = SourceText.From($@"
using System;
namespace GG 
{{
  public class TerraformModule 
  {{
    {inputs}
  }}
}}
      ", Encoding.UTF8);
        context.AddSource($"GenModule{count++}", src);


      })
    }

    public void Initialize(GeneratorInitializationContext context)
    {
      context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
    }
  }

  public class MySyntaxReceiver : ISyntaxReceiver
  {
    public List<ClassDeclarationSyntax> todo = new List<ClassDeclarationSyntax>();
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
      if (syntaxNode is ClassDeclarationSyntax cds && cds.AttributeLists.Count > 0)
      {
        if (cds.AttributeLists.Any(attr => attr.))
          todo.Add(cds);
      }
    }
  }


  public class Input
  {
    public string name { get; set; }
    public string type { get; set; }
    public string description { get; set; }
    public string @default { get; set; }
    public bool required { get; set; }
  }

  public class Output
  {
    public string name { get; set; }
    public string description { get; set; }
  }

  public class ProviderDependency
  {
    public string name { get; set; }
    public string @namespace { get; set; }
    public string source { get; set; }
    public string version { get; set; }
  }

  public class Resource
  {
    public string name { get; set; }
    public string type { get; set; }
  }

  public class Root
  {
    public string path { get; set; }
    public string name { get; set; }
    public string readme { get; set; }
    public bool empty { get; set; }
    public List<Input> inputs { get; set; }
    public List<Output> outputs { get; set; }
    public List<object> dependencies { get; set; }
    public List<ProviderDependency> provider_dependencies { get; set; }
    public List<Resource> resources { get; set; }
  }

  public class Submodule
  {
    public string path { get; set; }
    public string name { get; set; }
    public string readme { get; set; }
    public bool empty { get; set; }
    public List<Input> inputs { get; set; }
    public List<Output> outputs { get; set; }
    public List<object> dependencies { get; set; }
    public List<ProviderDependency> provider_dependencies { get; set; }
    public List<Resource> resources { get; set; }
  }

  public class Dependency
  {
    public string name { get; set; }
    public string source { get; set; }
    public string version { get; set; }
  }

  public class Example
  {
    public string path { get; set; }
    public string name { get; set; }
    public string readme { get; set; }
    public bool empty { get; set; }
    public List<Input> inputs { get; set; }
    public List<Output> outputs { get; set; }
    public List<Dependency> dependencies { get; set; }
    public List<ProviderDependency> provider_dependencies { get; set; }
    public List<Resource> resources { get; set; }
  }

  public class ModuleDetail
  {
    public string id { get; set; }
    public string owner { get; set; }
    public string @namespace { get; set; }
    public string name { get; set; }
    public string version { get; set; }
    public string provider { get; set; }
    public string description { get; set; }
    public string source { get; set; }
    public string tag { get; set; }
    public DateTime published_at { get; set; }
    public int downloads { get; set; }
    public bool verified { get; set; }
    public Root root { get; set; }
    public List<Submodule> submodules { get; set; }
    public List<Example> examples { get; set; }
    public List<string> providers { get; set; }
    public List<string> versions { get; set; }
  }
}
