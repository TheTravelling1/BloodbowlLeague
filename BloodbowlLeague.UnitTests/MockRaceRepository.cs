using BloodbowlLeague.Logic;
using BloodbowlLeague.Logic.Race;
using BloodbowlLeague.Logic.Values;

namespace BloodbowlLeague.UnitTests
{
    public class MockRaceRepository : IRaceRepository
    {
        public Race GetRace(string name)
        {
            if (name != "High Elf")
            {
                throw new RaceNotFoundException();
            }

            var race = new Race("High Elf");

            race.AddPlayerType( "Blitzer", new PlayerStats( 7, 3, 4, 8 ), new Skill( "Block", "Blocks" ) );
            race.AddPlayerType( "Catcher", new PlayerStats( 8, 3, 4, 7 ), new Skill( "Catch", "Catches" ) );
            race.AddPlayerType( "Thrower", new PlayerStats( 6, 3, 4, 8 ), new Skill( "Safe Throw", "Throws" ), new Skill( "Pass", "Passes" ) );
            race.AddPlayerType( "Lineman", new PlayerStats( 6, 3, 4, 8 ));

            return race;
        }
    }
}
