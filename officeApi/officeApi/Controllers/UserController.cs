using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using officeApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace officeApi.Controllers
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        [Route("api/User/GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(this.userManager.Users.ToList());
        }

        [Route("api/User/Register")]
        [HttpPost]
        public IdentityResult Register(UserModel u_model)
        {
            //var userStore = new UserStore<ApplicationUser>(db);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser();

            user.Email = u_model.Email;
            user.UserName = u_model.UserName;
            user.FirstName = u_model.FirstName;
            user.LastName = u_model.LastName;
            user.VacLimit = u_model.VacLimit;
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireDigit = true
            };

            IdentityResult result = userManager.Create(user, u_model.Password);
            return result;
        }

        [Route("api/User/Update/{userName}")]
        [HttpPut]
        public IdentityResult UpdateVaLimit(UserModel u_model, string userName)
        {
            //var userStore = new UserStore<ApplicationUser>(db);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var current = User.Identity.GetUserName();
            if (userName != u_model.UserName)
            {
                Console.WriteLine("error");
            }
            var user = userManager.FindByName(u_model.UserName);
            user.VacLimit = u_model.VacLimit;
            IdentityResult result = userManager.Update(user);
            return result;
        }

        private bool VacationExists(string UserName)
        {
            return db.Users.Count(e => e.UserName == UserName) > 0;
        }

        [HttpGet]
        [Route("api/GetUser")]
        public UserModel UserClaims()
        {
            var claim = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = claim.Claims;
            UserModel model = new UserModel()
            {
                UserName = claim.FindFirst("UserName").Value,
                Email = claim.FindFirst("Email").Value,
                FirstName = claim.FindFirst("FirstName").Value,
                LastName = claim.FindFirst("LastName").Value,
                VacLimit = claim.FindFirst("VacLimit").Value,
                Logged = claim.FindFirst("Logged").Value
            };
            return model;
        }
    }
}
