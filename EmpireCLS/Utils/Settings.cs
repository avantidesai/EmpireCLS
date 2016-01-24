using System;

namespace EmpireCLS
{
	public enum SettingsEvironmentType
	{
		Local,
		Dev,
		Test,
		Prod,
		Uat
	}

	public class Settings
	{

		private static readonly Settings _settings = new Settings ();

		/// <summary>
		/// singleton support
		/// </summary>
		private Settings ()
		{
#if LOCAL

           // this.EmpireCLSHost = "eclsapidev.empirecls.com";
           // this.EmpireCLSSite = "eclssitedev.empirecls.com/Booking";
            
            this.LogIssues = false;  //Xamarin Insights used currently

            this.GooglePlacesApiKey = "AIzaSyDJ8Kn0IEviBcqYBTcvDc0bCYA6Gs69pAA";
            EnableCrashReportExecution = false;

			this.EmpireCLSHost = "testservices.empirecls.com:64443";	            
			this.EmpireCLSSite = "testservices.empirecls.com/booking";
#elif DEV
			this.EmpireCLSHost = "pwdev.empirecls.com";
            this.EmpireCLSSite = "eclssitedev.empirecls.com/Booking";
            
            this.LogIssues = false;
            this.GooglePlacesApiKey = ""; 
            EnableCrashReportExecution = false;
#elif TEST
			this.EmpireCLSHost = "testservices.empirecls.com:64443";	            
            this.EmpireCLSSite = "testservices.empirecls.com/booking";
           
            this.GoogleMapsApiKey = "N_flvPyj0WjF2SWywP0P_u3NTug=";
            //this.GooglePlacesApiKey = "AIzaSyAdK1F5XEMBA3_1_L16hCJqT5HUw7qQsNQ";
            this.GooglePlacesApiKey = "AIzaSyAhXzqoaNTilC2LNuIou8gN4_FCXFR-p_k";
            this.LogIssues = false;
            EnableCrashReportExecution = true;
#elif PROD
			this.EmpireCLSHost = "api.empirecls.com";
            this.EmpireCLSSite = "empirecls.com/booking";
            
            this.GoogleMapsApiKey = "N_flvPyj0WjF2SWywP0P_u3NTug=";
            this.GooglePlacesApiKey = "AIzaSyAhXzqoaNTilC2LNuIou8gN4_FCXFR-p_k";
            this.LogIssues = true;
            EnableCrashReportExecution = true;
#else
			this.LogIssues = false;  //Xamarin Insights used currently

			this.GooglePlacesApiKey = "AIzaSyDJ8Kn0IEviBcqYBTcvDc0bCYA6Gs69pAA";
			EnableCrashReportExecution = false;

			this.EmpireCLSHost = "testservices.empirecls.com:64443";	            
			this.EmpireCLSSite = "testservices.empirecls.com/booking";
#endif
		}

		public static Settings Current { get { return _settings; } }

		public string StarRankTopPath { get { return "Images/star-rating-top.gif"; } }

		public SettingsEvironmentType Environment {
			get {
#if LOCAL
                return SettingsEvironmentType.Local;
#elif DEV
				return SettingsEvironmentType.Dev;			
#elif TEST
				return SettingsEvironmentType.Test;			
#elif PROD
				return SettingsEvironmentType.Prod;			
#elif UAT
				return SettingsEvironmentType.Uat;			
#else
				return SettingsEvironmentType.Local;
#endif
			}
		}

		public string MobileURL { get { return "Mobile"; } }

		public string AboutPath { get { return MobileURL + "/About/"; } }

		public string TermsPath { get { return MobileURL + "/Terms/"; } }

		public string PrivacyPath { get { return MobileURL + "/Privacy/"; } }

		public string FaqPath { get { return MobileURL + "/FAQ/"; } }

		public string FaqAsDirectedPath { get { return FaqPath + "default.html?theme=as-directed"; } }

		public string FaqEstimatedTotalPath { get { return FaqPath + "default.html?theme=estimated-Total"; } }

		public string DisclaimerPath { get { return MobileURL + "/TripDisclaimer/"; } }

		public string AccountBenefitPath { get { return MobileURL + "/AccountBenefits/"; } }

		public string ContactUsEmail { get { return "customer@empirecls.com"; } }

		public string ContactUsApplicationEmail { get { return "webapps@empirecls.com"; } }

		public string ContactUsReservationEmail { get { return "reservations@empirecls.com"; } }

		public string ContactUsEmailText { get { return "Email Customer Service"; } }

		public string ContactUsPhone { get { return "1-201-588-2966"; } }

		public string ContactUsApplicationPhone { get { return "1-201-588-2966"; } }

		public string ContactUsReservationPhone { get { return "1-800-451-5466"; } }

		public string ContactUsPhoneFormatted { get { return "1 (201) 588-2966"; } }

		public string ContactUsPhoneText { get { return "Call Customer Service"; } }


		/// <summary>
		/// Need to opt into this depending upon the application context being used
		/// </summary>
		private bool _useHttps = true;

		public bool UseHttps { get { return _useHttps; } set { _useHttps = value; } }

		public string HttpProtocol { get { return Settings.Current.UseHttps ? "HTTPS" : "HTTP"; } }

		public string GoogleMapApiHost {
			get {
				return "maps.googleapis.com";
			}
		}

		public string BingMapApiHost {
			get {
				return "dev.virtualearth.net";
			}
		}

		public string GoogleApiKey {
			get {
				return "N_flvPyj0WjF2SWywP0P_u3NTug=";
			}
		}

		public string GoogleApiUrl {
			get {
				//?key={0}&sensor=true&v=3.15
				return String.Format ("/maps/api/geocode/json?sensor=true&v=3.15", this.GoogleApiKey);
			}
		}

		public string EmpireCLSHost { get; set; }

		public string EmpireCLSSite { get; set; }

		public bool LogIssues { get; set; }

		public string GoogleMapsApiKey { get; set; }

		public string GooglePlacesApiKey { get; set; }

		public bool EnableCrashReportExecution { get; set; }
	}
}

