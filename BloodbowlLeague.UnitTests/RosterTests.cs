using BloodbowlLeague.Logic;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.UnitTests
{
  [TestFixture]
  public class RosterTests
  {
    [Test]
    public void Should_be_able_to_create_a_team_with_a_name()
    {
      var factory = new TeamFactory();

      var team = factory.CreateTeam( "Mikul's Merauders" );

      team.Name.ShouldBe( "Mikul's Merauders" );
    }
  }
}