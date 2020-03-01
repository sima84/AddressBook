using System;

namespace AddressBook.Api.Middleware.ExceptionToHttpResponseMapper
{
    public interface IExceptionToHttpResponseParametersMapper
    {
        bool TryGetHttpStatusCodeForException(Exception e, out HttpResponseParameters statusCode);
    }
}
