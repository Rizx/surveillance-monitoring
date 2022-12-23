using System;

namespace AutogateAPI.Models
{
    public class Users
    {
        public long Id { protected set; get; }
        public string Fullname { protected set; get; }
        public string Username { protected set; get; }
        public string Password { protected set; get; }
        public string Role { protected set; get; }
        public bool Active { protected set; get; }

        public Users(
            long id,
            string fullname,
            string username,
            string password,
            string role,
            bool active)
        {
            this.Id = id;
            this.Fullname = fullname;
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.Active = active;
        }

        internal void Changes(
            string fullname,
            string username,
            string password,
            string role,
            bool active)
        {
            if (!string.IsNullOrEmpty(password))
                this.Password = password;

            this.Fullname = fullname;
            this.Username = username;
            this.Role = role;
            this.Active = active;
        }
    }
}