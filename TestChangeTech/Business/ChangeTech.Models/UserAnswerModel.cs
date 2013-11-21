using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class UserAnswerModel
    {
        public string UserGuid { get; set; }
        public string PageGuid { get; set; }
        public string PageQuestionGUID { get; set; }
        public string QuestionValue { get; set; }
        public string ProgramGuid { get; set; }
        public string SessionGuid { get; set; }
        public int PageSequenceOrder { get; set; }
        public string LanguageGuid { get; set; }
        
        //for Relapse
        public string RelapsePageSequenceGuid { get; set; }
        public string RelapsePageGuid { get; set; }
    }
}
