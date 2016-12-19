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

        public void SaveTeam( Team toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );

                col.Insert( new TeamStorage { Name = toSave.Name } );
            }
        }
    }
}
