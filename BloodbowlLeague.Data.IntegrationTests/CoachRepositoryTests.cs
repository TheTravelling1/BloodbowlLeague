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
    public class CoachRepositoryTests
    {
        private static readonly string TempFilePath = Path.Combine( Path.GetTempPath(), Guid.NewGuid().ToString(), Guid.NewGuid() + ".tmp" );

        private ICoachRepository _coachRepository;
        private readonly StandardKernel _container = new StandardKernel( new LiteDbModule( TempFilePath ) );

        [SetUp]
        public void SetUp()
        {
            _coachRepository = _container.Get<ICoachRepository>();
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
        public void When_saving_a_coach__Then_it_should_be_persisted()
        {
            var toSave = new Coach( "Pete", "bloodbowl@peter-stephenson.co.uk", new byte[] { 0, 1, 2, 3, 4 }, new[] { "High and Mighty" } );

            _coachRepository.Save( toSave );

            CoachStorage fromDb;
            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<CoachStorage>( "coaches" );

                fromDb = col.FindOne( t => t.Name == "Pete" );
            }

            fromDb.Name.ShouldBe( toSave.Name );
            fromDb.EmailAddress.ShouldBe( toSave.EmailAddress );
            fromDb.HashedPassword.ShouldBe( toSave.HashedPassword );
            fromDb.Teams.ShouldBe( toSave.Teams );
        }

        [Test]
        public void Given_a_previously_saved_team_with_players__When_retrieving_it__Then_it_should_retrieve_successfully()
        {
            var toSave = new CoachStorage {
                Id = 1,
                Name = "Pete",
                EmailAddress = "bloodbowl@peter-stephenson.co.uk",
                Teams = new[] { "High and Mighty" },
                HashedPassword = new byte[] { 0, 1, 2, 3, 4 }
            };

            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<CoachStorage>( "coaches" );
                col.Insert( toSave );
            }

            var fromDb = _coachRepository.Get( "Pete" );

            fromDb.Name.ShouldBe( toSave.Name );
            fromDb.EmailAddress.ShouldBe( toSave.EmailAddress );
            fromDb.HashedPassword.ShouldBe( toSave.HashedPassword );
            fromDb.Teams.ShouldBe( toSave.Teams );
        }
    }
}
