using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.server
{
	class Game
	{
		private IDictionary<String, Player> _players;

		private IDictionary<String, Player> _playersName;

		private UdpServer _server;

		public UdpServer Server
		{
			get { return this._server; }
			set { this._server = value; }
		}

		public Game()
		{
			this._players = new Dictionary<String, Player>();
		}

		public void Add(Player p)
		{
			p.Game = this;
			this.Broadcast(new messages.JoinedGame(p.Info));
			this._players.Add(p.Endpoint.ToString(), p);
			this._playersName.Add(p.Info.Name, p);
		}

		public Player Get(EndPoint endpoint)
		{

			Player result = this._players[endpoint.ToString()];
			if (result == null)
				return null;
			else
				return result;
		}

		public void Broadcast(string data)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(data);
			foreach (KeyValuePair<string, Player> p in this._players)
			{
				this._server.Send(data, p.Value.Endpoint);
			}
		}

		public void Broadcast(object o)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(o));
			foreach (KeyValuePair<string, Player> p in this._players)
			{
				this._server.Send(bytes, p.Value.Endpoint);
			}
		}

		public void Send(string name, string data)
		{
			this._playersName[name].Send(data);
		}
	}
}
