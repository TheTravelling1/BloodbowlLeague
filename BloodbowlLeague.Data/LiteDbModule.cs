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
            Bind<ITeamRepository>()
                .ToConstant(new TeamRepository(_filePath))
                .InSingletonScope();
        }
    }
}
