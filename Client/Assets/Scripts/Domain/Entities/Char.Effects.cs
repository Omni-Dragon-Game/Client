using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- updateEffect ---
    private void updateEffect()
    {
        if (effPaints != null)
        {
            for (int i = 0; i < effPaints.Length; i++)
            {
                if (effPaints[i] == null)
                {
                    continue;
                }
                if (effPaints[i].eMob != null)
                {
                    if (!effPaints[i].isFly)
                    {
                        effPaints[i].eMob.setInjure();
                        effPaints[i].eMob.injureBy = this;
                        if (me)
                        {
                            effPaints[i].eMob.hpInjure = myCharz().cDamFull / 2 - myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
                        }
                        int num = effPaints[i].eMob.h >> 1;
                        if (effPaints[i].eMob.isBigBoss())
                        {
                            num = effPaints[i].eMob.getY() + 20;
                        }
                        GameScr.startSplash(effPaints[i].eMob.x, effPaints[i].eMob.y - num, cdir);
                        effPaints[i].isFly = true;
                    }
                }
                else if (effPaints[i].eChar != null && !effPaints[i].isFly)
                {
                    if (effPaints[i].eChar.charID >= 0)
                    {
                        effPaints[i].eChar.doInjure();
                    }
                    GameScr.startSplash(effPaints[i].eChar.cx, effPaints[i].eChar.cy - (effPaints[i].eChar.ch >> 1), cdir);
                    effPaints[i].isFly = true;
                }
                effPaints[i].index++;
                if (effPaints[i].index >= effPaints[i].effCharPaint.arrEfInfo.Length)
                {
                    effPaints[i] = null;
                }
            }
        }
        if (indexEff >= 0 && eff != null && GameCanvas.gameTick % 2 == 0)
        {
            indexEff++;
            if (indexEff >= eff.arrEfInfo.Length)
            {
                indexEff = -1;
                eff = null;
            }
        }
        if (indexEffTask >= 0 && effTask != null && GameCanvas.gameTick % 2 == 0)
        {
            indexEffTask++;
            if (indexEffTask >= effTask.arrEfInfo.Length)
            {
                indexEffTask = -1;
                effTask = null;
            }
        }
    }

    // --- superEff_soundVolumn ---
    public void updateSuperEff()
    {
        if (GameCanvas.panel.isShow || isCopy || isFusion || isSetPos || isPet || isMiniPet || isMonkey == 1)
        {
            return;
        }
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        ty++;
        if (clevel >= 14)
        {
            return;
        }
        if (clevel >= 9 && !GameCanvas.lowGraphic && (ty == 40 || ty == 50))
        {
            GameCanvas.gI().startDust(-1, cx - -8, cy);
            GameCanvas.gI().startDust(1, cx - 8, cy);
            addDustEff(1);
        }
        if (ty <= 50 || clevel < 9)
        {
            return;
        }
        int num = 0;
        if (cgender == 0)
        {
            if (GameCanvas.gameTick % 25 == 0)
            {
                num = 114;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 4 == 0)
            {
                num = 132;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
        if (cgender == 1)
        {
            if (GameCanvas.gameTick % 4 == 0)
            {
                num = 132;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 7 == 0)
            {
                num = 131;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
        if (cgender == 2)
        {
            if (GameCanvas.gameTick % 7 == 0)
            {
                num = 131;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 25 == 0)
            {
                num = 114;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
    }

    public float getSoundVolumn()
    {
        if (me)
        {
            return 0.1f;
        }
        int num = Res.abs(myChar.cx - cx);
        if (num >= 0 && num <= 50)
        {
            return 0.1f;
        }
        return 0.05f;
    }

    // --- setMabuHold ---
    public void setMabuHold(bool m)
    {
        isMabuHold = m;
    }

    // --- paintSuperEffects ---
    private void paintEff_Pet(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.effId >= 201)
            {
                effect.paint(g);
            }
        }
    }

    private void paintSuperEffBehind(mGraphics g)
    {
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        if (!isPaintAura2 || (statusMe != 1 && statusMe != 6) || GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0 || isCopy || clevel < 16)
        {
            return;
        }
        int num = 7598;
        int num2 = 4;
        if (clevel >= 19)
        {
            num = 7676;
        }
        if (clevel >= 22)
        {
            num = 7677;
        }
        if (clevel >= 25)
        {
            num = 7678;
        }
        if (num != -1)
        {
            Small small = SmallImage.imgNew[num];
            if (small == null)
            {
                SmallImage.createImage(num);
                return;
            }
            int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
            g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
        }
    }

    private void paintSuperEffFront(mGraphics g)
    {
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        if (!isPaintAura2)
        {
            return;
        }
        if (statusMe == 1 || statusMe == 6)
        {
            if (GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0)
            {
                return;
            }
            if (isCopy)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
                return;
            }
            if (clevel >= 14 && !GameCanvas.lowGraphic)
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
            }
            if (clevel == 14)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
            }
            else if (clevel == 15)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
            }
            else
            {
                if (clevel < 16)
                {
                    return;
                }
                int num = -1;
                int num2 = 4;
                if (clevel >= 16 && clevel < 22)
                {
                    num = 7599;
                    num2 = 4;
                }
                if (num != -1)
                {
                    Small small = SmallImage.imgNew[num];
                    if (small == null)
                    {
                        SmallImage.createImage(num);
                        return;
                    }
                    int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
                    g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
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

    private void paintEffect(mGraphics g)
    {
        if (effPaints != null)
        {
            for (int i = 0; i < effPaints.Length; i++)
            {
                if (effPaints[i] == null)
                {
                    continue;
                }
                if (effPaints[i].eMob != null)
                {
                    int y = effPaints[i].eMob.y;
                    if (effPaints[i].eMob is BigBoss)
                    {
                        y = effPaints[i].eMob.y - 60;
                    }
                    if (effPaints[i].eMob is BigBoss2)
                    {
                        y = effPaints[i].eMob.y - 50;
                    }
                    if (effPaints[i].eMob is BachTuoc)
                    {
                        y = effPaints[i].eMob.y - 40;
                    }
                    SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
                else if (effPaints[i].eChar != null)
                {
                    SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eChar.cx, effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
            }
        }
        if (indexEff >= 0 && eff != null)
        {
            SmallImage.drawSmallImage(g, eff.arrEfInfo[indexEff].idImg, cx + eff.arrEfInfo[indexEff].dx, cy + eff.arrEfInfo[indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
        }
        if (indexEffTask >= 0 && effTask != null)
        {
            SmallImage.drawSmallImage(g, effTask.arrEfInfo[indexEffTask].idImg, cx + effTask.arrEfInfo[indexEffTask].dx, cy + effTask.arrEfInfo[indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
        }
    }

    // --- addDustEff ---
    public void addDustEff(int type)
    {
        if (GameCanvas.lowGraphic)
        {
            return;
        }
        switch (type)
        {
            case 1:
                if (clevel >= 9)
                {
                    Effect effect3 = new Effect(19, cx - 5, cy + 20, 2, 1, -1);
                    EffecMn.addEff(effect3);
                }
                break;
            case 2:
                if ((!me || isMonkey != 1) && isNhapThe && GameCanvas.gameTick % 5 == 0)
                {
                    Effect effect2 = new Effect(22, cx - 5, cy + 35, 2, 1, -1);
                    EffecMn.addEff(effect2);
                }
                break;
            case 3:
                if (clevel >= 9 && ySd - cy <= 5)
                {
                    Effect effect = new Effect(19, cx - 5, ySd + 20, 2, 1, -1);
                    EffecMn.addEff(effect);
                }
                break;
        }
    }

    // --- removeStatusEffects ---
    public void removeHoleEff()
    {
        if (holder)
        {
            holder = false;
            charHold = null;
            mobHold = null;
        }
        else
        {
            holdEffID = 0;
            charHold = null;
            mobHold = null;
        }
    }

    public void removeProtectEff()
    {
        protectEff = false;
        eProtect = null;
    }

    public void removeBlindEff()
    {
        blindEff = false;
    }

    public void removeEffect()
    {
        if (holdEffID != 0)
        {
            holdEffID = 0;
        }
        if (holder)
        {
            holder = false;
        }
        if (protectEff)
        {
            protectEff = false;
        }
        eProtect = null;
        charHold = null;
        mobHold = null;
        blindEff = false;
        sleepEff = false;
    }

    // setPos extracted to Char.Navigation.cs

    public void removeHuytSao()
    {
        huytSao = false;
    }

    // fusion extracted to Char.Appearance.cs

    public void removeSleepEff()
    {
        sleepEff = false;
    }

    // --- effChar_customEffects ---
    public Effect getEffById(int id)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.effId == id)
            {
                return effect;
            }
        }
        return null;
    }

    public void addEffChar(Effect e)
    {
        removeEffChar(0, e.effId);
        vEffChar.addElement(e);
    }

    public void removeEffChar(int type, int id)
    {
        if (type == -1)
        {
            vEffChar.removeAllElements();
        }
        else if (getEffById(id) != null)
        {
            vEffChar.removeElement(getEffById(id));
        }
    }

    public void paintEffBehind(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.layer == 0)
            {
                bool flag = true;
                if (effect.isStand == 0)
                {
                    flag = ((statusMe == 1 || statusMe == 6) ? true : false);
                }
                if (flag)
                {
                    effect.paint(g);
                }
            }
        }
    }

    public void paintEffFront(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.layer == 1)
            {
                bool flag = true;
                if (effect.isStand == 0)
                {
                    flag = ((statusMe == 1 || statusMe == 6) ? true : false);
                }
                if (flag)
                {
                    effect.paint(g);
                }
            }
        }
    }

    public void updEffChar()
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            ((Effect)vEffChar.elementAt(i)).update();
        }
    }
}
