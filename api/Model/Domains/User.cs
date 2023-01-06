using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Extentions;

namespace API.Models
{
    public class User
    {
        public long Id { set; get; }
        public string Fullname { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public bool Active { set; get; }

        public User(
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

        public void Changes(
            string fullname,
            string role,
            bool active)
        {
            this.Fullname = fullname;
            this.Role = role;
            this.Active = active;
        }

        public void ChangePassword(string password)
        {
            if(!string.IsNullOrEmpty(password))
                this.Password = password.Hashing();
        }
    }
}