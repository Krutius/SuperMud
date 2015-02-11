using System;
using System.Reflection;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class TargetObjectMethodMatching
	{
		public MethodInfo Method {
			get;
			set;
		}

		public TargetObjectMatching ObjectMatching {
			get;
			set;
		}

		public TargetObjectMethodMatching (TargetObjectMatching objectMatching, MethodInfo method)
		{
			this.Method = method;
			this.ObjectMatching = objectMatching;
		}

		public void Execute(Player p) {
			try {
				this.Method.Invoke(this.ObjectMatching.GameObject, new object[] { p });
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}

