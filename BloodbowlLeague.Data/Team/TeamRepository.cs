using AutoMapper;
using BloodbowlLeague.Logic;
using LiteDB;

namespace BloodbowlLeague.Data
{
    public class TeamRepository: ITeamRepository
    {
        private readonly string _filePath;

        public TeamRepository( string filePath )
        {
            _filePath = filePath;
        }

        public void Save( Team toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );
                var storageObj = Mapper.Map<TeamStorage>( toSave );
                col.Insert( storageObj );
            }
        }

        public Team Get( string teamName )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );
                var storageObj = col.FindOne( t => t.Name == teamName );
                return Mapper.Map<Team>( storageObj );
            }
        }
    }
}
