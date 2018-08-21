using BackSoundMe.Abstracts;
using BackSoundMe.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackSoundMe.Controllers
{
    [RoutePrefix("song")]
    public class SongController : ApiController
    {
        [HttpGet]
        [Route("{key}")]
        public IHttpActionResult GetSong(int key)
        {
            try
            {
                if (key < 0)
                    throw new ArgumentException("Key as Guid is Empty.");

                IDal<Song, int> songsDAl = new SongDal();
                Song song = songsDAl.GetByID(key);

                return Ok(song);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("songs")]
        public IHttpActionResult GetSongs()
        {
            try
            {
                IDal<Song, int> songsDAl = new SongDal();
                List<Song> songs = songsDAl.GetAll()?.ToList();

                return Ok(songs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(Song song)
        {
            try
            {
                if (!IsSongValid(song))
                    throw new ArgumentException("Model is not valid.");

                IDal<Song, int> songsDAl = new SongDal();
                int songKey = songsDAl.Create(song);

                return Ok(songKey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult Delete(int key)
        {
            try
            {
                if (key < 1)
                    throw new ArgumentException("Key as Guid is Empty.");

                IDal<Song, int> songsDAl = new SongDal();
                songsDAl.Delete(key);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(Song song)
        {
            try
            {
                if (song == null || song.ID < 1)
                    throw new ArgumentException("Key as int is Empty.");

                IDal<Song, int> songsDAl = new SongDal();
                songsDAl.Update(song);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool IsSongValid(Song song)
        {
            if (song == null
                || song.ID < 0
                || string.IsNullOrEmpty(song.Lyrics))
                return false;
            else
                return true;
        }
    }
}
