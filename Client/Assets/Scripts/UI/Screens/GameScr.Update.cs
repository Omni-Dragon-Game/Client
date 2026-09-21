using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    private void checkAuto()
    {
        long num = mSystem.currentTimeMillis();
        if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3])
        {
            auto = 0;
            isAutoPlay = false;
        }
        if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !isPaintPopup())
        {
            if (auto == 0)
            {
                if (num - lastFire < 800 && checkSkillValid2() && (Char.myCharz().mobFocus != null || (Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))))
                {
                    Res.outz("toi day");
                    auto = 10;
                    GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
                }
            }
            else
            {
                auto = 0;
                GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
            }
            lastFire = num;
        }
        if (GameCanvas.gameTick % 5 == 0 && auto > 0 && Char.myCharz().currentMovePoint == null)
        {
            if (Char.myCharz().myskill != null && (Char.myCharz().myskill.template.isUseAlone() || Char.myCharz().myskill.paintCanNotUseSkill))
            {
                return;
            }
            if ((Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.status != 1 && Char.myCharz().mobFocus.status != 0 && Char.myCharz().charFocus == null) || (Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus)))
            {
                if (Char.myCharz().myskill.paintCanNotUseSkill)
                {
                    return;
                }
                doFire(isFireByShortCut: false, skipWaypoint: true);
            }
        }
        if (auto > 1)
        {
            auto--;
        }
    }

    public void doUseHP()
    {
        if (Char.myCharz().stone || Char.myCharz().blindEff || Char.myCharz().holdEffID > 0)
        {
            return;
        }
        long num = mSystem.currentTimeMillis();
        if (num - lastUsePotion >= 10000)
        {
            if (!Char.myCharz().doUsePotion())
            {
                info1.addInfo(mResources.HP_EMPTY, 0);
                return;
            }
            ServerEffect.addServerEffect(11, Char.myCharz(), 5);
            ServerEffect.addServerEffect(104, Char.myCharz(), 4);
            lastUsePotion = num;
            SoundMn.gI().eatPeans();
        }
    }

    public void activeSuperPower(int x, int y)
    {
        if (!isSuperPower)
        {
            SoundMn.gI().bigeExlode();
            isSuperPower = true;
            tPower = 0;
            dxPower = 0;
            xPower = x - cmx;
            yPower = y - cmy;
        }
    }

    // rongThan extracted to GameScr.Camera.cs

    private void autoPlay()
    {
        if (timeSkill > 0)
        {
            timeSkill--;
        }
        if (!canAutoPlay || isChangeZone || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5 || Char.myCharz().isCharge || Char.myCharz().isFlyAndCharge || Char.myCharz().isUseChargeSkill())
        {
            return;
        }
        bool flag = false;
        for (int i = 0; i < vMob.size(); i++)
        {
            Mob mob = (Mob)vMob.elementAt(i);
            if (mob.status != 0 && mob.status != 1)
            {
                flag = true;
            }
        }
        if (!flag)
        {
            return;
        }
        bool flag2 = false;
        for (int j = 0; j < Char.myCharz().arrItemBag.Length; j++)
        {
            Item item = Char.myCharz().arrItemBag[j];
            if (item != null && item.template.type == 6)
            {
                flag2 = true;
                break;
            }
        }
        if (!flag2 && GameCanvas.gameTick % 150 == 0)
        {
            Service.gI().requestPean();
        }
        if (Char.myCharz().cHP <= Char.myCharz().cHPFull * 20 / 100 || Char.myCharz().cMP <= Char.myCharz().cMPFull * 20 / 100)
        {
            doUseHP();
        }
        if (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.isMobMe))
        {
            for (int k = 0; k < vMob.size(); k++)
            {
                Mob mob2 = (Mob)vMob.elementAt(k);
                if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe)
                {
                    Char.myCharz().cx = mob2.x;
                    Char.myCharz().cy = mob2.y;
                    Char.myCharz().mobFocus = mob2;
                    Service.gI().charMove();
                    break;
                }
            }
        }
        else if (Char.myCharz().mobFocus.hp <= 0 || Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0)
        {
            Char.myCharz().mobFocus = null;
        }
        if (Char.myCharz().mobFocus == null || timeSkill != 0 || (Char.myCharz().skillInfoPaint() != null && Char.myCharz().indexSkill < Char.myCharz().skillInfoPaint().Length && Char.myCharz().dart != null && Char.myCharz().arr != null))
        {
            return;
        }
        Skill skill = null;
        if (GameCanvas.isTouch)
        {
            for (int l = 0; l < onScreenSkill.Length; l++)
            {
                if (onScreenSkill[l] == null || onScreenSkill[l].paintCanNotUseSkill || onScreenSkill[l].template.id == 10 || onScreenSkill[l].template.id == 11 || onScreenSkill[l].template.id == 14 || onScreenSkill[l].template.id == 23 || onScreenSkill[l].template.id == 7 || Char.myCharz().skillInfoPaint() != null || onScreenSkill[l].template.isSkillSpec())
                {
                    continue;
                }
                long num = ((onScreenSkill[l].template.manaUseType == 2) ? 1 : ((onScreenSkill[l].template.manaUseType == 1) ? (onScreenSkill[l].manaUse * Char.myCharz().cMPFull / 100) : onScreenSkill[l].manaUse));
                if (Char.myCharz().cMP >= num)
                {
                    if (skill == null)
                    {
                        skill = onScreenSkill[l];
                    }
                    else if (skill.coolDown < onScreenSkill[l].coolDown)
                    {
                        skill = onScreenSkill[l];
                    }
                }
            }
            if (skill != null)
            {
                doSelectSkill(skill, isShortcut: true);
                doDoubleClickToObj(Char.myCharz().mobFocus);
            }
            return;
        }
        for (int m = 0; m < keySkill.Length; m++)
        {
            if (keySkill[m] == null || keySkill[m].paintCanNotUseSkill || keySkill[m].template.id == 10 || keySkill[m].template.id == 11 || keySkill[m].template.id == 14 || keySkill[m].template.id == 23 || keySkill[m].template.id == 7 || Char.myCharz().skillInfoPaint() != null)
            {
                continue;
            }
            long num2 = (keySkill[m].template.manaUseType == 2) ? 1 : ((keySkill[m].template.manaUseType == 1) ? (keySkill[m].manaUse * Char.myCharz().cMPFull / 100) : keySkill[m].manaUse);
            if (Char.myCharz().cMP >= num2)
            {
                if (skill == null)
                {
                    skill = keySkill[m];
                }
                else if (skill.coolDown < keySkill[m].coolDown)
                {
                    skill = keySkill[m];
                }
            }
        }
        if (skill != null)
        {
            doSelectSkill(skill, isShortcut: true);
            doDoubleClickToObj(Char.myCharz().mobFocus);
        }
    }

    // doFire_useSkills extracted to GameScr.Skills.cs

    // open_xoso extracted to GameScr.Popups.cs

    public override void update()
    {
        if (ModFunc.GI().canUpdate)
        {
            AutoXmap.Update();
            ModFunc.GI().Update();
        }
        if (!AutoXmap.IsXmapRunning)
        {
            PickMob.Update();
        }
        if (GameCanvas.keyPressed[16])
        {
            GameCanvas.keyPressed[16] = false;
            Char.myCharz().findNextFocusByKey();
        }
        if (GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow)
        {
            GameCanvas.keyPressed[13] = false;
            Char.myCharz().findNextFocusByKey();
        }
        if (GameCanvas.keyPressed[17])
        {
            GameCanvas.keyPressed[17] = false;
            Char.myCharz().searchItem();
            if (Char.myCharz().itemFocus != null)
            {
                pickItem();
            }
        }
        if (GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137)
        {
            shock_scr = 30;
        }
        if (isAutoPlay && GameCanvas.gameTick % 20 == 0)
        {
            autoPlay();
        }
        updateXoSo();
        mSystem.checkAdComlete();
        SmallImage.update();
        try
        {
            if (LoginScr.isContinueToLogin)
            {
                LoginScr.isContinueToLogin = false;
            }
            if (tickMove == 1)
            {
                lastTick = mSystem.currentTimeMillis();
            }
            if (tickMove == 100)
            {
                tickMove = 0;
                currTick = mSystem.currentTimeMillis();
                int second = (int)(currTick - lastTick) / 1000;
                Service.gI().checkMMove(second);
            }
            if (lockTick > 0)
            {
                lockTick--;
                if (lockTick == 0)
                {
                    Controller.isStopReadMessage = false;
                }
            }
            checkCharFocus();
            GameCanvas.debug("E1", 0);
            updateCamera();
            GameCanvas.debug("E2", 0);
            ChatTextField.gI().update();
            GameCanvas.debug("E3", 0);
            for (int i = 0; i < vCharInMap.size(); i++)
            {
                ((Char)vCharInMap.elementAt(i)).update();
            }
            for (int i = 0; i < Teleport.vTeleport.size(); i++)
            {
                ((Teleport)Teleport.vTeleport.elementAt(i)).update();
            }
            Char.myCharz().update();
            if (Char.myCharz().statusMe == 1)
            {
            }
            if (popUpYesNo != null)
            {
                popUpYesNo.update();
            }
            EffecMn.update();
            GameCanvas.debug("E5x", 0);
            for (int i = 0; i < vMob.size(); i++)
            {
                ((Mob)vMob.elementAt(i)).update();
            }
            GameCanvas.debug("E6", 0);
            for (int i = 0; i < vNpc.size(); i++)
            {
                ((Npc)vNpc.elementAt(i)).update();
            }
            nSkill = onScreenSkill.Length;
            for (int i = onScreenSkill.Length - 1; i >= 0; i--)
            {
                Skill skill = onScreenSkill[i];
                if (skill != null)
                {
                    nSkill = i + 1;
                    break;
                }
                nSkill--;
            }
            if (nSkill == 1 && GameCanvas.isTouch)
            {
                xSkill = -200;
            }
            else if (xSkill < 0)
            {
                setSkillBarPosition();
            }
            GameCanvas.debug("E7", 0);
            GameCanvas.gI().updateDust();
            GameCanvas.debug("E8", 0);
            updateFlyText();
            PopUp.updateAll();
            updateSplash();
            updateSS();
            GameCanvas.updateBG();
            GameCanvas.debug("E9", 0);
            updateClickToArrow();
            GameCanvas.debug("E10", 0);
            for (int i = 0; i < vItemMap.size(); i++)
            {
                ((ItemMap)vItemMap.elementAt(i)).update();
            }
            GameCanvas.debug("E11", 0);
            GameCanvas.debug("E13", 0);
            for (int i = Effect2.vRemoveEffect2.size() - 1; i >= 0; i--)
            {
                Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(i));
                Effect2.vRemoveEffect2.removeElementAt(i);
            }
            for (int i = 0; i < Effect2.vEffect2.size(); i++)
            {
                Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
                effect.update();
            }
            for (int i = 0; i < Effect2.vEffect2Outside.size(); i++)
            {
                Effect2 effect2 = (Effect2)Effect2.vEffect2Outside.elementAt(i);
                effect2.update();
            }
            for (int i = 0; i < Effect2.vAnimateEffect.size(); i++)
            {
                Effect2 effect3 = (Effect2)Effect2.vAnimateEffect.elementAt(i);
                effect3.update();
            }
            for (int i = 0; i < Effect2.vEffectFeet.size(); i++)
            {
                Effect2 effect4 = (Effect2)Effect2.vEffectFeet.elementAt(i);
                effect4.update();
            }
            for (int i = 0; i < Effect2.vEffect3.size(); i++)
            {
                Effect2 effect5 = (Effect2)Effect2.vEffect3.elementAt(i);
                effect5.update();
            }
            BackgroudEffect.updateEff();
            info1.update();
            info2.update();
            GameCanvas.debug("E15", 0);
            if (currentCharViewInfo != null && !currentCharViewInfo.Equals(Char.myCharz()))
            {
                currentCharViewInfo.update();
            }
            runArrow++;
            if (runArrow > 3)
            {
                runArrow = 0;
            }
            if (isInjureHp)
            {
                twHp++;
                if (twHp == 20)
                {
                    twHp = 0;
                    isInjureHp = false;
                }
            }
            else if (dHP > Char.myCharz().cHP)
            {
                long num = dHP - Char.myCharz().cHP >> 1;
                if (num < 1)
                {
                    num = 1;
                }
                dHP -= num;
            }
            else
            {
                dHP = Char.myCharz().cHP;
            }
            if (isInjureMp)
            {
                twMp++;
                if (twMp == 20)
                {
                    twMp = 0;
                    isInjureMp = false;
                }
            }
            else if (dMP > Char.myCharz().cMP)
            {
                long num2 = dMP - Char.myCharz().cMP >> 1;
                if (num2 < 1)
                {
                    num2 = 1;
                }
                dMP -= num2;
            }
            else
            {
                dMP = Char.myCharz().cMP;
            }
            if (tMenuDelay > 0)
            {
                tMenuDelay--;
            }
            if (isRongThanMenu())
            {
                int num3 = 100;
                while (yR - num3 < cmy)
                {
                    cmy--;
                }
            }
            for (int i = 0; i < Char.vItemTime.size(); i++)
            {
                ((ItemTime)Char.vItemTime.elementAt(i)).update();
            }
            for (int i = 0; i < textTime.size(); i++)
            {
                ((ItemTime)textTime.elementAt(i)).update();
            }
            updateChatVip();
        }
        catch (Exception)
        {
        }
        int num4 = GameCanvas.gameTick % 4000;
        if (num4 == 1000)
        {
            checkRemoveImage();
        }
        EffectManager.update();
    }
}
