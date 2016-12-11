namespace BloodbowlLeague.Logic
{
  public class TeamFactory
  {
    public Team CreateTeam( string teamName )
    {
      return new Team( teamName );
    }
  }
}
