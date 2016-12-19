namespace BloodbowlLeague.Logic
{
    public interface ITeamRepository
    {
        void Save( Team toSave );
        Team Get(string teamName);
    }
}
