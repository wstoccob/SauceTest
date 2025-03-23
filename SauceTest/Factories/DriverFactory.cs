namespace SauceTest.Factories;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

/// <summary>
/// The class used to create instances of driver according to the browser needed.
/// </summary>
public class DriverFactory
{
    /// <summary>
    /// The class used to create instances of driver according to the browser needed.
    /// </summary>
    /// <param name="browserName">string representations of the required browser's name,
    /// only supported browser are Chrome and Edge at the moment.</param>
    /// <returns>The instance of IWebDriver taken from the required method.</returns>
    public IWebDriver CreateDriver(string browserName)
    {
        return browserName switch
        {
            "Chrome" => this.CreateChromeDriver(),
            "Edge" => this.CreateEdgeDriver(),
            _ => throw new InvalidOperationException("Unsupported browser!"),
        };
    }

    /// <summary>
    /// Method that created an instance of ChromeDriver with option
    /// that maximises window size from the start.
    /// </summary>
    /// <returns>The instance of ChromeDriver with options.</returns>
    private IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        return new ChromeDriver(options);
    }

    /// <summary>
    /// Method that created an instance of EdgeDriver with option
    /// that maximises window size from the start.
    /// </summary>
    /// <returns>The instance of EdgeDriver with options.</returns>
    private IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        return new EdgeDriver(options);
    }
}