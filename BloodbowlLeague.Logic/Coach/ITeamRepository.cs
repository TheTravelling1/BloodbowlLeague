namespace BloodbowlLeague.Logic
{
    public interface ICoachRepository
    {
        void Save( Coach toSave );
        Coach Get(string teamName);
    }
}
