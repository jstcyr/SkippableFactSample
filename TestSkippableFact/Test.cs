using Xunit;

namespace TestSkippableFact;
public class Test
{
    [SkippableFact]
    public void MySkippableTest()
    {
        Skip.If(true, "skipped");
    }
}
