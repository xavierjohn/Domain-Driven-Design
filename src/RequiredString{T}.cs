using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CWiz.RailwayOrientedProgramming;

namespace CWiz.DomainDrivenDesign
{
    public abstract class RequiredString<T> : ValueObject<RequiredString<T>> where T : RequiredString<T>
    {
        private static readonly Lazy<Func<string, T>> CreateInstance = new Lazy<Func<string, T>>(CreateInstanceFunc);
        private static readonly Result.Error cannotBeEmptyError
            = new Result.Error($"{typeof(T).Name} cannot be empty", $"{typeof(T).Name}");

        protected RequiredString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<T> Create(Maybe<string> requiredStringOrNothing)
        {
            return requiredStringOrNothing
                .EnsureNotNullOrWhiteSpace(cannotBeEmptyError)
                .Map(name => CreateInstance.Value(name));
        }

        private static Func<string, T> CreateInstanceFunc()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var ctor = typeof(T).GetTypeInfo().GetConstructors(flags).Single(
                ctors =>
                {
                    var parameters = ctors.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
                });
            var value = Expression.Parameter(typeof(string), "value");
            var body = Expression.New(ctor, value);
            var lambda = Expression.Lambda<Func<string, T>>(body, value);

            return lambda.Compile();
        }
    }
}