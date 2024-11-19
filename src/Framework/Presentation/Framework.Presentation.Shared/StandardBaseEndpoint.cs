using Identity.Core;

namespace Framework.Presentation.Shared;

public abstract class StandardBaseEndpoint : CarterModule
{
    private readonly string[]? _tagNames;
    private readonly string[]? _policyNames;
    private List<RouteHandlerBuilder> _routes = [];

    protected StandardBaseEndpoint(string? basePath, string[]? tagNames = null) : base(basePath)
    {
        _tagNames = tagNames ?? basePath?
            .Split('/', StringSplitOptions.RemoveEmptyEntries);
    }

    protected StandardBaseEndpoint(string? basePath, string[]? tagNames = null, params string[] policyNames) : base(basePath)
    {
        _tagNames = tagNames ?? basePath?
            .Split('/', StringSplitOptions.RemoveEmptyEntries);
        _policyNames = policyNames;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        _routes = GetEndpoint(app);
        ApplyPolicyNames();
        ApplyTagNames();
        _routes.ForEach(ForEachEndpoint);
    }

    private void ApplyPolicyNames()
    {
        if (!AuthorizationConfiguration.EnableAuthorization || _policyNames is null)
            return;

        _routes.ForEach(route =>
        {
            foreach (string policyName in _policyNames)
            {
                route.RequireAuthorization(policyName)
                    .Produces((int) HttpStatusCode.Unauthorized)
                    .Produces((int) HttpStatusCode.Forbidden);
            }
        });
    }

    private void ApplyTagNames()
    {
        if (_tagNames is null)
            return;

        _routes.ForEach(route => { route.WithTags(_tagNames); });
    }

    protected abstract List<RouteHandlerBuilder> GetEndpoint(IEndpointRouteBuilder app);

    protected virtual void ForEachEndpoint(RouteHandlerBuilder route)
    {
    }
}