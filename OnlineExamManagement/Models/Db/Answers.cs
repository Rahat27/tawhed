using System;
using System.Collections.Generic;

namespace OnlineExamManagement.Models.Db
{
    public partial class Answers
    {
        public string AnswerSerial { get; set; }
        public string Answer { get; set; }
        public string Correct { get; set; }
        public string QuestionNo { get; set; }

        public Questions QuestionNoNavigation { get; set; }
    }
}













