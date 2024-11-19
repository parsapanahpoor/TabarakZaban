using BaseInformation._Persistence;
using Identity.Persistence;
using Organization.Persistence;
using System.Reflection;

namespace Framework.Persistence.Context;

public static class AssembliesReference
{
    public static readonly Assembly[] Assemblies =
    [
        IdentityPersistenceAssemblyReference.Assembly,
        OrganizationPersistenceAssemblyReference.Assembly,
        BaseInformationPersistenceAssemblyReference.Assembly,
    ];
}