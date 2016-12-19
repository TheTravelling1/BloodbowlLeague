using System.Collections.Generic;

namespace BloodbowlLeague.Logic.Race
{
    public interface IRaceRepository
    {
        Race Get( string name );

        IReadOnlyCollection<Race> GetAll();

        void Save(Race toSave);
    }
}
