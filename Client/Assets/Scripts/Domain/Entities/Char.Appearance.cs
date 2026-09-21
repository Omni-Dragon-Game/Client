using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- fusion ---
    public void fusionComplete()
    {
        isFusion = false;
        isLockKey = false;
        tFusion = 0;
    }

    public void setFusion(sbyte fusion)
    {
        tFusion = 0;
        if (fusion == 4 || fusion == 5)
        {
            if (me)
            {
                Service.gI().funsion(fusion);
            }
            EffecMn.addEff(new Effect(34, cx, cy + 12, 2, 1, -1));
        }
        if (fusion == 6)
        {
            EffecMn.addEff(new Effect(38, cx, cy + 12, 2, 1, -1));
        }
        if (me)
        {
            GameCanvas.panel.hideNow();
            isLockKey = true;
        }
        isFusion = true;
        if (fusion == 1)
        {
            isNhapThe = false;
        }
        else
        {
            isNhapThe = true;
        }
    }

    // --- partTransforms ---
    public void setPartOld()
    {
        headTemp = head;
        bodyTemp = body;
        legTemp = leg;
        bagTemp = bag;
    }

    public void setPartTemp(int head, int body, int leg, int bag)
    {
        if (head != -1)
        {
            this.head = head;
        }
        if (body != -1)
        {
            this.body = body;
        }
        if (leg != -1)
        {
            this.leg = leg;
        }
        if (bag != -1)
        {
            this.bag = bag;
        }
    }

    public void resetPartTemp()
    {
        if (headTemp != -1)
        {
            head = headTemp;
            headTemp = -1;
        }
        if (bodyTemp != -1)
        {
            body = bodyTemp;
            bodyTemp = -1;
        }
        if (legTemp != -1)
        {
            leg = legTemp;
            legTemp = -1;
        }
        if (bagTemp != -1)
        {
            bag = bagTemp;
            bagTemp = -1;
        }
    }

    // --- eyeAuraHat ---
    public void updateEye()
    {
        if (head != 934)
        {
            return;
        }
        if (GameCanvas.timeNow - timeAddChopmat > 0)
        {
            fChopmat++;
            if (fChopmat > frEye.Length - 1)
            {
                fChopmat = 0;
                timeAddChopmat = GameCanvas.timeNow + Res.random(2000, 3500);
                frEye = frChopCham;
                if (Res.random(2) == 0)
                {
                    frEye = frChopNhanh;
                }
            }
        }
        else
        {
            fChopmat = 0;
        }
    }

    private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
    {
        if (head != 934 || (statusMe != 1 && statusMe != 6))
        {
            return;
        }
        if (fraRedEye == null || fraRedEye.imgFrame == null)
        {
            Image img = mSystem.loadImage("/redeye.png");
            fraRedEye = new FrameImage(img, 14, 10);
        }
        else if (frEye[fChopmat] != -1)
        {
            int num = 8;
            int num2 = 15;
            if (trans == 2)
            {
                num = -8;
            }
            fraRedEye.drawFrame(frEye[fChopmat], xx + num, yy + num2, trans, anchor, g);
        }
    }

    public bool isHead_2Fr(int idHead)
    {
        for (int i = 0; i < Arr_Head_2Fr.Length; i++)
        {
            if (Arr_Head_2Fr[i][0] == idHead)
            {
                return true;
            }
        }
        return false;
    }

    private void updateFHead()
    {
        if (isHead_2Fr(head))
        {
            fHead++;
            if (fHead > 10000)
            {
                fHead = 0;
            }
        }
        else
        {
            fHead = 0;
        }
    }

    private int getFHead(int idHead)
    {
        for (int i = 0; i < Arr_Head_2Fr.Length; i++)
        {
            if (Arr_Head_2Fr[i][0] == idHead)
            {
                return Arr_Head_2Fr[i][fHead / 4 % Arr_Head_2Fr[i].Length];
            }
        }
        return idHead;
    }

    public void paintAuraBehind(mGraphics g)
    {
        if ((!me || !isPaintAura) && idAuraEff > -1 && (statusMe == 1 || statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - timeBlue > 0)
        {
            string nameImg = strEffAura + idAuraEff + "_0";
            FrameImage fraImage = mSystem.getFraImage(nameImg);
            fraImage?.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
        }
    }

    public void paintAuraFront(mGraphics g)
    {
        if ((me && !isPaintAura) || idAuraEff <= -1)
        {
            return;
        }
        if (statusMe == 1 || statusMe == 6)
        {
            if (!GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
            {
                bool flag = false;
                if (mSystem.currentTimeMillis() - timeBlue > -1000 && IsAddDust1)
                {
                    flag = true;
                    IsAddDust1 = false;
                }
                if (mSystem.currentTimeMillis() - timeBlue > -500 && IsAddDust2)
                {
                    flag = true;
                    IsAddDust2 = false;
                }
                if (flag)
                {
                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                    GameCanvas.gI().startDust(1, cx - 8, cy);
                    addDustEff(1);
                }
                if (mSystem.currentTimeMillis() - timeBlue > 0)
                {
                    string nameImg = strEffAura + idAuraEff + "_1";
                    FrameImage fraImage = mSystem.getFraImage(nameImg);
                    fraImage?.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy + 2, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
            }
        }
        else
        {
            timeBlue = mSystem.currentTimeMillis() + 1500;
            IsAddDust1 = true;
            IsAddDust2 = true;
        }
    }

    public void paintEff_Lvup_behind(mGraphics g)
    {
        if (idEff_Set_Item != -1)
        {
            if (fraEff != null)
            {
                fraEff.drawFrame(GameCanvas.gameTick / 4 % fraEff.nFrame, cx, cy + 3, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraEff = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_0");
            }
        }
    }

    public void paintEff_Lvup_front(mGraphics g)
    {
        if (idEff_Set_Item != -1)
        {
            if (fraEffSub != null)
            {
                fraEffSub.drawFrame(GameCanvas.gameTick / 4 % fraEffSub.nFrame, cx, cy + 8, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraEffSub = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_1");
            }
        }
    }

    public void paintHat_behind(mGraphics g, int cf, int yh)
    {
        try
        {
            if (idHat == -1)
            {
                return;
            }
            if (isFrNgang(cf))
            {
                if (fraHat_behind_2 != null)
                {
                    fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind_2.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
                else
                {
                    fraHat_behind_2 = mSystem.getFraImage(strHat_behind + strNgang + idHat);
                }
            }
            else if (fraHat_behind != null)
            {
                fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraHat_behind = mSystem.getFraImage(strHat_behind + idHat);
            }
        }
        catch (Exception)
        {
        }
    }

    public void paintHat_front(mGraphics g, int cf, int yh)
    {
        try
        {
            if (idHat == -1)
            {
                return;
            }
            if (isFrNgang(cf))
            {
                if (fraHat_font_2 != null)
                {
                    fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_font_2.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
                else
                {
                    fraHat_font_2 = mSystem.getFraImage(strHat_font + strNgang + idHat);
                }
            }
            else if (fraHat_font != null)
            {
                fraHat_font.drawFrame(GameCanvas.gameTick / 4 % fraHat_font.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraHat_font = mSystem.getFraImage(strHat_font + idHat);
            }
        }
        catch (Exception)
        {
        }
    }
}
