BetterRobokassa
===============

Better support for [Robokassa.ru][1] for .Net

Why BetterRobokassa?
--------------------

Native Robokassa documentation and code for Microsoft .Net are bit confusing. The goal of this library is to provide easy to use code with plenty of samples.

How to use
----------

Make sure you have correct keys set in your .config file (appSettings section):

    <add key="RobokassaLogin" value="your_merchant" />
    <add key="RobokassaPass1" value="password1" />
    <add key="RobokassaPass2" value="password2" />
    <add key="RobokassaMode" value="Test" /> <!-- Test or Production -->
    
RobokassaMode has two types:

 - `Test` - uses `http://test.robokassa.ru/Index.aspx` as base url
 - `Production` - uses `https://auth.robokassa.ru/Merchant/Index.aspx` as base url

Make sure your settings accessible:

    RobokassaConfig.AssertConfigurationIsValid();

Exception will be thrown if configuration is not valid. `AssertConfigurationIsValid` can be used in Global.asax.cs in ASP.NET applications:

    protected void Application_Start()
    {
        // ...
        
        RobokassaConfig.AssertConfigurationIsValid();
    }


Example 1
---------

Run sample console application (**Example1**) to generate redirect url:

    C:\Projects4\BetterRobokassa\Example1\bin\Debug\Example1.exe

Output (in case of test mode):

    http://test.robokassa.ru/Index.aspx?MrchLogin=your_merchant&OutSum=1000.00&InvId=1&Desc=desc&SignatureValue=00a09f4eab03374b536539a5ee57ea2a

Example 2: BetterRobokassa with ASP.NET MVC
-------------------------------------------

Output is result url you may want to use in your application to redirect your customer. Sample code for ASP.NET MVC controller:

    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            // ...
            // note: customerEmail is optional
            string redirectUrl = Robokassa.GetRedirectUrl(priceRub, orderId, customerEmail);
            return Redirect(redirectUrl);
        }
    }

Things you need to know
-----------------------

Keep in mind that there are two passwords exist for Robokassa:

 - First password is used in so called *result url* (personally, I call it confirmation url)
 - Second password is used in so called *success url*

What's the main difference between those two?

First one (*result* or *confirmation url*) is used by Robokassa robot. The robot uses the first password (`RobokassaPass1` in config file).

Second (*success url*) is used to redirect the customer back to your website. Confirmation hashcode is generated using the second password in this case (`RobokassaPass2` in config file).

Don't miss this point, and use correct `RobokassaQueryType` while verifying incoming requests. **Most of your bugs are probably happen if you're not using these types properly**.

Strange bug with pass1 and pass2
--------------------------------

Don't use uppercase letters in your password1 and password2. If you do, you'll get error telling your signature is malformed. Use only lowercase letters.

Confirm, Success, Fail url settings
-----------------------------------

![robokassa_settings](https://f.cloud.github.com/assets/1477672/2059844/19f571fa-8bdc-11e3-83f3-7bedd834ae3f.png)

  [1]: http://robokassa.ru
