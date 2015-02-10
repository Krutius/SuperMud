using System;
using System.Reflection;

namespace SuperMud.Engine
{
	public class ExecutableItem
	{
		public Type Type {
			get;
			set;
		}

		public String Command {
			get;
			set;
		}

		public MethodInfo Method {
			get;
			set;
		}
	}
}

