using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Dto
{
    public class CreateUserDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
