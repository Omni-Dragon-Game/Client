using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- drag_click_tapTargets ---
    private void checkDrag()
    {
        if (isAnalog == 1 || gamePad.disableCheckDrag())
        {
            return;
        }
        Char.myCharz().cmtoChar = true;
        if (isUseTouch)
        {
            return;
        }
        if (GameCanvas.isPointerJustDown)
        {
            GameCanvas.isPointerJustDown = false;
            isPointerDowning = true;
            ptDownTime = 0;
            ptLastDownX = (ptFirstDownX = GameCanvas.px);
            ptLastDownY = (ptFirstDownY = GameCanvas.py);
        }
        if (isPointerDowning)
        {
            int num = GameCanvas.px - ptLastDownX;
            int num2 = GameCanvas.py - ptLastDownY;
            if (!isChangingCameraMode && (Res.abs(GameCanvas.px - ptFirstDownX) > 15 || Res.abs(GameCanvas.py - ptFirstDownY) > 15))
            {
                isChangingCameraMode = true;
            }
            ptLastDownX = GameCanvas.px;
            ptLastDownY = GameCanvas.py;
            ptDownTime++;
            if (isChangingCameraMode)
            {
                Char.myCharz().cmtoChar = false;
                cmx -= num;
                cmy -= num2;
                if (cmx < 24)
                {
                    int num3 = (24 - cmx) / 3;
                    if (num3 != 0)
                    {
                        cmx += num - num / num3;
                    }
                }
                if (cmx < (isVsMap() ? 24 : 0))
                {
                    cmx = (isVsMap() ? 24 : 0);
                }
                if (cmx > cmxLim)
                {
                    int num4 = (cmx - cmxLim) / 3;
                    if (num4 != 0)
                    {
                        cmx += num - num / num4;
                    }
                }
                if (cmx > cmxLim + ((!isVsMap()) ? 24 : 0))
                {
                    cmx = cmxLim + ((!isVsMap()) ? 24 : 0);
                }
                if (cmy < 0)
                {
                    int num5 = -cmy / 3;
                    if (num5 != 0)
                    {
                        cmy += num2 - num2 / num5;
                    }
                }
                if (cmy < -((!isVsMap()) ? 24 : 0))
                {
                    cmy = -((!isVsMap()) ? 24 : 0);
                }
                if (cmy > cmyLim)
                {
                    cmy = cmyLim;
                }
                cmtoX = cmx;
                cmtoY = cmy;
            }
        }
        if (isPointerDowning && GameCanvas.isPointerJustRelease)
        {
            isPointerDowning = false;
            isChangingCameraMode = false;
            if (Res.abs(GameCanvas.px - ptFirstDownX) > 15 || Res.abs(GameCanvas.py - ptFirstDownY) > 15)
            {
                GameCanvas.isPointerJustRelease = false;
            }
        }
    }

    private void checkClick()
    {
        if (isCharging())
        {
            return;
        }
        if (popUpYesNo != null && popUpYesNo.cmdYes != null && popUpYesNo.cmdYes.isPointerPressInside())
        {
            popUpYesNo.cmdYes.performAction();
        }
        else
        {
            if (checkClickToCapcha())
            {
                return;
            }
            long num = mSystem.currentTimeMillis();
            if (lastSingleClick != 0)
            {
                lastSingleClick = 0L;
                GameCanvas.isPointerJustDown = false;
                if (!disableSingleClick)
                {
                    checkSingleClick();
                    GameCanvas.isPointerJustRelease = false;
                    isWaitingDoubleClick = true;
                    timeStartDblClick = mSystem.currentTimeMillis();
                }
            }
            if (isWaitingDoubleClick)
            {
                timeEndDblClick = mSystem.currentTimeMillis();
                if (timeEndDblClick - timeStartDblClick < 300 && GameCanvas.isPointerJustRelease)
                {
                    isWaitingDoubleClick = false;
                    checkDoubleClick();
                }
            }
            if (GameCanvas.isPointerJustRelease)
            {
                disableSingleClick = checkSingleClickEarly();
                lastSingleClick = num;
                lastClickCMX = cmx;
                lastClickCMY = cmy;
                GameCanvas.isPointerJustRelease = false;
            }
        }
    }

    private IMapObject findClickToItem(int px, int py)
    {
        IMapObject mapObject = null;
        int num = 0;
        int num2 = 30;
        MyVector[] array = new MyVector[4] { vMob, vNpc, vItemMap, vCharInMap };
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].size(); j++)
            {
                IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
                if (mapObject2.isInvisible())
                {
                    continue;
                }
                if (mapObject2 is Mob)
                {
                    Mob mob = (Mob)mapObject2;
                    if (mob.isMobMe && mob.Equals(Char.myCharz().mobMe))
                    {
                        continue;
                    }
                }
                int x = mapObject2.getX();
                int y = mapObject2.getY();
                int w = mapObject2.getW();
                int h = mapObject2.getH();
                if (!inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2))
                {
                    continue;
                }
                if (mapObject == null)
                {
                    mapObject = mapObject2;
                    num = Res.abs(px - x) + Res.abs(py - y);
                    if (i == 1)
                    {
                        return mapObject;
                    }
                }
                else
                {
                    int num3 = Res.abs(px - x) + Res.abs(py - y);
                    if (num3 < num)
                    {
                        mapObject = mapObject2;
                        num = num3;
                    }
                }
            }
        }
        return mapObject;
    }

    private Mob findClickToMOB(int px, int py)
    {
        int num = 30;
        Mob mob = null;
        int num2 = 0;
        for (int i = 0; i < vMob.size(); i++)
        {
            Mob mob2 = (Mob)vMob.elementAt(i);
            if (mob2.isInvisible())
            {
                continue;
            }
            if (mob2 != null)
            {
                Mob mob3 = mob2;
                if (mob3.isMobMe && mob3.Equals(Char.myCharz().mobMe))
                {
                    continue;
                }
            }
            int x = mob2.getX();
            int y = mob2.getY();
            int w = mob2.getW();
            int h = mob2.getH();
            if (!inRectangle(px, py, x - w / 2 - num, y - h - num, w + num * 2, h + num * 2))
            {
                continue;
            }
            if (mob == null)
            {
                mob = mob2;
                num2 = Res.abs(px - x) + Res.abs(py - y);
                continue;
            }
            int num3 = Res.abs(px - x) + Res.abs(py - y);
            if (num3 < num2)
            {
                mob = mob2;
                num2 = num3;
            }
        }
        return mob;
    }

    private bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
    {
        return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
    }

    private bool checkSingleClickEarly()
    {
        int num = GameCanvas.px + cmx;
        int num2 = GameCanvas.py + cmy;
        Char.myCharz().cancelAttack();
        IMapObject mapObject = findClickToItem(num, num2);
        if (mapObject != null)
        {
            if (Char.myCharz().isAttacPlayerStatus() && Char.myCharz().charFocus != null && !mapObject.Equals(Char.myCharz().charFocus) && !mapObject.Equals(Char.myCharz().charFocus.mobMe) && mapObject is Char)
            {
                Char @char = (Char)mapObject;
                if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
                {
                    checkClickMoveTo(num, num2, 2);
                    return false;
                }
            }
            if (Char.myCharz().mobFocus == mapObject || Char.myCharz().itemFocus == mapObject)
            {
                doDoubleClickToObj(mapObject);
                return true;
            }
            if (TileMap.mapID == 51 && mapObject.Equals(Char.myCharz().npcFocus))
            {
                checkClickMoveTo(num, num2, 3);
                return false;
            }
            if (Char.myCharz().skillPaint != null || Char.myCharz().arr != null || Char.myCharz().dart != null || Char.myCharz().skillInfoPaint() != null)
            {
                return false;
            }
            Char.myCharz().FocusManualTo(mapObject);
            mapObject.stopMoving();
            return false;
        }
        return false;
    }

    private void checkDoubleClick()
    {
        int num = GameCanvas.px + lastClickCMX;
        int num2 = GameCanvas.py + lastClickCMY;
        int cy = Char.myCharz().cy;
        if (isLockKey)
        {
            return;
        }
        IMapObject mapObject = findClickToItem(num, num2);
        if (mapObject != null)
        {
            if (mapObject is Mob && !isMeCanAttackMob((Mob)mapObject))
            {
                checkClickMoveTo(num, num2, 4);
            }
            else
            {
                if (checkClickToBotton(mapObject) || (!mapObject.Equals(Char.myCharz().npcFocus) && mobCapcha != null))
                {
                    return;
                }
                if (Char.myCharz().isAttacPlayerStatus() && Char.myCharz().charFocus != null && !mapObject.Equals(Char.myCharz().charFocus) && !mapObject.Equals(Char.myCharz().charFocus.mobMe) && mapObject is Char)
                {
                    Char @char = (Char)mapObject;
                    if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
                    {
                        checkClickMoveTo(num, num2, 5);
                        return;
                    }
                }
                if (TileMap.mapID == 51 && mapObject.Equals(Char.myCharz().npcFocus))
                {
                    checkClickMoveTo(num, num2, 6);
                }
                else
                {
                    doDoubleClickToObj(mapObject);
                }
            }
        }
        else if (!checkClickToPopup(num, num2) && !checkClipTopChatPopUp(num, num2) && !Main.isPC)
        {
            checkClickMoveTo(num, num2, 7);
        }
    }

    public bool checkClickToBotton(IMapObject Object)
    {
        if (Object == null)
        {
            return false;
        }
        int y = Object.getY();
        int num = Char.myCharz().cy;
        if (y < num)
        {
            while (y < num)
            {
                num -= 5;
                if (TileMap.tileTypeAt(Char.myCharz().cx, num, 8192))
                {
                    auto = 0;
                    Char.myCharz().cancelAttack();
                    Char.myCharz().currentMovePoint = null;
                    return true;
                }
            }
        }
        return false;
    }

    private void doDoubleClickToObj(IMapObject obj)
    {
        if (obj.Equals(Char.myCharz().mobFocus) && PickMob.tanSat && PickMob.TypeMobsTanSat.Count != 0 && !PickMob.TypeMobsTanSat.Contains(((Mob)obj).templateId))
        {
            info1.addInfo("Quái đang đánh không có trong danh sách tàn sát", 0);
        }
        if ((obj.Equals(Char.myCharz().npcFocus) || mobCapcha == null) && !checkClickToBotton(obj))
        {
            checkEffToObj(obj, isnew: false);
            Char.myCharz().cancelAttack();
            Char.myCharz().currentMovePoint = null;
            Char.myCharz().cvx = (Char.myCharz().cvy = 0);
            obj.stopMoving();
            auto = 10;
            doFire(isFireByShortCut: false, skipWaypoint: true);
            clickToX = obj.getX();
            clickToY = obj.getY();
            clickOnTileTop = false;
            clickMoving = true;
            clickMovingRed = true;
            clickMovingTimeOut = 20;
            clickMovingP1 = 30;
        }
    }

    private void checkSingleClick()
    {
        int xClick = GameCanvas.px + lastClickCMX;
        int yClick = GameCanvas.py + lastClickCMY;
        if (!isLockKey && !checkClickToPopup(xClick, yClick) && !checkClipTopChatPopUp(xClick, yClick))
        {
            checkClickMoveTo(xClick, yClick, 0);
        }
    }

    private bool checkClipTopChatPopUp(int xClick, int yClick)
    {
        if (Equals(info2) && gI().popUpYesNo != null)
        {
            return false;
        }
        if (info2.info.info != null && info2.info.info.charInfo != null)
        {
            int num = 0;
            int num2 = 0;
            num = Res.abs(info2.cmx) + info2.info.X - 40;
            num2 = Res.abs(info2.cmy) + info2.info.Y;
            if (inRectangle(xClick - cmx, yClick - cmy, num, num2, 200, info2.info.H))
            {
                info2.doClick(10);
                return true;
            }
        }
        return false;
    }

    private bool checkClickToPopup(int xClick, int yClick)
    {
        for (int i = 0; i < PopUp.vPopups.size(); i++)
        {
            PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
            if (inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch))
            {
                if (popUp.cy <= 24 && TileMap.isInAirMap() && Char.myCharz().cTypePk != 0)
                {
                    return false;
                }
                if (popUp.isPaint)
                {
                    popUp.doClick(10);
                    return true;
                }
            }
        }
        return false;
    }

    private void checkClickMoveTo(int xClick, int yClick, int index)
    {
        if (gamePad.disableClickMove() || ChatTextField.gI().isShow)
        {
            return;
        }
        Char.myCharz().cancelAttack();
        if (xClick < TileMap.pxw && xClick > TileMap.pxw - 32)
        {
            Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
            return;
        }
        if (xClick < 32 && xClick > 0)
        {
            Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
            return;
        }
        if (xClick < TileMap.pxw && xClick > TileMap.pxw - 48)
        {
            Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
            return;
        }
        if (xClick < 48 && xClick > 0)
        {
            Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
            return;
        }
        clickToX = xClick;
        clickToY = yClick;
        clickOnTileTop = false;
        Char.myCharz().delayFall = 0;
        int num = ((!Char.myCharz().canFly || Char.myCharz().cMP <= 0) ? 1000 : 0);
        if (clickToY > Char.myCharz().cy && Res.abs(clickToX - Char.myCharz().cx) < 12)
        {
            return;
        }
        for (int i = 0; i < 60 + num && clickToY + i < TileMap.pxh - 24; i += 24)
        {
            if (TileMap.tileTypeAt(clickToX, clickToY + i, 2))
            {
                clickToY = TileMap.tileYofPixel(clickToY + i);
                clickOnTileTop = true;
                break;
            }
        }
        for (int j = 0; j < 40 + num; j += 24)
        {
            if (TileMap.tileTypeAt(clickToX, clickToY - j, 2))
            {
                clickToY = TileMap.tileYofPixel(clickToY - j);
                clickOnTileTop = true;
                break;
            }
        }
        clickMoving = true;
        clickMovingRed = false;
        clickMovingP1 = ((!clickOnTileTop) ? 30 : ((yClick >= clickToY) ? clickToY : yClick));
        Char.myCharz().delayFall = 0;
        if (!clickOnTileTop && clickToY < Char.myCharz().cy - 50)
        {
            Char.myCharz().delayFall = 20;
        }
        clickMovingTimeOut = 30;
        auto = 0;
        if (Char.myCharz().holder)
        {
            Char.myCharz().removeHoleEff();
        }
        Char.myCharz().currentMovePoint = new MovePoint(clickToX, clickToY);
        Char.myCharz().cdir = ((Char.myCharz().cx - Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : (-1));
        Char.myCharz().endMovePointCommand = null;
        isAutoPlay = false;
    }

    // --- touchControl_capcha_mouseChat ---
    public bool checkClickToCapcha()
    {
        if (mobCapcha == null)
        {
            return false;
        }
        int x = (GameCanvas.w - 5 * disXC) / 2;
        int w = 5 * disXC;
        int y = GameCanvas.h - 40;
        int h = disXC;
        if (GameCanvas.isPointerHoldIn(x, y, w, h))
        {
            return true;
        }
        return false;
    }

    public void checkMouseChat()
    {
        if (GameCanvas.isMouseFocus(xC, yC, 34, 34))
        {
            if (!TileMap.isOfflineMap())
            {
                keyMouse = 15;
            }
        }
        else if (GameCanvas.isMouseFocus(xHP, yHP, 40, 40))
        {
            if (Char.myCharz().statusMe != 14)
            {
                keyMouse = 10;
            }
        }
        else if (GameCanvas.isMouseFocus(xF, yF, 40, 40))
        {
            if (Char.myCharz().statusMe != 14)
            {
                keyMouse = 5;
            }
        }
        else if (cmdMenu != null && GameCanvas.isMouseFocus(cmdMenu.x, cmdMenu.y, cmdMenu.w / 2, cmdMenu.h))
        {
            keyMouse = 1;
        }
        else
        {
            keyMouse = -1;
        }
    }

    private void UpdateKeyTouchControl()
    {
        if (isNotPaintTouchControl())
        {
            return;
        }
        keyTouch = -1;
        if (ModFunc.GI().showCharsInMap)
        {
            int numY = ModFunc.notifBoss ? 92 : 50;
            MyVector chars = ModFunc.GI().charsInMap;
            for (int i = 0; i < chars.size(); i++)
            {
                Char @char = (Char)chars.elementAt(i);
                if (@char != null)
                {
                    if (GameCanvas.isPointerHoldIn(GameCanvas.w - 130, numY, 130, 10) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        if (Char.myCharz().charFocus == @char)
                        {
                            ModFunc.GI().MoveTo(@char.cx, @char.cy);
                        }
                        else
                        {
                            Char.myCharz().FocusManualTo(@char);
                            ModFunc.isLockFocus = true;
                        }
                        Char.myCharz().currentMovePoint = null;
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }
                    numY += 10;
                }
            }
        }
        if (ModFunc.notifBoss)
        {
            int numX = 38;
            for (int i = 0; i < ModFunc.bossNotif.size(); i++)
            {
                if (GameCanvas.isPointerHoldIn(GameCanvas.w - 20, numX + 3, 20, 10) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                {
                    ShowBoss boss = (ShowBoss) ModFunc.bossNotif.elementAt(i);
                    ModFunc.GI().GoToBoss(boss.mapID);
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
                numX += 10;
            }
        }
        if (GameCanvas.isTouchControl)
        {
            if (QuayTamBao.isTamBao)
            {

                QuayTamBao.doTamBao();
                return;
            }
            if (GameCanvas.isPointerHoldIn(GameCanvas.w - 100, 0, 45, 45) && GameCanvas.isPointerJustRelease)
            {
                QuayTamBao.isTamBao = true;
                QuayTamBao.sendDataTamBao();
                return;
            }
            if (GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                Char.myCharz().cmdMenu?.performAction();
                Char.myCharz().currentMovePoint = null;
                GameCanvas.clearAllPointerEvent();
                flareFindFocus = true;
                flareTime = 5;
                return;
            }
            if (Main.isPC)
            {
                checkMouseChat();
            }
            if (!TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(xC, yC, 34, 34))
            {
                keyTouch = 15;
                GameCanvas.isPointerJustDown = false;
                isPointerDowning = false;
                if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                {
                    ChatTextField.gI().startChat(this, string.Empty);
                    SoundMn.gI().buttonClick();
                    Char.myCharz().currentMovePoint = null;
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }
            if (Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(Char.myCharz().cmdMenu.x - 17, Char.myCharz().cmdMenu.y - 17, 34, 34))
            {
                keyTouch = 20;
                GameCanvas.isPointerJustDown = false;
                isPointerDowning = false;
                if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.clearAllPointerEvent();
                    Char.myCharz().cmdMenu.performAction();
                    return;
                }
            }
            updateGamePad();
            if (((isAnalog != 0) ? GameCanvas.isPointerHoldIn(xHP, yHP, 34, 34) : GameCanvas.isPointerHoldIn(xHP, yHP, 40, 40)) && Char.myCharz().statusMe != 14 && mobCapcha == null)
            {
                keyTouch = 10;
                GameCanvas.isPointerJustDown = false;
                isPointerDowning = false;
                if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.keyPressed[10] = true;
                    GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
                }
            }
        }
        if (mobCapcha != null)
        {
            updateKeyTouchCapcha();
        }
        else if (isHaveSelectSkill)
        {
            if (isCharging())
            {
                return;
            }
            keyTouchSkill = -1;
            bool flag = false;
            if (onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(xSkill + xS[0] - wSkill / 2 + 12, yS[0] - wSkill / 2 + 12, 5 * wSkill, wSkill) || GameCanvas.isPointerHoldIn(xSkill + xS[5] - wSkill / 2 + 12, yS[5] - wSkill / 2 + 12, 5 * wSkill, wSkill)))
            {
                flag = true;
            }
            if (flag || GameCanvas.isPointerHoldIn(xSkill + xS[0] - wSkill / 2 + 12, yS[0] - wSkill / 2 + 12, 5 * wSkill, wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(xSkill + xS[0] - wSkill / 2 + 12, yS[0] - wSkill / 2 + 12, wSkill, onScreenSkill.Length * wSkill)))
            {
                GameCanvas.isPointerJustDown = false;
                isPointerDowning = false;
                int num = (GameCanvas.pxLast - (xSkill + xS[0] - wSkill / 2 + 12)) / wSkill;
                if (flag && GameCanvas.pyLast < yS[0])
                {
                    num += 5;
                }
                keyTouchSkill = num;
                if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
                    selectedIndexSkill = num;
                    if (indexSelect < 0)
                    {
                        indexSelect = 0;
                    }
                    if (!Main.isPC)
                    {
                        if (selectedIndexSkill > onScreenSkill.Length - 1)
                        {
                            selectedIndexSkill = onScreenSkill.Length - 1;
                        }
                    }
                    else if (selectedIndexSkill > keySkill.Length - 1)
                    {
                        selectedIndexSkill = keySkill.Length - 1;
                    }
                    Skill skill = null;
                    skill = (Main.isPC ? keySkill[selectedIndexSkill] : onScreenSkill[selectedIndexSkill]);
                    if (skill != null)
                    {
                        doSelectSkill(skill, isShortcut: true);
                    }
                }
            }
        }
        if (GameCanvas.isPointerJustRelease)
        {
            if (GameCanvas.keyHold[1] || GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3] || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
            {
                GameCanvas.isPointerJustRelease = false;
            }
            GameCanvas.keyHold[1] = false;
            GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
            GameCanvas.keyHold[3] = false;
            GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
            GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
        }
    }

    // --- gamepad_touchBtn ---
    private static void setTouchBtn()
    {
        if (isAnalog != 0)
        {
            xTG = (xF = GameCanvas.w - 45);
            if (gamePad.isLargeGamePad)
            {
                xSkill = gamePad.wZone + 20;
                wSkill = 35;
                xHP = xF - 45;
            }
            else if (gamePad.isMediumGamePad)
            {
                xHP = xF - 45;
            }
            yF = GameCanvas.h - 45;
            yTG = yF - 45;
        }
    }

    private void updateGamePad()
    {
        if (isAnalog == 0 || Char.myCharz().statusMe == 14)
        {
            return;
        }
        if (GameCanvas.isPointerHoldIn(xF, yF, 40, 40))
        {
            keyTouch = 5;
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
                GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
            }
        }
        gamePad.update();
        if (GameCanvas.isPointerHoldIn(xTG, yTG, 34, 34))
        {
            keyTouch = 13;
            GameCanvas.isPointerJustDown = false;
            isPointerDowning = false;
            if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
            {
                Char.myCharz().findNextFocusByKey();
                GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
            }
        }
    }
}
