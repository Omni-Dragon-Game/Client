using System;
using UnityEngine;

public partial class ServerListScreen
{
    public void perform(int idAction, object p)
    {
        if (idAction == 1000)
        {
            GameCanvas.connect();
        }
        else if (idAction == 1 || idAction == 4)
        {
            Session_ME.gI().close();
            isAutoConect = false;
            countDieConnect = 0;
            loadScreen = true;
            testConnect = 0;
            isGetData = false;
            Rms.clearAll();
            switchToMe();
        }
        else if (idAction == 2)
        {
            stopDownload = false;
            cmdDownload = new Command(mResources.huy, this, 4, null);
            cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
            cmdDownload.y = GameCanvas.hh + 65;
            right = null;
            if (!GameCanvas.isTouch)
            {
                cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
                cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
            }
            center = new Command(string.Empty, this, 4, null);
            if (!isGetData)
            {
                Service.gI().updateData();
                Service.gI().getResource(1, null);
                if (!GameCanvas.isTouch)
                {
                    cmdDownload.isFocus = true;
                    center = new Command(string.Empty, this, 4, null);
                }
                isGetData = true;
            }
        }
        else if (idAction == 3)
        {
            if (GameCanvas.loginScr == null)
            {
                GameCanvas.loginScr = new LoginScr();
            }
            GameCanvas.loginScr.switchToMe();
            bool flag = Rms.loadRMSString("acc") != null && ((!Rms.loadRMSString("acc").Equals(string.Empty)) ? true : false);
            bool flag2 = Rms.loadRMSString("userAo" + ipSelect) != null && ((!Rms.loadRMSString("userAo" + ipSelect).Equals(string.Empty)) ? true : false);
            if (!flag && !flag2)
            {
                GameCanvas.connect();
                string text = Rms.loadRMSString("userAo" + ipSelect);
                if (text == null || text.Equals(string.Empty))
                {
                    Service.gI().login2(string.Empty);
                }
                else
                {
                    GameCanvas.loginScr.isLogin2 = true;
                    GameCanvas.connect();
                    Service.gI().setClientType();
                    Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
                }
                if (Session_ME.connected)
                {
                    GameCanvas.startWaitDlg();
                }
                else
                {
                    GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
                }
            }
            else
            {
                GameCanvas.loginScr.doLogin();
            }
            LoginScr.serverName = nameServer[ipSelect];
        }
        else if (idAction == 10100)
        {
            if (GameCanvas.loginScr == null)
            {
                GameCanvas.loginScr = new LoginScr();
            }
            GameCanvas.loginScr.switchToMe();
            GameCanvas.connect();
            Service.gI().login2(string.Empty);
            GameCanvas.startWaitDlg();
            LoginScr.serverName = nameServer[ipSelect];
        }
        else if (idAction == 5)
        {
            doUpdateServer();
            if (nameServer.Length == 1)
            {
                return;
            }
            MyVector myVector = new MyVector(string.Empty);
            for (int i = 0; i < nameServer.Length; i++)
            {
                myVector.addElement(new Command(nameServer[i], this, 6, null));
            }
            GameCanvas.menu.startAt(myVector, 0);
            if (!GameCanvas.isTouch)
            {
                GameCanvas.menu.menuSelectedItem = ipSelect;
            }
        }
        else if (idAction == 6)
        {
            ipSelect = GameCanvas.menu.menuSelectedItem;
            selectServer();
        }
        else if (idAction == 7)
        {
            if (GameCanvas.loginScr == null)
            {
                GameCanvas.loginScr = new LoginScr();
            }
            GameCanvas.loginScr.switchToMe();
        }
        else if (idAction == 8)
        {
            bool lowGraphic = Rms.loadRMSInt("lowGraphic") == 1;
            MyVector myVector2 = new MyVector("cau hinh");
            myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
            myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
            GameCanvas.menu.startAt(myVector2, 0);
            if (lowGraphic)
            {
                GameCanvas.menu.menuSelectedItem = 0;
            }
            else
            {
                GameCanvas.menu.menuSelectedItem = 1;
            }
        }
        else if (idAction == 9)
        {
            Rms.saveRMSInt("lowGraphic", 1);
            GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
        }
        else if (idAction == 10)
        {
            Rms.saveRMSInt("lowGraphic", 0);
            GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
        }
        else if (idAction == 11)
        {
            if (GameCanvas.loginScr == null)
            {
                GameCanvas.loginScr = new LoginScr();
            }
            GameCanvas.loginScr.switchToMe();
            string text2 = Rms.loadRMSString("userAo" + ipSelect);
            if (text2 == null || text2.Equals(string.Empty))
            {
                Service.gI().login2(string.Empty);
            }
            else
            {
                GameCanvas.loginScr.isLogin2 = true;
                GameCanvas.connect();
                Service.gI().setClientType();
                Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
            }
            GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
        }
        else if (idAction == 12)
        {
            GameMidlet.instance.exit();
        }
        else if (idAction == 13 && (!isGetData || loadScreen))
        {
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
            }
        }
        else if (idAction == 14)
        {
            Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
            Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
            GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
        }
        else if (idAction == 15)
        {
            Rms.clearAll();
            GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
        }
        else if (idAction == 16)
        {
            InfoDlg.hide();
            GameCanvas.currentDialog = null;
        }
        else if (idAction == 17)
        {
            if (GameCanvas.serverScr == null)
            {
                GameCanvas.serverScr = new ServerScr();
            }
            GameCanvas.serverScr.switchToMe();
        }
        else if (idAction == 18)
        {
            GameCanvas.endDlg();
            InfoDlg.hide();
            if (GameCanvas.serverScr == null)
            {
                GameCanvas.serverScr = new ServerScr();
            }
            GameCanvas.serverScr.switchToMe();
        }
        else if (idAction == 19)
        {
            if (mSystem.clientType == 1)
            {
                InfoDlg.hide();
                GameCanvas.currentDialog = null;
            }
            else
            {
                countDieConnect = 0;
                testConnect = 0;
                isAutoConect = true;
            }
        }
    }

}
