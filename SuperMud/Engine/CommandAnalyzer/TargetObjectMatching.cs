using System;
using System.Reflection;
using System.Linq;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class TargetObjectMatching
	{
		public IGameObject GameObject { get; private set; }

		public int Match { get; private set; }

		public String Description {
			get;
			set;
		}

		public TargetObjectMatching (IGameObject gameObject, int match, String description)
		{
			this.Description = description;
			this.GameObject = gameObject;
			this.Match = match;
		}

		public TargetObjectMatching (IGameObject gameObject, int match) : this(gameObject, match, null) {}
	}
}
