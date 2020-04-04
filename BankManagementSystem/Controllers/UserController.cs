using BankManagementData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly Context SContext;

        public UserController(Context context)
        {
            SContext = context;
        }

        [Route("CreateUser")]
        [HttpPost]
        public IActionResult CreateUser(ParentModel parent)
        {
            var user = new User();
            user = parent.User;

            var pass = new Passwords();

            PasswordHasher passhash = new PasswordHasher();

            SContext.Add(user);
            SContext.SaveChanges();

            pass.client_id = parent.User.client_id;
            pass.password = passhash.HashPassword(parent.Passwords.password);

            SContext.Add(pass);
            SContext.SaveChanges();

            return View("Views/Home/Index.cshtml");
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Passwords pass)
        {
            if (ModelState.IsValid)
            {
                Passwords passObj = SContext.Passwords.Find(pass.client_id);
                PasswordHasher passhash = new PasswordHasher();

                var result = passhash.VerifyHashedPassword(passObj.password, pass.password);

                if (result == PasswordVerificationResult.Success)
                {
                    User user = UserInfo(pass.client_id);

                    HttpCookie userIdCookie = CreateUserIdCookie(user);
                    HttpCookie userNameCookie = CreateUserNameCookie(user);

                    IEnumerable<Account> accounts = AccountsList(pass.client_id);

                    Response.Cookies.Append("UserIdCookie", userIdCookie.Value);
                    Response.Cookies.Append("UserNameCookie", userNameCookie.Value);
                    return View("Views/Home/BankAccountOverview.cshtml", accounts);
                }
                else
                {
                    return View("Views/Home/Login.cshtml");
                }
            }
            else
            {
                return View("Views/Home/Index.cshtml");
            }
        }

        [Route("BankAccountOverview")]
        [HttpGet]
        public IActionResult BankAccountOverview()
        {
            return View("Views/Home/BankAccountOverview.cshtml", AccountsList(CookieClientId()));
        }

        [Route("PersonalInformation")]
        [HttpGet]
        public IActionResult PersonalInformation()
        {
            return View("Views/Home/PersonalInformation.cshtml", UserInfo(CookieClientId()));
        }

        [Route("AccountDetails")]
        [HttpGet]
        public IActionResult AccountDetails(int accountId)
        {
            IEnumerable<TransactionInfo> transactionsList = SContext.TransactionInfo.Where(
                prop => prop.account_id == accountId);
            transactionsList = transactionsList.OrderByDescending(prop => prop.date.Date);

            return View("Views/Home/AccountDetails.cshtml", transactionsList);
        }

        [Route("EditPersonalInfo")]
        public IActionResult EditPersonalInfo()
        {
            User user = UserInfo(CookieClientId());

            if (user != null)
            {
                user.email = Request.Query["email"];
                user.phone_number = Request.Query["phone_number"];
                user.street_address = Request.Query["street_address"];
                user.city = Request.Query["city"];
                user.province = Request.Query["province"];
                user.country = Request.Query["country"];

                SContext.Update(user);
                SContext.SaveChanges();
            }

            return View("Views/Home/BankAccountOverview.cshtml", AccountsList(CookieClientId()));
        }

        public IActionResult TransferMoney(IFormCollection formCollection)
        {
            int fromAccountId = Int32.Parse(formCollection["fromAccount"]);
            int toAccountId = Int32.Parse(formCollection["toAccount"]);
            float transferAmount = float.Parse(formCollection["transferAmount"]);

            if (fromAccountId != toAccountId)
            {
                Account fromAccount = AccountInfo(fromAccountId);
                Account toAccount = AccountInfo(toAccountId);


                if (fromAccount.amount < transferAmount || fromAccount.amount <= 0)
                {
                    return RedirectToAction("BankAccountOverview");
                }

                fromAccount.amount -= transferAmount;
                toAccount.amount += transferAmount;

                TransactionInfo fromAccountInfo = TransactionDetailCreater(
                    fromAccount.account_id, transferAmount, fromAccount.amount, true);
                SContext.Add(fromAccountInfo);
                SContext.SaveChanges();

                TransactionInfo toAccountInfo = TransactionDetailCreater(
                    toAccount.account_id, transferAmount, toAccount.amount, false);
                toAccountInfo.transaction_id = fromAccountInfo.transaction_id;
                SContext.Add(toAccountInfo);

                SContext.Update(fromAccount);
                SContext.Update(toAccount);
                SContext.SaveChanges();

                return View("/Views/Home/TransferMoneyConfirmation.cshtml", fromAccountInfo);
            }

            return View("/Views/Home/BankAccountOverview.cshtml");
        }

        private TransactionInfo TransactionDetailCreater(int accountId, float transferAmount, float amountAfter, Boolean fromAccount)
        {
            string transferName = "Deposit";
            if(fromAccount == true)
            {
                transferAmount *= -1;
                transferName = "Withdrawl";
            }

            TransactionInfo transactionItem = new TransactionInfo
            {
                account_id = accountId,
                amount_changed = transferAmount,
                amount_after = amountAfter,
                transaction_name = transferName
            };

            return transactionItem;
        }

        private int CookieClientId()
        {
            String cookieString = HttpContext.Request.Cookies["UserIdCookie"];
            int clientId = Int32.Parse(cookieString);
            return clientId;
        }

        private List<Account> AccountsList(int clientId)
        {
            return SContext.Account.Where(acc => acc.client_id == clientId).ToList();
        }

        private Account AccountInfo(int accountId)
        {
            return SContext.Account.Find(accountId);
        }

        private User UserInfo(int clientId)
        {
            return SContext.User.Find(clientId);
        }

        private HttpCookie CreateUserIdCookie(User user)
        {
            HttpCookie userCookies = new HttpCookie
            {
                Value = String.Concat(user.client_id)
            };

            return userCookies;
        }

        private HttpCookie CreateUserNameCookie(User user)
        {
            HttpCookie userCookies = new HttpCookie
            {
                Value = String.Concat(user.first_name + " " + user.last_name)
            };

            return userCookies;
        }
    }
}