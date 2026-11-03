using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public abstract class Course
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public int Capacity { get; set; }
        public int CurrentEnrollment { get; set; }

        public Course(string courseCode, string title, int capacity, int currentEnrollment)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Capacity must be greater than 0");
            }
            if (currentEnrollment < 0 || currentEnrollment > capacity)
            {
                throw new ArgumentOutOfRangeException("CurrentEnrollment must be between 0 and capacity");
            }

            CourseCode=courseCode;
            Title=title;
            Capacity=capacity;
            CurrentEnrollment=currentEnrollment;
        }

        public abstract string GetCourseType();
    }
}