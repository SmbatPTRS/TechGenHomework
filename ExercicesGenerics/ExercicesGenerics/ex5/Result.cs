using ExercicesGenerics.ex4;

namespace ExercicesGenerics.ex5;

public sealed class Result<T>
{
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public int Attempts { get; set; }

    public Result(T? value, bool isSuccess, string? errorMessage, int attempts)
    {
        Value = value;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage ?? "no error";
        Attempts = attempts;
    }

    public static Result<T> Execute<T>(Func<T> operation, int maxAttempts, Func<Exception, bool>? shouldRetry = null)
    {
        int attempts = 0;
        while (attempts++ < maxAttempts)
        {
            try
            {
                T value = operation.Invoke();
                return new Result<T>(value, true, null, maxAttempts);
            }
            catch (Exception ex)
            {
                if (shouldRetry == null)
                {
                    operation.Invoke();
                }

                if (shouldRetry(ex))
                {
                    operation.Invoke();
                }
            }
        }
        return new Result<T>(default(T), false, "could not finish successfully", attempts);
    }
}