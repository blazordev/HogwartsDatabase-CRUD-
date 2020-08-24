using System;
using System.Collections.Generic;
using System.Text;

namespace Hogwarts.Data.ResourceParameters
{
    public class StaffResourceParameters
    {
        const int maxPageSize = 100;
        public int RoleId { get; set; }
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
