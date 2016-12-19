using System;
using System.IO;
using BloodbowlLeague.Logic;
using LiteDB;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.Data.IntegrationTests
{
    [TestFixture]
    public class SkillRepositoryTests
    {
        private static readonly string TempFilePath = Path.Combine( Path.GetTempPath(), Guid.NewGuid() + ".tmp" );

        private ISkillRepository _skillRepository;
        private readonly StandardKernel _container = new StandardKernel( new LiteDbModule( TempFilePath ) );

        [SetUp]
        public void SetUp()
        {
            _skillRepository = _container.Get<ISkillRepository>();
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
        public void When_saving_a_race__Then_it_should_be_persisted()
        {
            var toSave = new Skill( "Block", "Blocks Something" );

            _skillRepository.Save( toSave );

            SkillStorage fromDb;
            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<SkillStorage>( "skills" );

                fromDb = col.FindOne( t => t.Name == "Block" );
            }

            fromDb.Name.ShouldBe( "Block" );
            fromDb.Description.ShouldBe( "Blocks Something" );
        }

        [Test]
        public void Given_previously_saved_raced__When_retrieving_them__Then_all_are_retrieved()
        {
            var block = new SkillStorage { Name = "Block", Description = "Blocks Something" };
            var safethrow = new SkillStorage { Name = "Safe Throw", Description = "Throws Something" };

            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<SkillStorage>( "skills" );
                col.Insert( block );
                col.Insert( safethrow );
            }

            var fromDb = _skillRepository.GetAll();

            fromDb.Count.ShouldBe( 2 );
            fromDb.ShouldContain( r => r.Name == "Block" && r.Description == "Blocks Something" );
            fromDb.ShouldContain( r => r.Name == "Safe Throw" && r.Description == "Throws Something" );
        }

        private static RaceStorage CreateRace( string name )
        {
            var toSave = new RaceStorage {
                Name = name,
                PlayerTypes = new[]
                {
                    new PlayerTypeStorage
                    {
                        Name = "Blitzer",
                        BaseSkills = new[] {"Block"},
                        BaseStats = new PlayerStatStorage {MovementAllowance = 7, Agility = 4, ArmourValue = 8, Strength = 3},
                        Race = "High Elf"
                    }
                }
            };
            return toSave;
        }
    }
}
