using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- paintOngMauPercent ---
    public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
    {
        int clipX = g.getClipX();
        int clipY = g.getClipY();
        int clipWidth = g.getClipWidth();
        int clipHeight = g.getClipHeight();
        g.setClip((int)x, (int)y, (int)pixelPercent, 13);
        int num = size / 15 - 2;
        for (int i = 0; i < num; i++)
        {
            g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
        }
        g.drawImage(img0, x, y, 0);
        g.drawImage(img1, x + (float)size - 30f, y, 0);
        g.drawImage(img2, x + (float)size - 15f, y, 0);
        g.setClip(clipX, clipY, clipWidth, clipHeight);
    }

    // --- touchControls_skill_bars ---
    public static void resetTranslate(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
    }

    private void paintTouchControl(mGraphics g)
    {
        if (isNotPaintTouchControl())
        {
            return;
        }
        resetTranslate(g);
        if (!TileMap.isOfflineMap() && !isVS())
        {
            if (keyTouch == 15 || keyMouse == 15)
            {
                g.drawImage(imgChat2, xC + 17, yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            else
            {
                g.drawImage(imgChat, xC + 17, yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
            }
        }
        if (isUseTouch)
        {
        }
    }

    public void paintImageBarRight(mGraphics g, Char c)
    {
        if (c == null) return;
        long hpFull = (c.cHPFull > 0) ? c.cHPFull : 1;
        long mpFull = (c.cMPFull > 0) ? c.cMPFull : 1;
        int num = (int)(c.cHP * hpBarW / hpFull);
        int num2 = (int)c.cMP * mpBarW;
        int num3 = (int)(dHP * hpBarW / hpFull);
        int num4 = (int)dMP * mpBarW;
        g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(imgPanel), 0, 95, 100);
        g.drawRegion(imgPanel, 0, 0, mGraphics.getImageWidth(imgPanel), mGraphics.getImageHeight(imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - hpBarW + hpBarW - num3), 5, num3, 10);
        g.drawImage(imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - hpBarW + hpBarW - num), 5, num, 10);
        g.drawImage(imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - mpBarW + hpBarW - num4), 20, num4, 6);
        g.drawImage(imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - mpBarW + hpBarW - num2), 20, num2, 6);
        g.drawImage(imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
    }

    private void paintImageBar(mGraphics g, bool isLeft, Char c)
    {
        if (c != null)
        {
            long hpFull = (c.cHPFull > 0) ? c.cHPFull : 1;
            long mpFull = (c.cMPFull > 0) ? c.cMPFull : 1;
            int num;
            int num4;
            int num3;
            int num2;
            if (c.charID == Char.myCharz().charID)
            {
                num = (int)(dHP * hpBarW / hpFull);
                num2 = (int)(dMP * mpBarW / mpFull);
                num3 = (int)(c.cHP * hpBarW / hpFull);
                num4 = (int)(c.cMP * mpBarW / mpFull);
            }
            else
            {
                num = (int)(c.dHP * hpBarW / hpFull);
                num2 = c.perCentMp * mpBarW / 100;
                num3 = (int)(c.cHP * hpBarW / hpFull);
                num4 = c.perCentMp * mpBarW / 100;
            }
            if (Char.myCharz().secondPower > 0 && Char.myCharz().maxPowerPoint > 0)
            {
                int w = Char.myCharz().powerPoint * spBarW / Char.myCharz().maxPowerPoint;
                g.drawImage(imgPanel2, 58, 29, 0);
                g.setClip(83, 31, w, 10);
                g.drawImage(imgSP, 83, 31, 0);
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
                mFont.tahoma_7_white.drawString(g, Char.myCharz().strInfo + ":" + Char.myCharz().powerPoint + "/" + Char.myCharz().maxPowerPoint, 115, 29, 2);
            }
            if (c.charID != Char.myCharz().charID)
            {
                g.setClip(mGraphics.getImageWidth(imgPanel) - 95, 0, 95, 100);
            }
            g.drawImage(imgPanel, 0, 0, 0);
            if (isLeft)
            {
                g.setClip(83, 5, num, 10);
            }
            else
            {
                g.setClip((int)(83 + hpBarW - num), 5, num, 10);
            }
            g.drawImage(imgHPLost, 83, 5, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 5, num3, 10);
            }
            else
            {
                g.setClip((int)(83 + hpBarW - num3), 5, num3, 10);
            }
            g.drawImage(imgHP, 83, 5, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 20, num2, 6);
            }
            else
            {
                g.setClip(83 + mpBarW - num2, 20, num2, 6);
            }
            g.drawImage(imgMPLost, 83, 20, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 20, num2, 6);
            }
            else
            {
                g.setClip(83 + mpBarW - num4, 20, num4, 6);
            }
            g.drawImage(imgMP, 83, 20, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (Char.myCharz().cMP == 0 && GameCanvas.gameTick % 10 > 5)
            {
                g.setClip(83, 20, 2, 6);
                g.drawImage(imgMPLost, 83, 20, 0);
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            }
        }
    }

    // injure_vsCombat extracted to GameScr.Targeting.cs

    private void paintSelectedSkill(mGraphics g)
    {
        if (mobCapcha != null)
        {
            paintCapcha(g);
        }
        else
        {
            if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || isPaintPopup() || GameCanvas.panel.isShow || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
            {
                return;
            }
            long num = mSystem.currentTimeMillis();
            long num2 = num - lastUsePotion;
            int num3 = 0;
            if (num2 < 10000)
            {
                num3 = (int)(num2 * 20 / 10000);
            }
            if (!GameCanvas.isTouch)
            {
                g.drawImage((keyTouch != 10) ? imgSkill : imgSkill2, xSkill + xHP - 1, yHP - 1, 0);
                SmallImage.drawSmallImage(g, 542, xSkill + xHP + 3, yHP + 3, 0, 0);
                mFont.number_gray.drawString(g, string.Empty + hpPotion, xSkill + xHP + 22, yHP + 15, 1);
                if (num2 < 10000)
                {
                    g.setColor(2721889);
                    num3 = (int)(num2 * 20 / 10000);
                    g.fillRect(xSkill + xHP + 3, yHP + 3 + num3, 20, 20 - num3);
                }
            }
            else if (Char.myCharz().statusMe != 14)
            {
                if (gamePad.isSmallGamePad)
                {
                    if (isAnalog != 1)
                    {
                        g.setColor(9670800);
                        g.fillRect(xHP + 9, yHP + 10, 22, 20);
                        g.setColor(16777215);
                        g.fillRect(xHP + 9, yHP + 10 + ((num3 != 0) ? (20 - num3) : 0), 22, (num3 == 0) ? 20 : num3);
                        g.drawImage((keyTouch != 10) ? imgHP1 : imgHP2, xHP, yHP, 0);
                        mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 15, 2);
                    }
                    else if (isAnalog == 1)
                    {
                        g.drawImage((keyTouch != 10) ? imgSkill : imgSkill2, xSkill + xHP - 1, yHP - 1, 0);
                        SmallImage.drawSmallImage(g, 542, xSkill + xHP + 3, yHP + 3, 0, 0);
                        mFont.number_gray.drawString(g, string.Empty + hpPotion, xSkill + xHP + 22, yHP + 13, 1);
                        if (num2 < 10000)
                        {
                            g.setColor(2721889);
                            num3 = (int)(num2 * 20 / 10000);
                            g.fillRect(xSkill + xHP + 3, yHP + 3 + num3, 20, 20 - num3);
                        }
                    }
                }
                else if (isAnalog != 1)
                {
                    g.setColor(9670800);
                    g.fillRect(xHP + 9, yHP + 10 - 6, 22, 20);
                    g.setColor(16777215);
                    g.fillRect(xHP + 9, yHP + 10 + ((num3 != 0) ? (20 - num3) : 0) - 6, 22, (num3 == 0) ? 20 : num3);
                    g.drawImage((keyTouch != 10) ? imgHP1 : imgHP2, xHP, yHP - 6, 0);
                    mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 15 - 6, 2);
                }
                else
                {
                    g.setColor(9670800);
                    g.fillRect(xHP + 10, yHP + 10, 20, 18);
                    g.setColor(16777215);
                    g.fillRect(xHP + 10, yHP + 16 + ((num3 != 0) ? (20 - num3) : 0) - 6, 20, (num3 == 0) ? 18 : num3);
                    g.drawImage((keyTouch != 10) ? imgHP3 : imgHP4, xHP + 20, yHP + 20 - 3, mGraphics.HCENTER | mGraphics.VCENTER);
                    mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 11, 2);
                }
            }
            if (isHaveSelectSkill)
            {
                Skill[] array = Main.isPC ? keySkill : ((!GameCanvas.isTouch) ? keySkill : onScreenSkill);
                if (!GameCanvas.isTouch)
                {
                    g.setColor(11152401);
                    g.fillRect(xSkill + xHP + 2, yHP - 10 + 6, 20, 10);
                    mFont.tahoma_7_white.drawString(g, "*", xSkill + xHP + 12, yHP - 8 + 6, mFont.CENTER);
                }
                int num4 = Main.isPC ? array.Length : ((!GameCanvas.isTouch) ? array.Length : nSkill);
                for (int i = 0; i < num4; i++)
                {
                    Skill skill = array[i];
                    if (skill == null)
                    {
                        continue;
                    }
                    if (skill != Char.myCharz().myskill)
                    {
                        g.drawImage(imgSkill, xSkill + xS[i] - 1, yS[i] - 1, 0);
                    }
                    if (skill == Char.myCharz().myskill)
                    {
                        g.drawImage(imgSkill2, xSkill + xS[i] - 1, yS[i] - 1, 0);
                        if (GameCanvas.isTouch && !Main.isPC)
                        {
                            g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, xSkill + xS[i] + 8, yS[i] - 7, 0);
                        }
                    }
                    skill.paint(xSkill + xS[i] + 13, yS[i] + 13, g);
                    if ((i == selectedIndexSkill && !isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == keyTouchSkill)
                    {
                        g.drawImage(ItemMap.imageFlare, xSkill + xS[i] + 13, yS[i] + 14, 3);
                    }
                }
            }
            paintGamePad(g);
        }
    }

    // --- loadInforBar ---
    private void loadInforBar()
    {
        imgScrW = 84;
        hpBarW = 66L;
        mpBarW = 59;
        hpBarX = 52;
        hpBarY = 10;
        spBarW = 61;
        expBarW = gW - 61;
    }

    // --- paintTitle ---
    public void paintTitle(mGraphics g, string title, bool arrow)
    {
        int num = 0;
        num = gW / 2;
        g.setColor(Paint.COLORDARK);
        g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
        if ((indexTitle == 0 || GameCanvas.isTouch) && arrow)
        {
            SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), popupY + 16, 2, StaticObj.VCENTER_HCENTER);
            SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), popupY + 16, 0, StaticObj.VCENTER_HCENTER);
        }
        if (indexTitle == 0)
        {
            g.setColor(Paint.COLORFOCUS);
        }
        else
        {
            g.setColor(Paint.COLORBORDER);
        }
        g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
        mFont.tahoma_8b.drawString(g, title, num, popupY + 9, 2);
    }

    // --- paintGamePad ---
    private void paintGamePad(mGraphics g)
    {
        if (isAnalog != 0 && Char.myCharz().statusMe != 14)
        {
            g.drawImage((keyTouch != 5 && keyMouse != 5) ? imgFire0 : imgFire1, xF + 20, yF + 14, mGraphics.HCENTER | mGraphics.VCENTER);
            gamePad.paint(g);
            g.drawImage((keyTouch != 13) ? imgFocus : imgFocus2, xTG + 20, yTG + 14, mGraphics.HCENTER | mGraphics.VCENTER);
            ModFunc.GI().PaintButton(g, xTG, yTG);
        }
    }

    // --- phuBan_hpBars ---
    public static bool ispaintPhubangBar()
    {
        if (TileMap.mapPhuBang() && phuban_Info.type_PB == 0)
        {
            return true;
        }
        return false;
    }

    public void paintPhuBanBar(mGraphics g, int x, int y, int w)
    {
        if (phuban_Info == null || isPaintOther || isPaintRada != 1 || GameCanvas.panel.isShow || !ispaintPhubangBar())
        {
            return;
        }
        if (w < fra_PVE_Bar_1.frameWidth + fra_PVE_Bar_0.frameWidth * 4)
        {
            w = fra_PVE_Bar_1.frameWidth + fra_PVE_Bar_0.frameWidth * 4;
        }
        if (x > GameCanvas.w - w / 2)
        {
            x = GameCanvas.w - w / 2;
        }
        if (x < mGraphics.getImageWidth(imgKhung) + w / 2 + 10)
        {
            x = mGraphics.getImageWidth(imgKhung) + w / 2 + 10;
        }
        int frameHeight = fra_PVE_Bar_0.frameHeight;
        int num = y + frameHeight + mGraphics.getImageHeight(imgBall) / 2 + 2;
        int frameWidth = fra_PVE_Bar_1.frameWidth;
        int num2 = w / 2 - frameWidth / 2;
        int num3 = x - w / 2;
        int num4 = x + frameWidth / 2;
        int y2 = y + 3;
        int num5 = num2 - fra_PVE_Bar_0.frameWidth;
        int num6 = num5 / fra_PVE_Bar_0.frameWidth;
        if (num5 % fra_PVE_Bar_0.frameWidth > 0)
        {
            num6++;
        }
        for (int i = 0; i < num6; i++)
        {
            if (i < num6 - 1)
            {
                fra_PVE_Bar_0.drawFrame(1, num3 + fra_PVE_Bar_0.frameWidth + i * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
            else
            {
                fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
            }
            if (i < num6 - 1)
            {
                fra_PVE_Bar_0.drawFrame(1, num4 + i * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
            else
            {
                fra_PVE_Bar_0.drawFrame(1, num4 + num5 - fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
        }
        fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
        fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
        if (phuban_Info.pointTeam1 > 0)
        {
            int idx = 2;
            int idx2 = 3;
            if (phuban_Info.color_1 == 4)
            {
                idx = 4;
                idx2 = 5;
            }
            int num7 = phuban_Info.pointTeam1 * num2 / phuban_Info.maxPoint;
            if (num7 < 0)
            {
                num7 = 0;
            }
            if (num7 > num2)
            {
                num7 = num2;
            }
            g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
            for (int j = 0; j < num6; j++)
            {
                if (j < num6 - 1)
                {
                    fra_PVE_Bar_0.drawFrame(idx2, num3 + fra_PVE_Bar_0.frameWidth + j * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
                else
                {
                    fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
                }
            }
            fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
            GameCanvas.resetTrans(g);
        }
        if (phuban_Info.pointTeam2 > 0)
        {
            int idx3 = 2;
            int idx4 = 3;
            if (phuban_Info.color_2 == 4)
            {
                idx3 = 4;
                idx4 = 5;
            }
            int num8 = phuban_Info.pointTeam2 * num2 / phuban_Info.maxPoint;
            if (num8 < 0)
            {
                num8 = 0;
            }
            if (num8 > num2)
            {
                num8 = num2;
            }
            g.setClip(num4, y2, num8, frameHeight);
            for (int k = 0; k < num6; k++)
            {
                if (k < num6 - 1)
                {
                    fra_PVE_Bar_0.drawFrame(idx4, num4 + k * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
                else
                {
                    fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
            }
            fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
            GameCanvas.resetTrans(g);
        }
        fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
        string timeCountDown = mSystem.getTimeCountDown(phuban_Info.timeStart, phuban_Info.timeSecond, isOnlySecond: true, isShortText: false);
        mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
        Panel.setTextColor(phuban_Info.color_1, 1).drawString(g, phuban_Info.nameTeam1, x - 5, num + 5, 1);
        Panel.setTextColor(phuban_Info.color_2, 1).drawString(g, phuban_Info.nameTeam2, x + 5, num + 5, 0);
        if (phuban_Info.type_PB != 0)
        {
            int y3 = y + frameHeight / 2 - 2;
            mFont.bigNumber_While.drawString(g, string.Empty + phuban_Info.pointTeam1, num3 + num2 / 2, y3, 2);
            mFont.bigNumber_While.drawString(g, string.Empty + phuban_Info.pointTeam2, num4 + num2 / 2, y3, 2);
        }
        g.drawImage(imgVS, x, y + fra_PVE_Bar_1.frameHeight + 2, 3);
        if (phuban_Info.type_PB == 0)
        {
            paintChienTruong_Life(g, phuban_Info.maxLife, phuban_Info.color_1, phuban_Info.lifeTeam1, x - 13, phuban_Info.color_2, phuban_Info.lifeTeam2, x + 13, num);
        }
    }

    public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
    {
        if (imgBall == null)
        {
            return;
        }
        int num = mGraphics.getImageHeight(imgBall) / 2;
        for (int i = 0; i < maxLife; i++)
        {
            int num2 = 0;
            if (i < lifeTeam1)
            {
                num2 = 1;
            }
            g.drawRegion(imgBall, 0, num2 * num, mGraphics.getImageWidth(imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
        }
        for (int j = 0; j < maxLife; j++)
        {
            int num3 = 0;
            if (j < lifeTeam2)
            {
                num3 = 1;
            }
            g.drawRegion(imgBall, 0, num3 * num, mGraphics.getImageWidth(imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
        }
    }

    public static void paintHPBar_NEW(mGraphics g, int x, int y, Char c)
    {
        g.drawImage(imgKhung, x, y, 0);
        int x2 = x + 3;
        int num = y + 19;
        int num2 = 0;
        int num3 = 0;
        int width = imgHP_NEW.getWidth();
        int num4 = imgHP_NEW.getHeight() / 2;
        num2 = (int)(c.cHP * width / c.cHPFull);
        if (num2 <= 0)
        {
            num2 = 1;
        }
        else if (num2 > width)
        {
            num2 = width;
        }
        g.drawRegion(imgHP_NEW, 0, num4, num2, num4, 0, x2, num, 0);
        num3 = (int)(c.cMP * width / c.cMPFull);
        if (num3 <= 0)
        {
            num3 = 1;
        }
        else if (num3 > width)
        {
            num3 = width;
        }
        g.drawRegion(imgHP_NEW, 0, 0, num3, num4, 0, x2, num + 6, 0);
        int x3 = x + imgKhung.getWidth() / 2 + 1;
        int y2 = num + 13;
        mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
        if (c.mobFocus != null)
        {
            if (c.mobFocus.getTemplate() != null)
            {
                mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
            }
        }
        else if (c.npcFocus != null)
        {
            mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
        }
        else if (c.charFocus != null)
        {
            mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
        }
    }

    // --- paint_xp_bar ---
    private void paint_xp_bar(mGraphics g)
    {
        g.setColor(8421504);
        g.fillRect(0, GameCanvas.h - 2, GameCanvas.w, 2);
        int w = (int)(Char.myCharz().cLevelPercent * GameCanvas.w / 10000);
        g.setColor(16777215);
        g.fillRect(0, GameCanvas.h - 2, w, 2);
        g.setColor(0);
        w = GameCanvas.w / 10;
        for (int i = 1; i < 10; i++)
        {
            g.fillRect(i * w, GameCanvas.h - 2, 1, 2);
        }
    }
}
