using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_School.Models.DomainModels;
using E_School.Models.Repositories;
using Newtonsoft.Json;
using System.Text;
using System.Collections;
using E_School.Helpers.Utitlies;
using System.Web.Script.Serialization;
using E_School.Models.ViewModel;
using Newtonsoft.Json.Linq;

namespace E_School.Controllers.api.Management
{
    public class AbsController : ApiController
    {
        AbsRepository bl = new AbsRepository();

        [ActionName("select")]
        [HttpGet]
        public HttpResponseMessage select(int idClass, int idLesson)
        {
            schoolEntities db = new schoolEntities();
            YearRepository blYear = new YearRepository();
            int date = 0;
            date = date.GetPersianDate();

            int idYear = db.tbl_years.Where(x => x.yearStart <= date && x.yearEnd >= date).FirstOrDefault().idYear;

            try
            {
                var Result = (from a in db.tbl_absentation

                              join s in db.tbl_students
                              on a.idStudent equals s.idStudent

                              join dt in db.tbl_dataTable
                              on a.idDataTable equals dt.idDataTable
                              where dt.idClass == idClass & dt.idLesson == idLesson & dt.idYear == idYear



                              select new
                              {
                                  dt.idDataTable,
                                  a.idAbsentation,
                                  a.date,
                                  a.status,
                                  a.idStudent,
                                  s.FName,
                                  s.LName,

                              }).ToList().Select(a => new
                              {
                                  a.idDataTable,
                                  a.idAbsentation,
                                  date = a.date.ToSlashDate().Substring(5, 5),
                                  a.status,
                                  a.idStudent,
                                  Name = a.FName + " " + a.LName,
                                  a.FName,
                                  a.LName,
                              })
                                 .ToList();

                LsAbs ob;
                List<object> obj = new List<object>();
                List<int> idStud = new List<int>();
                bool flag = false;
                foreach (var a in Result)
                {
                    ob = new LsAbs();
                    int i = 1;

                    if (!idStud.Contains(a.idStudent))
                    {
                        var select = Result.Where(x => x.idStudent == a.idStudent);
                        foreach (var b in select)
                        {
                            if (!flag)
                            {
                                ob.Dates.Add(b.date);
                            }
                            if (b.status == false)
                            {
                                ob.Sessions.Add(i);
                            }
                            i++;
                        }
                        flag = true;
                        ob.idStud = a.idStudent;
                        ob.Name = a.Name;
                        idStud.Add(a.idStudent);
                        if (ob.Sessions.Count() == 0)
                        {
                            ob.Sessions.Add(-2);
                        }
                        obj.Add(ob);
                    }

                }
                string ss = JsonConvert.SerializeObject(obj);
                return new HttpResponseMessage()
                {
                    Content = new StringContent(ss)
                };
            }

            catch
            {
                return null;
            }
        }
        [ActionName("TodayAbs")]
        [HttpGet]
        public HttpResponseMessage TodayAbs([FromUri] int Date)
        {
            string strDT = Date.ToString();
            if (strDT.Length > 8)
            {
                strDT = strDT.Substring(0, 4) + strDT.Substring(5,4);
                Date = int.Parse(strDT);
            }
            schoolEntities db = new schoolEntities();

            var Result = (from a in db.tbl_absentation

                          join s in db.tbl_students
                          on a.idStudent equals s.idStudent
                          where a.date==Date && a.status==false

                          join dt in db.tbl_dataTable
                          on a.idDataTable equals dt.idDataTable

                          join c in db.tbl_classes
                          on dt.idClass equals c.idClass

                          select new 
                          {
                              Name=s.FName+" "+ s.LName,
                              c.className
                          });
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(Result);
            return new HttpResponseMessage()
            {
                Content = new StringContent(json)
            };
        }

        [ActionName("Add")]
        [HttpPost]
        public int Add([FromBody]tbl_absentation entity)
        {
            try
            {


                if (entity == null)
                {
                    return int.Parse("-1");
                }
                else
                {
                    entity.idAbsentation = bl.GetLastIdentity() + 1;
                    if (bl.Add(entity) == false)
                    {
                        return int.Parse("0");
                    }
                    else
                    {
                        return int.Parse(entity.idAbsentation.ToString());
                    }

                }

            }
            catch (Exception EX)
            {
                return int.Parse("-2");
            }
        }

        [ActionName("Update")]
        [HttpPost]
        public bool Update([FromBody]tbl_absentation entity)
        {
            try
            {

                if (entity == null)
                {
                    return false;
                }
                else
                {
                    if (bl.Update(entity))
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception EX)
            {
                return false;
            }

        }

        [ActionName("Delete")]
        [HttpPost]
        public bool Delete([FromBody]int id)
        {
            try
            {


                if (id == null)
                {
                    return false;
                }
                else
                {
                    if (bl.Delete(id))
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception EX)
            {
                return false;
            }

        }
    }
}