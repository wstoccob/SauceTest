namespace SauceTest.DataProviders;

using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// Provides test data for login tests.
/// </summary>
public static class SauceTestDataProvider
{
    /// <summary>
    /// Method that provides test cases for UC1 and UC2 unit tests.
    /// </summary>
    /// <returns>The list of test case data.</returns>
    public static IEnumerable<TestCaseData> UC1_2_BrowserTestCases()
    {
        yield return new TestCaseData("Chrome");
        yield return new TestCaseData("Edge");
    }

    /// <summary>
    /// Method that provides test cases for UC3 unit test.
    /// </summary>
    /// <returns>The list of test case data.</returns>
    public static IEnumerable<TestCaseData> UC3_ValidLoginTestCases()
    {
        string[] users =
        {
            "standard_user", "locked_out_user", "problem_user", "performance_glitch_user", "error_user", "visual_user"
        };
        string[] browsers = { "Chrome", "Edge" };

        foreach (var user in users)
        {
            foreach (var browser in browsers)
            {
                yield return new TestCaseData(user, browser);
            }
        }
    }
}