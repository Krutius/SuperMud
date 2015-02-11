using System;

namespace SuperMud.Engine.CommandAnalyzer
{
	public abstract class GermanVerb
	{
		public abstract bool SentenceContainsVerb (string sentence);
		public abstract IGermanVerbTarget IdentifyTarget (string sentence);

		public static GermanVerb Build(string verbPattern) {
			if (verbPattern.Contains ("*")) {
				var splitted = verbPattern.Split ('*');
				return new GermanVerbTwoParts (splitted [0], splitted [1]);
			} else {
				return new GermanVerbOnePart (verbPattern);
			}
		}
	}
}

