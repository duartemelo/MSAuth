﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Entities
{
    public class UserConfirmation : BaseEntity
    {
        public User? User { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public DateTime? DateOfConfirm { get; set; }
        public DateTime DateOfExpire { get; set; } = DateTime.Now.AddHours(2);

        private UserConfirmation() { }

        public UserConfirmation(User user)
        {
            User = user;
        }
    }
}
