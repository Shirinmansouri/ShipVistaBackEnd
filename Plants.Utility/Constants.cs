using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Utility
{
    public static class Constants
    {
        public enum  ApiResponseCode
        {
            Success = 0,
            ApiError = -1,
            ValidationError = 2,
            AuthenticationError = 3,
            NotFoundError=4,
            ToMuchWateringError=5,
            CancelRequest=6


        }
       
    }
    public static class ApiResponseMessages
    {
        public static string Sucsses { get { return "Sucsses Operation"; } }
        public static string ValidationError { get { return "ValidationError"; } }
        public static string AuthenticationError { get { return "AuthenticationError"; } }
        public static string ApiError { get { return "Internal ApiError"; } }
        public static string NotFoundError { get { return "NotFoundError"; } }
        public static string ToMuchWateringError { get { return "ToMuchWateringError"; } }
        public static string CancelRequest { get { return "CancelRequest"; } }
    }
}
