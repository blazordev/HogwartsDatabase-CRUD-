using Hogwarts.Api.DbContexts;
using Hogwarts.Api.Services.Interfaces;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hogwarts.Api.Services
{
    public class HouseRepository: IHouseRepository
    {
        private HogwartsDbContext _context;

        public HouseRepository(HogwartsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> HouseExistsAsync(int houseId)
        {
            if (String.IsNullOrEmpty(houseId.ToString()))
            {
                throw new ArgumentNullException(nameof(houseId));
            }
            return await _context.Houses.AnyAsync(h => h.Id == houseId);
        }

        public async Task<IEnumerable<House>> GetAllHousesAsync()
        {
            return await _context.Houses.ToListAsync();
        }

        public async Task<House> GetHouseByIdAsync(int houseId)
        {
            if (String.IsNullOrEmpty(houseId.ToString()))
            {
                throw new ArgumentNullException(nameof(houseId));
            }
            return await _context.Houses.FirstOrDefaultAsync(h => h.Id == houseId);
        }

        public async Task<House> GetHouseAssignedToHeadOfHouseAsync(int staffId)
        {
            return await _context.HeadOfHouses
                .Include(h => h.House)
                .Where(h => h.StaffId == staffId)
                .Select(h => h.House).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HeadOfHouse>> GetHeadsOfHouseAsync(int houseId)
        {
            return await _context.HeadOfHouses
                .Where(h => h.HouseId == houseId)
                .ToListAsync();
        }
        public async Task<HeadOfHouse> GetHeadOfHouseAsync(int staffId, int houseId)
        {
            return await _context.HeadOfHouses.FirstOrDefaultAsync(h =>
            h.StaffId == staffId && h.HouseId == houseId);
        }


    }

}
