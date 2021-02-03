using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Entities;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HoukaifaBlog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyContext myContext;

        public PostRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            return await myContext.Posts.ToListAsync();
        }
    }
}
