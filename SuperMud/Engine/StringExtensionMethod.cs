using System;

namespace SuperMud.Engine
{
	public static class StringExtensionMethod
	{
		public static String ReplaceSpecialSign (this String input)
		{
			return input
				.Replace ("ä", "ae")
				.Replace ("Ä", "AE")
				.Replace ("Ö", "OE")
				.Replace ("ö", "oe")
				.Replace ("Ü", "UE")
				.Replace ("ü", "ue");
		}
	}
}

