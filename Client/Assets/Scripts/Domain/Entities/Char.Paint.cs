using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- paintMaster ---
    public virtual void paint(mGraphics g)
    {
        if (isHide)
        {
            return;
        }
        if (isMafuba)
        {
            paintCharWithoutSkill(g);
        }
        else if (isMabuHold)
        {
            if (cmtoChar)
            {
                GameScr.cmtoX = cx - GameScr.gW2;
                GameScr.cmtoY = cy - GameScr.gH23;
                if (!GameCanvas.isTouchControl)
                {
                    GameScr.cmtoX += GameScr.gW6 * cdir;
                }
            }
        }
        else
        {
            if (!isPaint() || (!me && GameScr.notPaint))
            {
                return;
            }
            if (petFollow != null)
            {
                petFollow.paint(g);
            }
            paintMount1(g);
            if ((TileMap.isInAirMap() && cy >= TileMap.pxh - 48) || isTeleport)
            {
                return;
            }
            if (holder && GameCanvas.gameTick % 2 == 0)
            {
                g.setColor(16185600);
                if (charHold != null)
                {
                    g.drawLine(cx, cy - ch / 2, charHold.cx, charHold.cy - charHold.ch / 2);
                }
                if (mobHold != null)
                {
                    g.drawLine(cx, cy - ch / 2, mobHold.x, mobHold.y - mobHold.h / 2);
                }
            }
            paintSuperEffBehind(g);
            paintAuraBehind(g);
            paintEffBehind(g);
            paintEff_Lvup_behind(g);
            paintEff_Pet(g);
            if (shadowLife > 0)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    paintCharBody(g, shadowX, shadowY, cdir, 25, isPaintBag: true);
                }
                else if (shadowLife > 5)
                {
                    paintCharBody(g, shadowX, shadowY, cdir, 7, isPaintBag: true);
                }
            }
            if (!isPaint() && skillPaint != null && (skillPaint.id < 70 || skillPaint.id > 76) && (skillPaint.id < 77 || skillPaint.id > 83))
            {
                if (skillPaint != null)
                {
                    indexSkill = skillInfoPaint().Length;
                    skillPaint = null;
                }
                effPaints = null;
                eff = null;
                effTask = null;
                indexEff = -1;
                indexEffTask = -1;
            }
            else if (statusMe != 15 && (moveFast == null || moveFast[0] <= 0))
            {
                PaintCharName_HP_MP_Overhead(g);
                if (skillPaint == null || skillInfoPaint() == null || indexSkill >= skillInfoPaint().Length)
                {
                    paintCharWithoutSkill(g);
                }
                if (arr != null)
                {
                    arr.paint(g);
                }
                if (dart != null)
                {
                    dart.paint(g);
                }
                paintEffect(g);
                if (mobMe != null)
                {
                }
                paintMount2(g);
                paintEff_Lvup_front(g);
                paintSuperEffFront(g);
                paintAuraFront(g);
                paintEffFront(g);
                paint_map_line(g);
            }
        }
    }

    // --- charBodyParts_render ---
    private void paintCharWithoutSkill(mGraphics g)
    {
        try
        {
            if (isMafuba)
            {
                paintCharBody(g, xMFB, yMFB, cdir, cf, isPaintBag: false);
                return;
            }
            if (isInvisiblez)
            {
                if (me)
                {
                    if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
                    {
                        SmallImage.drawSmallImage(g, 1196, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                    else
                    {
                        SmallImage.drawSmallImage(g, 1195, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                }
            }
            else
            {
                paintCharBody(g, cx, cy + fy, cdir, cf, isPaintBag: true);
            }
            if (isLockAttack)
            {
                SmallImage.drawSmallImage(g, 290, cx, cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paint char without skill: " + ex.ToString());
        }
    }

    public void paintBag(mGraphics g, int[] id, int x, int y, int dir, bool isPaintChar)
    {
        int num = 0;
        int num2 = 0;
        if (statusMe == 6)
        {
            num = 8;
            num2 = 17;
        }
        if (statusMe == 1)
        {
            if (cp1 % 15 < 5)
            {
                num = 8;
                num2 = 17;
            }
            else
            {
                num = 8;
                num2 = 18;
            }
        }
        if (statusMe == 2)
        {
            if (cf <= 3)
            {
                num = 7;
                num2 = 17;
            }
            else
            {
                num = 7;
                num2 = 18;
            }
        }
        if (statusMe == 3 || statusMe == 9)
        {
            num = 5;
            num2 = 20;
        }
        if (statusMe == 4)
        {
            if (cf == 8)
            {
                num = 5;
                num2 = 16;
            }
            else
            {
                num = 5;
                num2 = 20;
            }
        }
        if (statusMe == 10)
        {
            if (cf == 8)
            {
                num = 0;
                num2 = 23;
            }
            else
            {
                num = 5;
                num2 = 22;
            }
        }
        if (isInjure > 0)
        {
            num = 5;
            num2 = 18;
        }
        if (skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length)
        {
            num = -1;
            num2 = 17;
        }
        fBag++;
        if (fBag > 10000)
        {
            fBag = 0;
        }
        sbyte b = (sbyte)(fBag / 4 % id.Length);
        if (!isPaintChar)
        {
            if (id.Length == 2)
            {
                b = 1;
            }
            if (id.Length == 3)
            {
                if (id[2] >= 0)
                {
                    b = 2;
                    if (GameCanvas.gameTick % 10 > 5)
                    {
                        b = 1;
                    }
                }
                else
                {
                    b = 1;
                }
            }
        }
        else if (id.Length > 1 && (b == 0 || b == 1) && statusMe != 1 && statusMe != 6)
        {
            fBag = 0;
            b = 0;
            if (GameCanvas.gameTick % 10 > 5)
            {
                b = 1;
            }
        }
        SmallImage.drawSmallImage(g, id[b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
    }

    public bool isCharBodyImageID(int id)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || leg < 0 || leg >= GameScr.parts.Length || body < 0 || body >= GameScr.parts.Length)
        {
            return false;
        }
        Part part = GameScr.parts[head];
        Part part2 = GameScr.parts[leg];
        Part part3 = GameScr.parts[body];
        if (part == null || part2 == null || part3 == null)
        {
            return false;
        }
        for (int i = 0; i < CharInfo.Length; i++)
        {
            if (id == part.pi[CharInfo[i][0][0]].id)
            {
                return true;
            }
            if (id == part2.pi[CharInfo[i][1][0]].id)
            {
                return true;
            }
            if (id == part3.pi[CharInfo[i][2][0]].id)
            {
                return true;
            }
        }
        return false;
    }

    public void paintHead(mGraphics g, int cx, int cy, int look)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || GameScr.parts[head] == null)
        {
            return;
        }
        Part part = GameScr.parts[head];
        SmallImage.drawSmallImage(g, part.pi[CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
    }

    public void paintHeadWithXY(mGraphics g, int x, int y, int look)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || GameScr.parts[head] == null)
        {
            return;
        }
        Part part = GameScr.parts[head];
        SmallImage.drawSmallImage(g, part.pi[CharInfo[0][0][0]].id, x + CharInfo[0][0][1] + part.pi[CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
    }

    public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || leg < 0 || leg >= GameScr.parts.Length || body < 0 || body >= GameScr.parts.Length)
        {
            return;
        }
        ph = GameScr.parts[head];
        pl = GameScr.parts[leg];
        pb = GameScr.parts[body];
        if (ph == null || pl == null || pb == null)
        {
            return;
        }
        if (bag >= 0 && statusMe != 14)
        {
            if (!ClanImage.idImages.containsKey(bag + string.Empty))
            {
                ClanImage.idImages.put(bag + string.Empty, new ClanImage());
                Service.gI().requestBagImage((sbyte)bag);
            }
            else
            {
                ClanImage clanImage = (ClanImage)ClanImage.idImages.get(bag + string.Empty);
                if (clanImage.idImage != null && isPaintBag)
                {
                    paintBag(g, clanImage.idImage, cx, cy, cdir, isPaintChar: true);
                }
            }
        }
        int num = 2;
        int anchor = 24;
        int anchor2 = StaticObj.TOP_RIGHT;
        int num2 = -1;
        if (cdir == 1)
        {
            num = 0;
            anchor = 0;
            anchor2 = 0;
            num2 = 1;
        }
        if (statusMe == 14)
        {
            if (GameCanvas.gameTick % 4 > 0)
            {
                g.drawImage(ItemMap.imageFlare, cx, cy - ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            int num3 = 0;
            if (head == 89 || head == 457 || head == 460 || head == 461 || head == 462 || head == 463 || head == 464 || head == 465 || head == 466)
            {
                num3 = 15;
            }
            SmallImage.drawSmallImage(g, 834, cx, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
            SmallImage.drawSmallImage(g, 79, cx, cy - ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
            SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            paintHat_behind(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            if (isHead_2Fr(head))
            {
                Part part = GameScr.parts[getFHead(head)];
                SmallImage.drawSmallImage(g, part.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + part.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + part.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            else
            {
                SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            paintHat_front(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            paintRedEye(g, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
        }
        else
        {
            paintHat_behind(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            if (isHead_2Fr(head))
            {
                Part part2 = GameScr.parts[getFHead(head)];
                SmallImage.drawSmallImage(g, part2.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + part2.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + part2.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            else
            {
                SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            SmallImage.drawSmallImage(g, pl.pi[CharInfo[cf][1][0]].id, cx + (CharInfo[cf][1][1] + pl.pi[CharInfo[cf][1][0]].dx) * num2, cy - CharInfo[cf][1][2] + pl.pi[CharInfo[cf][1][0]].dy, num, anchor);
            SmallImage.drawSmallImage(g, pb.pi[CharInfo[cf][2][0]].id, cx + (CharInfo[cf][2][1] + pb.pi[CharInfo[cf][2][0]].dx) * num2, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy, num, anchor);
            paintRedEye(g, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
        }
        ch = ((isMonkey != 1 && !isFusion) ? (CharInfo[0][0][2] + ph.pi[CharInfo[0][0][0]].dy + 10) : 60);
        int num4 = ((Res.abs(ph.pi[CharInfo[cf][0][0]].dy) < 22) ? ph.pi[CharInfo[cf][0][0]].dy : ((ph.pi[CharInfo[cf][0][0]].dy >= 0) ? (ph.pi[CharInfo[cf][0][0]].dy - 5) : (ph.pi[CharInfo[cf][0][0]].dy + 5)));
        cH_new = cy - CharInfo[cf][0][2] + num4;
        if (statusMe == 1 && charID > 0 && !isMask && !isUseChargeSkill() && !isWaitMonkey && skillPaint == null && cf != 23 && bag < 0 && ((GameCanvas.gameTick + charID) % 30 == 0 || isFreez))
        {
            g.drawImage((cgender != 1) ? eyeTraiDat : eyeNamek, cx + -((cgender != 1) ? 2 : 2) * num2, cy - 32 + ((cgender != 1) ? 11 : 10) - cf, anchor2);
        }
        if (eProtect != null)
        {
            eProtect.paint(g);
        }
        if (eDanhHieu != null)
        {
            eDanhHieu.paint(g);
        }
        paintPKFlag(g);
    }

    public void paintCharWithSkill(mGraphics g)
    {
        ty = 0;
        SkillInfoPaint[] array = skillInfoPaint();
        cf = array[indexSkill].status;
        paintCharWithoutSkill(g);
        if (cdir == 1)
        {
            if (eff0 != null)
            {
                if (dx0 == 0)
                {
                    dx0 = array[indexSkill].e0dx;
                }
                if (dy0 == 0)
                {
                    dy0 = array[indexSkill].e0dy;
                }
                SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx + dx0 + eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i0++;
                if (i0 >= eff0.arrEfInfo.Length)
                {
                    eff0 = null;
                    i0 = (dx0 = (dy0 = 0));
                }
            }
            if (eff1 != null)
            {
                if (dx1 == 0)
                {
                    dx1 = array[indexSkill].e1dx;
                }
                if (dy1 == 0)
                {
                    dy1 = array[indexSkill].e1dy;
                }
                SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx + dx1 + eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i1++;
                if (i1 >= eff1.arrEfInfo.Length)
                {
                    eff1 = null;
                    i1 = (dx1 = (dy1 = 0));
                }
            }
            if (eff2 != null)
            {
                if (dx2 == 0)
                {
                    dx2 = array[indexSkill].e2dx;
                }
                if (dy2 == 0)
                {
                    dy2 = array[indexSkill].e2dy;
                }
                SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx + dx2 + eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i2++;
                if (i2 >= eff2.arrEfInfo.Length)
                {
                    eff2 = null;
                    i2 = (dx2 = (dy2 = 0));
                }
            }
        }
        else
        {
            if (eff0 != null)
            {
                if (dx0 == 0)
                {
                    dx0 = array[indexSkill].e0dx;
                }
                if (dy0 == 0)
                {
                    dy0 = array[indexSkill].e0dy;
                }
                SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx - dx0 - eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i0++;
                if (i0 >= eff0.arrEfInfo.Length)
                {
                    eff0 = null;
                    i0 = 0;
                    dx0 = 0;
                    dy0 = 0;
                }
            }
            if (eff1 != null)
            {
                if (dx1 == 0)
                {
                    dx1 = array[indexSkill].e1dx;
                }
                if (dy1 == 0)
                {
                    dy1 = array[indexSkill].e1dy;
                }
                SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx - dx1 - eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i1++;
                if (i1 >= eff1.arrEfInfo.Length)
                {
                    eff1 = null;
                    i1 = 0;
                    dx1 = 0;
                    dy1 = 0;
                }
            }
            if (eff2 != null)
            {
                if (dx2 == 0)
                {
                    dx2 = array[indexSkill].e2dx;
                }
                if (dy2 == 0)
                {
                    dy2 = array[indexSkill].e2dy;
                }
                SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx - dx2 - eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i2++;
                if (i2 >= eff2.arrEfInfo.Length)
                {
                    eff2 = null;
                    i2 = 0;
                    dx2 = 0;
                    dy2 = 0;
                }
            }
        }
        indexSkill++;
    }

    public static int getIndexChar(int ID)
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = (Char)GameScr.vCharInMap.elementAt(i);
            if (@char.charID == ID)
            {
                return i;
            }
        }
        return -1;
    }

    // --- isFrNgang ---
    public bool isFrNgang(int fr)
    {
        if (fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29)
        {
            return true;
        }
        return false;
    }
}
