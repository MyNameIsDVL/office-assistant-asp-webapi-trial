using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using officeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace officeApi
{
    public class OAuthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // zostało już zwalidowane, nie chcemy validować użądzenia (clienta)
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // sprawdzamy zgodność podanego użytkownika i hasło z db
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            // znajdujemy użytkownika i hasło
            var user = await userManager.FindAsync(context.UserName, context.Password);

            // sprawdzamy czy istnije taki użytkownik i hasło
            if (user != null)
            {
                var claim = new ClaimsIdentity(context.Options.AuthenticationType);

                // będzie przechowywane jako np key -> UserName: dvl123
                claim.AddClaim(new Claim("UserName", user.UserName));
                claim.AddClaim(new Claim("Email", user.Email));
                claim.AddClaim(new Claim("FirstName", user.FirstName));
                claim.AddClaim(new Claim("LastName", user.LastName));
                claim.AddClaim(new Claim("VacLimit", user.VacLimit));
                claim.AddClaim(new Claim("Logged", DateTime.Now.ToString()));

                // walidacja dla użytkownika
                context.Validated(claim);
            }
            else return;           
        }
    }
}