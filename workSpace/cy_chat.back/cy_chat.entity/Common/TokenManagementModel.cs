using System;
using System.Collections.Generic;
using System.Text;

namespace cy_chat.entity.Common
{
    /// <summary>
    /// POCO类，用来存储签发或者验证jwt时用到的信息
    /// </summary>
    public class TokenManagementModel
    {
        public static string Secret = "987654321987654321";//私钥

        public static string Issuer = "webapi.cn";

        public static string Audience = "WebApi";

        public static int AccessExpiration = 180;//过期时间

        public static int RefreshExpiration = 180;//刷新时间
    }
}
