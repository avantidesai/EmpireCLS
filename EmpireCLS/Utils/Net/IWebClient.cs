using System;

namespace EmpireCLS
{
	public interface IWebClient
	{
		Action<IWebClient> ClientCompleted { get; set; }

		bool HasErrors { get; }

		string ErrorMessage { get; }

		Exception LastError { get; }
	}
}

