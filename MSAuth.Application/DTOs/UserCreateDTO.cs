using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Application.DTOs
{
    public class UserCreateDTO
    {
        public required string ExternalId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
