using CWiz.RailwayOrientedProgramming;
using FluentAssertions;
using SchoolDomain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SchoolDomainTests
{
    public class StudentTests
    {
        [Fact]
        public void Students_with_same_id_are_equal()
        {
            var firstName = FirstName.Create("Xavier");
            var lastName = LastName.Create("John");
            var zipCode = ZipCode.Create("98052");
            Result.Combine(firstName, lastName, zipCode)
                .IsSuccess
                .Should().BeTrue();

            var xavier = new Student(1, firstName, lastName, zipCode);

            firstName = FirstName.Create("Michael");
            lastName = LastName.Create("Jackson");
            Result.Combine(firstName, lastName, zipCode)
                .IsSuccess
                .Should().BeTrue();
            var micheal = new Student(1, firstName, lastName, zipCode);

            xavier.Should().Be(micheal);
        }
    }
}
