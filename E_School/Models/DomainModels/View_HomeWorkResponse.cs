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
    
    public partial class View_HomeWorkResponse
    {
        public int idRecHomeWork { get; set; }
        public int idHomeWork { get; set; }
        public string answerFile { get; set; }
        public Nullable<double> score { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int dateTime { get; set; }
        public int idStudent { get; set; }
        public string answerBody { get; set; }
    }
}