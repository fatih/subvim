using System.Linq;
using NUnit.Framework;
using Should;

namespace OmniSharp.Tests.AutoComplete
{
    [TestFixture]
    public class BugFixTests : CompletionTestBase
    {

        [Test]
        public void Should_not_add_property_body()
        {
            CompletionsFor(
                @"public class A {
    public A() 
    {
        string s;
        s.Leng$;
    }
}").ShouldContainOnly("Length");
        }
        
        [Test]
        public void Should_complete_to_string()
        {
            DisplayTextFor(
                @"public class A {
    public A() 
    {
        int n;
        n.T$;
    }
}").First().ShouldEqual("string ToString(string format)");
        }

        [Test]
        public void Should_complete_to_Process1More()
        {
            DisplayTextFor(
                    @"
public class MyClass
{
    public static int Process1More()
    {
        return 10;
    }

    public static void Main()
    {
        var i = Process1M$
    }
}").ShouldContain("int Process1More()");
        }

        [Test]
        public void Should_add_return_type_for_field()
        {
            DisplayTextFor(
                @"
public class MyClass
{
    public static void Main()
    {
        var i = int.maxv$
    }
}").ShouldContainOnly("int MaxValue");

        }

        [Test]
        public void Should_add_return_type_for_property()
        {
            DisplayTextFor(
                @"
public class MyClass
{
    public static void Main()
    {
        var i = ""test"".leng$
    }
}").ShouldContainOnly("int Length");

        }
    }
}
