using System;
using System.Linq;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class GermanVerbTwoParts : GermanVerb
	{
		public override IGermanVerbTarget IdentifyTarget (string sentence)
		{
			if (!this.SentenceContainsVerb(sentence)) {
				throw new Exception("Der satz enthält das Verb nicht");
			}

			var splitted = sentence.TrimEnd(' ', '.', '!').Split (' ');
			if (Char.IsUpper (splitted[splitted.Length-2
			][0])) {
				// ok, der letzte eintrag ist das nomen.
				// wir müssen nun alle modifier finden.
				return GermanVerbTargetNoun.BuildFromSentencePart (splitted.ToList ().GetRange (1, splitted.Length - 2));
			} else {
				// ok, spezialfall.
				// z.B. reflexiv (mich)
				throw new NotImplementedException ("muss ich noch machen");
			}
		}

		public override bool SentenceContainsVerb (string sentence)
		{
			var splitted = sentence.TrimEnd('.', '!', ' ').Split (' ');
			var first = splitted [0].ToLower ();
			var last = splitted [splitted.Length - 1].ToLower ();
			return first == this.Prefix && last == this.Suffix;
		}

		private String Prefix {
			get;
			set;
		}

		private String Suffix {
			get;
			set;
		}

		public GermanVerbTwoParts (String prefix, String suffix)
		{
			this.Prefix = prefix.ToLower();
			this.Suffix = suffix.ToLower();
		}
	}
}

