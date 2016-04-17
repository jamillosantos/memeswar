using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	class Error
	{
		[JsonProperty]
		public bool Success
		{
			get { return false; }
		}

		[JsonProperty]
		public string Message;

		public Error(string message)
		{
			this.Message = message;
		}
	}
}
