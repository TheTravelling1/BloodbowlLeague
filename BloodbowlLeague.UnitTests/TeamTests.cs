using System.Linq;
using BloodbowlLeague.Logic;
using BloodbowlLeague.Logic.Team;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.UnitTests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void When_creating_a_new_team__Then_it_is_created_correctly()
        {
            var team = new Team( "High and Mighty", "High Elf" );

            team.Name.ShouldBe( "High and Mighty" );
            team.Race.ShouldBe( "High Elf" );
        }

        [Test]
        public void When_adding_a_player_to_a_team__Then_is_it_created_correctly()
        {
            var team = new Team( "High and Mighty", "High Elf" );
            var highElf = new MockRaceRepository().GetRace( "High Elf" );

            var blitzer = highElf.PlayerTypes.Single( pt => pt.Name == "Blitzer" );
            team.AddPlayer( blitzer, "Verwarthil Undomiel" );

            team.Players.ShouldHaveSingleItem();
            team.Players.Single().Name.ShouldBe("Verwarthil Undomiel");
            team.Players.Single().Type.ShouldBe(blitzer.Name);
            team.Players.Single().BaseStats.ShouldBe(blitzer.BaseStats);
            team.Players.Single().Stats.ShouldBe(blitzer.BaseStats);
            team.Players.Single().Skills.ShouldBe(blitzer.BaseSkills);
            team.Players.Single().BaseSkills.ShouldBe(blitzer.BaseSkills);
        }

        [Test]
        public void Given_a_playerType_of_a_different_race_When_adding_a_player_to_a_team__Then_an_exception_is_thrown()
        {
            var team = new Team( "The Terminal Show", "Human" );
            var highElf = new MockRaceRepository().GetRace( "High Elf" );
            
            Should.Throw<IncorrectRaceException>( () => team.AddPlayer( highElf.PlayerTypes.Single( pt => pt.Name == "Blitzer" ), "Verwarthil Undomiel" ) );
        }
    }
}
