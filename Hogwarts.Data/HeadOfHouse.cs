using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Data
{
    public class HeadOfHouse
    {
        public int Id { get; set; }
        public Staff Staff { get; set; }
        [Required] public int StaffId { get; set; }
        [Required] public int HouseId { get; set; }
        public House House { get; set; }

    }
}