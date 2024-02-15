using System;
using System.Collections.Generic;
using System.Text;

namespace HorusApp.Models
{
	public class User
	{
        public string Email { get; set; }
		public string Firstname { get; set; }
		public string Surename { get; set; }
		public string Password { get; set; }
        public string AuthorizationToken { get; set; }
    }
}
