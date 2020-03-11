using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_School.Models.ViewModel.api
{
    public class LsAbs
    {
        public int idStud{ get; set; }
        public string Name { get; set; }
        public List<int> Sessions { get; set; }
        public List<string> Dates { get; set; }

        public LsAbs()
        {
            Dates = new List<string>();
            Sessions = new List<int>();
        }
    }
}