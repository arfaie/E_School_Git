using E_School.Helpers.Utitlies;
using E_School.Models;
using E_School.Models.DomainModels;
using E_School.Models.Repositories.api;
using E_School.Models.ViewModel.api;
using E_School.Models.ViewModel.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace E_School.api.Controllers.api.Student
{
    public class ScoreController : ApiController
    {
        ScoreRepository bl = new ScoreRepository();
        YearRepository blYear = new YearRepository();
        Models.Repositories.DescriptiveScoreRepository blDesScore = new Models.Repositories.DescriptiveScoreRepository();
        Models.Repositories.StudAvrageRepository blAverage = new Models.Repositories.StudAvrageRepository();
        StudentRateRepository blRate = new StudentRateRepository();
        schoolEntities db = new schoolEntities();



        [ActionName("StudentScore")]
        [HttpPost]
        public List<View_studentScore> StudentScore([FromBody] int idStudent)
        {
            return bl.StudentScore(idStudent);
        }



        [ActionName("StudentReport")]
        [HttpGet]
        public string StudentReport([FromUri] int idYear, [FromUri] int idTerm)
        {
            string test = "https://up.7learn.com/ss/andrd/7Learn-saeid-shahini.pdf";
            string noReport = "no";
            return test;

        }




        [ActionName("setHomeWorkScore")]
        [HttpGet]
        public Boolean setHomeWorkScore([FromUri] int idReceivedHomeWork, [FromUri] float score)
        {
            if (bl.setHomeWorkScore(idReceivedHomeWork, score))
                return true;

            else
                return false;
        }



        [ActionName("getScores")]
        [HttpPost]
        public Boolean getScores([FromBody] scoreModel entity)
        {
            return bl.insertScores(entity);
        }



        [ActionName("getDiscriptiveScores")]
        [HttpGet]
        public List<descriptiveScore> getDiscriptiveScores()
        {
            return bl.getDiscriptiveScores();
        }





        [ActionName("getSubmitedScores")]
        [HttpGet]
        public List<submitScores> getSubmitedScores([FromUri]int idExam, [FromUri]int idClass)
        {
            return bl.getSubmitedScores(idExam, idClass);
        }




        [ActionName("editScores")]
        [HttpPost]
        public Boolean editScores([FromBody]scoreModel entity)
        {
            return bl.editScores(entity);
        }



        [ActionName("checkSubmitedExam")]
        [HttpGet]
        public int checkSubmitedExam([FromUri]int idExam)
        {
            return bl.checkSubmitedExam(idExam);
        }

        //محاسبه نمره دانش آموز
        [ActionName("AvrageScore")]
        [HttpGet]
        public HttpResponseMessage AvrageScore(int idMonth, int idLesson, bool isNumeral, int idClass)
        {
            //idLesson = 23;وقتی تک زنگ هست ، فقط آی دی تک زنگ اول رو بر میداره برای محاسبه کارنامه و مجبوریم برای تک زنگ دوم دستی بدیم آی دی رو
            Methods ob = new Methods();
            ExamRepository blExam = new ExamRepository();
            Models.Repositories.StudentRegisterRepository blstudentRegister = new Models.Repositories.StudentRegisterRepository();
            Models.Repositories.api.ScoreRepository blScore = new Models.Repositories.api.ScoreRepository();
            Models.Repositories.StudAvrageRepository blStudAvrage = new Models.Repositories.StudAvrageRepository();
            Models.Repositories.StudentRepository blStudent = new Models.Repositories.StudentRepository();
            Models.Repositories.ExamRepository examRepository = new Models.Repositories.ExamRepository();
            Models.Repositories.ScoreRepository scoreRepository = new Models.Repositories.ScoreRepository();

            int today = ob.getTodayDate();
            var MonthDate = ob.Monthes(idMonth).Split(',');
            int Start = int.Parse(MonthDate[0]);
            int End = int.Parse(MonthDate[1]);
            int idYear = blYear.getThisYear();
            IQueryable<View_studentScore> select;
            AvrageScore obAvrage = new AvrageScore();
            List<int> idStuds = new List<int>();
            double Avrage = 0;
            var selectStudents = blstudentRegister.Where(x => x.idYear == idYear && x.idClass == idClass);
            var jsonSerialiser = new JavaScriptSerializer();
            var selectExam = examRepository.Select().ToList();

            //آیا نمره ماهانه برای درس ثبت شده است؟ 
            var isExist = blStudAvrage.Where(x => x.idYear == idYear && x.idClass == idClass && x.idLesson == idLesson && x.idMonth == idMonth);

            var Result = (from t in scoreRepository.Select().ToList()

                          join y in selectExam
                          on t.idExam equals y.idExam

                          select new
                          {
                              t.idDescriptiveScore,
                              y.examDate,
                              t.idStudent,
                              t.score,
                              y.idClass,
                              y.idLesson,
                              y.maxScore
                          });


            //اگر نمره ماهانه برای درس ثبت نشده باشد
            if (isExist.FirstOrDefault() == null)
            {
                foreach (var a in selectStudents)
                {
                    idStuds.Add(a.idStudent);
                }
                //محاسبه نمرات عددی
                if (isNumeral)
                {
                    var selectScore = Result.Where(x => x.idLesson == idLesson && x.examDate <= End && x.examDate >= Start && x.idDescriptiveScore == -1);
                    foreach (var b in idStuds)
                    {
                        if (!obAvrage.idStudent.Contains(b))
                        {
                            List<double> lsScoreList = new List<double>();
                            var selectScores = selectScore.Where(x => x.idStudent == b);
                            var selectName = blStudent.Where(x => x.idStudent == b);
                            obAvrage.idDescriptiveScore.Add(-1);

                            foreach (var s in selectScores)
                            {
                                double score = (double)((s.score * 20) / s.maxScore);
                                lsScoreList.Add(score);
                            }
                            var Name = selectName.Where(x => x.idStudent == b).FirstOrDefault();
                            if (selectScores.FirstOrDefault() != null)
                            {
                                double sum = lsScoreList.Sum();
                                int cnt = selectScores.Count();
                                Avrage = Math.Round(sum / cnt, 2);//معدل یک درس

                                if (sum != 0)
                                {
                                    obAvrage.NumeralScores.Add(Avrage);
                                }
                                else
                                {
                                    obAvrage.NumeralScores.Add(0);
                                }
                            }
                            else
                            {
                                obAvrage.NumeralScores.Add(-1);
                            }
                            obAvrage.StudentName.Add(Name.FName + " " + Name.LName);

                        }
                    }
                    obAvrage.idStudent = idStuds;
                }
                //محاسبه نمرات توصیفی
                else
                {
                    var Result2 = (from t in Result.ToList()

                                   join y in db.tbl_descriptiveScores
                                   on t.idDescriptiveScore equals y.idDescriptiveScore

                                   select new
                                   {
                                       y.idDescriptiveScore,
                                       y.numScore,
                                       t.idStudent,
                                       t.score,
                                       y.desScore,
                                       t.idLesson,
                                       t.examDate
                                   });

                    var SelectScore = Result2.Where(x => x.idLesson == idLesson && x.examDate <= End && x.examDate >= Start && x.score == -1);
                    string avrage = null;
                    int idDesScore = 0;
                    foreach (var b in idStuds)
                    {
                        if (!obAvrage.idStudent.Contains(b))
                        {
                            obAvrage.NumeralScores.Add(-1);
                            var selectScores = SelectScore.Where(x => x.idStudent == b);
                            var selectName = blStudent.Where(x => x.idStudent == b);
                            var Name = selectName.FirstOrDefault();
                            if (selectScores.FirstOrDefault() != null)
                            {
                                float sum = (int)selectScores.Sum(x => x.numScore);
                                int cnt = selectScores.Count();
                                float n = sum / cnt;
                                Avrage = Math.Ceiling(n);//معدل یک درس

                                string DesScore = ConvertDesScore(Avrage, sum);
                                avrage = DesScore;
                                idDesScore = blDesScore.Where(x => x.desScore == DesScore).FirstOrDefault().idDescriptiveScore;
                            }
                            else
                            {
                                avrage = null;
                                idDesScore = 0;
                            }
                            obAvrage.StudentName.Add(Name.FName + " " + Name.LName);
                            obAvrage.idDescriptiveScore.Add(idDesScore);
                            obAvrage.DescriptiveScores.Add(avrage);
                        }

                    }
                    obAvrage.idStudent = idStuds;
                }
            }

            //اگر نمره ای ثبت شده باشد،سلکت و نمایش بده
            else
            {
                foreach (var a in isExist)
                {
                    var list = isExist.ToList();
                    int i = 0;
                    var DesScoreSelect = blDesScore.Where(x => x.idDescriptiveScore == a.idDesScore).FirstOrDefault();
                    var StudSelect = blStudent.Where(x => x.idStudent == a.idStudent).FirstOrDefault();

                    obAvrage.idDescriptiveScore.Add(a.idDesScore);
                    obAvrage.id.Add(a.id);
                    obAvrage.idStudent.Add(a.idStudent);
                    obAvrage.NumeralScores.Add(a.Score);
                    obAvrage.DescriptiveScores.Add(DesScoreSelect.desScore);
                    obAvrage.StudentName.Add(StudSelect.FName + " " + StudSelect.LName);
                    i++;
                }
            }

            //نمایش نمرات محاسبه شده
            var json_ = jsonSerialiser.Serialize(obAvrage);

            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    json_.ToString()
                )
            };
        }

        //ذخیره نمرات محاسبه شده
        [ActionName("AddUpdateAverage")]
        [HttpPost]
        public int AddUpdateAverage([FromBody]StudentAvrage entity)
        {
            //idLesson = 23;وقتی تک زنگ هست ، فقط آی دی تک زنگ اول رو بر میداره برای محاسبه کارنامه و مجبوریم برای تک زنگ دوم دستی بدیم آی دی رو
            Models.Repositories.StudAvrageRepository bl = new Models.Repositories.StudAvrageRepository();
            var select = bl.Where(x => /*x.idStudent == entity.idStudent[0] &&*/ x.idMonth == entity.idMonth && x.idLesson == entity.idLesson && x.idClass == entity.idClass);
            tbl_Avrages ob;
            int i = 0;

            if (!select.Any())
            {
                //Add Data to Database
                try
                {

                    foreach (var a in entity.idStudent)
                    {
                        try
                        {

                            ob = new tbl_Avrages();

                            int id = bl.GetLastIdentity() + 1;
                            ob.id = id + i;
                            ob.idClass = entity.idClass;
                            ob.idLesson = entity.idLesson;
                            ob.idStudent = entity.idStudent[i];
                            ob.idYear = blYear.getThisYear();
                            ob.Score = entity.NumeralScores[i];
                            ob.idDesScore = entity.idDescriptiveScore[i];
                            ob.idMonth = entity.idMonth;
                            i++;
                            if (entity.idStudent.Count() != i)
                            {
                                bl.Add(ob, false);
                            }
                            else
                            {
                                bl.Add(ob, true);
                            }
                        }
                        catch
                        {
                            return -1;
                        }
                        //bl.Add(ob, true);

                    }
                    AddUpdateRates(entity);

                }
                catch
                {
                    return -2;
                }
            }
            else
            {
                //Update Database
                try
                {
                    foreach (var a in entity.idStudent)
                    {
                        try
                        {
                            ob = new tbl_Avrages();
                            ob.id = entity.id[i];
                            ob.idClass = entity.idClass;
                            ob.idDesScore = entity.idDescriptiveScore[i];
                            ob.idLesson = entity.idLesson;
                            ob.idStudent = entity.idStudent[i];
                            ob.idYear = blYear.getThisYear();
                            ob.Score = entity.NumeralScores[i];
                            ob.idMonth = entity.idMonth;
                            i++;

                            if (entity.idStudent.Count() != i)
                            {
                                bl.Update(ob, false);
                            }
                            else
                            {
                                bl.Update(ob, true);
                            }

                        }
                        catch
                        {
                            return -1;
                        }
                    }
                    AddUpdateRates(entity, false);
                }
                catch
                {
                    return -2;
                }
            }

            return 1;
        }

        //برای دانش آموز و اولیا لازمه این تابع
        [ActionName("GetAvrage")]
        [HttpGet]
        public HttpResponseMessage GetAvrage(int idMonth, int idClass, int idStudent)
        {
            AverageViewModel ob = new AverageViewModel();
            int Rate = 0;
            int idYear = blYear.getThisYear();

            //سلکت تمامی نمرات دانش آموز در ماه و کلاس مد نظر
            var select_ = blAverage.Where(x => x.idMonth == idMonth && x.idClass == idClass && x.idStudent == idStudent && x.idYear == idYear).ToList();

            var jsonSerialiser = new JavaScriptSerializer();
            List<obAverage> LsAverage = new List<obAverage>();
            if (select_.Any())
            {
                var Result = (from t in select_

                              join y in db.tbl_lessons
                              on t.idLesson equals y.idLesson

                              join yy in db.tbl_descriptiveScores
                              on t.idDesScore equals yy.idDescriptiveScore

                              select new
                              {
                                  t.id,
                                  t.idClass,
                                  t.idDesScore,
                                  t.idLesson,
                                  t.idMonth,
                                  t.idStudent,
                                  t.Score,
                                  y.lessonName,
                                  yy.desScore,
                                  yy.numScore

                              });

                foreach (var a in Result)
                {
                    obAverage obAverage = new obAverage();
                    obAverage.desScore = a.desScore;
                    obAverage.id = a.id;
                    obAverage.idClass = a.idClass;
                    obAverage.idDesScore = a.idDesScore;
                    obAverage.idLesson = a.idLesson;
                    obAverage.idMonth = a.idMonth;
                    obAverage.idStudent = a.idStudent;
                    obAverage.lessonName = a.lessonName;
                    obAverage.Score = a.Score;
                    LsAverage.Add(obAverage);
                }

                int i = 0;
                ob.obAverage = LsAverage;

                //محاسبه رتبه در کلاس
                double StudSum = 0;
                var SelectRate = blRate.Where(x => x.idMonth == idMonth && x.idClass == idClass && x.idYear == idYear).OrderByDescending(x => x.Sum).ToList();
                foreach (var a in SelectRate)
                {
                    i++;
                    double m = SelectRate.Where(x => x.idStudent == a.idStudent).FirstOrDefault().Sum;
                    if (m == StudSum)
                    {
                        i--;
                    };
                    if (a.idStudent == idStudent)
                    {
                        Rate = i;
                    }
                    StudSum = m;
                }
                ob.Rate = Rate;
                //محاسبه رتبه در کلاس

                //محاسبه معدل ماه
                var cnt = select_.Count;
                if (select_.FirstOrDefault().Score != -1)//محاسبه معدل عددی
                {
                    float Average = (float)Result.Sum(x => x.Score) / cnt;
                    if (Average.ToString().Length > 5)
                    {
                        ob.Average=Average.ToString().Substring(0, 5);
                    }
                    else
                    {
                    ob.Average = Average.ToString();
                    }
                }
                else//محاسبه معدل توصیفی
                {
                    double sum = (float)Result.Sum(x => x.numScore);
                    double Average = sum / cnt;
                    Average = Math.Round(Average);
                    ob.Average = ConvertDesScore(Average, sum);
                }
                //محاسبه معدل ماه


                var json_ = jsonSerialiser.Serialize(ob);

                return new HttpResponseMessage()
                {
                    Content = new StringContent(
                        json_.ToString()
                    )
                };
            }
            else
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("-1".ToString())
                };
            }

        }

        public int AddUpdateRates(StudentAvrage entity, bool isAdd = true)
        {
            int idYear = blYear.getThisYear();
            int idStud = entity.idStudent[0];
            int idMonth = entity.idMonth;
            var select = blRate.Where(x => x.idStudent == idStud&&x.idMonth==idMonth&&x.idYear==idYear).FirstOrDefault();
            var selectDesScore = blDesScore.Select();
            tbl_StudentsRates ob;
            int i = 0;
            double SumScore;
            var Result = (from t in blAverage.Select().ToList()

                          join y in db.tbl_descriptiveScores
                          on t.idDesScore equals y.idDescriptiveScore

                          select new
                          {
                              t.id,
                              t.idClass,
                              t.idDesScore,
                              t.idLesson,
                              t.idMonth,
                              t.idStudent,
                              t.Score,
                              y.numScore
                          });

            try
            {
                if (select == null)
                {

                    //Add
                    foreach (var a in entity.idStudent)
                    {
                        try
                        {
                            ob = new tbl_StudentsRates();
                            int id = blRate.GetLastIdentity() + 1;
                            ob.id = id + i;
                            ob.idClass = entity.idClass;
                            ob.idStudent = entity.idStudent[i];
                            ob.idYear = blYear.getThisYear();
                            ob.idMonth = entity.idMonth;
                            //int idStudent = entity.idStudent[i];

                            //نمره توصیفی باشد
                            if (entity.idDescriptiveScore[1] != -1)
                            {
                                var selectSum = Result.Where(x => x.idClass == entity.idClass && x.idMonth == entity.idMonth && x.idStudent == a).Sum(x => x.numScore);
                                //int aaa=(int)selectSum.AsEnumerable().Sum(x => x.numScore);
                                ob.Sum = (double)selectSum;
                            }

                            //نمره عددی باشد
                            else
                            {
                                SumScore = blAverage.Where(x => x.idClass == entity.idClass && x.idMonth == entity.idMonth && x.idStudent == a).ToList().Sum(x => x.Score);
                                ob.Sum = SumScore;
                            }
                            i++;
                            if (entity.idStudent.Count() != i)
                            {
                                blRate.Add(ob, false);
                            }
                            else
                            {
                                blRate.Add(ob, true);
                            }
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    //Update
                    foreach (var idStudent in entity.idStudent)
                    {
                        try
                        {
                            //int idStud = entity.idStudent[i];

                            var selectRate = blRate.Where(x => x.idClass == entity.idClass && x.idMonth == entity.idMonth && x.idStudent == idStudent).FirstOrDefault();
                            //selectRate.Sum = entity.NumeralScores[i];

                            //نمره توصیفی باشد
                            if (entity.idDescriptiveScore[1] != -1)
                            {
                                SumScore = (double)Result.Where(x => x.idClass == entity.idClass && x.idMonth == entity.idMonth && x.idStudent == idStudent).ToList().Sum(x => x.numScore);
                                selectRate.Sum = SumScore;
                            }

                            //نمره عددی باشد
                            else
                            {
                                SumScore = blAverage.Where(x => x.idClass == entity.idClass && x.idMonth == entity.idMonth && x.idStudent == idStudent).ToList().Sum(x => x.Score);
                                selectRate.Sum = SumScore;
                            }
                            i++;
                            if (entity.idStudent.Count() != i)
                            {
                                blRate.Update(selectRate, false);
                            }
                            else
                            {
                                blRate.Update(selectRate, true);
                            }
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public int _AddUpdateRates()
        {
            var newSelect=blAverage.Where(x => x.idMonth == 3).ToList();
            List<int> lsStudent = new List<int>();
            foreach(var b in newSelect)
            {
                if (!lsStudent.Contains(b.idStudent))
                {
                    lsStudent.Add(b.idStudent);
                }
            }
            int idYear = blYear.getThisYear();
            //int idStud = entity.idStudent[0];
            int idMonth = 3;
            var select = blRate.Where(x => x.idStudent == 0 && x.idMonth == idMonth && x.idYear == idYear).FirstOrDefault();
            var selectDesScore = blDesScore.Select();
            tbl_StudentsRates ob;
            int i = 0;
            double SumScore;
            var Result = (from t in blAverage.Select().ToList()

                          join y in db.tbl_descriptiveScores
                          on t.idDesScore equals y.idDescriptiveScore

                          select new
                          {
                              t.id,
                              t.idClass,
                              t.idDesScore,
                              t.idLesson,
                              t.idMonth,
                              t.idStudent,
                              t.Score,
                              y.numScore
                          });

            try
            {
                if (select == null)
                {

                    //Add
                    foreach (var a in lsStudent)
                    {
                        try
                        {
                            var ok = newSelect.Where(x => x.idStudent == a).FirstOrDefault();
                            ob = new tbl_StudentsRates();
                            int id = blRate.GetLastIdentity() + 1;
                            ob.id = id + i;
                            ob.idClass = ok.idClass;
                            ob.idStudent = a;
                            ob.idYear = blYear.getThisYear();
                            ob.idMonth = ok.idMonth;
                            //int idStudent = entity.idStudent[i];

                            //نمره توصیفی باشد
                            if (ok.idDesScore != -1)
                            {
                                var selectSum = Result.Where(x => x.idClass == ok.idClass && x.idMonth == ok.idMonth && x.idStudent == a).Sum(x => x.numScore);
                                //int aaa=(int)selectSum.AsEnumerable().Sum(x => x.numScore);
                                ob.Sum = (double)selectSum;
                            }

                            //نمره عددی باشد
                            else
                            {
                                SumScore = blAverage.Where(x => x.idClass == ok.idClass && x.idMonth == ok.idMonth && x.idStudent == a).ToList().Sum(x => x.Score);
                                ob.Sum = SumScore;
                            }
                            i++;
                            if (lsStudent.Count() != i)
                            {
                                blRate.Add(ob, false);
                            }
                            else
                            {
                                blRate.Add(ob, true);
                            }
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public string ConvertDesScore(double Avrage, double sum)
        {
            tbl_descriptiveScores selectDesScore = new tbl_descriptiveScores();
            Models.Repositories.SettingRepository blSetting = new Models.Repositories.SettingRepository();
            string SchoolName = blSetting.Select().FirstOrDefault().schoolName;
            bool isAnvar = SchoolName.Contains("انوار");

            if (isAnvar)
            {
                selectDesScore = blDesScore.Where(x => x.numScore == Avrage).FirstOrDefault();
                if (Avrage < 9)
                {
                    selectDesScore = blDesScore.Where(x => x.idDescriptiveScore == 12).FirstOrDefault();
                }
            }
            else
            {
                if (Avrage > 17)
                {
                    selectDesScore = blDesScore.Where(x => x.numScore > 17).FirstOrDefault();
                }
                else if (Avrage <= 17 && Avrage > 14)
                {
                    selectDesScore = blDesScore.Where(x => x.numScore <= 17 && x.numScore > 14).FirstOrDefault();
                }
                else if (Avrage <= 14 && Avrage >= 10)
                {
                    selectDesScore = blDesScore.Where(x => x.numScore <= 14 && x.numScore >= 10).FirstOrDefault();
                }
                else if (Avrage < 10)
                {
                    selectDesScore = blDesScore.Where(x => x.numScore < 10).FirstOrDefault();
                }
                else if (sum == 0)
                {
                    selectDesScore = blDesScore.Where(x => x.numScore < 10).FirstOrDefault();
                }
            }
            string DesScore = selectDesScore.desScore;
            return DesScore;
        }
    }
}
