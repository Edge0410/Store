﻿using Store.Models.Base;
using Store.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Store.Models
{
    public class User : BaseEntity
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string RefreshToken { get; set; }
            public DateTime RefreshTokenCreationDate { get; set; }
            public DateTime RefreshTokenExpirationDate { get; set; }
            public Roles Role { get; set; }
            public List<Order> Orders { get; set; } = new List<Order>();

    }
}
