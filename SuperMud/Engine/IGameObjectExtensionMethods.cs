using System;
using System.Linq;

namespace SuperMud.Engine
{
	public static class IGameObjectExtensionMethods
	{
		public static String GetIdentificationName (this IGameObject obj) 
		{
			INamed named = obj as INamed;
			if (named != null && named.Description != null) {
				return ((INamed)obj).Description.FullText;
			} else {
				GameNameAttribute nameAttr = obj.GetType ().GetCustomAttributes (true)
					.FirstOrDefault (a => a is
						GameNameAttribute) as GameNameAttribute;

				if(nameAttr != null) {
					return nameAttr.Descripton.FullText;
				} else {
					throw new Exception(obj.GetType().Name + " hat keinen Namen!");
				}
			}
		}


		public static bool Identify (this IGameObject obj, Description what) 
		{
			if(what == null) {
				throw new Exception("das Search-pattern darf nicht leer sein.");
			}


			if (obj is INamed && ((INamed)obj).Description != null && ((INamed)obj).Description.isA (what)) {
				return true;
			}

			var nameAttr = obj.GetType ().GetCustomAttributes (true).FirstOrDefault (a => a is GameNameAttribute) as GameNameAttribute;
			if (nameAttr != null) {
				if (what.Attributes.Except (nameAttr.Descripton.Attributes).Any()) {
					return false;
				}

				if (nameAttr.Descripton.GramaticalGender != what.GramaticalGender) {
					return false;
				}

				if (nameAttr.Descripton.Name == what.Name) {
					return true;
				}
			}
				
			if (obj.GetType ().GetCustomAttributes (typeof(GameIdentificationAttribute), true)
				.Cast<GameIdentificationAttribute> ()
				.Any (a => a.Name.Equals(what.Name, StringComparison.CurrentCultureIgnoreCase))) {
				return true;
			}
			
			return false;
		}
	}
}