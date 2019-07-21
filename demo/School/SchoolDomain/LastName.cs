using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;
using System;

namespace SchoolDomain
{
    public class LastName : RequiredString<LastName>
    {
        private LastName(string value) : base(value)
        {
        }

        public static implicit operator string(LastName lastName)
        {
            return lastName.Value;
        }

        public static implicit operator LastName(Result<LastName> lastName)
        {
            return lastName.Value;
        }
    }
}
