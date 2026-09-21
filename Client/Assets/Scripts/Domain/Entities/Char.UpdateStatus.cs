using System;
using Assets.src.e;
using Assets.src.g;
using Mod;
using UnityEngine;

public partial class Char
{
    public void soundUpdate()
    {
        if (me && statusMe == 10 && cf == 8 && ty > 20 && GameCanvas.gameTick % 20 == 0)
        {
            SoundMn.gI().charFly();
        }
        if (skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length && isPunchKickSkill() && (me || (!me && cx >= GameScr.cmx && cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
        {
            if (cf == 9 || cf == 10 || cf == 11)
            {
                SoundMn.gI().charPunch(isKick: true, (!me) ? 0.05f : 0.1f);
            }
            else
            {
                SoundMn.gI().charPunch(isKick: false, (!me) ? 0.05f : 0.1f);
            }
        }
    }


    public void updateChargeSkill()
    {
    }


    private void updateCharStatus()
    {
                switch (statusMe)
                {
                    case 1:
                        updateCharStand();
                        break;
                    case 2:
                        updateCharRun();
                        break;
                    case 3:
                        updateCharJump();
                        break;
                    case 4:
                        updateCharFall();
                        break;
                    case 5:
                        updateCharDeadFly();
                        break;
                    case 16:
                        updateResetPoint();
                        break;
                    case 9:
                        updateCharAutoJump();
                        break;
                    case 10:
                        updateCharFly();
                        break;
                    case 12:
                        updateSkillStand();
                        break;
                    case 13:
                        updateSkillFall();
                        break;
                    case 14:
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
                        break;
                    case 6:
                        if (isInjure <= 0)
                        {
                            cf = 0;
                        }
                        else if (statusBeforeNothing == 10)
                        {
                            cx += cvx;
                        }
                        else if (cf <= 1)
                        {
                            cp1++;
                            if (cp1 > 6)
                            {
                                cf = 0;
                            }
                            else
                            {
                                cf = 1;
                            }
                            if (cp1 > 10)
                            {
                                cp1 = 0;
                            }
                        }
                        if (cf != 7 && cf != 12 && (TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2)
                        {
                            cvx = 0;
                            cvy = 0;
                            statusMe = 4;
                            cf = 7;
                        }
                        if (me)
                        {
                            break;
                        }
                        cp3++;
                        if (cp3 > 10)
                        {
                            if ((TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2)
                            {
                                cy += 5;
                            }
                            else
                            {
                                cf = 0;
                            }
                        }
                        if (cp3 > 50)
                        {
                            cp3 = 0;
                            currentMovePoint = null;
                        }
                        break;
                }
                if (isInjure > 0)
                {
                    cf = 23;
                    isInjure--;
                }
                if (wdx != 0 || wdy != 0)
                {
                    startDie(wdx, wdy);
                    wdx = 0;
                    wdy = 0;
                }
                if (moveFast != null)
                {
                    if (moveFast[0] == 0)
                    {
                        moveFast[0]++;
                        ServerEffect.addServerEffect(60, this, 1);
                    }
                    else if (moveFast[0] < 10)
                    {
                        moveFast[0]++;
                    }
                    else
                    {
                        cx = moveFast[1];
                        cy = moveFast[2];
                        moveFast = null;
                        ServerEffect.addServerEffect(60, this, 1);
                        if (me)
                        {
                            if ((TileMap.tileTypeAtPixel(cx, cy) & 2) != 2)
                            {
                                statusMe = 4;
                                //myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
                            }
                            else
                            {
                                Service.gI().charMove();
                                //myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
                            }
                        }
                    }
                }
                if (statusMe != 10)
                {
                    fy = 0;
                }
                if (isCharge)
                {
                    cf = 17;
                    if (GameCanvas.gameTick % 4 == 0)
                    {
                        ServerEffect.addServerEffect(1, cx, cy + GameCanvas.transY, 1);
                    }
                    if (me)
                    {
                        long num7 = mSystem.currentTimeMillis();
                        if (num7 - last >= 1000)
                        {
                            last = num7;
                            cHP += cHPFull * myskill.damage / 100;
                            cMP += cMPFull * myskill.damage / 100;
                            if (cHP < cHPFull)
                            {
                                GameScr.startFlyText("+" + cHPFull * myskill.damage / 100 + " " + mResources.HP, cx, cy - ch - 20, 0, -1, mFont.HP);
                            }
                            if (cMP < cMPFull)
                            {
                                GameScr.startFlyText("+" + cMPFull * myskill.damage / 100 + " " + mResources.KI, cx, cy - ch - 20, 0, -2, mFont.MP);
                            }
                            Service.gI().skill_not_focus(2);
                        }
                    }
                }
                if (isFlyUp)
                {
                    if (me)
                    {
                        isLockKey = true;
                        statusMe = 3;
                        cvy = -8;
                        if (cy <= TileMap.pxh - 240)
                        {
                            isFlyUp = false;
                            isLockKey = false;
                            statusMe = 4;
                        }
                    }
                    else
                    {
                        statusMe = 3;
                        cvy = -8;
                        if (cy <= TileMap.pxh - 240)
                        {
                            cvy = 0;
                            isFlyUp = false;
                            cvy = 0;
                            statusMe = 1;
                        }
                    }
                }
                updateMount();
                updEffChar();
                updateEye();
                updateFHead();
    }

    private void checkHideCharName()
    {
        if (GameCanvas.gameTick % 20 != 0 || charID < 0)
        {
            return;
        }
        paintName = true;
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = null;
            try
            {
                @char = (Char)GameScr.vCharInMap.elementAt(i);
            }
            catch (Exception)
            {
            }
            if (@char != null && !@char.Equals(this) && ((@char.cy == cy && Res.abs(@char.cx - cx) < 35) || (cy - @char.cy < 32 && cy - @char.cy > 0 && Res.abs(@char.cx - cx) < 24)))
            {
                paintName = false;
            }
        }
        for (int j = 0; j < GameScr.vNpc.size(); j++)
        {
            Npc npc = null;
            try
            {
                npc = (Npc)GameScr.vNpc.elementAt(j);
            }
            catch (Exception)
            {
            }
            if (npc != null && npc.cy == cy && Res.abs(npc.cx - cx) < 24)
            {
                paintName = false;
            }
        }
    }

    private void updateMobMe()
    {
        if (tMobMeBorn != 0)
        {
            tMobMeBorn--;
        }
        if (tMobMeBorn == 0)
        {
            mobMe.xFirst = ((cdir != 1) ? (cx + 30) : (cx - 30));
            mobMe.yFirst = cy - 60;
            int num = mobMe.xFirst - mobMe.x;
            int num2 = mobMe.yFirst - mobMe.y;
            mobMe.x += num / 4;
            mobMe.y += num2 / 4;
            mobMe.dir = cdir;
        }
    }
}
