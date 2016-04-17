using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.server
{
	class Player
		: IServerHandler
	{
		private messages.PlayerInfo _info;

		private UdpServer _server;

		private IPEndPoint _endpoint;

		private Game _game;

		public IPEndPoint Endpoint
		{
			get { return this._endpoint; }
		}

		public messages.PlayerInfo Info
		{
			get { return this._info; }
			set
			{
				this._info = value;
			}
		}

		public UdpServer Server
		{
			get
			{
				return this._server;
			}

			set
			{
				this._server = value;
			}
		}

		public Game Game
		{
			get
			{
				return this._game;
			}
			set
			{
				this._game = value;
			}
		}

		public Player(messages.PlayerInfo info)
		{
			this._info = info;
			//
			this._server = new UdpServer(this);
			this._server.Start(this.Endpoint);
		}

		public void Received(byte[] data, EndPoint endpoint)
		{
			string msg = Encoding.ASCII.GetString(data);
			Console.WriteLine("@{0}: {1}", this.Info.Name, msg);
		}

		public void Send(byte[] data)
		{
			this._server.Send(data, this.Endpoint);
		}

		public void Send(string data)
		{
			this._server.Send(Encoding.ASCII.GetBytes(data), this.Endpoint);
		}
	}
}
