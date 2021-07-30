using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using officeApi.Models;

namespace officeApi.Controllers
{
    [AllowAnonymous]
    public class UserSettingsController : ApiController
    {
        private VacModel db = new VacModel();

        // GET: api/UserSettings
        public IQueryable<UserSettings> GetUserSettings()
        {
            return db.UserSettings;
        }

        // GET: api/UserSettings/5
        [ResponseType(typeof(UserSettings))]
        public IHttpActionResult GetUserSettings(int id)
        {
            UserSettings userSettings = db.UserSettings.Find(id);
            if (userSettings == null)
            {
                return NotFound();
            }

            return Ok(userSettings);
        }

        // PUT: api/UserSettings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserSettings(int id, UserSettings userSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSettings.Id)
            {
                return BadRequest();
            }

            db.Entry(userSettings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSettingsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserSettings
        [ResponseType(typeof(UserSettings))]
        public IHttpActionResult PostUserSettings(UserSettings userSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserSettings.Add(userSettings);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userSettings.Id }, userSettings);
        }

        // DELETE: api/UserSettings/5
        [ResponseType(typeof(UserSettings))]
        public IHttpActionResult DeleteUserSettings(int id)
        {
            UserSettings userSettings = db.UserSettings.Find(id);
            if (userSettings == null)
            {
                return NotFound();
            }

            db.UserSettings.Remove(userSettings);
            db.SaveChanges();

            return Ok(userSettings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserSettingsExists(int id)
        {
            return db.UserSettings.Count(e => e.Id == id) > 0;
        }

        [Route("api/upload/{username}")]
        [HttpPut]
        public IHttpActionResult UploadImage(string username)
        {
            string imageName = "";
            var httpRequest = HttpContext.Current.Request;
            var postFile = httpRequest.Files["Image"];
            imageName = new String(Path.GetFileNameWithoutExtension(postFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Assets/images/" + imageName);
            postFile.SaveAs(filePath);


            IQueryable<UserSettings> usernameAlreadyExists = db.UserSettings.Where(x => x.UserName == username).Take(1);

            UserSettings us = null;

            if (usernameAlreadyExists != null)
            {
                foreach (var item in usernameAlreadyExists)
                {
                    us = db.UserSettings.Find(item.Id);

                    break;
                }

                if (us != null)
                {
                    us.UserName = httpRequest["UserName"];
                    us.AvatarCaption = httpRequest["AvatarCaption"];
                    us.AvatarName = imageName;

                    //db.Entry(us).State = EntityState.Unchanged;
                    db.Entry(us).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserSettingsExists(us.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/upload/image/{username}")]
        [HttpGet]
        public byte[] GetUploadedImage(string username)
        {
            UserSettings us = null;

            var record = db.UserSettings.Where(c => c.UserName == username);

            foreach (var item in record)
            {
                us = db.UserSettings.Find(item.Id);

                break;
            }

            if (us != null)
            {
                var fileName = us.AvatarName;
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/images/" + fileName);

                //using (FileStream fs = new FileStream(filePath, FileMode.Open))
                //{
                //    var response = new HttpResponseMessage(HttpStatusCode.OK);
                //    response.Content = new StreamContent(fs);

                //    return ResponseMessage(response);
                //}

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        Bitmap image = new Bitmap(1, 1);
                        image.Save(memoryStream, ImageFormat.Png);

                        byte[] byteImage = memoryStream.ToArray();
                        return byteImage;
                    }
                }
            }

            return Encoding.ASCII.GetBytes("message");
            //return StatusCode(HttpStatusCode.NoContent);
            //return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}