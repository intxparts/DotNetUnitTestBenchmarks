using System;
using Testing;

namespace FastTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeMilliseconds = Test.Time(() => {
                Test.Run("hello", () => {
                    Assert.Pass();
                });
            });

            var failedTestData = Test.FailedTestData;
            foreach (var f in failedTestData)
            {
                Console.WriteLine($"Test '{f.Key}' failed: {f.Value.Message}\n{f.Value.StackTrace}");
            }

            Console.WriteLine($"Test Count: {Test.TestCount}");
            Console.WriteLine($"Tests Passed: {Test.PassedTests}");
            Console.WriteLine($"Tests Failed: {(Test.TestCount - Test.PassedTests)}");
            Console.WriteLine($"elapsed milliseconds: {timeMilliseconds}");
            Console.WriteLine($"elapsed seconds: {(timeMilliseconds / 1000.0)}");
        }
    }
}
