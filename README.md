# NavigatorAutomationFramework

Navigator Automation Framework is an automated testing framework writen in Selenium, with C# and in NUnit. 
The framework consists of several smoke case tests, along with all the necessary helper methods and resources (such as images and necessary drivers)

How to use: 
Clone the repository to your local machine. The necessary drivers, photos and other testing resources will be immediatelly placed in your local repository. **Note: In the _VerifyLocationCreation_ test and in the _PopulateCreationForm_ method, for uploading the photo a full path is added. In order for the test to work, just copy the photo path and paste it into the path within the method.**
In case necessary packages are missing - the followning packages need to be downloaded and installed in order for the framework to work: 
- covert.collector
- Microsoft.NET.Test.SDK
- NUnit
- NUnit.Analyzers
- NUnit3TestAdapter
- Selenium.Support
- Selenium.WebDriver.ChromeDriver

You can verify the necessary packages in the screenshot below: 
![image](https://github.com/ElmoThePuppet/NavigatorAutomationFramework/assets/45720097/fef985b3-02c6-489a-b593-a3901e206914)

The framework was created using Page Object Model, and it has the following segments:
- TestResources - This segment holds resources necessary for the tests to run such as photos
- PageObjects - This segment holds page objects as they are divided into two different objects: Home screen and Location details. All helper methods and initializations are in their respective classes
- Unit test - This segment holds the smoke tests for the application

**Testing note: Due to the fact that some functionalities of the application are either not working or user might lack the permissions - the test cases for testing purposes have been adjusted using Asserts, they have been created in a way to pass the test in case some test cases fail. This is not to be taken as actual behavior, but only as a way for the tests to pass for testing purposes. This should not be done in a real life production environment.**

[Navigator Testing Documentation.zip](https://github.com/ElmoThePuppet/NavigatorAutomationFramework/files/14009102/Navigator.Testing.Documentation.zip)

Above you will find the .zip file containing testing documentation. This .zip file holds all the test cases, which have been split into 3 groups: Smoke tests, Homepage tests and Location details tests. Test cases are written in detail with steps to reproduce. 
This file also contains found bugs that are documented with screenshots in a separate folder which you can find by refering to the bug documentation. **Bugs note: Some functionalities may not actually be bugs because they might occur due to insufficient permissions and/or authentication, but in my testing as I noticed functionalities are not working with limited error understanding, I treated them as bugs. If this is expected behavior - feel free to ignore those bugs.**
