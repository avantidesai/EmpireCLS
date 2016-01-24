using System;
using System.Net;
using System.Json;
using EmpireCLS.Mobile;

namespace EmpireCLS
{
	public class PromotionClient : ApiClientBase
	{
		private static class UrlPaths
		{
			public const string PromotionGetByCode = "/api/Promotions/Promotion_GetByCode?PromoCode={0}";

		}

		public PromotionClient ()
			: base (Settings.Current.EmpireCLSHost)
		{
			//AddUrlPathModelTypeMapping(UrlPaths.PromotionGetByCode, typeof(Promotion));
		}

		public Promotion GetPromotionResult { get { return this.Model as Promotion; } }

		public PromotionClient PromotionGet (string promotionCode, bool async = false)
		{
			if (promotionCode == null || promotionCode.Trim () == "")
				return null;

			string urlPath = string.Format (UrlPaths.PromotionGetByCode, promotionCode);
			AddUrlPathModelTypeMapping (urlPath, typeof(Promotion));
			return InvokeStrategy<PromotionClient> (() => {
				if (!async)
					Get (urlPath);
				else
					GetAsync (urlPath);
			});		
		}
	}
}

