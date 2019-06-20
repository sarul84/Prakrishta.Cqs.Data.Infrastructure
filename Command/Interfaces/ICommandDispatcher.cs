//----------------------------------------------------------------------------------
// <copyright file="ICommandDispatcher.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Command Dispatcher</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Command
{
    using System.Threading.Tasks;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="ICommandDispatcher" /> interface
    /// </summary>
    public interface ICommandDispatcher
    {
        #region |Methods|

        /// <summary>
        /// The method that executes data modification command synchornously
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommandEntity;

        /// <summary>
        /// The method that executes data modification command asynchornously
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        /// <returns>The awaitable task</returns>
        Task DispatchCommandAsync<TCommand>(TCommand command) where TCommand : ICommandEntity;

        #endregion
    }

    #endregion
}
