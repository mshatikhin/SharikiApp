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
    
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string Path { get; set; }
        public string Extention { get; set; }
        public int AlbumId { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Album Album { get; set; }
    }
}
