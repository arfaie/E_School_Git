﻿using E_School.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_School.Models.ViewModel.Student
{
    public class AvrageScore
    {
        public List<int> id { get; set; } = new List<int>();
        public List<int> idStudent { get; set; } = new List<int>();
        public List<int> idDescriptiveScore { get; set; } = new List<int>();
        public List<string> DescriptiveScores { get; set; } = new List<string>();
        public List<double> NumeralScores { get; set; } = new List<double>();
        public List<string> StudentName { get; set; } = new List<string>();

    }
}