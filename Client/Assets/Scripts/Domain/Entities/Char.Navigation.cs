using System;

public partial class Char
{
    // --- isInWaypoint ---
    public bool isInWaypoint()
    {
        if (TileMap.isInAirMap() && cy >= TileMap.pxh - 48)
        {
            return true;
        }
        if (isTeleport || isUsePlane)
        {
            return false;
        }
        int num = TileMap.vGo.size();
        for (sbyte b = 0; b < num; b++)
        {
            Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(b);
            if ((TileMap.mapID == 47 || TileMap.isInAirMap()) && cy <= waypoint.minY + waypoint.maxY && cx > waypoint.minX && cx < waypoint.maxX)
            {
                if (TileMap.isInAirMap() && cTypePk != 0)
                {
                    return false;
                }
                return true;
            }
            if (cx >= waypoint.minX && cx <= waypoint.maxX && cy >= waypoint.minY && cy <= waypoint.maxY && !waypoint.isEnter)
            {
                return true;
            }
        }
        return false;
    }

    // --- checkPerformEndMovePointAction ---
    private void checkPerformEndMovePointAction()
    {
        if (endMovePointCommand != null)
        {
            Command command = endMovePointCommand;
            endMovePointCommand = null;
            command.performAction();
        }
    }

    // --- resetPoints_autoJump ---
    public void setResetPoint(int x, int y)
    {
        InfoDlg.hide();
        currentMovePoint = null;
        int num = cx - x;
        if (cy - y == 0)
        {
            cx = x;
            ischangingMap = false;
            isLockKey = false;
            return;
        }
        statusMe = 16;
        cp2 = x;
        cp3 = y;
        cp1 = 0;
        myCharz().cxSend = x;
        myCharz().cySend = y;
    }

    private void updateCharDeadFly()
    {
        isFreez = false;
        if (isCharge)
        {
            isCharge = false;
            SoundMn.gI().taitaoPause();
            Service.gI().skill_not_focus(3);
        }
        cp1++;
        cx += (cp2 - cx) / 4;
        if (cp1 > 7)
        {
            cy += (cp3 - cy) / 4;
        }
        else
        {
            cy += cp1 - 10;
        }
        if (Res.abs(cp2 - cx) < 4 && Res.abs(cp3 - cy) < 10)
        {
            cx = cp2;
            cy = cp3;
            statusMe = 14;
            if (me)
            {
                GameScr.gI().resetButton();
                Service.gI().charMove();
            }
        }
        cf = 23;
    }

    private void updateResetPoint()
    {
        InfoDlg.hide();
        GameCanvas.clearAllPointerEvent();
        currentMovePoint = null;
        cp1++;
        cx += (cp2 - cx) / 4;
        if (cp1 > 7)
        {
            cy += (cp3 - cy) / 4;
        }
        else
        {
            cy += cp1 - 10;
        }
        if (Res.abs(cp2 - cx) < 4 && Res.abs(cp3 - cy) < 10)
        {
            cx = cp2;
            cy = cp3;
            statusMe = 1;
            cp3 = 0;
            ischangingMap = false;
            Service.gI().charMove();
        }
        cf = 23;
    }

    public void updateSkillFall()
    {
    }

    public void updateSkillStand()
    {
        ty = 0;
        cp1++;
        if (cdir == 1)
        {
            if ((TileMap.tileTypeAtPixel(cx + chw, cy - chh) & 4) == 4)
            {
                cvx = 0;
            }
        }
        else if ((TileMap.tileTypeAtPixel(cx - chw, cy - chh) & 8) == 8)
        {
            cvx = 0;
        }
        if (cy > ch && TileMap.tileTypeAt(cx, cy - ch + 24, 8192))
        {
            if (!TileMap.tileTypeAt(cx, cy, 2))
            {
                statusMe = 4;
                cp1 = 0;
                cp2 = 0;
                cvy = 1;
            }
            else
            {
                cy = TileMap.tileYofPixel(cy);
            }
        }
        cx += cvx;
        cy += cvy;
        if (cy < 0)
        {
            cy = (cvy = 0);
        }
        if (cvy == 0)
        {
            if ((TileMap.tileTypeAtPixel(cx, cy) & 2) != 2)
            {
                statusMe = 4;
                cvx = (cspeed >> 1) * cdir;
                cp1 = (cp2 = 0);
            }
        }
        else if (cvy < 0)
        {
            cvy++;
            if (cvy == 0)
            {
                cvy = 1;
            }
        }
        else
        {
            if (cvy < 20 && cp1 % 5 == 0)
            {
                cvy++;
            }
            if (cvy > 3)
            {
                cvy = 3;
            }
            if ((TileMap.tileTypeAtPixel(cx, cy + 3) & 2) == 2 && cy <= TileMap.tileXofPixel(cy + 3))
            {
                cvx = (cvy = 0);
                cy = TileMap.tileXofPixel(cy + 3);
            }
        }
        if (cvx > 0)
        {
            cvx--;
        }
        else if (cvx < 0)
        {
            cvx++;
        }
    }

    public void updateCharAutoJump()
    {
        isFreez = false;
        if (isCharge)
        {
            isCharge = false;
            SoundMn.gI().taitaoPause();
            Service.gI().skill_not_focus(3);
        }
        cx += cvx * cdir;
        cy += cvyJump;
        cvyJump++;
        if (cp1 == 0)
        {
            cf = 7;
        }
        else
        {
            cf = 23;
        }
        if (cvyJump == -3)
        {
            cf = 8;
        }
        else if (cvyJump == -2)
        {
            cf = 9;
        }
        else if (cvyJump == -1)
        {
            cf = 10;
        }
        else if (cvyJump == 0)
        {
            cf = 11;
        }
        if (cvyJump == 0)
        {
            statusMe = 6;
            cp3 = 0;
            ((MovePoint)vMovePoints.firstElement()).status = 4;
            isJump = true;
            cp1 = 0;
            cvy = 1;
        }
    }

    public int getVx(int size, int dx, int dy)
    {
        if (dy > 0 && !TileMap.tileTypeAt(cx, cy, 2))
        {
            if (dx - dy <= 10)
            {
                return 5;
            }
            if (dx - dy <= 30)
            {
                return 6;
            }
            if (dx - dy <= 50)
            {
                return 7;
            }
            if (dx - dy <= 70)
            {
                return 8;
            }
        }
        if (dx <= 30)
        {
            return 4;
        }
        if (dx <= 160)
        {
            return 5;
        }
        if (dx <= 270)
        {
            return 6;
        }
        if (dx <= 320)
        {
            return 7;
        }
        return 8;
    }

    public void hide()
    {
        isHide = true;
        EffecMn.addEff(new Effect(107, cx, cy + 25, 3, 15, 1));
    }

    public void show()
    {
        isHide = false;
        EffecMn.addEff(new Effect(107, cx, cy + 25, 3, 10, 1));
    }

    public int getVy(int size, int dx, int dy)
    {
        if (dy <= 10)
        {
            return 5;
        }
        if (dy <= 20)
        {
            return 6;
        }
        if (dy <= 30)
        {
            return 7;
        }
        if (dy <= 40)
        {
            return 8;
        }
        if (dy <= 50)
        {
            return 9;
        }
        return 10;
    }

    public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
    {
        int num = xEnd - xFirst;
        int num2 = yEnd - yFirst;
        if (num == 0 && num2 == 0)
        {
            return 1;
        }
        if (num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2))
        {
            return 2;
        }
        if (num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2)))
        {
            return 4;
        }
        cvy = -10;
        cp1 = 0;
        cdir = ((num > 0) ? 1 : (-1));
        if (num <= 5)
        {
            cvx = 0;
        }
        else if (num <= 10)
        {
            cvx = 3;
        }
        else
        {
            cvx = 5;
        }
        return 9;
    }

    public void setAutoJump()
    {
        int num = ((MovePoint)vMovePoints.firstElement()).xEnd - cx;
        cvyJump = -10;
        cp1 = 0;
        cdir = ((num > 0) ? 1 : (-1));
        if (num <= 6)
        {
            cvx = 0;
        }
        else if (num <= 20)
        {
            cvx = 3;
        }
        else
        {
            cvx = 5;
        }
    }

    // --- moveTo ---
    public void moveTo(int toX, int toY, int type)
    {
        if (type == 1 || Res.abs(toX - cx) > 100 || Res.abs(toY - cy) > 300)
        {
            createShadow(cx, cy, 10);
            cx = toX;
            cy = toY;
            vMovePoints.removeAllElements();
            statusMe = 6;
            cp3 = 0;
            currentMovePoint = null;
            cf = 25;
            return;
        }
        int dir = 0;
        int act = 0;
        int num = toX - cx;
        int num2 = toY - cy;
        if (num == 0 && num2 == 0)
        {
            act = 1;
            cp3 = 0;
        }
        else if (num2 == 0)
        {
            act = 2;
            if (num > 0)
            {
                dir = 1;
            }
            if (num < 0)
            {
                dir = -1;
            }
        }
        else if (num2 != 0)
        {
            if (num2 < 0)
            {
                act = 3;
            }
            if (num2 > 0)
            {
                act = 4;
            }
            if (num < 0)
            {
                dir = -1;
            }
            if (num > 0)
            {
                dir = 1;
            }
        }
        vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
        if (statusMe != 6)
        {
            statusBeforeNothing = statusMe;
        }
        statusMe = 6;
        cp3 = 0;
    }

    // --- updateCharInBridge ---
    public void updateCharInBridge()
    {
        if (!GameCanvas.lowGraphic)
        {
            if (TileMap.tileTypeAt(cx, cy + 1, 1024))
            {
                TileMap.setTileTypeAtPixel(cx, cy + 1, 512);
                TileMap.setTileTypeAtPixel(cx, cy - 2, 512);
            }
            if (TileMap.tileTypeAt(cx - TileMap.size, cy + 1, 512))
            {
                TileMap.killTileTypeAt(cx - TileMap.size, cy + 1, 512);
                TileMap.killTileTypeAt(cx - TileMap.size, cy - 2, 512);
            }
            if (TileMap.tileTypeAt(cx + TileMap.size, cy + 1, 512))
            {
                TileMap.killTileTypeAt(cx + TileMap.size, cy + 1, 512);
                TileMap.killTileTypeAt(cx + TileMap.size, cy - 2, 512);
            }
        }
    }

    // --- stopMoving ---
    public void stopMoving()
    {
    }

    // --- setPos ---
    public void setPos(short xPos, short yPos, sbyte typePos)
    {
        isSetPos = true;
        this.xPos = xPos;
        this.yPos = yPos;
        this.typePos = typePos;
        tpos = 0;
        if (me)
        {
            if (GameCanvas.panel != null)
            {
                GameCanvas.panel.hide();
            }
            if (GameCanvas.panel2 != null)
            {
                GameCanvas.panel2.hide();
            }
        }
    }
}
