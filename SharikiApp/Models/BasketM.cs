using System;
using System.Collections.Generic;

namespace SharikiApp.Models
{
    public class BasketM
    {
        public List<Good> Goods { get; set; }
        public Guid BasketId { get; set; }
        public string From { get; set; }
        public DateTime DateCreated { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string DateTo { get; set; }
        public string AddressTo { get; set; }
    }
}