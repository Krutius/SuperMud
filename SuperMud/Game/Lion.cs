using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	[GameName("Löwen", GramaticalGender.Male)]
	public class Lion : BasicItem
	{
		public Lion (): base(null) {}

		public bool Gefangen {
			get;
			private set;
		}

		[GameAction("fange")]
		public void Catch(Player p) {
			p.Inform ("Du hast einen Löwen gefangen!");
			p.CurrentEnviroment.Things.Remove (this);
			p.Inventory.Add (this);
			this.Gefangen = true;
		}
	}
}

