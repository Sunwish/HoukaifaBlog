﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoukaifaBlog.Core.Entities;

namespace HoukaifaBlog.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<PaginatedList<Post>> GetAllPostAsync(PostParameters postParameters);

        Task<Post> GetPostByIdAsync(int id);

        void AddPost(Post post);
    }
}
