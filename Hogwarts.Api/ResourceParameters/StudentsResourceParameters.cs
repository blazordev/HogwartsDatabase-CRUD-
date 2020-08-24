using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.ResourceParameters
{
    public class StudentsResourceParameters
    {
        const int maxPageSize = 100;
        public string HouseName { get; set; }
        public bool IncludeHouse { get; set; } = true;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 100;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
