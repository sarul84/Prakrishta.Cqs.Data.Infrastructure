//----------------------------------------------------------------------------------
// <copyright file="CommandDispatcher.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The Command Dispatcher class that helps to identify correct handler 
// ,resolve and execute change operation</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.Command
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CommandDispatcher" /> class
    /// </summary>
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger<CommandDispatcher> logger;

        /// <summary>
        /// Defines the serviceProvider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        /// <param name="logger">The logger object</param>
        /// <param name="serviceProvider">The serviceProvider object</param>
        public CommandDispatcher(ILogger<CommandDispatcher> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// The method that executes data modification command synchornously
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        [DebuggerStepThrough]
        public void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommandEntity
        {
            this.logger.LogDebug("Dispatching command {0}", command);

            CheckAllPreconditions(command);

            var handlers = this.serviceProvider.GetServices<ICommandHandler<TCommand>>();

            foreach (var handler in handlers)
            {
                handler.Handle(command);
            }
        }

        /// <summary>
        /// The method that executes data modification command asynchornously
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        /// <returns>The awaitable task</returns>
        [DebuggerStepThrough]
        public async Task DispatchCommandAsync<TCommand>(TCommand command) where TCommand : ICommandEntity
        {
            this.logger.LogDebug("Dispatching command {0}", command);

            await CheckAllPreconditionsAsync(command);

            var handlers = this.serviceProvider.GetServices<IAsyncCommandHandler<TCommand>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(command).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// The method to do data validation before executing data modification commands
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        private void CheckAllPreconditions<TCommand>(TCommand command) where TCommand : ICommandEntity
        {
            var results = new Collection<Exception>();
            foreach (var condition in this.serviceProvider.GetServices<ICommandPreCondition<TCommand>>())
            {
                try
                {
                    condition.Validate(command);
                }
                catch (Exception ex)
                {
                    results.Add(ex);
                }
            }

            if (results.Any())
            {
                throw new AggregateException(results);
            }
        }

        /// <summary>
        /// The method to do data validation before executing data modification commands
        /// </summary>
        /// <typeparam name="TCommand">The generic command type</typeparam>
        /// <param name="command">The command type object that has data modification details</param>
        /// <returns>The awaitable task</returns>
        private async Task CheckAllPreconditionsAsync<TCommand>(TCommand command) where TCommand : ICommandEntity
        {
            var results = new Collection<Exception>();
            foreach (var condition in this.serviceProvider.GetServices<IAsyncCommandPreCondition<TCommand>>())
            {
                try
                {
                    await condition.ValidateAsync(command);
                }
                catch (Exception ex)
                {
                    results.Add(ex);
                }
            }

            if (results.Any())
            {
                throw new AggregateException(results);
            }
        }

        #endregion
    }
}
