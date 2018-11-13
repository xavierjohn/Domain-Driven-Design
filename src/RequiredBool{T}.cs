using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CWiz.RailwayOrientedProgramming;

namespace CWiz.DomainDrivenDesign
{
    public abstract class RequiredBool<T> : ValueObject<RequiredBool<T>> where T : RequiredBool<T>
    {
        private static readonly Lazy<Func<bool, T>> CreateInstance = new Lazy<Func<bool, T>>(CreateInstanceFunc);
        private static readonly Result.Error cannotBeEmptyError
            = new Result.Error($"{typeof(T).Name} has to be 'true' or 'false'", $"{typeof(T).Name}");

        protected RequiredBool(bool value)
        {
            Value = value;
        }

        public bool Value { get; }

        public static Result<T> Create(bool value)
        {
            return Result.Ok(CreateInstance.Value(value));
        }

        private static Result<bool> ConvertStringToBool(string input)
        {
            if (bool.TryParse(input, out var value))
                return Result<bool>.Ok(value);
            return Result.Fail<bool>(cannotBeEmptyError);
        }
        public static Result<T> Create(Maybe<string> requiredStringOrNothing)
        {
            return requiredStringOrNothing
                .ToResult(cannotBeEmptyError)
                .OnSuccess(ConvertStringToBool)
                .Map(value => CreateInstance.Value(value));
        }

        private static Func<bool, T> CreateInstanceFunc()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var ctor = typeof(T).GetTypeInfo().GetConstructors(flags).Single(
                ctors =>
                {
                    var parameters = ctors.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == typeof(bool);
                });
            var value = Expression.Parameter(typeof(bool), "value");
            var body = Expression.New(ctor, value);
            var lambda = Expression.Lambda<Func<bool, T>>(body, value);

            return lambda.Compile();
        }
    }
}
