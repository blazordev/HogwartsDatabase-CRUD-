using System.Collections.Generic;

namespace Hogwarts.Server.Models
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> RoleNames { get; set; } = new List<string>();
         
               
    }
}