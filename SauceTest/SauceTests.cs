namespace SauceTest;

using SauceTest.DataProviders;
using SauceTest.Factories;
using SauceTest.Pages;

/// <summary>
/// Tests that check whether login function of saucedemo.com
/// work properly. All the task descriptions are in the README file.
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.All)]
public class SauceTests
{
    private DriverFactory driverFactory;

    /// <summary>
    /// Method that is completed before the tests. This method
    /// creates initialises an instance of DriverFactory class.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        this.driverFactory = new DriverFactory();
    }

    /// <summary>
    /// Checks whether the correct error is thrown
    /// if login and password input fields are empty.
    /// </summary>
    /// <param name="browserName">Browser's name taken from test case arguments.</param>
    [TestCaseSource(typeof(SauceTestDataProvider), nameof(SauceTestDataProvider.UC1_2_BrowserTestCases))]
    public void UC1_TestLoginForm_EmptyCredentials_ReturnsLoginError(string browserName)
    {
        var driver = this.driverFactory.CreateDriver(browserName);

        const string RANDOM_LOGIN_CREDENTIALS = "123";
        const string RANDOM_PASSWORD_CREDENTIALS = "123";
        const string EXPECTED_ERROR = "Epic sadface: Username is required";

        var page = new IndexPage(driver);
        page.Open();
        page.TypeLoginCredentials(RANDOM_LOGIN_CREDENTIALS);
        page.TypePasswordCredentials(RANDOM_PASSWORD_CREDENTIALS);
        page.ClearLoginInput();
        page.ClearPasswordInput();
        page.SubmitForm();
        var actualError = page.GetError();
        Assert.That(actualError, Is.EqualTo(EXPECTED_ERROR));

        page.Close();
    }

    /// <summary>
    /// Checks whether the correct error is thrown
    /// if password input field is empty.
    /// </summary>
    /// <param name="browserName">Browser's name taken from test case arguments.</param>
    [TestCaseSource(typeof(SauceTestDataProvider), nameof(SauceTestDataProvider.UC1_2_BrowserTestCases))]
    public void UC2_TestLoginForm_OnlyUsername_ReturnsPasswordError(string browserName)
    {
        var driver = this.driverFactory.CreateDriver(browserName);

        const string RANDOM_LOGIN_CREDENTIALS = "123";
        const string RANDOM_PASSWORD_CREDENTIALS = "123";
        const string EXPECTED_ERROR = "Epic sadface: Password is required";

        var page = new IndexPage(driver);
        page.Open();
        page.TypeLoginCredentials(RANDOM_LOGIN_CREDENTIALS);
        page.TypePasswordCredentials(RANDOM_PASSWORD_CREDENTIALS);
        page.ClearPasswordInput();
        page.SubmitForm();
        var actualError = page.GetError();
        Assert.That(actualError, Is.EqualTo(EXPECTED_ERROR));

        page.Close();
    }

    /// <summary>
    /// Checks whether the correct page loaded
    /// by comparing the title of the page.
    /// </summary>
    /// <param name="loginCredentials">Valid login credential taken from test case.</param>
    /// <param name="browserName">Browser name - Edge or Chrome only at the moment.</param>
    [TestCaseSource(typeof(SauceTestDataProvider), nameof(SauceTestDataProvider.UC3_ValidLoginTestCases))]
    public void UC3_TestLoginForm_ValidCredentials_SuccessfulLogin(string loginCredentials, string browserName)
    {
        var driver = this.driverFactory.CreateDriver(browserName);

        const string VALID_PASSWORD_CREDENTIALS = "secret_sauce";
        const string EXPECTED_TITLE = "Swag Labs";

        var page = new IndexPage(driver);
        page.Open();
        page.TypeLoginCredentials(loginCredentials);
        page.TypePasswordCredentials(VALID_PASSWORD_CREDENTIALS);
        page.SubmitForm();
        var actualTitle = page.GetTitle();
        Assert.That(actualTitle, Is.EqualTo(EXPECTED_TITLE));

        page.Close();
    }
}