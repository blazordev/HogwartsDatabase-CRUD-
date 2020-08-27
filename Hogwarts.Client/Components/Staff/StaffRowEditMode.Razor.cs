using Hogwarts.Client.Services;
using Hogwarts.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Client.Components.Staff
{
    public partial class StaffRowEditMode
    {
        [Parameter] public StaffDto Staff { get; set; }
        public string backgroundColor = "#ffede3";
        
    }
}
