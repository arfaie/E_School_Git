﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class schoolEntities : DbContext
    {
        public schoolEntities()
            : base("name=schoolEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_absentation> tbl_absentation { get; set; }
        public DbSet<tbl_Admin____> tbl_Admin____ { get; set; }
        public DbSet<tbl_answers> tbl_answers { get; set; }
        public DbSet<tbl_bells> tbl_bells { get; set; }
        public DbSet<tbl_classes> tbl_classes { get; set; }
        public DbSet<tbl_costs> tbl_costs { get; set; }
        public DbSet<tbl_dataTable> tbl_dataTable { get; set; }
        public DbSet<tbl_days> tbl_days { get; set; }
        public DbSet<tbl_debts> tbl_debts { get; set; }
        public DbSet<tbl_descriptiveScores> tbl_descriptiveScores { get; set; }
        public DbSet<tbl_disciplineTypes> tbl_disciplineTypes { get; set; }
        public DbSet<tbl_editTransections> tbl_editTransections { get; set; }
        public DbSet<tbl_educations> tbl_educations { get; set; }
        public DbSet<tbl_exams> tbl_exams { get; set; }
        public DbSet<tbl_examTypes> tbl_examTypes { get; set; }
        public DbSet<tbl_homeWorks> tbl_homeWorks { get; set; }
        public DbSet<tbl_images> tbl_images { get; set; }
        public DbSet<tbl_imageType> tbl_imageType { get; set; }
        public DbSet<tbl_lessons> tbl_lessons { get; set; }
        public DbSet<tbl_lessonTypes> tbl_lessonTypes { get; set; }
        public DbSet<tbl_levels> tbl_levels { get; set; }
        public DbSet<tbl_majors> tbl_majors { get; set; }
        public DbSet<tbl_marriges> tbl_marriges { get; set; }
        public DbSet<tbl_messageRecord> tbl_messageRecord { get; set; }
        public DbSet<tbl_messages> tbl_messages { get; set; }
        public DbSet<tbl_msgTypes> tbl_msgTypes { get; set; }
        public DbSet<tbl_news> tbl_news { get; set; }
        public DbSet<tbl_nowDate> tbl_nowDate { get; set; }
        public DbSet<tbl_offices> tbl_offices { get; set; }
        public DbSet<tbl_options> tbl_options { get; set; }
        public DbSet<tbl_payments> tbl_payments { get; set; }
        public DbSet<tbl_payTypes> tbl_payTypes { get; set; }
        public DbSet<tbl_receivedHomeWorks> tbl_receivedHomeWorks { get; set; }
        public DbSet<tbl_referendums> tbl_referendums { get; set; }
        public DbSet<tbl_regCourseLists> tbl_regCourseLists { get; set; }
        public DbSet<tbl_registrationCourses> tbl_registrationCourses { get; set; }
        public DbSet<tbl_registrationList> tbl_registrationList { get; set; }
        public DbSet<tbl_scores> tbl_scores { get; set; }
        public DbSet<tbl_studentRegister> tbl_studentRegister { get; set; }
        public DbSet<tbl_students> tbl_students { get; set; }
        public DbSet<tbl_studentsCosts> tbl_studentsCosts { get; set; }
        public DbSet<tbl_studentsData> tbl_studentsData { get; set; }
        public DbSet<tbl_studentsDisciplines> tbl_studentsDisciplines { get; set; }
        public DbSet<tbl_studentStatus> tbl_studentStatus { get; set; }
        public DbSet<tbl_supporters> tbl_supporters { get; set; }
        public DbSet<tbl_teachers> tbl_teachers { get; set; }
        public DbSet<tbl_terms> tbl_terms { get; set; }
        public DbSet<tbl_Types> tbl_Types { get; set; }
        public DbSet<tbl_years> tbl_years { get; set; }
        public DbSet<View_absentation> View_absentation { get; set; }
        public DbSet<view_Classes> view_Classes { get; set; }
        public DbSet<View_classStudents> View_classStudents { get; set; }
        public DbSet<View_discipline> View_discipline { get; set; }
        public DbSet<View_Exam> View_Exam { get; set; }
        public DbSet<View_homeWork> View_homeWork { get; set; }
        public DbSet<View_HomeWorkResponse> View_HomeWorkResponse { get; set; }
        public DbSet<view_Lesson> view_Lesson { get; set; }
        public DbSet<View_StudentFinancial> View_StudentFinancial { get; set; }
        public DbSet<View_studentInfo> View_studentInfo { get; set; }
        public DbSet<View_studentMessage> View_studentMessage { get; set; }
        public DbSet<view_studentRegister> view_studentRegister { get; set; }
        public DbSet<view_Terms> view_Terms { get; set; }
        public DbSet<View_studentScore> View_studentScore { get; set; }
        public DbSet<tbl_Setting> tbl_Setting { get; set; }
        public DbSet<tbl_Avrages> tbl_Avrages { get; set; }
        public DbSet<tbl_SiteContent> tbl_SiteContent { get; set; }
        public DbSet<tbl_StudentsRates> tbl_StudentsRates { get; set; }
        public DbSet<View_teacherSchedule> View_teacherSchedule { get; set; }
    }
}