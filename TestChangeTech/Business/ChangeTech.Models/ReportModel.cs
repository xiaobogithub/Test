using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ReportModel
    {
        public int AllUser { get; set; }
        public int NotCompleteScreening { get; set; }
        public int CompleteScreening { get; set; }
        public int RegisteredUser { get; set; }
        public int UsersInProgramme { get; set; }
        public int ActiveUser { get; set; }
        public int InActiveUser { get; set; }
        public int CompleteUser { get; set; }
        public int TerminateUser { get; set; }
        public int PauseUser { get; set; }
    }

    public class ReportItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class ProgramUserReportModel
    {
        public string ProgramLogoUrl { get; set; }
        public string ProgramName { get; set; }
        public int AllUser { get; set; }
        public int NotCompleteScreening { get; set; }
        public int CompleteScreening { get; set; }
        public int RegisteredUser { get; set; }
        public int UsersInProgramme { get; set; }
        public int CompleteUser { get; set; }
        public int TerminateUser { get; set; }
        public int RegisteredLast24Hours { get; set; }
        public int RegisteredLastWeek { get; set; }
        public int RegisteredLastMonth { get; set; }
    }
}
