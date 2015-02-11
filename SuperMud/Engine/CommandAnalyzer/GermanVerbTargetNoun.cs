using System;
using System.Linq;
using System.Collections.Generic;

namespace SuperMud.Engine.CommandAnalyzer
{
	/// <summary>
	/// Ziel eines verbs in einem Satz.
	/// Also ein Nomen mit modifiern (adjektiven)
	/// </summary>
	public class GermanVerbTargetNoun : IGermanVerbTarget
	{
		public IEnumerable<TargetObjectMatching> FindMatchings (Player player)
		{
			if (!this.Gender.HasValue) {
				throw new NotImplementedException ("no unknown gender. yet");
			}

			Description d = new Description (this.Noun, this.Gender.Value, this.Adjectives);
			List<IGameObject> gameObjects = new List<IGameObject> ();
			gameObjects.Add (player);
			gameObjects.Add (player.CurrentEnviroment);
			gameObjects.AddRange (player.CurrentEnviroment.Things);
			gameObjects.AddRange (player.Inventory);
			return gameObjects.Where (o => o.Identify (d)).Select (o => new TargetObjectMatching (o, 10, o.GetIdentificationName()));
		}


		public List<String> Adjectives {
			get;
			private set;
		}

		public String Noun {
			get;
			private set;
		}

		public GramaticalGender? Gender {
			get;
			private set;
		}

		public static GermanVerbTargetNoun BuildFromSentencePart(IEnumerable<String> words) {
			var result = new GermanVerbTargetNoun ();
			result.BuildFromSentencePartInstance (words);
			return result;
		}

		private void BuildFromSentencePartInstance (IEnumerable<String> words) {
			this.ParseArticle (words.First ());
			if (words.Count() > 2) {
				this.Adjectives.AddRange(words.ToList ().GetRange (1, words.Count () - 2));
			}
			this.Noun = words.Last ();
		}

		private void ParseArticle(string article) {
			switch (article) {
			case "den":
				this.Gender = GramaticalGender.Male;
				return;
			case "die":
			case "meine":
				this.Gender = GramaticalGender.Female;
				return;
			case "das":
				this.Gender = GramaticalGender.Neuter;
				return;
			}
		}

		private GermanVerbTargetNoun () {
			this.Adjectives = new List<string> ();
		}
	}
}

