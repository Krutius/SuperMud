using System;
using SuperMud.Game;

namespace SuperMudConsole
{
	public static class Application
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Startup...");
			Game.Start (new ConsoleUserInterface ());
		}
	}
}
