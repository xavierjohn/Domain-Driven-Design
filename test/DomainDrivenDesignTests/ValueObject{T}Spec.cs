using CWiz.DomainDrivenDesign;
using System.Collections.Generic;
using Xunit;

namespace DomainDrivenDesignTests
{
    [Trait("Category", "ValueObject")]
    public class ValueObject_T_Spec
    {
        [Theory, MemberData(nameof(EqualValueObjects))]
        public void Equal_should_return_true_for_two_different_instances_with_same_values(dynamic sameValueInstance1, dynamic sameValueInstance2)
        {
            Assert.Equal(sameValueInstance1, sameValueInstance2);
        }

        [Theory, MemberData(nameof(EqualValueObjects))]
        public void X3DX3D_should_return_true_for_two_different_instances_with_same_value(dynamic sameValueInstance1, dynamic sameValueInstance2)
        {
            Assert.True(sameValueInstance1 == sameValueInstance2);
        }

        [Theory, MemberData(nameof(DifferentValueObjects))]
        public void NotEqual_should_return_true_when_values_are_different(dynamic sameValueInstance1, dynamic differentValueInstance1)
        {
            Assert.NotEqual(sameValueInstance1, differentValueInstance1);
        }

        [Theory, MemberData(nameof(DifferentValueObjects))]
        public void ne_should_return_true_when_values_are_different(dynamic sameValueInstance1, dynamic differentValueInstance1)
        {
            Assert.True(sameValueInstance1 != differentValueInstance1);
        }

        [Theory, MemberData(nameof(EqualValueObjects))]
        public void GetHashCode_should_return_the_same_HashCode_for_two_different_instances_with_same_values(dynamic sameValueInstance1, dynamic sameValueInstance2)
        {
            if (sameValueInstance1 == null || sameValueInstance2 == null) return;
            Assert.Equal(sameValueInstance1.GetHashCode(), sameValueInstance2.GetHashCode());
        }

        [Theory, MemberData(nameof(DifferentValueObjects))]
        public void GetHashCode_should_return_different_HashCode_when_values_are_different(dynamic sameValueInstance1, dynamic sameValueInstance2)
        {
            if (sameValueInstance1 == null || sameValueInstance2 == null) return;
            Assert.NotEqual(sameValueInstance1.GetHashCode(), sameValueInstance2.GetHashCode());
        }

        public class ValueObjectWithoutCollection : ValueObject<ValueObjectWithoutCollection>
        {
            public ValueObjectWithoutCollection(string firstName, string lastName, int age)
            {
                FirstName = firstName;
                LastName = lastName;
                Age = age;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public int Age { get; }
        }

        public class ValueObjectWithNullProperty : ValueObject<ValueObjectWithNullProperty>
        {
            public ValueObjectWithNullProperty(string firstName, int age)
            {
                FirstName = firstName;
                LastName = null;
                Age = age;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public int Age { get; }
        }

        public class ValueObjectWithCollection : ValueObject<ValueObjectWithCollection>
        {
            public ValueObjectWithCollection(List<string> phoneNumbers)
            {
                PhoneNumbers = phoneNumbers;
            }

            public List<string> PhoneNumbers { get; }
        }

        public class ValueObjectWithNestedCollection : ValueObject<ValueObjectWithNestedCollection>
        {
            public int FirstProperty { get; set; }
            public int SecondProperty { get; set; }

            public List<NestedClass> NestedCollection { get; set; }

            public class NestedClass : ValueObject<NestedClass>
            {
                public string NestedFirstProperty { get; set; }
                public string NestedSecondProperty { get; set; }

                public List<string> NestedNestedCollection { get; set; }

            }
        }

        public static IEnumerable<object[]> EqualValueObjects => new[]
        {
            new object[] { default(ValueObjectWithoutCollection), default(ValueObjectWithoutCollection)},
            new object[] { new ValueObjectWithNullProperty("Prop1", 99), new ValueObjectWithNullProperty("Prop1", 99) },
            new object[] { new ValueObjectWithoutCollection("Prop1", "Prop2", 99), new ValueObjectWithoutCollection("Prop1", "Prop2", 99) },
            new object[] { new ValueObjectWithCollection(new List<string> { "123-456-7890", "234-567-8901" }), new ValueObjectWithCollection(new List<string> { "123-456-7890", "234-567-8901" }) },
            new object[]
            {
                new ValueObjectWithNestedCollection()
                {
                    FirstProperty = 1,
                    SecondProperty = 2,
                    NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
                    {
                        new ValueObjectWithNestedCollection.NestedClass() {
                            NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                            NestedNestedCollection =  new List<string>() { "ListItem1","ListItem2"}
                        }
                    }

                }
                ,
                new ValueObjectWithNestedCollection()
                {
                    FirstProperty = 1,
                    SecondProperty = 2,
                    NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
                    {
                        new ValueObjectWithNestedCollection.NestedClass() {
                            NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                            NestedNestedCollection =  new List<string>() { "ListItem1","ListItem2"}
                        }
                    }

                }
            }
        };
        public static IEnumerable<object[]> DifferentValueObjects => new[]
        {
            new object[] { default(ValueObjectWithoutCollection), new ValueObjectWithoutCollection("Prop1", "Different", 99) },
            new object[] { new ValueObjectWithNullProperty("Prop1", 99), new ValueObjectWithNullProperty("Different", 99) },
            new object[] { new ValueObjectWithoutCollection("Prop1", "Prop2", 99), default(ValueObjectWithoutCollection)},
            new object[] { new ValueObjectWithoutCollection("Prop1", "Prop2", 99), new ValueObjectWithoutCollection("Prop1", "Different", 99) },
            new object[] { new ValueObjectWithCollection(new List<string> { "123-456-7890", "234-567-8901" }), new ValueObjectWithCollection(new List<string> { "123-456-7890", "123-567-8901" }) },
            new object[]
            {
                new ValueObjectWithNestedCollection()
                {
                    FirstProperty = 1,
                    SecondProperty = 2,
                    NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
                    {
                        new ValueObjectWithNestedCollection.NestedClass() {
                            NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                            NestedNestedCollection =  new List<string>() { "ListItem1","ListItem2"}
                        }
                    }

                }
                ,
                new ValueObjectWithNestedCollection()
                {
                    FirstProperty = 1,
                    SecondProperty = 2,
                    NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
                    {
                        new ValueObjectWithNestedCollection.NestedClass() {
                            NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                            NestedNestedCollection =  new List<string>() { "ListItem1","DifferentItem2"}
                        }
                    }

                }
            }
        };
    }
}
