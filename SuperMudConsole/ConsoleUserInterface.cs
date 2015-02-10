using System;

namespace SuperMudConsole
{
	public class ConsoleUserInterface: SuperMud.Engine.IUserInterface
	{
		public void InformUser(String message) {
			Console.WriteLine (message);
		}
		public String AskUser() {
			return Console.ReadLine();
		}
	}
}

