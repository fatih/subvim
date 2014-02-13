using System.Linq;
using NUnit.Framework;
using OmniSharp.GotoImplementation;
using OmniSharp.Parser;
using Should;

namespace OmniSharp.Tests.GotoImplementation
{
    [TestFixture]
    public class GotoImplementationTests
    {
        [Test]
        public void Should_find_usages_in_same_file() {
            const string editorText =
@"
public class BaseClass {}
public class DerivedClassA : BaseClass {}
public class DerivedClassB : BaseClass {}
public class DerivedClassC : BaseClass {}
";

            var fileName = "test.cs";
            var solution = new FakeSolution();
            var project = new FakeProject();
            project.AddFile(editorText, fileName: fileName);
            solution.Projects.Add(project);

            var handler = new GotoImplementationHandler
                (solution, new BufferParser(solution));
            var request = new GotoImplementationRequest
                { Buffer   = editorText
                , Line     = 2
                , Column   = 14 // At word 'BaseClass'
                , FileName = fileName};
            var gotoImplementationResponse = handler.FindDerivedMembersAsQuickFixes(request);

            var quickFixes = gotoImplementationResponse.QuickFixes.ToArray();
            Assert.AreEqual(3, quickFixes.Length);
            quickFixes[0].Text.Trim().ShouldEqual("public class DerivedClassA : BaseClass {}");
            quickFixes[1].Text.Trim().ShouldEqual("public class DerivedClassB : BaseClass {}");
            quickFixes[2].Text.Trim().ShouldEqual("public class DerivedClassC : BaseClass {}");
        }
    }
}
