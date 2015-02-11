using System;
using System.Linq;
using System.Collections.Generic;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class GermanVerbTargetSelf : IGermanVerbTarget
	{
		public IEnumerable<TargetObjectMatching> FindMatchings (Player player)
		{
			return new List<TargetObjectMatching> () {
				new TargetObjectMatching (player, 10)
			};
		}
	}
}

