using System;
using System.Collections.Generic;

namespace SuperMud.Engine
{
	public interface INamed : IGameObject
	{
		Description Description {
			get;
		}
	}
}

