using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class CommandTarget
	{
		private IGermanVerbTarget VerbTarget {
			get;
			set;
		}

		private List<MethodInfo> TargetMethods {
			get;
			set;
		}

//		private IEnumerable<Type> TargetTypes {
//			get {
//				return this.TargetMethods.Select (m => m.ReflectedType).Distinct ();
//			}
//		}
//
		public CommandTarget (IGermanVerbTarget verbTarget, List<MethodInfo> targetMethods)
		{
			this.TargetMethods = targetMethods;
			this.VerbTarget = verbTarget;
		}

		public IEnumerable<TargetObjectMethodMatching> FindMatchings (Player player) {
			return this.VerbTarget.FindMatchings (player)
				.Select (t => new { matching = t, method = this.TargetMethods.FirstOrDefault (m => m.ReflectedType == t.GameObject.GetType ()) })
				.Where (x => x.method != null)
				.Select(x => new TargetObjectMethodMatching(x.matching, x.method));
		}
	}
}

