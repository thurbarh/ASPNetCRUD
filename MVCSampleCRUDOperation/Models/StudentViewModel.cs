using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSampleCRUDOperation.Models
{
    public class StudentViewModel
    {
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
        public Nullable<int> Student_Age { get; set; }
        public string Student_Email { get; set; }
        public string Phone_Number { get; set; }
        public Nullable<System.DateTime> Student_DOB { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
    }
}