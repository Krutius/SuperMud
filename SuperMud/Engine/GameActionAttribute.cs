using System;

namespace SuperMud.Engine
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class GameActionAttribute : Attribute
	{
		public String Command {
			get;
			private set;
		}

		public GameActionAttribute (String command)
		{
			this.Command = command.ReplaceSpecialSign().ToLower();
		}
	}
}

