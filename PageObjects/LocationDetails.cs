using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace NavigatorAutomationFramework.PageObjects
{
    public class LocationDetails
    {
        private IWebDriver driver;
        public WebDriverWait wait
        {
            get
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
            internal set
            { }
        }
        public LocationDetails(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement GetWebElement(string locator)
        {
            By elementLocator = By.CssSelector(locator);
            return wait.Until(d => d.FindElement(elementLocator));
        }
        public bool IsLoaded()
        {
            string dynamicElementSelector = "div.breadcrumbs-container";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool isVisible = wait.Until(driver =>
            {
                IWebElement element = driver.FindElement(By.CssSelector(dynamicElementSelector));
                return element != null && element.Displayed;
            });
            return isVisible;
        }
        public int RatingHelper(By locator, int starValue)
        {
            IWebElement ratingElement = wait.Until(d => d.FindElement(locator));
            int starCount = GetStarCount(ratingElement);

            // Click the star
            IWebElement star = wait.Until(d => ratingElement.FindElement(By.CssSelector($".empty span[data-value='{starValue}']")));
            star.Click();

            // Return the updated star count after clicking
            return GetStarCount(ratingElement);
        }
        public int GetStarCount(IWebElement ratingElement)
        {
            // Find the stars container
            IWebElement starsContainer = wait.Until(d => d.FindElement(By.CssSelector(".stars-container")));

            // Find the full stars within the container
            IReadOnlyCollection<IWebElement> fullStars = starsContainer.FindElements(By.CssSelector(".full span.iconav-star-3"));
            IWebElement ratingsElement = wait.Until(d => d.FindElement(By.CssSelector("span.nmb-votes")));
            string ratingsText = ratingsElement.Text.Trim();
            string numericPart = ratingsText.Split(' ')[0]; // Split by space and take the first part
            return int.Parse(numericPart);
        }
        public int ClickAndReturnUpdatedStarCount(By locator, int starValue)
        {
            IWebElement ratingElement = wait.Until(driver =>
            {
                IWebElement element = driver.FindElement(locator);
                return element.Displayed && element.Enabled ? element : null;
            });
            if (ratingElement == null)
            {
                throw new NoSuchElementException("Element is not displayed and enabled.");
            }
            int initialStarCount = GetStarCount(ratingElement);
            wait.Until(driver =>
            {
                try
                {
                    ratingElement.Click();
                    return true;
                }
                catch (ElementClickInterceptedException)
                {
                    return false;
                }
            });
            return GetStarCount(driver.FindElement(locator));
        }
        public bool IsEmailLinkClickable()
        {
            try
            {
                IWebElement emailLink = wait.Until(driver => driver.FindElement(By.CssSelector("div.email a")));
                return emailLink.Displayed && emailLink.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void ClickShowMore()
        {
            IWebElement showMoreDiv = GetWebElement(".show-more-content");
            showMoreDiv.Click();
        }

        public bool IsShowMoreExpanded()
        {
            IWebElement showMoreDiv = GetWebElement(".show-more-content");
            string classAttribute = showMoreDiv.GetAttribute("class");
            return classAttribute.Contains("more");
        }
        public void HoverOverSocialMedia()
        {
            IWebElement showSocialMedia = wait.Until(d => d.FindElement(By.CssSelector(".share-buttons.hidden")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].classList.remove('hidden')", showSocialMedia);
            Thread.Sleep(1000);
            IWebElement socialMediaIcon = GetWebElement(".share-buttons .iconav-twitter");
            socialMediaIcon.Click();
        }
        public void ScrollDragger(int height)
        {
            IWebElement dragger = driver.FindElement(By.CssSelector("div.mCSB_dragger"));

            Actions builder = new Actions(driver);
            builder.MoveToElement(dragger).DragAndDropToOffset(dragger, 0, height).Build().Perform();
        }
        public void PopulateCreationForm()
        {
            var name = GetWebElement("input#poi_name[title='Naziv']");
            name.SendKeys("Ime Lokacije");
            var city = GetWebElement("input#poi_city_name[title='Grad']");
            city.SendKeys("Sarajevo");
            var street = GetWebElement("input#poi_place_id[title='Ulica']");
            street.SendKeys("Mis Irbina");
            var zipcode = GetWebElement("input#poi_zip_code[title='Poštanski broj']");
            zipcode.SendKeys("71000");
            var houseNumber = GetWebElement("input#poi_house_number[title='Kućni broj']");
            houseNumber.SendKeys("1");
            var alternateAddress = GetWebElement("input#poi_street_name_alt[title='Alternativna adresa']");
            alternateAddress.SendKeys("Alternativna adresa");
            var description = GetWebElement("textarea#poi_description");
            description.SendKeys("Ovo je opis lokacije.");
            var categoryButton = GetWebElement("button.btn.btn-small[title='Odaberite kategoriju']");
            categoryButton.Click();
            IWebElement selectElement = wait.Until(d => d.FindElement(By.TagName("select")));
            SelectElement select = new SelectElement(selectElement);
            select.SelectByValue("8");
            var tagName = GetWebElement("input.ui-widget-content.ui-autocomplete-input");
            tagName.SendKeys("TestTag");
            var saturdayButton = GetWebElement("button#btn_day_sat[data-day='sat']");
            saturdayButton.Click();
            var workingHoursFrom = GetWebElement("input#working_hours_0_0.w_hours_check");
            workingHoursFrom.SendKeys("8");
            var workingHoursTo = GetWebElement("input#working_hours_0_1.w_hours_check");
            workingHoursTo.SendKeys("17");
            var addHoursButton = GetWebElement("button.btn.btnAddWorkingHours");
            addHoursButton.Click();
            ScrollDragger(440);
            Actions actions = new Actions(driver);

            for (int i = 0; i < 12; i++)
            {
                actions.SendKeys(Keys.Tab).Perform();
            }
            var landline = GetWebElement("input#poi_phone[title='Fiksni']");
            landline.SendKeys("033111222");
            var mobile = GetWebElement("input#poi_mobile_phone[title='Mobitel']");
            mobile.SendKeys("061333444");
            var web = GetWebElement("input#poi_web[title='Web']");
            web.SendKeys("https://www.google.com");
            var email = GetWebElement("input#poi_email[title='Email']");
            email.SendKeys("email@gmail.com");
            var hasWifi = GetWebElement("input#poi_has_wifi.ember-view.ember-checkbox");
            hasWifi.Click();
            var wifiPassword = GetWebElement("input#poi_wifi_pass[title='Wi-Fi šifra']");
            wifiPassword.SendKeys("password");
            var wifiSsid = GetWebElement("input#poi_wifi_ssid[title='Naziv mreže']");
            wifiSsid.SendKeys("Moj WiFi");
            wifiSsid.SendKeys(Keys.Tab);
            var acceptsCreditCard = GetWebElement("input#poi_accepts_credit_cards.ember-view.ember-checkbox");
            acceptsCreditCard.Click();
            var visaCreditCard = GetWebElement("input#chk_credit_card_1");
            visaCreditCard.Click();
            var imagePath = "C:\\Users\\Namir\\source\\repos\\NavigatorAutomationFramework\\TestResources\\foodImage.jpg";
            var uploadPhotoButton = GetWebElement("input#fileToUpload");
            uploadPhotoButton.SendKeys(imagePath);
            ScrollDragger(100);
            var comment = GetWebElement("textarea#poi_comment");
            comment.SendKeys("Ovo je komentar lokacije.");
            var submitButton = GetWebElement("button.btn.btn-success");
            submitButton.Click();
        }
    }
}