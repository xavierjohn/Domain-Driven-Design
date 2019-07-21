using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;
using System;

namespace SchoolDomain
{
    public class FirstName : RequiredString<FirstName>
    {
        private FirstName(string value) : base(value)
        {
        }

        public static implicit operator string(FirstName firstName)
        {
            return firstName.Value;
        }

        public static implicit operator FirstName(Result<FirstName> firstName)
        {
            return firstName.Value;
        }
    }
}
