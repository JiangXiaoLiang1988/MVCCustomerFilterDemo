using MVCCustomerFilterDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCCustomerFilterDemo.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 显示登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOn()
        {
            LogOnViewModel model = new LogOnViewModel();
            return View(model);

        }

        /// <summary>
        /// 处理用户点击登录提交回发的表单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model)
        {
            //只要输入的用户名和密码一样就过
            if (model.UserName.Trim() == model.Password.Trim())
            {
                // 判断是否勾选了记住我
                if (model.RememberMe)
                {
                    //2880分钟有效期的cookie
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                }
                else
                {
                    //会话cookie
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                }
                // 跳转到AuthFilters控制器的Welcome方法
                return RedirectToAction("Welcome", "AuthFilters");
            }
            else
            {
                return View(model);
            }

        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }
    }
}