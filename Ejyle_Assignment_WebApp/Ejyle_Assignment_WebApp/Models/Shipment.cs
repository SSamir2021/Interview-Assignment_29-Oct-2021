using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ejyle_Assignment_WebApp.Models
{
    public class Shipment
	{		
		public int ShipmentId { get; set; }

		[Required]
		[DisplayName("Sender Name")]
		public string SenderName { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[DisplayName("Recipient Address")]
		public string RecipientAddress { get; set; }

		[Required]
		public bool Expedited { get; set; }

		[Required]
		[DisplayName("Shipment Type")]
		public string ShipmentType { get; set; }
	}

	public enum ShipmentType
	{
		LTL,		
		VolumeLTL,
		Truckload
	}	
}
