﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using Netco.Logging.NLogIntegration;
using NUnit.Framework;
using ShopifyAccess;
using ShopifyAccess.Models.Configuration.Command;
using ShopifyAccess.Models.Order;

namespace ShopifyAccessTests.Orders
{
	[ TestFixture ]
	public class OrdersListTests
	{
		private readonly IShopifyFactory ShopifyFactory = new ShopifyFactory();
		private ShopifyCommandConfig Config;

		[ SetUp ]
		public void Init()
		{
			Directory.SetCurrentDirectory( TestContext.CurrentContext.TestDirectory );
			const string credentialsFilePath = @"..\..\Files\ShopifyCredentials.csv";
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();

			var cc = new CsvContext();
			var testConfig = cc.Read< TestCommandConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new ShopifyCommandConfig( testConfig.ShopName, testConfig.AccessToken );
		}

		[ Test ]
		public void OrdersFilteredFulfillmentStatusDateLoaded()
		{
			var service = this.ShopifyFactory.CreateService( this.Config );
			var orders = service.GetOrders( ShopifyOrderStatus.any, DateTime.UtcNow.AddDays( -10 ), DateTime.UtcNow );

			orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task OrdersFilteredFulfillmentStatusDateLoadedAsync()
		{
			var service = this.ShopifyFactory.CreateService( this.Config );
			var orders = await service.GetOrdersAsync( ShopifyOrderStatus.any, DateTime.UtcNow.AddDays( -200 ), DateTime.UtcNow, CancellationToken.None );

			orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void OrdersNotLoaded_IncorrectToken()
		{
			var config = new ShopifyCommandConfig( this.Config.ShopName, "blabla" );
			var service = this.ShopifyFactory.CreateService( config );
			ShopifyOrders orders = null;
			try
			{
				orders = service.GetOrders( ShopifyOrderStatus.any, DateTime.UtcNow.AddDays( -200 ), DateTime.UtcNow );
			}
			catch( WebException )
			{
				orders.Should().BeNull();
			}
		}

		[ Test ]
		public void OrdersNotLoaded_IncorrectShopName()
		{
			var config = new ShopifyCommandConfig( "blabla", this.Config.AccessToken );
			var service = this.ShopifyFactory.CreateService( config );
			ShopifyOrders orders = null;
			try
			{
				orders = service.GetOrders( ShopifyOrderStatus.any, DateTime.UtcNow.AddDays( -200 ), DateTime.UtcNow );
			}
			catch( WebException )
			{
				orders.Should().BeNull();
			}
		}
	}
}