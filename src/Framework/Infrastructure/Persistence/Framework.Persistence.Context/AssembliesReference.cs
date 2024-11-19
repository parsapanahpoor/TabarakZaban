using Identity.Persistence;
using System.Reflection;

namespace Framework.Persistence.Context;

public static class AssembliesReference
{
    public static readonly Assembly[] Assemblies =
    [
        IdentityPersistenceAssemblyReference.Assembly
    ];
}