using System;

namespace AutogateAPI.Models
{
    public class Roles
    {
        public long Id { protected set; get; }
        public string Name { protected set; get; }
        public Roles(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}