using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Helpers
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public PaginationMetadata MetaData { get; set; }
    }
}
