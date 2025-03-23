using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SauceTest.Pages;

public class IndexPage
{
    private static string Url { get; } = "https://www.saucedemo.com/";
    private readonly IWebDriver driver;

    public IndexPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

    private IWebElement LoginInput => driver.FindElement(By.CssSelector("#user-name"));
    private IWebElement PasswordInput => driver.FindElement(By.CssSelector("#password"));
    private IWebElement SubmitButton => driver.FindElement(By.CssSelector("#login-button"));
    
    private string ControlCommand
    {
        get
        {
            if (driver is RemoteWebDriver remoteDriver)
            {
                string? platform = remoteDriver.Capabilities.GetCapability("platformName") as string;
                ArgumentNullException.ThrowIfNull(platform);
                return platform.Contains("mac") ? Keys.Command : Keys.Control;
            }
        
            return Keys.Control;
        }
    }

    public void Open()
    {
        driver.Url = Url;
    }

    public void TypeLoginCredentials(string loginCredentials) => LoginInput.SendKeys(loginCredentials);

    public void TypePasswordCredentials(string passwordCredentials) => PasswordInput.SendKeys(passwordCredentials);

    public void ClearLoginInput()
    {
        LoginInput.SendKeys(ControlCommand + "a" + Keys.Delete);
    }

    public void ClearPasswordInput()
    {
        PasswordInput.SendKeys(ControlCommand + "a" + Keys.Delete);
    }

    public void SubmitForm() => SubmitButton.Click();

    public string GetError()
    {
        var errorElement = driver.FindElement(By.TagName("h3"));
        return errorElement.Text;
    }

    public string GetTitle()
    {
        return driver.Title;
    }

    public void Close() => driver.Quit();
}