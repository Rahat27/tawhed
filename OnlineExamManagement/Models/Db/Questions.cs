using System;
using System.Collections.Generic;

namespace OnlineExamManagement.Models.Db
{
    public partial class Questions
    {
        public Questions()
        {
            Answers = new HashSet<Answers>();
        }

        public string QuestionNo { get; set; }
        public string Question { get; set; }
        public string TopicsCode { get; set; }

        public Topics TopicsCodeNavigation { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
