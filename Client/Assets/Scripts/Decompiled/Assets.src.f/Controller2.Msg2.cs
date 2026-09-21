using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal partial class Controller2
	{
		private static void readMessagePart2(Message msg)
		{
			switch (msg.command)
			{
					case -124:
						{
							sbyte b4 = msg.reader().readByte();
							sbyte b5 = msg.reader().readByte();
							if (b5 == 0)
							{
								if (b4 == 2)
								{
									int num4 = msg.reader().readInt();
									if (num4 == Char.myCharz().charID)
									{
										Char.myCharz().removeEffect();
									}
									else if (GameScr.findCharInMap(num4) != null)
									{
										GameScr.findCharInMap(num4).removeEffect();
									}
								}
								int num5 = msg.reader().readUnsignedByte();
								int num6 = msg.reader().readInt();
								if (num5 == 32)
								{
									if (b4 == 1)
									{
										int num7 = msg.reader().readInt();
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().holdEffID = num5;
											GameScr.findCharInMap(num7).setHoldChar(Char.myCharz());
										}
										else if (GameScr.findCharInMap(num6) != null && num7 != Char.myCharz().charID)
										{
											GameScr.findCharInMap(num6).holdEffID = num5;
											GameScr.findCharInMap(num7).setHoldChar(GameScr.findCharInMap(num6));
										}
										else if (GameScr.findCharInMap(num6) != null && num7 == Char.myCharz().charID)
										{
											GameScr.findCharInMap(num6).holdEffID = num5;
											Char.myCharz().setHoldChar(GameScr.findCharInMap(num6));
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().removeHoleEff();
									}
									else if (GameScr.findCharInMap(num6) != null)
									{
										GameScr.findCharInMap(num6).removeHoleEff();
									}
								}
								if (num5 == 33)
								{
									if (b4 == 1)
									{
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().protectEff = true;
										}
										else if (GameScr.findCharInMap(num6) != null)
										{
											GameScr.findCharInMap(num6).protectEff = true;
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().removeProtectEff();
									}
									else if (GameScr.findCharInMap(num6) != null)
									{
										GameScr.findCharInMap(num6).removeProtectEff();
									}
								}
								if (num5 == 39)
								{
									if (b4 == 1)
									{
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().huytSao = true;
										}
										else if (GameScr.findCharInMap(num6) != null)
										{
											GameScr.findCharInMap(num6).huytSao = true;
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().removeHuytSao();
									}
									else if (GameScr.findCharInMap(num6) != null)
									{
										GameScr.findCharInMap(num6).removeHuytSao();
									}
								}
								if (num5 == 40)
								{
									if (b4 == 1)
									{
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().blindEff = true;
										}
										else if (GameScr.findCharInMap(num6) != null)
										{
											GameScr.findCharInMap(num6).blindEff = true;
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().removeBlindEff();
									}
									else if (GameScr.findCharInMap(num6) != null)
									{
										GameScr.findCharInMap(num6).removeBlindEff();
									}
								}
								if (num5 == 41)
								{
									if (b4 == 1)
									{
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().sleepEff = true;
										}
										else if (GameScr.findCharInMap(num6) != null)
										{
											GameScr.findCharInMap(num6).sleepEff = true;
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().removeSleepEff();
									}
									else if (GameScr.findCharInMap(num6) != null)
									{
										GameScr.findCharInMap(num6).removeSleepEff();
									}
								}
								if (num5 == 42)
								{
									if (b4 == 1)
									{
										if (num6 == Char.myCharz().charID)
										{
											Char.myCharz().stone = true;
										}
									}
									else if (num6 == Char.myCharz().charID)
									{
										Char.myCharz().stone = false;
									}
								}
							}
							if (b5 != 1)
							{
								break;
							}
							int num8 = msg.reader().readUnsignedByte();
							sbyte b6 = msg.reader().readByte();
							Res.outz("modbHoldID= " + b6 + " skillID= " + num8 + "eff ID= " + b4);
							if (num8 == 32)
							{
								if (b4 == 1)
								{
									int num9 = msg.reader().readInt();
									if (num9 == Char.myCharz().charID)
									{
										GameScr.findMobInMap(b6).holdEffID = num8;
										Char.myCharz().setHoldMob(GameScr.findMobInMap(b6));
									}
									else if (GameScr.findCharInMap(num9) != null)
									{
										GameScr.findMobInMap(b6).holdEffID = num8;
										GameScr.findCharInMap(num9).setHoldMob(GameScr.findMobInMap(b6));
									}
								}
								else
								{
									GameScr.findMobInMap(b6).removeHoldEff();
								}
							}
							if (num8 == 40)
							{
								if (b4 == 1)
								{
									GameScr.findMobInMap(b6).blindEff = true;
								}
								else
								{
									GameScr.findMobInMap(b6).removeBlindEff();
								}
							}
							if (num8 == 41)
							{
								if (b4 == 1)
								{
									GameScr.findMobInMap(b6).sleepEff = true;
								}
								else
								{
									GameScr.findMobInMap(b6).removeSleepEff();
								}
							}
							break;
						}
					case -125:
						{
							ChatTextField.gI().isShow = false;
							string text4 = msg.reader().readUTF();
							Res.outz("titile= " + text4);
							sbyte b32 = msg.reader().readByte();
							ClientInput.gI().setInput(b32, text4);
							for (int num45 = 0; num45 < b32; num45++)
							{
								ClientInput.gI().tf[num45].name = msg.reader().readUTF();
								sbyte b33 = msg.reader().readByte();
								if (b33 == 0)
								{
									ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_NUMERIC);
								}
								if (b33 == 1)
								{
									ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_ANY);
								}
								if (b33 == 2)
								{
									ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_PASSWORD);
								}
							}
							break;
						}
					case -110:
						{
							sbyte b28 = msg.reader().readByte();
							if (b28 == 1)
							{
								int num39 = msg.reader().readInt();
								sbyte[] array11 = Rms.loadRMS(num39 + string.Empty);
								if (array11 == null)
								{
									Service.gI().sendServerData(1, -1, null);
								}
								else
								{
									Service.gI().sendServerData(1, num39, array11);
								}
							}
							if (b28 == 0)
							{
								int num40 = msg.reader().readInt();
								short num41 = msg.reader().readShort();
								sbyte[] data = new sbyte[num41];
								msg.reader().read(ref data, 0, num41);
								Rms.saveRMS(num40 + string.Empty, data);
							}
							break;
						}
					case 93:
						{
							string str = msg.reader().readUTF();
							str = Res.changeString(str);
							GameScr.gI().chatVip(str);
							break;
						}
					case 98:
						{
							string str = msg.reader().readUTF();
							ModFunc.GI().AddNotifTichXanh(str);
							break;
						}
					case -106:
						{
							short num36 = msg.reader().readShort();
							int num37 = msg.reader().readShort();
							if (ItemTime.isExistItem(num36))
							{
								ItemTime.getItemById(num36).initTime(num37);
								break;
							}
							ItemTime o = new ItemTime(num36, num37);
							Char.vItemTime.addElement(o);
							break;
						}
					case -105:
						TransportScr.gI().time = 0;
						TransportScr.gI().maxTime = msg.reader().readShort();
						TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
						TransportScr.gI().type = msg.reader().readByte();
						TransportScr.gI().switchToMe();
						break;
					case -103:
						{
							sbyte b15 = msg.reader().readByte();
							if (b15 == 0)
							{
								GameCanvas.panel.vFlag.removeAllElements();
								sbyte b16 = msg.reader().readByte();
								for (int n = 0; n < b16; n++)
								{
									Item item = new Item();
									short num17 = msg.reader().readShort();
									if (num17 != -1)
									{
										item.template = ItemTemplates.get(num17);
										sbyte b17 = msg.reader().readByte();
										if (b17 != -1)
										{
											item.itemOption = new ItemOption[b17];
											for (int num18 = 0; num18 < item.itemOption.Length; num18++)
											{
												int num19 = msg.reader().readUnsignedByte();
												int param2 = msg.reader().readUnsignedShort();
												if (num19 != -1)
												{
													item.itemOption[num18] = new ItemOption(num19, param2);
												}
											}
										}
									}
									GameCanvas.panel.vFlag.addElement(item);
								}
								GameCanvas.panel.setTypeFlag();
								GameCanvas.panel.show();
							}
							else if (b15 == 1)
							{
								int num20 = msg.reader().readInt();
								sbyte b18 = msg.reader().readByte();
								Res.outz("---------------actionFlag1:  " + num20 + " : " + b18);
								if (num20 == Char.myCharz().charID)
								{
									Char.myCharz().cFlag = b18;
								}
								else if (GameScr.findCharInMap(num20) != null)
								{
									GameScr.findCharInMap(num20).cFlag = b18;
								}
								GameScr.gI().getFlagImage(num20, b18);
							}
							else
							{
								if (b15 != 2)
								{
									break;
								}
								sbyte b19 = msg.reader().readByte();
								int num21 = msg.reader().readShort();
								PKFlag pKFlag = new PKFlag();
								pKFlag.cflag = b19;
								pKFlag.IDimageFlag = num21;
								GameScr.vFlag.addElement(pKFlag);
								for (int num22 = 0; num22 < GameScr.vFlag.size(); num22++)
								{
									PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(num22);
									Res.outz("i: " + num22 + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
								}
								for (int num23 = 0; num23 < GameScr.vCharInMap.size(); num23++)
								{
									Char char5 = (Char)GameScr.vCharInMap.elementAt(num23);
									if (char5 != null && char5.cFlag == b19)
									{
										char5.flagImage = num21;
									}
								}
								if (Char.myCharz().cFlag == b19)
								{
									Char.myCharz().flagImage = num21;
								}
							}
							break;
						}
					case -102:
						{
							sbyte b11 = msg.reader().readByte();
							if (b11 != 0 && b11 == 1)
							{
								GameCanvas.loginScr.isLogin2 = false;
								Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
								LoginScr.isLoggingIn = true;
							}
							break;
						}
					case -101:
						{
							GameCanvas.loginScr.isLogin2 = true;
							GameCanvas.connect();
							string text = msg.reader().readUTF();
							Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text);
							Service.gI().setClientType();
							Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
							break;
						}
					case -100:
						{
							InfoDlg.hide();
							bool flag = false;
							if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
							{
								flag = true;
							}
							sbyte b = msg.reader().readByte();
							Res.outz("t Indxe= " + b);
							GameCanvas.panel.maxPageShop[b] = msg.reader().readByte();
							GameCanvas.panel.currPageShop[b] = msg.reader().readByte();
							Res.outz("max page= " + GameCanvas.panel.maxPageShop[b] + " curr page= " + GameCanvas.panel.currPageShop[b]);
							int num = msg.reader().readUnsignedByte();
							Char.myCharz().arrItemShop[b] = new Item[num];
							for (int i = 0; i < num; i++)
							{
								short num2 = msg.reader().readShort();
								if (num2 == -1)
								{
									continue;
								}
								Res.outz("template id= " + num2);
								Char.myCharz().arrItemShop[b][i] = new Item();
								Char.myCharz().arrItemShop[b][i].template = ItemTemplates.get(num2);
								Char.myCharz().arrItemShop[b][i].itemId = msg.reader().readShort();
								Char.myCharz().arrItemShop[b][i].buyCoin = msg.reader().readInt();
								Char.myCharz().arrItemShop[b][i].buyGold = msg.reader().readInt();
								Char.myCharz().arrItemShop[b][i].buyType = msg.reader().readByte();
								Char.myCharz().arrItemShop[b][i].quantity = msg.reader().readInt();
								Char.myCharz().arrItemShop[b][i].isMe = msg.reader().readByte();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
								sbyte b2 = msg.reader().readByte();
								if (b2 != -1)
								{
									Char.myCharz().arrItemShop[b][i].itemOption = new ItemOption[b2];
									for (int j = 0; j < Char.myCharz().arrItemShop[b][i].itemOption.Length; j++)
									{
										int num3 = msg.reader().readUnsignedByte();
										int param = msg.reader().readUnsignedShort();
										if (num3 != -1)
										{
											Char.myCharz().arrItemShop[b][i].itemOption[j] = new ItemOption(num3, param);
											Char.myCharz().arrItemShop[b][i].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b][i]);
										}
									}
								}
								sbyte b3 = msg.reader().readByte();
								if (b3 == 1)
								{
									int headTemp = msg.reader().readShort();
									int bodyTemp = msg.reader().readShort();
									int legTemp = msg.reader().readShort();
									int bagTemp = msg.reader().readShort();
									Char.myCharz().arrItemShop[b][i].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
								}
							}
							if (flag)
							{
								GameCanvas.panel2.setTabKiGui();
							}
							GameCanvas.panel.setTabShop();
							GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
							break;
						}
			}
		}
	}
}
