using System;
using System.Collections.Generic;

namespace API.Models
{
    public class History
    {
        public long Id { set; get; }
        public string Activity { set; get; }
        public string CardId { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string State { set; get; }
        public DateTime Date { set; get; }
        public string[] Photos { set; get; }

        protected History(){}
        public History(
            long id,
            string activity,
            string cardid,
            string name,
            string address,
            string state,
            DateTime date,
            string[] photos)
        {
            Id = id;
            Activity = activity;
            CardId = cardid;
            Name = name;
            Address = address;
            State = state;
            Date = date;
            Photos = photos;
        }
    }
}