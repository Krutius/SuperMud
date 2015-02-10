using System;
using System.Linq;
using System.Collections.Generic;
using SuperMud.Engine;

namespace SuperMud.Engine
{
	[GameIdentificationAttribute("Umgebung")]
	public abstract class AEnviroment : IGameObject
	{
		public List<IGameObject> Things {
			get;
			private set;
		}

		protected AEnviroment ()
		{
			this.Things = new List<IGameObject> ();
		}

		[GameActionAttribute("untersuche")]
		public abstract void Investigate (Player p);

		protected void InformAboutThings(Player p) {
			String result = "In deiner Umgebung befinden sich folgende Sachen:";
			this.Things.ForEach (t => result += "\n"+t.GetIdentificationName());
			p.Inform (result);
		}
	}
}