using System;

namespace AutogateAPI.Models
{
    public class Pictures
    {
        public long Id { protected set; get; }
        public long TransactionId { protected set; get; }
        public string Path { protected set; get; }

        protected Pictures()
        { }

        public Pictures(
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