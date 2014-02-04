﻿using RomanPushkin.BetterRobokassa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example2.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            int priceRub = 1000;
            int orderId = 1;

            // note: use GetRedirectUrl overloading to specify customer email

            string redirectUrl = Robokassa.GetRedirectUrl(priceRub, orderId);

            return Redirect(redirectUrl);
        }

        // So called "Result Url" in terms of Robokassa documentation.
        // This url is called by Robokassa robot.

        public ActionResult Confirm(RobokassaConfirmationRequest confirmationRequest)
        {
            try
            {
                if (confirmationRequest.IsQueryValid(RobokassaQueryType.ResultURL))
                {
                    processOrder(confirmationRequest);

                    return Content("OK"); // content for robot
                }
            }
            catch (Exception) { }
            return Content("ERR");
        }

        // So called "Success Url" in terms of Robokassa documentation.
        // Customer is redirected to this url after successful payment. 

        public ActionResult Success(RobokassaConfirmationRequest confirmationRequest)
        {
            try
            {

                if (confirmationRequest.IsQueryValid(RobokassaQueryType.SuccessURL))
                {
                    processOrder(confirmationRequest);

                    return View(); // content for user
                }
            }
            catch (Exception) { }

            return View("Fail");
        }

        // So called "Fail Url" in terms of Robokassa documentation.
        // Customer is redirected to this url after unsuccessful payment.

        public ActionResult Fail()
        {
            return View();
        }

        private void processOrder(RobokassaConfirmationRequest confirmationRequest)
        {
            // TODO:
            // 1. verify your order Id and price here
            // 2. mark your order as paid
        }
    }
}
