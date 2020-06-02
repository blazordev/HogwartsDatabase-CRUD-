using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hogwarts.Api.Services
{
    public class HouseLibraryRepository
    {
        private HogwartsDbContext _context;

        public HouseLibraryRepository(HogwartsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public bool HouseExists(int houseId)
        {
            if (String.IsNullOrEmpty(houseId.ToString()))
            {
                throw new ArgumentNullException(nameof(houseId));
            }
            return _context.Houses.Any(h => h.Id == houseId);
        }

        public IEnumerable<House> GetHouses()
        {
            return _context.Houses.ToList();
        }

        public House GetHouseById(int houseId)
        {
            if (String.IsNullOrEmpty(houseId.ToString()))
            {
                throw new ArgumentNullException(nameof(houseId));
            }
            return _context.Houses.FirstOrDefault(h => h.Id == houseId);
        }


        public IEnumerable<HeadOfHouse> GetHeadsOfHouse(int houseId)
        {
            if (String.IsNullOrEmpty(houseId.ToString()))
            {
                throw new ArgumentNullException(nameof(houseId));
            }
            return _context.HeadOfHouses
                .Where(h => h.HouseId == houseId)
                .ToList();
        }

    }

}
