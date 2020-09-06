using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.ResourceParameters
{
    public class StudentsResourceParameters
    {
        const int maxPageSize = 1000;
        public int HouseId { get; set; }
        public bool IncludeHouse { get; set; } = true;
        public string SearchQuery { get; set; }           
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 3;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
