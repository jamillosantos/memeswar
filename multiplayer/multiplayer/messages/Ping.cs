using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	class Ping
		: ICommand
	{
		public string Command
		{
			get
			{
				return "Ping";
			}
		}
	}
}
