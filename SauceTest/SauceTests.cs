using SauceTest.Factories;
using SauceTest.Pages;

namespace SauceTest;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Tests
{
    private DriverFactory _driverFactory;

    [SetUp]
    public void Setup()
    {
        _driverFactory = new DriverFactory();
    }

    [TestCase("Chrome")]
    [TestCase("Edge")]
    public void UC1_TestLoginForm_EmptyCredentials(string browserName)
    {
        var _driver = _driverFactory.CreateDriver(browserName);
        
        const string RANDOM_LOGIN_CREDENTIALS = "123";
        const string RANDOM_PASSWORD_CREDENTIALS = "123";
        const string EXPECTED_ERROR = "Epic sadface: Username is required";
        
        var page = new IndexPage(_driver);
        page.Open();
        page.TypeLoginCredentials(RANDOM_LOGIN_CREDENTIALS);
        page.TypePasswordCredentials(RANDOM_PASSWORD_CREDENTIALS);
        page.ClearLoginInput();
        page.ClearPasswordInput();
        page.SubmitForm();
        var actualError = page.GetError();
        Assert.That(actualError, Is.EqualTo(EXPECTED_ERROR));
        
        _driver.Quit();
    }
    
    [TestCase("Chrome")]
    [TestCase("Edge")]
    public void UC1_TestLoginForm_OnlyUsername(string browserName)
    {
        var _driver = _driverFactory.CreateDriver(browserName);
        
        const string RANDOM_LOGIN_CREDENTIALS = "123";
        const string RANDOM_PASSWORD_CREDENTIALS = "123";
        const string EXPECTED_ERROR = "Epic sadface: Password is required";
        
        var page = new IndexPage(_driver);
        page.Open();
        page.TypeLoginCredentials(RANDOM_LOGIN_CREDENTIALS);
        page.TypePasswordCredentials(RANDOM_PASSWORD_CREDENTIALS);
        page.ClearPasswordInput();
        page.SubmitForm();
        var actualError = page.GetError();
        Assert.That(actualError, Is.EqualTo(EXPECTED_ERROR));
        
        _driver.Quit();
    }
    
    [TestCase("standard_user", "Chrome")]
    [TestCase("locked_out_user", "Chrome")]
    [TestCase("problem_user", "Chrome")]
    [TestCase("performance_glitch_user", "Chrome")]
    [TestCase("error_user", "Chrome")]
    [TestCase("visual_user", "Chrome")]
    [TestCase("standard_user", "Edge")]
    [TestCase("locked_out_user", "Edge")]
    [TestCase("problem_user", "Edge")]
    [TestCase("performance_glitch_user", "Edge")]
    [TestCase("error_user", "Edge")]
    [TestCase("visual_user", "Edge")]
    public void UC1_TestLoginForm_ValidCredentials(string loginCredentials, string browserName)
    {
        var _driver = _driverFactory.CreateDriver(browserName);

        const string VALID_PASSWORD_CREDENTIALS = "secret_sauce";
        const string EXPECTED_TITLE = "Swag Labs";
        
        var page = new IndexPage(_driver);
        page.Open();
        page.TypeLoginCredentials(loginCredentials);
        page.TypePasswordCredentials(VALID_PASSWORD_CREDENTIALS);
        page.SubmitForm();
        var actualTitle = page.GetTitle();
        Assert.That(actualTitle, Is.EqualTo(EXPECTED_TITLE));
        
        _driver.Quit();
    }
}