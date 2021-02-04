using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddPost(Post post)
        {
            myContext.Posts.Add(post);
        }

        public async Task<PaginatedList<Post>> GetAllPostAsync(PostParameters postParameters)
        {
            var query = myContext.Posts.OrderBy(x => x.Id);

            var count = await query.CountAsync();

            var data = await query
                .Skip(postParameters.PageIndex * postParameters.PageSize)
                .Take(postParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<Post>(postParameters.PageIndex, postParameters.PageSize, count, data);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await myContext.Posts.FindAsync(id);
        }
    }
}
