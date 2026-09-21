using System;
using UnityEngine;

public partial class LoginScr
{
    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 13:
                switch (mSystem.clientType)
                {
                    case 1:
                        mSystem.callHotlineJava();
                        break;
                    case 3:
                    case 5:
                        mSystem.callHotlineIphone();
                        break;
                    case 6:
                        mSystem.callHotlineWindowsPhone();
                        break;
                    case 4:
                        mSystem.callHotlinePC();
                        break;
                    case 2:
                        break;
                }
                break;
            case 1000:
                try
                {
                    GameMidlet.instance.platformRequest((string)p);
                }
                catch (Exception)
                {
                }
                GameCanvas.endDlg();
                break;
            case 1001:
                GameCanvas.endDlg();
                isRes = false;
                break;
            case 1002:
                {
                    GameCanvas.startWaitDlg();
                    string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
                    if (text == null || text.Equals(string.Empty))
                    {
                        Service.gI().login2(string.Empty);
                        break;
                    }
                    GameCanvas.loginScr.isLogin2 = true;
                    GameCanvas.connect();
                    Service.gI().setClientType();
                    Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
                    break;
                }
            case 1004:
                ServerListScreen.doUpdateServer();
                GameCanvas.serverScreen.switchToMe();
                break;
            case 10021:
                actRegisterLeft();
                break;
            case 1003:
                GameCanvas.startOKDlg(mResources.goToWebForPassword);
                break;
            case 1005:
                try
                {
                    GameMidlet.instance.platformRequest(ServerListScreen.linkweb);
                    break;
                }
                catch (Exception)
                {
                    break;
                }
            case 10041:
                Rms.saveRMSInt("lowGraphic", 0);
                GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
                break;
            case 10042:
                Rms.saveRMSInt("lowGraphic", 1);
                GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
                break;
            case 2001:
                if (isCheck)
                {
                    isCheck = false;
                }
                else
                {
                    isCheck = true;
                }
                break;
            case 2002:
                doRegister();
                break;
            case 2003:
                doMenu();
                break;
            case 2004:
                actRegister();
                break;
            case 2008:
                string user = tfUser.getText().Trim();
                string pass = tfPass.getText().Trim();
                ModFunc.GI().AddAccount(user, pass);
                Rms.saveRMSString("acc", user);
                Rms.saveRMSString("pass", pass);
                if (ServerListScreen.loadScreen)
                {
                    GameCanvas.serverScreen.switchToMe();
                }
                else
                {
                    GameCanvas.serverScreen.show2();
                }
                break;
            case 4000:
                doRegister(tfUser.getText());
                break;
        }
    }

    public void actRegisterLeft()
    {
        if (isLogin2)
        {
            doLogin();
            return;
        }
        isRes = false;
        tfPass.isFocus = false;
        tfUser.isFocus = true;
        left = cmdMenu;
    }

    public void actRegister()
    {
        GameCanvas.endDlg();
        isRes = true;
        tfPass.isFocus = false;
        tfUser.isFocus = true;
    }

    public void backToRegister()
    {
        GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000;
        ServerListScreen.countDieConnect = 0;
        if (GameCanvas.loginScr.isLogin2)
        {
            GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
            return;
        }
        if (Main.isWindowsPhone)
        {
            GameMidlet.isBackWindowsPhone = true;
        }
        GameCanvas.instance.resetToLoginScr = false;
        GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
    }
}

