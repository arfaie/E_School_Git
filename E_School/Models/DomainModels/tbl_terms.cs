//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_School.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_terms
    {
        public int idTerm { get; set; }
        public int idYear { get; set; }
        public string termName { get; set; }
        public Nullable<int> termStart { get; set; }
        public Nullable<int> termEnd { get; set; }
        public string des { get; set; }
    }
}
