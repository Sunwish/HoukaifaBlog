using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using HoukaifaBlog.Core.Entities;
using HoukaifaBlog.Core.Interfaces;
using HoukaifaBlog.Infrastructure.Database;
using HoukaifaBlog.Infrastructure.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HoukaifaBlog.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;
        private readonly ILogger logger;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUrlHelper urlHelper)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
            this.logger = loggerFactory.CreateLogger("HoukaifaBlog.Api.Controllers.PostController");
        }

        /// <summary>
        /// Get all post of one page.
        /// </summary>
        /// <param name="postParameters"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetPosts")]
        public async Task<IActionResult> Get(PostParameters postParameters)
        {
            // Get the certain page's Post list and map it to PostResource list
            var postList = await postRepository.GetAllPostAsync(postParameters);
            var postResources = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(postList);

            // Get prev and next page uri by current page parameters
            var previousPageLink = postList.HasPrevious ?
                CreatePostUri(postParameters, PaginationResourceUriType.PreviousPage) : null;
            var NextPageLink = postList.HasNext?
                CreatePostUri(postParameters, PaginationResourceUriType.NextPage) : null;

            // Save page turning meta info
            var meta = new
            {
                pageSize = postList.PageSize,
                pageIndex = postList.PageIndex,
                totalItemsCount = postList.TotalItemsCount,
                pageCount = postList.PageCount,
                previousPageLink,
                NextPageLink
            };

            // Add meta info to response header
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta));

            return Ok(postResources);
        }

        /// <summary>
        /// Get a certain post by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await postRepository.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            var postResource = mapper.Map<Post, PostResource>(post);

            return Ok(postResource);
        }

        /// <summary>
        /// Add a test post to database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // throw new NotImplementedException();
            // TODO: THE CODE BELOW IS JUST FOR TESTING

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

        /// <summary>
        /// Create Next or Previous page uri by current page parameters.
        /// </summary>
        /// <param name="postParameters"></param>
        /// <param name="uriType"></param>
        /// <returns></returns>
        private string CreatePostUri(PostParameters postParameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = postParameters.PageIndex - 1,
                        pageSize = postParameters.PageSize,
                        orderBy = postParameters.OrderBy,
                        fields = postParameters.Fields
                    };
                    return urlHelper.Link("GetPosts", previousParameters);
                case PaginationResourceUriType.NextPage:
                    var NextParameters = new
                    {
                        pageIndex = postParameters.PageIndex + 1,
                        pageSize = postParameters.PageSize,
                        orderBy = postParameters.OrderBy,
                        fields = postParameters.Fields
                    };
                    return urlHelper.Link("GetPosts", NextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = postParameters.PageIndex,
                        pageSize = postParameters.PageSize,
                        orderBy = postParameters.OrderBy,
                        fields = postParameters.Fields
                    };
                    return urlHelper.Link("GetPosts", currentParameters);
            }
        }
    }
}
