using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	[GameName("Wasserflasche", GramaticalGender.Female)]
	public class BottleOfWater : IGameObject
	{
		// Instanzvariablen
		public Boolean IsFull;

		// Methoden
		[GameAction("trinke*aus")]
		public void DrinkOut(Player p) {
			if (this.IsFull) {
				p.Inform ("Du trinkst die Wasserflasche aus");
				this.IsFull = false;
			} else {
				p.Inform ("Die flasche ist bereits leer!");
			}
		}

		[GameAction("untersuche")]
		public void Investigate(Player p) {
			if (this.IsFull) {
				p.Inform ("Die flasche ist voll");
			} else {
				p.Inform ("Die flasche ist leer!");
			}
		}
	}
}


