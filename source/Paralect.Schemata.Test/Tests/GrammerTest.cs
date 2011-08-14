using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Paralect.Schemata.Test.Tests
{
    [TestFixture]
    public class GrammerTest
    {
        [Test]
        public void Test2()
        {
            var path = Path.Combine(AssemblyDirectory, @"Data\Test.types");
            var source = File.ReadAllText(path);
            var grammer = new SchemataGrammer();

            var valid = grammer.IsValid(source);
            Assert.IsTrue(valid, "Grammer is invalid");

            var root = grammer.GetRoot(source);
            grammer.DisplayTree(root, 1);
        }

        [Test]
        public void ToyTest()
        {
            for (int i = 0; i < 100; i++)
            {
                var guid = Guid.NewGuid();
                var base64 = Convert.ToBase64String(guid.ToByteArray());
                Console.WriteLine(base64 + " | " + guid.ToString());
            }
        }

        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


    }
}