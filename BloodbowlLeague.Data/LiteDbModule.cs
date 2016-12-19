using System.Linq;
using AutoMapper;
using BloodbowlLeague.Logic;
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
            Mapper.Initialize( c => {
                c.CreateMap<Team, TeamStorage>().ForMember( m => m.Id, m => m.Ignore() );
                c.CreateMap<TeamStorage, Team>();

                c.CreateMap<Player, PlayerStorage>().ReverseMap();

                c.CreateMap<Skill, SkillStorage>().ReverseMap();

                c.CreateMap<PlayerStats, PlayerStatStorage>();
                c.CreateMap<PlayerStatStorage, PlayerStats>().ConstructUsing( s => new PlayerStats( s.MovementAllowance, s.Strength, s.Agility, s.ArmourValue ) );

                c.CreateMap<PlayerType, PlayerTypeStorage>().ReverseMap();

                c.CreateMap<Race, RaceStorage>().ForMember( m => m.Id, m => m.Ignore() );
                c.CreateMap<RaceStorage, Race>();
            } );

            Mapper.AssertConfigurationIsValid();

            Bind<ITeamRepository>()
                .ToConstant( new TeamRepository( _filePath ) )
                .InSingletonScope();

            Bind<IRaceRepository>()
                .ToConstant( new RaceRepository( _filePath ) )
                .InSingletonScope();

            Bind<ISkillRepository>()
                .ToConstant( new SkillRepository( _filePath ) )
                .InSingletonScope();
        }
    }
}