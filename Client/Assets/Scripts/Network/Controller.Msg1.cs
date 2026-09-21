using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart1(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case 70:
                    QuayTamBao.receiveMsg(msg);
                    break;
                case 0:
                    readLogin(msg);
                    break;
                case 24:
                    read_opt(msg);
                    break;
                case 20:
                    phuban_Info(msg);
                    break;
                case 66:
                    readGetImgByName(msg);
                    break;
                case 65:
                    {
                        sbyte b67 = msg.reader().readSByte();
                        string text6 = msg.reader().readUTF();
                        short num162 = msg.reader().readShort();
                        if (ItemTime.isExistMessage(b67))
                        {
                            if (num162 != 0)
                            {
                                ItemTime.getMessageById(b67).initTimeText(b67, text6, num162);
                            }
                            else
                            {
                                GameScr.textTime.removeElement(ItemTime.getMessageById(b67));
                            }
                        }
                        else
                        {
                            ItemTime itemTime = new ItemTime();
                            itemTime.initTimeText(b67, text6, num162);
                            GameScr.textTime.addElement(itemTime);
                        }
                        break;
                    }
                case 112:
                    {
                        sbyte type = msg.reader().readByte();
                        if (type == 0)
                        {
                            Panel.spearcialImage = msg.reader().readShort();
                            Panel.specialInfo = msg.reader().readUTF(); // name
                            ModFunc.GI().CheckAutoIntrinsic(Panel.specialInfo);
                        }
                        else
                        {
                            if (type != 1)
                            {
                                break;
                            }
                            sbyte tabSize = msg.reader().readByte();
                            Char.myCharz().infoSpeacialSkill = new string[tabSize][];
                            Char.myCharz().imgSpeacialSkill = new short[tabSize][];
                            GameCanvas.panel.speacialTabName = new string[tabSize][];
                            for (int j = 0; j < tabSize; j++)
                            {
                                GameCanvas.panel.speacialTabName[j] = new string[2];
                                string[] array10 = Res.split(msg.reader().readUTF(), "\n", 0);
                                if (array10.Length == 2)
                                {
                                    GameCanvas.panel.speacialTabName[j] = array10;
                                }
                                if (array10.Length == 1)
                                {
                                    GameCanvas.panel.speacialTabName[j][0] = array10[0];
                                    GameCanvas.panel.speacialTabName[j][1] = string.Empty;
                                }
                                int size = msg.reader().readByte();
                                Char.myCharz().infoSpeacialSkill[j] = new string[size];
                                Char.myCharz().imgSpeacialSkill[j] = new short[size];
                                for (int i = 0; i < size; i++)
                                {
                                    Char.myCharz().imgSpeacialSkill[j][i] = msg.reader().readShort();
                                    Char.myCharz().infoSpeacialSkill[j][i] = msg.reader().readUTF();
                                }
                            }
                            GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
                            GameCanvas.panel.setTypeSpeacialSkill();
                            GameCanvas.panel.show();
                        }
                        break;
                    }
                case -98:
                    {
                        sbyte b39 = msg.reader().readByte();
                        GameCanvas.menu.showMenu = false;
                        if (b39 == 0)
                        {
                            GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
                        }
                        break;
                    }
                case -97:
                    Char.myCharz().cNangdong = msg.reader().readInt();
                    break;
                case -96:
                    {
                        sbyte typeTop = msg.reader().readByte();
                        GameCanvas.panel.vTop.removeAllElements();
                        string topName = msg.reader().readUTF();
                        sbyte size = msg.reader().readByte();
                        for (int n = 0; n < size; n++)
                        {
                            int rank = msg.reader().readInt();
                            int pId = msg.reader().readInt();
                            short headID = msg.reader().readShort();
                            short headICON = msg.reader().readShort();
                            short body = msg.reader().readShort();
                            short leg = msg.reader().readShort();
                            string name = msg.reader().readUTF();
                            string info2 = msg.reader().readUTF();
                            TopInfo topInfo = new()
                            {
                                rank = rank,
                                headID = headID,
                                headICON = headICON,
                                body = body,
                                leg = leg,
                                name = name,
                                info = info2,
                                info2 = msg.reader().readUTF(),
                                pId = pId
                            };
                            GameCanvas.panel.vTop.addElement(topInfo);
                        }
                        GameCanvas.panel.topName = topName;
                        GameCanvas.panel.setTypeTop(typeTop);
                        GameCanvas.panel.show();
                        break;
                    }
                case -94:
                    while (msg.reader().available() > 0)
                    {
                        short num136 = msg.reader().readShort();
                        int num137 = msg.reader().readInt();
                        for (int num138 = 0; num138 < Char.myCharz().vSkill.size(); num138++)
                        {
                            Skill skill = (Skill)Char.myCharz().vSkill.elementAt(num138);
                            if (skill != null && skill.skillId == num136)
                            {
                                if (num137 < skill.coolDown)
                                {
                                    skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (skill.coolDown - num137);
                                }
                            }
                        }
                    }
                    break;
                case -95:
                    {
                        sbyte type = msg.reader().readByte();
                        if (type == 0)
                        {
                            int num139 = msg.reader().readInt();
                            short templateId = msg.reader().readShort();
                            long hp = msg.readLong();
                            SoundMn.gI().explode_1();
                            if (num139 == Char.myCharz().charID)
                            {
                                Char.myCharz().mobMe = new Mob(num139, isDisable: false, isDontMove: false, isFire: false, isIce: false, isWind: false, templateId, 1, hp, 0, hp, (short)(Char.myCharz().cx + ((Char.myCharz().cdir != 1) ? (-40) : 40)), (short)Char.myCharz().cy, 4, 0)
                                {
                                    isMobMe = true
                                };
                                EffecMn.addEff(new Effect(18, Char.myCharz().mobMe.x, Char.myCharz().mobMe.y, 2, 10, -1));
                                Char.myCharz().tMobMeBorn = 30;
                                GameScr.vMob.addElement(Char.myCharz().mobMe);
                            }
                            else
                            {
                                @char = GameScr.findCharInMap(num139);
                                if (@char != null)
                                {
                                    Mob mob6 = new(num139, isDisable: false, isDontMove: false, isFire: false, isIce: false, isWind: false, templateId, 1, hp, 0, hp, (short)@char.cx, (short)@char.cy, 4, 0);
                                    mob6.isMobMe = true;
                                    @char.mobMe = mob6;
                                    GameScr.vMob.addElement(@char.mobMe);
                                }
                                else
                                {
                                    Mob mob7 = GameScr.findMobInMap(num139);
                                    if (mob7 == null)
                                    {
                                        mob7 = new Mob(num139, isDisable: false, isDontMove: false, isFire: false, isIce: false, isWind: false, templateId, 1, hp, 0, hp, -100, -100, 4, 0)
                                        {
                                            isMobMe = true
                                        };
                                        GameScr.vMob.addElement(mob7);
                                    }
                                }
                            }
                        }
                        if (type == 1)
                        {
                            int num141 = msg.reader().readInt();
                            int mobId = msg.reader().readByte();
                            if (num141 == Char.myCharz().charID)
                            {
                                if (GameScr.findMobInMap(mobId) != null)
                                {
                                    Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
                                }
                            }
                            else
                            {
                                @char = GameScr.findCharInMap(num141);
                                if (@char != null && GameScr.findMobInMap(mobId) != null)
                                {
                                    @char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
                                }
                            }
                        }
                        if (type == 2)
                        {
                            int num142 = msg.reader().readInt();
                            int num143 = msg.reader().readInt();
                            long dameHit = msg.readLong();
                            long cHPNew = msg.readLong();
                            if (num142 == Char.myCharz().charID)
                            {
                                @char = GameScr.findCharInMap(num143);
                                if (@char != null)
                                {
                                    @char.cHPNew = cHPNew;
                                    if (Char.myCharz().mobMe.isBusyAttackSomeOne)
                                    {
                                        @char.doInjure(dameHit, 0, isCrit: false, isMob: true);
                                    }
                                    else
                                    {
                                        Char.myCharz().mobMe.dame = dameHit;
                                        Char.myCharz().mobMe.setAttack(@char);
                                    }
                                }
                            }
                            else
                            {
                                mob = GameScr.findMobInMap(num142);
                                if (mob != null)
                                {
                                    if (num143 == Char.myCharz().charID)
                                    {
                                        Char.myCharz().cHPNew = cHPNew;
                                        if (mob.isBusyAttackSomeOne)
                                        {
                                            Char.myCharz().doInjure(dameHit, 0, isCrit: false, isMob: true);
                                        }
                                        else
                                        {
                                            mob.dame = dameHit;
                                            mob.setAttack(Char.myCharz());
                                        }
                                    }
                                    else
                                    {
                                        @char = GameScr.findCharInMap(num143);
                                        if (@char != null)
                                        {
                                            @char.cHPNew = cHPNew;
                                            if (mob.isBusyAttackSomeOne)
                                            {
                                                @char.doInjure(dameHit, 0, isCrit: false, isMob: true);
                                            }
                                            else
                                            {
                                                mob.dame = dameHit;
                                                mob.setAttack(@char);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (type == 3)
                        {
                            int num145 = msg.reader().readInt();
                            int mobId2 = msg.reader().readInt();
                            long hp = msg.readLong();
                            long dame = msg.readLong();
                            @char = null;
                            @char = ((Char.myCharz().charID != num145) ? GameScr.findCharInMap(num145) : Char.myCharz());
                            if (@char != null)
                            {
                                mob = GameScr.findMobInMap(mobId2);
                                if (@char.mobMe != null)
                                {
                                    @char.mobMe.attackOtherMob(mob);
                                }
                                if (mob != null)
                                {
                                    mob.hp = hp;
                                    mob.updateHp_bar();
                                    if (dame == 0)
                                    {
                                        mob.x = mob.xFirst;
                                        mob.y = mob.yFirst;
                                        GameScr.startFlyText(mResources.miss, mob.x, mob.y - mob.h, 0, -2, mFont.MISS);
                                    }
                                    else
                                    {
                                        GameScr.startFlyText("-" + dame, mob.x, mob.y - mob.h, 0, -2, mFont.ORANGE);
                                    }
                                }
                            }
                        }
                        if (type == 4)
                        {
                        }
                        if (type == 5)
                        {
                            int num147 = msg.reader().readInt();
                            sbyte b61 = msg.reader().readByte();
                            int mobId3 = msg.reader().readInt();
                            int num148 = msg.readInt3Byte();
                            int hp2 = msg.readInt3Byte();
                            @char = null;
                            @char = ((num147 != Char.myCharz().charID) ? GameScr.findCharInMap(num147) : Char.myCharz());
                            if (@char == null)
                            {
                                return;
                            }
                            if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
                            {
                                @char.setSkillPaint(GameScr.sks[b61], 0);
                            }
                            else
                            {
                                @char.setSkillPaint(GameScr.sks[b61], 1);
                            }
                            Mob mob8 = GameScr.findMobInMap(mobId3);
                            if (@char.cx <= mob8.x)
                            {
                                @char.cdir = 1;
                            }
                            else
                            {
                                @char.cdir = -1;
                            }
                            @char.mobFocus = mob8;
                            mob8.hp = hp2;
                            mob8.updateHp_bar();
                            if (num148 == 0)
                            {
                                mob8.x = mob8.xFirst;
                                mob8.y = mob8.yFirst;
                                GameScr.startFlyText(mResources.miss, mob8.x, mob8.y - mob8.h, 0, -2, mFont.MISS);
                            }
                            else
                            {
                                GameScr.startFlyText("-" + num148, mob8.x, mob8.y - mob8.h, 0, -2, mFont.ORANGE);
                            }
                        }
                        if (type == 6)
                        {
                            int num149 = msg.reader().readInt();
                            if (num149 == Char.myCharz().charID)
                            {
                                Char.myCharz().mobMe.startDie();
                            }
                            else
                            {
                                GameScr.findCharInMap(num149)?.mobMe.startDie();
                            }
                        }
                        if (type != 7)
                        {
                            break;
                        }
                        int num150 = msg.reader().readInt();
                        if (num150 == Char.myCharz().charID)
                        {
                            Char.myCharz().mobMe = null;
                            for (int num151 = 0; num151 < GameScr.vMob.size(); num151++)
                            {
                                if (((Mob)GameScr.vMob.elementAt(num151)).mobId == num150)
                                {
                                    GameScr.vMob.removeElementAt(num151);
                                }
                            }
                            break;
                        }
                        @char = GameScr.findCharInMap(num150);
                        for (int num152 = 0; num152 < GameScr.vMob.size(); num152++)
                        {
                            if (((Mob)GameScr.vMob.elementAt(num152)).mobId == num150)
                            {
                                GameScr.vMob.removeElementAt(num152);
                            }
                        }
                        if (@char != null)
                        {
                            @char.mobMe = null;
                        }
                        break;
                    }
                case -92:
                    Main.typeClient = msg.reader().readByte();
                    if (Rms.loadRMSString("ResVersion") == null)
                    {
                        Rms.clearAll();
                    }
                    Rms.saveRMSInt("clienttype", Main.typeClient);
                    Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
                    if (Rms.loadRMSString("ResVersion") == null)
                    {
                        GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
                    }
                    break;
                case -91:
                    {
                        sbyte b37 = msg.reader().readByte();
                        GameCanvas.panel.mapNames = new string[b37];
                        GameCanvas.panel.planetNames = new string[b37];
                        for (int num84 = 0; num84 < b37; num84++)
                        {
                            GameCanvas.panel.mapNames[num84] = msg.reader().readUTF();
                            GameCanvas.panel.planetNames[num84] = msg.reader().readUTF();
                        }
                        AutoXmap.ShowPanelMapTrans();
                        break;
                    }
                case -90:
                    {
                        sbyte b35 = msg.reader().readByte();
                        int num79 = msg.reader().readInt();
                        @char = ((Char.myCharz().charID != num79) ? GameScr.findCharInMap(num79) : Char.myCharz());
                        if (b35 != -1)
                        {
                            short num80 = msg.reader().readShort();
                            short num81 = msg.reader().readShort();
                            short num82 = msg.reader().readShort();
                            sbyte isMonkey = msg.reader().readByte();
                            if (@char != null)
                            {
                                if (@char.charID == num79)
                                {
                                    @char.isMask = true;
                                    @char.isMonkey = isMonkey;
                                    if (@char.isMonkey != 0)
                                    {
                                        @char.isWaitMonkey = false;
                                        @char.isLockMove = false;
                                    }
                                }
                                else if (@char != null)
                                {
                                    @char.isMask = true;
                                    @char.isMonkey = isMonkey;
                                }
                                if (num80 != -1)
                                {
                                    @char.head = num80;
                                }
                                if (num81 != -1)
                                {
                                    @char.body = num81;
                                }
                                if (num82 != -1)
                                {
                                    @char.leg = num82;
                                }
                            }
                        }
                        if (b35 == -1 && @char != null)
                        {
                            @char.isMask = false;
                            @char.isMonkey = 0;
                        }
                        if (@char == null)
                        {
                        }
                        break;
                    }
                case -88:
                    GameCanvas.endDlg();
                    GameCanvas.serverScreen.switchToMe();
                    break;
                case -87:
                    {
                        msg.reader().mark(100000);
                        createData(msg.reader(), isSaveRMS: true);
                        msg.reader().reset();
                        sbyte[] data3 = new sbyte[msg.reader().available()];
                        msg.reader().readFully(ref data3);
                        sbyte[] data4 = new sbyte[1] { GameScr.vcData };
                        Rms.saveRMS("NRdataVersion", data4);
                        LoginScr.isUpdateData = false;
                        GameScr.gI().readDart();
                        GameScr.gI().readEfect();
                        GameScr.gI().readArrow();
                        GameScr.gI().readSkill();
                        GameScr.gI().readPart();
                        SmallImage.init();
                        SmallImage.loadBigRMS();
                        if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
                        {
                            Service.gI().clientOk();
                            return;
                        }
                        break;
                    }
                case -86:
                    {
                        sbyte b42 = msg.reader().readByte();
                        if (b42 == 0)
                        {
                            int playerID = msg.reader().readInt();
                            GameScr.gI().giaodich(playerID);
                        }
                        if (b42 == 1)
                        {
                            int num97 = msg.reader().readInt();
                            Char char7 = GameScr.findCharInMap(num97);
                            if (char7 == null)
                            {
                                return;
                            }
                            GameCanvas.panel.setTypeGiaoDich(char7);
                            GameCanvas.panel.show();
                            Service.gI().getPlayerMenu(num97);
                        }
                        if (b42 == 2)
                        {
                            sbyte b43 = msg.reader().readByte();
                            for (int num98 = 0; num98 < GameCanvas.panel.vMyGD.size(); num98++)
                            {
                                Item item2 = (Item)GameCanvas.panel.vMyGD.elementAt(num98);
                                if (item2.indexUI == b43)
                                {
                                    GameCanvas.panel.vMyGD.removeElement(item2);
                                    break;
                                }
                            }
                        }
                        if (b42 == 6)
                        {
                            GameCanvas.panel.isFriendLock = true;
                            if (GameCanvas.panel2 != null)
                            {
                                GameCanvas.panel2.isFriendLock = true;
                            }
                            GameCanvas.panel.vFriendGD.removeAllElements();
                            if (GameCanvas.panel2 != null)
                            {
                                GameCanvas.panel2.vFriendGD.removeAllElements();
                            }
                            int friendMoneyGD = msg.reader().readInt();
                            sbyte b44 = msg.reader().readByte();
                            for (int num99 = 0; num99 < b44; num99++)
                            {
                                Item item3 = new Item();
                                item3.template = ItemTemplates.get(msg.reader().readShort());
                                item3.quantity = msg.reader().readInt();
                                int num100 = msg.reader().readUnsignedByte();
                                if (num100 != 0)
                                {
                                    item3.itemOption = new ItemOption[num100];
                                    for (int num101 = 0; num101 < item3.itemOption.Length; num101++)
                                    {
                                        int num102 = msg.reader().readUnsignedByte();
                                        int param5 = msg.reader().readUnsignedShort();
                                        if (num102 != -1)
                                        {
                                            item3.itemOption[num101] = new ItemOption(num102, param5);
                                            item3.compare = GameCanvas.panel.getCompare(item3);
                                        }
                                    }
                                }
                                if (GameCanvas.panel2 != null)
                                {
                                    GameCanvas.panel2.vFriendGD.addElement(item3);
                                }
                                else
                                {
                                    GameCanvas.panel.vFriendGD.addElement(item3);
                                }
                            }
                            if (GameCanvas.panel2 != null)
                            {
                                GameCanvas.panel2.setTabGiaoDich(isMe: false);
                                GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
                            }
                            else
                            {
                                GameCanvas.panel.friendMoneyGD = friendMoneyGD;
                                if (GameCanvas.panel.currentTabIndex == 2)
                                {
                                    GameCanvas.panel.setTabGiaoDich(isMe: false);
                                }
                            }
                        }
                        if (b42 == 7)
                        {
                            InfoDlg.hide();
                            if (GameCanvas.panel.isShow)
                            {
                                GameCanvas.panel.hide();
                            }
                        }
                        break;
                    }
                case -85:
                    {
                        sbyte b47 = msg.reader().readByte();
                        if (b47 == 0)
                        {
                            int num106 = msg.reader().readUnsignedShort();
                            sbyte[] data2 = new sbyte[num106];
                            msg.reader().read(ref data2, 0, num106);
                            GameScr.imgCapcha = Image.createImage(data2, 0, num106);
                            GameScr.gI().keyInput = "-----";
                            GameScr.gI().strCapcha = msg.reader().readUTF();
                            GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
                            GameScr.gI().mobCapcha = new Mob();
                            GameScr.gI().right = null;
                        }
                        if (b47 == 1)
                        {
                            MobCapcha.isAttack = true;
                        }
                        if (b47 == 2)
                        {
                            MobCapcha.explode = true;
                            GameScr.gI().right = GameScr.gI().cmdFocus;
                        }
                        break;
                    }
                case -112:
                    {
                        sbyte b40 = msg.reader().readByte();
                        if (b40 == 0)
                        {
                            sbyte mobIndex = msg.reader().readByte();
                            GameScr.findMobInMap(mobIndex).clearBody();
                        }
                        if (b40 == 1)
                        {
                            sbyte mobIndex2 = msg.reader().readByte();
                            GameScr.findMobInMap(mobIndex2).setBody(msg.reader().readShort());
                        }
                        break;
                    }
                case -84:
                    {
                        int index2 = msg.reader().readUnsignedByte();
                        Mob mob4 = null;
                        try
                        {
                            mob4 = (Mob)GameScr.vMob.elementAt(index2);
                        }
                        catch (Exception)
                        {
                        }
                        if (mob4 != null)
                        {
                            mob4.maxHp = msg.reader().readInt();
                        }
                        break;
                    }
                case -83:
                    {
                        sbyte b30 = msg.reader().readByte();
                        if (b30 == 0)
                        {
                            int num66 = msg.reader().readShort();
                            int bgRID = msg.reader().readShort();
                            int num67 = msg.reader().readUnsignedByte();
                            int num68 = msg.reader().readInt();
                            string text2 = msg.reader().readUTF();
                            int num69 = msg.reader().readShort();
                            int num70 = msg.reader().readShort();
                            sbyte b31 = msg.reader().readByte();
                            if (b31 == 1)
                            {
                                GameScr.gI().isRongNamek = true;
                            }
                            else
                            {
                                GameScr.gI().isRongNamek = false;
                            }
                            GameScr.gI().xR = num69;
                            GameScr.gI().yR = num70;
                            if (Char.myCharz().charID == num68)
                            {
                                GameCanvas.panel.hideNow();
                                GameScr.gI().activeRongThanEff(isMe: true);
                            }
                            else if (TileMap.mapID == num66 && TileMap.zoneID == num67)
                            {
                                GameScr.gI().activeRongThanEff(isMe: false);
                            }
                            else if (mGraphics.zoomLevel > 1)
                            {
                                GameScr.gI().doiMauTroi();
                            }
                            GameScr.gI().mapRID = num66;
                            GameScr.gI().bgRID = bgRID;
                            GameScr.gI().zoneRID = num67;
                        }
                        if (b30 == 1)
                        {
                            if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
                            {
                                GameScr.gI().hideRongThanEff();
                            }
                            else
                            {
                                GameScr.gI().isRongThanXuatHien = false;
                                if (GameScr.gI().isRongNamek)
                                {
                                    GameScr.gI().isRongNamek = false;
                                }
                            }
                        }
                        if (b30 != 2)
                        {
                        }
                        break;
                    }
                case -82:
                    {
                        sbyte size = msg.reader().readByte();
                        TileMap.tileIndex = new int[size][][];
                        TileMap.tileType = new int[size][];
                        for (int i = 0; i < size; i++)
                        {
                            sbyte b15 = msg.reader().readByte();
                            TileMap.tileType[i] = new int[b15];
                            TileMap.tileIndex[i] = new int[b15][];
                            for (int j = 0; j < b15; j++)
                            {
                                TileMap.tileType[i][j] = msg.reader().readInt();
                                sbyte b16 = msg.reader().readByte();
                                TileMap.tileIndex[i][j] = new int[b16];
                                for (int k = 0; k < b16; k++)
                                {
                                    TileMap.tileIndex[i][j][k] = msg.reader().readByte();
                                }
                            }
                        }
                        break;
                    }
                case -81:
                    {
                        sbyte b24 = msg.reader().readByte();
                        if (b24 == 0)
                        {
                            string src = msg.reader().readUTF();
                            string src2 = msg.reader().readUTF();
                            GameCanvas.panel.setTypeCombine();
                            GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
                            GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
                            GameCanvas.panel.show();
                        }
                        if (b24 == 1)
                        {
                            GameCanvas.panel.vItemCombine.removeAllElements();
                            sbyte b25 = msg.reader().readByte();
                            for (int num51 = 0; num51 < b25; num51++)
                            {
                                sbyte b26 = msg.reader().readByte();
                                for (int num52 = 0; num52 < Char.myCharz().arrItemBag.Length; num52++)
                                {
                                    Item item = Char.myCharz().arrItemBag[num52];
                                    if (item != null && item.indexUI == b26)
                                    {
                                        item.isSelect = true;
                                        GameCanvas.panel.vItemCombine.addElement(item);
                                    }
                                }
                            }
                            if (GameCanvas.panel.isShow)
                            {
                                GameCanvas.panel.setTabCombine();
                            }
                        }
                        if (b24 == 2)
                        {
                            GameCanvas.panel.combineSuccess = 0;
                            GameCanvas.panel.setCombineEff(0);
                        }
                        if (b24 == 3)
                        {
                            GameCanvas.panel.combineSuccess = 1;
                            GameCanvas.panel.setCombineEff(0);
                        }
                        if (b24 == 4)
                        {
                            short iconID = msg.reader().readShort();
                            GameCanvas.panel.iconID3 = iconID;
                            GameCanvas.panel.combineSuccess = 0;
                            GameCanvas.panel.setCombineEff(1);
                        }
                        if (b24 == 5)
                        {
                            short iconID2 = msg.reader().readShort();
                            GameCanvas.panel.iconID3 = iconID2;
                            GameCanvas.panel.combineSuccess = 0;
                            GameCanvas.panel.setCombineEff(2);
                        }
                        if (b24 == 6)
                        {
                            short iconID3 = msg.reader().readShort();
                            short iconID4 = msg.reader().readShort();
                            GameCanvas.panel.combineSuccess = 0;
                            GameCanvas.panel.setCombineEff(3);
                            GameCanvas.panel.iconID1 = iconID3;
                            GameCanvas.panel.iconID3 = iconID4;
                        }
                        if (b24 == 7)
                        {
                            short iconID5 = msg.reader().readShort();
                            GameCanvas.panel.iconID3 = iconID5;
                            GameCanvas.panel.combineSuccess = 0;
                            GameCanvas.panel.setCombineEff(4);
                        }
                        if (b24 == 8)
                        {
                            GameCanvas.panel.iconID3 = -1;
                            GameCanvas.panel.combineSuccess = 1;
                            GameCanvas.panel.setCombineEff(4);
                        }
                        short num53 = 21;
                        int num54 = 0;
                        int num55 = 0;
                        try
                        {
                            num53 = msg.reader().readShort();
                            num54 = msg.reader().readShort();
                            num55 = msg.reader().readShort();
                            GameCanvas.panel.xS = num54 - GameScr.cmx;
                            GameCanvas.panel.yS = num55 - GameScr.cmy;
                        }
                        catch (Exception)
                        {
                        }
                        for (int num56 = 0; num56 < GameScr.vNpc.size(); num56++)
                        {
                            Npc npc = (Npc)GameScr.vNpc.elementAt(num56);
                            if (npc.template.npcTemplateId == num53)
                            {
                                GameCanvas.panel.xS = npc.cx - GameScr.cmx;
                                GameCanvas.panel.yS = npc.cy - GameScr.cmy;
                                GameCanvas.panel.idNPC = num53;
                                break;
                            }
                        }
                        break;
                    }
        }
    }
}
