namespace MarketToolsV3.PermissionStore.Application.Utilities.Abstract;

public interface IPermissionsUtility
{
    string FindOrDefaultByPathView(string path);
    string FindOrDefaultByNameView(string name);
}