using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using multiplayer.server;
using Newtonsoft.Json;

namespace multiplayer
{

	public class PortAlreadyUsed: Exception
	{
		private int _port;

		public int port
		{
			get { return this._port; }
		}

		public PortAlreadyUsed(int port, Exception innerException)
			: base("The port " + port + " is already in use.", innerException)
		{
			this._port = port;
		}
	}

	class ServerHandler
		: server.IServerHandler
	{
		private UdpServer _server;

		private int port = 50000;

		private server.Game _game;

		public ServerHandler()
		{
			this._game = new server.Game();
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
				this._game.Server = this._server;
			}
		}

		public void Received(byte[] data, EndPoint endpoint)
		{
			try
			{
				Player player = this._game.Get(endpoint);
				if (player == null)
				{
					messages.PlayerInfo pi = JsonConvert.DeserializeObject<messages.PlayerInfo>(Encoding.ASCII.GetString(data));
					messages.PlayerInfoResponse piResponse = new messages.PlayerInfoResponse(this.port++);
					this._server.Send(JsonConvert.SerializeObject(piResponse), endpoint);
					this._game.Add(new Player(pi));
				}
				else
				{
					player.Info = JsonConvert.DeserializeObject<messages.PlayerInfo>(Encoding.ASCII.GetString(data));
				}
			}
			catch (Exception e)
			{
				this._server.Send(JsonConvert.SerializeObject(new messages.Error(String.Format("Error initializing client. Message: {0}", e.Message))), endpoint);
			}
		}
	}

	public class Server
	{
		public int Port = 8463;

		private bool _running;

		server.UdpServer _server;

		public bool Running
		{
			get
			{
				return this._running;
			}
		}

		public void Stop()
		{
			this._running = false;
		}

		public void Run()
		{
			if (this.Running)
				throw new Exception("Already running.");

			this._running = true;
			IPEndPoint ep = new IPEndPoint(IPAddress.Any, this.Port);
			this._server = new server.UdpServer(new ServerHandler());
			this._server.Start(ep);
		}
	}
}
