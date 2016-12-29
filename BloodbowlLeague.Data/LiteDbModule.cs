using System;
using System.IO;
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
            var directory = Path.GetDirectoryName( _filePath );
            if (directory == null) throw new ArgumentNullException(nameof(directory));
            
            if ( !Directory.Exists( directory ) )
            {
                Directory.CreateDirectory(directory);
            }

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

                c.CreateMap<Coach, CoachStorage>().ForMember( m => m.Id, m => m.Ignore() );
                c.CreateMap<CoachStorage, Coach>();
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

            Bind<ICoachRepository>()
                .ToConstant( new CoachRepository( _filePath ) )
                .InSingletonScope();
        }
    }
}