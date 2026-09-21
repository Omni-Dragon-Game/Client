using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal partial class Controller2
	{
		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[b2];
					for (int i = 0; i < b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else if (b == 1)
				{
					sbyte b4 = msg.reader().readByte();
					short[] array2 = new short[b4];
					for (int j = 0; j < b4; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array2);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = msg.reader().readShort();
						int no = i + 1;
						int idIcon = msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						if (b2 == 0)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = msg.reader().readShort();
							int body = msg.reader().readShort();
							int leg = msg.reader().readShort();
							int bag = msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b4 != 0)
						{
							array = new ItemOption[b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = msg.reader().readUnsignedByte();
								int param = msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								if (num3 != -1)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						if (b3 > 0)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, num);
					RadarScr.gI().switchToMe();
				}
				else if (b == 1)
				{
					int id2 = msg.reader().readShort();
					sbyte use2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
					}
					RadarScr.SetListUse();
				}
				else if (b == 2)
				{
					int num4 = msg.reader().readShort();
					sbyte level = msg.reader().readByte();
					int num5 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num4)
							{
								info_RadaScr2.SetLevel(level);
							}
							if (info_RadaScr2.level > 0)
							{
								num5++;
							}
						}
					}
					RadarScr.SetNum(num5, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
					}
				}
				else if (b == 3)
				{
					int id3 = msg.reader().readShort();
					sbyte amount2 = msg.reader().readByte();
					sbyte max_amount2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
					}
				}
				else if (b == 4)
				{
					int num6 = msg.reader().readInt();
					short idAuraEff = msg.reader().readShort();
					Char @char = null;
					@char = ((num6 != Char.myCharz().charID) ? GameScr.findCharInMap(num6) : Char.myCharz());
					if (@char != null)
					{
						@char.idAuraEff = idAuraEff;
						@char.idEff_Set_Item = msg.reader().readByte();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte(); // on off
				int num = msg.reader().readInt(); // plId
				Char @char = null;
				@char = ((num != Char.myCharz().charID) ? GameScr.findCharInMap(num) : Char.myCharz());
				if (b == 0) // bat
				{
					int id = msg.reader().readShort(); // id eff
					int layer = msg.reader().readByte(); // 
					int loop = msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					@char?.addEffChar(new Effect(id, @char, layer, loop, loopCount, isStand));
				}
				else if (b == 1) // tat
				{
					int id2 = msg.reader().readShort();
					@char?.removeEffChar(0, id2);
				}
				else if (b == 2)
				{
					@char?.removeEffChar(-1, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				sbyte idBoss = msg.reader().readByte();
				NewBoss newBoss = Mob.getNewBoss(idBoss);
				if (newBoss == null)
				{
					return;
				}
				if (actionBoss == 10)
				{
					short xMoveTo = msg.reader().readShort();
					short yMoveTo = msg.reader().readShort();
					newBoss.move(xMoveTo, yMoveTo);
				}
				if (actionBoss >= 11 && actionBoss <= 20)
				{
					sbyte b = msg.reader().readByte();
					Char[] array = new Char[b];
					int[] array2 = new int[b];
					for (int i = 0; i < b; i++)
					{
						int num = msg.reader().readInt();
						array[i] = null;
						if (num != Char.myCharz().charID)
						{
							array[i] = GameScr.findCharInMap(num);
						}
						else
						{
							array[i] = Char.myCharz();
						}
						array2[i] = msg.reader().readInt();
					}
					sbyte dir = msg.reader().readByte();
					newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
				}
				if (actionBoss == 21)
				{
					newBoss.xTo = msg.reader().readShort();
					newBoss.yTo = msg.reader().readShort();
					newBoss.setFly();
				}
				if (actionBoss == 22)
				{
				}
				if (actionBoss == 23)
				{
					newBoss.setDie();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
