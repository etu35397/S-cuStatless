using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Stateless_Token.Model
{
    public class User
    {
        public string Username {get; set;}
        public string Password {get; set;}

        public int Id {get; set;}
        public string Email {get; set;}
    }
}
