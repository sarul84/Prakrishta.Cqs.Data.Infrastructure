//----------------------------------------------------------------------------------
// <copyright file="IQueryProcessor.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Query Processor that finds correct query
// handler to serve the request</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Query
{
    using System.Threading.Tasks;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IQueryProcessor" /> interface
    /// </summary>
    public interface IQueryProcessor
    {
        #region |Methods|

        /// <summary>
        /// The Process finds the correct query handler to serve the request
        /// </summary>
        /// <typeparam name="TResult">The generic return type</typeparam>
        /// <param name="query">The query object</param>
        /// <returns>The data that is returned by query handler</returns>
        TResult Process<TResult>(IQueryEntity<TResult> query);

        /// <summary>
        /// The Process finds the correct query handler to serve the request
        /// </summary>
        /// <typeparam name="TResult">The generic return type</typeparam>
        /// <param name="query">The query object</param>
        /// <returns>The data that is returned by query handler</returns>
        Task<TResult> ProcessAsync<TResult>(IQueryEntity<TResult> query);

        #endregion
    }

    #endregion
}
