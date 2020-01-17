using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using claims = System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Infrastructure.Service;
using Auth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Net;
using Auth.Infrastructure.Repository;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace Auth.Interface.Controllers
{

    public class AuthenticateController : Controller
    {
        private IRepositoryWrapper _repository;
        private IUserService _userService;
        private TokenManager _tokenManager;

        public AuthenticateController(IRepositoryWrapper repository, IUserService userService,
                                        TokenManager tokenManager)
        {
            _userService = userService;
            _repository = repository;
            _tokenManager = tokenManager;
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        // GET: Authenticate/Create
        public async Task<ActionResult> Create([FromBody] User user)
        {

            await _repository.User.CreateBasicUser(user);
            _repository.Commit();
            return Ok(user);
        }
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        // GET: Authenticate/Edit/5
        public async Task<ActionResult> Authenticate([FromBody] User auth,[FromQuery] Guid application)
        {

            if (auth == null || application == null )
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            var user = await _tokenManager.AuthenticateAsync(auth, application);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            _tokenManager.SaveToken(user, application );
            return Ok(user);
        }
        [HttpGet, Authorize]
        public ActionResult Validate([FromBody]User user)
        {
            string tokenUsername = _tokenManager.ValidateToken(user.Token);
            if (user.Name.Trim().Equals(tokenUsername.Trim(), StringComparison.InvariantCulture)) {
                StringValues originValues;
                Request.Headers.TryGetValue("Origin", out originValues);
                return Ok(HttpStatusCode.Accepted);
            }
            return BadRequest(HttpStatusCode.Unauthorized);
        }

    }
}