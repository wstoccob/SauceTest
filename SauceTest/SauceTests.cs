using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceTest.Pages;

namespace SauceTest;

[Parallelizable]
public class Tests
{
    private WebDriver driver;
    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [Test]
    public void UC1_TestLoginForm_EmptyCredentials()
    {
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
    }
    
    [Test]
    public void UC1_TestLoginForm_OnlyUsername()
    {
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
    }
    
    [TestCase("standard_user")]
    [TestCase("locked_out_user")]
    [TestCase("problem_user")]
    [TestCase("performance_glitch_user")]
    [TestCase("error_user")]
    [TestCase("visual_user")]
    public void UC1_TestLoginForm_ValidCredentials(string loginCredentials)
    {
        const string VALID_PASSWORD_CREDENTIALS = "secret_sauce";
        const string EXPECTED_TITLE = "Swag Labs";
        
        var page = new IndexPage(driver);
        page.Open();
        page.TypeLoginCredentials(loginCredentials);
        page.TypePasswordCredentials(VALID_PASSWORD_CREDENTIALS);
        page.ClearPasswordInput();
        page.SubmitForm();
        var actualTitle = page.GetTitle();
        Assert.That(actualTitle, Is.EqualTo(EXPECTED_TITLE));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}