using System.Diagnostics.CodeAnalysis;

namespace CurrencyConverter.Model;


// wyjątki zamiast errors as values dla logiki? który my mamy rok
public class Result<T>
{
    // prosta adnotacja, zawsze przed sprawdzeniem value, sprawdzany jest IsSuccess
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    public T? Value { get; }
    public string? Error { get; }

    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
