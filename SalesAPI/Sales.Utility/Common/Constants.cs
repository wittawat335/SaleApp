using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utility.Common
{
    public static class Constants
    {
        public struct AppSettings
        {
            //ConnectionString
            public const string ConnectionStringSqlServer = "SqlServer";
            //JWT
            public const string JWT_Secret = "AppSetting:JWT:Secret";
            public const string JWT_ExpireTime = "AppSetting:JWT:ExpireTime";
            public const string JWT_TokenDescriptor_Issuer = "AppSetting:JWT:TokenDescriptor:Issuer";
            public const string JWT_TokenDescriptor_Audience = "AppSetting:JWT:TokenDescriptor:Audience";
            //DateFormat
            public const string DateFormat = "AppSetting:DateFormat";
            //Port Url
            public const string Client_URL = "AppSetting:Client_URL";
        }
        public struct StatusMessage
        {
            public const string Success = "OK";
            public const string No_Data = "No Data";
            public const string Could_Not_Create = "Could not create";
            public const string No_Delete = "No Deleted";
            public const string Cannot_Update_Data = "Cannot Update Data";
            public const string Cannot_Map_Data = "Cannot Map Data";
            public const string Create_Action = "Successfully Created";
            public const string Update_Action = "Successfully Updated";
            public const string Delete_Action = "Successfully Deleted";
        }
        public struct Status
        {
            public const bool True = true;
            public const bool False = false;
        }
        public struct DateTimeFormat
        {
            public const string ddMMyyyy = "dd/MM/yyyy";
        }
        public struct CultureInfoFormat
        {
            public const string en_US = "en-US";
        }
    }
}
