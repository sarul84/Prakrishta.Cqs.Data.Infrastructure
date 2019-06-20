//----------------------------------------------------------------------------------
// <copyright file="ICommandPreCondition.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for data validation</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Command
{
    using System.Threading.Tasks;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IAsyncCommandPreCondition{in TCommand}" /> interface
    /// </summary>
    /// <typeparam name="TCommand">The generic command type</typeparam>
    public interface IAsyncCommandPreCondition<in TCommand> where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that does data validation before committing data modification into data store
        /// </summary>
        /// <param name="command">The command that has to be validated</param>
        /// <returns>The awaitable task</returns>
        Task ValidateAsync(TCommand command);

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="ICommandPreCondition{in TCommand}" /> interface
    /// </summary>
    /// <typeparam name="TCommand">The generic command type</typeparam>
    public interface ICommandPreCondition<in TCommand> where TCommand : ICommandEntity
    {
        #region |Methods|

        /// <summary>
        /// The method that does data validation before committing data modification into data store
        /// </summary>
        /// <param name="command">The command that has to be validated</param>
        void Validate(TCommand command);

        #endregion
    }

    #endregion
}
