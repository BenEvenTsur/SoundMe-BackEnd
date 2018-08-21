using BackSoundMe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackSoundMe.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            try
            {
                UserDal userDal = new UserDal();

                userDal.Create(new User()
                {
                    Email = "sadasd",
                    First_Name = "sadasdas",
                    Last_Name = "12321",
                    Password = "HAHA",
                    Username = "HOLA1A"
                });

                List<User> users = userDal.GetAll().ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
