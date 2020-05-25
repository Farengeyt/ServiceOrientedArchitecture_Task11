namespace WpfWebApiClient.Models
{
    public class Student
    {
        public string Name { get; set; }
        public float AvgMark { get; set; }

        public Student() { }

        public Student(string name, float avgMark)
        {
            Name = name;
            AvgMark = avgMark;
        }
    }
}
