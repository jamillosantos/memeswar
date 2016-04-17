using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
	class Program
	{
		static void Main(string[] args)
		{
			multiplayer.Server server = new multiplayer.Server();
			try
			{
				server.Run();
				while (server.Running)
				{
					Thread.Sleep(500);
				}
			}
			catch (Exception)
			{
				// Console.WriteLine(e.Message);
				throw;
			}
		}
	}
}
