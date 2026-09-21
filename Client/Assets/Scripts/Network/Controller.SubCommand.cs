using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    public void messageNotMap(Message msg)
    {
        try
        {
            sbyte b = msg.reader().readByte();
            Debug.Log("messageNotMap subCmd=" + b);
            switch (b)
            {
                case 16:
                    MoneyCharge.gI().switchToMe();
                    break;
                case 17:
                    Char.myCharz().clearTask();
                    break;
                case 18:
                    {
                        GameCanvas.isLoading = false;
                        GameCanvas.endDlg();
                        int num2 = msg.reader().readInt();
                        GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
                        break;
                    }
                case 20:
                    Char.myCharz().cPk = msg.reader().readByte();
                    GameScr.info1.addInfo(mResources.PK_NOW + " " + Char.myCharz().cPk, 0);
                    break;
                case 35:
                    GameCanvas.endDlg();
                    GameScr.gI().resetButton();
                    GameScr.info1.addInfo(msg.reader().readUTF(), 0);
                    break;
                case 36:
                    GameScr.typeActive = msg.reader().readByte();
                    break;
                case 4:
                    {
                        GameCanvas.loginScr.savePass();
                        GameScr.isAutoPlay = false;
                        GameScr.canAutoPlay = false;
                        LoginScr.isUpdateAll = true;
                        LoginScr.isUpdateData = true;
                        LoginScr.isUpdateMap = true;
                        LoginScr.isUpdateSkill = true;
                        LoginScr.isUpdateItem = true;
                        GameScr.vsData = msg.reader().readByte();
                        GameScr.vsMap = msg.reader().readByte();
                        GameScr.vsSkill = msg.reader().readByte();
                        GameScr.vsItem = msg.reader().readByte();
                        sbyte b3 = msg.reader().readByte();
                        Debug.Log("VersionCheck sub4: vsData=" + GameScr.vsData + " vcData=" + GameScr.vcData
                            + " vsMap=" + GameScr.vsMap + " vcMap=" + GameScr.vcMap
                            + " vsSkill=" + GameScr.vsSkill + " vcSkill=" + GameScr.vcSkill
                            + " vsItem=" + GameScr.vsItem + " vcItem=" + GameScr.vcItem);
                        sbyte b4 = msg.reader().readByte();
                        GameScr.exps = new long[b4];
                        for (int j = 0; j < GameScr.exps.Length; j++)
                        {
                            GameScr.exps[j] = msg.reader().readLong();
                        }

                        if (GameCanvas.loginScr.isLogin2)
                        {
                            Rms.saveRMSString("acc", string.Empty);
                            Rms.saveRMSString("pass", string.Empty);
                        }
                        else
                        {
                            Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
                        }

                        // Sync vcData to vsData
                        GameScr.vcData = GameScr.vsData;

                        // Ensure cache is loaded into memory from RMS
                        if (ItemTemplates.itemTemplates == null || ItemTemplates.itemTemplates.size() == 0)
                        {
                            loadItemFromRMS();
                        }
                        if (GameScr.nClasss == null || GameScr.nClasss.Length == 0 || Skills.skills == null || Skills.skills.size() == 0)
                        {
                            loadSkillFromRMS();
                        }
                        if (TileMap.mapNames == null || TileMap.mapNames.Length == 0)
                        {
                            loadMapFromRMS();
                        }

                        bool needMap = (TileMap.mapNames == null || TileMap.mapNames.Length == 0 || GameScr.vsMap != GameScr.vcMap);
                        bool needSkill = (GameScr.nClasss == null || GameScr.nClasss.Length == 0 || Skills.skills == null || Skills.skills.size() == 0 || GameScr.vsSkill != GameScr.vcSkill);
                        bool needItem = (ItemTemplates.itemTemplates == null || ItemTemplates.itemTemplates.size() == 0 || GameScr.vsItem != GameScr.vcItem);

                        LoginScr.isUpdateMap = needMap;
                        LoginScr.isUpdateSkill = needSkill;
                        LoginScr.isUpdateItem = needItem;
                        LoginScr.isUpdateData = false;

                        if (needMap)
                        {
                            Debug.Log("Requesting updateMap() vs=" + GameScr.vsMap + " vc=" + GameScr.vcMap);
                            Service.gI().updateMap();
                        }
                        if (needSkill)
                        {
                            Debug.Log("Requesting updateSkill() vs=" + GameScr.vsSkill + " vc=" + GameScr.vcSkill);
                            Service.gI().updateSkill();
                        }
                        if (needItem)
                        {
                            Debug.Log("Requesting updateItem() vs=" + GameScr.vsItem + " vc=" + GameScr.vcItem);
                            Service.gI().updateItem();
                        }

                        checkDoneDataUpdate();
                        break;
                    }
                case 6:
                    {
                        msg.reader().mark(100000);
                        createMap(msg.reader());
                        msg.reader().reset();
                        sbyte[] data3 = new sbyte[msg.reader().available()];
                        msg.reader().readFully(ref data3);
                        Rms.saveRMS("NRmap", data3);
                        GameScr.vcMap = GameScr.vsMap; // Fix: update vcMap to match server version
                        Rms.saveRMS("NRmapVersion", new sbyte[1] { GameScr.vcMap });
                        LoginScr.isUpdateMap = false;
                        Debug.Log("Map updated, vcMap=" + GameScr.vcMap + " vsMap=" + GameScr.vsMap);
                        checkDoneDataUpdate();
                        break;
                    }
                case 7:
                    {
                        msg.reader().mark(100000);
                        createSkill(msg.reader());
                        msg.reader().reset();
                        sbyte[] data = new sbyte[msg.reader().available()];
                        msg.reader().readFully(ref data);
                        Rms.saveRMS("NRskill", data);
                        GameScr.vcSkill = GameScr.vsSkill; // Fix: update vcSkill to match server version
                        Rms.saveRMS("NRskillVersion", new sbyte[1] { GameScr.vcSkill });
                        LoginScr.isUpdateSkill = false;
                        checkDoneDataUpdate();
                        break;
                    }
                case 8:
                    Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
                    createItemNew(msg.reader());
                    GameScr.vcItem = GameScr.vsItem; // Fix: update vcItem to match server version
                    checkDoneDataUpdate();
                    break;
                case 10:
                    try
                    {
                        Char.isLoadingMap = true;
                        Res.outz("REQUEST MAP TEMPLATE");
                        GameCanvas.isLoading = true;
                        TileMap.maps = null;
                        TileMap.types = null;
                        mSystem.gcc();
                        GameCanvas.debug("SA99", 2);
                        TileMap.tmw = msg.reader().readByte();
                        TileMap.tmh = msg.reader().readByte();
                        TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
                        Res.err("   M apsize= " + TileMap.tmw * TileMap.tmh);
                        for (int i = 0; i < TileMap.maps.Length; i++)
                        {
                            int num = msg.reader().readByte();
                            if (num < 0)
                            {
                                num += 256;
                            }
                            TileMap.maps[i] = (ushort)num;
                        }
                        TileMap.types = new int[TileMap.maps.Length];
                        msg = messWait;
                        loadInfoMap(msg);
                        try
                        {
                            sbyte b2 = msg.reader().readByte();
                            TileMap.isMapDouble = ((b2 != 0) ? true : false);
                        }
                        catch (Exception ex)
                        {
                            Res.err(" 1 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex.ToString());
                        }
                    }
                    catch (Exception ex2)
                    {
                        Res.err("2 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex2.ToString());
                    }
                    msg.cleanup();
                    messWait.cleanup();
                    msg = (messWait = null);
                    GameScr.gI().switchToMe();
                    break;
                case 12:
                    GameCanvas.debug("SA10", 2);
                    break;
                case 9:
                    GameCanvas.debug("SA11", 2);
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("LOI TAI messageNotMap: " + ex.ToString());
        }
        finally
        {
            msg?.cleanup();
        }
    }

    public void messageNotLogin(Message msg)
    {
        try
        {
            sbyte b = msg.reader().readByte();
            if (b != 2)
            {
                return;
            }
            string linkDefault = msg.reader().readUTF();
            if (Rms.loadRMSInt("AdminLink") == 1)
            {
                return;
            }
            if (mSystem.clientType == 1)
            {
                ServerListScreen.linkDefault = linkDefault;
            }
            else
            {
                ServerListScreen.linkDefault = linkDefault;
            }
            mSystem.AddIpTest();
            ServerListScreen.GetServerList(ServerListScreen.linkDefault);
            try
            {
                sbyte b2 = msg.reader().readByte();
                Panel.CanNapTien = b2 == 1;
                sbyte b3 = msg.reader().readByte();
                Rms.saveRMSInt("AdminLink", b3);
            }
            catch (Exception)
            {
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            msg?.cleanup();
        }
    }

    public void messageSubCommand(Message msg)
    {
        try
        {
            sbyte b = msg.reader().readByte();
            Debug.Log("byte: " + b);
            switch (b)
            {
                case 63:
                    {
                        sbyte b5 = msg.reader().readByte();
                        if (b5 > 0)
                        {
                            GameCanvas.panel.vPlayerMenu_id.removeAllElements();
                            InfoDlg.showWait();
                            MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
                            for (int j = 0; j < b5; j++)
                            {
                                string caption = msg.reader().readUTF();
                                string caption2 = msg.reader().readUTF();
                                short num5 = msg.reader().readShort();
                                GameCanvas.panel.vPlayerMenu_id.addElement(num5 + string.Empty);
                                Char.myCharz().charFocus.menuSelect = num5;
                                Command command = new Command(caption, 11115, Char.myCharz().charFocus);
                                command.caption2 = caption2;
                                vPlayerMenu.addElement(command);
                            }
                            InfoDlg.hide();
                            GameCanvas.panel.setTabPlayerMenu();
                        }
                        break;
                    }
                case 1:
                    GameCanvas.debug("SA13", 2);
                    Char.myCharz().nClass = GameScr.nClasss[msg.reader().readByte()];
                    Char.myCharz().cTiemNang = msg.reader().readLong();
                    Char.myCharz().vSkill.removeAllElements();
                    Char.myCharz().vSkillFight.removeAllElements();
                    Char.myCharz().myskill = null;
                    break;
                case 2:
                    {
                        GameCanvas.debug("SA14", 2);
                        if (Char.myCharz().statusMe != 14 && Char.myCharz().statusMe != 5)
                        {
                            Char.myCharz().cHP = Char.myCharz().cHPFull;
                            Char.myCharz().cMP = Char.myCharz().cMPFull;
                            Cout.LogError2(" ME_LOAD_SKILL");
                        }
                        Char.myCharz().vSkill.removeAllElements();
                        Char.myCharz().vSkillFight.removeAllElements();
                        sbyte b2 = msg.reader().readByte();
                        for (sbyte b3 = 0; b3 < b2; b3++)
                        {
                            short skillId = msg.reader().readShort();
                            Skill skill2 = Skills.get(skillId);
                            useSkill(skill2);
                        }
                        GameScr.gI().sortSkill();
                        if (GameScr.isPaintInfoMe)
                        {
                            GameScr.indexRow = -1;
                            GameScr.gI().left = (GameScr.gI().center = null);
                        }
                        break;
                    }
                case 19:
                    GameCanvas.debug("SA17", 2);
                    Char.myCharz().boxSort();
                    break;
                case 21:
                    {
                        int num3 = msg.reader().readInt();
                        Char.myCharz().xuInBox -= num3;
                        Char.myCharz().xu += num3;
                        Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                        break;
                    }
                case 0:
                    {
                        if (ItemTemplates.itemTemplates == null || ItemTemplates.itemTemplates.size() == 0)
                        {
                            loadItemFromRMS();
                        }
                        if (GameScr.nClasss == null || GameScr.nClasss.Length == 0 || Skills.skills == null || Skills.skills.size() == 0)
                        {
                            loadSkillFromRMS();
                        }
                        if (TileMap.mapNames == null || TileMap.mapNames.Length == 0)
                        {
                            loadMapFromRMS();
                        }

                        RadarScr.list = new MyVector();
                        Teleport.vTeleport.removeAllElements();
                        GameScr.vCharInMap.removeAllElements();
                        GameScr.vItemMap.removeAllElements();
                        Char.vItemTime.removeAllElements();
                        GameScr.loadImg();
                        GameScr.currentCharViewInfo = Char.myCharz();
                        Char.myCharz().charID = msg.reader().readInt();
                        Char.myCharz().ctaskId = msg.reader().readByte();
                        Char.myCharz().cgender = msg.reader().readByte();
                        Char.myCharz().head = msg.reader().readShort();
                        Char.myCharz().cName = msg.reader().readUTF();
                        Char.myCharz().cPk = msg.reader().readByte();
                        Char.myCharz().cTypePk = msg.reader().readByte();
                        Char.myCharz().cPower = msg.reader().readLong();
                        Char.myCharz().applyCharLevelPercent();
                        Char.myCharz().eff5BuffHp = msg.reader().readShort();
                        Char.myCharz().eff5BuffMp = msg.reader().readShort();
                        sbyte classId = msg.reader().readByte();
                        if (GameScr.nClasss != null && classId >= 0 && classId < GameScr.nClasss.Length)
                        {
                            Char.myCharz().nClass = GameScr.nClasss[classId];
                        }
                        Char.myCharz().vSkill.removeAllElements();
                        Char.myCharz().vSkillFight.removeAllElements();
                        GameScr.gI().dHP = Char.myCharz().cHP;
                        GameScr.gI().dMP = Char.myCharz().cMP;
                        sbyte b2 = msg.reader().readByte();
                        for (sbyte b6 = 0; b6 < b2; b6++)
                        {
                            Skill skill3 = Skills.get(msg.reader().readShort());
                            if (skill3 != null)
                            {
                                useSkill(skill3);
                            }
                        }
                        GameScr.gI().sortSkill();
                        GameScr.gI().loadSkillShortcut();
                        Char.myCharz().xu = msg.reader().readLong();
                        Char.myCharz().luongKhoa = msg.reader().readInt();
                        Char.myCharz().luong = msg.reader().readInt();
                        Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                        Char.myCharz().luongStr = mSystem.numberTostring(Char.myCharz().luong);
                        Char.myCharz().luongKhoaStr = mSystem.numberTostring(Char.myCharz().luongKhoa);
                        Char.myCharz().arrItemBody = new Item[msg.reader().readByte()];
                        try
                        {
                            Char.myCharz().setDefaultPart();
                            for (int k = 0; k < Char.myCharz().arrItemBody.Length; k++)
                            {
                                short num6 = msg.reader().readShort();
                                if (num6 == -1)
                                {
                                    continue;
                                }
                                ItemTemplate itemTemplate = ItemTemplates.get(num6);
                                int num7 = (itemTemplate != null) ? itemTemplate.type : -1;
                                Char.myCharz().arrItemBody[k] = new Item();
                                Char.myCharz().arrItemBody[k].template = itemTemplate;
                                Char.myCharz().arrItemBody[k].quantity = msg.reader().readInt();
                                Char.myCharz().arrItemBody[k].info = msg.reader().readUTF();
                                Char.myCharz().arrItemBody[k].content = msg.reader().readUTF();
                                int num8 = msg.reader().readUnsignedByte();
                                if (num8 != 0)
                                {
                                    Char.myCharz().arrItemBody[k].itemOption = new ItemOption[num8];
                                    for (int l = 0; l < Char.myCharz().arrItemBody[k].itemOption.Length; l++)
                                    {
                                        int num9 = msg.reader().readUnsignedByte();
                                        int param = msg.reader().readUnsignedShort();
                                        if (num9 != -1)
                                        {
                                            Char.myCharz().arrItemBody[k].itemOption[l] = new ItemOption(num9, param);
                                        }
                                    }
                                }
                                if (itemTemplate != null)
                                {
                                    switch (num7)
                                    {
                                        case 0:
                                            Char.myCharz().body = itemTemplate.part;
                                            break;
                                        case 1:
                                            Char.myCharz().leg = itemTemplate.part;
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        Char.myCharz().arrItemBag = new Item[msg.reader().readByte()];
                        GameScr.hpPotion = 0;
                        for (int m = 0; m < Char.myCharz().arrItemBag.Length; m++)
                        {
                            short num10 = msg.reader().readShort();
                            if (num10 == -1)
                            {
                                continue;
                            }
                            Char.myCharz().arrItemBag[m] = new Item();
                            Char.myCharz().arrItemBag[m].template = ItemTemplates.get(num10);
                            Char.myCharz().arrItemBag[m].quantity = msg.reader().readInt();
                            Char.myCharz().arrItemBag[m].info = msg.reader().readUTF();
                            Char.myCharz().arrItemBag[m].content = msg.reader().readUTF();
                            Char.myCharz().arrItemBag[m].indexUI = m;
                            sbyte b7 = msg.reader().readByte();
                            if (b7 != 0)
                            {
                                Char.myCharz().arrItemBag[m].itemOption = new ItemOption[b7];
                                for (int n = 0; n < Char.myCharz().arrItemBag[m].itemOption.Length; n++)
                                {
                                    int num11 = msg.reader().readUnsignedByte();
                                    int param2 = msg.reader().readUnsignedShort();
                                    if (num11 != -1)
                                    {
                                        Char.myCharz().arrItemBag[m].itemOption[n] = new ItemOption(num11, param2);
                                        Char.myCharz().arrItemBag[m].getCompare();
                                    }
                                }
                            }
                            if (Char.myCharz().arrItemBag[m].template != null && Char.myCharz().arrItemBag[m].template.type == 6)
                            {
                                GameScr.hpPotion += Char.myCharz().arrItemBag[m].quantity;
                            }
                        }
                        Char.myCharz().arrItemBox = new Item[msg.reader().readByte()];
                        GameCanvas.panel.hasUse = 0;
                        for (int num12 = 0; num12 < Char.myCharz().arrItemBox.Length; num12++)
                        {
                            short num13 = msg.reader().readShort();
                            if (num13 == -1)
                            {
                                continue;
                            }
                            Char.myCharz().arrItemBox[num12] = new Item();
                            Char.myCharz().arrItemBox[num12].template = ItemTemplates.get(num13);
                            Char.myCharz().arrItemBox[num12].quantity = msg.reader().readInt();
                            Char.myCharz().arrItemBox[num12].info = msg.reader().readUTF();
                            Char.myCharz().arrItemBox[num12].content = msg.reader().readUTF();
                            Char.myCharz().arrItemBox[num12].itemOption = new ItemOption[msg.reader().readByte()];
                            for (int num14 = 0; num14 < Char.myCharz().arrItemBox[num12].itemOption.Length; num14++)
                            {
                                int num15 = msg.reader().readUnsignedByte();
                                int param3 = msg.reader().readUnsignedShort();
                                if (num15 != -1)
                                {
                                    Char.myCharz().arrItemBox[num12].itemOption[num14] = new ItemOption(num15, param3);
                                    Char.myCharz().arrItemBox[num12].getCompare();
                                }
                            }
                            GameCanvas.panel.hasUse++;
                        }
                        Char.myCharz().statusMe = 4;
                        int num16 = Rms.loadRMSInt(Char.myCharz().cName + "vci");
                        if (num16 < 1)
                        {
                            GameScr.isViewClanInvite = false;
                        }
                        else
                        {
                            GameScr.isViewClanInvite = true;
                        }
                        short num17 = msg.reader().readShort();
                        Char.idHead = new short[num17];
                        Char.idAvatar = new short[num17];
                        for (int num18 = 0; num18 < num17; num18++)
                        {
                            Char.idHead[num18] = msg.reader().readShort();
                            Char.idAvatar[num18] = msg.reader().readShort();
                        }
                        for (int num19 = 0; num19 < GameScr.info1.charId.Length; num19++)
                        {
                            GameScr.info1.charId[num19] = new int[3];
                        }
                        GameScr.info1.charId[Char.myCharz().cgender][0] = msg.reader().readShort();
                        GameScr.info1.charId[Char.myCharz().cgender][1] = msg.reader().readShort();
                        GameScr.info1.charId[Char.myCharz().cgender][2] = msg.reader().readShort();
                        Char.myCharz().isNhapThe = msg.reader().readByte() == 1;
                        GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
                        GameScr.isNewMember = msg.reader().readByte();
                        Char.myCharz().isTichXanh = GameScr.isNewMember == 1;
                        Service.gI().updateCaption((sbyte)Char.myCharz().cgender);
                        Service.gI().androidPack();
                        try
                        {
                            Char.myCharz().idAuraEff = msg.reader().readShort();
                            Char.myCharz().idEff_Set_Item = msg.reader().readSByte();
                            Char.myCharz().idHat = msg.reader().readShort();
                            break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                case 4:
                    Char.myCharz().xu = msg.reader().readLong();
                    Char.myCharz().luong = msg.reader().readInt();
                    Char.myCharz().cHP = msg.readLong();
                    Char.myCharz().cMP = msg.readLong();
                    Char.myCharz().luongKhoa = msg.reader().readInt();
                    Char.myCharz().xuStr = mSystem.numberTostring(Char.myCharz().xu);
                    Char.myCharz().luongStr = mSystem.numberTostring(Char.myCharz().luong);
                    Char.myCharz().luongKhoaStr = mSystem.numberTostring(Char.myCharz().luongKhoa);
                    break;
                case 5:
                    {
                        long cHP = Char.myCharz().cHP;
                        Char.myCharz().cHP = msg.readLong();
                        if (Char.myCharz().cHP > cHP && Char.myCharz().cTypePk != 4)
                        {
                            GameScr.startFlyText("+" + (Char.myCharz().cHP - cHP) + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
                            SoundMn.gI().HP_MPup();
                            if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5003)
                            {
                                MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? (-10) : 10), Char.myCharz().petFollow.cmy + 10, isBoss: true, -1, -1, Char.myCharz(), 29);
                            }
                        }
                        if (Char.myCharz().cHP < cHP)
                        {
                            GameScr.startFlyText("-" + (cHP - Char.myCharz().cHP) + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
                        }
                        GameScr.gI().dHP = Char.myCharz().cHP;
                        if (GameScr.isPaintInfoMe)
                        {
                        }
                        break;
                    }
                case 6:
                    {
                        if (Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5)
                        {
                            break;
                        }
                        long cMP = Char.myCharz().cMP;
                        Char.myCharz().cMP = msg.readLong();
                        if (Char.myCharz().cMP > cMP)
                        {
                            GameScr.startFlyText("+" + (Char.myCharz().cMP - cMP) + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
                            SoundMn.gI().HP_MPup();
                            if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5001)
                            {
                                MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? (-10) : 10), Char.myCharz().petFollow.cmy + 10, isBoss: true, -1, -1, Char.myCharz(), 29);
                            }
                        }
                        if (Char.myCharz().cMP < cMP)
                        {
                            GameScr.startFlyText("-" + (cMP - Char.myCharz().cMP) + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
                        }
                        GameScr.gI().dMP = Char.myCharz().cMP;
                        if (GameScr.isPaintInfoMe)
                        {
                        }
                        break;
                    }
                case 7:
                    {
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char == null)
                        {
                            break;
                        }
                        @char.clanID = msg.reader().readInt();
                        if (@char.clanID == -2)
                        {
                            @char.isCopy = true;
                        }
                        readCharInfo(@char, msg);
                        try
                        {
                            @char.idAuraEff = msg.reader().readShort();
                            @char.idEff_Set_Item = msg.reader().readSByte();
                            @char.idHat = msg.reader().readShort();
                            if (@char.bag >= 201)
                            {
                                Effect effect = new Effect(@char.bag, @char, 2, -1, 10, 1);
                                effect.typeEff = 5;
                                @char.addEffChar(effect);
                            }
                            else
                            {
                                @char.removeEffChar(0, 201);
                            }
                            break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                case 8:
                    {
                        GameCanvas.debug("SA26", 2);
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cspeed = msg.reader().readByte();
                        }
                        break;
                    }
                case 9:
                    {
                        GameCanvas.debug("SA27", 2);
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cHP = msg.readInt3Byte();
                            @char.cHPFull = msg.readInt3Byte();
                        }
                        break;
                    }
                case 10:
                    {
                        GameCanvas.debug("SA28", 2);
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cHP = msg.readInt3Byte();
                            @char.cHPFull = msg.readInt3Byte();
                            @char.eff5BuffHp = msg.reader().readShort();
                            @char.eff5BuffMp = msg.reader().readShort();
                            @char.wp = msg.reader().readShort();
                            if (@char.wp == -1)
                            {
                                @char.setDefaultWeapon();
                            }
                        }
                        break;
                    }
                case 11:
                    {
                        GameCanvas.debug("SA29", 2);
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cHP = msg.readInt3Byte();
                            @char.cHPFull = msg.readInt3Byte();
                            @char.eff5BuffHp = msg.reader().readShort();
                            @char.eff5BuffMp = msg.reader().readShort();
                            @char.body = msg.reader().readShort();
                            if (@char.body == -1)
                            {
                                @char.setDefaultBody();
                            }
                        }
                        break;
                    }
                case 12:
                    {
                        GameCanvas.debug("SA30", 2);
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cHP = msg.readInt3Byte();
                            @char.cHPFull = msg.readInt3Byte();
                            @char.eff5BuffHp = msg.reader().readShort();
                            @char.eff5BuffMp = msg.reader().readShort();
                            @char.leg = msg.reader().readShort();
                            if (@char.leg == -1)
                            {
                                @char.setDefaultLeg();
                            }
                        }
                        break;
                    }
                case 13:
                    {
                        GameCanvas.debug("SA31", 2);
                        int num2 = msg.reader().readInt();
                        Char @char = ((num2 != Char.myCharz().charID) ? GameScr.findCharInMap(num2) : Char.myCharz());
                        if (@char != null)
                        {
                            @char.cHP = msg.readInt3Byte();
                            @char.cHPFull = msg.readInt3Byte();
                            @char.eff5BuffHp = msg.reader().readShort();
                            @char.eff5BuffMp = msg.reader().readShort();
                        }
                        break;
                    }
                case 14:
                    {
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char == null)
                        {
                            break;
                        }
                        @char.cHP = msg.readLong();
                        sbyte b4 = msg.reader().readByte();
                        if (b4 == 1)
                        {
                            ServerEffect.addServerEffect(11, @char, 5);
                            ServerEffect.addServerEffect(104, @char, 4);
                        }
                        if (b4 == 2)
                        {
                            @char.doInjure();
                        }
                        try
                        {
                            @char.cHPFull = msg.readLong();
                            break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                case 15:
                    {
                        Char @char = GameScr.findCharInMap(msg.reader().readInt());
                        if (@char != null)
                        {
                            @char.cHP = msg.readLong();
                            @char.cHPFull = msg.readLong();
                            @char.cx = msg.reader().readShort();
                            @char.cy = msg.reader().readShort();
                            @char.statusMe = 1;
                            @char.cp3 = 3;
                            ServerEffect.addServerEffect(109, @char, 2);
                        }
                        break;
                    }
                case 35:
                    {
                        GameCanvas.debug("SY3", 2);
                        int num4 = msg.reader().readInt();
                        Res.outz("CID = " + num4);
                        if (TileMap.mapID == 130)
                        {
                            GameScr.gI().starVS();
                        }
                        if (num4 == Char.myCharz().charID)
                        {
                            Char.myCharz().cTypePk = msg.reader().readByte();
                            if (GameScr.gI().isVS() && Char.myCharz().cTypePk != 0)
                            {
                                GameScr.gI().starVS();
                            }
                            Res.outz("type pk= " + Char.myCharz().cTypePk);
                            Char.myCharz().npcFocus = null;
                            if (!GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus))
                            {
                                Char.myCharz().mobFocus = null;
                            }
                            Char.myCharz().itemFocus = null;
                        }
                        else
                        {
                            Char @char = GameScr.findCharInMap(num4);
                            if (@char != null)
                            {
                                Res.outz("type pk= " + @char.cTypePk);
                                @char.cTypePk = msg.reader().readByte();
                                if (@char.isAttacPlayerStatus())
                                {
                                    Char.myCharz().charFocus = @char;
                                }
                            }
                        }
                        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                        {
                            Char char2 = GameScr.findCharInMap(i);
                            if (char2 != null && char2.cTypePk != 0 && char2.cTypePk == Char.myCharz().cTypePk)
                            {
                                if (!Char.myCharz().mobFocus.isMobMe)
                                {
                                    Char.myCharz().mobFocus = null;
                                }
                                Char.myCharz().npcFocus = null;
                                Char.myCharz().itemFocus = null;
                                break;
                            }
                        }
                        Res.outz("update type pk= ");
                        break;
                    }
                case 61:
                    {
                        string text = msg.reader().readUTF();
                        sbyte[] data = new sbyte[msg.reader().readInt()];
                        msg.reader().read(ref data);
                        if (data.Length == 0)
                        {
                            data = null;
                        }
                        if (text.Equals("KSkill"))
                        {
                            GameScr.gI().onKSkill(data);
                        }
                        else if (text.Equals("OSkill"))
                        {
                            GameScr.gI().onOSkill(data);
                        }
                        else if (text.Equals("CSkill"))
                        {
                            GameScr.gI().onCSkill(data);
                        }
                        break;
                    }
                case 23:
                    {
                        short num = msg.reader().readShort();
                        Skill skill = Skills.get(num);
                        useSkill(skill);
                        if (num != 0 && num != 14 && num != 28)
                        {
                            GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill.template.name, 0);
                        }
                        break;
                    }
                case 62:
                    Res.outz("ME UPDATE SKILL");
                    read_UpdateSkill(msg);
                    break;
            }
        }
        catch (Exception ex5)
        {
            Cout.println("Loi tai Sub : " + ex5.ToString());
        }
        finally
        {
            msg?.cleanup();
        }
    }
}
