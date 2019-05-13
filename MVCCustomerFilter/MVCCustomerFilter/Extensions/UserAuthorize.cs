using MVCCustomerFilterDemo.Models;
using MVCCustomerFilterDemo.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCustomerFilterDemo.Extensions
{
    public class UserAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 授权失败时呈现的视图
        /// </summary>
        public string AuthorizationFailView { get; set; }

        /// <summary>
        /// 请求授权时执行
        /// </summary>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // 判断是否已经验证用户(即判定用户是否已经登录)
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // 如果没有验证则跳转到LogOn页面
                filterContext.HttpContext.Response.Redirect("/Account/LogOn");
            }

            //获得url请求里的controller和action：
            string strControllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string strActionName = filterContext.RouteData.Values["action"].ToString().ToLower();

            //根据请求过来的controller和action去查询可以被哪些角色操作:
            Models.RoleWithControllerAction roleWithControllerAction =
                SampleData.roleWithControllerAndAction.Find(r => r.ControllerName.ToLower() == strControllerName &&
                r.ActionName.ToLower() == strActionName);

            if (roleWithControllerAction != null)
            {
                //有权限操作当前控制器和Action的角色id
                this.Roles = roleWithControllerAction.RoleIds;
            }

            base.OnAuthorization(filterContext);
        }
        /// <summary>
        /// 自定义授权检查（返回False则授权失败）
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext.User.Identity.IsAuthenticated)
            {
                //当前登录用户的用户名
                string userName = httpContext.User.Identity.Name;
                //当前登录用户对象
                User user = SampleData.users.Find(u => u.UserName == userName);

                if (user != null)
                {
                    //当前登录用户的角色
                    Role role = SampleData.roles.Find(r => r.Id == user.RoleId);
                    foreach (string roleid in Roles.Split(','))
                    {
                        if (role.Id.ToString() == roleid)
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            else
            {
                //进入HandleUnauthorizedRequest
                return false;
            }

        }

        /// <summary>
        /// 处理授权失败的HTTP请求
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult { ViewName = AuthorizationFailView };
        }
    }
}