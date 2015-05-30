using NUnit.Framework;
using RefactoringEssentials.CSharp.CodeRefactorings;

namespace RefactoringEssentials.Tests.CSharp.CodeRefactorings
{
    [TestFixture]
    public class CheckStringBuilderIndexValueTests : ContextActionTestBase
    {
        [Test]
        public void TestSimpleCase()
        {
            Test<CheckStringBuilderIndexValueCodeRefactoringProvider>(@"
class Test
{
    public static void Main (System.Text.StringBuilder args)
    {
        Console.WriteLine(args[$5]);
    }
}", @"
class Test
{
    public static void Main (System.Text.StringBuilder args)
    {
        if (args.Length > 5)
            Console.WriteLine(args[5]);
    }
}");
        }

        [Test]
        public void TestNestedCase()
        {
            Test<CheckStringBuilderIndexValueCodeRefactoringProvider>(@"
class Test
{
    public static void Main (System.Text.StringBuilder args)
    {
        if (true)
            if (true)
                Console.WriteLine(args[$5 + 234 - 234]);
    }
}", @"
class Test
{
    public static void Main (System.Text.StringBuilder args)
    {
        if (true)
            if (true)
                if (args.Length > 5 + 234 - 234)
                    Console.WriteLine(args[5 + 234 - 234]);
    }
}");
        }
    }
}