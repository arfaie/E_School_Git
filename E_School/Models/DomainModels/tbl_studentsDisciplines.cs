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
    
    public partial class tbl_studentsDisciplines
    {
        public int idStudentDisciplines { get; set; }
        public int idDisType { get; set; }
        public int idStudent { get; set; }
        public int actDate { get; set; }
        public int idTerm { get; set; }
        public string des { get; set; }
        public bool justified { get; set; }
    }
}
