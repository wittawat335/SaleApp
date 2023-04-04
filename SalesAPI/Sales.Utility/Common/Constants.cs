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
            //JWT
            public const string JWT_Key = "AppSettings:JWT:key";
            public const string JWT_ExpireTime = "AppSettings:JWT:ExpireTime";
            public const string JWT_TokenDescriptor_Issuer = "AppSettings:JWT:TokenDescriptor:Issuer";
            public const string JWT_TokenDescriptor_Audience = "AppSettings:JWT:TokenDescriptor:Audience";
            //DateFormat
            public const string DateFormat = "AppSettings:DateFormat";
            //Port Url
            public const string Client_URL = "AppSettings:Client_URL";
        }

        public struct ConnnectionString
        {
            public const string SqlServer = "SqlServer";
            public const string SqlServerToAzure = "SqlServerToAzure";
        }
        public struct StatusMessage
        {
            public const string Success = "OK";
            public const string No_Data = "No Data";
            public const string Could_Not_Create = "Could not create";
            public const string No_Delete = "No Deleted";
            public const string Duplicate_User = "User is Duplicate";
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
