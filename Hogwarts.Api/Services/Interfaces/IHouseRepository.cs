using Hogwarts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.Services.Interfaces
{
    interface IHouseRepository
    {
        public Task<bool> HouseExistsAsync(int houseId);

        public Task<IEnumerable<House>> GetAllHousesAsync();

        public Task<House> GetHouseByIdAsync(int houseId);

        public Task<House> GetHouseAssignedToHeadOfHouseAsync(int staffId);

        public Task<IEnumerable<HeadOfHouse>> GetHeadsOfHouseAsync(int houseId);

        public Task<HeadOfHouse> GetHeadOfHouseAsync(int staffId, int houseId);
        
    }
}
