using System;

namespace SuperMud.Engine
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class GameIdentificationAttribute : Attribute
	{
		public String Name {
			get;
			private set;
		}

		public GameIdentificationAttribute (String name)
		{
			this.Name = name.ReplaceSpecialSign().ToLower();
		}
	}
}

