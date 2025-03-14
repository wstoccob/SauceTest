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

    [TearDown]
    public void TearDown()
    {
        driver.Dispose();
    }
}