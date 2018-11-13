using CWiz.DomainDrivenDesign;
using CWiz.RailwayOrientedProgramming;
using Xunit;

namespace DomainDrivenDesignTests
{
    public class TrackingId : RequiredString<TrackingId>
    {
        private TrackingId(string value) : base(value)
        {
        }

        public static explicit operator TrackingId(string trackingId)
        {
            return Create(trackingId).Value;
        }

        public static implicit operator string(TrackingId trackingId)
        {
            return trackingId.Value;
        }
    }

    public class RequiredString_T_Spec
    {
        [Fact]
        public void Cannot_create_empty_RequiredString()
        {
            var trackingId1 = TrackingId.Create("");
            Assert.True(trackingId1.IsFailure);
            Assert.Single(trackingId1.Errors);
            Assert.Equal("TrackingId cannot be empty", trackingId1.Errors[0].Message);
            Assert.Equal("TrackingId", trackingId1.Errors[0].Field);
        }

        [Fact]
        public void Two_RequiredString_with_different_value_should_be__not_equal()
        {
            var trackingId1 = TrackingId.Create("Value1");
            var trackingId2 = TrackingId.Create("Value2");
            var result = Result.Combine(trackingId2, trackingId1);
            Assert.True(result.IsSuccess);
            Assert.NotEqual(trackingId1.Value, trackingId2.Value);
        }

        [Fact]
        public void Two_RequiredString_with_same_value_should_be_equal()
        {
            var trackingId1 = TrackingId.Create("SameValue");
            var trackingId2 = TrackingId.Create("SameValue");
            var result = Result.Combine(trackingId2, trackingId1);
            Assert.True(result.IsSuccess);
            Assert.Equal(trackingId1.Value, trackingId2.Value);
        }
    }
}