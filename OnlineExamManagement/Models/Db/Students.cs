using System;
using System.Collections.Generic;

namespace OnlineExamManagement.Models.Db
{
    public partial class Students
    {
        public Students()
        {
            Subjects = new HashSet<Subjects>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string Father { get; set; }
        public string Picture { get; set; }

        public ICollection<Subjects> Subjects { get; set; }
        public IEnumerable<Itempictures> Itempictures { get; internal set; }
    }
}
