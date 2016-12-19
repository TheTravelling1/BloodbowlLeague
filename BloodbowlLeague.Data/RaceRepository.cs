using System.Collections.Generic;
using AutoMapper;
using BloodbowlLeague.Logic.Race;
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
            throw new System.NotImplementedException();
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