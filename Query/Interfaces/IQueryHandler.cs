//----------------------------------------------------------------------------------
// <copyright file="IQueryHandler.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Query Handler</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Query
{
    /// <summary>
    /// Defines the <see cref="IQueryHandler{in TQuery, out TResult}" /> interface
    /// </summary>
    /// <typeparam name="TQuery">The generic query type</typeparam>
    /// <typeparam name="TResult">The generic result type</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQueryEntity<TResult>
    {
        #region |Methods|

        /// <summary>
        /// The method that handles query execution on database context and returns result
        /// </summary>
        /// <param name="query">The query that has to be executed against the data context</param>
        /// <returns>The data from data store for the given query</returns>
        TResult Execute(TQuery query);

        #endregion
    }
}
