using System;
using Assets.src.g;
using Mod;

public partial class Panel
{
    // ==================== setTypeFlag ====================
    public void setTypeFlag()
    {
        type = 18;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabFlag();
    }


    // ==================== setTabFlag ====================
    public void setTabFlag()
    {
        currentListLength = vFlag.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        if (selected > currentListLength - 1)
        {
            selected = currentListLength - 1;
        }
        cmx = (cmtoX = 0);
    }


    // ==================== setTypeAuto ====================
    public void setTypeAuto()
    {
        type = 22;
        setType(0);
        setTabAuto();
        cmx = (cmtoX = 0);
    }


    // ==================== setTabAuto ====================
    private void setTabAuto()
    {
        currentListLength = strAuto.Length;
        ITEM_HEIGHT = 29;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
    }


    // ==================== setTabTool ====================
    private void setTabTool()
    {
        SoundMn.gI().getSoundOption();
        currentListLength = strTool.Length;
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
    }


    // ==================== paintAuto ====================
    private void paintAuto(mGraphics g)
    {
    }


    // ==================== paintTools ====================
    private void paintTools(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strTool.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, h);
            mFont.tahoma_7b_dark.drawString(g, strTool[i], xScroll + wScroll / 2, num2 + 6, mFont.CENTER);
            if (!strTool[i].Equals(mResources.gameInfo))
            {
                continue;
            }
            for (int j = 0; j < vGameInfo.size(); j++)
            {
                GameInfo gameInfo = (GameInfo)vGameInfo.elementAt(j);
                if (!gameInfo.hasRead)
                {
                    if (GameCanvas.gameTick % 20 > 10)
                    {
                        g.drawImage(imgNew, num + 10, num2 + 10, 3);
                    }
                    break;
                }
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintGameSubInfo ====================
    private void paintGameSubInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < contenInfo.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * 15;
            int num3 = wScroll - 1;
            int num4 = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                mFont.tahoma_7b_dark.drawString(g, contenInfo[i], xScroll + 5, num2 + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintPlayerInfo ====================
    private void paintPlayerInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < contenInfo.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * 15;
            int num3 = wScroll - 1;
            int num4 = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                mFont.tahoma_7b_dark.drawString(g, contenInfo[i], xScroll + 5, num2 + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintGameInfo ====================
    private void paintGameInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < vGameInfo.size(); i++)
        {
            GameInfo gameInfo = (GameInfo)vGameInfo.elementAt(i);
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(num, num2, num3, h);
                mFont.tahoma_7b_dark.drawString(g, gameInfo.main, xScroll + wScroll / 2, num2 + 6, mFont.CENTER);
                if (!gameInfo.hasRead && GameCanvas.gameTick % 20 > 10)
                {
                    g.drawImage(imgNew, num + 10, num2 + 10, 3);
                }
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintFlagChange ====================
    private void paintFlagChange(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll + 26;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 26;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = 24;
            int num7 = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, h);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num4, num5, num6, num7);
            Item item = (Item)vFlag.elementAt(i);
            if (item == null)
            {
                continue;
            }
            mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
            string text = string.Empty;
            if (item.itemOption != null && item.itemOption.Length >= 1)
            {
                if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
                {
                    text += item.itemOption[0].getOptionString();
                }
                mFont tahoma_7_blue = mFont.tahoma_7_blue;
                tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
                SmallImage.drawSmallImage(g, item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintToolInfo ====================
    private void paintToolInfo(mGraphics g)
    {
        mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7_yellow.drawString(g, (Char.myCharz().isTichXanh ? "     " : string.Empty) + Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
        if (Char.myCharz().isTichXanh)
        {
            ModFunc.PaintTicks(g, 58, 17);
        }
        string text = ((!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet);
        mFont.tahoma_7_yellow.drawString(g, mResources.account_server + " " + ServerListScreen.nameServer[ServerListScreen.ipSelect] + ": " + text, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== doFireGameInfo ====================
    private void doFireGameInfo()
    {
        if (selected != -1)
        {
            infoSelect = selected;
            ((GameInfo)vGameInfo.elementAt(infoSelect)).hasRead = true;
            Rms.saveRMSInt(((GameInfo)vGameInfo.elementAt(infoSelect)).id + string.Empty, 1);
            setTypeGameSubInfo();
        }
    }


    // ==================== doFireAuto ====================
    private void doFireAuto()
    {
    }


    // ==================== doRada ====================
    private void doRada()
    {
        hide();
        if (RadarScr.list == null || RadarScr.list.size() == 0)
        {
            Service.gI().SendRada(0, -1);
            RadarScr.gI().switchToMe();
        }
        else
        {
            RadarScr.gI().switchToMe();
        }
    }


    // ==================== doFireTool ====================
    private void doFireTool()
    {
        if (selected < 0)
        {
            return;
        }
        if (SoundMn.IsDelAcc && selected == strTool.Length - 1)
        {
            Service.gI().sendDelAcc();
            return;
        }
        if (!Char.myCharz().havePet && !Char.myCharz().havePet2)
        {
            switch (selected)
            {
                case 0:
                    setTypeGameInfo();
                    break;
                case 1:
                    SetTypeModFunc();
                    break;
                case 2:
                    SetTypePlayerInfo();
                    break;
                case 3:
                    doRada();
                    break;
                case 4:
                    Service.gI().getFlag(0, -1);
                    InfoDlg.showWait();
                    break;
                case 5:
                    if (Char.myCharz().statusMe == 14)
                    {
                        GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                    }
                    else
                    {
                        ModFunc.GI().userOpenZones = true;
                        Service.gI().openUIZone();
                    }
                    break;
                case 6:
                    ModFunc.DoChatGlobal();
                    break;
                case 7:
                    setTypeAccount();
                    break;
                case 8:
                    setTypeOption();
                    break;
                case 9:
                    GameCanvas.loginScr.backToRegister();
                    break;
                case 10:
                    if (GameCanvas.loginScr.isLogin2)
                    {
                        SoundMn.gI().backToRegister();
                    }
                    break;
            }
            return;
        }
        if (Char.myCharz().havePet && Char.myCharz().havePet2)
        {
            switch (selected)
            {
                case 0:
                    setTypeGameInfo();
                    break;
                case 1:
                    SetTypeModFunc();
                    break;
                case 2:
                    SetTypePlayerInfo();
                    break;
                case 3:
                    doRada();
                    break;
                case 4:
                    doFirePet();
                    break;
                case 5:
                    doFirePet2();
                    break;
                case 6:
                    Service.gI().getFlag(0, -1);
                    InfoDlg.showWait();
                    break;
                case 7:
                    if (Char.myCharz().statusMe == 14)
                    {
                        GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                    }
                    else
                    {
                        ModFunc.GI().userOpenZones = true;
                        Service.gI().openUIZone();
                    }
                    break;
                case 8:
                    ModFunc.DoChatGlobal();
                    break;
                case 9:
                    setTypeAccount();
                    break;
                case 10:
                    setTypeOption();
                    break;
                case 11:
                    GameCanvas.loginScr.backToRegister();
                    break;
                case 12:
                    if (GameCanvas.loginScr.isLogin2)
                    {
                        SoundMn.gI().backToRegister();
                    }
                    break;
            }
            return;
        }
        switch (selected)
        {
            case 0:
                setTypeGameInfo();
                break;
            case 1:
                SetTypeModFunc();
                break;
            case 2:
                SetTypePlayerInfo();
                break;
            case 3:
                doRada();
                break;
            case 4:
                if (Char.myCharz().havePet)
                {
                    doFirePet();
                }
                else
                {
                    doFirePet2();
                }
                break;
            case 5:
                Service.gI().getFlag(0, -1);
                InfoDlg.showWait();
                break;
            case 6:
                if (Char.myCharz().statusMe == 14)
                {
                    GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                }
                else
                {
                    ModFunc.GI().userOpenZones = true;
                    Service.gI().openUIZone();
                }
                break;
            case 7:
                ModFunc.DoChatGlobal();
                break;
            case 8:
                setTypeAccount();
                break;
            case 9:
                setTypeOption();
                break;
            case 10:
                GameCanvas.loginScr.backToRegister();
                break;
            case 11:
                if (GameCanvas.loginScr.isLogin2)
                {
                    SoundMn.gI().backToRegister();
                }
                break;
        }
    }


    // ==================== setTypeGameSubInfo ====================
    private void setTypeGameSubInfo()
    {
        string content = ((GameInfo)vGameInfo.elementAt(infoSelect)).content;
        contenInfo = mFont.tahoma_7_grey.splitFontArray(content, wScroll - 40);
        currentListLength = contenInfo.Length;
        ITEM_HEIGHT = 16;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        type = 24;
        setType(0);
    }


    // ==================== SetTypePlayerInfo ====================
    private void SetTypePlayerInfo()
    {
        string content = "Tộc: " + (Char.myCharz().cgender == 0 ? "Trái Đất" : Char.myCharz().cgender == 1 ? "Namek" : "Xayda") + "\n" +
            "HP: " + NinjaUtil.getMoneys(Char.myCharz().cHP) + " / " + NinjaUtil.getMoneys(Char.myCharz().cHPFull) + "\n" +
            "KI: " + NinjaUtil.getMoneys(Char.myCharz().cMP) + " / " + NinjaUtil.getMoneys(Char.myCharz().cMPFull) + "\n" +
            "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull) + "\n" +
            "Chí mạng: " + Char.myCharz().cCriticalFull + "%\n" +
            "Giảm sát thương: " + Char.myCharz().tlDef + "%\n" +
            "Phản sát thương: " + Char.myCharz().tlPst + "%\n" +
            "Né đòn: " + Char.myCharz().tlNeDon + "%\n" +
            "Hút HP: " + Char.myCharz().tlHutHp + "%\n" +
            "Hút KI: " + Char.myCharz().tlHutMp + "%\n" +

            "Giảm TDHS: " + Char.myCharz().tileGiamTDHS + "%\n" +
            "Giảm TDHS: " + Char.myCharz().timeGiamTDHS + " giây\n" +
            "Kháng TDHS: " + (Char.myCharz().khangTDHS ? "Có" : "Không") + "\n" +
            "Kháng lạnh: " + (Char.myCharz().isKhongLanh ? "Có" : "Không") + "\n" +
            "Vô hình: " + (Char.myCharz().wearingVoHinh ? "Có" : "Không") + "\n" +
            "Dịch chuyển: " + (Char.myCharz().teleport ? "Có" : "Không") + "\n";
        contenInfo = mFont.tahoma_7_grey.splitFontArray(content, wScroll - 40);
        currentListLength = contenInfo.Length;
        ITEM_HEIGHT = 16;
        selected = GameCanvas.isTouch ? (-1) : 0;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        if (cmy < 0)
        {
            cmy = cmtoY = 0;
        }
        if (cmy > cmyLim)
        {
            cmy = cmtoY = cmyLim;
        }
        type = 27;
        setType(0);
    }


    // ==================== setTypeGameInfo ====================
    private void setTypeGameInfo()
    {
        currentListLength = vGameInfo.size();
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        type = 23;
        setType(0);
    }


    // ==================== doFireChangeFlag ====================
    private void doFireChangeFlag()
    {
        if (selected >= 0)
        {
            MyVector myVector = new MyVector();
            currInfoItem = selected;
            myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
            myVector.addElement(new Command(mResources.BACK, this, 10031, null));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
        }
    }


}
