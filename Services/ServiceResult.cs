﻿using System.Net;

namespace App.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }

        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0; 
        public bool IsFailure => !IsSuccess;
        public HttpStatusCode Status{ get; set; }

        //static factory methods to manage the creation of ServiceResult objects (avoiding the need to use the new keyword)
        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = status
            };
        }

        public static ServiceResult<T> Failure(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = errorMessage,
                Status = status

            };
        }

        public static ServiceResult<T> Failure(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = [errorMessage],
                Status = status

            }; 
        }
    } 
    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }

        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0; 
        public bool IsFailure => !IsSuccess;
        public HttpStatusCode Status{ get; set; }

        //static factory methods to manage the creation of ServiceResult objects (avoiding the need to use the new keyword)
        public static ServiceResult Success( HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
            
                Status = status
            };
        }

        public static ServiceResult Failure(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = errorMessage,
                Status = status

            };
        }

        public static ServiceResult Failure(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = [errorMessage],
                Status = status

            }; 
        }
    }
}
