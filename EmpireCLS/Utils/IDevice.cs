using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmpireCLS
{
	public interface IDevice
	{
		string Name { get; }

		string Model { get; }

		string SystemName { get; }

		string SystemVersion { get; }

		string UiIdiom { get; }

      
	}
}