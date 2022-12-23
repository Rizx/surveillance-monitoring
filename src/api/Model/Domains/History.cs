using System;

namespace API.Models
{
    public class History
    {
        public long Id { set; get; }
        public string Activity { set; get; }
        public string CardId { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public DateTime Date { set; get; }

        protected History()
        { }

        public History(long id, string activity, string cardId, string name, string address, DateTime date)
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