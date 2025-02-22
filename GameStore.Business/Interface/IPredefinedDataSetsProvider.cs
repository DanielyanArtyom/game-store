namespace GameStore.Business.Interface;

public interface IPredefinedDataSetsProvider
{
    List<string> GetBanDurations();
    List<string> GetPaginationOptions();
    List<string> GetSortingOptions();
    List<string> GetPublishDateOptions();
    List<string> GetAllPermissions();
}