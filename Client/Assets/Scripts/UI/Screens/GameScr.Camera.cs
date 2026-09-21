using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- loadBg ---
    public static void loadBg()
    {

       QuayTamBao.loadImage();
        fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
        fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
        imgVS = mSystem.loadImage("/mainImage/i_vs.png");
        imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
        imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
        imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
        imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
        imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
        imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
        imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
        imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
        imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
        imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
        imgSP = GameCanvas.loadImage("/mainImage/SP.png");
        imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
        imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
        imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
        imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
        imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
        imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
        imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
        imgHP_tm_do = GameCanvas.loadImage("/mainImage/tm-do.png");
        imgHP_tm_vang = GameCanvas.loadImage("/mainImage/tm-vang.png");
        imgHP_tm_xam = GameCanvas.loadImage("/mainImage/tm-xam.png");
        imgHP_tm_xanh = GameCanvas.loadImage("/mainImage/tm-xanh.png");
        if (GameCanvas.isTouch)
        {
            imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
            imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
            imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
            imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
            imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
            imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
            imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
            imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
            imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
            imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
            imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
            imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
            imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
            imgModFunc = GameCanvas.loadImage("/mainImage/imgModFuc.png");
        }
        flyTextX = new int[5];
        flyTextY = new int[5];
        flyTextDx = new int[5];
        flyTextDy = new int[5];
        flyTextState = new int[5];
        flyTextString = new string[5];
        flyTextYTo = new int[5];
        flyTime = new int[5];
        flyTextColor = new int[8];
        for (int i = 0; i < 5; i++)
        {
            flyTextState[i] = -1;
        }
        sbyte[] array = Rms.loadRMS("NRdataVersion");
        sbyte[] array2 = Rms.loadRMS("NRmapVersion");
        sbyte[] array3 = Rms.loadRMS("NRskillVersion");
        sbyte[] array4 = Rms.loadRMS("NRitemVersion");
        if (array != null)
        {
            vcData = array[0];
        }
        if (array2 != null)
        {
            vcMap = array2[0];
        }
        if (array3 != null)
        {
            vcSkill = array3[0];
        }
        if (array4 != null)
        {
            vcItem = array4[0];
        }
        imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
        imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
        MobCapcha.init();
        isAnalog = ((Rms.loadRMSInt("analog") == 0) ? 0 : Main.isIPhone ? 1 : 0);
        gamePad = new GamePad();
        arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
        imgTrans = GameCanvas.loadImage("/bg/trans.png");
        imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
        frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
        frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
        frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
        frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
        frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
        frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
    }

    // --- mapChecks ---
    public bool isMapDocNhan()
    {
        if (TileMap.mapID >= 53 && TileMap.mapID <= 62)
        {
            return true;
        }
        return false;
    }

    public bool isMapFize()
    {
        if (TileMap.mapID >= 63)
        {
            return true;
        }
        return false;
    }

    // --- camera ---
    public static void loadCamera(bool fullmScreen, int cx, int cy)
    {
        gW = GameCanvas.w;
        cmdBarH = 39;
        gH = GameCanvas.h;
        cmdBarW = gW;
        cmdBarX = 0;
        cmdBarY = GameCanvas.h - Paint.hTab - cmdBarH;
        girlHPBarY = 0;
        csPadMaxH = GameCanvas.h / 6;
        if (csPadMaxH < 48)
        {
            csPadMaxH = 48;
        }
        gW2 = gW >> 1;
        gH2 = gH >> 1;
        gW3 = gW / 3;
        gH3 = gH / 3;
        gW23 = gH - 120;
        gH23 = gH * 2 / 3;
        gW34 = 3 * gW / 4;
        gH34 = 3 * gH / 4;
        gW6 = gW / 6;
        gH6 = gH / 6;
        gssw = gW / TileMap.size + 2;
        gssh = gH / TileMap.size + 2;
        if (gW % 24 != 0)
        {
            gssw++;
        }
        cmxLim = (TileMap.tmw - 1) * TileMap.size - gW;
        cmyLim = (TileMap.tmh - 1) * TileMap.size - gH;
        if (cx == -1 && cy == -1)
        {
            cmx = (cmtoX = Char.myCharz().cx - gW2 + gW6 * Char.myCharz().cdir);
            cmy = (cmtoY = Char.myCharz().cy - gH23);
        }
        else
        {
            cmx = (cmtoX = cx - gW23 + gW6 * Char.myCharz().cdir);
            cmy = (cmtoY = cy - gH23);
        }
        firstY = cmy;
        if (cmx < 24)
        {
            cmx = (cmtoX = 24);
        }
        if (cmx > cmxLim)
        {
            cmx = (cmtoX = cmxLim);
        }
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        gssx = cmx / TileMap.size - 1;
        if (gssx < 0)
        {
            gssx = 0;
        }
        gssy = cmy / TileMap.size;
        gssxe = gssx + gssw;
        gssye = gssy + gssh;
        if (gssy < 0)
        {
            gssy = 0;
        }
        if (gssye > TileMap.tmh - 1)
        {
            gssye = TileMap.tmh - 1;
        }
        TileMap.countx = (gssxe - gssx) * 4;
        if (TileMap.countx > TileMap.tmw)
        {
            TileMap.countx = TileMap.tmw;
        }
        TileMap.county = (gssye - gssy) * 4;
        if (TileMap.county > TileMap.tmh)
        {
            TileMap.county = TileMap.tmh;
        }
        TileMap.gssx = (Char.myCharz().cx - 2 * gW) / TileMap.size;
        if (TileMap.gssx < 0)
        {
            TileMap.gssx = 0;
        }
        TileMap.gssxe = TileMap.gssx + TileMap.countx;
        if (TileMap.gssxe > TileMap.tmw)
        {
            TileMap.gssxe = TileMap.tmw;
        }
        TileMap.gssy = (Char.myCharz().cy - 2 * gH) / TileMap.size;
        if (TileMap.gssy < 0)
        {
            TileMap.gssy = 0;
        }
        TileMap.gssye = TileMap.gssy + TileMap.county;
        if (TileMap.gssye > TileMap.tmh)
        {
            TileMap.gssye = TileMap.tmh;
        }
        ChatTextField.gI().parentScreen = instance;
        ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
        ChatTextField.gI().initChatTextField();
        if (GameCanvas.isTouch)
        {
            yTouchBar = gH - 88;
            xC = gW - 40;
            yC = 2;
            if (GameCanvas.w <= 240)
            {
                xC = gW - 35;
                yC = 5;
            }
            xF = gW - 55;
            yF = yTouchBar + 35;
            xTG = gW - 37;
            yTG = yTouchBar - 1;
            if (GameCanvas.w >= 450)
            {
                yTG -= 12;
                yHP -= 7;
                xF -= 10;
                yF -= 5;
                xTG -= 10;
            }
        }
        setSkillBarPosition();
        disXC = ((GameCanvas.w <= 200) ? 30 : 40);
        if (Rms.loadRMSInt("viewchat") == -1)
        {
            GameCanvas.panel.isViewChatServer = true;
        }
        else
        {
            GameCanvas.panel.isViewChatServer = Rms.loadRMSInt("viewchat") == 1;
        }
    }

    // skillBarPosition extracted to GameScr.Skills.cs

    private static void updateCamera()
    {
        if (isPaintOther)
        {
            return;
        }
        if (cmx != cmtoX || cmy != cmtoY)
        {
            cmvx = cmtoX - cmx << 2;
            cmvy = cmtoY - cmy << 2;
            cmdx += cmvx;
            cmx += cmdx >> 4;
            cmdx &= 15;
            cmdy += cmvy;
            cmy += cmdy >> 4;
            cmdy &= 15;
            if (cmx < 24)
            {
                cmx = 24;
            }
            if (cmx > cmxLim)
            {
                cmx = cmxLim;
            }
            if (cmy < 0)
            {
                cmy = 0;
            }
            if (cmy > cmyLim)
            {
                cmy = cmyLim;
            }
        }
        gssx = cmx / TileMap.size - 1;
        if (gssx < 0)
        {
            gssx = 0;
        }
        gssy = cmy / TileMap.size;
        gssxe = gssx + gssw;
        gssye = gssy + gssh;
        if (gssy < 0)
        {
            gssy = 0;
        }
        if (gssye > TileMap.tmh - 1)
        {
            gssye = TileMap.tmh - 1;
        }
        TileMap.gssx = (Char.myCharz().cx - 2 * gW) / TileMap.size;
        if (TileMap.gssx < 0)
        {
            TileMap.gssx = 0;
        }
        TileMap.gssxe = TileMap.gssx + TileMap.countx;
        if (TileMap.gssxe > TileMap.tmw)
        {
            TileMap.gssxe = TileMap.tmw;
            TileMap.gssx = TileMap.gssxe - TileMap.countx;
        }
        TileMap.gssy = (Char.myCharz().cy - 2 * gH) / TileMap.size;
        if (TileMap.gssy < 0)
        {
            TileMap.gssy = 0;
        }
        TileMap.gssye = TileMap.gssy + TileMap.county;
        if (TileMap.gssye > TileMap.tmh)
        {
            TileMap.gssye = TileMap.tmh;
            TileMap.gssy = TileMap.gssye - TileMap.county;
        }
        scrMain.updatecm();
        scrInfo.updatecm();
    }

    // --- isVsMap ---
    public bool isVsMap()
    {
        return true;
    }

    // --- rongThan ---
    public void activeRongThanEff(bool isMe)
    {
        activeRongThan = true;
        isUseFreez = true;
        isMeCallRongThan = true;
        if (isMe)
        {
            Effect me = new Effect(20, Char.myCharz().cx, Char.myCharz().cy - 77, 2, 8, 1);
            EffecMn.addEff(me);
        }
    }

    public void hideRongThanEff()
    {
        activeRongThan = false;
        isUseFreez = true;
        isMeCallRongThan = false;
    }

    public void doiMauTroi()
    {
        isRongThanXuatHien = true;
        mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
    }

    public void callRongThan(int x, int y)
    {
        Res.outz("VE RONG THAN O VI TRI x= " + x + " y=" + y);
        doiMauTroi();
        Effect me = new Effect((!isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1);
        EffecMn.addEff(me);
    }

    public void hideRongThan()
    {
        isRongThanXuatHien = false;
        EffecMn.removeEff(17);
        if (isRongNamek)
        {
            isRongNamek = false;
            EffecMn.removeEff(25);
        }
    }

    // --- isRongThanMenu ---
    public bool isRongThanMenu()
    {
        if (isMeCallRongThan)
        {
            return true;
        }
        return false;
    }

    // --- paintSky ---
    public void paintBgItem(mGraphics g, int layer)
    {
        for (int i = 0; i < TileMap.vCurrItem.size(); i++)
        {
            BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
            if (bgItem.idImage != -1 && bgItem.layer == layer)
            {
                bgItem.paint(g);
            }
        }
        if (TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0)
        {
            for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
            {
                g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
            }
        }
    }

    public void paintBlackSky(mGraphics g)
    {
        if (!GameCanvas.lowGraphic)
        {
            g.fillTrans(imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
        }
    }

    // --- paint_ios_bg ---
    private void paint_ios_bg(mGraphics g)
    {
        if (mSystem.clientType == 5)
        {
            if (imgBgIOS != null)
            {
                g.setColor(16777215);
                g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
                g.drawImage(imgBgIOS, GameCanvas.w / 2, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
            }
            else
            {
                int num = ((TileMap.bgID % 2 != 0) ? 1 : 2);
                imgBgIOS = GameCanvas.loadImage("/bg/bg_ios_" + num + ".png");
            }
        }
    }
}
