using HotChocolate.Execution;

namespace LocalBitcoinsAPI.Utilities;

public static class QueryExceptionUtility
{
    public static QueryException QueryException(string message = "Internal Server Error", int code = StatusCodes.Status500InternalServerError)
    {
        var error = ErrorBuilder.New()
            .SetMessage(message)
            .SetCode($"{code}")
            .SetException(new QueryException(message))
            .Build();

        return new QueryException(error);
    }

    public static QueryException NotFoundException(string message = "Not Found")
    {
        return QueryException(message, StatusCodes.Status404NotFound);
    }

    public static QueryException BadRequestException(string message = "Bad Request")
    {
        return QueryException(message, StatusCodes.Status400BadRequest);
    }
}
