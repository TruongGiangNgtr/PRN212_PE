using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class CourseManager
    {
        private List<OnlineCourse> _onlineCourses;

        private RegistrationChangedCallback _onRegistrationChanged;

        public CourseManager(RegistrationChangedCallback callback)
        {
            _onlineCourses = new List<OnlineCourse>();
            _onRegistrationChanged = callback;
        }

        public void AddCourse(OnlineCourse course)
        {
            _onlineCourses.Add(course);
        }

        public bool RegisterCourse(string courseCode, int quantity)
        {
            var course = _onlineCourses.FirstOrDefault(c => c.CourseCode == courseCode);
            if (course == null)
            {
                throw new InvalidOperationException($"Course not found {courseCode}");
            }
            bool registrationSuccessful = course.RegisterStudent(quantity);
            if (registrationSuccessful)
            {
                _onRegistrationChanged.Invoke(course.Title, course.CurrentEnrollment);
            }
            return registrationSuccessful;
        }

        public List<OnlineCourse> GetAvailableCourses(int minSeats)
        {
            return _onlineCourses.Where(c => c.Capacity - c.CurrentEnrollment >= minSeats)
                                .OrderByDescending(c => c.Capacity - c.CurrentEnrollment)
                                .ToList();
        }
    }
}