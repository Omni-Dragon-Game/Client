using System;

public partial class Char
{
    public void updateCharStand()
    {
        isSoundJump = false;
        isAttack = false;
        isAttFly = false;
        cvx = 0;
        cvy = 0;
        cp1++;
        if (cp1 > 30)
        {
            cp1 = 0;
        }
        if (cp1 % 15 < 5)
        {
            cf = 0;
        }
        else
        {
            cf = 1;
        }
        updateCharInBridge();
        if (!me)
        {
            cp3++;
            if (cp3 > 50)
            {
                cp3 = 0;
                currentMovePoint = null;
            }
        }
        updateSuperEff();
        if (!me || GameScr.vCharInMap.size() == 0 || TileMap.mapID != 50)
        {
            return;
        }
        Char @char = (Char)GameScr.vCharInMap.elementAt(0);
        if (!@char.changePos)
        {
            if (@char.statusMe != 2)
            {
                @char.moveTo(cx - 45, cy, 0);
            }
            @char.lastUpdateTime = mSystem.currentTimeMillis();
            if (Res.abs(cx - 45 - @char.cx) <= 10)
            {
                @char.changePos = true;
            }
        }
        else
        {
            if (@char.statusMe != 2)
            {
                @char.moveTo(cx + 45, cy, 0);
            }
            @char.lastUpdateTime = mSystem.currentTimeMillis();
            if (Res.abs(cx + 45 - @char.cx) <= 10)
            {
                @char.changePos = false;
            }
        }
        if (GameCanvas.gameTick % 100 == 0)
        {
            @char.addInfo("Cắc cùm cum");
        }
    }

    public void updateCharRun()
    {
        int num = ((isMonkey != 1 || me) ? 1 : 2);
        if (cx >= GameScr.cmx && cx <= GameScr.cmx + GameCanvas.w)
        {
            if (isMonkey == 0)
            {
                SoundMn.gI().charRun(getSoundVolumn());
            }
            else
            {
                SoundMn.gI().monkeyRun(getSoundVolumn());
            }
        }
        ty = 0;
        isFreez = false;
        if (isCharge)
        {
            isCharge = false;
            SoundMn.gI().taitaoPause();
            Service.gI().skill_not_focus(3);
        }
        int num2 = 0;
        if (!me && currentMovePoint != null)
        {
            num2 = abs(cx - currentMovePoint.xEnd);
        }
        cp1++;
        if (cp1 >= 10)
        {
            cp1 = 0;
            cBonusSpeed = 0;
        }
        cf = (cp1 >> 1) + 2;
        if ((TileMap.tileTypeAtPixel(cx, cy - 1) & 0x40) == 64)
        {
            cx += cvx * num >> 1;
        }
        else
        {
            cx += cvx * num;
        }
        if (cdir == 1)
        {
            if (TileMap.tileTypeAt(cx + chw, cy - chh, 4))
            {
                if (me)
                {
                    cvx = 0;
                    cx = TileMap.tileXofPixel(cx + chw) - chw;
                }
                else
                {
                    stop();
                }
            }
        }
        else if (TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8))
        {
            if (me)
            {
                cvx = 0;
                cx = TileMap.tileXofPixel(cx - chw - 1) + TileMap.size + chw;
            }
            else
            {
                stop();
            }
        }
        if (me)
        {
            if (cvx > 0)
            {
                cvx--;
            }
            else if (cvx < 0)
            {
                cvx++;
            }
            else
            {
                if (cx - cxSend != 0 && me)
                {
                    Service.gI().charMove();
                }
                statusMe = 1;
                cBonusSpeed = 0;
            }
        }
        if ((TileMap.tileTypeAtPixel(cx, cy) & 2) != 2)
        {
            if (me)
            {
                if (cx - cxSend != 0 || cy - cySend != 0)
                {
                    Service.gI().charMove();
                }
                cf = 7;
                statusMe = 4;
                delayFall = 0;
                cvx = 3 * cdir;
                cp2 = 0;
            }
            else
            {
                stop();
            }
        }
        if (!me && currentMovePoint != null)
        {
            int num3 = abs(cx - currentMovePoint.xEnd);
            if (num3 > num2)
            {
                stop();
            }
        }
        GameCanvas.gI().startDust(cdir, cx - (cdir << 3), cy);
        updateCharInBridge();
        addDustEff(2);
    }

    private void stop()
    {
        statusMe = 6;
        cp3 = 0;
        cvx = 0;
        cvy = 0;
        cp1 = (cp2 = 0);
    }

    public static int abs(int i)
    {
        return (i <= 0) ? (-i) : i;
    }

    public void updateCharJump()
    {
        setMountIsStart();
        ty = 0;
        isFreez = false;
        if (isCharge)
        {
            isCharge = false;
            SoundMn.gI().taitaoPause();
            Service.gI().skill_not_focus(3);
        }
        addDustEff(3);
        cx += cvx;
        cy += cvy;
        if (cy < 0)
        {
            cy = 0;
            cvy = -1;
        }
        cvy++;
        if (cvy > 0)
        {
            cvy = 0;
        }
        if (!me && currentMovePoint != null)
        {
            int num = currentMovePoint.xEnd - cx;
            if (num > 0)
            {
                if (cvx > num)
                {
                    cvx = num;
                }
                if (cvx < 0)
                {
                    cvx = num;
                }
            }
            else if (num < 0)
            {
                if (cvx < num)
                {
                    cvx = num;
                }
                if (cvx > 0)
                {
                    cvx = num;
                }
            }
            else
            {
                cvx = num;
            }
        }
        if (cdir == 1)
        {
            if ((TileMap.tileTypeAtPixel(cx + chw, cy - 1) & 4) == 4 && cx <= TileMap.tileXofPixel(cx + chw) + 12)
            {
                cx = TileMap.tileXofPixel(cx + chw) - chw;
                cvx = 0;
            }
        }
        else if ((TileMap.tileTypeAtPixel(cx - chw, cy - 1) & 8) == 8 && cx >= TileMap.tileXofPixel(cx - chw) + 12)
        {
            cx = TileMap.tileXofPixel(cx + 24 - chw) + chw;
            cvx = 0;
        }
        if (cvy == 0)
        {
            if (!isAttFly)
            {
                if (me)
                {
                    setCharFallFromJump();
                }
                else
                {
                    stop();
                }
            }
            else
            {
                setCharFallFromJump();
            }
        }
        if (me && !ischangingMap && isInWaypoint())
        {
            Service.gI().charMove();
            if (TileMap.isTrainingMap())
            {
                ischangingMap = true;
                Service.gI().getMapOffline();
            }
            else
            {
                Service.gI().requestChangeMap();
            }
            isLockKey = true;
            ischangingMap = true;
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
            InfoDlg.showWait();
            return;
        }
        if (statusMe != 16 && (TileMap.tileTypeAt(cx, cy - ch + 24, 8192) || cy < 0))
        {
            statusMe = 4;
            cp1 = 0;
            cp2 = 0;
            cvy = 1;
            delayFall = 0;
            if (cy < 0)
            {
                cy = 0;
            }
            cy = TileMap.tileYofPixel(cy + 25);
            GameCanvas.clearKeyHold();
        }
        if (cp3 < 0)
        {
            cp3++;
        }
        cf = 7;
        if (!me && currentMovePoint != null && cy < currentMovePoint.yEnd)
        {
            stop();
        }
    }

    public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
    {
        if (xmob > xw1 || xmob < x1 || ymob > y1 || ymob < yh1)
        {
            return false;
        }
        return true;
    }

    public void setCharFallFromJump()
    {
        cyStartFall = cy;
        cp1 = 0;
        cp2 = 0;
        statusMe = 10;
        cvx = cdir << 2;
        cvy = 0;
        cy = TileMap.tileYofPixel(cy) + 12;
        if (me && (cx - cxSend != 0 || cy - cySend != 0) && (Res.abs(myCharz().cx - myCharz().cxSend) > 96 || Res.abs(myCharz().cy - myCharz().cySend) > 24))
        {
            Service.gI().charMove();
        }
    }

    public void updateCharFall()
    {
        if (holder)
        {
            return;
        }
        ty = 0;
        if (cy + 4 >= TileMap.pxh)
        {
            statusMe = 1;
            if (me)
            {
                SoundMn.gI().charFall();
            }
            cvx = (cvy = 0);
            cp3 = 0;
            return;
        }
        if (cy % 24 == 0 && (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
        {
            delayFall = 0;
            if (me)
            {
                if (cy - cySend > 0)
                {
                    Service.gI().charMove();
                }
                else if (cx - cxSend != 0 || cy - cySend < 0)
                {
                    Service.gI().charMove();
                }
                cvx = (cvy = 0);
                cp1 = (cp2 = 0);
                statusMe = 1;
                cp3 = 0;
                return;
            }
            stop();
            cf = 0;
            GameCanvas.gI().startDust(-1, cx - -8, cy);
            GameCanvas.gI().startDust(1, cx - 8, cy);
            addDustEff(1);
        }
        if (delayFall > 0)
        {
            delayFall--;
            if (delayFall % 10 > 5)
            {
                cy++;
            }
            else
            {
                cy--;
            }
            return;
        }
        if (cvy < -4)
        {
            cf = 7;
        }
        else
        {
            cf = 12;
        }
        cx += cvx;
        if (!me && currentMovePoint != null)
        {
            int num = currentMovePoint.xEnd - cx;
            if (num > 0)
            {
                if (cvx > num)
                {
                    cvx = num;
                }
                if (cvx < 0)
                {
                    cvx = num;
                }
            }
            else if (num < 0)
            {
                if (cvx < num)
                {
                    cvx = num;
                }
                if (cvx > 0)
                {
                    cvx = num;
                }
            }
            else
            {
                cvx = num;
            }
        }
        cvy++;
        if (cvy > 8)
        {
            cvy = 8;
        }
        if (skillPaintRandomPaint == null)
        {
            cy += cvy;
        }
        if (cdir == 1)
        {
            if ((TileMap.tileTypeAtPixel(cx + chw, cy - 1) & 4) == 4 && cx <= TileMap.tileXofPixel(cx + chw) + 12)
            {
                cx = TileMap.tileXofPixel(cx + chw) - chw;
                cvx = 0;
            }
        }
        else if ((TileMap.tileTypeAtPixel(cx - chw, cy - 1) & 8) == 8 && cx >= TileMap.tileXofPixel(cx - chw) + 12)
        {
            cx = TileMap.tileXofPixel(cx + 24 - chw) + chw;
            cvx = 0;
        }
        if (cvy > 3 && (cyStartFall == 0 || cyStartFall <= TileMap.tileYofPixel(cy + 3)) && (TileMap.tileTypeAtPixel(cx, cy + 3) & 2) == 2)
        {
            if (me)
            {
                cyStartFall = 0;
                cvx = (cvy = 0);
                cp1 = (cp2 = 0);
                cy = TileMap.tileXofPixel(cy + 3);
                statusMe = 1;
                if (me)
                {
                    SoundMn.gI().charFall();
                }
                cp3 = 0;
                GameCanvas.gI().startDust(-1, cx - -8, cy);
                GameCanvas.gI().startDust(1, cx - 8, cy);
                addDustEff(1);
                if (cy - cySend > 0)
                {
                    if (me)
                    {
                        Service.gI().charMove();
                    }
                }
                else if ((cx - cxSend != 0 || cy - cySend < 0) && me)
                {
                    Service.gI().charMove();
                }
            }
            else
            {
                stop();
                cy = TileMap.tileXofPixel(cy + 3);
                cf = 0;
                GameCanvas.gI().startDust(-1, cx - -8, cy);
                GameCanvas.gI().startDust(1, cx - 8, cy);
                addDustEff(1);
            }
            return;
        }
        cf = 12;
        if (me)
        {
            if (!isAttack)
            {
            }
            return;
        }
        if ((TileMap.tileTypeAtPixel(cx, cy + 1) & 2) == 2)
        {
            cf = 0;
        }
        if (currentMovePoint != null && cy > currentMovePoint.yEnd)
        {
            stop();
        }
    }

    public void updateCharFly()
    {
        int num = ((isMonkey != 1 || me) ? 1 : 2);
        setMountIsStart();
        if (statusMe != 16 && (TileMap.tileTypeAt(cx, cy - ch + 24, 8192) || cy < 0))
        {
            if (cy - ch < 0)
            {
                cy = ch;
            }
            cf = 7;
            statusMe = 4;
            cvx = 0;
            cp2 = 0;
            currentMovePoint = null;
            return;
        }
        int num2 = cy;
        cp1++;
        if (cp1 >= 9)
        {
            cp1 = 0;
            if (!me)
            {
                cvx = (cvy = 0);
            }
            cBonusSpeed = 0;
        }
        cf = 8;
        if (Res.abs(cvx) <= 4 && me)
        {
            if (currentMovePoint != null)
            {
                int num3 = abs(cx - currentMovePoint.xEnd);
                int num4 = abs(cy - currentMovePoint.yEnd);
                if (num3 > num4 * 10)
                {
                    cf = 8;
                }
                else if (num3 > num4 && num3 > 48 && num4 > 32)
                {
                    cf = 8;
                }
                else
                {
                    cf = 7;
                }
            }
            else
            {
                if (cvy < 0)
                {
                    cvy = 0;
                }
                if (cvy > 16)
                {
                    cvy = 16;
                }
                cf = 7;
            }
        }
        if (!me)
        {
            if (abs(cvx) < 2)
            {
                cvx = (cdir << 1) * num;
            }
            if (cvy != 0)
            {
                cf = 7;
            }
            if (abs(cvx) <= 2)
            {
                cp2++;
                if (cp2 > 32)
                {
                    statusMe = 4;
                    cvx = 0;
                    cvy = 0;
                }
            }
        }
        if (cdir == 1)
        {
            if (TileMap.tileTypeAt(cx + chw, cy - 1, 4))
            {
                cvx = 0;
                cx = TileMap.tileXofPixel(cx + chw) - chw;
                if (cvy == 0)
                {
                    currentMovePoint = null;
                }
            }
        }
        else if (TileMap.tileTypeAt(cx - chw - 1, cy - 1, 8))
        {
            cvx = 0;
            cx = TileMap.tileXofPixel(cx - chw - 1) + TileMap.size + chw;
            if (cvy == 0)
            {
                currentMovePoint = null;
            }
        }
        cx += cvx * num;
        cy += cvy * num;
        if (!isMount && num2 - cy == 0)
        {
            ty++;
            wt++;
            fy += ((!wy) ? 1 : (-1));
            if (wt == 10)
            {
                wt = 0;
                wy = !wy;
            }
            if (ty > 20)
            {
                delayFall = 10;
                if (GameCanvas.gameTick % 3 == 0)
                {
                    ServerEffect.addServerEffect(111, cx + ((cdir != 1) ? 27 : (-17)), cy + fy + 13, 1, (cdir != 1) ? 2 : 0);
                }
            }
        }
        if (!me)
        {
            return;
        }
        if (cvx > 0)
        {
            cvx--;
        }
        else if (cvx < 0)
        {
            cvx++;
        }
        else if (cvy == 0)
        {
            statusMe = 4;
            checkDelayFallIfTooHigh();
            Service.gI().charMove();
        }
        if ((TileMap.tileTypeAtPixel(cx, cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(cx, cy + 40) & 2) == 2)
        {
            if (cvy == 0)
            {
                delayFall = 0;
            }
            cyStartFall = 0;
            cvx = (cvy = 0);
            cp1 = (cp2 = 0);
            statusMe = 4;
            addDustEff(3);
        }
        if (abs(cx - cxSend) > 96 || abs(cy - cySend) > 24)
        {
            Service.gI().charMove();
        }
    }

    // setMount() extracted to Char.Mount.cs

    // updateMount() extracted to Char.Mount.cs

    // getMountData() extracted to Char.Mount.cs

    // checkFrameTick() extracted to Char.Mount.cs

    // paintMount1() extracted to Char.Mount.cs

    // paintMount2() extracted to Char.Mount.cs

    // setMountIsStart() extracted to Char.Mount.cs

    // setMountIsEnd() extracted to Char.Mount.cs

    // checkHaveMount() extracted to Char.Mount.cs

    private void checkDelayFallIfTooHigh()
    {
        bool flag = true;
        for (int i = 0; i < 150; i += 24)
        {
            if ((TileMap.tileTypeAtPixel(cx, cy + i) & 2) == 2 || cy + i > TileMap.tmh * TileMap.size - 24)
            {
                flag = false;
                break;
            }
        }
        if (flag)
        {
            delayFall = 40;
        }
    }
}
