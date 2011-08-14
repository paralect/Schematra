using System;
using System.IO;
using NUnit.Framework;
using Paralect.Schematra.Definitions;

namespace Paralect.Schematra.Test.Tests
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void S03_Full_Context()
        {
            var context = GetContext(@"Data\Schemas\S03_Full.schema");
        }

        [Test]
        public void S03_Full()
        {
            var compilation = GetCompilation(@"Data\Schemas\S03_Full.schema");

            Assert.AreEqual(compilation.Units.Count, 1);
            Assert.AreEqual(compilation.Units[0].TypeDefinitions.Count, 4);

            Assert.AreEqual(compilation.Units[0].Usings.Count, 2);
            Assert.AreEqual(compilation.Units[0].Usings[0], "Hello.Bye");
            Assert.AreEqual(compilation.Units[0].Usings[1], "Hello.Bye.Goodbye");

            // First schema
            {
                var schema = (RecordDefinition)compilation.Units[0].TypeDefinitions[0];
                Assert.AreEqual("FirstSchema", schema.Name);
                Assert.AreEqual("", schema.Namespace);
                Assert.AreEqual(Guid.Parse("8170cb805b92441eb198fb71f343b717"), schema.Tag);
                Assert.AreEqual("SecondSchema", schema.Extends);
                Assert.AreEqual(2, schema.FieldDefinitions.Count);

                var field1 = schema.FieldDefinitions[0];
                Assert.AreEqual(1, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.TypeName);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.FieldDefinitions[1];
                Assert.AreEqual(2, field2.Index);
                Assert.AreEqual(FieldQualifier.Required, field2.Qualifier);
                Assert.AreEqual("String", field2.TypeName);
                Assert.AreEqual("Name", field2.Name);
            }

            // Second schema
            {
                var schema = (RecordDefinition)compilation.Units[0].TypeDefinitions[1];
                Assert.AreEqual("SecondSchema", schema.Name);
                Assert.AreEqual("Topmost.Namespace", schema.Namespace);
                Assert.AreEqual(Guid.Parse("cd545b5dc832441eb198fb71f3436cde"), schema.Tag);
                Assert.AreEqual(null, schema.Extends);
                Assert.AreEqual(3, schema.FieldDefinitions.Count);

                var field1 = schema.FieldDefinitions[0];
                Assert.AreEqual(1, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.TypeName);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.FieldDefinitions[1];
                Assert.AreEqual(2, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.TypeName);
                Assert.AreEqual("Count", field2.Name);

                var field3 = schema.FieldDefinitions[2];
                Assert.AreEqual(3, field3.Index);
                Assert.AreEqual(FieldQualifier.Historic, field3.Qualifier);
                Assert.AreEqual("Double", field3.TypeName);
                Assert.AreEqual("Rate", field3.Name);
            }            
            
            // Third schema
            {
                var schema = (RecordDefinition)compilation.Units[0].TypeDefinitions[2];
                Assert.AreEqual("ThirdSchema", schema.Name);
                Assert.AreEqual("Topmost.Namespace.Inner.Namespace", schema.Namespace);
                Assert.AreEqual(Guid.Parse("c4d6d8d8d8d8d8d8d8d8d6834343cd32"), schema.Tag);
                Assert.AreEqual(null, schema.Extends);
                Assert.AreEqual(2, schema.FieldDefinitions.Count);

                var field1 = schema.FieldDefinitions[0];
                Assert.AreEqual(4, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.TypeName);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.FieldDefinitions[1];
                Assert.AreEqual(18, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.TypeName);
                Assert.AreEqual("Count", field2.Name);
            }

            // Fourth schema
            {
                var schema = (RecordDefinition)compilation.Units[0].TypeDefinitions[3];
                Assert.AreEqual("FourthSchema", schema.Name);
                Assert.AreEqual("", schema.Namespace);
                Assert.AreEqual(Guid.Empty, schema.Tag);
                Assert.AreEqual("ThirdSchema", schema.Extends);
                Assert.AreEqual(2, schema.FieldDefinitions.Count);

                var field1 = schema.FieldDefinitions[0];
                Assert.AreEqual(4, field1.Index);
                Assert.AreEqual(FieldQualifier.Optional, field1.Qualifier);
                Assert.AreEqual("Int32", field1.TypeName);
                Assert.AreEqual("Year", field1.Name);

                var field2 = schema.FieldDefinitions[1];
                Assert.AreEqual(18, field2.Index);
                Assert.AreEqual(FieldQualifier.Optional, field2.Qualifier);
                Assert.AreEqual("Int32", field2.TypeName);
                Assert.AreEqual("Count", field2.Name);
            }
        }

        private CompilationDefinition GetCompilation(String path)
        {
            var lexer = new Paralect.Schematra.Definitions.Lexer();
            return lexer.BuildCompilationDefinition(new[] { Path.Combine(GrammerTest.AssemblyDirectory, path) });
        }

        private TypeContext GetContext(String path)
        {
            var lexer = new Lexer();
            return lexer.Build(new[] { Path.Combine(GrammerTest.AssemblyDirectory, path) });
        }
    }
}