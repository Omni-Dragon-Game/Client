using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- open_xoso ---
    public void updateOpen()
    {
        if (isstarOpen)
        {
            if (moveUp > -3)
            {
                moveUp -= 4;
            }
            else
            {
                moveUp = -2;
            }
            if (moveDow < GameCanvas.h + 3)
            {
                moveDow += 4;
            }
            else
            {
                moveDow = GameCanvas.h + 2;
            }
            if (moveUp <= -2 && moveDow >= GameCanvas.h + 2)
            {
                isstarOpen = false;
            }
        }
    }

    public void initCreateCommand()
    {
    }

    // checkCharFocus extracted to GameScr.Targeting.cs

    public void updateXoSo()
    {
        if (tShow == 0)
        {
            return;
        }
        currXS = mSystem.currentTimeMillis();
        if (currXS - lastXS > 1000)
        {
            lastXS = mSystem.currentTimeMillis();
            secondXS++;
        }
        if (secondXS > 20)
        {
            for (int i = 0; i < winnumber.Length; i++)
            {
                randomNumber[i] = winnumber[i];
            }
            tShow--;
            if (tShow == 0)
            {
                yourNumber = string.Empty;
                info1.addInfo(strFinish, 0);
                secondXS = 0;
            }
            return;
        }
        if (moveIndex > winnumber.Length - 1)
        {
            tShow--;
            if (tShow == 0)
            {
                yourNumber = string.Empty;
                info1.addInfo(strFinish, 0);
            }
            return;
        }
        if (moveIndex < randomNumber.Length)
        {
            if (tMove[moveIndex] == 15)
            {
                if (randomNumber[moveIndex] == winnumber[moveIndex] - 1)
                {
                    delayMove[moveIndex] = 10;
                }
                if (randomNumber[moveIndex] == winnumber[moveIndex])
                {
                    tMove[moveIndex] = -1;
                    moveIndex++;
                }
            }
            else if (GameCanvas.gameTick % 5 == 0)
            {
                tMove[moveIndex]++;
            }
        }
        for (int j = 0; j < winnumber.Length; j++)
        {
            if (tMove[j] == -1)
            {
                continue;
            }
            moveCount[j]++;
            if (moveCount[j] > tMove[j] + delayMove[j])
            {
                moveCount[j] = 0;
                randomNumber[j]++;
                if (randomNumber[j] >= 10)
                {
                    randomNumber[j] = 0;
                }
            }
        }
    }

    // --- paintCapcha ---
    public void paintCapcha(mGraphics g)
    {
        MobCapcha.paint(g, Char.myCharz().cx, Char.myCharz().cy);
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if (GameCanvas.menu.showMenu || GameCanvas.panel.isShow || ChatPopup.currChatPopup != null || !GameCanvas.isTouch)
        {
            return;
        }
        for (int i = 0; i < strCapcha.Length; i++)
        {
            int x = (GameCanvas.w - strCapcha.Length * disXC) / 2 + i * disXC + disXC / 2;
            if (keyCapcha[i] == -1)
            {
                g.drawImage(imgNut, x, GameCanvas.h - 25, 3);
                mFont.tahoma_7b_dark.drawString(g, strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
            }
            else
            {
                g.drawImage(imgNutF, x, GameCanvas.h - 25, 3);
                mFont.tahoma_7b_green2.drawString(g, strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
            }
        }
    }

    // --- popups_chatVip ---
    private void paintXoSo(mGraphics g)
    {
        if (tShow != 0)
        {
            string text = string.Empty;
            for (int i = 0; i < winnumber.Length; i++)
            {
                text = text + randomNumber[i] + " ";
            }
            PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, isButton: false);
            mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
            mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
        }
    }

    // targetArrows_npcSearch extracted to GameScr.Targeting.cs

    // touchControls_skill_bars extracted to GameScr.HUD.cs

    public void paintOpen(mGraphics g)
    {
        if (isstarOpen)
        {
            g.translate(-g.getTranslateX(), -g.getTranslateY());
            g.fillRect(0, 0, GameCanvas.w, moveUp);
            g.setColor(10275899);
            g.fillRect(0, moveUp - 1, GameCanvas.w, 1);
            g.fillRect(0, moveDow + 1, GameCanvas.w, 1);
        }
    }

    public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
    {
        int num = -1;
        for (int i = 0; i < 5; i++)
        {
            if (flyTextState[i] == -1)
            {
                num = i;
                break;
            }
        }
        if (num == -1)
        {
            return;
        }
        flyTextColor[num] = color;
        flyTextString[num] = flyString;
        flyTextX[num] = x;
        flyTextY[num] = y;
        flyTextDx[num] = dx;
        flyTextDy[num] = ((dy >= 0) ? 5 : (-5));
        flyTextState[num] = 0;
        flyTime[num] = 0;
        flyTextYTo[num] = 10;
        for (int j = 0; j < 5; j++)
        {
            if (flyTextState[j] != -1 && num != j && flyTextDy[num] < 0 && Res.abs(flyTextX[num] - flyTextX[j]) <= 20 && flyTextYTo[num] == flyTextYTo[j])
            {
                flyTextYTo[num] += 10;
            }
        }
    }

    public static void updateFlyText()
    {
        for (int i = 0; i < 5; i++)
        {
            if (flyTextState[i] == -1)
            {
                continue;
            }
            if (flyTextState[i] > flyTextYTo[i])
            {
                flyTime[i]++;
                if (flyTime[i] == 25)
                {
                    flyTime[i] = 0;
                    flyTextState[i] = -1;
                    flyTextYTo[i] = 0;
                    flyTextDx[i] = 0;
                    flyTextX[i] = 0;
                }
            }
            else
            {
                flyTextState[i] += Res.abs(flyTextDy[i]);
                flyTextX[i] += flyTextDx[i];
                flyTextY[i] += flyTextDy[i];
            }
        }
    }

    public static void loadSplash()
    {
        if (imgSplash == null)
        {
            imgSplash = new Image[3];
            for (int i = 0; i < 3; i++)
            {
                imgSplash[i] = GameCanvas.loadImage("/e/sp" + i + ".png");
            }
        }
        splashX = new int[2];
        splashY = new int[2];
        splashState = new int[2];
        splashF = new int[2];
        splashDir = new int[2];
        splashState[0] = (splashState[1] = -1);
    }

    public static bool startSplash(int x, int y, int dir)
    {
        int num = ((splashState[0] != -1) ? 1 : 0);
        if (splashState[num] != -1)
        {
            return false;
        }
        splashState[num] = 0;
        splashDir[num] = dir;
        splashX[num] = x;
        splashY[num] = y;
        return true;
    }

    public static void updateSplash()
    {
        for (int i = 0; i < 2; i++)
        {
            if (splashState[i] != -1)
            {
                splashState[i]++;
                splashX[i] += splashDir[i] << 2;
                splashY[i]--;
                if (splashState[i] >= 6)
                {
                    splashState[i] = -1;
                }
                else
                {
                    splashF[i] = (splashState[i] >> 1) % 3;
                }
            }
        }
    }

    public static void paintSplash(mGraphics g)
    {
        for (int i = 0; i < 2; i++)
        {
            if (splashState[i] != -1)
            {
                if (splashDir[i] == 1)
                {
                    g.drawImage(imgSplash[splashF[i]], splashX[i], splashY[i], 3);
                }
                else
                {
                    g.drawRegion(imgSplash[splashF[i]], 0, 0, mGraphics.getImageWidth(imgSplash[splashF[i]]), mGraphics.getImageHeight(imgSplash[splashF[i]]), 2, splashX[i], splashY[i], 3);
                }
            }
        }
    }

    // loadInforBar extracted to GameScr.HUD.cs

    public void updateSS()
    {
        if (indexMenu != -1)
        {
            if (cmySK != cmtoYSK)
            {
                cmvySK = cmtoYSK - cmySK << 2;
                cmdySK += cmvySK;
                cmySK += cmdySK >> 4;
                cmdySK &= 15;
            }
            if (Math.abs(cmtoYSK - cmySK) < 15 && cmySK < 0)
            {
                cmtoYSK = 0;
            }
            if (Math.abs(cmtoYSK - cmySK) < 15 && cmySK > cmyLimSK)
            {
                cmtoYSK = cmyLimSK;
            }
        }
    }

    // updateKeyAlert extracted to GameScr.Input.cs

    public bool isPaintPopup()
    {
        if (isPaintItemInfo || isPaintInfoMe || isPaintStore || isPaintWeapon || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintSplit || isPaintUpPearl || isPaintBox || isPaintTrade || isPaintAlert || isPaintZone || isPaintTeam || isPaintClan || isPaintFindTeam || isPaintTask || isPaintFriend || isPaintEnemies || isPaintCharInMap || isPaintMessage)
        {
            return true;
        }
        return false;
    }

    public bool isNotPaintTouchControl()
    {
        if (!GameCanvas.isTouchControl && GameCanvas.currentScreen == gI())
        {
            return true;
        }
        if (!GameCanvas.isTouch)
        {
            return true;
        }
        if (ChatTextField.gI().isShow)
        {
            return true;
        }
        if (InfoDlg.isShow)
        {
            return true;
        }
        if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || isPaintPopup())
        {
            return true;
        }
        return false;
    }

    public bool isPaintUI()
    {
        if (isPaintStore || isPaintWeapon || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintSplit || isPaintUpPearl || isPaintBox || isPaintTrade)
        {
            return true;
        }
        return false;
    }

    public bool isOpenUI()
    {
        if (isPaintItemInfo || isPaintInfoMe || isPaintStore || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintWeapon || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintUpPearl || isPaintBox || isPaintSplit || isPaintTrade)
        {
            return true;
        }
        return false;
    }

    public static void setPopupSize(int w, int h)
    {
        if (GameCanvas.w == 128 || GameCanvas.h <= 208)
        {
            w = 126;
            h = 160;
        }
        indexTitle = 0;
        popupW = w;
        popupH = h;
        popupX = gW2 - w / 2;
        popupY = gH2 - h / 2;
        if (GameCanvas.isTouch && !isPaintZone && !isPaintTeam && !isPaintClan && !isPaintCharInMap && !isPaintFindTeam && !isPaintFriend && !isPaintEnemies && !isPaintTask && !isPaintMessage)
        {
            if (GameCanvas.h <= 240)
            {
                popupY -= 10;
            }
            if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
            {
                popupW = 310;
                popupX = gW / 2 - popupW / 2;
                if (isPaintInfoMe && indexMenu > 0)
                {
                    popupW = w;
                    popupX = gW2 - w / 2;
                }
            }
        }
        if (popupY < -10)
        {
            popupY = -10;
        }
        if (GameCanvas.h > 208 && popupY < 0)
        {
            popupY = 0;
        }
        if (GameCanvas.h == 208 && popupY < 10)
        {
            popupY = 10;
        }
    }

    public static void loadImg()
    {
        TileMap.loadTileImage();
    }

    // paintTitle extracted to GameScr.HUD.cs

    public static int getTaskMapId()
    {
        int num = 0;
        if (Char.myCharz().taskMaint == null)
        {
            return -1;
        }
        return mapTasks[Char.myCharz().taskMaint.index];
    }

    public static sbyte getTaskNpcId()
    {
        sbyte result = 0;
        if (Char.myCharz().taskMaint == null)
        {
            result = -1;
        }
        else if (Char.myCharz().taskMaint.index <= tasks.Length - 1)
        {
            result = (sbyte)tasks[Char.myCharz().taskMaint.index];
        }
        return result;
    }

    public void refreshTeam()
    {
    }

    // actionPerform_chat_menus extracted to GameScr.Actions.cs

    // gamepad_touchBtn extracted to GameScr.Touch.cs

    // paintGamePad extracted to GameScr.HUD.cs

    public void showWinNumber(string num, string finish)
    {
        winnumber = new int[num.Length];
        randomNumber = new int[num.Length];
        tMove = new int[num.Length];
        moveCount = new int[num.Length];
        delayMove = new int[num.Length];
        try
        {
            for (int i = 0; i < num.Length; i++)
            {
                winnumber[i] = short.Parse(num[i].ToString());
                randomNumber[i] = Res.random(0, 11);
                tMove[i] = 1;
                delayMove[i] = 0;
            }
        }
        catch (Exception)
        {
        }
        tShow = 100;
        moveIndex = 0;
        strFinish = finish;
        lastXS = (currXS = mSystem.currentTimeMillis());
    }

    public void chatVip(string chatVip)
    {
        if (!startChat)
        {
            currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
            xChatVip = GameCanvas.w;
            startChat = true;
        }
        if (chatVip.StartsWith("!"))
        {
            chatVip = chatVip.Substring(1, chatVip.Length);
            isFireWorks = true;
        }
        vChatVip.addElement(chatVip);
        if (chatVip.Trim().ToLower().Contains("boss") && chatVip.Trim().ToLower().Contains("xuất hiện"))
        {
            ModFunc.bossNotif.addElement(new ShowBoss(chatVip));
            if (ModFunc.bossNotif.size() > 5)
            {
                ModFunc.bossNotif.removeElementAt(0);
            }
        }
    }

    public void clearChatVip()
    {
        vChatVip.removeAllElements();
        xChatVip = GameCanvas.w;
        startChat = false;
    }

    public void paintChatVip(mGraphics g)
    {
        if (vChatVip.size() != 0 && isPaintChatVip)
        {
            g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
            g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
            string st = (string)vChatVip.elementAt(0);
            mFont.tahoma_7b_yellow.drawStringBorder(g, st, xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
        }
    }

    public void updateChatVip()
    {
        if (!startChat)
        {
            return;
        }
        xChatVip -= 2;
        if (xChatVip < -currChatWidth)
        {
            xChatVip = GameCanvas.w;
            vChatVip.removeElementAt(0);
            if (vChatVip.size() == 0)
            {
                isFireWorks = false;
                startChat = false;
            }
            else
            {
                currChatWidth = mFont.tahoma_7b_white.getWidth((string)vChatVip.elementAt(0));
            }
        }
    }

    public void showYourNumber(string strNum)
    {
        yourNumber = strNum;
        strPaint = mFont.tahoma_7.splitFontArray(yourNumber, 500);
    }

    public static void checkRemoveImage()
    {
        ImgByName.checkDelHash(ImgByName.hashImagePath, 10, isTrue: false);
    }

    public static void StartServerPopUp(string strMsg)
    {
        GameCanvas.endDlg();
        int avatar = 1139;
        Npc npc = new Npc(-1, 0, 0, 0, 0, 0);
        npc.avatar = avatar;
        ChatPopup.addBigMessage(strMsg, 100000, npc);
        ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
        ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
        ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
    }
}
