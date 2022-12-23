using System;

namespace AutogateAPI.Models
{
    public class Histories
    {
        public long Id { protected set; get; }
        public string Activity { protected set; get; }
        public string CardId { protected set; get; }
        public string Name { protected set; get; }
        public string Address { protected set; get; }
        public DateTime Date { protected set; get; }

        protected Histories()
        { }

        public Histories(long id, string activity, string cardId, string name, string address, DateTime date)
        {
            Id = id;
            Activity = activity;
            CardId = cardId;
            Name = name;
            Address = address;
            Date = date;
        }
    }
}