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
   [RoutePrefix("createdby")]
    public class CreatedByController : ApiController
    {
        [HttpGet]
        [Route("getartistsbysongid/{songID}")]
        public IHttpActionResult GetArtistsBySongID(int songID)
        {
            try
            {
                CreatedByDal createdByDal = new CreatedByDal();
                List<CreatedBy> artistsOfSong = createdByDal.GetAllArtistsOfSong(songID).ToList();

                return Ok(artistsOfSong);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /* [HttpGet]
         [Route("getpublicsongs")]
         public IHttpActionResult GetPublicSongs()
         {
             try
             {
                 SongDal songsDAl = new SongDal();
                 List<Song> songs = songsDAl.GetPubicSongs().ToList();

                 return Ok(songs);
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }

             [HttpGet]
             [Route("getsongbyid/{key}")]
             public IHttpActionResult GetSongById(int key)
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
             [Route("getsongsbyartist/{key}")]
             public IHttpActionResult getSongsByArtist(int key)
             {
                 try
                 {
                     IDal<Song, int> songsDAl = new SongDal();
                     List<Song> songs = songsDAl.GetAll().ToList();

                     return Ok(songs);
                 }
                 catch (Exception ex)
                 {
                     return BadRequest(ex.Message);
                 }
             }
 */


    }
}
