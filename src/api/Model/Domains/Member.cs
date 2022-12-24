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
        public string CardId { set; get; }
        public string FotoProfile { set; get; }
        public bool Active { set; get; }

        protected Member(){}
        
        public Member(
            long id,
            string fullname,
            string username,
            string password,
            string address,
            string cardId,
            string fotoProfile,
            bool active)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Password = password;
            Address = address;
            CardId = cardId;
            FotoProfile = fotoProfile;
            Active = active;
        }
        public void Changes(
            string fullname,
            string address,
            string cardId,
            string fotoProfile,
            bool active)
        {
            Fullname = fullname;
            Address = address;
            CardId = cardId;
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