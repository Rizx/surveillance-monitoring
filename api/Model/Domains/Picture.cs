using System;

namespace API.Models
{
    public class Picture
    {
        public long Id { set; get; }
        public long TransactionId { set; get; }
        public string Path { set; get; }

        protected Picture()
        { }

        public Picture(
            long id,
            long transactionId,
            string path)
        {
            this.Id = id;
            this.TransactionId = transactionId;
            this.Path = path;
        }
    }
}