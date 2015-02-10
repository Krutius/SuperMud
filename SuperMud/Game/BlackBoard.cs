using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	[GameName("Schiefertafel", GramaticalGender.Female, "alte")]
	[GameIdentification("Tafel")]
	public class BlackBoard : IGameObject
	{
		public String Text {
			get;
			set;
		}


		[GameAction("untersuche")]
		[GameAction("betrachte" +
			"")]
		public void Investigate(Player p) {
			p.Inform ("Eine Schiefertafel mit Kreide. Du kannst auf sie schreiben.");
			if (String.IsNullOrEmpty (this.Text)) {
				p.Inform ("Aktuell ist die Tafel leer");
			} else {
				p.Inform ("Aktuell steht auf der Tafel:");
				p.Inform(this.Text);
			}
		}

		[GameAction("schreibe")]
		public void Write(Player p) {
			p.Inform("Was willst du auf die Tafel schreiben?");
			this.Text = p.Ask ();
		}
	}
}

