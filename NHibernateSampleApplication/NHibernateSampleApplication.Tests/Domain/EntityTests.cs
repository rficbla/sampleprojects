using System;
using NHibernateSampleApplication.Domain;
using NUnit.Framework;

namespace NHibernateSampleApplication.Tests.Domain
{
    public class EntityTests
    {
        [TestFixture]
        public class When_asked_to_compare_two_transient_objects_that_derive_from_entity
        {
            [Test]
            public void Should_be_equal_if_the_natural_keys_are_same()
            {
                Dog dog1 = new Dog
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true,
                                   Breed = "German Shepherd"
                               };
                Dog dog2 = new Dog
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true,
                                   Breed = "German Shepherd"
                               };

                Assert.AreEqual(dog1, dog2, "a should be equal to b");
            }

            [Test]
            public void Should_be_not_equal_if_the_natural_keys_are_different()
            {
                Dog dog1 = new Dog
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true,
                                   Breed = "German Shepherd"
                               };
                Dog dog2 = new Dog
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true,
                                   Breed = "Golden Retriever"
                               };

                Assert.AreNotEqual(dog1, dog2, "a should not be equal to b");
            }

            [Test]
            public void Should_be_equal_if_they_refer_to_the_same_object()
            {
                Mammal a = new Mammal
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true
                               };
                Mammal b = a;

                Assert.AreEqual(a, b, "a should be equal to b");
            }

            [Test]
            public void Should_be_not_equal_if_the_object_to_be_compared_with_is_null()
            {
                Mammal a = new Mammal
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true
                               };
                Mammal b = null;

                Assert.AreNotEqual(a, b, "a should not be equal to b");
            }

            [Test]
            public void Should_be_not_equal_if_they_are_different_types()
            {
                Mammal a = new Mammal
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true
                               };
                FakeMammal b = new FakeMammal
                                   {
                                       HasDiaphragm = true,
                                       HasSweatGlands = true
                                   };

                Assert.IsFalse(a.Equals(b), "a should not be equal to b");
            }

            [Test]
            public void Should_be_not_equal_if_they_are_different_types_that_are_derived_from_the_same_base_class()
            {
                Mammal cat = new Cat();

                Dog dog = new Dog();

                Assert.IsFalse(cat.Equals(dog), "a should not be equal to b");
            }

            [Test]
            public void Should_be_equal_if_one_type_is_assignable_to_the_other_and_they_have_same_property_values()
            {
                Mammal mammal = new Mammal
                                    {
                                        HasDiaphragm = true,
                                        HasSweatGlands = true
                                    };

                Dog dog = new Dog
                              {
                                  HasDiaphragm = true,
                                  HasSweatGlands = true,
                                  Breed = "German Shepherd"
                              };

                Assert.IsTrue(mammal.Equals(dog), "a should be equal to b");
            }
        }

        [TestFixture]
        public class When_asked_to_compare_two_persisted_objects_that_derive_from_entity
        {
            [Test]
            public void Should_be_equal_if_they_have_the_same_Id()
            {
                Guid guid = Guid.NewGuid();

                Mammal a = new Mammal
                               {
                                   Id = guid,
                               };
                Mammal b = new Mammal
                               {
                                   Id = guid,
                               };

                Assert.AreEqual(a, b, "a should be equal to b");
            }

            [Test]
            public void Should_not_be_equal_if_they_have_the_different_Id()
            {
                Mammal a = new Mammal
                               {
                                   Id = Guid.NewGuid(),
                               };
                Mammal b = new Mammal
                               {
                                   Id = Guid.NewGuid(),
                               };

                Assert.AreNotEqual(a, b, "a should not be equal to b");
            }
        }

        [TestFixture]
        public class When_asked_to_compare_a_transient_object_with_a_persisted_object
        {
            [Test]
            public void Should_be_not_be_equal_if_the_Id_doesnot_match_even_if_they_have_the_same_natural_keys()
            {
                Mammal a = new Mammal
                               {
                                   Id = Guid.NewGuid(),
                                   HasDiaphragm = true,
                                   HasSweatGlands = true
                               };

                Mammal b = new Mammal
                               {
                                   HasDiaphragm = true,
                                   HasSweatGlands = true,
                               };

                Assert.AreNotEqual(a, b, "a should not be equal to b");
            }
        }

        public class Mammal : DomainEntity<Mammal>
        {
            public bool HasDiaphragm { get; set; }

            public bool HasSweatGlands { get; set; }
        }

        public class Cat : Mammal
        {
        }

        public class Dog : Mammal
        {
            [NaturalKey]
            public string Breed { get; set; }
        }

        public class FakeMammal : DomainEntity<FakeMammal>
        {
            [NaturalKey]
            public bool HasDiaphragm { get; set; }

            [NaturalKey]
            public bool HasSweatGlands { get; set; }
        }
    }
}