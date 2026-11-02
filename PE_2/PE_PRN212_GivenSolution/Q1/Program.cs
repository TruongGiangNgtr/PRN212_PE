namespace Q1
{
    public delegate void RegistrationChangedCallback(string courseTitle, int currentEnrollment);

    public class Program
    {
        public static void Main(string[] args)
        {
            //create 3 online courses
            OnlineCourse course1 = new OnlineCourse("CS101", "Introduction to Computer Science", 30, 0, "Zoom");
            OnlineCourse course2 = new OnlineCourse("CS102", "Data Structures and Algorithms", 25, 0, "Google Meet");
            OnlineCourse course3 = new OnlineCourse("CS103", "Database Systems", 20, 0, "Microsoft Teams");

            CourseManager courseManager = new CourseManager((x, y) =>
            {
                Console.WriteLine($"Course: {x}, Enrollment: {y}");
            });

            courseManager.AddCourse(course1);
            courseManager.AddCourse(course2);
            courseManager.AddCourse(course3);

            try
            {
                courseManager.RegisterCourse("CS101", 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                courseManager.RegisterCourse("CS105", 10);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("----------------------------------");
            courseManager.GetAvailableCourses(5).ForEach(c => Console.WriteLine($"Course: {c.Title}, Available Seats: {c.Capacity - c.CurrentEnrollment}"));
        }
    }
}