using Hogwarts.Api.DbContexts;
using Hogwarts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Hogwarts.Api.Services
{
    public class HouseRepository
    {
        private HogwartsDbContext _context;

        public HouseRepository(HogwartsDbContext context)
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

        public House GetHouseOfHeadOfHouse(int staffId)
        {
            var headOfHouse = _context.HeadOfHouses.FirstOrDefault(h => h.StaffId == staffId);
            return _context.Houses.FirstOrDefault(h =>
            h.Id == headOfHouse.HouseId);
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
        public HeadOfHouse GetHeadOfHouse(int staffId, int houseId)
        {
            return _context.HeadOfHouses.FirstOrDefault(h =>
            h.StaffId == staffId && h.HouseId == houseId);
        }


    }

}
