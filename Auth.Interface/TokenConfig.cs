using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Interface
{
    public class TokenConfig
    {
        public string Secret { get; set; }
       public string Issuer { get; set; }
        public string Audience { get; set; }
    }

}
