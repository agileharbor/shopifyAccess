﻿using FluentAssertions;
using NUnit.Framework;
using ShopifyAccess;
using ShopifyAccess.Models.Configuration.Command;
using ShopifyAccess.Models.ProductVariant;

namespace ShopifyAccessTests.Products
{
	[ TestFixture ]
	public class ProductVariantTests
	{
		private readonly IShopifyFactory ShopifyFactory = new ShopifyFactory();
		private const string ShopName = "skuvault";
		private const string AccessToken = "ce22522b5b2ad8cce975429ec265db4c";
		private const long VariantId = 337095345;

		[ Test ]
		public void ProductVariantQuantityUpdated()
		{
			var config = new ShopifyCommandConfig( ShopName, AccessToken );
			var service = this.ShopifyFactory.CreateService( config );
			var variantToUpdate = new ShopifyProductVariant { Id = VariantId, Quantity = 100, InventoryManagement = InventoryManagement.Shopify };
			service.UpdateProductVariantQuantity( variantToUpdate );
		}
	}
}