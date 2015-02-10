using System;

namespace SuperMud.Engine
{
	public interface IUserInterface
	{
		void InformUser(String message);
		String AskUser();
	}
}