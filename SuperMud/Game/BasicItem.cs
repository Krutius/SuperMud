using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	public abstract class BasicItem : IGameObject, INamed
	{
		protected BasicItem (Description d)
		{
			this.Description = d;
		}

		[GameAction("nehme*auf")]
		public void Take (Player p)
		{
			p.Inform ("Du nimmst " + this.Description.FullText + " auf.");
			p.CurrentEnviroment.Things.Remove (this);
			p.Inventory.Add (this);
		}

		[GameAction("lege*ab")]
		public void ThrowAway (Player p)
		{
			p.Inform ("Du legst " + this.GetIdentificationName() + " ab.");
			p.Inventory.Remove (this);
			p.CurrentEnviroment.Things.Add (this);
		}

		public Description Description {
			get;
			private set;
		}
	}
}

