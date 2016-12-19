using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BloodbowlLeague.Logic;
using LiteDB;

namespace BloodbowlLeague.Data
{
    public class RaceRepository: IRaceRepository
    {
        private readonly string _filePath;

        public RaceRepository( string filePath )
        {
            _filePath = filePath;
        }

        public Race Get( string name )
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var col = db.GetCollection<RaceStorage>( "races" );
                var fromDb = col.FindOne(r => r.Name == name);
                return Mapper.Map<Race>(fromDb);
            }
        }

        public IReadOnlyCollection<Race> GetAll()
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var col = db.GetCollection<RaceStorage>( "races" );
                var fromDb = col.FindAll();
                return fromDb.Select(Mapper.Map<Race>).ToList();
            }
        }

        public void Save( Race toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<RaceStorage>( "races" );
                var storageObj = Mapper.Map<RaceStorage>( toSave );
                col.Insert( storageObj );
            }
        }
    }
}