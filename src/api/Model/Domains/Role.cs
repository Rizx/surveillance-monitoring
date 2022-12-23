using System;

namespace API.Models
{
    public class Role
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public Role(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}