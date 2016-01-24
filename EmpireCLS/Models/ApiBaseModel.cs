using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpireCLS
{
    
	/// <summary>
	/// class for all core API calls internal to the API
	/// </summary>
	public partial class ApiResponseMessage
	{
	}

	/// <summary>
	/// base class for all API results
	/// </summary>
	public partial class ApiBaseModel
	{
		public ApiBaseModel ()
		{
			this.AllItems = new List<ApiModelItem> ();
		}

		public ApiBaseModel (string errorMessage, ApiModelItemType itemType = ApiModelItemType.SystemError)
		{
			this.AllItems = new List<ApiModelItem> () {
				new ApiModelItem () { Type = itemType, Message = errorMessage }
			};
		}

		public ApiBaseModel (Exception ex)
		{
			this.AllItems = new List<ApiModelItem> () {
				new ApiModelItem () { Type = ApiModelItemType.SystemError, Message = ex.Message }
			};
		}

		public ApiBaseModel AddInfo (string message, int code = 0)
		{
			this.AllItems.Add (new ApiModelItem () { Type = ApiModelItemType.Info, Message = message, Code = code });
			return this;
		}

		public ApiBaseModel AddValidationError (string message, int code = 0)
		{
			this.AllItems.Add (new ApiModelItem () {
				Type = ApiModelItemType.ValidationError,
				Message = message,
				Code = code
			});
			return this;
		}

            
		public List<ApiModelItem> AllItems { get; set; }

		public bool HasErrors { get { return this.AllItems.Count (di => di.Type == ApiModelItemType.SystemError || di.Type == ApiModelItemType.ValidationError) > 0; } }

		public bool HasWarnings { get { return this.AllItems.Count (di => di.Type == ApiModelItemType.Warning) > 0; } }

		public bool HasInfo { get { return this.AllItems.Count (di => di.Type == ApiModelItemType.Info) > 0; } }

	}

	public enum ApiModelItemType
	{
		Unknown,
		SystemError,
		ValidationError,
		Warning,
		Info
	}

	public partial class ApiModelItem
	{
		public ApiModelItem ()
		{
		}

		public string Message { get; set; }

		public int Code { get; set; }

		public ApiModelItemType Type { get; set; }

		public bool MustAccept { get; set; }

		public bool MustProvideOverride { get; set; }

	}
    
}