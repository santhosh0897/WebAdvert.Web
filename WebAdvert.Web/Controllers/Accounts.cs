using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Schema;
using WebAdvert.Web.Models.Accounts;


namespace WebAdvert.Web.Controllers
{
    public class Accounts : Controller
    {

        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _UserManager;
        private readonly CognitoUserPool _pool;

        public Accounts(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _UserManager = userManager;
            _pool = pool;

        }


        public async Task<IActionResult> Signup()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> Signup(SignupModel signupModel)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(signupModel.Email);

                if(user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User in this email already exists");
                    return View(signupModel);
                }

                user.Attributes.Add(CognitoAttribute.Name.ToString(), signupModel.Email);

                var CreatedUser= await _UserManager.CreateAsync(user, signupModel.Password).ConfigureAwait(false);

               if (CreatedUser.Succeeded)
                {
                    return RedirectToAction("Confirm", "Accounts");
                }
            }


            return View(signupModel);
        }

        public async Task<IActionResult> Confirm()
        {
            var result = new ConfirmModel();

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmModel confirm)
        {

            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(confirm.Email);


                if (user== null)
                {
                    ModelState.AddModelError("Not Found", "User with given Email Id not found");

                    return View(confirm);
                }

                //user.Attributes.Add(CognitoAttribute.Name.ToString(), confirm.Email);



             //instead of Confirm EmailSync try using ConfirmSignUpAsync

                var result = await ((CognitoUserManager<CognitoUser>)_UserManager).ConfirmSignUpAsync(user, confirm.Code, true);



                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }

                    return View(confirm);
                }
            }
            return View(confirm);
            
        }

        public async Task<IActionResult> Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login_Post(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("Error Message", "User name and password doesnt match");

                    return RedirectToAction("Login", model);
                }
            }
            return View("Login",model);
        }

        
        

         
    }
}
