using GC.MFI.DataAccess;
using GC.MFI.Models.DbModels;

namespace GC.MFI.DataAccess.InfrastructureBase
{
    public class DatabaseFactory: Disposable,IDatabaseFactory
    {
       private BntPOSContext dataContext;
      //  private BntPOSContext posContext;

        public DatabaseFactory(BntPOSContext dataContext)
        {
            this.dataContext = dataContext;
            //this.posContext = posContext;
        }

        //public ApplicationDbContext Get()
        //{
        //    return dataContext;
        //}

        public BntPOSContext Get()
        {
            return dataContext;
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
