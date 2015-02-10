using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

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

		private Player (AEnviroment enviroment, IUserInterface ui)
		{
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

		private List<IGameObject> GetCurrentTargets() {
			List<IGameObject> currentGameObjects = new List<IGameObject> ();
			currentGameObjects.Add (this);
			currentGameObjects.Add (this.CurrentEnviroment);
			currentGameObjects.AddRange (this.CurrentEnviroment.Things);
			return currentGameObjects;
		}

		private List<ExecutableItem> ExecutableItems = new List<ExecutableItem>();
		private void BuildCommandTargets() {
			List<IGameObject> currentGameObjects = GetCurrentTargets ();

			foreach (var obj in currentGameObjects) {
				// nur wenn nicht im cache
				if (this.ExecutableItems.All (x => x.Type != obj.GetType ())) {
					foreach (var method in obj.GetType().GetMethods()) {
						foreach (var attribute in method.GetCustomAttributes(typeof(GameActionAttribute), true).Cast<GameActionAttribute>()) {
							var newExecutable = new ExecutableItem () {
								Command = attribute.Command,
								Method = method,
								Type = obj.GetType ()
							};

							if (this.ExecutableItems.Any (x => x.Command == newExecutable.Command && x.Type == newExecutable.Type)) {
								throw new Exception ("Das müsste schon im cache sein! Möglicherweise zwei methoden mit dem gleichen GameAction attribut auf " + obj.GetType ());
							} else {
								this.ExecutableItems.Add (newExecutable);
							}
						}
					}
				}
			}
		}

		private bool DoCommand (string command)
		{
			command = command.ReplaceSpecialSign();

			this.BuildCommandTargets ();

			var executables = this.ExecutableItems.Where (x => command.StartsWith (x.Command, StringComparison.CurrentCultureIgnoreCase));

			var commandStrings = executables.Select (x => x.Command).Distinct ();
			if (commandStrings.Count() > 1) {
				throw new Exception (String.Join(", ", executables.Select (x => x.Command).Distinct ()) + " stehen im konflikt. unklar was gemeint ist mit " + command);
			}

			var commandVerb = commandStrings.FirstOrDefault ();
			if (commandVerb == null) {
				return false;
			}

			var targetObjects = this.GetCurrentTargets().Where (obj => executables.Any (e => e.Type.IsInstanceOfType(obj))).ToList();

			String regex = @"^" + command.Substring(0, commandVerb.Length) + " .*?(?<article>die|das|den)(?<adjectives>.*?) (?<noun>[A-Z]{1}[a-z]*?)$";
			var match = System.Text.RegularExpressions.Regex.Match (command, regex);

			if (!match.Success) {

				return false;
			}

			GramaticalGender g;
			switch (match.Groups ["article"].Value) {
				case "die":
					g = GramaticalGender.Female;
					break;
				case "den":
					g = GramaticalGender.Male;
					break;
				case "das":
					g = GramaticalGender.Neuter;
					break;
				default:
					throw new Exception ("Kein artikel im Satz gefunden?");
			}

			Description d = new Description (match.Groups ["noun"].Value, g, match.Groups ["adjectives"].Value.Split (new char[]{' '}, StringSplitOptions.RemoveEmptyEntries));


			var targets = targetObjects.Where (o => o.Identify (d)).ToArray();

			IGameObject target = null;
			if (!targets.Any()) {
				this.Inform ("Mit was soll ich das machen");
				return false;
			} else if (targets.Count() == 1) {
				target = targets[0];
			} else {
				this.Inform ("Bitte auswählen:");
				this.Inform(String.Join("\n", targetObjects.Select ((o, i) => String.Format ("[{0}] - {1}", i, o.GetIdentificationName ()))));
				int idx = 0;
				do {
					int.TryParse(this.Ask(), out idx);
				} while(!(idx >= 0 && idx < targets.Count()));
				target = targets[idx];
			}

			executables.FirstOrDefault(x => x.Type.IsInstanceOfType(target)).Method.Invoke(target, new object[] { this });

			return true;
		}

		private String cleanTarget (String input)
		{
			string start = (new String[] {
				"der ",
				"die ",
				"das ",
				"den "
			}).FirstOrDefault (x => input.StartsWith (x, StringComparison.CurrentCultureIgnoreCase));

			if (!String.IsNullOrEmpty (start)) {
				return input.Substring (start.Length);
			} else {
				return input;
			}
		}

		public static void Spawn (AEnviroment enviroment, IUserInterface ui)
		{
			Player p = new Player (enviroment, ui);
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
	}
}