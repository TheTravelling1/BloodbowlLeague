using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BloodbowlLeague.Logic;
using LiteDB;

namespace BloodbowlLeague.Data
{
    public class SkillRepository: ISkillRepository
    {
        private readonly string _filePath;

        public SkillRepository( string filePath )
        {
            _filePath = filePath;
        }

        public void Save( Skill toSave )
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<SkillStorage>( "skills" );
                var storageObj = Mapper.Map<SkillStorage>( toSave );
                col.Insert( storageObj );
            }
        }

        public IReadOnlyCollection<Skill> GetAll()
        {
            using ( var db = new LiteDatabase( _filePath ) )
            {
                var col = db.GetCollection<SkillStorage>( "skills" );
                var fromDb = col.FindAll();
                return fromDb.Select( Mapper.Map<Skill> ).ToList();
            }
        }
    }
}