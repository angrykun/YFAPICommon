using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using YFAPICommon.Models;

namespace YFAPICommon.Controllers
{
    public class LoginInput
    {
        public string account { set; get; }
        public string pass { set; get; }
    }
    public class AuthenticateController : BaseController
    {
        [HttpPost]
        public ReturnNode GetAccessTokenByPass(LoginInput input)
        {
            var user = dbContext.User.Where(u => u.Name == input.account && u.Pass == input.pass).FirstOrDefault();
            if (user == null)
                return ReturnNode.ReturnError("登录失败");

            var tokenExpiration = TimeSpan.FromDays(14);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, "zzzili"));
            identity.AddClaim(new Claim(ClaimTypes.Sid, "1"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            JObject tokenResponse = new JObject(
                                        new JProperty("userName", "zzzili"),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));

            return ReturnNode.ReturnSuccess(tokenResponse);
        }
    }
}
