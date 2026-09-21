using System;
using System.Collections;
using System.Threading;
using Assets.src.g;
using UnityEngine;
//using static UnityEditor.ShaderData;

public partial class GameCanvas : IActionListener
{
    // Fields extracted to GameCanvas.Fields.cs

    public static string getPlatformName()
    {
        return "Pc platform xxx";
    }

    public void initGame()
    {
        MotherCanvas.instance.setChildCanvas(this);
        w = MotherCanvas.instance.getWidthz();
        h = MotherCanvas.instance.getHeightz();
        hw = w / 2;
        hh = h / 2;
        isTouch = true;
        if (w >= 240)
        {
            isTouchControl = true;
        }
        if (w < 320)
        {
            isTouchControlSmallScreen = true;
        }
        if (w >= 320)
        {
            isTouchControlLargeScreen = true;
        }
        msgdlg = new MsgDlg();
        if (h <= 160)
        {
            Paint.hTab = 15;
            mScreen.cmdH = 17;
        }
        GameScr.d = ((w <= h) ? h : w) + 20;
        instance = this;
        mFont.init();
        mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
        initPaint();
        loadDust();
        loadWaterSplash();
        panel = new Panel();
        imgShuriken = loadImage("/mainImage/myTexture2df.png");
        int num = Rms.loadRMSInt("clienttype");
        if (num != -1)
        {
            if (num > 7)
            {
                Rms.saveRMSInt("clienttype", mSystem.clientType);
            }
            else
            {
                mSystem.clientType = num;
            }
        }
        imgClear = loadImage("/mainImage/myTexture2der.png");
        debugUpdate = new MyVector();
        debugPaint = new MyVector();
        debugSession = new MyVector();
        for (int i = 0; i < 3; i++)
        {
            imgBorder[i] = loadImage("/mainImage/myTexture2dbd" + i + ".png");
        }
        borderConnerW = mGraphics.getImageWidth(imgBorder[0]);
        borderConnerH = mGraphics.getImageHeight(imgBorder[0]);
        borderCenterW = mGraphics.getImageWidth(imgBorder[1]);
        borderCenterH = mGraphics.getImageHeight(imgBorder[1]);
        Panel.graphics = Rms.loadRMSInt("lowGraphic");
        lowGraphic = Rms.loadRMSInt("lowGraphic") == 1;
        GameScr.isPaintChatVip = Rms.loadRMSInt("serverchat") != 1;
        Char.isPaintAura = Rms.loadRMSInt("isPaintAura") == 1;
        Char.isPaintAura2 = Rms.loadRMSInt("isPaintAura2") == 1;
        Res.init();
        SmallImage.loadBigImage();
        Panel.WIDTH_PANEL = 176;
        if (Panel.WIDTH_PANEL > w)
        {
            Panel.WIDTH_PANEL = w;
        }
        InfoMe.gI().loadCharId();
        Command.btn0left = loadImage("/mainImage/btn0left.png");
        Command.btn0mid = loadImage("/mainImage/btn0mid.png");
        Command.btn0right = loadImage("/mainImage/btn0right.png");
        Command.btn1left = loadImage("/mainImage/btn1left.png");
        Command.btn1mid = loadImage("/mainImage/btn1mid.png");
        Command.btn1right = loadImage("/mainImage/btn1right.png");
        serverScreen = new ServerListScreen();
        ServerListScreen.createDeleteRMS();
        serverScr = new ServerScr();
        chooseCharScr = new ChooseCharScr();
    }

    public static GameCanvas gI()
    {
        return instance;
    }

    public void initPaint()
    {
        paintz = new Paint();
    }

    public static void closeKeyBoard()
    {
        mGraphics.addYWhenOpenKeyBoard = 0;
        timeOpenKeyBoard = 0;
        Main.closeKeyBoard();
    }
    public void update()
    {
        //if (ModFunc.isAutoLogin)
        //{
        //    Main.main.StartCoroutine(loadAccount());
        //}
        if (mSystem.currentTimeMillis() > timefps)
        {
            timefps += 1000L;
            max = fps;
            fps = 0;
        }
        fps++;
        if (messageServer.size() > 0 && thongBaoTest == null)
        {
            startserverThongBao((string)messageServer.elementAt(0));
            messageServer.removeElementAt(0);
        }
        if (gameTick % 5 == 0)
        {
            timeNow = mSystem.currentTimeMillis();
        }
        Res.updateOnScreenDebug();
        try
        {
            if (TouchScreenKeyboard.visible)
            {
                timeOpenKeyBoard++;
                if (timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5))
                {
                    mGraphics.addYWhenOpenKeyBoard = 94;
                }
            }
            else
            {
                mGraphics.addYWhenOpenKeyBoard = 0;
                timeOpenKeyBoard = 0;
            }
            debugUpdate.removeAllElements();
            long num = mSystem.currentTimeMillis();
            if (num - timeTickEff1 >= 780 && !isEff1)
            {
                timeTickEff1 = num;
                isEff1 = true;
            }
            else
            {
                isEff1 = false;
            }
            if (num - timeTickEff2 >= 7800 && !isEff2)
            {
                timeTickEff2 = num;
                isEff2 = true;
            }
            else
            {
                isEff2 = false;
            }
            if (taskTick > 0)
            {
                taskTick--;
            }
            gameTick++;
            if (gameTick > 10000)
            {

                if (mSystem.currentTimeMillis() - lastTimePress > 20000 && currentScreen == loginScr)
                {
                    startOKDlg(mResources.maychutathoacmatsong);
                }
                gameTick = 0;
            }
            if (currentScreen != null)
            {
                if (ChatPopup.serverChatPopUp != null)
                {
                    ChatPopup.serverChatPopUp.update();
                    ChatPopup.serverChatPopUp.updateKey();
                }
                else if (ChatPopup.currChatPopup != null)
                {
                    ChatPopup.currChatPopup.update();
                    ChatPopup.currChatPopup.updateKey();
                }
                else if (currentDialog != null)
                {
                    currentDialog.update();
                }
                else if (menu.showMenu)
                {
                    menu.updateMenu();
                    menu.updateMenuKey();
                }
                else if (panel.isShow)
                {
                    panel.update();
                    if (isPointer(panel.X, panel.Y, panel.W, panel.H))
                    {
                        isFocusPanel2 = false;
                    }
                    if (panel2 != null && panel2.isShow)
                    {
                        panel2.update();
                        if (isPointer(panel2.X, panel2.Y, panel2.W, panel2.H))
                        {
                            isFocusPanel2 = true;
                        }
                    }
                    if (panel2 != null)
                    {
                        if (isFocusPanel2)
                        {
                            panel2.updateKey();
                        }
                        else
                        {
                            panel.updateKey();
                        }
                    }
                    else
                    {
                        panel.updateKey();
                    }
                    if (panel.chatTField != null && panel.chatTField.isShow)
                    {
                        panel.chatTFUpdateKey();
                    }
                    else if (panel2 != null && panel2.chatTField != null && panel2.chatTField.isShow)
                    {
                        panel2.chatTFUpdateKey();
                    }
                    else if ((isPointer(panel.X, panel.Y, panel.W, panel.H) && panel2 != null) || panel2 == null)
                    {
                        panel.updateKey();
                    }
                    else if (panel2 != null && panel2.isShow && isPointer(panel2.X, panel2.Y, panel2.W, panel2.H))
                    {
                        panel2.updateKey();
                    }
                    if (isPointer(panel.X + panel.W, panel.Y, w - panel.W * 2, panel.H) && isPointerJustRelease && panel.isDoneCombine)
                    {
                        panel.hide();
                    }
                }
                if (!isLoading)
                {
                    currentScreen.update();
                }
                if (!panel.isShow && ChatPopup.serverChatPopUp == null)
                {
                    currentScreen.updateKey();
                }
                Hint.update();
                SoundMn.gI().update();
            }
            Timer.update();
            InfoDlg.update();
            if (resetToLoginScr)
            {
                resetToLoginScr = false;
                doResetToLoginScr(serverScreen);
            }
            if (Controller.isConnectOK)
            {
                if (Controller.isMain)
                {
                    GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
                    GameMidlet.PORT = ServerListScreen.port[ServerListScreen.ipSelect];
                    ServerListScreen.testConnect = 2;
                    Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
                    Service.gI().setClientType();
                    Service.gI().androidPack();
                    Service.gI().updateData();
                }
                else
                {
                    Service.gI().setClientType2();
                    Service.gI().androidPack2();
                }
                Controller.isConnectOK = false;
            }
            if (Controller.isDisconnected)
            {
                if (!Controller.isMain)
                {
                    if (currentScreen == serverScreen && !Service.reciveFromMainSession)
                    {
                        serverScreen.cancel();
                    }
                    if (currentScreen == loginScr && !Service.reciveFromMainSession)
                    {
                        onDisconnected();
                    }
                }
                else
                {
                    onDisconnected();
                }
                Controller.isDisconnected = false;
            }
            if (Controller.isConnectionFail)
            {
                Debug.Log("connect fail");
                if (!Controller.isMain)
                {
                    if (currentScreen == serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession)
                    {
                        ServerListScreen.testConnect = 0;
                        serverScreen.cancel();
                    }
                    if (currentScreen == loginScr && !Service.reciveFromMainSession)
                    {
                        onConnectionFail();
                    }
                }
                else if (Session_ME.gI().isCompareIPConnect())
                {
                    onConnectionFail();
                }
                Controller.isConnectionFail = false;
            }
            if (Main.isResume)
            {
                Main.isResume = false;
                if (currentDialog != null && currentDialog.left != null && currentDialog.left.actionListener != null)
                {
                    currentDialog.left.performAction();
                }
            }
            if (currentScreen == null || !(currentScreen is GameScr))
            {
                return;
            }
            xThongBaoTranslate += dir_ * 2;
            if (xThongBaoTranslate - Panel.imgNew.getWidth() <= 60)
            {
                dir_ = 0;
                tickWaitThongBao++;
                if (tickWaitThongBao > 150)
                {
                    tickWaitThongBao = 0;
                    thongBaoTest = null;
                }
            }
        }
        catch (Exception)
        {
        }
    }
    public static Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        UnityEngine.Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
    public static void resizeImage(Image img)
    {
        int wx1 = img.w / 4;
        int hx1 = img.h / 4;
        int w = wx1 * mGraphics.zoomLevel;
        int h = hx1 * mGraphics.zoomLevel;
        img.texture = Resize(img.texture, w, h);
        img.w = img.texture.width;
        img.h = img.texture.height;
        Image.setTextureQuality(img.texture);
    }
    public static Image loadEffect(string path)
    {
        path = Main.res + "/x"+ mGraphics.zoomLevel +"/inven" + path;
        //cutPng(path);
        Image result = null;
        try
        {
            result = Image.createImage(path);
        }
        catch (Exception)
        {
        }
        return result;
    }
    public void onDisconnected()
    {
        if (Controller.isConnectionFail)
        {
            Controller.isConnectionFail = false;
        }
        isResume = true;
        Session_ME.gI().clearSendingMessage();
        Session_ME2.gI().clearSendingMessage();
        Session_ME.gI().close();
        Session_ME2.gI().close();
        if (Controller.isLoadingData)
        {
            instance.resetToLoginScrz();
            startOK(mResources.pls_restart_game_error, 8885, null);
            Controller.isDisconnected = false;
            return;
        }
        if (currentScreen != serverScreen)
        {
            startOKDlg(mResources.maychutathoacmatsong);
        }
        else
        {
            endDlg();
        }
        Char.isLoadingMap = false;
        if (Controller.isMain)
        {
            ServerListScreen.testConnect = 0;
        }
        instance.resetToLoginScrz();
        startOKDlg(mResources.maychutathoacmatsong);
        mSystem.endKey();
    }

    public void onConnectionFail()
    {
        if (currentScreen.Equals(SplashScr.instance))
        {
            if (ServerListScreen.hasConnected != null)
            {
                ServerListScreen.GetServerList(ServerListScreen.linkDefault);
                if (!ServerListScreen.hasConnected[0])
                {
                    ServerListScreen.hasConnected[0] = true;
                    ServerListScreen.ipSelect = 0;
                    GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
                    Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
                    connect();
                }
                else if (!ServerListScreen.hasConnected[2])
                {
                    ServerListScreen.hasConnected[2] = true;
                    ServerListScreen.ipSelect = 2;
                    GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
                    Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
                    connect();
                }
                else
                {
                    startOK(mResources.pls_restart_game_error, 8885, null);
                }
            }
            else
            {
                //startOK(mResources.pls_restart_game_error, 8885, null);
            }
            return;
        }
        Session_ME.gI().clearSendingMessage();
        Session_ME2.gI().clearSendingMessage();
        ServerListScreen.isWait = false;
        if (Controller.isLoadingData)
        {
            startOK(mResources.pls_restart_game_error, 8885, null);
            Controller.isConnectionFail = false;
            return;
        }
        isResume = true;
        LoginScr.isContinueToLogin = false;
        if (loginScr != null)
        {
            instance.resetToLoginScrz();
        }
        else
        {
            loginScr = new LoginScr();
        }
        LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
        if (currentScreen != serverScreen)
        {
            ServerListScreen.countDieConnect = 0;
        }
        else
        {
            endDlg();
            ServerListScreen.loadScreen = true;
            serverScreen.switchToMe();
        }
        Char.isLoadingMap = false;
        if (Controller.isMain)
        {
            ServerListScreen.testConnect = 0;
        }
        mSystem.endKey();
    }

    public static bool isWaiting()
    {
        if (InfoDlg.isShow || (msgdlg != null && msgdlg.info.Equals(mResources.PLEASEWAIT)) || Char.isLoadingMap || LoginScr.isContinueToLogin)
        {
            return true;
        }
        return false;
    }

    public static void connect()
    {
        if (!Session_ME.gI().isConnected())
        {
            Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
        }
    }

    public static void connect2()
    {
        if (!Session_ME2.gI().isConnected())
        {
            Res.outz("IP2= " + GameMidlet.IP2 + " PORT 2= " + GameMidlet.PORT2);
            Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
        }
    }

    public static void resetTrans(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.setClip(0, 0, w, h);
    }

    public static void resetTransGameScr(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.translate(0, 0);
        g.setClip(0, 0, w, h);
        g.translate(-GameScr.cmx, -GameScr.cmy);
    }

    public void initGameCanvas()
    {
        debug("SP2i1", 0);
        w = MotherCanvas.instance.getWidthz();
        h = MotherCanvas.instance.getHeightz();
        debug("SP2i2", 0);
        hw = w / 2;
        hh = h / 2;
        wd3 = w / 3;
        hd3 = h / 3;
        w2d3 = 2 * w / 3;
        h2d3 = 2 * h / 3;
        w3d4 = 3 * w / 4;
        h3d4 = 3 * h / 4;
        wd6 = w / 6;
        hd6 = h / 6;
        debug("SP2i3", 0);
        mScreen.initPos();
        debug("SP2i4", 0);
        debug("SP2i5", 0);
        inputDlg = new InputDlg();
        debug("SP2i6", 0);
        listPoint = new MyVector();
        debug("SP2i7", 0);
    }

    public void start()
    {
    }

    public int getWidth()
    {
        return (int)ScaleGUI.WIDTH;
    }

    public int getHeight()
    {
        return (int)ScaleGUI.HEIGHT;
    }

    public static void debug(string s, int type)
    {
    }

    public void doResetToLoginScr(mScreen screen)
    {
        try
        {
            SoundMn.gI().stopAll();
            LoginScr.isContinueToLogin = false;
            TileMap.lastType = (TileMap.bgType = 0);
            Char.clearMyChar();
            GameScr.clearGameScr();
            GameScr.resetAllvector();
            InfoDlg.hide();
            GameScr.info1.hide();
            GameScr.info2.hide();
            GameScr.info2.cmdChat = null;
            Hint.isShow = false;
            ChatPopup.currChatPopup = null;
            Controller.isStopReadMessage = false;
            GameScr.loadCamera(fullmScreen: true, -1, -1);
            GameScr.cmx = 100;
            panel.currentTabIndex = 0;
            panel.selected = isTouch ? (-1) : 0;
            panel.init();
            panel2 = null;
            GameScr.isPaint = true;
            ClanMessage.vMessage.removeAllElements();
            GameScr.textTime.removeAllElements();
            GameScr.vClan.removeAllElements();
            GameScr.vFriend.removeAllElements();
            GameScr.vEnemies.removeAllElements();
            TileMap.vCurrItem.removeAllElements();
            BackgroudEffect.vBgEffect.removeAllElements();
            EffecMn.vEff.removeAllElements();
            Effect.newEff.removeAllElements();
            menu.showMenu = false;
            panel.vItemCombine.removeAllElements();
            panel.isShow = false;
            if (panel.tabIcon != null)
            {
                panel.tabIcon.isShow = false;
            }
            if (mGraphics.zoomLevel == 1)
            {
                SmallImage.clearHastable();
            }
            Session_ME.gI().close();
            Session_ME2.gI().close();
            screen.switchToMe();
        }
        catch (Exception ex)
        {
            Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
        }
        ServerListScreen.isAutoConect = true;
        ServerListScreen.countDieConnect = 0;
        ServerListScreen.testConnect = -1;
        ServerListScreen.loadScreen = true;
    }


    // Background extracted to GameCanvas.Background.cs
    // Input extracted to GameCanvas.Input.cs


    public void paintChangeMap(mGraphics g)
    {
        string empty = string.Empty;
        resetTrans(g);
        g.setColor(0);
        g.fillRect(0, 0, w, h);
        //g.drawImage(SplashScr.imgLogo, w / 2, h / 2 - 24, StaticObj.BOTTOM_HCENTER);

        int imgW = ModFunc.imgLogoBig.getWidth() * mGraphics.zoomLevel / 4;
        int imgH = ModFunc.imgLogoBig.getHeight() * mGraphics.zoomLevel / 4;
        g.drawImageScale(ModFunc.imgLogoBig, (w - imgW) / 2, (h - imgH) / 2 - 30, imgW, imgH);

        paintShukiren(hw, h / 2 + 24, g);
        mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? empty : (" " + LoginScr.timeLogin + "s")), w / 2, h / 2, 2);
    }

    public void paint(mGraphics gx)
    {
        try
        {
            debugPaint.removeAllElements();
            debug("PA", 1);
            if (currentScreen != null)
            {
                currentScreen.paint(g);
            }
            debug("PB", 1);
            g.translate(-g.getTranslateX(), -g.getTranslateY());
            g.setClip(0, 0, w, h);
            if (panel.isShow)
            {
                panel.paint(g);
                if (panel2 != null && panel2.isShow)
                {
                    panel2.paint(g);
                }
                if (panel.chatTField != null && panel.chatTField.isShow)
                {
                    panel.chatTField.paint(g);
                }
                if (panel2 != null && panel2.chatTField != null && panel2.chatTField.isShow)
                {
                    panel2.chatTField.paint(g);
                }
            }
            Res.paintOnScreenDebug(g);
            InfoDlg.paint(g);
            if (currentDialog != null)
            {
                debug("PC", 1);
                currentDialog.paint(g);
            }
            else if (menu.showMenu)
            {
                debug("PD", 1);
                menu.paintMenu(g);
            }
            GameScr.info1.paint(g);
            GameScr.info2.paint(g);
            if (GameScr.gI().popUpYesNo != null)
            {
                GameScr.gI().popUpYesNo.paint(g);
            }
            if (ChatPopup.currChatPopup != null)
            {
                ChatPopup.currChatPopup.paint(g);
            }
            Hint.paint(g);
            if (ChatPopup.serverChatPopUp != null)
            {
                ChatPopup.serverChatPopUp.paint(g);
            }
            for (int i = 0; i < Effect2.vEffect2.size(); i++)
            {
                Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
                if (effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp))
                {
                    effect.paint(g);
                }
            }
            if (Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait)
            {
                paintChangeMap(g);
                if (timeLoading > 0 && LoginScr.timeLogin <= 0)
                {
                    startWaitDlg();
                    if (mSystem.currentTimeMillis() - TIMEOUT >= 1000)
                    {
                        timeLoading--;
                        Res.outz("[COUNT] == " + timeLoading);
                        if (timeLoading == 0)
                        {
                            timeLoading = 15;
                        }
                        TIMEOUT = mSystem.currentTimeMillis();
                    }
                }
                if (mSystem.currentTimeMillis() > timeBreakLoading)
                {
                    timeBreakLoading = mSystem.currentTimeMillis() + 30000;
                    if (currentScreen != null)
                    {
                        if (currentScreen is GameScr)
                        {
                            GameScr.gI().switchToMe();
                        }
                        else if (!(currentScreen is SplashScr) && currentScreen is LoginScr)
                        {
                            gI().resetToLoginScrz();
                        }
                    }
                }
            }
            debug("PE", 1);
            resetTrans(g);
            EffecMn.paintLayer4(g);
            resetTrans(g);
            int num = h / 4;
            if (currentScreen != null && currentScreen is GameScr && thongBaoTest != null)
            {
                g.setClip(60, num, w - 120, mFont.tahoma_7_white.getHeight() + 2);
                mFont.tahoma_7_grey.drawString(g, thongBaoTest, xThongBaoTranslate, num + 1, 0);
                mFont.tahoma_7_yellow.drawString(g, thongBaoTest, xThongBaoTranslate, num, 0);
                g.setClip(0, 0, w, h);
            }
        }
        catch (Exception)
        {
        }
    }


    // Dialogs extracted to GameCanvas.Dialogs.cs
    // Actions extracted to GameCanvas.Actions.cs
}
