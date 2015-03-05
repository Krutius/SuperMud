using System;
using System.Linq;
using System.Collections.Generic;
using SuperMud.Engine;

namespace SuperMud.Game
{
	public abstract class BasicMob : IGameObject
	{
		protected abstract IEnumerable<IGameObject> Drops {
			get;
		}

		[GameAction("greife*an")]
		public void Attack(Player p) {
			p.Inform ("Du attackierst " + this.GetIdentificationName ());
			p.CurrentEnviroment.Things.Remove (this);
			p.Inform ("Folgende Items werden fallengelassen:");
			p.Inform(String.Join("\n", this.Drops.Select(x => x.GetIdentificationName())));
			p.CurrentEnviroment.Things.AddRange (this.Drops);
		}
	}
}

