using System;
using System.IO;
using NUnit.Framework;
using Paralect.Schematra.Definitions;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra.Test.Tests
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void S01_Empty()
        {
            var context = GetContext(@"Data\Schemas\S01_Empty.schema");
            context.Build();

            var recordType = (RecordType)context.GetByFullName("SimpleSchema");
            Assert.AreEqual("SimpleSchema", recordType.Name);
            Assert.AreEqual("", recordType.Namespace);
            Assert.AreEqual(Guid.Parse("9090cb805b92441eb198fb71f343b717"), recordType.Tag);
            Assert.AreEqual(null, recordType.BaseType);
            Assert.AreEqual(0, recordType.GetFields().Count);
        }

        [Test]
        public void S01_WithProperties()
        {
            var context = GetContext(@"Data\Schemas\S02_WithProperties.schema");
            context.Build();

            var recordType = (RecordType)context.GetByFullName("SimpleRecord");
            Assert.AreEqual("SimpleRecord", recordType.Name);
            Assert.AreEqual("", recordType.Namespace);
            Assert.AreEqual(Guid.Parse("9090cb805b92441eb198fb71f343b717"), recordType.Tag);
            Assert.AreEqual(null, recordType.BaseType);
            Assert.AreEqual(2, recordType.GetFields().Count);

            var field1 = recordType.GetField(1);
            Assert.AreEqual(1, field1.Index);
            Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
            Assert.AreEqual("Int32", field1.Type.Name);
            Assert.AreEqual("Year", field1.Name);

            var field2 = recordType.GetField(2);
            Assert.AreEqual(2, field2.Index);
            Assert.AreEqual(FieldQualifier.Required, field2.Qualifier);
            Assert.AreEqual("String", field2.Type.Name);
            Assert.AreEqual("Text", field2.Name);
        }

        [Test]
        public void S03_Full_Context()
        {
            var context = GetContext(@"Data\Schemas\S03_Full.schema");
            context.Build();

            // First schema
            {
                var recordType = (RecordType) context.GetByFullName("FirstSchema");
                Assert.AreEqual("FirstSchema", recordType.Name);
                Assert.AreEqual("", recordType.Namespace);
                Assert.AreEqual(Guid.Parse("8170cb805b92441eb198fb71f343b717"), recordType.Tag);
                Assert.AreEqual("Topmost.Namespace.SecondSchema", recordType.BaseType.FullName);
                Assert.AreEqual(2, recordType.GetFields().Count);

                var field1 = recordType.GetField(1);
                Assert.AreEqual(1, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.Type.Name);
                Assert.AreEqual("Year", field1.Name);

                var field2 = recordType.GetField(2);
                Assert.AreEqual(2, field2.Index);
                Assert.AreEqual(FieldQualifier.Required, field2.Qualifier);
                Assert.AreEqual("String", field2.Type.Name);
                Assert.AreEqual("Name", field2.Name);
            }

            // Second schema
            {
                var schema = (RecordType) context.GetByFullName("Topmost.Namespace.SecondSchema");
                Assert.AreEqual("SecondSchema", schema.Name);
                Assert.AreEqual("Topmost.Namespace", schema.Namespace);
                Assert.AreEqual(Guid.Parse("cd545b5dc832441eb198fb71f3436cde"), schema.Tag);
                Assert.AreEqual(null, schema.BaseType);
                Assert.AreEqual(3, schema.GetFields().Count);

                var field1 = schema.GetField(1);
                Assert.AreEqual(1, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.Type.Name);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.GetField(2);
                Assert.AreEqual(2, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.Type.Name);
                Assert.AreEqual("Count", field2.Name);

                var field3 = schema.GetField(3);
                Assert.AreEqual(3, field3.Index);
                Assert.AreEqual(FieldQualifier.Historic, field3.Qualifier);
                Assert.AreEqual("Double", field3.Type.Name);
                Assert.AreEqual("Rate", field3.Name);
            }

            // Third schema
            {
                var schema = (RecordType) context.GetByFullName("Topmost.Namespace.Inner.Namespace.ThirdSchema");
                Assert.AreEqual("ThirdSchema", schema.Name);
                Assert.AreEqual("Topmost.Namespace.Inner.Namespace", schema.Namespace);
                Assert.AreEqual(Guid.Parse("c4d6d8d8d8d8d8d8d8d8d6834343cd32"), schema.Tag);
                Assert.AreEqual(null, schema.BaseType);
                Assert.AreEqual(2, schema.GetFields().Count);

                var field1 = schema.GetField(4);
                Assert.AreEqual(4, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.Type.Name);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.GetField(18);
                Assert.AreEqual(18, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.Type.Name);
                Assert.AreEqual("Count", field2.Name);
            }

            // Fourth schema
            {
                var schema = (RecordType)context.GetByFullName("FourthSchema");
                Assert.AreEqual("FourthSchema", schema.Name);
                Assert.AreEqual("", schema.Namespace);
                Assert.AreEqual(Guid.Parse("d889843c-deee-d8d8-d8d8-d6834343cd32"), schema.Tag);
                Assert.AreEqual("ThirdSchema", schema.BaseType.Name);
                Assert.AreEqual(2, schema.GetFields().Count);

                var field1 = schema.GetField(4);
                Assert.AreEqual(4, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.Type.Name);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.GetField(18);
                Assert.AreEqual(18, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.Type.Name);
                Assert.AreEqual("Count", field2.Name);
            }
        }

        [Test]
        public void S04_CircularDependency()
        {
            var context = GetContext(@"Data\Schemas\S04_CircularDependency.schema");

            Assert.Throws<CircularDependencyException>(() =>
            {
                context.Build();
            });
        }

        private TypeContext GetContext(String path)
        {
            var lexer = new Lexer();
            return lexer.Build(
                new[] { Path.Combine(GrammerTest.AssemblyDirectory, path)}
            );
        }
    }
}