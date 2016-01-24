using System;
using System.Linq;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Threading;

namespace EmpireCLS
{
	public class JsonWebClientBase : WebClientBase
	{

		public JsonWebClientBase (string hostName)
			: base (hostName)
		{
			SetupHeaders ();
		}

		protected override void SetupHeaders ()
		{
			if (!this.Headers.AllKeys.Contains ("Accept"))
				this.Headers.Add ("Accept", "application/json");
			if (!this.Headers.AllKeys.Contains ("Content-Type"))
				this.Headers.Add ("Content-Type", "application/json");
		}

		public JsonValue Results { get; protected set; }
				
	}
}

