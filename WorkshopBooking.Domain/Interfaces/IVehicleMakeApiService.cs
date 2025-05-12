namespace WorkshopBooking.Domain.Interfaces
{
    public interface IVehicleMakeApiService
    {
        Task<bool> DoesVehicleMakeExistAsync(string vehicleMake, CancellationToken cancellationToken);
        Task<List<string>> GetVehicleMakesAsync(CancellationToken cancellationToken);
        Task LoadVehicleMakesAsync(CancellationToken cancellationToken);
    }
}
