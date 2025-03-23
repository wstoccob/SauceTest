using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SauceTest.Factories;

public class DriverFactory
{
    private static IWebDriver? _driver;

    public IWebDriver CreateDriver(string browserName)
    {
        return _driver ??= browserName switch
        {
            "Chrome" => CreateChromeDriver(),
            "Edge" => CreateEdgeDriver(),
            _ => throw new InvalidOperationException("Unsupported browser!"),
        };
    }

    private IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized"); // Maximizing browser
        options.AddArgument("--disable-popup-blocking");
        return new ChromeDriver(options);
    }

    private IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-popup-blocking");
        return new EdgeDriver(options);
    }
}