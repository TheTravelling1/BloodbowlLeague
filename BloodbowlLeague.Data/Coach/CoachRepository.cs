using AutoMapper;
using BloodbowlLeague.Logic;
using LiteDB;

namespace BloodbowlLeague.Data
{
    public class CoachRepository: ICoachRepository
    {
        private readonly string _filePath;

        public CoachRepository( string filePath )
        {
            _filePath = filePath;
        }

        public void Save( Coach toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<CoachStorage>( "coaches" );
                var storageObj = Mapper.Map<CoachStorage>( toSave );
                col.Insert( storageObj );
            }
        }

        public Coach Get( string teamName )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<CoachStorage>( "coaches" );
                var storageObj = col.FindOne( t => t.Name == teamName );
                return Mapper.Map<Coach>( storageObj );
            }
        }
    }
}
