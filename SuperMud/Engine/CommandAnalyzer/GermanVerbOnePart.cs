using System;
using System.Linq;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class GermanVerbOnePart : GermanVerb
	{
		public override bool SentenceContainsVerb (string sentence)
		{
			return sentence.StartsWith (this.Verb + " ") || sentence.Equals (this.Verb);
		}

		public override IGermanVerbTarget IdentifyTarget (string sentence)
		{
			if (!this.SentenceContainsVerb(sentence)) {
				throw new Exception("Der satz enthält das Verb nicht");
			}

			var splitted = sentence.TrimEnd(' ', '.', '!').Split (' ');
			if (Char.IsUpper (splitted.Last()[0])) {
				// ok, der letzte eintrag ist das nomen.
				// wir müssen nun alle modifier finden.
				return GermanVerbTargetNoun.BuildFromSentencePart (splitted.ToList ().GetRange (1, splitted.Length - 1));
			} else {
				// ok, spezialfall.
				// z.B. reflexiv (mich)
				if (splitted.Last().Equals("mich")) {
					return new GermanVerbTargetSelf ();
				}

				return null;
			}
		}

		private String Verb {
			get;
			set;
		}
		public GermanVerbOnePart (String verb)
		{
			this.Verb = verb.ToLower();
		}
	}
}

