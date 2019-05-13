using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCCustomerFilterDemo.Models
{
    // <summary>
    /// 用户登录类
    /// </summary>
    public class LogOnViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        [DisplayName("记住我")]
        public bool RememberMe { get; set; }

    }
}