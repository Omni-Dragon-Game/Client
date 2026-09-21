using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart5(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case -19:
                    {
                        short itemMapID = msg.reader().readShort();
                        @char = GameScr.findCharInMap(msg.reader().readInt());
                        for (int num115 = 0; num115 < GameScr.vItemMap.size(); num115++)
                        {
                            ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num115);
                            if (itemMap.itemMapID != itemMapID)
                            {
                                continue;
                            }
                            if (@char == null)
                            {
                                return;
                            }
                            itemMap.setPoint(@char.cx, @char.cy - 10);
                            if (itemMap.x < @char.cx)
                            {
                                @char.cdir = -1;
                            }
                            else if (itemMap.x > @char.cx)
                            {
                                @char.cdir = 1;
                            }
                            break;
                        }
                        break;
                    }
                case -18:
                    {
                        GameCanvas.debug("SA63", 2);
                        int num114 = msg.reader().readByte();
                        GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), Char.myCharz().arrItemBag[num114].template.id, Char.myCharz().cx, Char.myCharz().cy, msg.reader().readShort(), msg.reader().readShort()));
                        Char.myCharz().arrItemBag[num114] = null;
                        break;
                    }
                case 68:
                    {
                        short itemMapID = msg.reader().readShort();
                        short itemTemplateID = msg.reader().readShort();
                        int x = msg.reader().readShort();
                        int y = msg.reader().readShort();
                        int num107 = msg.reader().readInt();
                        short r = 0;
                        if (num107 == -2)
                        {
                            r = msg.reader().readShort();
                        }
                        ItemMap o2 = new(num107, itemMapID, itemTemplateID, x, y, r);
                        GameScr.vItemMap.addElement(o2);
                        break;
                    }
                case 69:
                    SoundMn.IsDelAcc = ((msg.reader().readByte() != 0) ? true : false);
                    break;
                case -14:
                    @char = GameScr.findCharInMap(msg.reader().readInt());
                    if (@char == null)
                    {
                        return;
                    }
                    GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, msg.reader().readShort(), msg.reader().readShort()));
                    break;
                case -22:
                    Char.isLockKey = true;
                    Char.ischangingMap = true;
                    GameScr.gI().timeStartMap = 0;
                    GameScr.gI().timeLengthMap = 0;
                    Char.myCharz().mobFocus = null;
                    Char.myCharz().npcFocus = null;
                    Char.myCharz().charFocus = null;
                    Char.myCharz().itemFocus = null;
                    Char.myCharz().focus.removeAllElements();
                    Char.myCharz().testCharId = -9999;
                    Char.myCharz().killCharId = -9999;
                    GameCanvas.resetBg();
                    GameScr.gI().resetButton();
                    GameScr.gI().center = null;
                    break;
                case -70:
                    {
                        GameCanvas.endDlg();
                        if (PickMob.tanSat)
                        {
                            ModFunc.GI().perform(44, true);
                            return;
                        }
                        int avatar2 = msg.reader().readShort();
                        string chat3 = msg.reader().readUTF();
                        Npc npc6 = new(-1, 0, 0, 0, 0, 0)
                        {
                            avatar = avatar2
                        };
                        ChatPopup.addBigMessage(chat3, 100000, npc6);
                        sbyte type = msg.reader().readByte();
                        if (type == 0)
                        {
                            ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null)
                            {
                                x = GameCanvas.w / 2 - 35,
                                y = GameCanvas.h - 35
                            };
                        }
                        if (type == 1)
                        {
                            string p2 = msg.reader().readUTF();
                            string caption2 = msg.reader().readUTF();
                            ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null)
                            {
                                x = GameCanvas.w / 2 + 11,
                                y = GameCanvas.h - 35
                            };
                            ChatPopup.serverChatPopUp.cmdMsg2 = new Command(caption2, ChatPopup.serverChatPopUp, 1000, p2)
                            {
                                x = GameCanvas.w / 2 - 75,
                                y = GameCanvas.h - 35
                            };
                        }
                        break;
                    }
                case 38:
                    {
                        InfoDlg.hide();
                        int num85 = msg.reader().readShort();
                        string str = msg.reader().readUTF();
                        str = Res.changeString(str);
                        for (int num103 = 0; num103 < GameScr.vNpc.size(); num103++)
                        {
                            Npc npc4 = (Npc)GameScr.vNpc.elementAt(num103);
                            if (npc4.template.npcTemplateId == num85)
                            {
                                ChatPopup.addChatPopupMultiLine(str, 100000, npc4);
                                GameCanvas.panel.hideNow();
                                return;
                            }
                        }
                        Npc npc5 = new Npc(num85, 0, 0, 0, num85, GameScr.info1.charId[Char.myCharz().cgender][2]);
                        if (npc5.template.npcTemplateId == 5)
                        {
                            npc5.charID = 5;
                        }
                        try
                        {
                            npc5.avatar = msg.reader().readShort();
                        }
                        catch (Exception)
                        {
                        }
                        ChatPopup.addChatPopupMultiLine(str, 100000, npc5);
                        GameCanvas.panel.hideNow();
                        break;
                    }
                case 32:
                    {
                        int npcId = msg.reader().readShort();
                        Npc targetNpc = null;
                        for (int i = 0; i < GameScr.vNpc.size(); i++)
                        {
                            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
                            if (npc != null && npc.template != null && npc.template.npcTemplateId == npcId)
                            {
                                if (npc.Equals(Char.myCharz().npcFocus))
                                {
                                    targetNpc = npc;
                                    break;
                                }
                                if (targetNpc == null)
                                {
                                    targetNpc = npc;
                                }
                            }
                        }
                        if (targetNpc != null)
                        {
                            string chat = msg.reader().readUTF();
                            string[] menu = new string[msg.reader().readByte()];
                            for (int num87 = 0; num87 < menu.Length; num87++)
                            {
                                menu[num87] = msg.reader().readUTF();
                            }
                            try
                            {
                                short avatar = msg.reader().readShort();
                                if (avatar > 0)
                                {
                                    targetNpc.avatar = avatar;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            GameScr.gI().createMenu(menu, targetNpc);
                            ChatPopup.addChatPopup(chat, 100000, targetNpc);

                            if (npcId == 21 && chat.Contains("tối đa"))
                            {
                                ModFunc.GI().maxPhale = ModFunc.GI().currPhale;
                            }
                            return;
                        }
                        Npc npc1 = new(npcId, 0, -100, 100, npcId, GameScr.info1.charId[Char.myCharz().cgender][2]);
                        string chat1 = msg.reader().readUTF();
                        string[] menu1 = new string[msg.reader().readByte()];
                        for (int j = 0; j < menu1.Length; j++)
                        {
                            menu1[j] = msg.reader().readUTF();
                        }
                        try
                        {
                            short avatar = msg.reader().readShort();
                            npc1.avatar = avatar;
                        }
                        catch (Exception)
                        {
                        }
                        GameScr.gI().createMenu(menu1, npc1);
                        ChatPopup.addChatPopup(chat1, 100000, npc1);
                        break;
                    }
                case 7:
                    {
                        sbyte type = msg.reader().readByte();
                        short id = msg.reader().readShort();
                        string info3 = msg.reader().readUTF();
                        GameCanvas.panel.saleRequest(type, info3, id);
                        break;
                    }
                case 6:
                    Char.myCharz().xu = msg.reader().readLong();
                    Char.myCharz().luong = msg.reader().readInt();
                    Char.myCharz().luongKhoa = msg.reader().readInt();
                    Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                    Char.myCharz().luongStr = mSystem.numberTostring(Char.myCharz().luong);
                    Char.myCharz().luongKhoaStr = mSystem.numberTostring(Char.myCharz().luongKhoa);
                    GameCanvas.endDlg();
                    break;
                case -23:
                    LoadAuraNpcs(msg);
                    break;
                case -24:
                    if (GameCanvas.currentScreen is GameScr)
                    {
                        GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 3000;
                    }
                    else
                    {
                        GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000;
                    }
                    Char.isLoadingMap = true;
                    GameScr.gI().magicTree = null;
                    GameCanvas.isLoading = true;
                    GameScr.resetAllvector();
                    GameCanvas.endDlg();
                    TileMap.vGo.removeAllElements();
                    PopUp.vPopups.removeAllElements();
                    mSystem.gcc();
                    TileMap.mapID = msg.reader().readUnsignedByte();
                    TileMap.planetID = msg.reader().readByte();
                    TileMap.tileID = msg.reader().readByte();
                    TileMap.bgID = msg.reader().readByte();
                    TileMap.typeMap = msg.reader().readByte();
                    TileMap.mapName = msg.reader().readUTF();
                    TileMap.zoneID = msg.reader().readByte();
                    try
                    {
                        TileMap.loadMapFromResource(TileMap.mapID);
                    }
                    catch (Exception)
                    {
                        Service.gI().requestMaptemplate(TileMap.mapID);
                        messWait = msg;
                        return;
                    }
                    loadInfoMap(msg);
                    try
                    {
                        sbyte b33 = msg.reader().readByte();
                        TileMap.isMapDouble = ((b33 != 0) ? true : false);
                    }
                    catch (Exception)
                    {
                    }
                    GameScr.cmx = GameScr.cmtoX;
                    GameScr.cmy = GameScr.cmtoY;
                    break;
                case -31:
                    {
                        TileMap.vItemBg.removeAllElements();
                        short num71 = msg.reader().readShort();
                        for (int num72 = 0; num72 < num71; num72++)
                        {
                            BgItem bgItem = new BgItem();
                            bgItem.id = num72;
                            bgItem.idImage = msg.reader().readShort();
                            bgItem.layer = msg.reader().readByte();
                            bgItem.dx = msg.reader().readShort();
                            bgItem.dy = msg.reader().readShort();
                            sbyte b32 = msg.reader().readByte();
                            bgItem.tileX = new int[b32];
                            bgItem.tileY = new int[b32];
                            for (int num73 = 0; num73 < b32; num73++)
                            {
                                bgItem.tileX[num72] = msg.reader().readByte();
                                bgItem.tileY[num72] = msg.reader().readByte();
                            }
                            TileMap.vItemBg.addElement(bgItem);
                        }
                        break;
                    }
                case -4:
                    {
                        GameCanvas.debug("SA76", 2);
                        @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char == null)
                        {
                            return;
                        }
                        GameCanvas.debug("SA76v1", 2);
                        if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
                        {
                            @char.setSkillPaint(GameScr.sks[msg.reader().readUnsignedByte()], 0);
                        }
                        else
                        {
                            @char.setSkillPaint(GameScr.sks[msg.reader().readUnsignedByte()], 1);
                        }
                        GameCanvas.debug("SA76v2", 2);
                        @char.attMobs = new Mob[msg.reader().readByte()];
                        for (int num26 = 0; num26 < @char.attMobs.Length; num26++)
                        {
                            Mob mob3 = (Mob)GameScr.vMob.elementAt(msg.reader().readByte());
                            @char.attMobs[num26] = mob3;
                            if (num26 == 0)
                            {
                                if (@char.cx <= mob3.x)
                                {
                                    @char.cdir = 1;
                                }
                                else
                                {
                                    @char.cdir = -1;
                                }
                            }
                        }
                        GameCanvas.debug("SA76v3", 2);
                        @char.charFocus = null;
                        @char.mobFocus = @char.attMobs[0];
                        Char[] array = new Char[10];
                        num = 0;
                        try
                        {
                            for (num = 0; num < array.Length; num++)
                            {
                                int num18 = msg.reader().readInt();
                                Char char4 = (array[num] = ((num18 != Char.myCharz().charID) ? GameScr.findCharInMap(num18) : Char.myCharz()));
                                if (num == 0)
                                {
                                    if (@char.cx <= char4.cx)
                                    {
                                        @char.cdir = 1;
                                    }
                                    else
                                    {
                                        @char.cdir = -1;
                                    }
                                }
                            }
                        }
                        catch (Exception ex5)
                        {
                            Cout.println("Loi PLAYER_ATTACK_N_P " + ex5.ToString());
                        }
                        GameCanvas.debug("SA76v4", 2);
                        if (num > 0)
                        {
                            @char.attChars = new Char[num];
                            for (num = 0; num < @char.attChars.Length; num++)
                            {
                                @char.attChars[num] = array[num];
                            }
                            @char.charFocus = @char.attChars[0];
                            @char.mobFocus = null;
                        }
                        GameCanvas.debug("SA76v5", 2);
                        break;
                    }
                case 54:
                    {
                        @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char == null)
                        {
                            return;
                        }
                        int num17 = msg.reader().readUnsignedByte();
                        if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
                        {
                            @char.setSkillPaint(GameScr.sks[num17], 0);
                        }
                        else
                        {
                            @char.setSkillPaint(GameScr.sks[num17], 1);
                        }
                        Mob[] array3 = new Mob[10];
                        num = 0;
                        try
                        {
                            for (num = 0; num < array3.Length; num++)
                            {
                                Mob mob2 = (array3[num] = (Mob)GameScr.vMob.elementAt(msg.reader().readByte()));
                                if (num == 0)
                                {
                                    if (@char.cx <= mob2.x)
                                    {
                                        @char.cdir = 1;
                                    }
                                    else
                                    {
                                        @char.cdir = -1;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        if (num > 0)
                        {
                            @char.attMobs = new Mob[num];
                            for (num = 0; num < @char.attMobs.Length; num++)
                            {
                                @char.attMobs[num] = array3[num];
                            }
                            @char.charFocus = null;
                            @char.mobFocus = @char.attMobs[0];
                        }
                        break;
                    }
                case -60:
                    {
                        GameCanvas.debug("SA7666", 2);
                        int num2 = msg.reader().readInt();
                        int num3 = -1;
                        if (num2 != Char.myCharz().charID)
                        {
                            Char char2 = GameScr.findCharInMap(num2);
                            if (char2 == null)
                            {
                                return;
                            }
                            if (char2.currentMovePoint != null)
                            {
                                char2.createShadow(char2.cx, char2.cy, 10);
                                char2.cx = char2.currentMovePoint.xEnd;
                                char2.cy = char2.currentMovePoint.yEnd;
                            }
                            int num4 = msg.reader().readUnsignedByte();
                            if ((TileMap.tileTypeAtPixel(char2.cx, char2.cy) & 2) == 2)
                            {
                                char2.setSkillPaint(GameScr.sks[num4], 0);
                            }
                            else
                            {
                                char2.setSkillPaint(GameScr.sks[num4], 1);
                            }
                            sbyte b = msg.reader().readByte();
                            Char[] array = new Char[b];
                            for (num = 0; num < array.Length; num++)
                            {
                                num3 = msg.reader().readInt();
                                Char char3;
                                if (num3 == Char.myCharz().charID)
                                {
                                    char3 = Char.myCharz();
                                    if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay)
                                    {
                                        Service.gI().requestChangeZone(-1, -1);
                                        GameScr.isChangeZone = true;
                                    }
                                }
                                else
                                {
                                    char3 = GameScr.findCharInMap(num3);
                                }
                                array[num] = char3;
                                if (num == 0)
                                {
                                    if (char2.cx <= char3.cx)
                                    {
                                        char2.cdir = 1;
                                    }
                                    else
                                    {
                                        char2.cdir = -1;
                                    }
                                }
                            }
                            if (num > 0)
                            {
                                char2.attChars = new Char[num];
                                for (num = 0; num < char2.attChars.Length; num++)
                                {
                                    char2.attChars[num] = array[num];
                                }
                                char2.mobFocus = null;
                                char2.charFocus = char2.attChars[0];
                            }
                        }
                        else
                        {
                            sbyte b2 = msg.reader().readByte();
                            sbyte b3 = msg.reader().readByte();
                            num3 = msg.reader().readInt();
                        }
                        try
                        {
                            sbyte b4 = msg.reader().readByte();
                            if (b4 != 1)
                            {
                                break;
                            }
                            sbyte b5 = msg.reader().readByte();
                            if (num3 == Char.myCharz().charID)
                            {
                                bool flag = false;
                                @char = Char.myCharz();
                                long num5 = msg.readLong();
                                @char.isDie = msg.reader().readBoolean();
                                if (@char.isDie)
                                {
                                    Char.isLockKey = true;
                                }
                                long num6 = 0;
                                flag = (@char.isCrit = msg.reader().readBoolean());
                                @char.isMob = false;
                                num5 = (@char.damHP = num5 + num6);
                                if (b5 == 0)
                                {
                                    @char.doInjure(num5, 0, flag, isMob: false);
                                }
                            }
                            else
                            {
                                @char = GameScr.findCharInMap(num3);
                                if (@char == null)
                                {
                                    return;
                                }
                                bool flag2 = false;
                                long num7 = msg.readLong();
                                @char.isDie = msg.reader().readBoolean();
                                long num8 = 0;
                                flag2 = (@char.isCrit = msg.reader().readBoolean());
                                @char.isMob = false;
                                num7 = (@char.damHP = num7 + num8);
                                if (b5 == 0)
                                {
                                    @char.doInjure(num7, 0, flag2, isMob: false);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    }
            }
            switch (msg.command)
            {
                case -2:
                    {
                        GameCanvas.debug("SA77", 22);
                        int num195 = msg.reader().readInt();
                        Char.myCharz().yen += num195;
                        GameScr.startFlyText((num195 <= 0) ? (string.Empty + num195) : ("+" + num195), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
                        break;
                    }
                case 95:
                    {
                        GameCanvas.debug("SA77", 22);
                        int num182 = msg.reader().readInt();
                        Char.myCharz().xu += num182;
                        Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                        GameScr.startFlyText((num182 <= 0) ? (string.Empty + num182) : ("+" + num182), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
                        break;
                    }
                case 96:
                    GameCanvas.debug("SA77a", 22);
                    Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
                    break;
                case 97:
                    {
                        sbyte b75 = msg.reader().readByte();
                        for (int num188 = 0; num188 < Char.myCharz().taskOrders.size(); num188++)
                        {
                            TaskOrder taskOrder = (TaskOrder)Char.myCharz().taskOrders.elementAt(num188);
                            if (taskOrder.taskId == b75)
                            {
                                taskOrder.count = msg.reader().readShort();
                                break;
                            }
                        }
                        break;
                    }
                case -1:
                    {
                        GameCanvas.debug("SA77", 222);
                        int num194 = msg.reader().readInt();
                        Char.myCharz().xu += num194;
                        Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                        Char.myCharz().yen -= num194;
                        GameScr.startFlyText("+" + num194, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
                        break;
                    }
                case -3:
                    {
                        sbyte type = msg.reader().readByte();
                        long param = msg.readLong();
                        if (type == 0)
                        {
                            Char.myCharz().cPower += param;
                        }
                        if (type == 1)
                        {
                            Char.myCharz().cTiemNang += param;
                        }
                        if (type == 2)
                        {
                            Char.myCharz().cPower += param;
                            Char.myCharz().cTiemNang += param;
                        }
                        Char.myCharz().applyCharLevelPercent();
                        if (Char.myCharz().cTypePk != 3)
                        {
                            GameScr.startFlyText(((param <= 0) ? string.Empty : "+") + param, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -4, mFont.GREEN);
                            if (param > 0 && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5002)
                            {
                                ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
                                ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
                            }
                        }
                        break;
                    }
                case -73:
                    {
                        sbyte npcId = msg.reader().readByte();
                        for (int i = 0; i < GameScr.vNpc.size(); i++)
                        {
                            Npc npc7 = (Npc)GameScr.vNpc.elementAt(i);
                            if (npc7.template.npcTemplateId == npcId)
                            {
                                sbyte isHide = msg.reader().readByte();
                                if (isHide == 0)
                                {
                                    npc7.isHide = true;
                                }
                                else
                                {
                                    npc7.isHide = false;
                                }
                                break;
                            }
                        }
                        break;
                    }
                case -5:
                    {
                        int charID = msg.reader().readInt();
                        int num184 = msg.reader().readInt();
                        Char char15;
                        if (num184 != -100)
                        {
                            char15 = new Char
                            {
                                charID = charID,
                                clanID = num184
                            };
                        }
                        else
                        {
                            char15 = new Mabu
                            {
                                charID = charID,
                                clanID = num184
                            };
                        }
                        if (char15.clanID == -2)
                        {
                            char15.isCopy = true;
                        }
                        if (readCharInfo(char15, msg))
                        {
                            sbyte b73 = msg.reader().readByte();
                            if (char15.cy <= 10 && b73 != 0 && b73 != 2)
                            {
                                Teleport teleport2 = new Teleport(char15.cx, char15.cy, char15.head, char15.cdir, 1, isMe: false, (b73 != 1) ? b73 : char15.cgender);
                                teleport2.id = char15.charID;
                                char15.isTeleport = true;
                                Teleport.addTeleport(teleport2);
                            }
                            if (b73 == 2)
                            {
                                char15.show();
                            }
                            for (int num185 = 0; num185 < GameScr.vMob.size(); num185++)
                            {
                                Mob mob10 = (Mob)GameScr.vMob.elementAt(num185);
                                if (mob10 != null && mob10.isMobMe && mob10.mobId == char15.charID)
                                {
                                    char15.mobMe = mob10;
                                    char15.mobMe.x = char15.cx;
                                    char15.mobMe.y = char15.cy - 40;
                                    break;
                                }
                            }
                            if (GameScr.findCharInMap(char15.charID) == null)
                            {
                                GameScr.vCharInMap.addElement(char15);
                            }
                            char15.isMonkey = msg.reader().readByte();
                            short num186 = msg.reader().readShort();
                            if (num186 != -1)
                            {
                                char15.isHaveMount = true;
                                switch (num186)
                                {
                                    case 346:
                                    case 347:
                                    case 348:
                                        char15.isMountVip = false;
                                        break;
                                    case 349:
                                    case 350:
                                    case 351:
                                        char15.isMountVip = true;
                                        break;
                                    case 396:
                                        char15.isEventMount = true;
                                        break;
                                    case 532:
                                        char15.isSpeacialMount = true;
                                        break;
                                    default:
                                        if (num186 >= Char.ID_NEW_MOUNT)
                                        {
                                            char15.idMount = num186;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                char15.isHaveMount = false;
                            }
                        }
                        sbyte b74 = msg.reader().readByte();
                        char15.cFlag = b74;
                        char15.isNhapThe = msg.reader().readByte() == 1;
                        try
                        {
                            char15.idAuraEff = msg.reader().readShort();
                            char15.idEff_Set_Item = msg.reader().readSByte();
                            char15.idHat = msg.reader().readShort();
                            if (char15.bag >= 201 && char15.bag < 255)
                            {
                                Effect effect2 = new Effect(char15.bag, char15, 2, -1, 10, 1);
                                effect2.typeEff = 5;
                                char15.addEffChar(effect2);
                            }
                            else
                            {
                                for (int num187 = 0; num187 < 54; num187++)
                                {
                                    char15.removeEffChar(0, 201 + num187);
                                }
                            }
                        }
                        catch (Exception ex37)
                        {
                            Res.outz("cmd: -5 err: " + ex37.StackTrace);
                        }
                        char15.isTichXanh = msg.reader().readByte() == 1;
                        GameScr.gI().getFlagImage(char15.charID, char15.cFlag);
                        break;
                    }
        }
    }
}
