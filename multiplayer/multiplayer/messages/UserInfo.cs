using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	class PlayerInfo
		: ICommand
	{
		public string Name;

		public string Skin;
	}

	class PlayerInfoResponse
		: ICommand
	{
		public int Port;

		public PlayerInfoResponse(int port)
		{
			this.Port = port;
		}
	}
}
