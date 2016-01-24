using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using MonoTouch.Foundation;
//using MonoTouch.UIKit;

namespace EmpireCLS
{
	public class DefaultSettingsCache
	{
		private object _lock = new object ();

		public DefaultSettings Value { get; set; }

		public DefaultSettingsCache (Action<DefaultSettings> onSyncComplete = null)
		{
			Sync (onSyncComplete);
		}

		/// <summary>
		/// DI device specific application version stuff
		/// </summary>
		/// <remarks>
		/// 6/5/14 JMO, decouple IOS utils from portable mobile assembly
		/// </remarks>
		public static Func<string> ApplicationVersionLocator { get; set; }

		private static DefaultSettings RetrieveDefaultSettings ()
		{
			using (CachesClient c = new CachesClient ()) {
				ApplicationContext.Current.PerfStart ("[ -- RetrieveDefaultSettings -- ]");
				var appVersion = DefaultSettingsCache.ApplicationVersionLocator != null ? DefaultSettingsCache.ApplicationVersionLocator () : "";
				ApplicationContext.Current.PerfStart ("[ -- RetrieveDefaultSettings : Get From API -- ]");
				var response = c.GetDefaultSettings (appVersion).GetDefaultSettingsResult;
				ApplicationContext.Current.PerfEnd ("[ -- RetrieveDefaultSettings : Get From API -- ]");
				ApplicationContext.Current.PerfEnd ("[ -- RetrieveDefaultSettings -- ]");
				return response;
			}
		}

		public void Sync (Action<DefaultSettings> onSyncComplete = null)
		{
			lock (_lock) {
				var defaultSettings = RetrieveDefaultSettings ();
				this.Value = defaultSettings;
			}

			if (onSyncComplete != null) {
				onSyncComplete (this.Value);
			}
		}
	}
}