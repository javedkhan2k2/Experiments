using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using ClampingDevice.Common.Results;

namespace ClampingDevice.Services;

public abstract class BaseService(ILogger logger)
{
    protected async Task<Result<T>> TryExecuteAsync<T>(Func<Task<Result<T>>> operation, string operationName) => await ExecuteWithHandlingAsync(operation, operationName);
    private async Task<Result<T>> ExecuteWithHandlingAsync<T>(Func<Task<Result<T>>> operation, string operationName)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex)
        {
            return HandleError<T>(ex, operationName);
        }
    }

    protected async Task<Result> TryExecuteAsync(Func<Task<Result>> operation, string operationName) => await ExecuteWithHandlingAsync(operation, operationName);
    private async Task<Result> ExecuteWithHandlingAsync(Func<Task<Result>> operation, string operationName)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex)
        {
            return HandleError(ex, operationName);
        }
    }

    protected Result<T> TryExecute<T>(Func<Result<T>> operation, string operationName) => ExecuteWithHandling(operation, operationName);
    private Result<T> ExecuteWithHandling<T>(Func<Result<T>> operation, string operationName)
    {
        try
        {
            return operation();
        }
        catch (Exception ex)
        {
            return HandleError<T>(ex, operationName);
        }
    }

    protected Result TryExecute(Func<Result> operation, string operationName) => ExecuteWithHandling(operation, operationName);

    private Result ExecuteWithHandling(Func<Result> operation, string operationName)
    {
        try
        {
            return operation();
        }
        catch (Exception ex)
        {
            return HandleError(ex, operationName);
        }
    }

    // Shared error handling logic
    private Result<T> HandleError<T>(Exception ex, string operationName)
    {
        logger.LogError(ex, "Error during {OperationName}", operationName);

        return ex switch
        {
            DbUpdateConcurrencyException => Result<T>.Failure(new Error("ConcurrencyError", $"A concurrency error occurred during {operationName}: {ex.Message}")),
            DbUpdateException => Result<T>.Failure(new Error("DatabaseUpdateError", $"A database update error occurred during {operationName}: {ex.Message}")),
            HttpRequestException httpEx when httpEx.StatusCode == HttpStatusCode.OK =>
                Result<T>.Failure(new Error("ParsingError", $"Failed to parse response during {operationName}. Please check the response format.")),
            HttpRequestException => Result<T>.Failure(new Error("NetworkError", $"A network error occurred during {operationName}: {ex.Message}")),
            _ => Result<T>.Failure(new Error("OperationFailed", $"An unexpected error occurred during {operationName}: {ex.Message}"))
        };
    }

    private Result HandleError(Exception ex, string operationName)
    {
        logger.LogError(ex, "Error during {OperationName}", operationName);

        return ex switch
        {
            DbUpdateConcurrencyException => Result.Failure(new Error("ConcurrencyError", $"A concurrency error occurred during {operationName}: {ex.Message}")),
            DbUpdateException => Result.Failure(new Error("DatabaseUpdateError", $"A database update error occurred during {operationName}: {ex.Message}")),
            HttpRequestException httpEx when httpEx.StatusCode == HttpStatusCode.OK =>
                Result.Failure(new Error("ParsingError", $"Failed to parse response during {operationName}. Please check the response format.")),
            HttpRequestException => Result.Failure(new Error("NetworkError", $"A network error occurred during {operationName}: {ex.Message}")),
            _ => Result.Failure(new Error("OperationFailed", $"An unexpected error occurred during {operationName}: {ex.Message}"))
        };
    }

    
    
}
