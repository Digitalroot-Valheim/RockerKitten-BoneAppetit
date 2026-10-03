using System.Diagnostics.CodeAnalysis;

namespace BoneAppetit
{
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public partial class Main
  {
    public const string Version = "3.4.0";
    public const string Name = "BoneAppetit";
    public const string Guid = "com.rockerkitten.boneappetit";
    public const string Namespace = nameof(BoneAppetit);

    internal static class PluginConfigSection
    {
      internal static string General = nameof(General);
      internal static string Food = nameof(Food);
    }
  }
}
