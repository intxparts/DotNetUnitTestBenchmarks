using System;
using Testing;

namespace FastTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestConsole.RunTests(() => {
                Test.Run("hello", () => {
                    Assert.Pass();
                });
            });
        }
    }
}
