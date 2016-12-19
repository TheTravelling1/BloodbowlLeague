using System;
using System.IO;
using System.Linq;
using BloodbowlLeague.Logic;
using LiteDB;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.Data.IntegrationTests
{
    [TestFixture]
    public class TeamRepositoryTests
    {
        private static readonly string TempFilePath = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString(), Guid.NewGuid() + ".tmp" );

        private ITeamRepository _teamRepository;
        private readonly StandardKernel _container = new StandardKernel( new LiteDbModule( TempFilePath ) );

        [SetUp]
        public void SetUp()
        {
            _teamRepository = _container.Get<ITeamRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if ( File.Exists( TempFilePath ) )
            {
                File.Delete( TempFilePath );
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _container?.Dispose();
        }

        [Test]
        public void When_saving_a_team__Then_it_should_be_persisted()
        {
            var toSave = new Team( "High and Mighty", "High Elf" );
            var playerType = new PlayerType( "Blitzer", "High Elf", new PlayerStats( 1, 2, 3, 4 ), new[] { "Block" } );
            toSave.AddPlayer( playerType, "Mikul Maviv" );

            _teamRepository.Save( toSave );

            TeamStorage fromDb;
            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );

                fromDb = col.FindOne( t => t.Name == "High and Mighty" );
            }

            AssertThatTeamStorageMatchesTeam( fromDb, toSave );
        }

        [Test]
        public void Given_a_previously_saved_team_with_players__When_retrieving_it__Then_it_should_retrieve_successfully()
        {
            var toSave = CreatePlayerStorage();
            SavePlayerStorage( toSave );

            var fromDb = _teamRepository.Get( "High and Mighty" );

            AssertThatTeamMatchesTeamStorage( fromDb, toSave );
        }

        private void AssertThatTeamStorageMatchesTeam( TeamStorage fromDb, Team toSave )
        {
            fromDb.Name.ShouldBe( toSave.Name );
            fromDb.Race.ShouldBe( toSave.Race );
            fromDb.Players.Length.ShouldBe( toSave.Players.Count );

            foreach ( var playerToCheck in toSave.Players )
            {
                var player = fromDb.Players.SingleOrDefault( p => p.Name == playerToCheck.Name );

                player.ShouldNotBeNull( "The team in the database did not contain a player with name " + playerToCheck.Name );
                player.Type.ShouldBe( playerToCheck.Type );

                player.BaseStats.MovementAllowance.ShouldBe( playerToCheck.BaseStats.MovementAllowance );
                player.BaseStats.Strength.ShouldBe( playerToCheck.BaseStats.Strength );
                player.BaseStats.Agility.ShouldBe( playerToCheck.BaseStats.Agility );
                player.BaseStats.ArmourValue.ShouldBe( playerToCheck.BaseStats.ArmourValue );
                player.BaseSkills.ShouldBe( playerToCheck.BaseSkills );

                player.Stats.MovementAllowance.ShouldBe( playerToCheck.Stats.MovementAllowance );
                player.Stats.Strength.ShouldBe( playerToCheck.Stats.Strength );
                player.Stats.Agility.ShouldBe( playerToCheck.Stats.Agility );
                player.Stats.ArmourValue.ShouldBe( playerToCheck.Stats.ArmourValue );
                player.Skills.ShouldBe( playerToCheck.Skills );
            }
        }

        private static void AssertThatTeamMatchesTeamStorage( Team fromDb, TeamStorage toSave )
        {
            fromDb.Name.ShouldBe( toSave.Name );
            fromDb.Race.ShouldBe( toSave.Race );
            fromDb.Players.Count.ShouldBe( toSave.Players.Length );
            foreach ( var playerToCheck in toSave.Players )
            {
                var player = fromDb.Players.SingleOrDefault( p => p.Name == playerToCheck.Name );

                player.ShouldNotBeNull( "The team in the database did not contain a player with name " + playerToCheck.Name );
                player.Type.ShouldBe( playerToCheck.Type );

                player.BaseStats.MovementAllowance.ShouldBe( playerToCheck.BaseStats.MovementAllowance );
                player.BaseStats.Strength.ShouldBe( playerToCheck.BaseStats.Strength );
                player.BaseStats.Agility.ShouldBe( playerToCheck.BaseStats.Agility );
                player.BaseStats.ArmourValue.ShouldBe( playerToCheck.BaseStats.ArmourValue );
                player.BaseSkills.ShouldBe( playerToCheck.BaseSkills );

                player.Stats.MovementAllowance.ShouldBe( playerToCheck.Stats.MovementAllowance );
                player.Stats.Strength.ShouldBe( playerToCheck.Stats.Strength );
                player.Stats.Agility.ShouldBe( playerToCheck.Stats.Agility );
                player.Stats.ArmourValue.ShouldBe( playerToCheck.Stats.ArmourValue );
                player.Skills.ShouldBe( playerToCheck.Skills );
            }
        }

        private static void SavePlayerStorage( TeamStorage toSave )
        {
            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );

                col.Insert( toSave );
            }
        }

        private static TeamStorage CreatePlayerStorage()
        {
            return new TeamStorage {
                Id = 1,
                Name = "High and Mighty",
                Players = new[]
                {
                    new PlayerStorage
                    {
                        Name = "Mikul Maviv",
                        Type = "Blitzer",
                        BaseStats = new PlayerStatStorage {MovementAllowance = 1, Strength = 2, Agility = 3, ArmourValue = 4},
                        BaseSkills = new[] { "Block" },
                        Stats = new PlayerStatStorage {MovementAllowance = 9, Strength = 8, Agility = 7, ArmourValue = 6},
                        Skills = new[] { "Block", "Safe Throw" }
                    }
            }
            };
        }
    }
}
