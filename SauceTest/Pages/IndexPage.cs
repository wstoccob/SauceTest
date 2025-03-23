namespace SauceTest.Pages;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

/// <summary>
/// A class that implements Page Object Pattern for this task.
/// </summary>
public class IndexPage
{
    private readonly IWebDriver driver;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexPage"/> class.
    /// </summary>
    /// <param name="driver">Driver that is used to load the page and interact with it.</param>
    public IndexPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

    private static string Url { get; } = "https://www.saucedemo.com/";

    private IWebElement LoginInput => this.driver.FindElement(By.CssSelector("#user-name"));

    private IWebElement PasswordInput => this.driver.FindElement(By.CssSelector("#password"));

    private IWebElement SubmitButton => this.driver.FindElement(By.CssSelector("#login-button"));

    private string ControlCommand
    {
        get
        {
            if (this.driver is RemoteWebDriver remoteDriver)
            {
                string? platform = remoteDriver.Capabilities.GetCapability("platformName") as string;
                ArgumentNullException.ThrowIfNull(platform);
                return platform.Contains("mac") ? Keys.Command : Keys.Control;
            }

            return Keys.Control;
        }
    }

    /// <summary>
    /// Sets the driver's url to the Url of Swag Labs page contained in the Url property.
    /// </summary>
    public void Open()
    {
        this.driver.Url = Url;
    }

    /// <summary>
    /// Types login credentials into the login input field.
    /// </summary>
    /// <param name="loginCredentials">string representation of the login credentials,
    /// required for the login input field.</param>
    public void TypeLoginCredentials(string loginCredentials) => this.LoginInput.SendKeys(loginCredentials);

    /// <summary>
    /// Types password credentials into the password input field.
    /// </summary>
    /// <param name="passwordCredentials">string representation of the password credentials,
    /// required for the password input field.</param>
    public void TypePasswordCredentials(string passwordCredentials) => this.PasswordInput.SendKeys(passwordCredentials);

    /// <summary>
    /// Clear login input field using Ctrl + A + Delete combination of keys,
    /// since element.Clear() does not work properly for input tag.
    /// </summary>
    public void ClearLoginInput()
    {
        this.LoginInput.SendKeys(this.ControlCommand + "a" + Keys.Delete);
    }

    /// <summary>
    /// Clear password input field using Ctrl + A + Delete combination of keys,
    /// since element.Clear() does not work properly for input tag.
    /// </summary>
    public void ClearPasswordInput()
    {
        this.PasswordInput.SendKeys(this.ControlCommand + "a" + Keys.Delete);
    }

    /// <summary>
    /// Clicks submit button on the page.
    /// </summary>
    public void SubmitForm() => this.SubmitButton.Click();

    /// <summary>
    /// Gets the error text on the page.
    /// </summary>
    /// <returns>string representation of the error's text.</returns>
    public string GetError()
    {
        var errorElement = this.driver.FindElement(By.TagName("h3"));
        return errorElement.Text;
    }

    /// <summary>
    /// Returns the current page's title.
    /// </summary>
    /// <returns>string representation of the page's current title.</returns>
    public string GetTitle() => this.driver.Title;

    /// <summary>
    /// Closes the driver and, therefore, the page.
    /// </summary>
    public void Close() => this.driver.Quit();
}