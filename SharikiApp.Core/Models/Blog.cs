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
    
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Header { get; set; }
        public string ContentText { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    
        public virtual User User { get; set; }
    }
}
