using System;
using System.Linq;
using System.Collections.Generic;
using SuperMud.Engine;

namespace SuperMud.Game
{
	public class BasicRoom : AEnviroment
	{
		public List<String> Descriptions {
			get;
			private set;
		}

		public BasicRoom (params String[] desciptions)
		{
			this.Descriptions = new List<String> ();
			this.Descriptions.AddRange (desciptions);
		}

		public override void Investigate (Player p)
		{
			p.Inform ("Du befindest dich in einem Raum.");
			this.Descriptions.ForEach (p.Inform);
			this.InformAboutThings (p);
		}
	}
}

