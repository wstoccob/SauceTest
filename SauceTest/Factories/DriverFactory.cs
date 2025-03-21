using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SauceTest.Factories;

public class DriverFactory
{
    public IWebDriver CreateDriver(string browserName)
    {
        return browserName switch
        {
            "Chrome" => new ChromeDriver(),
            "Edge" => new EdgeDriver(),
            _ => throw new InvalidOperationException("Unsupported browser!"),
        };
    }
}