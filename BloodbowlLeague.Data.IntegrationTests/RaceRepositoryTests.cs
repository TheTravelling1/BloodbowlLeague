﻿using System;
using System.IO;
using BloodbowlLeague.Logic;
using LiteDB;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace BloodbowlLeague.Data.IntegrationTests
{
    [TestFixture]
    public class RaceRepositoryTests
    {
        private static readonly string TempFilePath = Path.Combine( Path.GetTempPath(), Guid.NewGuid() + ".tmp" );

        private IRaceRepository _raceRepository;
        private readonly StandardKernel _container = new StandardKernel( new LiteDbModule( TempFilePath ) );

        [SetUp]
        public void SetUp()
        {
            _raceRepository = _container.Get<IRaceRepository>();
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
            var toSave = new Race( "High Elf" );

            _raceRepository.Save( toSave );

            RaceStorage fromDb;
            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<RaceStorage>( "races" );

                fromDb = col.FindOne( t => t.Name == "High Elf" );
            }

            fromDb.Name.ShouldBe( "High Elf" );
        }

        [Test]
        public void Given_a_previously_saved_race__When_retrieving_it__Then_it_is_retrieved_correctly()
        {
            var toSave = CreateRace("High Elf");

            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<RaceStorage>( "races" );
                col.Insert(toSave);
            }

            var fromDb = _raceRepository.Get( "High Elf");
            fromDb.Name.ShouldBe( "High Elf" );
        }

        [Test]
        public void Given_previously_saved_raced__When_retrieving_them__Then_all_are_retrieved()
        {
            var highElf = CreateRace("High Elf");
            var orc = CreateRace( "Orc");

            using ( var db = new LiteDatabase( TempFilePath ) )
            {
                var col = db.GetCollection<RaceStorage>( "races" );
                col.Insert(highElf);
                col.Insert(orc);
            }

            var fromDb = _raceRepository.GetAll();

            fromDb.Count.ShouldBe(2);
            fromDb.ShouldContain(r => r.Name == "High Elf");
            fromDb.ShouldContain(r => r.Name == "Orc");
        }

        private static RaceStorage CreateRace(string name)
        {
            var toSave = new RaceStorage
            {
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
