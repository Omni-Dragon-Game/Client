using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart4(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case -41:
                    {
                        sbyte b23 = msg.reader().readByte();
                        Char.myCharz().strLevel = new string[b23];
                        for (int i = 0; i < b23; i++)
                        {
                            string text = msg.reader().readUTF();
                            Char.myCharz().strLevel[i] = text;
                        }
                        Res.outz("---   xong  level caption cmd : " + msg.command);
                        break;
                    }
                case -34:
                    {
                        sbyte b12 = msg.reader().readByte();
                        Res.outz("act= " + b12);
                        if (b12 == 0 && GameScr.gI().magicTree != null)
                        {
                            Res.outz("toi duoc day");
                            MagicTree magicTree = GameScr.gI().magicTree;
                            magicTree.id = msg.reader().readShort();
                            magicTree.name = msg.reader().readUTF();
                            magicTree.name = Res.changeString(magicTree.name);
                            magicTree.x = msg.reader().readShort();
                            magicTree.y = msg.reader().readShort();
                            magicTree.level = msg.reader().readByte();
                            magicTree.currPeas = msg.reader().readShort();
                            magicTree.maxPeas = msg.reader().readShort();
                            Res.outz("curr Peas= " + magicTree.currPeas);
                            magicTree.strInfo = msg.reader().readUTF();
                            magicTree.seconds = msg.reader().readInt();
                            magicTree.timeToRecieve = magicTree.seconds;
                            sbyte b13 = msg.reader().readByte();
                            magicTree.peaPostionX = new int[b13];
                            magicTree.peaPostionY = new int[b13];
                            for (int num22 = 0; num22 < b13; num22++)
                            {
                                magicTree.peaPostionX[num22] = msg.reader().readByte();
                                magicTree.peaPostionY[num22] = msg.reader().readByte();
                            }
                            magicTree.isUpdate = msg.reader().readBool();
                            magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
                            GameScr.gI().magicTree.isUpdateTree = true;
                        }
                        if (b12 == 1)
                        {
                            myVector = new MyVector();
                            try
                            {
                                while (msg.reader().available() > 0)
                                {
                                    string caption = msg.reader().readUTF();
                                    myVector.addElement(new Command(caption, GameCanvas.instance, 888392, null));
                                }
                            }
                            catch (Exception ex4)
                            {
                                Cout.println("Loi MAGIC_TREE " + ex4.ToString());
                            }
                            GameCanvas.menu.startAt(myVector, 3);
                        }
                        if (b12 == 2)
                        {
                            GameScr.gI().magicTree.remainPeas = msg.reader().readShort();
                            GameScr.gI().magicTree.seconds = msg.reader().readInt();
                            GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
                            GameScr.gI().magicTree.isUpdateTree = true;
                            GameScr.gI().magicTree.isPeasEffect = true;
                        }
                        break;
                    }
                case 11:
                    {
                        GameCanvas.debug("SA9", 2);
                        int num9 = msg.reader().readByte();
                        sbyte b6 = msg.reader().readByte();
                        if (b6 != 0)
                        {
                            Mob.arrMobTemplate[num9].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b6);
                        }
                        else
                        {
                            Mob.arrMobTemplate[num9].data.readData(NinjaUtil.readByteArray(msg));
                        }
                        for (int i = 0; i < GameScr.vMob.size(); i++)
                        {
                            mob = (Mob)GameScr.vMob.elementAt(i);
                            if (mob.templateId == num9)
                            {
                                mob.w = Mob.arrMobTemplate[num9].data.width;
                                mob.h = Mob.arrMobTemplate[num9].data.height;
                            }
                        }
                        sbyte[] array2 = NinjaUtil.readByteArray(msg);
                        Image img = Image.createImage(array2, 0, array2.Length);
                        Mob.arrMobTemplate[num9].data.img = img;
                        int num10 = msg.reader().readByte();
                        Mob.arrMobTemplate[num9].data.typeData = num10;
                        if (num10 == 1 || num10 == 2)
                        {
                            readFrameBoss(msg, num9);
                        }
                        break;
                    }
                case -69:
                    Char.myCharz().cMaxStamina = msg.reader().readShort();
                    break;
                case -68:
                    Char.myCharz().cStamina = msg.reader().readShort();
                    break;
                case -67:
                    {

                        int iconId = msg.reader().readInt();
                        //string key = msg.reader().readUTF();
                        Debug.Log("GET ICON: " + iconId);
                        sbyte[] data = null;
                        try
                        {
                            data = NinjaUtil.readByteArray(msg);
                           // Image img = createImage(data, key);
                            Image img = createImage(data);
                            SmallImage.imgNew[iconId].img = img;
                            if (mGraphics.zoomLevel > 1)
                            {
                                SmallImage.imageRaw.Add(iconId, img);
                        }
                        }
                        catch (Exception)
                        {
                            SmallImage.imgNew[iconId].img = Image.createRGBImage(new int[1], 1, 1, bl: true);
                        }
                                                break;
                    }
                /*  case -67:
                      {
                          Res.outz("RECIEVE ICON");
                          demCount += 1f;
                          int num174 = msg.reader().readInt();
                          string key = msg.reader().readUTF();
                          sbyte[] array18 = null;
                          try
                          {
                              array18 = NinjaUtil.readByteArray(msg);
                              if (num174 == 3896)
                              {
                                  Res.outz("SIZE CHECK= " + array18.Length);
                              }
                              SmallImage.imgNew[num174].img = createImage(array18, key);
                          }
                          catch (Exception)
                          {
                              array18 = null;
                              SmallImage.imgNew[num174].img = Image.createRGBImage(new int[1], 1, 1, true);
                          }
                          break;
                      }*/

                case -66:
                    {
                        short id2 = msg.reader().readShort();
                        sbyte[] data5 = NinjaUtil.readByteArray(msg);
                        EffectData effDataById = Effect.getEffDataById(id2);
                        sbyte b62 = msg.reader().readSByte();
                        if (b62 == 0)
                        {
                            effDataById.readData(data5);
                        }
                        else
                        {
                            effDataById.readDataNewBoss(data5, b62);
                        }
                        sbyte[] array15 = NinjaUtil.readByteArray(msg);
                        effDataById.img = Image.createImage(array15, 0, array15.Length);
                        break;
                    }
                case -32:
                    {
                        short num131 = msg.reader().readShort();
                        int num132 = msg.reader().readInt();
                        sbyte[] array11 = null;
                        Image image = null;
                        try
                        {
                            array11 = new sbyte[num132];
                            for (int num133 = 0; num133 < num132; num133++)
                            {
                                array11[num133] = msg.reader().readByte();
                            }
                            image = Image.createImage(array11, 0, num132);
                            BgItem.imgNew.put(num131 + string.Empty, image);
                        }
                        catch (Exception)
                        {
                            array11 = null;
                            BgItem.imgNew.put(num131 + string.Empty, Image.createRGBImage(new int[1], 1, 1, bl: true));
                        }
                        if (array11 != null)
                        {
                            if (mGraphics.zoomLevel > 1)
                            {
                                Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num131, array11);
                            }
                            BgItemMn.blendcurrBg(num131, image);
                        }
                        break;
                    }
                case 92:
                    {
                        if (GameCanvas.currentScreen == GameScr.instance)
                        {
                            GameCanvas.endDlg();
                        }
                        string text4 = msg.reader().readUTF();
                        string str2 = msg.reader().readUTF();
                        str2 = Res.changeString(str2);
                        string empty = string.Empty;
                        Char char8 = null;
                        sbyte b46 = 0;
                        if (!text4.Equals(string.Empty))
                        {
                            char8 = new Char();
                            char8.charID = msg.reader().readInt();
                            char8.head = msg.reader().readShort();
                            char8.headICON = msg.reader().readShort();
                            char8.body = msg.reader().readShort();
                            char8.bag = msg.reader().readShort();
                            char8.leg = msg.reader().readShort();
                            b46 = msg.reader().readByte();
                            char8.cName = text4;
                            try
                            {
                                char8.isTichXanh = msg.reader().readByte() == 1;
                            }
                            catch (Exception)
                            {
                                char8.isTichXanh = false;
                            }
                        }
                        empty += str2;
                        InfoDlg.hide();
                        if (text4.Equals(string.Empty))
                        {
                            GameScr.info1.addInfo(empty, 0);
                            break;
                        }
                        GameScr.info2.addInfoWithChar(empty, char8, (b46 == 0) ? true : false);
                        if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
                        {
                            GameCanvas.panel.initLogMessage();
                        }
                        break;
                    }
                case -26:
                    ServerListScreen.testConnect = 2;
                    string msgDlg = msg.reader().readUTF();
                    if (msgDlg == "Vui lòng mở giới hạn sức mạnh" || msgDlg == "")
                    {
                        ModFunc.indexAutoPoint = -1;
                        ModFunc.pointIncrease = 0;
                        ModFunc.autoPointForPet = false;
                        GameScr.info1.addInfo("Chỉ số đã đạt tối đa", 0);
                    }
                    GameCanvas.startOKDlg(msgDlg);
                    InfoDlg.hide();
                    LoginScr.isContinueToLogin = false;
                    Char.isLoadingMap = false;
                    if (GameCanvas.currentScreen == GameCanvas.loginScr)
                    {
                        GameCanvas.serverScreen.switchToMe();
                        //Main.main.StartCoroutine(ModFunc.GI().AutoLogin2());
                    }
                    if (ModFunc.autoLogin != null)
                    {
                        ModFunc.autoLogin.waitToNextLogin = false;
                    }
                    break;
                case -25:
                    GameScr.info1.addInfo(msg.reader().readUTF(), 0);
                    break;
                case 94:
                    GameScr.info1.addInfo(msg.reader().readUTF(), 0);
                    break;
                case 47:
                    GameScr.gI().resetButton();
                    break;
                case 81:
                    {
                        Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        mob5.isDisable = msg.reader().readBool();
                        break;
                    }
                case 82:
                    {
                        Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        mob5.isDontMove = msg.reader().readBool();
                        break;
                    }
                case 85:
                    {
                        Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        mob5.isFire = msg.reader().readBool();
                        break;
                    }
                case 86:
                    {
                        Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        mob5.isIce = msg.reader().readBool();
                        if (!mob5.isIce)
                        {
                            ServerEffect.addServerEffect(77, mob5.x, mob5.y - 9, 1);
                        }
                        break;
                    }
                case 87:
                    {
                        Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        mob5.isWind = msg.reader().readBool();
                        break;
                    }
                case 56:
                    {
                        @char = null;
                        int charID = msg.reader().readInt();
                        if (charID == Char.myCharz().charID)
                        {
                            bool flag3 = false;
                            @char = Char.myCharz();
                            @char.cHP = msg.readLong();
                            long dameHit = msg.readLong();
                            if (dameHit != 0)
                            {
                                @char.doInjure();
                            }
                            try
                            {
                                flag3 = msg.reader().readBoolean();
                                sbyte effId = msg.reader().readByte();
                                if (effId != -1)
                                {
                                    EffecMn.addEff(new Effect(effId, @char.cx, @char.cy, 3, 1, -1));
                                }
                            }
                            catch (Exception)
                            {
                            }
                            if (Char.myCharz().cTypePk != 4)
                            {
                                if (dameHit == 0)
                                {
                                    GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
                                }
                                else
                                {
                                    GameScr.startFlyText("-" + dameHit, @char.cx, @char.cy - @char.ch, 0, -3, flag3 ? mFont.FATAL : mFont.RED);
                                }
                            }
                        }
                        else
                        {
                            @char = GameScr.findCharInMap(charID);
                            if (@char == null)
                            {
                                return;
                            }
                            @char.cHP = msg.readLong();
                            bool flag4 = false;
                            long dameHit1 = msg.readLong();
                            if (dameHit1 != 0)
                            {
                                @char.doInjure();
                            }
                            int num32 = 0;
                            try
                            {
                                flag4 = msg.reader().readBoolean();
                                sbyte effId = msg.reader().readByte();
                                if (effId != -1)
                                {
                                    EffecMn.addEff(new Effect(effId, @char.cx, @char.cy, 3, 1, -1));
                                }
                            }
                            catch (Exception)
                            {
                            }
                            dameHit1 += num32;
                            if (@char.cTypePk != 4)
                            {
                                if (dameHit1 == 0)
                                {
                                    GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
                                }
                                else
                                {
                                    GameScr.startFlyText("-" + dameHit1, @char.cx, @char.cy - @char.ch, 0, -3, flag4 ? mFont.FATAL : mFont.ORANGE);
                                }
                            }
                        }
                        break;
                    }
                case 83:
                    {
                        int num18 = msg.reader().readInt();
                        @char = ((num18 != Char.myCharz().charID) ? GameScr.findCharInMap(num18) : Char.myCharz());
                        if (@char == null)
                        {
                            return;
                        }
                        Mob mobToAttack = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        if (@char.mobMe != null)
                        {
                            @char.mobMe.attackOtherMob(mobToAttack);
                        }
                        break;
                    }
                case 84:
                    {
                        int num18 = msg.reader().readInt();
                        if (num18 == Char.myCharz().charID)
                        {
                            @char = Char.myCharz();
                        }
                        else
                        {
                            @char = GameScr.findCharInMap(num18);
                            if (@char == null)
                            {
                                return;
                            }
                        }
                        @char.cHP = @char.cHPFull;
                        @char.cMP = @char.cMPFull;
                        @char.cx = msg.reader().readShort();
                        @char.cy = msg.reader().readShort();
                        @char.liveFromDead();
                        break;
                    }
                case 46:
                    GameCanvas.debug("SA5", 2);
                    Cout.LogWarning("Controler RESET_POINT  " + Char.ischangingMap);
                    Char.isLockKey = false;
                    Char.myCharz().setResetPoint(msg.reader().readShort(), msg.reader().readShort());
                    break;
                case -29:
                    messageNotLogin(msg);
                    break;
                case -28:
                    messageNotMap(msg);
                    break;
                case -30:
                    messageSubCommand(msg);
                    break;
                case 62:
                    @char = GameScr.findCharInMap(msg.reader().readInt());
                    if (@char != null)
                    {
                        @char.killCharId = Char.myCharz().charID;
                        Char.myCharz().npcFocus = null;
                        Char.myCharz().mobFocus = null;
                        Char.myCharz().itemFocus = null;
                        Char.myCharz().charFocus = @char;
                        Char.isManualFocus = true;
                        GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
                    }
                    break;
                case 63:
                    Char.myCharz().killCharId = msg.reader().readInt();
                    Char.myCharz().npcFocus = null;
                    Char.myCharz().mobFocus = null;
                    Char.myCharz().itemFocus = null;
                    Char.myCharz().charFocus = GameScr.findCharInMap(Char.myCharz().killCharId);
                    Char.isManualFocus = true;
                    break;
                case 64:
                    GameCanvas.debug("SZ5", 2);
                    @char = Char.myCharz();
                    try
                    {
                        @char = GameScr.findCharInMap(msg.reader().readInt());
                    }
                    catch (Exception ex2)
                    {
                        Cout.println("Loi CLEAR_CUU_SAT " + ex2.ToString());
                    }
                    @char.killCharId = -9999;
                    break;
                case 39:
                    GameCanvas.debug("SA49", 2);
                    GameScr.gI().typeTradeOrder = 2;
                    if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
                    {
                        InfoDlg.showWait();
                    }
                    break;
                case 57:
                    {
                        GameCanvas.debug("SZ6", 2);
                        MyVector myVector2 = new MyVector();
                        myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
                        GameCanvas.menu.startAt(myVector2, 3);
                        break;
                    }
                case 58:
                    {
                        int charId = msg.reader().readInt();
                        Char char10 = (charId != Char.myCharz().charID) ? GameScr.findCharInMap(charId) : Char.myCharz();
                        char10.moveFast = new short[3];
                        char10.moveFast[0] = 0;
                        short x = msg.reader().readShort();
                        short y = msg.reader().readShort();
                        char10.moveFast[1] = x;
                        char10.moveFast[2] = y;
                        try
                        {
                            charId = msg.reader().readInt();
                            Char char11 = (charId != Char.myCharz().charID) ? GameScr.findCharInMap(charId) : Char.myCharz();
                            char11.cx = x;
                            char11.cy = y;
                        }
                        catch (Exception ex25)
                        {
                            Cout.println("Loi MOVE_FAST " + ex25.ToString());
                        }
                        break;
                    }
                case 88:
                    {
                        string info4 = msg.reader().readUTF();
                        short num170 = msg.reader().readShort();
                        GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num170), TField.INPUT_TYPE_ANY);
                        break;
                    }
                case 27:
                    {
                        myVector = new MyVector();
                        string text7 = msg.reader().readUTF();
                        int num164 = msg.reader().readByte();
                        for (int num165 = 0; num165 < num164; num165++)
                        {
                            string caption4 = msg.reader().readUTF();
                            short num166 = msg.reader().readShort();
                            myVector.addElement(new Command(caption4, GameCanvas.instance, 88819, num166));
                        }
                        GameCanvas.menu.startWithoutCloseButton(myVector, 3);
                        break;
                    }
                case 33:
                    {
                        InfoDlg.hide();
                        GameCanvas.clearKeyHold();
                        GameCanvas.clearKeyPressed();
                        myVector = new MyVector();
                        try
                        {
                            while (true)
                            {
                                string caption3 = msg.reader().readUTF();
                                myVector.addElement(new Command(caption3, GameCanvas.instance, 88822, null));
                            }
                        }
                        catch (Exception ex22)
                        {
                            Cout.println("Loi OPEN_UI_MENU " + ex22.ToString());
                        }
                        if (Char.myCharz().npcFocus == null)
                        {
                            return;
                        }
                        for (int num153 = 0; num153 < Char.myCharz().npcFocus.template.menu.Length; num153++)
                        {
                            string[] array16 = Char.myCharz().npcFocus.template.menu[num153];
                            myVector.addElement(new Command(array16[0], GameCanvas.instance, 88820, array16));
                        }
                        GameCanvas.menu.startAt(myVector, 3);
                        break;
                    }
                case 40:
                    {
                        GameCanvas.debug("SA52", 2);
                        GameCanvas.taskTick = 150;
                        short taskId = msg.reader().readShort();
                        sbyte index3 = msg.reader().readByte();
                        string str3 = msg.reader().readUTF();
                        str3 = Res.changeString(str3);
                        string str4 = msg.reader().readUTF();
                        str4 = Res.changeString(str4);
                        string[] array12 = new string[msg.reader().readByte()];
                        string[] array13 = new string[array12.Length];
                        GameScr.tasks = new int[array12.Length];
                        GameScr.mapTasks = new int[array12.Length];
                        short[] array14 = new short[array12.Length];
                        short count = -1;
                        for (int num134 = 0; num134 < array12.Length; num134++)
                        {
                            string str5 = msg.reader().readUTF();
                            str5 = Res.changeString(str5);
                            GameScr.tasks[num134] = msg.reader().readByte();
                            GameScr.mapTasks[num134] = msg.reader().readShort();
                            string str6 = msg.reader().readUTF();
                            str6 = Res.changeString(str6);
                            array14[num134] = -1;
                            if (!str5.Equals(string.Empty))
                            {
                                array12[num134] = str5;
                                array13[num134] = str6;
                            }
                        }
                        try
                        {
                            count = msg.reader().readShort();
                            for (int num135 = 0; num135 < array12.Length; num135++)
                            {
                                array14[num135] = msg.reader().readShort();
                            }
                        }
                        catch (Exception ex21)
                        {
                            Cout.println("Loi TASK_GET " + ex21.ToString());
                        }
                        Char.myCharz().taskMaint = new Task(taskId, index3, str3, str4, array12, array14, count, array13);
                        if (Char.myCharz().npcFocus != null)
                        {
                            Npc.clearEffTask();
                        }
                        Char.taskAction(isNextStep: false);
                        break;
                    }
                case 41:
                    GameCanvas.debug("SA53", 2);
                    GameCanvas.taskTick = 100;
                    Res.outz("TASK NEXT");
                    Char.myCharz().taskMaint.index++;
                    Char.myCharz().taskMaint.count = 0;
                    Npc.clearEffTask();
                    Char.taskAction(isNextStep: true);
                    break;
                case 50:
                    {
                        sbyte b59 = msg.reader().readByte();
                        Panel.vGameInfo.removeAllElements();
                        for (int num130 = 0; num130 < b59; num130++)
                        {
                            GameInfo gameInfo = new GameInfo();
                            gameInfo.id = msg.reader().readShort();
                            gameInfo.main = msg.reader().readUTF();
                            gameInfo.content = msg.reader().readUTF();
                            Panel.vGameInfo.addElement(gameInfo);
                            bool hasRead = Rms.loadRMSInt(gameInfo.id + string.Empty) != -1;
                            gameInfo.hasRead = hasRead;
                        }
                        break;
                    }
                case 43:
                    GameCanvas.taskTick = 50;
                    GameCanvas.debug("SA55", 2);
                    Char.myCharz().taskMaint.count = msg.reader().readShort();
                    if (Char.myCharz().npcFocus != null)
                    {
                        Npc.clearEffTask();
                    }
                    try
                    {
                        short num127 = msg.reader().readShort();
                        short num128 = msg.reader().readShort();
                        Char.myCharz().x_hint = num127;
                        Char.myCharz().y_hint = num128;
                        Res.outz("CMD   TASK_UPDATE:43_mapID =    x|y " + num127 + "|" + num128);
                        for (int num129 = 0; num129 < TileMap.vGo.size(); num129++)
                        {
                            Res.outz("===> " + TileMap.vGo.elementAt(num129));
                        }
                    }
                    catch (Exception)
                    {
                    }
                    break;
                case 90:
                    GameCanvas.debug("SA577", 2);
                    requestItemPlayer(msg);
                    break;
                case 29:
                    GameCanvas.debug("SA58", 2);
                    GameScr.gI().openUIZone(msg);
                    break;
                case -21:
                    {
                        GameCanvas.debug("SA60", 2);
                        short itemMapID = msg.reader().readShort();
                        for (int num123 = 0; num123 < GameScr.vItemMap.size(); num123++)
                        {
                            if (((ItemMap)GameScr.vItemMap.elementAt(num123)).itemMapID == itemMapID)
                            {
                                GameScr.vItemMap.removeElementAt(num123);
                                break;
                            }
                        }
                        break;
                    }
                case -20:
                    {
                        GameCanvas.debug("SA61", 2);
                        Char.myCharz().itemFocus = null;
                        short itemMapID = msg.reader().readShort();
                        for (int num116 = 0; num116 < GameScr.vItemMap.size(); num116++)
                        {
                            ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num116);
                            if (itemMap2.itemMapID != itemMapID)
                            {
                                continue;
                            }
                            itemMap2.setPoint(Char.myCharz().cx, Char.myCharz().cy - 10);
                            string text5 = msg.reader().readUTF();
                            num = 0;
                            try
                            {
                                num = msg.reader().readShort();
                                if (itemMap2.template.type == 9)
                                {
                                    num = msg.reader().readShort();
                                    Char.myCharz().xu += num;
                                    Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                                }
                                else if (itemMap2.template.type == 10)
                                {
                                    num = msg.reader().readShort();
                                    Char.myCharz().luong += num;
                                    Char.myCharz().luongStr = mSystem.numberTostring(Char.myCharz().luong);
                                }
                                else if (itemMap2.template.type == 34)
                                {
                                    num = msg.reader().readShort();
                                    Char.myCharz().luongKhoa += num;
                                    Char.myCharz().luongKhoaStr = mSystem.numberTostring(Char.myCharz().luongKhoa);
                                }
                            }
                            catch (Exception)
                            {
                            }
                            if (text5.Equals(string.Empty))
                            {
                                if (itemMap2.template.type == 9)
                                {
                                    GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.YELLOW);
                                    SoundMn.gI().getItem();
                                }
                                else if (itemMap2.template.type == 10)
                                {
                                    GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.GREEN);
                                    SoundMn.gI().getItem();
                                }
                                else if (itemMap2.template.type == 34)
                                {
                                    GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.RED);
                                    SoundMn.gI().getItem();
                                }
                                else
                                {
                                    GameScr.info1.addInfo(mResources.you_receive + " " + ((num <= 0) ? string.Empty : (num + " ")) + itemMap2.template.name, 0);
                                    SoundMn.gI().getItem();
                                }
                                if (num > 0 && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 4683)
                                {
                                    ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
                                    ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
                                }
                            }
                            else if (text5.Length == 1)
                            {
                                Cout.LogError3("strInf.Length =1:  " + text5);
                            }
                            else
                            {
                                GameScr.info1.addInfo(text5, 0);
                            }
                            break;
                        }
                        break;
                    }
        }
    }
}
