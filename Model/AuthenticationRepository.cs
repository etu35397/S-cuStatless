using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Stateless_Token.Model
{
    public class AutenticationRepository
    {
        private static User[] _users = new User[]{
            new User() {Username="jane", Email="jane@doe.com", Id=1, Password="123"},
            new User() {Username="jone", Email="jone@doe.com", Id=2, Password="456"}
        };

        public IEnumerable<User> GetUsers()
        {
            return  _users;
        }
    }
}
