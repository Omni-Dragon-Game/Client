using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal partial class Controller2
	{
		private static void readMessagePart1(Message msg)
		{
			switch (msg.command)
			{
					case sbyte.MinValue:
						readInfoEffChar(msg);
						break;
					case sbyte.MaxValue:
						readInfoRada(msg);
						break;
					case 114:
						try
						{
							string text2 = msg.reader().readUTF();
							mSystem.curINAPP = msg.reader().readByte();
							mSystem.maxINAPP = msg.reader().readByte();
							break;
						}
						catch (Exception)
						{
							break;
						}
					case 113:
						{
							int loop = 0;
							int layer = 0;
							int id4 = 0;
							short x2 = 0;
							short y2 = 0;
							short loopCount = -1;
							try
							{
								loop = msg.reader().readByte();
								layer = msg.reader().readByte();
								id4 = msg.reader().readUnsignedByte();
								x2 = msg.reader().readShort();
								y2 = msg.reader().readShort();
								loopCount = msg.reader().readShort();
							}
							catch (Exception)
							{
							}
							EffecMn.addEff(new Effect(id4, x2, y2, layer, loop, loopCount));
							break;
						}
					case 48:
						{
							sbyte b10 = msg.reader().readByte();
							ServerListScreen.ipSelect = b10;
							GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
							Session_ME.gI().close();
							GameCanvas.endDlg();
							ServerListScreen.waitToLogin = true;
							break;
						}
					case 31:
						{
							int num15 = msg.reader().readInt();
							sbyte b12 = msg.reader().readByte();
							if (b12 == 1)
							{
								short smallID = msg.reader().readShort();
								sbyte b13 = -1;
								int[] array3 = null;
								short wimg = 0;
								short himg = 0;
								try
								{
									b13 = msg.reader().readByte();
									if (b13 > 0)
									{
										sbyte b14 = msg.reader().readByte();
										array3 = new int[b14];
										for (int m = 0; m < b14; m++)
										{
											array3[m] = msg.reader().readByte();
										}
										wimg = msg.reader().readShort();
										himg = msg.reader().readShort();
									}
								}
								catch (Exception)
								{
								}
								if (num15 == Char.myCharz().charID)
								{
									Char.myCharz().petFollow = new PetFollow();
									Char.myCharz().petFollow.smallID = smallID;
									if (b13 > 0)
									{
										Char.myCharz().petFollow.SetImg(b13, array3, wimg, himg);
									}
									break;
								}
								Char char3 = GameScr.findCharInMap(num15);
								char3.petFollow = new PetFollow();
								char3.petFollow.smallID = smallID;
								if (b13 > 0)
								{
									char3.petFollow.SetImg(b13, array3, wimg, himg);
								}
							}
							else if (num15 == Char.myCharz().charID)
							{
								Char.myCharz().petFollow.remove();
								Char.myCharz().petFollow = null;
							}
							else
							{
								Char char4 = GameScr.findCharInMap(num15);
								char4.petFollow.remove();
								char4.petFollow = null;
							}
							break;
						}
					case -89:
						GameCanvas.open3Hour = msg.reader().readByte() == 1;
						break;
					case 42:
						{
							GameCanvas.endDlg();
							LoginScr.isContinueToLogin = false;
							Char.isLoadingMap = false;
							sbyte haveName = msg.reader().readByte();
							if (GameCanvas.registerScr == null)
							{
								GameCanvas.registerScr = new RegisterScreen();
							}
							GameCanvas.registerScr.switchToMe();
							break;
						}
					case 52:
						{
							sbyte b8 = msg.reader().readByte();
							if (b8 == 1)
							{
								int num11 = msg.reader().readInt();
								if (num11 == Char.myCharz().charID)
								{
									Char.myCharz().setMabuHold(m: true);
									Char.myCharz().cx = msg.reader().readShort();
									Char.myCharz().cy = msg.reader().readShort();
								}
								else
								{
									Char char2 = GameScr.findCharInMap(num11);
									if (char2 != null)
									{
										char2.setMabuHold(m: true);
										char2.cx = msg.reader().readShort();
										char2.cy = msg.reader().readShort();
									}
								}
							}
							if (b8 == 0)
							{
								int num12 = msg.reader().readInt();
								if (num12 == Char.myCharz().charID)
								{
									Char.myCharz().setMabuHold(m: false);
								}
								else
								{
									GameScr.findCharInMap(num12)?.setMabuHold(m: false);
								}
							}
							if (b8 == 2)
							{
								int charId = msg.reader().readInt();
								int id = msg.reader().readInt();
								Mabu mabu = (Mabu)GameScr.findCharInMap(charId);
								mabu.eat(id);
							}
							if (b8 == 3)
							{
								GameScr.mabuPercent = msg.reader().readByte();
							}
							break;
						}
					case 51:
						{
							int charId2 = msg.reader().readInt();
							Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId2);
							sbyte id2 = msg.reader().readByte();
							short x = msg.reader().readShort();
							short y = msg.reader().readShort();
							sbyte b9 = msg.reader().readByte();
							Char[] array = new Char[b9];
							int[] array2 = new int[b9];
							for (int k = 0; k < b9; k++)
							{
								int num13 = msg.reader().readInt();
								Res.outz("char ID=" + num13);
								array[k] = null;
								if (num13 != Char.myCharz().charID)
								{
									array[k] = GameScr.findCharInMap(num13);
								}
								else
								{
									array[k] = Char.myCharz();
								}
								array2[k] = msg.reader().readInt();
							}
							mabu2.setSkill(id2, x, y, array, array2);
							break;
						}
					case -127:
						readLuckyRound(msg);
						break;
					case -126:
						{
							sbyte type = msg.reader().readByte();
							Res.outz("type quay= " + type);
							if (type == 1)
							{
								sbyte b27 = msg.reader().readByte();
								string num38 = msg.reader().readUTF();
								string finish = msg.reader().readUTF();
								GameScr.gI().showWinNumber(num38, finish);
							}
							if (type == 0)
							{
								GameScr.gI().showYourNumber(msg.reader().readUTF());
							}
							break;
						}
					case -122:
						{
							short id3 = msg.reader().readShort();
							Npc npc = GameScr.findNPCInMap(id3);
							sbyte b20 = msg.reader().readByte();
							npc.duahau = new int[b20];
							for (int num25 = 0; num25 < b20; num25++)
							{
								npc.duahau[num25] = msg.reader().readShort();
							}
							npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
							break;
						}
					case 102:
						{
							sbyte b21 = msg.reader().readByte();
							if (b21 == 0 || b21 == 1 || b21 == 2 || b21 == 6)
							{
								BigBoss2 bigBoss = Mob.getBigBoss2();
								if (bigBoss == null)
								{
									break;
								}
								if (b21 == 6)
								{
									bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
									break;
								}
								sbyte b22 = msg.reader().readByte();
								Char[] array4 = new Char[b22];
								int[] array5 = new int[b22];
								for (int num28 = 0; num28 < b22; num28++)
								{
									int num29 = msg.reader().readInt();
									array4[num28] = null;
									if (num29 != Char.myCharz().charID)
									{
										array4[num28] = GameScr.findCharInMap(num29);
									}
									else
									{
										array4[num28] = Char.myCharz();
									}
									array5[num28] = msg.reader().readInt();
								}
								bigBoss.setAttack(array4, array5, b21);
							}
							if (b21 == 3 || b21 == 4 || b21 == 5 || b21 == 7)
							{
								BachTuoc bachTuoc = Mob.getBachTuoc();
								if (bachTuoc == null)
								{
									break;
								}
								if (b21 == 7)
								{
									bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
									break;
								}
								if (b21 == 3 || b21 == 4)
								{
									sbyte b23 = msg.reader().readByte();
									Char[] array6 = new Char[b23];
									int[] array7 = new int[b23];
									for (int num30 = 0; num30 < b23; num30++)
									{
										int num31 = msg.reader().readInt();
										array6[num30] = null;
										if (num31 != Char.myCharz().charID)
										{
											array6[num30] = GameScr.findCharInMap(num31);
										}
										else
										{
											array6[num30] = Char.myCharz();
										}
										array7[num30] = msg.reader().readInt();
									}
									bachTuoc.setAttack(array6, array7, b21);
								}
								if (b21 == 5)
								{
									short xMoveTo = msg.reader().readShort();
									bachTuoc.move(xMoveTo);
								}
							}
							if (b21 > 9 && b21 < 30)
							{
								readActionBoss(msg, b21);
							}
							break;
						}
					case 101:
						{
							Res.outz("big boss--------------------------------------------------");
							BigBoss bigBoss2 = Mob.getBigBoss();
							if (bigBoss2 == null)
							{
								break;
							}
							sbyte b24 = msg.reader().readByte();
							if (b24 == 0 || b24 == 1 || b24 == 2 || b24 == 4 || b24 == 3)
							{
								if (b24 == 3)
								{
									bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
									bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
									bigBoss2.setFly();
								}
								else
								{
									sbyte b25 = msg.reader().readByte();
									Res.outz("CHUONG nChar= " + b25);
									Char[] array8 = new Char[b25];
									int[] array9 = new int[b25];
									for (int num32 = 0; num32 < b25; num32++)
									{
										int num33 = msg.reader().readInt();
										Res.outz("char ID=" + num33);
										array8[num32] = null;
										if (num33 != Char.myCharz().charID)
										{
											array8[num32] = GameScr.findCharInMap(num33);
										}
										else
										{
											array8[num32] = Char.myCharz();
										}
										array9[num32] = msg.reader().readInt();
									}
									bigBoss2.setAttack(array8, array9, b24);
								}
							}
							if (b24 == 5)
							{
								bigBoss2.haftBody = true;
								bigBoss2.status = 2;
							}
							if (b24 == 6)
							{
								bigBoss2.getDataB2();
								bigBoss2.x = msg.reader().readShort();
								bigBoss2.y = msg.reader().readShort();
							}
							if (b24 == 7)
							{
								bigBoss2.setAttack(null, null, b24);
							}
							if (b24 == 8)
							{
								bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
								bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
								bigBoss2.status = 2;
							}
							if (b24 == 9)
							{
								bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							}
							break;
						}
					case -120:
						{
							long num24 = mSystem.currentTimeMillis();
							Service.logController = num24 - Service.curCheckController;
							Service.gI().sendCheckController();
							break;
						}
					case -121:
						{
							long num27 = mSystem.currentTimeMillis();
							Service.logMap = num27 - Service.curCheckMap;
							Service.gI().sendCheckMap();
							break;
						}
					case 100:
						{
							sbyte b29 = msg.reader().readByte();
							sbyte b30 = msg.reader().readByte();
							Item item2 = null;
							if (b29 == 0)
							{
								item2 = Char.myCharz().arrItemBody[b30];
							}
							if (b29 == 1)
							{
								item2 = Char.myCharz().arrItemBag[b30];
							}
							short num42 = msg.reader().readShort();
							if (num42 == -1)
							{
								break;
							}
							item2.template = ItemTemplates.get(num42);
							item2.quantity = msg.reader().readInt();
							item2.info = msg.reader().readUTF();
							item2.content = msg.reader().readUTF();
							sbyte b31 = msg.reader().readByte();
							if (b31 == 0)
							{
								break;
							}
							item2.itemOption = new ItemOption[b31];
							for (int num43 = 0; num43 < item2.itemOption.Length; num43++)
							{
								int num44 = msg.reader().readUnsignedByte();
								Res.outz("id o= " + num44);
								int param3 = msg.reader().readUnsignedShort();
								if (num44 != -1)
								{
									item2.itemOption[num43] = new ItemOption(num44, param3);
								}
							}
							break;
						}
					case -123:
						{
							int charId3 = msg.reader().readInt();
							if (GameScr.findCharInMap(charId3) != null)
							{
								GameScr.findCharInMap(charId3).perCentMp = msg.reader().readByte();
							}
							break;
						}
					case -119:
						Char.myCharz().rank = msg.reader().readInt();
						break;
					case -117:
						GameScr.gI().tMabuEff = 0;
						GameScr.gI().percentMabu = msg.reader().readByte();
						if (GameScr.gI().percentMabu == 100)
						{
							GameScr.gI().mabuEff = true;
						}
						if (GameScr.gI().percentMabu == 101)
						{
							Npc.mabuEff = true;
						}
						break;
					case -116:
						GameScr.canAutoPlay = msg.reader().readByte() == 1;
						break;
					case -115:
						Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
						break;
					case -113:
						{
							sbyte[] array10 = new sbyte[10];
							for (int num35 = 0; num35 < 10; num35++)
							{
								array10[num35] = msg.reader().readByte();
								Res.outz("vlue i= " + array10[num35]);
							}
							GameScr.gI().onKSkill(array10);
							GameScr.gI().onOSkill(array10);
							GameScr.gI().onCSkill(array10);
							break;
						}
					case -111:
						{
							short num14 = msg.reader().readShort();
							ImageSource.vSource = new MyVector();
							for (int l = 0; l < num14; l++)
							{
								string iD = msg.reader().readUTF();
								sbyte version = msg.reader().readByte();
								ImageSource.vSource.addElement(new ImageSource(iD, version));
							}
							ImageSource.checkRMS();
							ImageSource.saveRMS();
							break;
						}
					case 125:
						{
							sbyte fusion = msg.reader().readByte();
							int num16 = msg.reader().readInt();
							if (num16 == Char.myCharz().charID)
							{
								Char.myCharz().setFusion(fusion);
							}
							else if (GameScr.findCharInMap(num16) != null)
							{
								GameScr.findCharInMap(num16).setFusion(fusion);
							}
							break;
						}
					case 124:
						{
							short num26 = msg.reader().readShort();
							string text3 = msg.reader().readUTF();
							Res.outz("noi chuyen = " + text3 + "npc ID= " + num26);
							GameScr.findNPCInMap(num26)?.addInfo(text3);
							break;
						}
					case 123:
						{
							Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
							int num10 = msg.reader().readInt();
							short xPos = msg.reader().readShort();
							short yPos = msg.reader().readShort();
							sbyte b7 = msg.reader().readByte();
							Char @char = null;
							if (num10 == Char.myCharz().charID)
							{
								@char = Char.myCharz();
							}
							else if (GameScr.findCharInMap(num10) != null)
							{
								@char = GameScr.findCharInMap(num10);
							}
							if (@char != null)
							{
								ServerEffect.addServerEffect((b7 != 0) ? 173 : 60, @char, 1);
								@char.setPos(xPos, yPos, b7);
							}
							break;
						}
					case 122:
						{
							short num34 = msg.reader().readShort();
							Res.outz("second login = " + num34);
							LoginScr.timeLogin = num34;
							LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
							GameCanvas.endDlg();
							break;
						}
					case 121:
						mSystem.publicID = msg.reader().readUTF();
						mSystem.strAdmob = msg.reader().readUTF();
						Res.outz("SHOW AD public ID= " + mSystem.publicID);
						mSystem.createAdmob();
						break;
			}
		}
	}
}
