using RefactoringEssentials.CSharp.CodeRefactorings;
using Xunit;

namespace RefactoringEssentials.Tests.CSharp.CodeRefactorings
{
    public class ChangeAccessModifierTests : CSharpCodeRefactoringTestBase
    {
        [Fact]
        public void TestNoEnumMember()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
enum Test
{
	$Foo
}");
        }

        [Fact]
        public void TestNoInterfaceMember()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
interface Test
{
	void $Foo ();
}");
        }

        [Fact]
        public void TestNoExplicitInterfaceImplementationMember()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
interface Test
{
	void Foo ();
}
class TestClass : Test
{
	void $Test.Foo () {}
}");
        }

        [Fact]
        public void TestNoOverrideMember()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
class TestClass : Test
{
	public override string $ToString()
	{
		return ""Test"";
	}
		
}");
        }

        [Fact(Skip="Not implemented!")]
        public void TestType()
        {
            Test<ChangeAccessModifierAction>(@"
class $Foo
{
}", @"
public class Foo
{
}");
        }

        [Fact(Skip="Not implemented!")]
        public void TestMethodToProtected()
        {
            Test<ChangeAccessModifierAction>(@"
class Foo
{
	void $Bar ()
	{
	}
}", @"
class Foo
{
	protected void Bar ()
	{
	}
}");
        }

        [Fact(Skip="Not implemented!")]
        public void TestPrivateMethodToProtected()
        {
            Test<ChangeAccessModifierAction>(@"
class Foo
{
	$private void Bar ()
	{
	}
}", @"
class Foo
{
	protected void Bar ()
	{
	}
}");
        }

        [Fact(Skip="Not implemented!")]
        public void TestMethodToProtectedInternal()
        {
            Test<ChangeAccessModifierAction>(@"
class Foo
{
	void $Bar ()
	{
	}
}", @"
class Foo
{
	protected internal void Bar ()
	{
	}
}", 1);
        }

        [Fact(Skip="Not implemented!")]
        public void TestAccessor()
        {
            Test<ChangeAccessModifierAction>(@"
class Foo
{
	public int Bar
	{
		get; $set;
	}
}", @"
class Foo
{
	public int Bar
	{
		get; private set;
	}
}");
        }

        [Fact]
        public void TestStrictAccessor()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
class Foo
{
	private int Bar
	{
		get; $set;
	}
}");
        }

        [Fact(Skip="Not implemented!")]
        public void TestChangeAccessor()
        {
            Test<ChangeAccessModifierAction>(@"
class Foo
{
	public int Bar
	{
		get; private $set;
	}
}", @"
class Foo
{
	public int Bar
	{
		get; protected set;
	}
}");
        }

        [Fact]
        public void TestReturnTypeWrongContext()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
class Test
{
	public $void Foo () {}
}");
        }

        [Fact]
        public void TestWrongModiferContext()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"
class Test
{
	public $virtual void Foo () {}
}");
        }

        [Fact]
        public void TestMethodImplementingInterface()
        {
            TestWrongContext<ChangeAccessModifierAction>(@"using System;

class BaseClass : IDisposable
{
	public void $Dispose()
	{
	}
}");
        }


    }
}

