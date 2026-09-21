using System;
using UnityEngine;

public partial class Effect_End
{
    public static Image getImage(int id)
    {
        if (id < 0)
        {
            return null;
        }
        string path = "/e/e_" + id + ".png";
        Image result = null;
        try
        {
            result = mSystem.loadImage(path);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static void setSoundSkill_END(int x, int y, int typeEffect)
    {
        try
        {
            int num = -1;
            int num2 = Res.random(3);
            if (num >= 0)
            {
                SoundMn.playSound(x, y, num, SoundMn.volume);
            }
        }
        catch (Exception ex)
        {
            Res.err("ERR setSoundSkill_END: " + ex.ToString());
        }
    }

    public void create_Effect()
    {
        try
        {
            setSoundSkill_END(x, y, typeEffect);
            switch (typeEffect)
            {
                case 0:
                case 1:
                case 2:
                    set_End_String(typeEffect);
                    break;
                case 3:
                    set_FireWork();
                    break;
                case 16:
                case 17:
                    set_Sub();
                    break;
                case 18:
                case 19:
                case 20:
                    set_Pow();
                    break;
                case 21:
                case 22:
                case 23:
                    set_Gong();
                    break;
                case 24:
                    set_Skill_Kamex10();
                    break;
                case 25:
                    set_Skill_Destroy();
                    break;
                case 26:
                    set_Skill_MaFuba();
                    break;
                case 9:
                    set_LINE_IN();
                    break;
                case 10:
                case 11:
                    set_End_Rock();
                    break;
            }
        }
        catch (Exception ex)
        {
            Res.err("ERR create_Effect: " + ex.ToString());
            removeEff();
        }
    }

    public void update()
    {
        try
        {
            f++;
            switch (typeEffect)
            {
                case 0:
                case 1:
                case 2:
                    upd_End_String();
                    break;
                case 3:
                    upd_FireWork();
                    break;
                case 16:
                case 17:
                    upd_Sub();
                    break;
                case 18:
                case 19:
                case 20:
                    upd_Pow();
                    break;
                case 21:
                case 22:
                case 23:
                    upd_Gong();
                    break;
                case 24:
                    upd_Skill_Kamex10();
                    break;
                case 25:
                    upd_Skill_Destroy();
                    break;
                case 26:
                    upd_Skill_MaFuba();
                    break;
                case 9:
                    upd_LINE_IN();
                    break;
                case 10:
                case 11:
                    upd_End_Rock();
                    break;
            }
        }
        catch (Exception ex)
        {
            Res.err("ERR update: " + ex.ToString());
            removeEff();
        }
    }

    public void paint(mGraphics g)
    {
        try
        {
            if (isRemove || f < 0)
            {
                return;
            }
            switch (typeEffect)
            {
                case 0:
                case 1:
                case 2:
                    pnt_End_String(g);
                    break;
                case 3:
                    pnt_FireWork(g);
                    break;
                case 17:
                    pnt_Sub(g, mGraphics.VCENTER);
                    break;
                case 16:
                    if (typeSub == 0)
                    {
                        pnt_Sub(g, mGraphics.BOTTOM | mGraphics.HCENTER);
                    }
                    else
                    {
                        pnt_Sub(g, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                    break;
                case 18:
                case 19:
                case 20:
                    pnt_Pow(g, mGraphics.BOTTOM | mGraphics.HCENTER);
                    break;
                case 21:
                case 22:
                case 23:
                    pnt_Gong(g, mGraphics.VCENTER | mGraphics.HCENTER);
                    break;
                case 24:
                    pnt_Skill_Kamex10(g);
                    break;
                case 25:
                    pnt_Skill_Destroy(g);
                    break;
                case 26:
                    pnt_Skill_MaFuba(g);
                    break;
                case 9:
                    pnt_LINE_IN(g);
                    break;
                case 10:
                case 11:
                    pnt_End_Rock(g);
                    break;
            }
        }
        catch (Exception ex)
        {
            Res.err(ex.ToString());
            removeEff();
        }
    }

    public void removeEff()
    {
        isRemove = true;
    }

    public void createDanFocus(bool isRandom, Char obj)
    {
        if (isRandom)
        {
            switch (Res.random(4))
            {
                case 0:
                    gocT_Arc = 90;
                    break;
                case 1:
                    gocT_Arc = 270;
                    break;
                case 2:
                    gocT_Arc = 180;
                    break;
                case 3:
                    gocT_Arc = 0;
                    break;
            }
        }
        else if (obj.cdir == 1)
        {
            gocT_Arc = 0;
        }
        else
        {
            gocT_Arc = 180;
        }
        va = (short)(256 * vMax);
        vx = 0;
        vy = 0;
        life = 0;
        vx1000 = va * Res.cos(gocT_Arc) >> 10;
        vy1000 = va * Res.sin(gocT_Arc) >> 10;
    }

    public void updateAngleXP(int fmove)
    {
        if (f < fmove)
        {
            return;
        }
        if (charUse == null || target == null || f >= fRemove)
        {
            f = fRemove;
            return;
        }
        int num = target.x - charUse.cx;
        int num2 = target.y - charUse.cy;
        life++;
        if ((Res.abs(num) < 10 && Res.abs(num2) < 10) || life > fRemove)
        {
            f = fRemove;
            return;
        }
        int num3 = Res.angle(num, num2);
        if (Res.abs(num3 - gocT_Arc) < 90 || num * num + num2 * num2 > 4096)
        {
            if (Res.abs(num3 - gocT_Arc) < 15)
            {
                gocT_Arc = num3;
            }
            else if ((num3 - gocT_Arc >= 0 && num3 - gocT_Arc < 180) || num3 - gocT_Arc < -180)
            {
                gocT_Arc = Res.fixangle(gocT_Arc + 15);
            }
            else
            {
                gocT_Arc = Res.fixangle(gocT_Arc - 15);
            }
        }
        if (f > fRemove * 2 / 3 && va < 8192)
        {
            va += 3096;
        }
        vx1000 = va * Res.cos(gocT_Arc) >> 10;
        vy1000 = va * Res.sin(gocT_Arc) >> 10;
        num += vx1000;
        int num4 = num >> 10;
        x += num4;
        num &= 0x3FF;
        num2 += vy1000;
        int num5 = num2 >> 10;
        y += num5;
        num2 &= 0x3FF;
    }

    public int setFrameAngle(int goc)
    {
        if (goc <= 15 || goc > 345)
        {
            return 12;
        }
        int num = (goc - 15) / 15 + 1;
        if (num > 24)
        {
            num = 24;
        }
        return mpaintone_Arrow[num];
    }

    public void create_Arrow(int vMax, Point targetPoint)
    {
        this.vMax = vMax;
        int num = 0;
        int num2 = 0;
        if (targetPoint != null)
        {
            num = targetPoint.x - x;
            num2 = targetPoint.y - y;
            toX = targetPoint.x;
            toY = targetPoint.y;
        }
        else
        {
            num = toX - x;
            num2 = toY - y;
        }
        if (x > toX)
        {
            dir = 2;
            dir_nguoc = 0;
        }
        else
        {
            dir = 0;
            dir_nguoc = 2;
        }
        int frameAngle = Res.angle(num, num2);
        frame = setFrameAngle(frameAngle);
        fSpeed = frame;
        create_Speed(num, num2);
    }

    public void create_Speed(int dx, int dy)
    {
        int num = 0;
        int num2 = 0;
        int num3 = Res.getDistance(dx, dy) / vMax;
        if (num3 == 0)
        {
            num3 = 1;
        }
        num = dx / num3;
        num2 = dy / num3;
        if (num == 0 && dx < num3)
        {
            num = ((dx >= 0) ? 1 : (-1));
        }
        if (num2 == 0 && dy < num3)
        {
            num2 = ((dy >= 0) ? 1 : (-1));
        }
        if (Res.abs(num) > Res.abs(dx))
        {
            num = dx;
        }
        if (Res.abs(num2) > Res.abs(dy))
        {
            num2 = dy;
        }
        vx = num;
        vy = num2;
    }

    public void moveTo_xy(int toX, int toY, int fMove, int typeEff_End, int rangeEnd)
    {
        if (f < fMove)
        {
            frame = setFrameAngle((dir == -1) ? 180 : 0);
            return;
        }
        frame = fSpeed;
        if (Res.abs(x - toX) < Res.abs(vx))
        {
            x = toX;
            vx = 0;
        }
        else
        {
            x += vx;
        }
        if (Res.abs(y - toY) < Res.abs(vy))
        {
            y = toY;
            vy = 0;
        }
        else
        {
            y += vy;
        }
        if (Res.abs(x - toX) >= Res.abs(vMax) || Res.abs(y - toY) >= Res.abs(vMax) || typeEff_End < 0)
        {
            return;
        }
        if (target != null)
        {
            int num = target.x;
            int num2 = target.y;
            if (rangeEnd > 0)
            {
                num += Res.random_Am(0, rangeEnd);
                num2 += Res.random_Am(0, rangeEnd);
            }
            GameScr.addEffectEnd(typeEff_End, 0, 0, num, num2, 1, 0, -1, null);
            removeEff();
        }
        else if (isAddSub)
        {
            isAddSub = false;
            int num3 = x;
            int num4 = y;
            if (rangeEnd > 1)
            {
                num3 += Res.random_Am_0(rangeEnd);
                num4 += Res.random_Am_0(rangeEnd);
            }
            GameScr.addEffectEnd(typeEff_End, 0, 0, num3, num4, 1, 0, -1, null);
        }
    }

    public void paint_Arrow(mGraphics g, FrameImage frm, int index, int x, int y, int anchor, bool isCountFr)
    {
        if (frm != null)
        {
            int num = frm.nFrame / 3;
            if (num < 1)
            {
                num = 1;
            }
            int num2 = 0;
            int num3 = 3;
            if (frm.nFrame <= 6)
            {
                num2 = ((frm.nFrame <= 3) ? (f % num) : ((f / num3 % 2 != 0) ? 3 : 0));
            }
            else
            {
                num = 1;
                num2 = ((f / num3 - fMove > 8) ? 6 : ((f / num3 - fMove > 4) ? 3 : 0));
            }
            int idx = num * mImageArrow[index] + num2;
            if (frm.nFrame < 3)
            {
                idx = f / num3 % frm.nFrame;
            }
            if (isCountFr)
            {
                idx = f / num3 % frm.nFrame;
            }
            frm.drawFrame(idx, x, y, mXoayArrow[index], anchor, g);
        }
    }




























    //private void get_Img_Skill()
    //{
    //    int num = 0;
    //    bool highLevel = this.skillLevel > 4;

    //    int[] array = null;
    //    switch (typeEffect)
    //    {
    //        case 18:
    //            num = 24;
    //            array = new int[1] { highLevel ? 31 : 9 };
    //            break;
    //        case 21:
    //            num = 24;
    //            array = new int[1] { highLevel ? 32 : 10 };
    //            break;
    //        case 24:
    //            num = 24;
    //            array = highLevel ? new int[3] { 33, 34, 35 } : new int[3] { 11, 12, 13 };
    //            break;

    //        case 19:
    //            num = 25;
    //            array = new int[1] { highLevel ? 36 : 14 };
    //            break;
    //        case 22:
    //            num = 25;
    //            array = new int[1] { highLevel ? 37 : 15 };
    //            break;
    //        case 17:
    //            num = 25;
    //            array = new int[1] { highLevel ? 38 : 16 };
    //            break;
    //        case 25:
    //            num = 25;
    //            array = highLevel ? new int[4] { 40, 42, 43, 44 } : new int[4] { 17, 18, 19, 20 };
    //            break;

    //        case 20:
    //            num = 26;
    //            array = new int[1] { highLevel ? 45 : 21 };
    //            break;
    //        case 23:
    //            num = 26;
    //            array = new int[1] { highLevel ? 46 : 22 };
    //            break;
    //        case 16:
    //            num = 26;
    //            if (typeSub == 0)
    //            {
    //                array = new int[1] { 28 };
    //            }
    //            if (typeSub == 1)
    //            {
    //                array = new int[1] { highLevel ? 48 : 23 };
    //            }
    //            break;
    //        case 26:
    //            {
    //                num = 26;
    //                int itemType = 0;
    //                if (typeSub == 0)
    //                {
    //                    itemType = 25;
    //                }
    //                else if (typeSub == 1)
    //                {
    //                    itemType = 26;
    //                }
    //                else if (typeSub == 2)
    //                {
    //                    itemType = 27;
    //                }
    //                array = new int[2] { itemType, highLevel ? 49 : 24 };
    //                break;
    //            }
    //    }
    //    if (array == null)
    //    {
    //        return;
    //    }
    //    fra_skill = new FrameImage[array.Length];
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        string nameImg = "Skills_" + num + "_" + typePaint + "_" + array[i];
    //        Debug.Log("NAME: " + nameImg);
    //        FrameImage frameImage = mSystem.getFraImage(nameImg);
    //        frameImage ??= new FrameImage(array[i]);
    //        if (frameImage != null)
    //        {
    //            fra_skill[i] = frameImage;
    //        }
    //    }
    //}













}
