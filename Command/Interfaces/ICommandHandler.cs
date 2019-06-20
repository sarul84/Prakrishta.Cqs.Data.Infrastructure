//----------------------------------------------------------------------------------
// <copyright file="ICommandHandler.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Command Handler</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Command
{
    using System.Threading.Tasks;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IAsyncCommandHandler" /> interface
    /// </summary>
    public interface IAsyncCommandHandler
    {
    }

    /// <summary>
    /// Defines the <see cref="IAsyncCommandHandler{in TCommand}" /> interface
    /// </summary>
    /// <typeparam name="TCommand">The generic command type</typeparam>
    public interface IAsyncCommandHandler<in TCommand> : IAsyncCommandHandler
        where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that handles data modification operation
        /// </summary>
        /// <param name="command">The command that has data modification details</param>
        /// <returns>The awaitable task</returns>
        Task HandleAsync(TCommand command);

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="IAsyncCommandHandler{in TCommand, TReturn}" /> interface
    /// </summary>
    /// <typeparam name="TCommand">The generic command type</typeparam>
    /// <typeparam name="TReturn">The genetic type of return data</typeparam>
    public interface IAsyncCommandHandler<in TCommand, TReturn> : IAsyncCommandHandler
        where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that handles data modification operation
        /// </summary>
        /// <param name="command">The command that has data modification details</param>
        /// <returns>The awaitable task</returns>
        Task<TReturn> HandleAsync(TCommand command);

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="ICommandHandler" /> interface
    /// </summary>
    public interface ICommandHandler
    {
    }

    /// <summary>
    /// Defines the <see cref="ICommandHandler{in TCommand}" /> interface
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that handles data modification operation
        /// </summary>
        /// <param name="command">The command that has data modification details</param>
        void Handle(TCommand command);

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="ICommandHandler{in TCommand, out TReturn}" />
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public interface ICommandHandler<in TCommand, out TReturn> : ICommandHandler
        where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that handles data modification operation
        /// </summary>
        /// <param name="command">The command that has data modification details</param>
        /// <returns>The data to be returned after execution</returns>
        TReturn Handle(TCommand command);

        #endregion
    }

    #endregion
}
