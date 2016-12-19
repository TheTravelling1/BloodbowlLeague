using System.Collections.Generic;

namespace BloodbowlLeague.Logic
{
    public interface IRaceRepository
    {
        Race Get( string name );

        IReadOnlyCollection<Race> GetAll();

        void Save(Race toSave);
    }
}
