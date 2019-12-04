using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Stateless_Token.DTO
{
    public class LoginModel
    {
        public string Username {get; set;}
        public string Password {get; set;}
    }
}
