using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;

namespace School.DTO
{
    public class Student : ValueObject<Student>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }

        public Result<SchoolDomain.Student> ToStudent()
        {
            var firstName = SchoolDomain.FirstName.Create(FirstName);
            var lastName = SchoolDomain.LastName.Create(LastName);
            var zipCode = SchoolDomain.ZipCode.Create(ZipCode);

            return Result.Combine(firstName, lastName, zipCode)
                .Map(() => new SchoolDomain.Student(Id, firstName, lastName, zipCode));
        }
    }
}
