using System;
using System.Threading;
using Mod;

public partial class Panel
{
    // ==================== setTypeOption ====================
    public void setTypeOption()
    {
        type = 19;
        setType(0);
        setTabOption();
        cmx = (cmtoX = 0);
    }


    // ==================== SetTypeModFunc ====================
    public void SetTypeModFunc()
    {
        type = 26;
        setType(0);
        SetTabModFunc();
        cmx = cmtoX = 0;
    }


    // ==================== SetTabModFunc ====================
    private void SetTabModFunc()
    {
        SoundMn.gI().GetStrModFunc();
        currentListLength = strModFunc.Length;
        ITEM_HEIGHT = 24;
        selected = GameCanvas.isTouch ? (-1) : 0;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = cmtoY = cmyLast[currentTabIndex];
        if (cmy < 0)
        {
            cmy = cmtoY = 0;
        }
        if (cmy > cmyLim)
        {
            cmy = cmtoY = cmyLim;
        }
    }


    // ==================== setTabOption ====================
    private void setTabOption()
    {
        SoundMn.gI().getStrOption();
        currentListLength = strCauhinh.Length;
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


    // ==================== paintOption ====================
    private void paintOption(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strCauhinh.Length; i++)
        {
            int x = xScroll;
            int num = yScroll + i * ITEM_HEIGHT;
            int num2 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num - cmy <= yScroll + hScroll && num - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(x, num, num2, h);
                mFont.tahoma_7b_dark.drawString(g, strCauhinh[i], xScroll + 10, num + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== PaintModFunc ====================
    private void PaintModFunc(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strModFunc.Length; i++)
        {
            int x = xScroll;
            int num = yScroll + i * ITEM_HEIGHT;
            int num2 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num - cmy <= yScroll + hScroll && num - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(x, num, num2, h);
                mFont.tahoma_7b_dark.drawString(g, strModFunc[i], xScroll + 10, num + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== doFireOption ====================
    private void doFireOption()
    {
        if (selected < 0)
        {
            return;
        }
        switch (selected)
        {
            case 0:
                SoundMn.gI().AuraToolOption();
                break;
            case 1:
                SoundMn.gI().AuraToolOption2();
                break;
            case 2:
                SoundMn.gI().soundToolOption();
                break;
            case 3:
                if (Main.isPC && !Main.isIPhone)
                {
                    GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
                }
                else
                {
                    SoundMn.gI().CaseSizeScr();
                }
                break;
            case 4:
                if (Main.isPC && !Main.isIPhone)
                {
                    GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
                }
                else
                {
                    SoundMn.gI().CaseAnalog();
                }
                break;
            case 5:
                SoundMn.gI().CaseAnalog();
                break;
        }
    }


    // ==================== DoFireModFunc ====================
    private void DoFireModFunc()
    {
        if (selected < 0)
        {
            return;
        }
        switch (selected)
        {
            case 0:
                ModFunc.GI().isHighFps = !ModFunc.GI().isHighFps;
                ModFunc.GI().ChangeFPSTarget();
                break;
            case 1:
                ModFunc.GI().isUpdateZones = !ModFunc.GI().isUpdateZones;
                break;
            case 2:
                ModFunc.GI().showCharsInMap = !ModFunc.GI().showCharsInMap;
                break;
            case 3:
                ModFunc.GI().showInfoMe = !ModFunc.GI().showInfoMe;
                break;
            case 4:
                ModFunc.GI().isAutoPhaLe = !ModFunc.GI().isAutoPhaLe;
                if (ModFunc.GI().isAutoPhaLe)
                {
                    new Thread(new ThreadStart(ModFunc.GI().AutoPhaLe)).Start();
                }
                break;
            case 5:
                if (!ModFunc.GI().isAutoVQMM)
                {
                    hideNow();
                }
                ModFunc.GI().isAutoVQMM = !ModFunc.GI().isAutoVQMM;
                break;
            case 6:
                ModFunc.GI().autoWakeUp = !ModFunc.GI().autoWakeUp;
                break;
            case 7:
                //ModFunc.accAutoLogin = GameCanvas.loginScr.tfUser.getText();
                //ModFunc.isAutoLogin = !ModFunc.isAutoLogin;
                if (ModFunc.isAutoLogin)
                {
                    ModFunc.isAutoLogin = false;
                    ModFunc.autoLogin = null;
                }
                else
                {
                    ModFunc.isAutoLogin = true;
                    ModFunc.autoLogin = new AutoLogin
                    {
                        accAutoLogin = GameCanvas.loginScr.tfUser.getText()
                    };
                }
                break;
            case 8:
                ModFunc.GI().isShowButton = !ModFunc.GI().isShowButton;
                break;
            case 9:
                ModFunc.GI().isIntroOff = !ModFunc.GI().isIntroOff;
                Rms.saveRMSInt("IntroOff", ModFunc.GI().isIntroOff ? 1 : 0);
                break;
        }
        SoundMn.gI().GetStrModFunc();
    }


    // ==================== setTypeAccount ====================
    public void setTypeAccount()
    {
        type = 20;
        setType(0);
        setTabAccount();
        cmx = (cmtoX = 0);
    }


    // ==================== setTabAccount ====================
    private void setTabAccount()
    {
        if (Main.IphoneVersionApp)
        {
            strAccount = new string[4]
            {
                mResources.inventory_Pass,
                mResources.friend,
                mResources.enemy,
                mResources.msg
            };
            if (GameScr.canAutoPlay)
            {
                strAccount = new string[5]
                {
                    mResources.inventory_Pass,
                    mResources.friend,
                    mResources.enemy,
                    mResources.msg,
                    mResources.autoFunction
                };
            }
        }
        else
        {
            strAccount = new string[5]
            {
                mResources.inventory_Pass,
                mResources.friend,
                mResources.enemy,
                mResources.msg,
                mResources.charger
            };
            if (GameScr.canAutoPlay)
            {
                strAccount = new string[6]
                {
                    mResources.inventory_Pass,
                    mResources.friend,
                    mResources.enemy,
                    mResources.msg,
                    mResources.charger,
                    mResources.autoFunction
                };
            }
            if ((mSystem.clientType == 2 || mSystem.clientType == 7) && mResources.language != 2)
            {
                strAccount = new string[5]
                {
                    mResources.inventory_Pass,
                    mResources.friend,
                    mResources.enemy,
                    mResources.msg,
                    mResources.charger
                };
                if (GameScr.canAutoPlay)
                {
                    strAccount = new string[6]
                    {
                        mResources.inventory_Pass,
                        mResources.friend,
                        mResources.enemy,
                        mResources.msg,
                        mResources.charger,
                        mResources.autoFunction
                    };
                }
            }
        }
        currentListLength = strAccount.Length;
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


    // ==================== paintAccount ====================
    private void paintAccount(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strAccount.Length; i++)
        {
            int x = xScroll;
            int num = yScroll + i * ITEM_HEIGHT;
            int num2 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num - cmy <= yScroll + hScroll && num - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(x, num, num2, h);
                mFont.tahoma_7b_dark.drawString(g, strAccount[i], xScroll + wScroll / 2, num + 6, mFont.CENTER);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== doFireAccount ====================
    private void doFireAccount()
    {
        if (selected < 0)
        {
            return;
        }
        switch (selected)
        {
            case 0:
                GameCanvas.endDlg();
                if (chatTField == null)
                {
                    chatTField = new ChatTextField();
                    chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
                    chatTField.initChatTextField();
                    chatTField.parentScreen = GameCanvas.panel;
                }
                chatTField.tfChat.setText(string.Empty);
                chatTField.strChat = mResources.input_Inventory_Pass;
                chatTField.tfChat.name = mResources.input_Inventory_Pass;
                chatTField.to = string.Empty;
                chatTField.isShow = true;
                chatTField.tfChat.isFocus = true;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
                if (GameCanvas.isTouch)
                {
                    chatTField.tfChat.doChangeToTextBox();
                }
                if (!Main.isPC)
                {
                    chatTField.startChat(this, string.Empty);
                }
                if (Main.isWindowsPhone)
                {
                    chatTField.tfChat.strInfo = chatTField.strChat;
                }
                break;
            case 1:
                Service.gI().friend(0, -1);
                InfoDlg.showWait();
                break;
            case 2:
                Service.gI().enemy(0, -1);
                InfoDlg.showWait();
                break;
            case 3:
                setTypeMessage();
                if (chatTField == null)
                {
                    chatTField = new ChatTextField();
                    chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
                    chatTField.initChatTextField();
                    chatTField.parentScreen = GameCanvas.panel;
                }
                break;
            case 4:
                if (mResources.language == 2)
                {
                    string url = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
                    hideNow();
                    try
                    {
                        GameMidlet.instance.platformRequest(url);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ex.StackTrace.ToString();
                        break;
                    }
                }
                hideNow();
                if (Char.myCharz().taskMaint.taskId <= 10)
                {
                    GameCanvas.startOKDlg(mResources.finishBomong);
                }
                else
                {
                    MoneyCharge.gI().switchToMe();
                }
                break;
            case 5:
                setTypeAuto();
                break;
        }
    }


    // ==================== updateKeyOption ====================
    private void updateKeyOption()
    {
        updateKeyScrollView();
    }


    // ==================== IsTabOption ====================
    private bool IsTabOption()
    {
        return false;
    }


}
