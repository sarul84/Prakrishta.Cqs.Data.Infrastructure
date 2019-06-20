//----------------------------------------------------------------------------------
// <copyright file="IConnectionProvider.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Connection Provider</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.DatabaseAccess
{
    using System.Data;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IConnectionProvider" /> interface
    /// </summary>
    public interface IConnectionProvider
    {
        #region |Methods|

        /// <summary>
        /// Closes a connection.
        /// </summary>
        /// <param name="databaseName">The database name.</param>
        /// <param name="mars">Whether or not to allow multiple result sets.</param>
        /// <returns>The connection object.</returns>
        IDbConnection GetClosedConnection(string databaseName = null, bool mars = false);

        /// Gets the connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        string GetConnectionString();

        /// <summary>
        /// Get the connection string from the config file with catalog name appended.
        /// </summary>
        /// <param name="databaseName">The database name.</param>
        /// <returns>connection string</returns>
        string GetConnectionStringWithCatalog(string databaseName);

        /// <summary>
        /// Opens a connection based on the given parameters.
        /// </summary>
        /// <param name="databaseName">The database name.</param>
        /// <param name="mars">Whether or not to allow multiple result sets.</param>
        /// <returns>The connection object.</returns>
        IDbConnection GetOpenConnection(string databaseName = null, bool mars = false);

        #endregion
    }

    #endregion
}
