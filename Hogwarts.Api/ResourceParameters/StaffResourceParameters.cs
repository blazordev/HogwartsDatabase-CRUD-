using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Api.ResourceParameters
{
    public class StaffResourceParameters
    {
        const int maxPageSize = 25;
        public string SearchQuery { get; set; }
        public int RoleId { get; set; }      
        public int PageNumber { get; set; } = 1;
        
        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
