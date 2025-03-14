using OpenQA.Selenium;

namespace SauceTest.Pages;

public class IndexPage
{
    private static string Url { get; } = "https://www.saucedemo.com/";
    private readonly WebDriver driver;

    public IndexPage(WebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

    private IWebElement LoginInput => driver.FindElement(By.Id("user-name"));
    private IWebElement PasswordInput => driver.FindElement(By.Id("password"));
    private IWebElement SubmitButton => driver.FindElement(By.Id("login-button"));
    
    private string ControlCommand
    {
        get
        {
            string? platform = driver.Capabilities.GetCapability("platformName") as string;
            ArgumentNullException.ThrowIfNull(platform);
            return platform.Contains("mac") ? Keys.Command : Keys.Control;
        }
    }

    public IndexPage Open()
    {
        driver.Url = Url;
        return this;
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
}