using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NavigatorAutomationFramework.PageObjects
{
    public class HomeScreen
    {
        private IWebDriver driver;

        public bool? IsLoaded
        {
            get
            {
                return driver.Title.Contains("Navigator | Mapa Sarajeva");
            }
            internal set
            { }
        }

        public WebDriverWait wait
        {
            get
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
            internal set
            { }
        }

        public HomeScreen(IWebDriver driver)
        {
            this.driver = driver;
        }

        internal void GoToPage()
        {
            driver.Navigate().GoToUrl("https://www.navigator.ba/");
        }
        public void GoToNarodnoPozoriste()
        {
            GoToPage();
            var pozoristeCategory = GetPozoristaCategory();
            pozoristeCategory.Click();
            var categorySelection = GetCategorySelection();
            categorySelection.Click();
        }

        public IWebElement GetSearchBar()
        {
            return driver.FindElement(By.CssSelector("input.ember-view.ember-text-field.tt-query[placeholder='Traži ulicu ili objekat']"));

        }
        public IWebElement GetFirstSuggestion()
        {
            return wait.Until(driver =>
            {
                var suggestions = driver.FindElements(By.CssSelector(".tt-suggestions .tt-suggestion"));
                return suggestions.Count > 0 ? suggestions[0] : null;
            });
        }
        public IWebElement GetCategorySelection()
        {
            return wait.Until(driver =>
            {
                return driver.FindElement(By.CssSelector("a[class='ember-view'][href='#/p/narodno-pozoriste?list=sarajevska-pozorista']"));
            });
        }
        public IWebElement GetPozoristaCategory()
        {
            return wait.Until(driver =>
            {
                return driver.FindElement(By.CssSelector("a[class='ember-view'][href='#/list/sarajevska-pozorista']"));
            });

        }
    }
}