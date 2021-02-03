using System;
using System.Collections.Generic;
using System.Text;
using HoukaifaBlog.Core.Interfaces;

namespace HoukaifaBlog.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
