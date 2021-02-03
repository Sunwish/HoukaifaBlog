using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoukaifaBlog.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
