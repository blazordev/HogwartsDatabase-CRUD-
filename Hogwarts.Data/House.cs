using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Data
{
    public class House
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string MascotImageLink { get; set; }  
        public IEnumerable<HeadOfHouse> HeadsOfHouse { get; set; } = new List<HeadOfHouse>();
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        
    }
}
