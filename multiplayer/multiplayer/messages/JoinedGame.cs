using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	class JoinedGame
		: ICommand
	{
		public JoinedGame()
		{ }

		public JoinedGame(PlayerInfo info)
		{
			this.Player = info;
		}

		public string Command
		{
			get
			{
				return "JoinedGame";
			}
		}

		public PlayerInfo Player;
	}
}
