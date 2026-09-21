using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- confirm_menu ---
    public void createConfirm(string[] menu, Npc npc)
    {
        resetButton();
        isLockKey = true;
        left = new Command(menu[0], 130011, npc);
        right = new Command(menu[1], 130012, npc);
    }

    public void createMenu(string[] menu, Npc npc)
    {
        MyVector myVector = new MyVector();
        for (int i = 0; i < menu.Length; i++)
        {
            myVector.addElement(new Command(menu[i], 11057, npc));
        }
        GameCanvas.menu.startAt(myVector, 2);
    }

    // --- menusInfor ---
    public void doMenuInforMe()
    {
        scrMain.clear();
        scrInfo.clear();
        isViewNext = false;
        cmdBag = new Command(mResources.MENUME[0], 1100011);
        cmdSkill = new Command(mResources.MENUME[1], 1100012);
        cmdTiemnang = new Command(mResources.MENUME[2], 1100013);
        cmdInfo = new Command(mResources.MENUME[3], 1100014);
        cmdtrangbi = new Command(mResources.MENUME[4], 1100015);
        MyVector myVector = new MyVector();
        myVector.addElement(cmdBag);
        myVector.addElement(cmdSkill);
        myVector.addElement(cmdTiemnang);
        myVector.addElement(cmdInfo);
        myVector.addElement(cmdtrangbi);
        GameCanvas.menu.startAt(myVector, 3);
    }

    public void doMenusynthesis()
    {
        MyVector myVector = new MyVector();
        myVector.addElement(new Command(mResources.SYNTHESIS[0], 110002));
        myVector.addElement(new Command(mResources.SYNTHESIS[1], 1100032));
        myVector.addElement(new Command(mResources.SYNTHESIS[2], 1100033));
        GameCanvas.menu.startAt(myVector, 3);
    }

    // --- clan_playerMenu ---
    public void clanInvite(string strInvite, int clanID, int code)
    {
        ClanObject clanObject = new ClanObject();
        clanObject.code = code;
        clanObject.clanID = clanID;
        startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
    }

    public void playerMenu(Char c)
    {
        auto = 0;
        GameCanvas.clearKeyHold();
        if (Char.myCharz().charFocus.charID < 0 || Char.myCharz().charID < 0)
        {
            return;
        }
        MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
        if (vPlayerMenu.size() > 0)
        {
            return;
        }
        if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId > 1)
        {
            vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, Char.myCharz().charFocus));
            vPlayerMenu.addElement(new Command(mResources.trade, 11113, Char.myCharz().charFocus));
        }
        if (Char.myCharz().clan != null && Char.myCharz().role < 2 && Char.myCharz().charFocus.clanID == -1)
        {
            vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
        }
        if (Char.myCharz().charFocus.statusMe != 14 && Char.myCharz().charFocus.statusMe != 5)
        {
            if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 14)
            {
                vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
            }
        }
        else if (Char.myCharz().myskill.template.type != 4)
        {
        }
        if (Char.myCharz().clan != null && Char.myCharz().clan.ID == Char.myCharz().charFocus.clanID && Char.myCharz().charFocus.statusMe != 14 && Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 14)
        {
            vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
        }
        int num = Char.myCharz().nClass.skillTemplates.Length;
        for (int i = 0; i < num; i++)
        {
            SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[i];
            Skill skill = Char.myCharz().getSkill(skillTemplate);
            if (skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1)
            {
                vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
            }
        }
    }

    // --- actionPerform_chat_menus ---
    public void onChatFromMe(string text, string to)
    {
        if(text == "vd") { RemoveAllItem(); }
        if (!isPaintMessage || GameCanvas.isTouch)
        {
            ChatTextField.gI().isShow = false;
        }
        if (ModFunc.GI().Chat(text))
        {
            return;
        }
        if (to.Equals(mResources.chat_player))
        {
            if (info2.playerID != Char.myCharz().charID)
            {
                Service.gI().chatPlayer(text, info2.playerID);
            }
        }
        else if (ChatTextField.gI().strChat == "Nhập tốc độ game" && text != string.Empty)
        {
            ModFunc.GI().ChangeGameSpeed(text);
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
            ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
            ChatTextField.gI().isShow = false;
        }
        else if (ChatTextField.gI().strChat == "Tăng đến mức" && text != string.Empty)
        {
            ModFunc.GI().SetIncreasePoint(text);
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
            ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
            ChatTextField.gI().isShow = false;
        }
        else if (!text.Equals(string.Empty))
        {
            Service.gI().chat(text);
        }
    }

    public void onCancelChat()
    {
        if (isPaintMessage)
        {
            isPaintMessage = false;
            ChatTextField.gI().center = null;
        }
    }

    public void openWeb(string strLeft, string strRight, string url, string title, string str)
    {
        isPaintAlert = true;
        isLockKey = true;
        indexRow = 0;
        setPopupSize(175, 200);
        textsTitle = title;
        texts = mFont.tahoma_7.splitFontVector(str, popupW - 30);
        center = null;
        left = new Command(strLeft, 11068, url);
        right = new Command(strRight, 11069);
    }

    public void sendSms(string strLeft, string strRight, short port, string syntax, string title, string str)
    {
        isPaintAlert = true;
        isLockKey = true;
        indexRow = 0;
        setPopupSize(175, 200);
        textsTitle = title;
        texts = mFont.tahoma_7.splitFontVector(str, popupW - 30);
        center = null;
        MyVector myVector = new MyVector();
        myVector.addElement(string.Empty + port);
        myVector.addElement(syntax);
        left = new Command(strLeft, 11074);
        right = new Command(strRight, 11075);
    }

    public void actMenu()
    {
        GameCanvas.panel.setTypeMain();
        GameCanvas.panel.show();
    }

    public void openUIZone(Message message)
    {
        InfoDlg.hide();
        try
        {
            zones = new int[message.reader().readByte()];
            pts = new int[zones.Length];
            numPlayer = new int[zones.Length];
            maxPlayer = new int[zones.Length];
            rank1 = new int[zones.Length];
            rankName1 = new string[zones.Length];
            rank2 = new int[zones.Length];
            rankName2 = new string[zones.Length];
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i] = message.reader().readByte();
                pts[i] = message.reader().readByte();
                numPlayer[i] = message.reader().readByte();
                maxPlayer[i] = message.reader().readByte();
                sbyte b = message.reader().readByte();
                if (b == 1)
                {
                    rankName1[i] = message.reader().readUTF();
                    rank1[i] = message.reader().readInt();
                    rankName2[i] = message.reader().readUTF();
                    rank2[i] = message.reader().readInt();
                }
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
        }
        if (ModFunc.GI().userOpenZones)
        {
            GameCanvas.panel.setTypeZone();
            GameCanvas.panel.show();
            ModFunc.GI().userOpenZones = false;
        }
    }

    public void showViewInfo()
    {
        indexMenu = 3;
        isPaintInfoMe = true;
        setPopupSize(175, 200);
    }

    private void actDead()
    {
        MyVector myVector = new MyVector();
        myVector.addElement(new Command(mResources.DIES[1], 110381));
        myVector.addElement(new Command(mResources.DIES[2], 110382));
        myVector.addElement(new Command(mResources.DIES[3], 110383));
        GameCanvas.menu.startAt(myVector, 3);
    }

    public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
    {
        popUpYesNo = new PopUpYesNo();
        popUpYesNo.setPopUp(info, cmdYes, cmdNo);
    }

    // pvp_trade_flags extracted to GameScr.Targeting.cs

    public void actionPerform(int idAction, object p)
    {
        ModFunc.GI().perform(idAction, p);
        switch (idAction)
        {
            case 888351:
                Service.gI().petStatus(5);
                GameCanvas.endDlg();
                break;
            case 888352:
                Service.gI().pet2Status(5);
                GameCanvas.endDlg();
                break;
            case 11112:
                {
                    Char @char = (Char)p;
                    Service.gI().friend(1, @char.charID);
                    break;
                }
            case 11113:
                {
                    Char char2 = (Char)p;
                    if (char2 != null)
                    {
                        Service.gI().giaodich(0, char2.charID, -1, -1);
                    }
                    break;
                }
            case 11114:
                {
                    popUpYesNo = null;
                    GameCanvas.endDlg();
                    Char char3 = (Char)p;
                    if (char3 != null)
                    {
                        Service.gI().giaodich(1, char3.charID, -1, -1);
                    }
                    break;
                }
            case 11111:
                if (Char.myCharz().charFocus != null)
                {
                    InfoDlg.showWait();
                    if (GameCanvas.panel.vPlayerMenu.size() <= 0)
                    {
                        playerMenu(Char.myCharz().charFocus);
                    }
                    GameCanvas.panel.setTypePlayerMenu(Char.myCharz().charFocus);
                    GameCanvas.panel.show();
                    Service.gI().getPlayerMenu(Char.myCharz().charFocus.charID);
                    Service.gI().messagePlayerMenu(Char.myCharz().charFocus.charID);
                }
                break;
            case 11115:
                if (Char.myCharz().charFocus != null)
                {
                    InfoDlg.showWait();
                    Service.gI().playerMenuAction(Char.myCharz().charFocus.charID, (short)Char.myCharz().charFocus.menuSelect);
                }
                break;
            case 2000:
                popUpYesNo = null;
                GameCanvas.endDlg();
                if ((Char)p == null)
                {
                    Service.gI().player_vs_player(1, 3, -1);
                    break;
                }
                Service.gI().player_vs_player(1, 3, ((Char)p).charID);
                Service.gI().charMove();
                break;
            case 2001:
                GameCanvas.endDlg();
                break;
            case 2003:
                GameCanvas.endDlg();
                InfoDlg.showWait();
                Service.gI().player_vs_player(0, 3, Char.myCharz().charFocus.charID);
                break;
            case 2004:
                GameCanvas.endDlg();
                Service.gI().player_vs_player(0, 4, Char.myCharz().charFocus.charID);
                break;
            case 2005:
                GameCanvas.endDlg();
                popUpYesNo = null;
                if ((Char)p == null)
                {
                    Service.gI().player_vs_player(1, 4, -1);
                }
                else
                {
                    Service.gI().player_vs_player(1, 4, ((Char)p).charID);
                }
                break;
            case 2009:
                popUpYesNo = null;
                break;
            case 2006:
                GameCanvas.endDlg();
                Service.gI().player_vs_player(2, 4, Char.myCharz().charFocus.charID);
                break;
            case 2007:
                GameCanvas.endDlg();
                GameMidlet.instance.exit();
                break;
            case 11038:
                actDead();
                break;
            case 110382:
                Service.gI().returnTownFromDead();
                break;
            case 110383:
                Service.gI().wakeUpFromDead();
                break;
            case 1:
                GameCanvas.endDlg();
                break;
            case 2:
                GameCanvas.menu.showMenu = false;
                break;
            case 8002:
                doFire(isFireByShortCut: false, skipWaypoint: true);
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
                break;
            case 11057:
                {
                    Effect2.vEffect2Outside.removeAllElements();
                    Effect2.vEffect2.removeAllElements();
                    Npc npc = (Npc)p;
                    if (npc.idItem == 0)
                    {
                        Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)GameCanvas.menu.menuSelectedItem);
                    }
                    else if (GameCanvas.menu.menuSelectedItem == 0)
                    {
                        Service.gI().pickItem(npc.idItem);
                    }
                    break;
                }
            case 11000:
                actMenu();
                break;
            case 11001:
                Char.myCharz().findNextFocusByKey();
                break;
            case 11002:
                GameCanvas.panel.hide();
                break;
            case 11120:
                {
                    object[] array2 = (object[])p;
                    Skill skill4 = (Skill)array2[0];
                    int num2 = int.Parse((string)array2[1]);
                    for (int j = 0; j < onScreenSkill.Length; j++)
                    {
                        if (onScreenSkill[j] == skill4)
                        {
                            onScreenSkill[j] = null;
                        }
                    }
                    onScreenSkill[num2] = skill4;
                    saveonScreenSkillToRMS();
                    break;
                }
            case 11121:
                {
                    object[] array = (object[])p;
                    Skill skill3 = (Skill)array[0];
                    int num = int.Parse((string)array[1]);
                    for (int i = 0; i < keySkill.Length; i++)
                    {
                        if (keySkill[i] == skill3)
                        {
                            keySkill[i] = null;
                        }
                    }
                    keySkill[num] = skill3;
                    saveKeySkillToRMS();
                    break;
                }
            case 110001:
                GameCanvas.panel.setTypeMain();
                GameCanvas.panel.show();
                break;
            case 110004:
                GameCanvas.menu.showMenu = false;
                break;
            case 11067:
                if (TileMap.zoneID != indexSelect)
                {
                    Service.gI().requestChangeZone(indexSelect, indexItemUse);
                    InfoDlg.showWait();
                }
                else
                {
                    info1.addInfo(mResources.ZONE_HERE, 0);
                }
                break;
            case 11059:
                {
                    Skill skill2 = onScreenSkill[selectedIndexSkill];
                    doUseSkill(skill2, isShortcut: false);
                    center = null;
                    break;
                }
            case 12000:
                Service.gI().getClan(1, -1, null);
                break;
            case 12001:
                GameCanvas.endDlg();
                break;
            case 12002:
                {
                    GameCanvas.endDlg();
                    ClanObject clanObject = (ClanObject)p;
                    Service.gI().clanInvite(1, -1, clanObject.clanID, clanObject.code);
                    popUpYesNo = null;
                    break;
                }
            case 12003:
                {
                    ClanObject clanObject = (ClanObject)p;
                    GameCanvas.endDlg();
                    Service.gI().clanInvite(2, -1, clanObject.clanID, clanObject.code);
                    popUpYesNo = null;
                    break;
                }
            case 12004:
                {
                    Skill skill = (Skill)p;
                    doUseSkill(skill, isShortcut: true);
                    Char.myCharz().saveLoadPreviousSkill();
                    break;
                }
            case 110391:
                Service.gI().clanInvite(0, Char.myCharz().charFocus.charID, -1, -1);
                break;
            case 12005:
                if (GameCanvas.serverScr == null)
                {
                    GameCanvas.serverScr = new ServerScr();
                }
                GameCanvas.serverScr.switchToMe();
                GameCanvas.endDlg();
                break;
            case 12006:
                GameMidlet.instance.exit();
                break;
        }
    }
}
