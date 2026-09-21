using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal partial class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				switch (msg.command)
				{
					case sbyte.MinValue:
					case sbyte.MaxValue:
					case 114:
					case 113:
					case 48:
					case 31:
					case -89:
					case 42:
					case 52:
					case 51:
					case -127:
					case -126:
					case -122:
					case 102:
					case 101:
					case -120:
					case -121:
					case 100:
					case -123:
					case -119:
					case -117:
					case -116:
					case -115:
					case -113:
					case -111:
					case 125:
					case 124:
					case 123:
					case 122:
					case 121:
						readMessagePart1(msg);
						break;
					case -124:
					case -125:
					case -110:
					case 93:
					case 98:
					case -106:
					case -105:
					case -103:
					case -102:
					case -101:
					case -100:
						readMessagePart2(msg);
						break;
				}
			}
			catch (Exception ex4)
			{
				Res.outz("=====> Controller2 " + ex4.StackTrace);
			}
		}
	}
}
