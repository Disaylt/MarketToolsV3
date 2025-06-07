using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Domain.Seed;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.PermissionStore.Application.Utilities.Implementation;

public class PermissionsUtility(
    IOptions<ServiceConfiguration> configurationOptions)
    : IPermissionsUtility
{
    public string FindOrDefaultByPathView(string path)
    {
        string lastKey = path
            .Split(':')
            .Last();

        return FindOrDefaultByNameView(lastKey);
    }

    public string FindOrDefaultByNameView(string name)
    {
        return configurationOptions
                   .Value
                   .TranslatePermissions
                   .Parameters
                   .GetValueOrDefault(name)
               ?? "Ниезвестно";
    }
}