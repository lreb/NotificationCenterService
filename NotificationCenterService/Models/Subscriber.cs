using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenterService.Models
{
	/// <summary>
	/// Subscriber model
	/// </summary>
	public class Subscriber
	{
		/// <summary>
		/// arn name provided by aws
		/// </summary>
		public string TopicArn { get; set; }
		/// <summary>
		/// protocol e.g. sms
		/// </summary>
		public string Protocol { get; set; }
		/// <summary>
		/// endpoint to add e.g. phone number, email, etc
		/// </summary>
		public string EndPoint { get; set; }
	}
}
