using System;
using System.Collections.Generic;
using SuperMud.Engine;

namespace SuperMud.Game
{
	[GameName("Löwen", GramaticalGender.Male)]
	public class Lion : BasicMob
	{
		protected override IEnumerable<IGameObject> Drops {
			get {
				yield return new Stone (new Description("Stein", GramaticalGender.Male, "grauer"));
			}
		}
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

