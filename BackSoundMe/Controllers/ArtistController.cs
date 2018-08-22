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
    [RoutePrefix("artist")]
    public class ArtistController : ApiController
    {
        [HttpGet]
        [Route("getartistbyid/{key}")]
        public IHttpActionResult GetArtistByID(int key)
        {
            try
            {
                if (key < 0)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<Artist, int> artistDAl = new ArtistDal();
                Artist artist = artistDAl.GetByID(key);

                return Ok(artist);
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
                ArtistDal artistDal = new ArtistDal();
                List<Artist> artists = artistDal.GetAll().ToList();

                return Ok(artists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(Artist artist)
        {
            try
            {
                if (!IsArtistValid(artist))
                    throw new ArgumentException("Model is not valid.");

                IDal<Artist, int> artistsDAl = new ArtistDal();
                int artistKey = artistsDAl.Create(artist);

                return Ok(artistKey);
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

                IDal<Artist, int> artistDAl = new ArtistDal();
                artistDAl.Delete(key);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(Artist arist)
        {
            try
            {
                if (arist == null || arist.ID < 1)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<Artist, int> artistDAl = new ArtistDal();
                artistDAl.Update(arist);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsArtistValid(Artist ar)
        {
            if (ar == null
                || ar.ID < 0)
                return false;
            else
                return true;
        }
    }
}

