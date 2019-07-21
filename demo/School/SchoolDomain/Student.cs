using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDomain
{
    public class Student : Entity<Student, int>
    {
        public Student(int id, FirstName firstName, LastName lastName, ZipCode zipCode) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            ZipCode = zipCode;
        }

        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        public ZipCode ZipCode { get; set; }

        public static implicit operator Student(Result<Student> student)
        {
            return student.Value;
        }
    }
}
