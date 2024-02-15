using HorusApp.Enumerations;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorusApp.Models.Responses
{
	public class CustomError : BindableBase
	{
		[JsonProperty("error")]
		public CustomHttpStatusCodes Error { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
