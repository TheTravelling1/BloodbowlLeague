using System.Linq;
using AutoMapper;
using BloodbowlLeague.Logic.Team;
using BloodbowlLeague.Logic.Values;
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
                c.CreateMap<Team, TeamStorage>()
                    .ForMember( m => m.Id, m => m.Ignore() );
                //.ForMember( m => m.Players, m => m.ResolveUsing(ts => ts.Players.Select(Mapper.Map<PlayerStorage>)) );

                c.CreateMap<TeamStorage, Team>()
                    .ConstructUsing( ts => new Team( ts.Name, ts.Race, ts.Players.Select( Mapper.Map<Player> ) ) );

                c.CreateMap<Player, PlayerStorage>();

                c.CreateMap<PlayerStorage, Player>()
                    .ConstructUsing(
                        ps => new Player(
                            ps.Name,
                            ps.Type,
                            new PlayerStats( ps.BaseStats.MovementAllowance, ps.BaseStats.Strength, ps.BaseStats.Agility, ps.BaseStats.ArmourValue ),
                            ps.BaseSkills.Select( Mapper.Map<Skill> ),
                            new PlayerStats( ps.Stats.MovementAllowance, ps.Stats.Strength, ps.Stats.Agility, ps.Stats.ArmourValue ),
                            ps.Skills.Select( Mapper.Map<Skill> )
                        )
                    );

                c.CreateMap<Skill, SkillStorage>();
                c.CreateMap<SkillStorage, Skill>();

                c.CreateMap<PlayerStats, StatStorage>();
                c.CreateMap<StatStorage, PlayerStats>();
            } );

            Mapper.AssertConfigurationIsValid();

            Bind<ITeamRepository>()
                .ToConstant( new TeamRepository( _filePath ) )
                .InSingletonScope();
        }
    }
}
