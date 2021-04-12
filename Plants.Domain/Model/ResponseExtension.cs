using Plants.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using static Plants.Utility.Constants;

namespace Plants.Domain.Model
{
    public static class ResponseExtension
    {
        public static T ToSuccess<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.Success;
            response.responseMessage = ApiResponseMessages.Sucsses;
            response.HasError = false;
            return response;
        }
        public static T ToFailedAuthentication<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.AuthenticationError;
            response.responseMessage = ApiResponseMessages.AuthenticationError;
            response.HasError = true;
            return response;
        }
        public static T ToApiError<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.ApiError;
            response.responseMessage = ApiResponseMessages.ApiError;
            response.HasError = true;
            return response;
        }
        public static T ToValidationError<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.ValidationError;
            response.responseMessage = ApiResponseMessages.ValidationError;
            response.HasError = true;
            return response;
        }
        public static T ToNotFoundError<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.NotFoundError;
            response.responseMessage = ApiResponseMessages.NotFoundError;
            response.HasError = true;
            return response;
        }
        public static T ToToMuchWateringError<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.ToMuchWateringError;
            response.responseMessage = ApiResponseMessages.ToMuchWateringError;
            response.HasError = false;
            return response;
        }
        public static T ToCancelRequest<T>(this T response) where T : BaseResponse
        {
            response.responseCode = (int)ApiResponseCode.CancelRequest;
            response.responseMessage = ApiResponseMessages.CancelRequest;
            response.HasError = false;
            return response;
        }
    }
}
