using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SuperMud.Engine.CommandAnalyzer;

namespace SuperMud.Engine
{
	[GameName("Spieler", GramaticalGender.Male)]
	public class Player : IGameObject
	{
		private IUserInterface UserInterface {
			get;
			set;
		}

		private AEnviroment _AEnviroment;

		public AEnviroment CurrentEnviroment {
			get {
				return this._AEnviroment;
			}
			set {
				if (value == null) {
					throw new Exception ("Das Enviroment darf nicht auf null gesetzt werden");
				}
				this._AEnviroment = value;
			}
		}

		public List<IGameObject> Inventory {
			get;
			private set;
		}

		private Player (AEnviroment enviroment, IUserInterface ui, Analyzer analyzer)
		{
			this.CommandAnalyzer = analyzer;

			this.Inventory = new List<IGameObject> ();
			this.CurrentEnviroment = enviroment;
			this.UserInterface = ui;
		}

		private void Spawn ()
		{
			this.UserInterface.InformUser ("Dein Avatar taucht in der Spielwelt auf und ist noch leicht benommen.");
			this.UserInterface.InformUser ("Hallo, ich bin dein Avatar. Ich werde für dich mit der Spielwelt interagieren.");
			this.UserInterface.InformUser ("Was soll ich tun?");

			String message = this.UserInterface.AskUser ();
			while (!message.Equals ("verschwinde", StringComparison.CurrentCultureIgnoreCase)) {

				if (!String.IsNullOrEmpty (message)) {
					if (DoCommand (message))
					{
						this.UserInterface.InformUser ("Was soll ich als nächstes tun?");
					} else {
						this.UserInterface.InformUser ("Ich weiss nicht was du von mir willst. Was soll ich tun?");
					}

					message = this.UserInterface.AskUser ();
				}
			}
		}

		private CommandAnalyzer.Analyzer CommandAnalyzer {
			get;
			set;
		}

		private bool DoCommand(String command) {
			if (command.ToCharArray ().All (Char.IsLower)) {
				return false;
			}

			var matchings = this.CommandAnalyzer.FindMethodMatchings (command, this).OrderBy(m => m.ObjectMatching.Match);

			if (matchings == null || !matchings.Any ()) {
				return false;
			}

			if (matchings.Count () == 1 && matchings.First ().ObjectMatching.Match > 5) {
				matchings.First ().Execute (this);
			} else if (matchings.Count (m => m.ObjectMatching.Match > matchings.First ().ObjectMatching.Match - 5) == 1) {
				matchings.First ().Execute (this);
			} else {
				this.Inform ("Bitte auswählen:");
				this.Inform (String.Join ("\n", matchings.Select ((o, i) => String.Format ("[{0}] - {1}", i, o.ObjectMatching.Description))));
				int idx = 0;
				do {
					int.TryParse (this.Ask (), out idx);
				} while(!(idx >= 0 && idx < matchings.Count ()));
				matchings.ElementAt (idx).Execute (this);
			}
			return true;
		}

		public static void Spawn (AEnviroment enviroment, IUserInterface ui, params Assembly[] assemblies)
		{
			Player p = new Player (enviroment, ui, new Analyzer(assemblies));
			p.Spawn ();
		}

		public void Inform (String about)
		{
			this.UserInterface.InformUser (about);
		}

		public String Ask ()
		{
			return this.UserInterface.AskUser ();
		}

		[GameAction("untersuche")]
		public void Investigate(Player p) {
			p.Inform ("Du hast folgende sachen dabei:");
			p.Inform (String.Join("\n", this.Inventory.Select(x => x.GetIdentificationName())));
		}
	}
}