//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SharikiApp.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Balloon
    {
        public int BalloonId { get; set; }
        public int BalloonTypeId { get; set; }
        public string Description { get; set; }
        public string BalloonImage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Name { get; set; }
        public string AdditionalDescription { get; set; }
        public bool Hide { get; set; }
        public int Ordering { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string DiscountDescription { get; set; }
        public string Discount { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
    
        public virtual BalloonType BalloonType { get; set; }
    }
}
