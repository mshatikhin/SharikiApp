using System;
using System.Collections.Generic;
using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class Good
    {
        public Balloon Balloon { get; set; }
        public int Count { get; set; }

        public decimal PriceWithDiscount
        {
            get
            {
                var priceWithDiscount = GetPrice(this.Balloon.Price ?? 0, this.Count, this.Balloon.Discount);
                return priceWithDiscount;
            }
        }

        private static decimal GetPrice(decimal price, int count, string discountString)
        {
            var discountRange = new List<Discount>();
            if (!string.IsNullOrEmpty(discountString))
            {
                var discounts = discountString.Split(';')
                    .Where(d => d != "");

                foreach (var disc in discounts)
                {
                    var p = disc.Split(':');
                    if (p.Length == 2)
                    {
                        var counts = p[0].Split('-');
                        switch (counts.Length)
                        {
                            case 1:
                                discountRange.Add(new Discount
                                {
                                    StartValue = Convert.ToInt32(counts[0]),
                                    MaxValue = int.MaxValue,
                                    Price = Convert.ToInt32(p[1])
                                });
                                break;
                            case 2:
                                discountRange.Add(new Discount
                                {
                                    StartValue = Convert.ToInt32(counts[0]),
                                    MaxValue = string.IsNullOrEmpty(counts[1]) ? int.MaxValue : Convert.ToInt32(counts[1]),
                                    Price = Convert.ToInt32(p[1])
                                });
                                break;
                        }
                    }
                }
                var fitDiscountRange = discountRange
                    .Where(d => count >= d.StartValue)
                    .OrderByDescending(b => b.MaxValue)
                    .ToArray();

                if (fitDiscountRange.Any())
                {
                    var discount = fitDiscountRange.First();
                    return discount.Price;
                }
            }
            return price;
        }
    }
}