﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Models
{
    public class UserModel
    {
        public string UserId {  get; set; }

        public string PasswordHash { get; set; }
    }

}
