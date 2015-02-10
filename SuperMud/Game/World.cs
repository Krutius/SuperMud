using System;
using SuperMud.Engine;

namespace SuperMud.Game
{
	public static class World
	{
		public static AEnviroment BootWorld ()
		{
			var startRoom = new BasicRoom ("Der Raum ist verfallen und der Boden ist aus glitschigen Steinen.");
			var secondRoom = new BasicRoom ("Der Raum sieht aus als hätte ihn schon ewigs niemand mehr betreten");
			BasicDoor.CreateDoor (startRoom, secondRoom);

			return startRoom;
		}
	}
}

