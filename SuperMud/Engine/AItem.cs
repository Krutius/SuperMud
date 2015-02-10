using System;
using System.Collections.Generic;

namespace SuperMud.Engine
{
	public abstract class AItem : IGameObject, INamed
	{
		public Description Description {
			get;
			private set;
		}

		protected AItem (Description desc)
		{
			this.Description = desc;
		}
	}
}

