//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompetitiveRankingWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Playthrough
    {
        public int PlaythroughID { get; set; }
        public int Game_ID { get; set; }
        public int Platform_ID { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<decimal> Time { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
