using System.Collections.Generic;

namespace StudentRecordApp.Entities
{
    public class StudentViewModel
    {

        public Student ActiveStudent { get; set; }

        public ICollection<Program> Programs { get; set; }
    }
}
