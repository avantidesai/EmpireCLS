using System;

using System.Linq;
using System.Collections.Generic;

namespace EmpireCLS
{
	public class LogContext
	{
		#region singleton

		private static readonly LogContext _current = new LogContext ();

		private LogContext ()
		{
		}

		public static LogContext Current { get { return _current; } }

		public static bool CrashReportEnabled;
		private readonly List<string> _logEntries = new List<string> ();

		#endregion

		#region DI stuff

		public static IDevice Device { get; set; }

		#endregion

		/// <summary>
		/// Log the specified context, error and contextParams.
		/// </summary>
		/// <param name='context'>
		/// Context.
		/// </param>
		/// <param name='error'>
		/// Error.
		/// </param>
		/// <param name='contextParams'>
		/// Context parameters.
		/// </param>
		/// <typeparam name='typeOfEntity'>
		/// The 1st type parameter.
		/// </typeparam>
		/// <description>
		/// 
		/// *****   uses a generic so that static classes can call
		/// 
		/// </description>
		/// 
		
        
		public void Log<typeOfEntity> (string context, Exception error, params object[] contextParams)
		{
           
			Log<typeOfEntity> (context, "", error, contextParams);
		}

		public void Log<typeOfEntity> (string message)
		{
			Console.WriteLine (message);
		}

		public void Log<typeOfEntity> (string context, string user, Exception error, params object[] contextParams)
		{
			/*string contextString = string.Format ("{0}.{1}", typeof(typeOfEntity).Name, context);

			Utilities.TryMobileAction<LogContext> ("Log", () => {
				string contextParamsString = string.Join (", ", (from c in contextParams
				                                                 select c.ToString ()).ToArray ());

				string exceptionString = error != null
                    ? string.Format (" [{0}][{1}]", error.GetType ().Name, error.Message)
                    : "";

				// 5/12/14 JMO, make it easier to read the log output
				string entryBase = string.Format ("{0:yyyy-MM-dd HH:mm:ss.fff} {1}[{2}]{3}", DateTime.Now, contextString, contextParamsString, exceptionString);
				string entry = "";
				
				lock (_logEntries) {
					if (_logEntries.Count > 100)
						_logEntries.RemoveAt (0);
					_logEntries.Add (entry);
				}
                
				Console.WriteLine (entry);
			});
			/* catch (Exception ex)
            {
                Console.WriteLine("Exception during logging: context:{0}, exception:{1}", contextString, ex == null ? "" : ex.Message);
            }
            * */
		}

		public void Log<typeOfEntity> (string context, params object[] contextParams)
		{
			Log<typeOfEntity> (context, null, contextParams);
		}

		public List<string> LogEntries {
			get {
				lock (_logEntries) {
					List<string> entries = new List<string> (_logEntries);
					return entries;
				}
			}
		}
	
	
	}
}

