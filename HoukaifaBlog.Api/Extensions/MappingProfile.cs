using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HoukaifaBlog.Core.Entities;
using HoukaifaBlog.Infrastructure.Resources;

namespace HoukaifaBlog.Api.Extensions
{
    public class MappingProfile : Profile
    {
        // Map Entities to Resources
        public MappingProfile()
        {
            // Map Post to PostResource
            CreateMap<Post, PostResource>()
                // Map Post.LastModified to PostResource.UpdateTime
                .ForMember(dist => dist.UpdateTime, opt => opt.MapFrom(src => src.LastModified));

            // Map PostResource to Post
            CreateMap<PostResource, Post>();
        }
    }
}
