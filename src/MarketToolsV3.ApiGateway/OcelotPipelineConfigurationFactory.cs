using Ocelot.Middleware;

namespace MarketToolsV3.ApiGateway
{
    public static class OcelotPipelineConfigurationFactory
    {
        public static OcelotPipelineConfiguration Create()
        {
            return new OcelotPipelineConfiguration
            {
                AuthorizationMiddleware = async (context, next) =>
                {
                    await next.Invoke();
                }
            };
        }
    }
}
