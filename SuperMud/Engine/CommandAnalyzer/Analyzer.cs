using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SuperMud.Engine.CommandAnalyzer
{
	public class Analyzer
	{
		private Dictionary<GermanVerb, List<MethodInfo>> PossibleTargets { get; set; }

		public Analyzer (params Assembly[] assembliesToAnalyze)
		{
			this.PossibleTargets = new Dictionary<GermanVerb, List<MethodInfo>> ();

			assembliesToAnalyze.SelectMany (a => a.GetTypes ())
				.SelectMany (t => t.GetMethods ())
				.SelectMany (m => m.GetCustomAttributes<GameActionAttribute> (true)
					.Select (a => new {
						Command = a.Command,
						Method = m
					}))
				.GroupBy (x => x.Command)
				.ToList ()
				.ForEach (x => this.PossibleTargets.Add (GermanVerb.Build(x.Key), x.Select(m => m.Method).ToList ()));
		}

		private CommandTarget Analyze(String sentence) {
			String lowerSentence = sentence.ToLower ();
			var target = this.PossibleTargets.FirstOrDefault (t => t.Key.SentenceContainsVerb (lowerSentence));
			if (target.Key == null) {
				return null;
			}
			return new CommandTarget (target.Key.IdentifyTarget (sentence), target.Value);
		}

		public IEnumerable<TargetObjectMethodMatching> FindMethodMatchings(String sentence, Player p) {
			sentence = sentence.ReplaceSpecialSign();
			CommandTarget t = this.Analyze (sentence);
			if (t == null) {
				return new List<TargetObjectMethodMatching>();
			}
			return t.FindMatchings (p);

		}
	}
}

