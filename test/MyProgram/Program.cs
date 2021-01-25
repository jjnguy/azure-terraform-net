using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using GG;

namespace MyProgram
{

  [TerraformModuleAttribute(Url = "")]
  class GoogModule
  {

  }
  class Program
  {
    static void Main(string[] args)
    {



      Console.WriteLine(new TerraformModule().http_forward);

    }
  }

  public partial class ModuleListResponse
  {
    public Meta meta { get; set; }

    public Module[] modules { get; set; }
  }

  public partial class Meta
  {
    public long limit { get; set; }

    public long current_offset { get; set; }

    public long next_offset { get; set; }

    public string next_url { get; set; }
  }

  public partial class Module
  {
    public string id { get; set; }

    public string owner { get; set; }

    public string @namespace { get; set; }

    public string name { get; set; }

    public string version { get; set; }

    public string provider { get; set; }

    public string description { get; set; }

    public Uri source { get; set; }

    public string tag { get; set; }

    public DateTimeOffset published_at { get; set; }

    public long downloads { get; set; }

    public bool verified { get; set; }
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
