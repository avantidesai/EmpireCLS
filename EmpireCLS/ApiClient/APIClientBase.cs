using System;
using System.Linq;
using System.Collections.Generic;

using EmpireCLS;

namespace EmpireCLS
{
	public class ApiClientBase : XmlClientBase
	{
		/*public ApiClientBase ()
		{
		}*/

		public ApiClientBase (string hostName)
			: base (hostName)
		{
			this.Token = ApplicationContext.Current.SessionToken;
			this.TokenExpiration = ApplicationContext.Current.SessionTokenExpiration;
			this.OnTokenExpired += TokenClient.GetTokenSilently;

			AddUrlPathModelTypeMapping ("ApiBaseModel", typeof(ApiBaseModel));
		}

		public ApiBaseModel Model { get; protected set; }

		protected override void ParseResponse (string urlPath)
		{
			string responseString = this.ResponseString;

			if (responseString.StartsWith ("<ApiBaseModel"))
				this.Model = base.DeserializeResponse<ApiBaseModel> ("ApiBaseModel", responseString);
			else {

				this.Model = base.DeserializeResponse<ApiBaseModel> (urlPath, responseString);

			}

			foreach (ApiModelItem i in this.Model.AllItems) {
				LogContext.Current.Log<ApiClientBase> ("ParseResponse", i.Code, i.Message, i.Type);
			}
		}

		public override bool HasErrors {
			get {
				if (this.Model != null && this.Model.HasErrors)
					return true;
				else
					return base.HasErrors;
			}
		}

		public override string ErrorMessage {
			get {
				if (this.Model != null && this.Model.AllItems.Count > 0)
					return this.Model.AllItems [0].Message;
				else
					return base.ErrorMessage;
			}
		}

		protected override System.Net.CookieContainer CookieContainer {
			get {
				return ApplicationContext.Current.Cookies;	
			}
		}
	}
}

