using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopBooking.Domain.Interfaces
{
    public interface IVehicleMakeApiService
    {
        public Task<List<string>> GetVehicleMakesAsync(CancellationToken cancellationToken);
        public Task LoadVehicleMakesAsync(CancellationToken cancellationToken);
        public Task<bool> DoesVehicleMakeExistAsync(string vehicleMake, CancellationToken cancellationToken);
    }
}
