using System;
using NUnit.Framework;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra.Test.Tests
{
    [TestFixture]
    public class BuilderTest
    {
        [Test]
        public void record_type_without_fields_and_namespace()
        {
            var context = new TypeContext();

            var tag = Guid.NewGuid();
            const string name = "First";

            context.DefineRecord(builder => builder
                .SetName(name)
                .SetTag(tag)
            );

            context.Build();

            var firstType = context.GetRecordType(name);
            Assert.That(firstType.FullName, Is.EqualTo(name));
            Assert.That(firstType.Name, Is.EqualTo(name));
            Assert.That(firstType.Namespace, Is.EqualTo(""));
            Assert.That(firstType.Tag, Is.EqualTo(tag));
            Assert.That(firstType.BaseType, Is.Null);
            Assert.That(firstType.GetFields().Count, Is.EqualTo(0));
        }

        [Test]
        public void record_type_without_fields_and_with_namespace()
        {
            var context = new TypeContext();

            var tag = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("SomeNamespace.First")
                .SetTag(tag)
            );

            context.Build();

            var firstType = context.GetRecordType("SomeNamespace.First");
            Assert.That(firstType.FullName, Is.EqualTo("SomeNamespace.First"));
            Assert.That(firstType.Name, Is.EqualTo("First"));
            Assert.That(firstType.Namespace, Is.EqualTo("SomeNamespace"));
            Assert.That(firstType.Tag, Is.EqualTo(tag));
            Assert.That(firstType.BaseType, Is.Null);
            Assert.That(firstType.GetFields().Count, Is.EqualTo(0));
        }

        [Test]
        public void record_type_with_self_base_type()
        {
            var context = new TypeContext();

            var tag = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("SomeNamespace.First")
                .SetTag(tag)
                .SetBaseType("SomeNamespace.First")
            );

            Assert.Throws<CircularDependencyException>(() =>
            {
                context.Build();
            });
        }

        [Test]
        public void record_type_with_fields_having_the_same_index()
        {
            var context = new TypeContext();

            Assert.Throws<DuplicateFieldIndexException>(() =>
            {
                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First")
                    .AddField(1, "Name", "SomeNamespace.First", FieldQualifier.Optional)
                    .AddField(1, "Year", "SomeNamespace.First", FieldQualifier.Optional)
                );
            });
        }

        [Test]
        public void record_type_with_fields_having_the_same_name()
        {
            var context = new TypeContext();

            Assert.Throws<DuplicateFieldNameException>(() =>
            {
                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First")
                    .AddField(1, "Name", "SomeNamespace.First", FieldQualifier.Optional)
                    .AddField(2, "Name", "SomeNamespace.First", FieldQualifier.Optional)
                );
            });
        }

        [Test]
        public void record_type_with_fields()
        {
            var context = new TypeContext();
            var tag = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("SomeNamespace.First")
                .SetTag(tag)
                .AddField(1, "Name", "SomeNamespace.First", FieldQualifier.Optional)
                .AddField(2, "Year", "SomeNamespace.First", FieldQualifier.Optional)
            );

            context.Build();

            var firstType = context.GetRecordType("SomeNamespace.First");
            Assert.That(firstType.FullName, Is.EqualTo("SomeNamespace.First"));
            Assert.That(firstType.Name, Is.EqualTo("First"));
            Assert.That(firstType.Namespace, Is.EqualTo("SomeNamespace"));
            Assert.That(firstType.Tag, Is.EqualTo(tag));
            Assert.That(firstType.BaseType, Is.Null);

            var fieldInfos = firstType.GetFields();
            Assert.That(fieldInfos.Count, Is.EqualTo(2));

            var field1 = firstType.GetField(1);
            Assert.That(field1.Index, Is.EqualTo(1));
            Assert.That(field1.Name, Is.EqualTo("Name"));
            Assert.That(field1.Type, Is.EqualTo(firstType));
            Assert.That(field1.TypeFullName, Is.EqualTo("SomeNamespace.First"));

            var field2 = firstType.GetField(2);
            Assert.That(field2.Index, Is.EqualTo(2));
            Assert.That(field2.Name, Is.EqualTo("Year"));
            Assert.That(field2.Type, Is.EqualTo(firstType));
            Assert.That(field2.TypeFullName, Is.EqualTo("SomeNamespace.First"));  
        }

        [Test]
        public void two_record_types_with_the_same_full_name()
        {
            var context = new TypeContext();
            var tag1 = Guid.NewGuid();
            var tag2 = Guid.NewGuid();

            Assert.Throws<DuplicateTypeNameException>(() =>
            {
                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First")
                    .SetTag(tag1)
                );

                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First")
                    .SetTag(tag2)
                );
            });
        }

        [Test]
        public void two_record_types_with_the_same_name_but_different_namespaces()
        {
            var context = new TypeContext();
            var tag1 = Guid.NewGuid();
            var tag2 = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("SomeNamespace1.First")
                .SetTag(tag1)
            );

            context.DefineRecord(builder => builder
                .SetName("SomeNamespace2.First")
                .SetTag(tag2)
            );

            var type1 = context.GetRecordType("SomeNamespace1.First");
            var type2 = context.GetRecordType("SomeNamespace2.First");

            Assert.That(type1, Is.Not.EqualTo(type2));
            Assert.That(type1.Name, Is.EqualTo(type2.Name));
            Assert.That(type1.FullName, Is.Not.EqualTo(type2.FullName));
        }

        [Test]
        public void two_record_types_with_the_same_tag_name()
        {
            var context = new TypeContext();
            var tag = Guid.NewGuid();

            Assert.Throws<DuplicateTypeTagException>(() =>
            {
                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First1")
                    .SetTag(tag)
                );

                context.DefineRecord(builder => builder
                    .SetName("SomeNamespace.First2")
                    .SetTag(tag)
                );
            });
        }


        [Test]
        public void two_record_types_where_one_extend_another()
        {
            var context = new TypeContext();
            var tag1 = Guid.NewGuid();
            var tag2 = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("First")
                .SetTag(tag1)
                .SetBaseType("Second")
            );

            context.DefineRecord(builder => builder
                .SetName("Second")
                .SetTag(tag2)
            );

            context.Build();

            var type1 = context.GetRecordType("First");
            var type2 = context.GetRecordType("Second");

            Assert.That(type1, Is.Not.EqualTo(type2));
            Assert.That(type2.BaseType, Is.Null);
            Assert.That(type1.BaseType, Is.EqualTo(type2));
        }

        [Test]
        public void one_record_extends_not_existed_type()
        {
            var context = new TypeContext();
            var tag = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("First")
                .SetTag(tag)
                .SetBaseType("NotExistedType")
            );

            Assert.Throws<TypeNotFoundException>(() =>
            {
                context.Build();
            });
        }


        [Test]
        public void one_record_extends_not_record_type()
        {
            var context = new TypeContext();
            var tag = Guid.NewGuid();

            context.DefineRecord(builder => builder
                .SetName("First")
                .SetTag(tag)
                .SetBaseType("Abracadabra")
            );            
            
            context.DefineEnum(builder => builder
                .SetName("Abracadabra")
                .AddConstant(1, "First")
                .AddConstant(2, "Second")
            );

            Assert.Throws<TypeMismatchException>(() =>
            {
                context.Build();
            });
        }


    }
}