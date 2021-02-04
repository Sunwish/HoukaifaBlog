using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HoukaifaBlog.Core.Entities;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using HoukaifaBlog.Infrastructure.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HoukaifaBlog.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = loggerFactory.CreateLogger("HoukaifaBlog.Api.Controllers.PostController");
        }

        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPostAsync();

            logger.LogInformation("Get all posts.....");

            var postResources = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

            // throw new Exception("Globle Error Handler Test!!!!!");

            return Ok(postResources);
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
