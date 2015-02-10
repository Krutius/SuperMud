using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	[GameName("Tür", GramaticalGender.Female)]
	public class BasicDoor : INamed
	{
		public Description Description { get; protected set; }

		[GameAction("untersuche")]
		public void Investigate (Player p)
		{
			p.Inform ("Eine einfache Tür. Du kannst sie durchqueren.");
		}

		public AEnviroment Enviroment1 {
			get;
			set;
		}

		public AEnviroment Enviroment2 {
			get;
			set;
		}

		private BasicDoor (AEnviroment env1, AEnviroment env2, Description description)
		{
			this.Enviroment1 = env1;
			this.Enviroment2 = env2;
			this.Description = description;
		}

		public static BasicDoor CreateDoor(AEnviroment env1, AEnviroment env2, Description name) {
			var door = new BasicDoor (env1, env2, name);
			env1.Things.Add (door);
			env2.Things.Add (door);
			return door;
		}

		public static BasicDoor CreateDoor(AEnviroment env1, AEnviroment env2) {
			return BasicDoor.CreateDoor (env1, env2, null);
		}

		[GameAction("benutze")]
		[GameAction("durchquere")]
		public void Use (Player p)
		{
			p.Inform ("Du durchquerst die Tür");
			if (p.CurrentEnviroment == Enviroment1) {
				p.CurrentEnviroment = Enviroment2;
			} else if (p.CurrentEnviroment == Enviroment2) {
				p.CurrentEnviroment = Enviroment1;
			} else {
				throw new Exception ("Du kannst die Türe nur benutzen wenn du auf einer der beiden Seiten bist");
			}
		}
	}
}

