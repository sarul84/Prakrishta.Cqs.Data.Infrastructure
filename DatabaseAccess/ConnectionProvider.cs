//----------------------------------------------------------------------------------
// <copyright file="ConnectionProvider.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The class that implements IConnectionProvider contract</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Cqs.Infrastructure.DatabaseAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="ConnectionProvider" /> class
    /// </summary>
    public sealed class ConnectionProvider : IConnectionProvider
    {
        #region |Private Fields|

        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string connectionString;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Deserialized representation of the config file.</param>
        public ConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region |Methods|

        /// <inheritdoc />
        public IDbConnection GetClosedConnection(string databaseName = null, bool mars = false)
        {
            var cs = new SqlConnectionStringBuilder(this.GetConnectionString())
            {
                InitialCatalog = databaseName.ToString(),
                MultipleActiveResultSets = mars
            };

            var connection = new SqlConnection(cs.ConnectionString);

            if (connection.State != ConnectionState.Closed)
            {
                throw new InvalidOperationException("Should be closed!");
            }

            return connection;
        }

        /// <summary>
        /// Get the connection string from the config file.
        /// </summary>
        /// <returns>Connection string.</returns>
        public string GetConnectionString()
        {
            return this.connectionString;
        }

        /// <inheritdoc />
        public string GetConnectionStringWithCatalog(string databaseName)
        {
            return $"{this.connectionString}Initial Catalog={databaseName};";
        }

        /// <inheritdoc />
        public IDbConnection GetOpenConnection(string databaseName = null, bool mars = false)
        {
            var cs = new SqlConnectionStringBuilder(this.GetConnectionString())
            {
                InitialCatalog = databaseName.ToString(),
                MultipleActiveResultSets = mars
            };

            var connection = new SqlConnection(cs.ConnectionString);

            connection.Open();

            return connection;
        }

        #endregion
    }
}
