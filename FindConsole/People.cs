namespace FindConsole
{
    public class Person
    {
        [EPiServer.Find.Id] // without an ID, GUID string is generated
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Student : Person
    {
        public string Course { get; set; }
    }
}
