using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Application.DTOs
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string? ExternalId { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
    }
}
