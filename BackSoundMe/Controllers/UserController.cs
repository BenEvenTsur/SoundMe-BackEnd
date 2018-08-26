using BackSoundMe.Abstracts;
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
        [Route("getuserbyid/{key}")]
        public IHttpActionResult GetUserByID(int key)
        {
            try
            {
                if (key < 0)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<User, int> userDAl = new UserDal();
                User user = userDAl.GetByID(key);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getuserbyusername/{username}")]
        public IHttpActionResult GetUserByUserName(string userName)
        {
            try
            {
                if (userName == null)
                    throw new ArgumentException("UserName as string is Empty.");

                UserDal userDAl = new UserDal();
                User user = userDAl.GetByUserName(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            try
            {
                UserDal userDal = new UserDal();
                List<User> users = userDal.GetAll().ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[HttpGet]
        [Route("getSharedsongs")]
        public IHttpActionResult GetSharedSongs()
        {
            try
            {
                UserDal userDAl = new UserDal();
                List<Song> songs = songsDAl.GetPubicSongs().ToList();

                return Ok(songs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [HttpGet]
        [Route("getmysongs/{userID}")]
        public IHttpActionResult GetMySongs(int userID)
        {
            try
            {
                UserDal userDAl = new UserDal();
                List<Song> songs = userDAl.GetMySongs(userID).ToList();

                return Ok(songs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(User user)
        {
            try
            {
                if (!IsUserValid(user))
                    throw new ArgumentException("Model is not valid.");

                IDal<User, int> usersDAl = new UserDal();
                int userKey = usersDAl.Create(user);

                return Ok(userKey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{key}")]
        public IHttpActionResult Delete(int key)
        {
            try
            {
                if (key < 1)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<User, int> userDAl = new UserDal();
                userDAl.Delete(key);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(User user)
        {
            try
            {
                if (user == null || user.ID < 1)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<User, int> userDAl = new UserDal();
                userDAl.Update(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsUserValid(User user)
        {
            if (user == null
                || user.ID < 0
                || string.IsNullOrEmpty(user.Username))
                return false;
            else
                return true;
        }
    }
}
