using System;
using System.Linq;
using System.Collections.Generic;

namespace SuperMud.Engine
{
	public class Description : Attribute
	{
		public String Name {
			get;
			private set;
		}

		public List<String> Attributes {
			get;
			private set;
		}

		public GramaticalGender GramaticalGender {
			get;
			private set;
		}

		public String FullText {
			get {
				if (this.GramaticalGender == GramaticalGender.Female) {
					return "eine " + String.Join (" ", this.Attributes) + this.Name;
				} else {
					return "ein " + String.Join (" ", this.Attributes) + this.Name;
				}
			}
		}


		public Description(String name, GramaticalGender gramaticalGender, IEnumerable<String> attributes)
			: this(name, gramaticalGender, attributes.ToArray()) {}

		public Description (String name, GramaticalGender gramaticalGender, params String[] attributes) {
			this.Name = name;
			this.GramaticalGender = gramaticalGender;
			this.Attributes = new List<String> ();
			this.Attributes.AddRange (attributes);
		}

		public bool isA(Description compareTo) {
			if (compareTo.Name != this.Name) {
				return false;
			}

			if (compareTo.GramaticalGender != this.GramaticalGender) {
				return false;
			}

			if (compareTo.Attributes.Except (this.Attributes).Any ()) {
				return false;
			}

			return true;
		}
	}
}

