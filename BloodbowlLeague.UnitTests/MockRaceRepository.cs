using System.Collections.Generic;
using BloodbowlLeague.Logic;

namespace BloodbowlLeague.UnitTests
{
    public class MockRaceRepository: IRaceRepository
    {
        public Race Get( string name )
        {
            if ( name != "High Elf" )
            {
                throw new RaceNotFoundException();
            }

            return CreateHighElf();
        }

        public IReadOnlyCollection<Race> GetAll()
        {
            return new[] { CreateHighElf() };
        }

        public void Save(Race toSave)
        {
            throw new System.NotImplementedException();
        }

        private static Race CreateHighElf()
        {
            var race = new Race( "High Elf" );

            race.AddPlayerType( "Blitzer", new PlayerStats( 7, 3, 4, 8 ), new Skill( "Block", "Blocks" ) );
            race.AddPlayerType( "Catcher", new PlayerStats( 8, 3, 4, 7 ), new Skill( "Catch", "Catches" ) );
            race.AddPlayerType( "Thrower", new PlayerStats( 6, 3, 4, 8 ), new Skill( "Safe Throw", "Throws" ), new Skill( "Pass", "Passes" ) );
            race.AddPlayerType( "Lineman", new PlayerStats( 6, 3, 4, 8 ) );

            return race;
        }
    }
}
