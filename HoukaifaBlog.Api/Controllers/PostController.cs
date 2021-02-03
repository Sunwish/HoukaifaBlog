using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoukaifaBlog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoukaifaBlog.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly MyContext myContext;

        public PostController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task<IActionResult> Get()
        {
            var posts = await myContext.Posts.ToListAsync();

            return Ok(posts);
        }
    }
}
