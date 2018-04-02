﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Titanbrary.WebAPI.Models
{
    public class RoleModel : IdentityRole
    {

        public string RoleId { get; set; }
        
        public string RoleName { get; set; }
        
    }
}