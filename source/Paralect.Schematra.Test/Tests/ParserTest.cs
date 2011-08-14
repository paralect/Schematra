using System;
using System.IO;
using NUnit.Framework;
using Paralect.Schematra.Definitions;

namespace Paralect.Schematra.Test.Tests
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void First()
        {
/*            var compilation = GetCompilation(@"Data\Schemas\S04_CircularDependency.schema");

            var parser = new Parser(compilation);
            var context = parser.Parse();*/
        }

        public void DynamicTypeCreation()
        {
            var context = new TypeContext();

            context.DefineRecord(builder => builder
                .SetName("First")
                .SetTag(Guid.NewGuid())
                .SetBaseType("Second")
                .AddField(1, "Name", "Second")
                .AddField(2, "Year", "Second")
            );

            context.DefineRecord(builder => builder
                .SetName("Second")
                .SetBaseType("First")
                .SetTag(Guid.NewGuid())
                .AddField(1, "Name", "First")
                .AddField(2, "Year", "Second")
            );

            context.Build();


        }

        private CompilationDefinition GetCompilation(String path)
        {
            var lexer = new Lexer();
            return lexer.BuildCompilationDefinition(new[] { Path.Combine(GrammerTest.AssemblyDirectory, path) });
        }
    }
}