using E_School.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_School.Models.ViewModel.Student
{
    public class obAverage
    {
        public int id { get; set; }
        public int idClass { get; set; }
        public int idDesScore { get; set; }
        public int idLesson { get; set; }
        public int idMonth { get; set; }
        public int idStudent { get; set; }
        public double Score { get; set; }
        public string lessonName { get; set; }
        public string desScore { get; set; }
    }
    public class AverageViewModel
    {
        public List<obAverage> obAverage { get;set; }
        public int Rate { get; set; }
        public string Average { get; set; }
    }
}