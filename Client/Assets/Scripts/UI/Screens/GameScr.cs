using System;
using System.Threading;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr : mScreen, IChatable
{
    // All variable and constant declarations extracted to GameScr.Fields.cs


    public GameScr()
    {
        if (GameCanvas.w == 128 || GameCanvas.h <= 208)
        {
            indexSize = 20;
        }
        cmdback = new Command(string.Empty, 11021);
        cmdMenu = new Command("menu", 11000);
        cmdFocus = new Command(string.Empty, 11001);
        cmdMenu.img = imgMenu;
        cmdMenu.w = mGraphics.getImageWidth(cmdMenu.img) + 20;
        cmdMenu.isPlaySoundButton = false;
        cmdFocus.img = imgFocus;
        if (GameCanvas.isTouch)
        {
            cmdMenu.x = 0;
            cmdMenu.y = 50;
            cmdFocus = null;
        }
        else
        {
            cmdMenu.x = 0;
            cmdMenu.y = gH - 30;
            cmdFocus.x = gW - 32;
            cmdFocus.y = gH - 32;
        }
        right = cmdFocus;
        isPaintRada = 1;
        if (GameCanvas.isTouch)
        {
            isHaveSelectSkill = true;
        }
    }
    // dataInit_readers extracted to GameScr.Data.cs

    // clan_playerMenu extracted to GameScr.Actions.cs

    // attackValidation extracted to GameScr.Skills.cs

    // keyboardInput extracted to GameScr.Input.cs

    // isVsMap extracted to GameScr.Camera.cs

    // drag_click_tapTargets extracted to GameScr.Touch.cs

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

    // updateKeyChatPopUp extracted to GameScr.Input.cs

    // isRongThanMenu extracted to GameScr.Camera.cs

    public void paintEffect(mGraphics g)
    {
        for (int i = 0; i < Effect2.vEffect2.size(); i++)
        {
            Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
            if (effect != null && !(effect is ChatPopup))
            {
                effect.paint(g);
            }
        }
        if (!GameCanvas.lowGraphic)
        {
            for (int i = 0; i < Effect2.vAnimateEffect.size(); i++)
            {
                Effect2 effect2 = (Effect2)Effect2.vAnimateEffect.elementAt(i);
                effect2.paint(g);
            }
        }
        for (int i = 0; i < Effect2.vEffect2Outside.size(); i++)
        {
            Effect2 effect3 = (Effect2)Effect2.vEffect2Outside.elementAt(i);
            effect3.paint(g);
        }
    }

    // paintSky extracted to GameScr.Camera.cs

    // paintCapcha extracted to GameScr.Popups.cs

    public override void paint(mGraphics g)
    {
        countEff = 0;
        if (!isPaint)
        {
            return;
        }
        if (QuayTamBao.isTamBao)
        {
            QuayTamBao.paint(g);
            return;
        }
        if (isFreez || (isUseFreez && ChatPopup.currChatPopup == null))
        {
            dem++;
            if ((dem < 30 && dem >= 0 && GameCanvas.gameTick % 4 == 0) || (dem >= 30 && dem <= 50 && GameCanvas.gameTick % 3 == 0) || dem > 50)
            {
                g.setColor(16777215);
                g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
                if (dem <= 50)
                {
                    return;
                }
                if (isUseFreez)
                {
                    isUseFreez = false;
                    dem = 0;
                    if (activeRongThan)
                    {
                        callRongThan(xR, yR);
                    }
                    else
                    {
                        hideRongThan();
                    }
                }
                paintInfoBar(g);
                g.translate(-cmx, -cmy);
                g.translate(0, GameCanvas.transY);
                Char.myCharz().paint(g);
                mSystem.paintFlyText(g);
                resetTranslate(g);
                paintSelectedSkill(g);
                return;
            }
        }
        GameCanvas.paintBGGameScr(g);
        paint_ios_bg(g);
        if ((isRongThanXuatHien || isFireWorks) && TileMap.bgID != 3)
        {
            paintBlackSky(g);
        }
        GameCanvas.debug("PA3", 1);
        if (shock_scr > 0)
        {
            g.translate(-cmx + shock_x[shock_scr % shock_x.Length], -cmy + shock_y[shock_scr % shock_y.Length]);
            shock_scr--;
        }
        else
        {
            g.translate(-cmx, -cmy);
        }
        if (isSuperPower)
        {
            int tx = ((GameCanvas.gameTick % 3 != 0) ? (-3) : 3);
            g.translate(tx, 0);
        }
        BackgroudEffect.paintBehindTileAll(g);
        EffecMn.paintLayer1(g);
        TileMap.paintTilemap(g);
        TileMap.paintOutTilemap(g);
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char @char = (Char)vCharInMap.elementAt(i);
            if (@char.isMabuHold && TileMap.mapID == 128)
            {
                @char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
            }
        }
        if (Char.myCharz().isMabuHold && TileMap.mapID == 128)
        {
            Char.myCharz().paintHeadWithXY(g, Char.myCharz().cx, Char.myCharz().cy, 0);
        }
        paintBgItem(g, 2);
        if (Char.myCharz().cmdMenu != null && GameCanvas.isTouch)
        {
            if (keyTouch == 20)
            {
                g.drawImage(imgChat2, Char.myCharz().cmdMenu.x + cmx, Char.myCharz().cmdMenu.y + cmy, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            else
            {
                g.drawImage(imgChat, Char.myCharz().cmdMenu.x + cmx, Char.myCharz().cmdMenu.y + cmy, mGraphics.HCENTER | mGraphics.VCENTER);
            }
        }
        GameCanvas.debug("PA4", 1);
        GameCanvas.debug("PA5", 1);
        BackgroudEffect.paintBackAll(g);
        EffectManager.lowEffects.paintAll(g);
        for (int i = 0; i < Effect2.vEffectFeet.size(); i++)
        {
            Effect2 effect = (Effect2)Effect2.vEffectFeet.elementAt(i);
            effect.paint(g);
        }
        for (int i = 0; i < Teleport.vTeleport.size(); i++)
        {
            ((Teleport)Teleport.vTeleport.elementAt(i)).paintHole(g);
        }
        for (int i = 0; i < vNpc.size(); i++)
        {
            Npc npc = (Npc)vNpc.elementAt(i);
            if (npc.cHP > 0)
            {
                npc.paintShadow(g);
            }
        }
        for (int i = 0; i < vNpc.size(); i++)
        {
            ((Npc)vNpc.elementAt(i)).paint(g);
        }
        g.translate(0, GameCanvas.transY);
        GameCanvas.debug("PA7", 1);
        GameCanvas.debug("PA8", 1);
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char char2 = null;
            try
            {
                char2 = (Char)vCharInMap.elementAt(i);
            }
            catch (Exception ex)
            {
                Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
            }
            if (char2 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char2.isShadown)
            {
                char2.paintShadow(g);
            }
        }
        Char.myCharz().paintShadow(g);
        EffecMn.paintLayer2(g);
        for (int i = 0; i < vMob.size(); i++)
        {
            ((Mob)vMob.elementAt(i)).paint(g);
        }
        for (int i = 0; i < Teleport.vTeleport.size(); i++)
        {
            ((Teleport)Teleport.vTeleport.elementAt(i)).paint(g);
        }
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char char3 = null;
            try
            {
                char3 = (Char)vCharInMap.elementAt(i);
            }
            catch (Exception)
            {
            }
            if (char3 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
            {
                char3.paint(g);
            }
        }
        Char.myCharz().paint(g);
        if (Char.myCharz().skillPaint != null && Char.myCharz().skillInfoPaint() != null && Char.myCharz().indexSkill < Char.myCharz().skillInfoPaint().Length)
        {
            Char.myCharz().paintCharWithSkill(g);
            Char.myCharz().paintMount2(g);
        }
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char char4 = null;
            try
            {
                char4 = (Char)vCharInMap.elementAt(i);
            }
            catch (Exception ex3)
            {
                Cout.LogError("Loi ham paint char gamescr: " + ex3.ToString());
            }
            if (char4 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length)
            {
                char4.paintCharWithSkill(g);
                char4.paintMount2(g);
            }
        }
        for (int i = 0; i < vItemMap.size(); i++)
        {
            ((ItemMap)vItemMap.elementAt(i)).paint(g);
        }
        g.translate(0, -GameCanvas.transY);
        GameCanvas.debug("PA9", 1);
        paintSplash(g);
        GameCanvas.debug("PA10", 1);
        GameCanvas.debug("PA11", 1);
        GameCanvas.debug("PA13", 1);
        paintEffect(g);
        paintBgItem(g, 3);
        for (int i = 0; i < vNpc.size(); i++)
        {
            Npc npc2 = (Npc)vNpc.elementAt(i);
            npc2.paintName(g);
        }
        EffecMn.paintLayer3(g);
        for (int i = 0; i < vNpc.size(); i++)
        {
            Npc npc3 = (Npc)vNpc.elementAt(i);
            if (npc3.chatInfo != null)
            {
                npc3?.chatInfo.paint(g, npc3.cx, npc3.cy - npc3.ch - GameCanvas.transY, npc3.cdir);
            }
        }
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char char5 = null;
            try
            {
                char5 = (Char)vCharInMap.elementAt(i);
            }
            catch (Exception)
            {
            }
            if (char5 != null && char5.chatInfo != null)
            {
                char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
            }
        }
        if (Char.myCharz().chatInfo != null)
        {
            Char.myCharz().chatInfo.paint(g, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, Char.myCharz().cdir);
        }
        EffectManager.mid_2Effects.paintAll(g);
        EffectManager.midEffects.paintAll(g);
        BackgroudEffect.paintFrontAll(g);
        for (int j = 0; j < TileMap.vCurrItem.size(); j++)
        {
            BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(j);
            if (bgItem.idImage != -1 && bgItem.layer > 3)
            {
                bgItem.paint(g);
            }
        }
        PopUp.paintAll(g);
        if (TileMap.mapID == 120)
        {
            if (percentMabu != 100)
            {
                int w = percentMabu * mGraphics.getImageWidth(imgHPLost) / 100;
                int num = percentMabu;
                g.drawImage(imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(imgHPLost) / 2, 220, 0);
                g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(imgHPLost) / 2, 220, w, 10);
                g.drawImage(imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(imgHPLost) / 2, 220, 0);
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            }
            if (mabuEff)
            {
                tMabuEff++;
                if (GameCanvas.gameTick % 3 == 0)
                {
                    Effect me = new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1);
                    EffecMn.addEff(me);
                }
                if (GameCanvas.gameTick % 15 == 0)
                {
                    Effect me2 = new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1);
                    EffecMn.addEff(me2);
                }
                if (tMabuEff == 100)
                {
                    activeSuperPower(TileMap.pxw / 2, 300);
                }
                if (tMabuEff == 110)
                {
                    tMabuEff = 0;
                    mabuEff = false;
                }
            }
        }
        BackgroudEffect.paintFog(g);
        bool flag = true;
        for (int i = 0; i < BackgroudEffect.vBgEffect.size(); i++)
        {
            BackgroudEffect backgroudEffect = (BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(i);
            if (backgroudEffect.typeEff == 0)
            {
                flag = false;
                break;
            }
        }
        if (mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4)
        {
            flag = false;
        }
        if (flag && !isRongThanXuatHien)
        {
            int num2 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
            if (num2 <= 0)
            {
                num2 = 1;
            }
            if (TileMap.tileID != 28)
            {
                for (int i = 0; i < num2; i++)
                {
                    int num3 = 100 + i * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - cmx / 2;
                    int num4 = -20;
                    int imageWidth = mGraphics.getImageWidth(TileMap.imgLight);
                    if (num3 + imageWidth >= cmx && num3 <= cmx + GameCanvas.w && num4 + mGraphics.getImageHeight(TileMap.imgLight) >= cmy && num4 <= cmy + GameCanvas.h)
                    {
                        g.drawImage(TileMap.imgLight, 100 + i * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - cmx / 2, num4, 0);
                    }
                }
            }
        }
        mSystem.paintFlyText(g);
        GameCanvas.debug("PA14", 1);
        GameCanvas.debug("PA15", 1);
        GameCanvas.debug("PA16", 1);
        paintArrowPointToNPC(g);
        GameCanvas.debug("PA17", 1);
        if (!isPaintOther && !GameCanvas.panel.isShow)
        {
            try
            {
                paintInfoBar(g);
            }
            catch (Exception ex)
            {
                Cout.LogError("Loi paintInfoBar: " + ex.ToString());
            }
        }
        resetTranslate(g);
        paint_xp_bar(g);
        if (!isPaintOther)
        {
            ModFunc.GI().Paint(g);
            g.translate(-g.getTranslateX(), -g.getTranslateY());
            if ((TileMap.mapID == 128 || TileMap.mapID == 127) && mabuPercent != 0)
            {
                int num5 = 30;
                int num6 = 200;
                g.setColor(0);
                g.fillRect(num5 - 27, num6 - 112, 54, 8);
                g.setColor(16711680);
                g.setClip(num5 - 25, num6 - 110, mabuPercent, 4);
                g.fillRect(num5 - 25, num6 - 110, 50, 4);
                g.setClip(0, 0, 3000, 3000);
                mFont.tahoma_7b_white.drawString(g, "Mabu", num5, num6 - 112 + 10, 2, mFont.tahoma_7b_dark);
            }
            if (Char.myCharz().isFusion)
            {
                Char.myCharz().tFusion++;
                if (GameCanvas.gameTick % 3 == 0)
                {
                    g.setColor(16777215);
                    g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
                }
                if (Char.myCharz().tFusion >= 100)
                {
                    Char.myCharz().fusionComplete();
                }
            }
            for (int i = 0; i < vCharInMap.size(); i++)
            {
                Char char6 = null;
                try
                {
                    char6 = (Char)vCharInMap.elementAt(i);
                }
                catch (Exception)
                {
                }
                if (char6 != null && char6.isFusion && Char.isCharInScreen(char6))
                {
                    char6.tFusion++;
                    if (GameCanvas.gameTick % 3 == 0)
                    {
                        g.setColor(16777215);
                        g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
                    }
                    if (char6.tFusion >= 100)
                    {
                        char6.fusionComplete();
                    }
                }
            }
            GameCanvas.paintz.paintTabSoft(g);
            GameCanvas.debug("PA19", 1);
            GameCanvas.debug("PA20", 1);
            resetTranslate(g);
            paintSelectedSkill(g);
            GameCanvas.debug("PA22", 1);
            resetTranslate(g);
            if (GameCanvas.isTouch && GameCanvas.isTouchControl)
            {
                paintTouchControl(g);
            }
            resetTranslate(g);
            paintChatVip(g);
            if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(instance))
            {
                base.paint(g);
                if (keyMouse == 1 && cmdMenu != null)
                {
                    g.drawImage(ItemMap.imageFlare, cmdMenu.x + 7, cmdMenu.y + 15, 3);
                }
            }
            resetTranslate(g);
            int num7 = 100 + ((Char.vItemTime.size() != 0) ? (textTime.size() * 12) : 0);
            if (Char.myCharz().clan != null)
            {
                int num8 = 0;
                int num9 = 0;
                int num10 = (GameCanvas.h - 100 - 60) / 12;
                for (int i = 0; i < vCharInMap.size(); i++)
                {
                    Char char7 = (Char)vCharInMap.elementAt(i);
                    if (char7.clanID == -1 || char7.clanID != Char.myCharz().clan.ID)
                    {
                        continue;
                    }
                    if (char7.isOutX() && char7.cx < Char.myCharz().cx)
                    {
                        int num11 = num10;
                        if (Char.vItemTime.size() != 0)
                        {
                            num11 -= textTime.size();
                        }
                        if (num8 <= num11)
                        {
                            mFont.tahoma_7_green.drawString(g, char7.cName, 20, num7 - 12 + num8 * 12, mFont.LEFT, mFont.tahoma_7_grey);
                            char7.paintHp(g, 10, num7 + num8 * 12 - 5);
                            num8++;
                        }
                    }
                    else if (char7.isOutX() && char7.cx > Char.myCharz().cx && num9 <= num10)
                    {
                        mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num7 - 12 + num9 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
                        char7.paintHp(g, GameCanvas.w - 15, num7 + num9 * 12 - 5);
                        num9++;
                    }
                }
            }
            ChatTextField.gI().paint(g);
            NewBagUI.GI().Paint(g);
            if (isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0)
            {
                g.drawImage(ItemMap.imageFlare, cmdMenu.x + 15, cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
            }
            if (isSuperPower)
            {
                dxPower += 5;
                if (tPower >= 0)
                {
                    tPower += dxPower;
                }
                Res.outz("x power= " + xPower);
                if (tPower < 0)
                {
                    tPower--;
                    if (tPower == -20)
                    {
                        isSuperPower = false;
                        tPower = 0;
                        dxPower = 0;
                    }
                }
                else if ((xPower - tPower > 0 || tPower < TileMap.pxw) && tPower > 0)
                {
                    g.setColor(16777215);
                    if (!GameCanvas.lowGraphic)
                    {
                        g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
                    }
                    else
                    {
                        g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
                    }
                }
                else
                {
                    tPower = -1;
                }
            }
            for (int i = 0; i < Char.vItemTime.size(); i++)
            {
                ((ItemTime)Char.vItemTime.elementAt(i)).paint(g, cmdMenu.x + 32 + i * 24, 55);
            }
            for (int i = 0; i < textTime.size(); i++)
            {
                ((ItemTime)textTime.elementAt(i)).paintText(g, cmdMenu.x + ((Char.vItemTime.size() == 0) ? 25 : 5), ((Char.vItemTime.size() == 0) ? 45 : 90) + i * 12);
            }
            paintXoSo(g);
            g.drawImageScale(QuayTamBao.quay, GameCanvas.w - 100, 0, 45, 45, 0);
            if (mResources.language == 1)
            {
                long second = mSystem.currentTimeMillis() - deltaTime;
                mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(second), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
            }
            if (!yourNumber.Equals(string.Empty))
            {
                for (int i = 0; i < strPaint.Length; i++)
                {
                    mFont.tahoma_7b_white.drawString(g, strPaint[i], 5, 85 + i * 18, 0, mFont.tahoma_7b_dark);
                }
            }
        }
        int num12 = 0;
        int num13 = GameCanvas.hw;
        if (num13 > 200)
        {
            num13 = 200;
        }
        paintPhuBanBar(g, num12 + GameCanvas.w / 2, 0, num13);
        EffectManager.hiEffects.paintAll(g);
    }

    // popups_chatVip extracted to GameScr.Popups.cs

    // phuBan_hpBars extracted to GameScr.HUD.cs

    // effectEnd_screenChecks extracted to GameScr.Data.cs

    // paint_xp_bar extracted to GameScr.HUD.cs

    // paint_ios_bg extracted to GameScr.Camera.cs
}
