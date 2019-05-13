using MVCCustomerFilterDemo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCustomerFilterDemo.Controllers
{
    public class AuthFiltersController : Controller
    {
        // GET: AuthFilters
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 使用自定义的授权验证，登录成功就可以访问
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [UserAuthorize(AuthorizationFailView = "Error")]
        public ActionResult Welcome()
        {
            return View();
        }

        [UserAuthorize(AuthorizationFailView = "Error")]
        public ActionResult AdminUser()
        {
            ViewBag.Message = "管理员页面";
            return View("Welcome");
        }

        /// <summary>
        /// 会员页面（管理员、会员都可访问）
        /// 在自定义过滤器里面要获取IsAuthenticated的值，所以上面要添加[Authorize]特性
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [UserAuthorize(AuthorizationFailView = "Error")]
        public ActionResult SeniorUser()
        {
            ViewBag.Message = "高级会员页面";
            return View("Welcome");
        }

        /// <summary>
        /// 游客页面（管理员、会员、游客都可访问）
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [UserAuthorize(AuthorizationFailView = "Error")]
        public ActionResult JuniorUser()
        {
            ViewBag.Message = "初级会员页面";
            return View("Welcome");
        }
    }
}