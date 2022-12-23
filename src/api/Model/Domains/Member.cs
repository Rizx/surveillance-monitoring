using API.Extentions;

namespace API.Models
{
    public class Member
    {
        public long Id { set; get; }
        public string Fullname { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Address { set; get; }
        public string CardNumber { set; get; }
        public string FotoProfile { set; get; }
        public bool Active { set; get; }

        public Member(
            long id,
            string fullname,
            string username,
            string password,
            string address,
            string cardNumber,
            string fotoProfile,
            bool active)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Password = password;
            Address = address;
            CardNumber = cardNumber;
            FotoProfile = fotoProfile;
            Active = active;
        }
        public void Changes(
            string fullname,
            string address,
            string cardNumber,
            string fotoProfile,
            bool active)
        {
            Fullname = fullname;
            Address = address;
            CardNumber = cardNumber;
            FotoProfile = fotoProfile;
            Active = active;
        }

        public void ChangePassword(string password)
        {
            if(!string.IsNullOrEmpty(password))
                Password = password.Hashing();
        }
    }
}