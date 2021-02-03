using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Entities;
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
        private readonly IUnitOfWork unitOfWork;

        public PostController(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPostAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            postRepository.AddPost(new Post
            {
                Title = "Test add",
                Body = "Test body",
                Author = "Sunwish",
                LastModified = DateTime.Now
            });

            await unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
