using System.Linq;
using BloodbowlLeague.Logic;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.UnitTests
{
    [TestFixture]
    public class RaceTests
    {
        [Test]
        public void When_adding_a_valid_player_type_to_a_race__Then_it_is_added_correctly()
        {
            var highElf = new Race( "High Elf" );

            var playerStats = new PlayerStats( 7, 3, 4, 8 );
            var block = new Skill( "Block", "Blocks" );

            highElf.AddPlayerType( "Blitzer", playerStats, block );

            highElf.PlayerTypes.ShouldHaveSingleItem();
            highElf.PlayerTypes.Single().Name.ShouldBe( "Blitzer" );
            highElf.PlayerTypes.Single().BaseStats.ShouldBeSameAs( playerStats );
            highElf.PlayerTypes.Single().BaseSkills.ShouldBe( new[] { "Block" } );
            highElf.PlayerTypes.Single().Race.ShouldBe( "High Elf" );
        }
    }
}