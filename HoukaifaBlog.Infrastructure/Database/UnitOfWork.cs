using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Interfaces;

namespace HoukaifaBlog.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext myContext;

        public UnitOfWork(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task<bool> SaveAsync()
        {
            return await myContext.SaveChangesAsync() > 0;
        }
    }
}
