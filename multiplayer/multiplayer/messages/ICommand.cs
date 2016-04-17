using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	interface ICommand
	{
		string Command
		{
			get;
		}
	}
}
