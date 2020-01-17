using Auth.Infrastructure.Repository;
using Auth.Infrastructure.Service;
using Auth.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Interface
{
    public class TokenManager
    {
        private readonly TokenConfig _tokenConfig;
        IRepositoryWrapper _repository;
        Cipher _cipher;

        public TokenManager(IOptions<TokenConfig> tokenConfig, IRepositoryWrapper repository, Cipher cipher) {
            _tokenConfig = tokenConfig.Value;
            _repository = repository;
            _cipher = cipher;
        }
        public async Task<User> AuthenticateAsync(User auth, Guid application )
        {
          
            var user = await _repository.User.Authenticate(auth.Login, auth.Password);
            if(user!= null) {
                if (_cipher.Decrypt(user.Password) != auth.Password)
                    return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var roles = await _repository.UserRole.GetUserRoles(user.Id);
            var permissions = _repository.EndPoint.GetByUserIdAndApplicationId(user.Id, application);
            var key = Convert.FromBase64String(_tokenConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = getClaimsIdentity(user, roles, permissions),
                Expires = DateTime.UtcNow.AddMinutes(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }
        protected ClaimsPrincipal GetPrincipal(string token)
        {
            try {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(_tokenConfig.Secret);
                TokenValidationParameters parameters = new TokenValidationParameters() {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //  ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception e) {
                return null;
            }
        }
        public string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException) {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }
        protected ClaimsIdentity getClaimsIdentity(User user, List<string> roles, List<Guid> permissions)
        {
            return new ClaimsIdentity(
                getClaims()
                );

            Claim[] getClaims()
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                if (roles != null) {
                    foreach (var item in roles) {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                }
                if (permissions != null) {
                    foreach (var item in permissions) {
                        claims.Add(new Claim("hash", item.ToString()));
                    }
                }
                return claims.ToArray();
            }

        }
        public void SaveToken(User user, Guid applicationId)
        {
            var appId = _repository.Application.GetWhere(x => x.Id == applicationId);
            if(appId == null) {
                throw new Exception("Application not found");
            }
            _repository.Token.Create(new Token() {
                Content = user.Token,
                IssueDate = DateTime.Now,
                IssuedBy = _tokenConfig.Issuer,
                Application_Id = applicationId ,
                User_Id = user.Id
            });
            _repository.Commit();
        }
    }
}
