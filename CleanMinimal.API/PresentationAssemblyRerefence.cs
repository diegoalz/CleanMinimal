using System.Reflection;

namespace CleanMinimal.API;

public class PresentationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}