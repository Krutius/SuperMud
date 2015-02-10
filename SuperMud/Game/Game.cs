using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	public static class Game
	{
		public static void Start(IUserInterface ui) {
			Player.Spawn (World.BootWorld(), ui);
		}
	}
}

