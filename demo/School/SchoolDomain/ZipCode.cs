using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolDomain
{
    public class ZipCode : ValueObject<ZipCode>
    {
        const string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

        public string Value { get; }

        public ZipCode(Maybe<string> requiredZipCodeOrNothing)
        {
            Validate(requiredZipCodeOrNothing)
                .OnFailure((errors) => throw new ArgumentException(errors.ToString()));

            Value = requiredZipCodeOrNothing.Value;
        }

        public static Result<ZipCode> Create(Maybe<string> requiredZipCodeOrNothing)
        {
            return Validate(requiredZipCodeOrNothing)
                .Map(zipCode => new ZipCode(zipCode));
        }

        private static Result<string> Validate(Maybe<string> requiredZipCodeOrNothing)
        {
            return requiredZipCodeOrNothing
                            .EnsureNotNullOrWhiteSpace(new Result.Error($"{nameof(ZipCode)} cannot be empty.", nameof(ZipCode)))
                            .Ensure(zipCode => Regex.Match(zipCode, _usZipRegEx).Success, new Result.Error($"{nameof(ZipCode)} is not valid.", nameof(ZipCode)));
        }

        public static implicit operator string(ZipCode zipCode)
        {
            return zipCode.Value;
        }

        public static implicit operator ZipCode(Result<ZipCode> zipCode)
        {
            return zipCode.Value;
        }
    }
}
