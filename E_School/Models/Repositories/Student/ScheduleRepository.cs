using E_School.Helpers.Utitlies;
using E_School.Models.DomainModels;
using E_School.Models.ViewModel.api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace E_School.Models.Repositories.api
{
    public class ScheduleRepository
    {
        private schoolEntities db = null;

        public ScheduleRepository()
        {
            db = new schoolEntities();
        }

        public List<scheduleModel> StudentSchedule(int idClass)
        {
            List<scheduleModel> list = new List<scheduleModel>();
            List<tbl_dataTable> ls;
            scheduleModel model;
            int idTeacher, idLesson;

            try
            {
                int date = getTodayDate();

                int idYear = db.tbl_years.Where(x => x.yearStart <= date && x.yearEnd >= date).FirstOrDefault().idYear;

                if (db.tbl_dataTable.Where(x => x.idYear == idYear && x.idClass == idClass).Any())
                {
                    ls = db.tbl_dataTable.Where(x => x.idYear == idYear && x.idClass == idClass).ToList();
                    for (int i = 0; i < ls.Count(); i++)
                    {
                        model = new scheduleModel();
                        model.idDataTable = ls.ElementAt(i).idDataTable;
                        model.idTeacher = ls.ElementAt(i).idTeacher;
                        model.idLesson = ls.ElementAt(i).idLesson;
                        model.idDay = ls.ElementAt(i).idDay;
                        model.idBell = ls.ElementAt(i).idBell;
                        idTeacher = ls.ElementAt(i).idTeacher;
                        model.teacherName = db.tbl_teachers.Where(x => x.idTeacher == idTeacher).FirstOrDefault().FName + " " + db.tbl_teachers.Where(x => x.idTeacher == idTeacher).FirstOrDefault().LName;
                        idLesson = ls.ElementAt(i).idLesson;
                        model.lessonName = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;

                        if (ls.ElementAt(i).isTak == true)
                        {
                            model.isTak = true;
                            model.idTeacher2 = (int)ls.ElementAt(i).idTeacher2;
                            model.idLesson2 = (int)ls.ElementAt(i).idLesson2;
                            idTeacher = (int)ls.ElementAt(i).idTeacher2;
                            idLesson = (int)ls.ElementAt(i).idLesson2;
                            model.teacherName2 = db.tbl_teachers.Where(x => x.idTeacher == idTeacher).FirstOrDefault().FName + " " + db.tbl_teachers.Where(x => x.idTeacher == idTeacher).FirstOrDefault().LName;
                            model.lessonName2 = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;
                        }

                        list.Add(model);
                    }

                    return list;
                }

                else
                    return list;


            }
            catch
            {
                return null;
            }
        }


        public List<scheduleModel> teacherSchedule(int idTeacher)
        {
            List<scheduleModel> list = new List<scheduleModel>();
            List<View_teacherSchedule> ls, lls;
            scheduleModel model;
            int idLesson, idClass;
            bool isTak = false;

            try
            {

                int date = getTodayDate();

                int idYear = db.tbl_years.Where(x => x.yearStart <= date && x.yearEnd >= date).FirstOrDefault().idYear;

                if (db.View_teacherSchedule.Where(x => x.idTeacher == idTeacher || x.idTeacher2 == idTeacher && x.idYear == idYear).Any())
                {
                    ls = db.View_teacherSchedule.Where(x => x.idTeacher == idTeacher && x.idYear == idYear).ToList();
                    lls = db.View_teacherSchedule.Where(x => x.idTeacher2 == idTeacher && x.idYear == idYear).ToList();



                    for (int i = 0; i < ls.Count(); i++)
                    {
                        isTak = false;


                        for (int j = 0; j < list.Count(); j++)
                        {
                            if (list.ElementAt(j).idBell == ls.ElementAt(i).idBell && list.ElementAt(j).idDay == ls.ElementAt(i).idDay)
                            {
                                list.ElementAt(j).isTak = true;
                                idClass = ls.ElementAt(i).idClass;
                                idLesson = ls.ElementAt(i).idLesson;
                                list.ElementAt(j).idLesson2 = idLesson;
                                list.ElementAt(j).idClass2 = idClass;
                                list.ElementAt(j).lessonName2 = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;
                                list.ElementAt(j).className2 = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().className;
                                isTak = true;
                                break;
                            }
                        }


                        if (!isTak)
                        {
                            model = new scheduleModel();
                            model.idDataTable = ls.ElementAt(i).idDataTable;
                            model.idTeacher = ls.ElementAt(i).idTeacher;
                            model.idLesson = ls.ElementAt(i).idLesson;
                            model.idDay = ls.ElementAt(i).idDay;
                            model.idBell = ls.ElementAt(i).idBell;
                            model.idClass = ls.ElementAt(i).idClass;
                            idClass = ls.ElementAt(i).idClass;
                            model.className = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().className;
                            model.idLevel = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().idLevel;
                            idLesson = ls.ElementAt(i).idLesson;
                            model.lessonName = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;


                            list.Add(model);
                        }


                    }




                    for (int k = 0; k < lls.Count(); k++)
                    {
                        isTak = false;


                        for (int j = 0; j < list.Count(); j++)
                        {
                            if (list.ElementAt(j).idBell == lls.ElementAt(k).idBell && list.ElementAt(j).idDay == lls.ElementAt(k).idDay)
                            {
                                list.ElementAt(j).isTak = true;
                                idClass = lls.ElementAt(k).idClass;
                                idLesson = (int)lls.ElementAt(k).idLesson2;
                                list.ElementAt(j).idLesson2 = idLesson;
                                list.ElementAt(j).idClass2 = idClass;
                                list.ElementAt(j).lessonName2 = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;
                                list.ElementAt(j).className2 = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().className;
                                isTak = true;
                                break;
                            }
                        }


                        if (!isTak)
                        {
                            model = new scheduleModel();
                            model.idDataTable = lls.ElementAt(k).idDataTable;
                            model.idTeacher = (int)lls.ElementAt(k).idTeacher2;
                            model.idLesson = (int)lls.ElementAt(k).idLesson2;
                            model.idDay = lls.ElementAt(k).idDay;
                            model.idBell = lls.ElementAt(k).idBell;
                            model.idClass = lls.ElementAt(k).idClass;
                            idClass = lls.ElementAt(k).idClass;
                            model.className = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().className;
                            model.idLevel = db.tbl_classes.Where(x => x.idClass == idClass).FirstOrDefault().idLevel;
                            idLesson = (int)lls.ElementAt(k).idLesson2;
                            model.lessonName = db.tbl_lessons.Where(x => x.idLesson == idLesson).FirstOrDefault().lessonName;


                            list.Add(model);
                        }


                    }

                    return list;
                }

                else
                    return list;
            }


            catch
            {
                return null;
            }
        }



        public Boolean absentation(absentModel entity)
        {

            tbl_absentation tbl;
            String sms = "";
            int idPresent, idTerm, today;


            try
            {
                for (int i = 0; i < entity.idAbsents.Count(); i++)
                {
                    tbl = new tbl_absentation();

                    if (db.tbl_absentation.Any())
                        tbl.idAbsentation = db.tbl_absentation.OrderByDescending(p => p.idAbsentation).FirstOrDefault().idAbsentation + 1;

                    else
                        tbl.idAbsentation = 1;

                    tbl.idStudent = entity.idAbsents.ElementAt(i);
                    tbl.status = true;
                    tbl.date = entity.date;
                    tbl.idDataTable = entity.idDataTable;
                    db.tbl_absentation.Add(tbl);
                    db.SaveChanges();


                }



                for (int i = 0; i < entity.idPresents.Count(); i++)
                {
                    tbl = new tbl_absentation();

                    if (db.tbl_absentation.Any())
                        tbl.idAbsentation = db.tbl_absentation.OrderByDescending(p => p.idAbsentation).FirstOrDefault().idAbsentation + 1;

                    else
                        tbl.idAbsentation = 1;

                    tbl.idStudent = entity.idPresents.ElementAt(i);
                    idPresent = entity.idPresents.ElementAt(i);
                    if (!sms.Equals(""))
                    {
                        sms += ",";
                        sms += db.tbl_students.Where(x => x.idStudent == idPresent).FirstOrDefault().fPhone;
                    }
                    else
                        sms += db.tbl_students.Where(x => x.idStudent == idPresent).FirstOrDefault().fPhone;

                    tbl.status = false;
                    tbl.date = entity.date;
                    tbl.idDataTable = entity.idDataTable;
                    db.tbl_absentation.Add(tbl);
                    db.SaveChanges();



                    //save presents in disciplines

                    tbl_studentsDisciplines tb = new tbl_studentsDisciplines();


                    if (db.tbl_studentsDisciplines.Any())
                        tb.idStudentDisciplines = db.tbl_studentsDisciplines.OrderByDescending(p => p.idStudentDisciplines).FirstOrDefault().idStudentDisciplines + 1;

                    else
                        tb.idStudentDisciplines = 1;

                    tb.idDisType = -2;
                    tb.idStudent = entity.idPresents.ElementAt(i);
                    tb.actDate = entity.date;
                    tb.justified = true;


                    today = getTodayDate();
                    idTerm = db.tbl_terms.Where(x => x.termStart <= today && x.termEnd >= today).FirstOrDefault().idTerm;

                    tb.idTerm = idTerm;
                    db.tbl_studentsDisciplines.Add(tb);
                    db.SaveChanges();


                }

                if (!sms.Equals(""))
                    sendSMS(entity.idPresents, entity.date, entity.idDataTable);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void sendSMS(int[] idPresents, int date, int idDataTable)
        {
            SendSMS ob = new SendSMS();
            SettingRepository settingBL = new SettingRepository();
            string schoolName = settingBL.Select().Single().schoolName.Replace("مدرسه", ""); ;

            MelliPayamak.Send sms = new MelliPayamak.Send();
            string retval = null;

            int id, idBell;
            String name, y, m, d, time, Date, bell;

            idBell = db.tbl_dataTable.Where(x => x.idDataTable == idDataTable).FirstOrDefault().idBell;
            bell = db.tbl_bells.Where(x => x.idBells == idBell).FirstOrDefault().BellName;

            int Numbercount = 0;
            for (int i = 0; i < idPresents.Count(); i++)
            {
                id = idPresents.ElementAt(i);
                string Recipient = db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().fPhone;
                name = db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().FName + " " + db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().LName;
                y = date.ToString().Substring(0, 4);
                m = date.ToString().Substring(4, 2);
                d = date.ToString().Substring(6, 2);
                Date = y + "/" + m + "/" + d;
                time = DateTime.Now.ToString("HH:mm");

                string text = schoolName + ";" + name + ";" + Date + ";" + time + ";" + bell + Environment.NewLine;
                retval = sms.SendByBaseNumber2("09144942959", "2478", text, Recipient, 3392);
                long sendStatus = long.Parse(retval);


                if (long.Parse(retval) > 100)
                {
                    Numbercount++;
                }
                //if (sendStatus == 0) // sms sent successful
                //{
                //    tbl_Setting tbl = db.tbl_Setting.FirstOrDefault();
                //    tbl.smsCount--;
                //    db.SaveChanges();
                //}
            }
            ob.updateSMSCount(2, true, Numbercount);

        }


        public Boolean editAbsentation(absentModel entity)
        {

            int idAbsent, idPresents, idStudent, presentCounter = 0, today, idTerm;
            bool status, sms = false, isThereAnyLateCommer = false;
            int[] idStudents = new int[entity.idPresents.Count()];
            List<int> idLateCommers = new List<int>();

            try
            {
                for (int i = 0; i < entity.idAbsents.Count(); i++) //ثبت حاضرین
                {
                    idAbsent = entity.idAbsents.ElementAt(i);

                    if (db.tbl_absentation.Where(x => x.idAbsentation == idAbsent && x.status == false).Any())
                    {
                        tbl_absentation abs = db.tbl_absentation.Where(x => x.idAbsentation == idAbsent && x.status == false).FirstOrDefault();
                        idLateCommers.Add(abs.idStudent);
                        isThereAnyLateCommer = true;
                    }

                    tbl_absentation tbl = db.tbl_absentation.Where(x => x.idAbsentation == idAbsent).FirstOrDefault();
                    tbl.status = true;
                    db.SaveChanges();



                }

                if (isThereAnyLateCommer)
                {
                    sendLateCommersSMS(idLateCommers, entity.date, entity.idDataTable);

                    for (int i = 0; i < idLateCommers.Count(); i++) // اگر دانش آموزی غیبت داشته و سپس حاضر شده، در موارد انضباطی غیبت او حذف شده و تاخیر برایش ثبت می گردد
                    {
                        int idAbsentStudent = idLateCommers.ElementAt(i);
                        tbl_studentsDisciplines discipline = db.tbl_studentsDisciplines.Where(x => x.actDate == entity.date && x.idDisType == -2 && x.idStudent == idAbsentStudent).FirstOrDefault();
                        if (discipline != null)
                        {
                            discipline.idDisType = -1;
                            db.SaveChanges();
                        }
                    }
                }





                for (int i = 0; i < entity.idPresents.Count(); i++) // ثبت غائبین
                {
                    idPresents = entity.idPresents.ElementAt(i);
                    tbl_absentation tbl = db.tbl_absentation.Where(x => x.idAbsentation == idPresents).FirstOrDefault();
                    status = tbl.status;
                    tbl.status = false;
                    idStudent = tbl.idStudent;
                    db.SaveChanges();



                    //save presents in disciplines
                    tbl_studentsDisciplines tb = new tbl_studentsDisciplines();

                    if (db.tbl_studentsDisciplines.Any())
                        tb.idStudentDisciplines = db.tbl_studentsDisciplines.OrderByDescending(p => p.idStudentDisciplines).FirstOrDefault().idStudentDisciplines + 1;
                    else
                        tb.idStudentDisciplines = 1;

                    tb.idDisType = -2;
                    tb.idStudent = idPresents;
                    tb.actDate = entity.date;
                    tb.justified = true;

                    today = getTodayDate();
                    idTerm = db.tbl_terms.Where(x => x.termStart <= today && x.termEnd >= today).FirstOrDefault().idTerm;
                    tb.idTerm = idTerm;
                    db.tbl_studentsDisciplines.Add(tb);
                    db.SaveChanges();



                    if (status == true)
                    {
                        sms = true;
                        idStudents[presentCounter] = idStudent;
                        presentCounter++;
                    }
                }

                if (sms)
                    sendSMS(idStudents, entity.date, entity.idDataTable);


                return true;
            }
            catch
            {
                return false;
            }
        }




        private void sendLateCommersSMS(List<int> idLateCommers, int date, int idDataTable)
        {
            SendSMS ob = new SendSMS();
            SettingRepository settingBL = new SettingRepository();
            string schoolName = settingBL.Select().Single().schoolName.Replace("مدرسه", ""); ;

            MelliPayamak.Send sms = new MelliPayamak.Send();
            string retval = null;

            //SmsServise.Send objSend = new SmsServise.Send();
            //byte[] StatusArray = null;
            //long[] RecIdArray = null;
            //string userName = "arfaie";
            //string password = "Navidarfaie123456789";
            ////string virtualNumber = "30008561661151";
            //string virtualNumber = "30008561222088";



            int id, idBell;
            String name, y, m, d, time, Date, bell;

            idBell = db.tbl_dataTable.Where(x => x.idDataTable == idDataTable).FirstOrDefault().idBell;
            bell = db.tbl_bells.Where(x => x.idBells == idBell).FirstOrDefault().BellName;

            int Numbercount = 0;
            for (int i = 0; i < idLateCommers.Count(); i++)
            {
                id = idLateCommers.ElementAt(i);
                string Recipient = db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().fPhone;
                name = db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().FName + " " + db.tbl_students.Where(x => x.idStudent == id).FirstOrDefault().LName;
                y = date.ToString().Substring(0, 4);
                m = date.ToString().Substring(4, 2);
                d = date.ToString().Substring(6, 2);
                Date = y + "/" + m + "/" + d;
                time = DateTime.Now.ToString("HH:mm");

                string text = schoolName + ";" + name + ";" + Date + ";" + time + ";" + bell + Environment.NewLine;
                retval = sms.SendByBaseNumber2("09144942959", "2478", text, Recipient, 3403);
                long sendStatus = long.Parse(retval);

                //string MessageBody = "گزارش تاخیر" + Environment.NewLine + name + Environment.NewLine + time + Environment.NewLine + bell;
                //sendStatus = objSend.SendSms(userName, password, virtualNumber, Recipient.Split(new char[] { ',' }), MessageBody, false, ref StatusArray, ref RecIdArray);

                if (sendStatus == 0) // sms sent successful
                {
                    tbl_Setting tbl = db.tbl_Setting.FirstOrDefault();
                    tbl.smsCount--;
                    db.SaveChanges();
                }
            }
            ob.updateSMSCount(2, true, Numbercount);


        }

        
        public HttpResponseMessage absHistory(int idClass, int idLesson)
        {
            schoolEntities db = new schoolEntities();

            int date = getTodayDate();

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
                var aa = obj.ToList();
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


        public List<View_absentation> getDayAbsentation(int date, int idClass, int idDataTable)
        {
            return db.View_absentation.Where(x => x.date == date && x.idClass == idClass && x.idDataTable == idDataTable).ToList();
        }

        private int getTodayDate()
        {
            DateTime d = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            string y = pc.GetYear(d).ToString();
            string m = pc.GetMonth(d).ToString();
            if (m.Count() == 1)
                m = "0" + m;
            string day = pc.GetDayOfMonth(d).ToString();
            if (day.Count() == 1)
                day = "0" + day;
            int date = int.Parse(y + m + day);

            return date;
        }
    }
}