using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.IO;

//using WebSiteProject.Areas.Api.Models;
using Foundation;

using Xamarin;


namespace EmpireCLS
{


	/// <summary>
	/// Application context.
	/// </summary>
	public class ApplicationContext
	{
		private static class Constants
		{

			public const string ActiveQuoteGetInfoFileName = "QuoteGetInfo.xml";
			public const string RememberedUserFileName = "AccountUser.xml";
			public const string LastGuestInfoFileName = "LastGuestInfo.xml";
		}

		#region singleton

		private static readonly ApplicationContext _applicationContext = new ApplicationContext ();

		public static ApplicationContext Current { get { return _applicationContext; } }

		#endregion // singleton

		private BookingEntry _activeQuoteGetInfo = null;
		private AccountLogonInfo _rememberedUser = null;

		/// <summary>
		/// support singleton
		/// </summary>
		private ApplicationContext ()
		{

		}

		#region perf stuff

		internal class PerfItem
		{
			public string Context { get; set; }

			public double StartTick { get; set; }
		}

		private readonly List<PerfItem> _perfItems = new List<PerfItem> ();

		public void PerfStart (string context)
		{
#if DEBUG
			var item = new PerfItem () { Context = context, StartTick = Environment.TickCount };
			_perfItems.Add (item);
#endif
		}

		public void PerfEnd (string context, params object[] contextParams)
		{
#if DEBUG
			var item = (from i in _perfItems
			            where i.Context == context
			            select i).FirstOrDefault ();
			if (item == null)
				return;
			_perfItems.Remove (item);

			List<object> contextParamsToLog = new List<object> (contextParams);
			contextParamsToLog.Add ("Perf");
			contextParamsToLog.Add (string.Format ("{0:0,0}", Environment.TickCount - item.StartTick));

			LogContext.Current.Log<ApplicationContext> (context, contextParamsToLog.ToArray ());
#endif
		}

		#endregion // perfstuff

		public bool WasUserCached {
			get {
				return this.RememberedUser.UserName != null && this.RememberedUser.Password != null && !this.RememberedUser.IsGuest;
			}
		}

		public bool IsLoggedIn {
			get {

				if (this.CurrentUser.User == null)
					return false;

				return !this.CurrentUser.User.IsGuest;

			}
		}

		public UserContext CurrentUser { get { return UserContext.Current; } }

		private string _sessionToken;

		public string SessionToken {
			get {
				return _sessionToken;
			}
			set {
				_sessionToken = value;
			}

		}

		public DateTime SessionTokenExpiration { get; set; }

		public DefaultSettingsCache DefaultSettingsCache {
			get {
				return _defaultSettingsCache;
			}

			set {
				_defaultSettingsCache = value;
			}
		}

		private DefaultSettingsCache _defaultSettingsCache;

		public CookieContainer Cookies { get; internal set; }

		public string DeviceModel { get; set; }

		public string DeviceOsVersion { get; set; }

		/// <summary>
		/// loads an XML entity from a persistent file
		/// </summary>
		/// <returns>
		/// The entity load.
		/// </returns>
		/// <param name='pathToFile'>
		/// Path to file.
		/// </param>
		/// <typeparam name='typeOfEntity'>
		/// The 1st type parameter.
		/// </typeparam>
		public typeOfEntity XmlEntityLoad<typeOfEntity> (string fileName, bool instantiateIfNotExists = true)
            where typeOfEntity : class, new()
		{
			PerfStart ("XmlEntityLoad");

			string pathToFile;

			// if the file already contains a path, don't add another
			if (fileName.Contains ("/"))
				pathToFile = fileName;
			else
				pathToFile = GetFilePath (fileName);
			//pathToFile = System.IO.Path.Combine("..", "Library", fileName);

			typeOfEntity loadedEntity = null;

			try {
				if (System.IO.File.Exists (pathToFile)) {
					loadedEntity = XMLUtil.Deserialize<typeOfEntity> (pathToFile);
					LogContext.Current.Log<ApplicationContext> ("LoadXmlFile.Loaded", pathToFile, typeof(typeOfEntity).Name);
				}

                    // If the file does not exists and we should create a new instance of this object
                else if (instantiateIfNotExists) {
					loadedEntity = new typeOfEntity ();
					LogContext.Current.Log<ApplicationContext> ("LoadXmlFile.Created", pathToFile, typeof(typeOfEntity).Name);
				}
			} catch (Exception ex) {
				LogContext.Current.Log<ApplicationContext> ("LoadXmlFile.Error", ex, pathToFile, typeof(typeOfEntity).Name);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				Utilities.TryMobileAction<ApplicationContext> ("LoadXMLFile", () => {
					LogContext.Current.Log<ApplicationContext> ("Deleting file", pathToFile);
					if (System.IO.File.Exists (pathToFile))
						System.IO.File.Delete (pathToFile);
				});

			}

			PerfEnd ("XmlEntityLoad", typeof(typeOfEntity).Name);
			return loadedEntity;
		}

		/// <summary>
		/// saves the xml entity to the persistent store
		/// </summary>
		/// <param name='pathToFile'>
		/// Path to file.
		/// </param>
		/// <param name='xmlEntity'>
		/// Xml entity.
		/// </param>
		/// <typeparam name='typeOfEntity'>
		/// The 1st type parameter.
		/// </typeparam>
		public void XmlEntitySave<typeOfEntity> (string fileName, typeOfEntity xmlEntity)
            where typeOfEntity : class
		{
			//string pathToFile = System.IO.Path.Combine("..", "Library", fileName);

			Utilities.TryMobileAction<ApplicationContext> ("XmlEntitySave", () => {
				string pathToFile = GetFilePath (fileName);
				PerfStart ("XmlEntitySave");
				XMLUtil.SerializeToFile (xmlEntity, pathToFile);
				LogContext.Current.Log<ApplicationContext> ("XmlEntitySave.Saved", pathToFile, typeof(typeOfEntity).Name);
				PerfEnd ("XmlEntitySave", typeof(typeOfEntity).Name);
			});

		}

		private string GetFilePath (string fileName)
		{
			string cacheFolderPath;
			string deviceVersionMainPart = DeviceOsVersion.Substring (0, DeviceOsVersion.IndexOf ("."));
			if (Convert.ToDouble (deviceVersionMainPart) >= 8 && DeviceModel.ToLower ().Contains ("iphone")) {
				var url = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User) [0];

				cacheFolderPath = url.Path;
			} else {
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				cacheFolderPath = Path.GetFullPath (Path.Combine (documents, "..", "Library", "Caches"));
			}
			return System.IO.Path.Combine (cacheFolderPath, fileName);
		}

		/// <summary>
		/// Gets or sets the remembered user.
		/// </summary>
		/// <value>
		/// The remembered user.
		/// </value>

		public AccountLogonInfo RememberedUser {
			get {
				if (_rememberedUser != null)
					return _rememberedUser;

				_rememberedUser = XmlEntityLoad<AccountLogonInfo> (Constants.RememberedUserFileName);
				return _rememberedUser;
			}

			set {
				if (!value.RememberMe) {
					value.UserName = null;
					value.Password = null;
				}

				XmlEntitySave<AccountLogonInfo> (Constants.RememberedUserFileName, value);
				_rememberedUser = XmlEntityLoad<AccountLogonInfo> (Constants.RememberedUserFileName);
			}
		}
		/*
        public void RememberedUserUpdate(Action<AccountLogonInfo> updateStrategy)
        {
            updateStrategy(Current.RememberedUser);
            Current.RememberedUser = Current.RememberedUser;
        }
*/
		public TripUpdateConfirmation TripConfirmation { get; set; }

		public TripCancelConfirmation TripCancelConfirmation { get; set; }

		/// <summary>
		/// Gets or sets the active quote get info.
		/// </summary>
		/// <value>
		/// The active quote get info.
		/// </value>

		public BookingEntry ActiveQuoteGetInfo {
			get {
				if (_activeQuoteGetInfo != null)
					return _activeQuoteGetInfo;

				_activeQuoteGetInfo = XmlEntityLoad<BookingEntry> (Constants.ActiveQuoteGetInfoFileName);

				return _activeQuoteGetInfo;
			}
			set {
				// Clear fields that should not be saved/serialized to file.
				/* Alternative solution would be to modify booking controller fields
                   to be virtual, override them with non-serializable attributes,
                   and serialize that class. */
				_activeQuoteGetInfo = null; //reset
				var creditCardNumber = value.CreditCardNumber;
				var creditCardSecurityCode = value.CreditCardSecurityCode;
				value.CreditCardNumber = String.Empty;
				value.CreditCardSecurityCode = String.Empty;

				XmlEntitySave<BookingEntry> (Constants.ActiveQuoteGetInfoFileName, value);
				_activeQuoteGetInfo = XmlEntityLoad<BookingEntry> (Constants.ActiveQuoteGetInfoFileName);

				// Restore the old values we are not serializing.
				value.CreditCardNumber = creditCardNumber;
				value.CreditCardSecurityCode = creditCardSecurityCode;

				// don't allow number of passengers to be less than 1
				if (value.NumberOfPassengers < 1)
					value.NumberOfPassengers = 1;
				//if (_activeQuoteGetInfo.NumberOfPassengers < 1)
				//_activeQuoteGetInfo.NumberOfPassengers = 1;
			}
		}
	}
}

