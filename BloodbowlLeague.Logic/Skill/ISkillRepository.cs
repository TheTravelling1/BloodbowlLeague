using System.Collections.Generic;

namespace BloodbowlLeague.Logic
{
    public interface ISkillRepository
    {
        void Save(Skill toSave);
        IReadOnlyCollection<Skill> GetAll();
    }
}
