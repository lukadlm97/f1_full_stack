using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class VerificationDto
    {
        public long Id { get; set; }
        public int VerificationCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
