using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TestCQRS.CqrsUtils
{
    /// <summary>
    /// Provides global configuration for CQRS utilities.
    /// </summary>
    public static class CqrsConfig
    {
        /// <summary>
        /// The service provider used for dependency injection.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; private set; }

        /// <summary>
        /// Configures the service provider. Must be called once during application startup.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use.</param>
        public static void Configure(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }

    /// <summary>
    /// Represents a void-like result for commands or queries that do not return a value.
    /// </summary>
    public readonly struct Unit
    {
        /// <summary>
        /// The single instance of Unit.
        /// </summary>
        public static readonly Unit Value = new Unit();

        public override string ToString() => "()";
    }

    /// <summary>
    /// Defines the base functionality for CQRS operations.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the operation.</typeparam>
    /// <typeparam name="TInput">The type of input passed to the operation.</typeparam>
    public abstract class CqrsBase<TResult, TInput>
    {
        /// <summary>
        /// Creates an instance of the specified CQRS type using dependency injection.
        /// </summary>
        /// <typeparam name="TSelf">The CQRS type to create.</typeparam>
        /// <returns>An initialized instance of the specified type.</returns>
        protected static TSelf Create<TSelf>() where TSelf : CqrsBase<TResult, TInput>
        {
            return ActivatorUtilities.CreateInstance<TSelf>(CqrsConfig.ServiceProvider!);
        }

        /// <summary>
        /// Executes the operation logic with the specified input.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>The result of the operation.</returns>
        public abstract Task<TResult> Handle(TInput input);
    }

    /// <summary>
    /// Represents a command that modifies application state and returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the command.</typeparam>
    /// <typeparam name="TInput">The type of input passed to the command.</typeparam>
    public interface ICommand<TResult, TInput>
    {
        /// <summary>
        /// Executes the command logic.
        /// </summary>
        /// <param name="input">The input data for the command.</param>
        /// <returns>The result of the command.</returns>
        Task<TResult> Handle(TInput input);
    }

    /// <summary>
    /// Represents a query that retrieves application state and returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
    /// <typeparam name="TInput">The type of input passed to the query.</typeparam>
    public interface IQuery<TResult, TInput>
    {
        /// <summary>
        /// Executes the query logic.
        /// </summary>
        /// <param name="input">The input data for the query.</param>
        /// <returns>The result of the query.</returns>
        Task<TResult> Handle(TInput input);
    }

    /// <summary>
    /// Provides a base class for implementing commands.
    /// </summary>
    /// <typeparam name="TSelf">The type of the command itself.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the command.</typeparam>
    /// <typeparam name="TInput">The type of input passed to the command.</typeparam>
    public abstract class CommandBase<TSelf, TResult, TInput> : CqrsBase<TResult, TInput>, ICommand<TResult, TInput>
        where TSelf : CommandBase<TSelf, TResult, TInput>
    {
        /// <summary>
        /// Creates an instance of the command using dependency injection.
        /// </summary>
        public static TSelf Create()
        {
            return Create<TSelf>();
        }
    }

    /// <summary>
    /// Provides a base class for implementing queries.
    /// </summary>
    /// <typeparam name="TSelf">The type of the query itself.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
    /// <typeparam name="TInput">The type of input passed to the query.</typeparam>
    public abstract class QueryBase<TSelf, TResult, TInput> : CqrsBase<TResult, TInput>, IQuery<TResult, TInput>
        where TSelf : QueryBase<TSelf, TResult, TInput>
    {
        /// <summary>
        /// Creates an instance of the query using dependency injection.
        /// </summary>
        public static TSelf Create()
        {
            return Create<TSelf>();
        }
    }
}
