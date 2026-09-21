using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- setAttack ---
    public void setAttack()
    {
        if (me)
        {
            SkillPaint skillPaint = skillPaintRandomPaint;
            if (dart != null)
            {
                skillPaint = dart.skillPaint;
            }
            if (skillPaint == null)
            {
                return;
            }
            MyVector myVector = new MyVector();
            MyVector myVector2 = new MyVector();
            if (charFocus != null)
            {
                myVector2.addElement(charFocus);
            }
            else if (mobFocus != null)
            {
                myVector.addElement(mobFocus);
            }
            effPaints = new EffectPaint[myVector.size() + myVector2.size()];
            for (int i = 0; i < myVector.size(); i++)
            {
                effPaints[i] = new EffectPaint();
                effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
                if (!isSelectingSkillUseAlone())
                {
                    effPaints[i].eMob = (Mob)myVector.elementAt(i);
                }
            }
            for (int j = 0; j < myVector2.size(); j++)
            {
                effPaints[j + myVector.size()] = new EffectPaint();
                effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
                effPaints[j + myVector.size()].eChar = (Char)myVector2.elementAt(j);
            }
            int type = 0;
            if (mobFocus != null)
            {
                type = 1;
            }
            else if (charFocus != null)
            {
                type = 2;
            }
            if (myVector.size() == 0 && myVector2.size() == 0)
            {
                stopUseChargeSkill();
            }
            if (me && !isSelectingSkillUseAlone() && !hasSendAttack)
            {
                Service.gI().sendPlayerAttack(myVector, myVector2, type);
                hasSendAttack = true;
            }
            return;
        }
        SkillPaint skillPaint2 = skillPaintRandomPaint;
        if (dart != null)
        {
            skillPaint2 = dart.skillPaint;
        }
        if (skillPaint2 == null)
        {
            return;
        }
        if (attMobs != null)
        {
            effPaints = new EffectPaint[attMobs.Length];
            for (int k = 0; k < attMobs.Length; k++)
            {
                effPaints[k] = new EffectPaint();
                effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
                effPaints[k].eMob = attMobs[k];
            }
            attMobs = null;
        }
        else if (attChars != null)
        {
            effPaints = new EffectPaint[attChars.Length];
            for (int l = 0; l < attChars.Length; l++)
            {
                effPaints[l] = new EffectPaint();
                effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
                effPaints[l].eChar = attChars[l];
            }
            attChars = null;
        }
    }

    // --- getcharInjure ---
    public static void getcharInjure(int cID, int dx, int dy, long HP)
    {
        Char @char = (Char)GameScr.vCharInMap.elementAt(cID);
        if (@char.vMovePoints.size() != 0)
        {
            MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
            int xEnd = movePoint.xEnd + dx;
            int yEnd = movePoint.yEnd + dy;
            Char char2 = (Char)GameScr.vCharInMap.elementAt(cID);
            char2.cHP -= HP;
            if (char2.cHP < 0)
            {
                char2.cHP = 0;
            }
            char2.cHPShow = ((Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
            char2.statusMe = 6;
            char2.cp3 = 0;
            char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
        }
    }

    // --- injure_die_revive ---
    public void doInjure(long HPShow, int MPShow, bool isCrit, bool isMob)
    {
        this.isCrit = isCrit;
        this.isMob = isMob;
        cHP -= HPShow;
        cMP -= MPShow;
        GameScr.gI().isInjureHp = true;
        GameScr.gI().twHp = 0;
        GameScr.gI().isInjureMp = true;
        GameScr.gI().twMp = 0;
        if (cHP < 0)
        {
            cHP = 0;
        }
        if (cMP < 0)
        {
            cMP = 0;
        }
        if (isMob || (!isMob && cTypePk != 4 && damMP != -100))
        {
            if (HPShow <= 0)
            {
                if (me)
                {
                    GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS_ME);
                }
                else
                {
                    GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS);
                }
            }
            else
            {
                GameScr.startFlyText("-" + HPShow, cx, cy - ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
            }
        }
        if (HPShow > 0)
        {
            isInjure = 6;
        }
        ServerEffect.addServerEffect(80, this, 1);
        if (isDie)
        {
            isDie = false;
            isLockKey = false;
            startDie((short)xSd, (short)ySd);
        }
    }

    public void doInjure()
    {
        GameScr.gI().isInjureHp = true;
        GameScr.gI().twHp = 0;
        GameScr.gI().isInjureMp = true;
        GameScr.gI().twMp = 0;
        isInjure = 6;
        ServerEffect.addServerEffect(8, this, 1);
        isInjureHp = true;
        twHp = 0;
    }

    public void startDie(short toX, short toY)
    {
        isMonkey = 0;
        isWaitMonkey = false;
        if (me && isDie)
        {
            return;
        }
        if (me)
        {
            isLockMove = true;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                @char.killCharId = -9999;
            }
            if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
            {
                GameCanvas.panel.cp = null;
            }
            if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
            {
                GameCanvas.panel2.cp = null;
            }
        }
        statusMe = 5;
        cp2 = toX;
        cp3 = toY;
        cp1 = 0;
        cHP = 0;
        testCharId = -9999;
        killCharId = -9999;
        if (me && myskill != null && myskill.template.id != 14)
        {
            stopUseChargeSkill();
        }
        cTypePk = 0;
    }

    public void waitToDie(short toX, short toY)
    {
        wdx = toX;
        wdy = toY;
    }

    public void liveFromDead()
    {
        cHP = cHPFull;
        cMP = cMPFull;
        statusMe = 1;
        cp1 = (cp2 = (cp3 = 0));
        ServerEffect.addServerEffect(109, this, 2);
        GameScr.gI().center = null;
        GameScr.isHaveSelectSkill = true;
    }

    // --- isLang_isMeCanAttack ---
    public bool isLang()
    {
        if (TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48)
        {
            return true;
        }
        return false;
    }

    public bool isMeCanAttackOtherPlayer(Char cAtt)
    {
        if (cAtt == null || myCharz().myskill == null || myCharz().myskill.template.type == 2 || (myCharz().myskill.template.type == 4 && cAtt.statusMe != 14 && cAtt.statusMe != 5))
        {
            return false;
        }
        return ((cAtt.cTypePk == 3 && myCharz().cTypePk == 3) || myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (myCharz().cTypePk == 1 && cAtt.cTypePk == 1) || (myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (myCharz().testCharId >= 0 && myCharz().testCharId == cAtt.charID) || (myCharz().killCharId >= 0 && myCharz().killCharId == cAtt.charID && !isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == myCharz().charID && !isLang()) || (myCharz().cFlag == 8 && cAtt.cFlag != 0) || (myCharz().cFlag != 0 && cAtt.cFlag == 8) || (myCharz().cFlag != cAtt.cFlag && myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14 && cAtt.statusMe != 5;
    }

    // --- cancelAttack ---
    public void cancelAttack()
    {
    }

    // --- sendNewAttack ---
    public void sendNewAttack(short idTemplateSkill)
    {
        short x = -1;
        short y = -1;
        if (mobFocus != null)
        {
            x = (short)mobFocus.x;
            y = (short)mobFocus.y;
        }
        if (charFocus != null && !charFocus.isPet && !charFocus.isMiniPet)
        {
            x = (short)charFocus.cx;
            y = (short)charFocus.cy;
        }
        Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)cdir, x, y);
    }
}
