using E_School.Helpers.Utitlies;
using E_School.Models.DomainModels;
using E_School.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;

namespace E_School.Controllers.api.Management
{
    public class UpdateSMSController : ApiController
    {
        StudentRepository blStudent = new StudentRepository();
        // GET: api/UpdateSMS
        SendSMS ob = new SendSMS();

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public bool UpdateVersion()
        {
            StudentRepository blStud = new StudentRepository();
            SettingRepository settingBL = new SettingRepository();
            var setting = settingBL.Select().Single();
            string schoolName = setting.schoolName.ToString().Replace("مدرسه", "");
            string Version = setting.teacherVersion.ToString();
            SendSMS ob = new SendSMS();

            List<tbl_students> studs = new List<tbl_students>();
            studs = blStud.Where(x=>x.idStatus==1).ToList();
            string number;
            bool check = true;
            int Numbercount = 0;

            foreach (var a in studs)
            {

                try
                {
                    string studName = a.FName + " " + a.LName;
                    string text = studName + ";" + Version + ";" + schoolName +"\n";
                    number = a.fPhone;
                    int isDone=ob.send(number, text, 3, true, 10199);
                    Thread.Sleep(100);
                    if (isDone == 1)
                    {
                        Numbercount++;
                    }

                }
                catch
                {
                    check = false;
                }
            }
            ob.updateSMSCount(3, true, Numbercount);
            return check;
        }
        public bool SendToAllParent()
        {
            try
            {
                ob.SendToAllParent();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
