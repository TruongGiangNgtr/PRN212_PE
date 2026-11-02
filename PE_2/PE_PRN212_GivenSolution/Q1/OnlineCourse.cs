using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class OnlineCourse : Course, IRegisterable
    {
        public string Platform { get; set; }

        public OnlineCourse(string courseCode, string title, int capacity, int currentEnrollment, string platform)
            : base(courseCode, title, capacity, currentEnrollment)
        {
            Platform = platform;
        }

        public override string GetCourseType()
        {
            return "Online";
        }

        public bool RegisterStudent(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Registration quantity must be positive");
            }
            if (CurrentEnrollment + quantity > Capacity)
            {
                return false;
            }
            CurrentEnrollment += quantity;
            return true;
        }
    }
}