using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;



namespace EmpireCLS
{
	public class XmlClientBase : WebClientBase
	{
		private readonly Dictionary<string, Type> _urlPathXmlModelTypes = new Dictionary<string, Type> ();

		public XmlClientBase (string hostName)
			: base (hostName)
		{

		}


		protected void AddUrlPathModelTypeMapping (string urlPath, Type modelType)
		{
			if (!_urlPathXmlModelTypes.ContainsKey (urlPath))
				_urlPathXmlModelTypes.Add (urlPath, modelType);
		}

		protected typeOfObject DeserializeResponse<typeOfObject> (string urlPath, string responseString)
			where typeOfObject : class
		{
			if (!_urlPathXmlModelTypes.ContainsKey (urlPath))
				throw new ApplicationException (string.Format ("Missing urlPath key: {0}", urlPath));


			return XMLUtil.DeserializePlainXMLToObject (
				responseString, _urlPathXmlModelTypes [urlPath]
			) as typeOfObject;			
		}

		protected XmlClientBase PutObject (string urlPath, object o)
		{

			return Put (
				urlPath, XMLUtil.SerializeToXElement (o).ToString ()
			) as XmlClientBase;
		}

		protected XmlClientBase PostObject (string urlPath, object o)
		{

			return Post (
				urlPath, XMLUtil.SerializeToXElement (o).ToString ()
			) as XmlClientBase;
		}

		protected XmlClientBase PostObjectAsync (string urlPath, object o, Action asyncCompletedStrategy = null)
		{

			return PostAsync (
				urlPath, XMLUtil.SerializeToXElement (o).ToString (), asyncCompletedStrategy
			) as XmlClientBase;
		}


		protected override void SetupHeaders ()
		{
			if (!this.Headers.AllKeys.Contains (HttpRequestHeader.Accept.ToString ()))
				this.Headers.Add (HttpRequestHeader.Accept, "text/xml");
			if (!this.Headers.AllKeys.Contains (HttpRequestHeader.ContentType.ToString ()))
				this.Headers.Add (HttpRequestHeader.ContentType, "text/xml");

			base.CheckToken ();
	

			// Always set token, as it's value could change
			if (Token != null)
			if (this.Headers.AllKeys.Contains (HttpRequestHeader.Authorization.ToString ()))
				this.Headers.Remove (HttpRequestHeader.Authorization);


			// JMO, for s		ome reason the Token had a preceding quote, and we were missing the "Session" piece as well. 
			// this matches exactly to what is in the unit test.
			// The only problem is that putting "Session" in this header in the base class fails the application API calls for cache data
			this.Headers.Add (HttpRequestHeader.Authorization, "Session " + Token.Trim ('"'));
	
			
		}
	}
}

