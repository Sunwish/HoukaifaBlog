using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoukaifaBlog.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly MyContext myContext;
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPostAsync();

            return Ok(posts);
        }
    }
}
