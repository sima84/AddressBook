using AddressBook.Api.Infrastructure;
using AddressBook.Business.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Api.Middleware.ExceptionToHttpResponseMapper
{
    public class ExceptionToHttpResponseParametersMapper : IExceptionToHttpResponseParametersMapper
    {
        public bool TryGetHttpStatusCodeForException(Exception exception, out HttpResponseParameters parameters)
        {
            if (exception is ArgumentOutOfRangeException)
            {
                parameters = new HttpResponseParameters
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ContentType = HttpResponseContentTypes.TextPlain,
                    Content = exception.Message
                };
                return true;
            }
            else if (exception is ArgumentException)
            {
                parameters = new HttpResponseParameters
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ContentType = HttpResponseContentTypes.TextPlain,
                    Content = exception.Message
                };
                return true;
            }
            else if (exception is BusinessException)
            {
                parameters = GetResponseParametersFromBusinessException((BusinessException)exception);
                return true;
            }
            else
            {
                parameters = null;
                return false;
            }
        }

        private HttpResponseParameters GetResponseParametersFromBusinessException(BusinessException exception)
        {
            var result = new HttpResponseParameters
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            if (exception.Errors == null || exception.Errors.Count == 0)
            {
                result.ContentType = HttpResponseContentTypes.TextPlain;
                result.Content = exception.Message.ToString();
            }
            else
            {
                result.ContentType = HttpResponseContentTypes.ApplicationJson;
                result.Content = CreateJsonFromBusinessException(exception).ToString();
            }

            return result;
        }

        private JObject CreateJsonFromBusinessException(BusinessException exception)
        {
            var exceptionObject =
                new JObject(
                    exception.Errors.Keys
                        .Select(key => new JProperty(key,
                            new JArray(
                                exception.Errors[key].Select(err =>
                                    new JValue(err)
                                )
                            )
                        ))
                );

            return exceptionObject;
        }
    }
}
