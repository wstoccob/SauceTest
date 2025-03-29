using OpenQA.Selenium;

namespace SauceTest.Pages;

public class MainPage
{
    private readonly IWebDriver driver;

    private string titleClassName = "app_logo";

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPage"/> class.
    /// </summary>
    /// <param name="driver">Driver that is used to load the page and interact with it.</param>
    public MainPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

    private IWebElement Title => driver.FindElement(By.ClassName(titleClassName));


    /// <summary>
    /// Returns the current page's title.
    /// </summary>
    /// <returns>string representation of the page's current title.</returns>
    public string GetTitle() => this.Title.Text;

    /// <summary>
    /// Closes the driver and, therefore, the page.
    /// </summary>
    public void Close() => this.driver.Quit();
}