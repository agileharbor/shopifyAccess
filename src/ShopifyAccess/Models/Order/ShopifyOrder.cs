﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ShopifyAccess.Models.Order.Discounts;

namespace ShopifyAccess.Models.Order
{
	[ DataContract ]
	public class ShopifyOrder
	{
		[ DataMember( Name = "id" ) ]
		public long Id{ get; set; }

		[ DataMember( Name = "total_price" ) ]
		public decimal Total{ get; set; }

		[ DataMember( Name = "created_at" ) ]
		public DateTime CreatedAt{ get; set; }

		[ DataMember( Name = "line_items" ) ]
		public IList< ShopifyOrderItem > OrderItems{ get; set; }

		[ DataMember( Name = "order_number" ) ]
		public int OrderNumber{ get; set; }

		[ DataMember( Name = "billing_address" ) ]
		public ShopifyBillingAddress BillingAddress{ get; set; }

		[ DataMember( Name = "shipping_address" ) ]
		public ShopifyShippingAddress ShippingAddress{ get; set; }

		[ DataMember( Name = "customer" ) ]
		public ShopifyCustomer Customer{ get; set; }

		[ DataMember( Name = "closed_at" ) ]
		public DateTime? ClosedAt{ get; set; }

		[ DataMember( Name = "cancelled_at" ) ]
		public DateTime? CancelledAt{ get; set; }

		[ DataMember( Name = "financial_status" ) ]
		public ShopifyFinancialStatus FinancialStatus{ get; set; }

		[ DataMember( Name = "fulfillment_status" ) ]
		public FulfillmentStatusEnum FulfillmentStatus{ get; set; }

		[ DataMember( Name = "source_name" ) ]
		public ShopifySourceNameEnum SourceName{ get; set; }

		[ DataMember( Name = "location_id" ) ]
		public string LocationId{ get; set; }

		[ DataMember( Name = "name" ) ]
		public string Name{ get; set; }

		[ DataMember( Name = "shipping_lines" ) ]
		public IList< ShopifyOrderShippingLine > ShippingLines{ get; set; }

		[ DataMember( Name = "discount_codes" ) ]
		public IEnumerable< ShopifyDiscountCode > DiscountCodes { get; set; }

		[ DataMember( Name = "tax_lines" ) ]
		public IEnumerable< ShopifyTaxLine > TaxLines{ get; set; }

		public bool IsShipped
		{
			get { return this.ClosedAt.HasValue; }
		}

		public bool IsCancelled
		{
			get { return this.CancelledAt.HasValue; }
		}
	}

	public enum ShopifyFinancialStatus
	{
		// ReSharper disable InconsistentNaming
		Undefined,
		pending,
		authorized,
		partially_paid,
		paid,
		partially_refunded,
		refunded,
		voided
		// ReSharper restore InconsistentNaming
	}

	public enum FulfillmentStatusEnum
	{
		// ReSharper disable InconsistentNaming
		Undefined,
		fulfilled,
		partial
		// ReSharper restore InconsistentNaming
	}

	public enum ShopifySourceNameEnum
	{
		Undefined,
		web,
		pos,
		iphone,
		android
	}
}