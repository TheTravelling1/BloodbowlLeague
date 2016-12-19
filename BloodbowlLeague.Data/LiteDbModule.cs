using AutoMapper;
using BloodbowlLeague.Logic;
using BloodbowlLeague.Logic.Team;
using Ninject.Modules;

namespace BloodbowlLeague.Data
{
    public class LiteDbModule: NinjectModule
    {
        private readonly string _filePath;

        public LiteDbModule( string filePath )
        {
            _filePath = filePath;
        }

        public override void Load()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Team, TeamStorage>();
                c.CreateMap<TeamStorage, Team>();
            });

            Bind<ITeamRepository>()
                .ToConstant(new TeamRepository(_filePath))
                .InSingletonScope();
        }
    }
}
