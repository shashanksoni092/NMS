//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGroupMessage
    {
        public int GroupID { get; set; }
        public string USN { get; set; }
        public string message { get; set; }
        public System.TimeSpan Time { get; set; }
        public byte[] Media { get; set; }
    
        public virtual NGroup NGroup { get; set; }
        public virtual Student Student { get; set; }
    }
}
