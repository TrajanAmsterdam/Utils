﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartomatic.Utils.Data;
using NUnit.Framework;
using FluentAssertions;

namespace Data.Tests
{
    [TestFixture]
    public class DataSourcerTests
    {
        [Test]
        public void DefaultDbName_WhenDataSourceIsPostgres_ShouldBePostgres()
        {
            var dsc = new DataSourceCredentials()
            {
                DataSourceType = DataSourceType.PgSql
            };

            dsc.GetDefaultDbName().Should().Be("postgres");
        }

        [Test]
        public void ConnectionString_WhenDataSourceIsPgSqlAndDbNameIsNotSpecified_ShouldUseDefaultDbName()
        {
            var dsc = GetDsc() as DataSourceCredentials;
            dsc.DataSourceType = DataSourceType.PgSql;
            dsc.DbName = null;

            dsc.GetConnectionString().Should().Be("Server=ServerHost;Port=666;Database=" + dsc.GetDefaultDbName() + ";user id=TestUser;password=TestPass;");
        }

        [Test]
        public void ConnectionString_WhenDataSourceIsPgSqlAndDbNameIsSpecified_ShouldBeValid()
        {
            var dsc = GetDsc();
            dsc.DataSourceType = DataSourceType.PgSql;

            dsc.GetConnectionString().Should().Be("Server=ServerHost;Port=666;Database=DbName;user id=TestUser;password=TestPass;");
        }

        

        [Test]
        public void ConnectionString_WhenDataSourceIsSqlServerAndPortIsNotSpecified_ShouldBeValid()
        {
            var dsc = GetDsc();
            dsc.DataSourceType = DataSourceType.SqlServer;
            dsc.ServerPort = null;

            dsc.GetConnectionString().Should().Be("server=ServerHost;user id=TestUser;password=TestPass;database=DbName;");
        }

        [Test]
        public void ConnectionString_WhenDataSourceIsSqlServerAndPorIsSpecified_ShouldBeValid()
        {
            var dsc = GetDsc();
            dsc.DataSourceType = DataSourceType.SqlServer;

            dsc.GetConnectionString().Should().Be("server=ServerHost,666;user id=TestUser;password=TestPass;database=DbName;");
        }

      
        private IDataSourceCredentials GetDsc()
        {
            return new DataSourceCredentials()
            {
                ServerHost = "ServerHost",
                ServerPort = 666,
                DbName = "DbName",
                UserName = "TestUser",
                Pass = "TestPass"
            };
        }
    }
}
