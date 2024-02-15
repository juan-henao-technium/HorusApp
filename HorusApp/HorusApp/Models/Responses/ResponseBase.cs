using HorusApp.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HorusApp.Models.Responses
{
	public class ResponseBase<T>
	{
		[JsonIgnore()]
		public HttpResponseMessage HttpResponse { get; set; }
		public T Data { get; set; }
		public CustomError CustomError { get; set; }
		public CustomHttpStatusCodes HttpCustomStatusCode { get; set; }
	}

	public class ResponseBase
	{
		[JsonIgnore()]
		public HttpResponseMessage HttpResponse { get; set; }
		public CustomError CustomError { get; set; }
		public CustomHttpStatusCodes HttpCustomStatusCode { get; set; }
	}
}
