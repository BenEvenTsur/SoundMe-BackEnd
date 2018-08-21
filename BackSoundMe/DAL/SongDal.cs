using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BackSoundMe.Abstracts;

namespace BackSoundMe.DAL
{
    public class SongDal : IDal<Song, int>
    {
        public int Create(Song model)
        {
            int id = MaxIDInTable() + 1;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                model.ID = id;
                model = db.Songs.Add(model);
                db.SaveChanges();
            }

            return model.ID;
        }

        public void Delete(int id)
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Song song = db.Songs.FirstOrDefault(x => x.ID == id);

                if (song == null)
                    throw new NullReferenceException("Attempt to remove item that doesn't exist in database.");

                db.Songs.Remove(song);

                db.Entry(song).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public IEnumerable<Song> GetAll()
        {
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach(Song song in db.Songs)
                {
                    yield return song;
                }
            }
        }

        public Song GetByID(int ID)
        {
            Song song = null;

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                song = db.Songs.FirstOrDefault(s => s.ID == ID);
            }

            return song;
        }

        public void Update(Song model)
        {
            if (model == null || model.ID < 1)
                throw new ArgumentException("Mode is not valid.");

            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                Song modelFromDB = db.Songs.FirstOrDefault(e => e.ID  == model.ID);

                if (modelFromDB == null)
                    throw new NullReferenceException("Model by given ID doesn't exist at database.");

                modelFromDB.Permission = model.Permission;
                modelFromDB.Language = model.Language;
                modelFromDB.Lyrics = model.Lyrics;
                modelFromDB.Comments = model.Comments;
                modelFromDB.Chords = model.Chords;
                modelFromDB.Name = model.Name;

                db.Entry(modelFromDB).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private int MaxIDInTable()
        {
            int id = 0;
            using (ChordsDBEntities1 db = new ChordsDBEntities1())
            {
                foreach (Song song in db.Songs)
                {
                    if (song.ID > id)
                    {
                        id = song.ID;
                    }
                }
            }

            return id;
        }
    }
}