using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;

namespace LocalBitcoinsAPI.GraphQL;

public class GraphQLDiagnosticEventListener : ExecutionDiagnosticEventListener
{
    private readonly ILogger<GraphQLDiagnosticEventListener> _logger;

    public GraphQLDiagnosticEventListener(ILogger<GraphQLDiagnosticEventListener> logger)
    {
        _logger = logger;
    }

    public override void RequestError(IRequestContext context, Exception exception)
    {
        LogException(exception);
        base.RequestError(context, exception);
    }

    public override void ResolverError(IMiddlewareContext context, IError error)
    {
        LogError(error);
        base.ResolverError(context, error);
    }

    public override void SubscriptionEventError(SubscriptionEventContext context, Exception exception)
    {
        LogException(exception);
        base.SubscriptionEventError(context, exception);
    }

    public override void SubscriptionTransportError(ISubscription subscription, Exception exception)
    {
        LogException(exception);
        base.SubscriptionTransportError(subscription, exception);
    }

    public override void TaskError(IExecutionTask task, IError error)
    {
        LogError(error);
        base.TaskError(task, error);
    }

    private void LogException(Exception exception)
    {
        if (exception is QueryException queryException)
            _logger.LogWarning(queryException, queryException.Message);
        else
            _logger.LogError(exception, exception.Message);
    }

    private void LogError(IError error) 
    {
        if (error.Exception != null)
            LogException(error.Exception);
    }
}
