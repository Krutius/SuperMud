using System;
using System.Collections.Generic;

namespace SuperMud.Engine.CommandAnalyzer
{
	public interface IGermanVerbTarget
	{

		IEnumerable<TargetObjectMatching> FindMatchings (Player player);
	}
}

