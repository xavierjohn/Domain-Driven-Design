using FluentAssertions;
using SchoolDomain;
using System;
using Xunit;

namespace SchoolDomainTests
{
    public class ZipCodeTests
    {
        [Fact]
        public void ZipCode_can_be_created()
        {
            var zipCodeResult = ZipCode.Create("98052");
            zipCodeResult.IsSuccess.Should().BeTrue();
            ZipCode zipCode = zipCodeResult.Value;
            zipCode.Value.Length.Should().Be(5);
        }

        [Fact]
        public void Invalid_ZipCode_cannot_be_created()
        {
            var zipCodeResult = ZipCode.Create("");
            zipCodeResult.IsFailure.Should().BeTrue();
            zipCodeResult.Errors[0].Field.Should().Be(nameof(ZipCode));
            zipCodeResult.Errors[0].Message.Should().Be($"{nameof(ZipCode)} cannot be empty.");

            zipCodeResult = ZipCode.Create("213");
            zipCodeResult.IsFailure.Should().BeTrue();
            zipCodeResult.Errors[0].Field.Should().Be(nameof(ZipCode));
            zipCodeResult.Errors[0].Message.Should().Be($"{nameof(ZipCode)} is not valid.");

            zipCodeResult = ZipCode.Create("abcdef");
            zipCodeResult.IsFailure.Should().BeTrue();
            zipCodeResult.Errors[0].Field.Should().Be(nameof(ZipCode));
            zipCodeResult.Errors[0].Message.Should().Be($"{nameof(ZipCode)} is not valid.");
        }
    }
}
