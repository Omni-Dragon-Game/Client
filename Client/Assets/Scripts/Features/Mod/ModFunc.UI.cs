using JetBrains.Annotations;
using Mod;
using Mod.XMAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;

public partial class ModFunc
{
    public void OpenMenu()
    {
        MyVector myVector = new();
        myVector.addElement(new Command("Bản đồ", 883)); // Xong
        myVector.addElement(new Command("Luyện tập", 45)); // Xong
        myVector.addElement(new Command("Nhặt đồ", 89));
        //myVector.addElement(new Command("Đậu thần", 14));
        myVector.addElement(new Command("Đệ tử", 16));
        myVector.addElement(new Command("BOSS", 32)); // Xong
        //myVector.addElement(new Command(Mod.doHoa, 15));
        //myVector.addElement(new Command(Mod.upYardrat, 31));
        myVector.addElement(new Command("Khác", 53));
        GameCanvas.menu.startAt(myVector, 4);
    }

    public bool UpdateKey(int key)
    {
        switch (key)
        {
            case 'z':
                NewBagUI.GI().OpenBag();
                break;
            case 'e':
                // Friend
                Service.gI().friend(0, -1);
                InfoDlg.showWait();
                break;
            case 'h':
                // Update khu
                GameScr.gI().onChatFromMe("ukhu", string.Empty);
                break;
            case 'u':
                // Auto attack
                perform(42, null);
                break;
            case 'x':
                OpenMenu();
                break;
            case 'f':
                UsePorata();
                break;
            case 'c':
                UseItem(194);
                break;
            case 'm':
                userOpenZones = true;
                Service.gI().openUIZone();
                break;
            case 't':
                UseItem(521);
                break;
            case 'n':
                PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
                GameScr.info1.addInfo("Tự động nhặt: " + (PickMob.IsAutoPickItems ? "Bật" : "Tắt"), 0);
                break;
            case 'j':
                ManualXmap.GI().LoadMapLeft();
                break;
            case 'k':
                ManualXmap.GI().LoadMapCenter();
                break;
            case 'l':
                ManualXmap.GI().LoadMapRight();
                break;
            case 'g':
                if (Char.myCharz().charFocus != null)
                {
                    Service.gI().giaodich(0, Char.myCharz().charFocus.charID, -1, -1);
                    GameScr.info1.addInfo("Đã gửi lời mời giao dịch đến " + Char.myCharz().charFocus.cName, 0);
                }
                break;
            default:
                return false;
        }
        return true;
    }

    private void UpdateTouch()
    {
        if (GameScr.gI().isNotPaintTouchControl())
        {
            return;
        }
        if (GameCanvas.isPointerHoldIn(modKeyPosX + 2, modKeyPosY - 41, 32, 32))
        {
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                UseItem(194);
                GameCanvas.clearAllPointerEvent();
            }
        }
        // F button
        else if (GameCanvas.isPointerHoldIn(modKeyPosX - 39, modKeyPosY + 6, 32, 32))
        {
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                UsePorata();
                GameCanvas.clearAllPointerEvent();
            }
        }
        // M button
        else if (GameCanvas.isPointerHoldIn(modKeyPosX - 84, modKeyPosY + 47, 32, 32))
        {
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                userOpenZones = true;
                Service.gI().openUIZone();
                GameCanvas.clearAllPointerEvent();
            }
        }
        // Open Menu Mod
        else if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 8, 3, GameScr.imgModFunc.getWidth() + 2, GameScr.imgModFunc.getHeight() + 2))
        {
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                OpenMenu();
                SoundMn.gI().buttonClick();
                GameCanvas.clearAllPointerEvent();
            }
        }
    }

    public void PaintButton(mGraphics g, int xAnchor, int yAnchor)
    {
        if (Main.isIPhone && isShowButton)
        {
            if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameScr.gI().isPaintPopup() || GameCanvas.panel.isShow || Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
            {
                return;
            }

            modKeyPosX = xAnchor;
            modKeyPosY = yAnchor;

            // C button
            g.drawImage(GameScr.imgNut, xAnchor + 20, yAnchor - 26, mGraphics.HCENTER | mGraphics.VCENTER);
            mFont.tahoma_7_green2.drawString(g, "C", xAnchor + 6 + (GameScr.imgNut.getWidth() / 2), yAnchor - 47 + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            if (GameCanvas.isPointerHoldIn(xAnchor + 2, yAnchor - 41, 32, 32))
            {
                g.drawImage(GameScr.imgNutF, xAnchor + 20, yAnchor - 26, mGraphics.HCENTER | mGraphics.VCENTER);
                mFont.tahoma_7b_focus.drawString(g, "C", xAnchor + 6 + (GameScr.imgNut.getWidth() / 2), yAnchor - 46 + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            }
            // F button
            g.drawImage(GameScr.imgNut, xAnchor - 21, yAnchor + 21, mGraphics.HCENTER | mGraphics.VCENTER);
            mFont.tahoma_7_green2.drawString(g, "F", xAnchor - 35 + (GameScr.imgNut.getWidth() / 2), yAnchor + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            if (GameCanvas.isPointerHoldIn(xAnchor - 39, yAnchor + 6, 32, 32))
            {
                g.drawImage(GameScr.imgNutF, xAnchor - 21, yAnchor + 21, mGraphics.HCENTER | mGraphics.VCENTER);
                mFont.tahoma_7b_focus.drawString(g, "F", xAnchor - 35 + (GameScr.imgNut.getWidth() / 2), yAnchor + 1 + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            }
            // M button
            g.drawImage(GameScr.imgNut, xAnchor - 66, yAnchor + 62, mGraphics.HCENTER | mGraphics.VCENTER);
            mFont.tahoma_7_green2.drawString(g, "M", xAnchor - 80 + (GameScr.imgNut.getWidth() / 2), yAnchor + 41 + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            if (GameCanvas.isPointerHoldIn(xAnchor - 84, yAnchor + 47, 32, 32))
            {
                g.drawImage(GameScr.imgNutF, xAnchor - 66, yAnchor + 62, mGraphics.HCENTER | mGraphics.VCENTER);
                mFont.tahoma_7b_focus.drawString(g, "M", xAnchor - 80 + (GameScr.imgNut.getWidth() / 2), yAnchor + 42 + (GameScr.imgNut.getHeight() / 2), mGraphics.HCENTER | mGraphics.VCENTER);
            }
        }
    }

    public void Paint(mGraphics g)
    {
        int imgHPWidth = mGraphics.getImageWidth(GameScr.imgHP);
        int imgMPWidth = mGraphics.getImageWidth(GameScr.imgMP);

        // FPS
        //if (mSystem.currentTimeMillis() - lastUpdateFPS > 200L)
        //{
        //    lastFps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);
        //    lastUpdateFPS = mSystem.currentTimeMillis();
        //}
        //mFont.tahoma_7_white.drawStringBorder(g, "FPS: " + lastFps, 56 + imgMPWidth / 2, 30, mFont.LEFT, mFont.tahoma_7_grey);

        //mFont.tahoma_7_red.drawStringBorder(g, NinjaUtil.NumberTostring(Char.myCharz().cHP.ToString()), 84 + imgHPWidth / 2, 4, mFont.CENTER, mFont.tahoma_7_grey);
        //mFont.tahoma_7_blue1.drawStringBorder(g, NinjaUtil.NumberTostring(Char.myCharz().cMP.ToString()), 84 + imgMPWidth / 2, 17, mFont.CENTER, mFont.tahoma_7_grey);
        mFont.tahoma_7_red.drawStringBorder(g, NinjaUtil.NumberTostring(Panel.formatLargeNumber(Char.myCharz().cHP)), 84 + imgHPWidth / 2, 4, mFont.CENTER, mFont.tahoma_7_grey);
        mFont.tahoma_7_blue1.drawStringBorder(g, NinjaUtil.NumberTostring(Panel.formatLargeNumber(Char.myCharz().cMP)), 84 + imgMPWidth / 2, 17, mFont.CENTER, mFont.tahoma_7_grey);
        int xText = 90;
        int yText = GameScr.gI().cmdMenu.y - 20;

        if (!showInfoMe)
        {
            //mFont.tahoma_7_yellow.drawStringBorder(g, "Time: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), xText, yText, mFont.LEFT, mFont.tahoma_7_grey);
            int num2 = 0;
            mFont.tahoma_7_red.drawStringBorder(g, string.Concat(new object[]
            {
                TileMap.mapName,
                " [",
                TileMap.mapID,
                "]  - Khu: ",
                TileMap.zoneID
                }), xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);

            num2 += 10;
            mFont.tahoma_7_red.drawStringBorder(g, string.Concat(new object[]
            {
                "X: ",
                Char.myCharz().cx,
                " - Y: ",
                Char.myCharz().cy
            }), xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
        }

        if (isAutoPhaLe)
        {
            mFont.tahoma_7b_red.drawString(g, (itemPhale != null) ? itemPhale.template.name : "Chưa Có", GameCanvas.w / 2, 72, mFont.CENTER);
            mFont.tahoma_7b_red.drawString(g, (itemPhale != null) ? ("Số Sao : " + currPhale.ToString()) : "Số Sao : -1", GameCanvas.w / 2, 82, mFont.CENTER);
            mFont.tahoma_7b_red.drawString(g, "Số Sao Cần Đập : " + maxPhale + " Sao", GameCanvas.w / 2, 92, mFont.CENTER);
        }
        if (isAutoPhaLe || isAutoVQMM)
        {
            Item tv = FindItemBagWithIndexUI(FindItemIndex(457));
            mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys((long)Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys(Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
            mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
            {
                "Vàng : ",
                NinjaUtil.getMoneys(Char.myCharz().xu),
                " Thỏi Vàng : ",
                tv == null ? 0 : tv.quantity
            }), GameCanvas.w / 2, 112, mFont.CENTER);
        }

        //if (Mod.isPKM && !Mod.isGMT && (Char.myCharz().charFocus == null || (Char.myCharz().charFocus != null && !Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))))
        //{
        //    for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        //    {
        //        Char @char = (Char)GameScr.vCharInMap.elementAt(i);
        //        if (@char != null && Char.myCharz().isMeCanAttackOtherPlayer(@char) && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("$") && !@char.cName.StartsWith("#") && @char.charID >= 0)
        //        {
        //            Char.myCharz().focusManualTo(@char);
        //            return;
        //        }
        //    }
        //}

        if (showInfoMe)
        {
            if (mSystem.currentTimeMillis() - lastUpdateInfoMe > 3000L)
            {
                Service.gI().petInfo();
                lastUpdateInfoMe = mSystem.currentTimeMillis();
            }

            int num = 10;
            int numy = 64;
            mFont.tahoma_7b_yellow.drawStringBorder(g, "Sư Phụ :", xText, yText, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myCharz().cPower), xText, yText + num, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), xText, yText + 2 * num, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull), xText, yText + 3 * num, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myCharz().cDefull), xText, yText + 4 * num, mFont.LEFT, mFont.tahoma_7_grey);

            xText += GameCanvas.w / 8;

            mFont.tahoma_7b_yellow.drawStringBorder(g, "Đệ Tử :", xText - numy, yText + numy, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myPetz().cPower), xText - numy , yText + num + numy, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myPetz().cTiemNang), xText - numy, yText + 2 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myPetz().cDamFull), xText - numy, yText + 3 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "HP : " + NinjaUtil.getMoneys(Char.myPetz().cHP), xText - numy, yText + 4 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "MP : " + NinjaUtil.getMoneys(Char.myPetz().cMP), xText - numy, yText + 5 * num + numy , mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myPetz().cDefull), xText - numy, yText + 6 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
        }

        if (notifBoss)
        {
            int numX = 38;
            for (int i = 0; i < bossNotif.size(); i++)
            {
                ((ShowBoss)bossNotif.elementAt(i)).PaintBoss(g, GameCanvas.w - 2, numX, mFont.RIGHT);
                numX += 10;
            }
        }

        if (showCharsInMap)
        {
            int numX = GameCanvas.w - 130;
            int numY = notifBoss ? 92 : 50;
            charsInMap.removeAllElements();
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                if (i > 15 || numY > GameScr.yHP - 20)
                {
                    g.fillRect(numX - 2, numY + 1, 150, 10, 2721889, 90);
                    mFont.tahoma_7_white.drawStringBorder(g, string.Concat(new object[]
                    {
                    i + 1,
                    " ..."
                    }), numX, numY, mFont.LEFT, mFont.tahoma_7_grey);
                    break;
                }
                Char char6 = (Char)GameScr.vCharInMap.elementAt(i);
                if (char6 != null && char6.cName != null && char6.cName.Length > 0
                    && !char6.isMiniPet // Minipet
                    && char6.cName.ToLower() != "trọng tài")
                {
                    g.fillRect(numX - 2, numY + 1, 150, 10, 2721889, 90);
                    string[] str = new string[]
                        {
                        (i + 1) < 10 ? "0" : "",
                        (i + 1).ToString(),
                        ". [",
                        CharGender(char6),
                        "] ",
                        char6.cName,
                        " [ ",
                        NinjaUtil.getMoneys(char6.cHP).ToString(),
                        " ]"
                        };

                    // Player focus
                    if (char6 == Char.myCharz().charFocus)
                    {
                        mFont.tahoma_7_yellow.drawStringBorder(g, string.Concat(str), numX, numY, mFont.LEFT, mFont.tahoma_7_grey);
                    }
                    // Boss
                    else if (char6.charID < 0 && char6.charID > -1000 && char6.charID != -114)
                    {
                        mFont.tahoma_7_red.drawStringBorder(g, string.Concat(str), numX, numY, mFont.LEFT, mFont.tahoma_7_grey);
                    }
                    // Same clan
                    else if (Char.myCharz().clan != null && char6.clanID == Char.myCharz().clan.ID)
                    {
                        mFont.tahoma_7_green.drawStringBorder(g, string.Concat(str), numX, numY, mFont.LEFT, mFont.tahoma_7_grey);
                    }
                    // Others
                    else
                    {
                        mFont.tahoma_7_white.drawStringBorder(g, string.Concat(str), numX, numY, mFont.LEFT, mFont.tahoma_7_grey);
                    }

                    charsInMap.addElement(char6);
                    numY += 10;
                }
            }
        }

        int num4 = 70;
        Char charFocus = Char.myCharz().charFocus;
        if (charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(charFocus))
        {
            mFont.tahoma_7b_red.drawStringBorder(g, string.Concat(new object[]
            {
                        charFocus.cName,
                        " [",
                        NinjaUtil.getMoneys(charFocus.cHP),
                        " / ",
                        NinjaUtil.getMoneys(charFocus.cHPFull),
                        "] [",
                        CharGender(charFocus),
                        "]"
            }), GameCanvas.w / 2, num4, mFont.CENTER, mFont.tahoma_7_grey);
            num4 += 10;
            if (charFocus.protectEff)
            {
                mFont.tahoma_7b_red.drawString(g, "Đang khiên năng lượng", GameCanvas.w / 2, num4, mFont.CENTER);
                num4 += 10;
            }
            if (charFocus.isMonkey == 1)
            {
                mFont.tahoma_7b_red.drawString(g, "Đang biến khỉ", GameCanvas.w / 2, num4, mFont.CENTER);
                num4 += 10;
            }
            if (charFocus.sleepEff)
            {
                mFont.tahoma_7b_red.drawString(g, "Bị thôi miên", GameCanvas.w / 2, num4, mFont.CENTER);
                num4 += 10;
            }
            if (charFocus.holdEffID != 0)
            {
                mFont.tahoma_7b_red.drawString(g, "Bị trói", GameCanvas.w / 2, num4, mFont.CENTER);
                num4 += 10;
            }
            if (charFocus.isFreez)
            {
                mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + charFocus.freezSeconds.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
                num4 += 10;
            }
            if (charFocus.blindEff)
            {
                mFont.tahoma_7b_red.drawString(g, "Bị choáng", GameCanvas.w / 2, num4, mFont.CENTER);
            }
        }

        if (lineToBoss)
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
                {
                    g.setColor(Color.red);
                    g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
                }
            }
        }

        // Logo EMTI disabled to keep game HUD clean
        /*
        if (TileMap.mapID != 51 && TileMap.mapID != 52 && TileMap.mapID != 113 && TileMap.mapID != 129 && TileMap.mapID != 165)
        {
            int id = GameCanvas.gameTick / 4 % 30;
            if (logos[id] != null)
            {
                int imgW = logos[id].getWidth() * mGraphics.zoomLevel / 4;
                int imgH = logos[id].getHeight() * mGraphics.zoomLevel / 4;
                g.drawImageScale(logos[id], (GameCanvas.w - imgW) / 2, -5, imgW, imgH);//chinh logo
            }
        }
        */

        g.drawImage(GameScr.imgModFunc, GameScr.imgPanel.getWidth() + 20, 15, 3);

        PaintPlayerTichXanh(g);
    }

    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 1:
                string notif;
                bool success = int.TryParse((string)p, out int mapId);
                if (success)
                {
                    XmapController.StartRunToMapId(mapId);
                    notif = "Di chuyển đến boss ở MAP " + mapId;
                }
                else
                {
                    notif = "Địa điểm không hợp lệ!";
                }
                GameScr.info1.addInfo(notif, 0);
                break;
            case 2:
                GameScr.info1.addInfo("Đã huỷ di chuyển đến Boss", 0);
                break;
            case 8:
                break;
            case 16:
                MyVector menuPet = new();
                menuPet.addElement(new Command(isPeanPet ? "Buff đậu cho đệ [Bật]" : "Buff đậu cho đệ [Tắt]", 17));
                GameCanvas.menu.startAt(menuPet, 4);
                break;
            case 17:
                isPeanPet = !isPeanPet;
                GameScr.info1.addInfo("Buff đậu cho đệ " + (isPeanPet ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 32:
                MyVector myVector1 = new();
                myVector1.addElement(new Command(notifBoss ? "Thông báo BOSS [Bật]" : "Thông báo BOSS [Tắt]", 46));
                myVector1.addElement(new Command(lineToBoss ? "Kẻ đường tới BOSS [Bật]" : "Đường kẻ tới BOSS [Tắt]", 47));
                myVector1.addElement(new Command(focusBoss ? "Focus BOSS [Bật]" : "Focus BOSS [Tắt]", 52));
                GameCanvas.menu.startAt(myVector1, 4);
                break;
            // Goback
            case 38:
                PickMob.mapGoback = TileMap.mapID;
                PickMob.zoneGoback = TileMap.zoneID;
                PickMob.xGoback = Char.myCharz().cx;
                PickMob.yGoback = Char.myCharz().cy;

                PickMob.isGoBack = !PickMob.isGoBack;
                if (PickMob.isGoBack)
                {
                    GameScr.info1.addInfo(string.Concat(new object[]
                    {
                        "Map Goback: ",
                        TileMap.mapName,
                        " | Khu: ",
                        TileMap.zoneID
                    }), 0);
                    GameScr.info1.addInfo(string.Concat(new object[]
                    {
                        "Tọa độ X: ",
                        PickMob.xGoback,
                        " | Y: ",
                        PickMob.yGoback
                    }), 0);
                    if (Char.myCharz().cHP <= 0 || Char.myCharz().statusMe == 14)
                    {
                        Service.gI().returnTownFromDead();
                        new Thread(new ThreadStart(PickMob.GoBack)).Start();
                    }
                }
                GameScr.info1.addInfo("Goback tọa độ " + (PickMob.isGoBack ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 42:
                autoAttack = !autoAttack;
                GameScr.info1.addInfo("Tự đánh " + (autoAttack ? "[Bật]" : "[Tắt]"), 0);
                break;
            // Ne sieu quai
            case 43:
                PickMob.neSieuQuai = !PickMob.neSieuQuai;
                GameScr.info1.addInfo("Né siêu quái " + (PickMob.neSieuQuai ? "[Bật]" : "[Tắt]"), 0);
                break;
            // Tan sat
            case 44:
                PickMob.tsPlayer = false;
                PickMob.tanSat = p != null ? (bool)p : !PickMob.tanSat;
                GameScr.info1.addInfo("Tàn sát " + (PickMob.tanSat ? "[Bật]" : "[Tắt]"), 0);
                break;
            // Menu train
            case 45:
                MyVector myVector = new();
                MyVector mobIds = new MyVector();
                for (int i = 0; i < GameScr.vMob.size(); i++)
                {
                    Mob mob = (Mob)GameScr.vMob.elementAt(i);
                    if (GameScr.gI().isMeCanAttackMob(mob) && !mobIds.contains(mob.templateId) && !PickMob.TypeMobsTanSat.Contains(mob.templateId))
                    {
                        mobIds.addElement(mob.templateId);
                        myVector.addElement(new Command("Tàn sát " + mob.getTemplate().name, 49, mob));
                    }
                }
                myVector.addElement(new Command(PickMob.tanSat ? "Tàn sát [Bật]" : "Tàn sát [Tắt]", 44));
                myVector.addElement(new Command(PickMob.tsPlayer ? "Tàn sát\nngười [Bật]" : "Tàn sát\nngười [Tắt]", 48));
                myVector.addElement(new Command(autoAttack ? "Tự đánh [Bật]" : "Tự đánh [Tắt]", 42));
                myVector.addElement(new Command(PickMob.neSieuQuai ? "Né siêu quái [Bật]" : "Né siêu quái [Tắt]", 43));
                myVector.addElement(new Command(PickMob.vuotDiaHinh ? "Vượt địa hình [Bật]" : "Vượt địa hình [Tắt]", 76));
                myVector.addElement(new Command(PickMob.telePem ? "Dịch chuyển\n[Bật]" : "Dịch chuyển\n[Tắt]", 80));
                myVector.addElement(new Command(PickMob.isGoBack ? "Goback Tọa Độ [Bật]" : "Goback Tọa Độ [Tắt]", 38));
                myVector.addElement(new Command("Xoá danh sách tàn sát", 51));
                GameCanvas.menu.startAt(myVector, 4);
                break;
            case 46:
                notifBoss = !notifBoss;
                GameScr.info1.addInfo("Thông báo BOSS " + (notifBoss ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 47:
                lineToBoss = !lineToBoss;
                GameScr.info1.addInfo("Kẻ đường tới BOSS " + (lineToBoss ? "[Bật]" : "[Tắt]"), 0);
                break;
            // Tan sat nguoi
            case 48:
                PickMob.tanSat = false;
                PickMob.tsPlayer = p != null ? (bool)p : !PickMob.tsPlayer;
                GameScr.info1.addInfo("Tàn sát người " + (PickMob.tsPlayer ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 49:
                Mob mobType = (Mob)p;
                if (!PickMob.TypeMobsTanSat.Contains(mobType.templateId))
                {
                    PickMob.TypeMobsTanSat.Add(mobType.templateId);
                }
                GameScr.info1.addInfo("Tàn sát " + mobType.getTemplate().name, 0);
                perform(44, true);
                break;
            case 51:
                PickMob.TypeMobsTanSat.Clear();
                GameScr.info1.addInfo("Đã xoá danh sách quái tàn sát!", 0);
                break;
            case 52:
                focusBoss = !focusBoss;
                GameScr.info1.addInfo("Focus BOSS " + (focusBoss ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 53:
                MyVector menuOthers = new();
                menuOthers.addElement(new Command("Tốc độ\nGame", 54));
                menuOthers.addElement(new Command("Tự động\nChat " + (isAutoChat ? "[Bật]" : "[Tắt]"), 55));
                menuOthers.addElement(new Command("Tự động\nChat Thế\nGiới " + (isAutoChatTG ? "[Bật]" : "[Tắt]"), 56));
                menuOthers.addElement(new Command("Load ô\nskill", 57));
                menuOthers.addElement(new Command(isPlayingMusic ? "Tắt nhạc" : "Bật nhạc", 60));
                GameCanvas.menu.startAt(menuOthers, 4);
                break;
            case 54:
                MyChatTextField(ChatTextField.gI(), "Nhập tốc độ game", "1 đến 10");
                break;
            case 55:
                isAutoChat = !isAutoChat;
                GameScr.info1.addInfo("Tự động chat " + (isAutoChat ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 56:
                isAutoChatTG = !isAutoChatTG;
                GameScr.info1.addInfo("Tự động chat thế giới " + (isAutoChatTG ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 57:
                LoadSkillToScreen();
                GameScr.info1.addInfo("Đã load ô skill", 0);
                break;
            case 60:
                Sound.PlayMusic(UnityEngine.Random.Range(0, 3));
                Debug.Log("Music " + musics.Count);
                GameScr.info1.addInfo("Đã bật trình phát nhạc", 0);
                break;
            // Vuot dia hinh
            case 76:
                PickMob.vuotDiaHinh = !PickMob.vuotDiaHinh;
                GameScr.info1.addInfo("Vượt địa hình " + (PickMob.vuotDiaHinh ? "[Bật]" : "[Tắt]"), 0);
                break;
            // Dich chuyen danh quai
            case 80:
                PickMob.telePem = !PickMob.telePem;
                GameScr.info1.addInfo("Dịch chuyển đến quái\n" + (PickMob.telePem ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 89:
                MyVector menuAutoPick = new();
                menuAutoPick.addElement(new Command("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 90));
                menuAutoPick.addElement(new Command("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 91));
                menuAutoPick.addElement(new Command("Nhặt xa\n" + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 92));
                GameCanvas.menu.startAt(menuAutoPick, 4);
                break;
            case 90:
                PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
                GameScr.info1.addInfo("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 91:
                PickMob.IsPickItemsAll = !PickMob.IsPickItemsAll;
                GameScr.info1.addInfo("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 92:
                PickMob.IsPickItemsDis = !PickMob.IsPickItemsDis;
                GameScr.info1.addInfo("Nhặt xa " + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 0);
                break;
            case 100:
                string str = (string)p;
                int.TryParse(str.Split("-")[0], out indexAutoPoint);
                bool.TryParse(str.Split("-")[1], out autoPointForPet);
                GameCanvas.panel.hideNow();
                MyChatTextField(ChatTextField.gI(), "Tăng đến mức", "VD: 220000");
                break;
            case 101:
                isOpenAccMAnager = true;
                break;
            case 102:
                Account account = (Account)p;
                Rms.saveRMSString("acc", account.getUsername());
                Rms.saveRMSString("pass", account.getPassword());
                if (GameCanvas.loginScr != null && GameCanvas.currentScreen == GameCanvas.loginScr)
                {
                    GameCanvas.loginScr.setUserPass();
                }
                isOpenAccMAnager = false;
                break;
            case 103:
                int index = accounts.IndexOf((Account)p);
                accounts.RemoveAt(index);
                cmdsChooseAcc.RemoveAt(index);
                cmdsDelAcc.RemoveAt(index);
                SaveAcc();
                break;
            case 104:
                isOpenAccMAnager = false;
                break;
            case 500:
            case 501:
                AddOrRemoveAutoItem((Item)p, idAction == 500);
                break;
            // Menu XMAP
            case 883:
                XmapController.ShowXmapMenu();
                break;
        }
    }

    public void AddNotifTichXanh(string notif)
    {
        listNotifTichXanh.addElement(notif);
        if (!startChat)
        {
            int halfW = GameCanvas.w / 2;
            startChat = true;
            xNotif = halfW + halfW / 2;
            lastUpdateNotif = mSystem.currentTimeMillis();
        }
    }

    private void PaintPlayerTichXanh(mGraphics g)
    {
        if (listNotifTichXanh.size() != 0)
        {
            string st = (string)listNotifTichXanh.elementAt(0);
            int halfW = GameCanvas.w / 2;
            g.setClip(halfW - halfW / 3, 50, halfW / 3 * 2, 12);
            g.fillRect(halfW - halfW / 3, 50, halfW / 3 * 2, 12, 0, 60);
            mFont.tahoma_7_yellow.drawStringBorder(g, st, xNotif, 50, 0, mFont.tahoma_7_grey);
            PaintTicks(g, xNotif - 12, 51);
        }
    }

    private void UpdateNotifTichXanh()
    {
        if (!startChat || mSystem.currentTimeMillis() - lastUpdateNotif < 10)
        {
            return;
        }
        xNotif -= 1;
        string strChat = (string)listNotifTichXanh.elementAt(0);
        lastUpdateNotif = mSystem.currentTimeMillis();
        if (xNotif < GameCanvas.w / 2 - 100 - mFont.tahoma_7_yellow.getWidth(strChat))
        {
            xNotif = GameCanvas.w / 2 + 100;
            listNotifTichXanh.removeElementAt(0);
            if (listNotifTichXanh.size() == 0)
            {
                startChat = false;
            }
        }
    }

    public static void LoadLogoImages()
    {

        for (int i = 0; i < 60; i++)
        {
            logos[i] = GameCanvas.LoadImageFromRoot("/logo/" + i + ".png");
        }

        //logo = GameCanvas.LoadImageFromRoot("/logo/logo.png");
        imgBg = null;
        imgLogoBig = GameCanvas.LoadImageFromRoot("/logo/logo.png");
       // imgLogoBig = GameCanvas.LoadImageFromRoot("/logo/logo2.png");
    }

    public static void LoadTickImages()
    {
        for (int i = 0; i < 20; i++)
        {
            ticks[i] = GameCanvas.loadImage("/tick/tick_" + i);
        }
    }

    public static void PaintTicks(mGraphics g, int x, int y)
    {
        int id = GameCanvas.gameTick / 4 % 20;
        if (ticks[id] != null)
        {
            g.drawImage(ticks[id], x, y);
        }
    }

}
