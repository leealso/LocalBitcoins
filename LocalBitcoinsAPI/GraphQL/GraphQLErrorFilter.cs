using HotChocolate.Execution;

namespace LocalBitcoinsAPI.GraphQL;

public class GraphQLErrorFilter : IErrorFilter
{

    public GraphQLErrorFilter()
    {
    }

    public IError OnError(IError error)
    {
        if (error.Exception is QueryException exception)
            return error;
        else
        {
            return ErrorBuilder.FromError(error)
#if (DEBUG)
                .SetMessage(error.Message)
#else
                .SetMessage("Internal Server Error")
#endif
                .SetCode($"{StatusCodes.Status500InternalServerError}")
#if (DEBUG)
                .SetException(error.Exception)
#endif
                .Build();
        }
    }
}
