using AutoMapper;
using BloodbowlLeague.Logic;
using BloodbowlLeague.Logic.Team;
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

        public void SaveTeam( Team toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );

                var storageObject = Mapper.Map<TeamStorage>( toSave );
                col.Insert( storageObject );
            }
        }
    }
}
