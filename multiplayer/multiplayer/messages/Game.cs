using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace multiplayer.messages
{
	class Game<T>
	{
		public string Id;

		public T Message;
	}
}
