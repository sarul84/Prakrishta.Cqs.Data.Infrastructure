// -------------------------------------------------------------------------------
// <copyright file="QueryProcessor.cs" company="Mr. Cooper">
// Copyright © 2019 All Right Reserved
// </copyright>
// <Author>Arul Sengottaiyan</Author>
// <date>6/19/2019</date>
// <summary>The Query Processor that implements query processor contract</summary>
// --------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Query
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the methods and properties for Query processor
    /// </summary>
    public sealed class QueryProcessor : IQueryProcessor
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger<QueryProcessor> logger;

        /// <summary>
        /// Defines the serviceProvider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        #endregion

        #region |Constructor|

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryProcessor"/> class.
        /// </summary>
        /// <param name="logger">The logger object</param>
        /// <param name="serviceProvider">The serviceProvider object</param>
        public QueryProcessor(ILogger<QueryProcessor> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        #endregion

        #region |Methods|

        /// <inheritdoc />
        [DebuggerStepThrough]
        public TResult Process<TResult>(IQueryEntity<TResult> query)
        {
            this.logger.LogDebug($"Processing query {query}");

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = this.serviceProvider.GetService(handlerType);

            var queryResult = handler?.Execute((dynamic)query);

            return queryResult;
        }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public async Task<TResult> ProcessAsync<TResult>(IQueryEntity<TResult> query)
        {
            this.logger.LogDebug($"Async Processing query {query}");

            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = this.serviceProvider.GetService(handlerType);

            var queryResult = await handler?.ExecuteAsync((dynamic)query).ConfigureAwait(false);

            return queryResult;
        }

        #endregion
    }
}
