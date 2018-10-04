using System;
using System.Collections.Generic;

namespace OnlineExamManagement.Models.Db
{
    public partial class Subjects
    {
        public Subjects()
        {
            Topics = new HashSet<Topics>();
        }

        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int? StudentId { get; set; }

        public Students Student { get; set; }
        public ICollection<Topics> Topics { get; set; }
    }
}
