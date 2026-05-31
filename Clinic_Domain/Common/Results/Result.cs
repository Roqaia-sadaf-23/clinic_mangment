 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clinic_Domain.Common.Results;

public static class Result
{
    public static Success Success => default;

    public static Created Created => default;
    public static Deleted deleted => default;

    public static Updated Updated => default;
    //public static Result<T> Success<T>(T value)
    //{
    //    return new Result<T>(value, true, null);
    //}
}


public sealed class Result<TValue> : IResult<TValue>
{
    private readonly TValue? _value=default;
    private readonly List<Error>? _errors = null;

    private Result(List <Error> errors)
    {
        if (errors is null || errors.Count == 0)
        {
         throw new ArgumentException("cannot creatre an errorOr<TValue> froman empty collection of errors. provide at least one error. ", nameof(errors));
        };
        _errors = errors;
        IsSuccess = false;
    }


    private Result(TValue Value)
    {
        if (Value is null )
        {
   throw new ArgumentException(nameof(Value));
        }
        _value = Value;
        IsSuccess = true;
    }

    private Result(Error errors)
    {
        _errors = [errors];
    }
    [JsonConstructor]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For serialization only.", true)]
    public Result(TValue Value, List<Error>? Errors, bool issuccess)
    {
        if (issuccess)
        {
            _value = Value ?? throw new ArgumentException(nameof(Value));
            _errors = [];

            IsSuccess = true;
        }
        else
        {
            if (Errors is null || Errors.Count == 0)
            {
                throw new ArgumentException("Prvide at least one error ", nameof(Errors));
            }
            _errors = Errors;
            _value = default;
            IsSuccess = false;
        }
    }
        
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;

    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
        => IsSuccess ? onValue(Value!) : onError(Errors!);

    public static implicit operator Result<TValue>(TValue value)=>new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);

    public List<Error> Errors => IsError ? _errors !: [];

    public TValue Value => IsSuccess ? _value! : default;

    public Error TopError => (_errors?.Count > 0) ? _errors[0] : default;

}
public readonly record struct Success;

public readonly record struct Created;

public readonly record struct Deleted;

public readonly record struct Updated;
