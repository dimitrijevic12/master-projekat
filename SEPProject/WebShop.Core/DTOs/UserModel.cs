﻿using System;

namespace WebShop.Core.DTOs
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
    }
}
