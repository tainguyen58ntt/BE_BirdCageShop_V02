﻿using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool IsDelete { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
