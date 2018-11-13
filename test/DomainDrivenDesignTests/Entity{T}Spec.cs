using CWiz.DomainDrivenDesign;
using System.Collections.Generic;
using Xunit;

namespace DomainDrivenDesignTests
{
    [Trait("Category", "Entity")]
    public class Entity_T_Spec
    {
        [Theory, MemberData(nameof(EqualEntity))]
        public void Equal_should_return_true_for_two_different_entities_with_same_key(dynamic entity1, dynamic entity2)
        {
            Assert.Equal(entity1, entity2);
        }

        [Theory, MemberData(nameof(EqualEntity))]
        public void X3DX3D_should_return_true_for_two_different_entities_with_same_key(dynamic entity1, dynamic entity2)
        {
            Assert.True(entity1 == entity2);
        }
        public class TestEntity: Entity<TestEntity, int>
        {
            public TestEntity(int key, string first, string last) : base(key)
            {
            }

            public string First { get; set; }
            public string Last { get; set; }
        }

        public static IEnumerable<object[]> EqualEntity => new[]
         {
            new object[] { default(TestEntity), default(TestEntity) },
            new object[] { new TestEntity(1, "Tom","Thumb"), new TestEntity(1, "Other","Thumb") },
        };
    }
}
