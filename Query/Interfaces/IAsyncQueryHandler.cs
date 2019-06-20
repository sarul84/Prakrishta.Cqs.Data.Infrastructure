//----------------------------------------------------------------------------------
// <copyright file="IAsyncQueryHandler.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Query Handler</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Query
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IAsyncQueryHandler{in TQuery, TResult}" /> interface
    /// </summary>
    /// <typeparam name="TQuery">The generic query type</typeparam>
    /// <typeparam name="TResult">The generic result type</typeparam>
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQueryEntity<TResult>
    {
        #region |Methods|

        /// <summary>
        /// The ExecuteAsync
        /// </summary>
        /// <param name="query">The query object</param>
        /// <returns>The awaitable task</returns>
        Task<TResult> ExecuteAsync(TQuery query);

        #endregion
    }
}
