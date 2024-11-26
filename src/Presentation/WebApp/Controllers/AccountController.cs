using Identity.Application.User.Commands.LoginRegister;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.HttpManager;
using Identity.Application.User.Queries.LoginWithCode;
using Identity.Application.User.Commands.LoginWithCode;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using Identity.Application.User.Queries.LogOut;

namespace WebApp.Controllers;

public class AccountController(IMediator mediator) : SiteBaseController
{
    #region Register And Login

    [HttpGet("LoginRegister")]
    [RedirectHomeIfLoggedInActionFilter]
    public IActionResult LoginRegister(LoginRegisterDto model)
        => View(model);

    [HttpPost("LoginRegister"), ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginRegister(
    LoginRegisterDto data,
    CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var result = await mediator.Send(new LoginRegisterCommand(
                        data: data),
                        cancellationToken);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد .";
                TempData[InfoMessage] = $"پیامی  حاوی کد فعالسازی حساب کاربری به {data.Mobile} ارسال شد .";

                return RedirectToAction("LoginWithCode", new { Mobile = data.Mobile });
            }

            if (result.IsFailure)
                TempData[ErrorMessage] = result.Errors;
        }

        TempData[ErrorMessage] = "مقادیر وارد شده معتبر نمی باشد .";
        return View(data);
    }

    #endregion

    #region Login With Code

    [HttpGet("LoginWithCode/{Mobile}/{Resend?}")]
    public async Task<IActionResult> LoginWithCode(CancellationToken cancellationToken,
        string Mobile,
        bool Resend = false)
    {
        var res = await mediator.Send(new LoginWithCodeQuery(
            Mobile: Mobile,
            Resend: Resend),
            cancellationToken);

        if (res.IsFailure || res is null)
            return NotFound();

        ViewBag.Mobile = Mobile;
        return View();
    }

    [HttpPost("LoginWithCode/{Mobile}/{Resend?}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginWithCode(LoginByCodeDto model, CancellationToken cancellationToken)
    {
        #region Active User Mobile

        if (ModelState.IsValid)
        {
            var result = await mediator.Send(new LoginWithCodeCommand(
                data: model),
                cancellationToken);

            if (result.IsSuccess)
            {
                TempData[SuccessMessage] = $"خوش آمدید {result.Value.UserName}";

                return Redirect("/");
            }

            foreach (var error in result.Errors!)
            {
                TempData[ErrorMessage] = error;
            }
        }

        #endregion

        return View(model);
    }

    #endregion

    #region Log out

    [Authorize]
    [HttpGet("LogOut")]
    public async Task<IActionResult> LogOut()
    {
        var result = await mediator.Send(new LogOutQuery());
        if (result.IsSuccess)
        {
            TempData[SuccessMessage] = "خروج با موفقیت انجام شد.";
            return Redirect("/");
        }

        TempData[ErrorMessage] = "خروج ناموفق بوده است.";
        return Redirect("/");
    }

    #endregion
}
