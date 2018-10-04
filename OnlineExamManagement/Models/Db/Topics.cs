using System;
using System.Collections.Generic;

namespace OnlineExamManagement.Models.Db
{
    public partial class Topics
    {
        public Topics()
        {
            Questions = new HashSet<Questions>();
        }

        public string TopicsCode { get; set; }
        public string TopicsName { get; set; }
        public string SubjectCode { get; set; }

        public Subjects SubjectCodeNavigation { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public int StudentId { get; internal set; }
    }
}
