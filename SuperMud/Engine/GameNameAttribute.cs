using System;
using System.Linq;
using System.Collections.Generic;

namespace SuperMud.Engine
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class GameNameAttribute : Attribute
	{
		public Description Descripton {
			get;
			protected set;
		}

		/// <summary>
		/// Initializes a new GameName
		/// </summary>
		/// <param name="name">Name. MUST be in Accusative form!</param>
		/// <param name="gramaticalGender">Gramatical gender, male or female</param>
		/// <param name="attributes">Attributes, so adjectives like "gross", "rot", "gefährlichen", everything in Accusative</param>
		public GameNameAttribute (String name, GramaticalGender gramaticalGender, params String[] attributes) 
			: this(new Description(name.ReplaceSpecialSign(), gramaticalGender, attributes.Select(s => s.ReplaceSpecialSign()))) {}

		public GameNameAttribute (Description description) {
			this.Descripton = description;
		}
	}
}

