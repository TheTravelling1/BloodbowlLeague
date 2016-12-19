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
    public class TeamRepositoryTests
    {
        private static readonly string TempFilePath = Path.Combine( Path.GetTempPath(), Guid.NewGuid() + ".tmp" );

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
        public void When_saving_a_team_Then_its_name_should_be_persisted()
        {
            var toSave = new Team( "High and Mighty" );

            _teamRepository.SaveTeam( toSave );

            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<TeamStorage>( "teams" );

                col.Exists( t => t.Name == "High and Mighty" ).ShouldBeTrue();
            }
        }
    }
}
