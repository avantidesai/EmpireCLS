using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpireCLS
{
	public class InvalidAddressException : Exception
	{
		public InvalidAddressException (string message, Exception innerException = null) : base (message, innerException)
		{

		}
	}
}
