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
    public void RemoveAllItem()
    {
        foreach (var item in Char.myCharz().arrItemBag)
        {
            if (item != null)
            {
                Service.gI().useItem(1, 1, (sbyte)item.indexUI, -1);
                Service.gI().useItem(2, 1, (sbyte)item.indexUI, -1);
                Thread.Sleep(300);
            }
        } 
    }
    // loadBg extracted to GameScr.Camera.cs

    public void initSelectChar()
    {
        readPart();
        SmallImage.init();
        SmallImage.loadBigRMS();
    }

    public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
    {
        int clipX = g.getClipX();
        int clipY = g.getClipY();
        int clipWidth = g.getClipWidth();
        int clipHeight = g.getClipHeight();
        g.setClip((int)x, (int)y, (int)pixelPercent, 13);
        int num = size / 15 - 2;
        for (int i = 0; i < num; i++)
        {
            g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
        }
        g.drawImage(img0, x, y, 0);
        g.drawImage(img1, x + (float)size - 30f, y, 0);
        g.drawImage(img2, x + (float)size - 15f, y, 0);
        g.setClip(clipX, clipY, clipWidth, clipHeight);
    }

    public void initTraining()
    {
        if (CreateCharScr.isCreateChar)
        {
            CreateCharScr.isCreateChar = false;
            right = null;
        }
    }

    // mapChecks extracted to GameScr.Camera.cs

    public override void switchToMe()
    {
        if (ModFunc.autoLogin != null)
        {
            ModFunc.autoLogin.waitToNextLogin = false;
        }
        vChatVip.removeAllElements();
        ServerListScreen.isWait = false;
        if (BackgroudEffect.isHaveRain())
        {
            SoundMn.gI().rain();
        }
        LoginScr.isContinueToLogin = false;
        Char.isLoadingMap = false;
        if (!isPaintOther)
        {
            Service.gI().finishLoadMap();
        }
        if (TileMap.isTrainingMap())
        {
            initTraining();
        }
        info1.isUpdate = true;
        info2.isUpdate = true;
        resetButton();
        isLoadAllData = true;
        isPaintOther = false;
        base.switchToMe();
    }

    public static int getMaxExp(int level)
    {
        int num = 0;
        for (int i = 0; i <= level; i++)
        {
            num += (int)exps[i];
        }
        return num;
    }

    public static void resetAllvector()
    {
        vCharInMap.removeAllElements();
        Teleport.vTeleport.removeAllElements();
        vItemMap.removeAllElements();
        Effect2.vEffect2.removeAllElements();
        Effect2.vAnimateEffect.removeAllElements();
        Effect2.vEffect2Outside.removeAllElements();
        Effect2.vEffectFeet.removeAllElements();
        Effect2.vEffect3.removeAllElements();
        vMobAttack.removeAllElements();
        vMob.removeAllElements();
        vNpc.removeAllElements();
        Char.myCharz().vMovePoints.removeAllElements();
    }

    // shortcuts extracted to GameScr.Skills.cs

    public bool isBagFull()
    {
        for (int num = Char.myCharz().arrItemBag.Length - 1; num >= 0; num--)
        {
            if (Char.myCharz().arrItemBag[num] == null)
            {
                return false;
            }
        }
        return true;
    }

    // confirm_menu extracted to GameScr.Actions.cs

    public void readPart()
    {
        DataInputStream dataInputStream = null;
        try
        {
            dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
            int partSize = dataInputStream.readShort();
            parts = new Part[partSize];
            for (int i = 0; i < partSize; i++)
            {
                int type = dataInputStream.readByte();
                parts[i] = new Part(type);
                for (int j = 0; j < parts[i].pi.Length; j++)
                {
                    parts[i].pi[j] = new PartImage
                    {
                        id = dataInputStream.readShort(),
                        dx = dataInputStream.readByte(),
                        dy = dataInputStream.readByte()
                    };
                }
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("LOI TAI readPart " + ex.ToString());
        }
        finally
        {
            try
            {
                dataInputStream.close();
            }
            catch (Exception ex2)
            {
                Res.outz2("LOI TAI readPart 2" + ex2.StackTrace);
            }
        }
    }

    public void readEfect()
    {
        DataInputStream dataInputStream = null;
        try
        {
            dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
            int num = dataInputStream.readShort();
            efs = new EffectCharPaint[num];
            for (int i = 0; i < num; i++)
            {
                efs[i] = new EffectCharPaint();
                efs[i].idEf = dataInputStream.readShort();
                efs[i].arrEfInfo = new EffectInfoPaint[dataInputStream.readByte()];
                for (int j = 0; j < efs[i].arrEfInfo.Length; j++)
                {
                    efs[i].arrEfInfo[j] = new EffectInfoPaint();
                    efs[i].arrEfInfo[j].idImg = dataInputStream.readShort();
                    efs[i].arrEfInfo[j].dx = dataInputStream.readByte();
                    efs[i].arrEfInfo[j].dy = dataInputStream.readByte();
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            try
            {
                dataInputStream.close();
            }
            catch (Exception ex2)
            {
                Cout.LogError("Loi ham Eff: " + ex2.ToString());
            }
        }
    }

    public void readArrow()
    {
        DataInputStream dataInputStream = null;
        try
        {
            dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
            int num = dataInputStream.readShort();
            arrs = new Arrowpaint[num];
            for (int i = 0; i < num; i++)
            {
                arrs[i] = new Arrowpaint();
                arrs[i].id = dataInputStream.readShort();
                arrs[i].imgId[0] = dataInputStream.readShort();
                arrs[i].imgId[1] = dataInputStream.readShort();
                arrs[i].imgId[2] = dataInputStream.readShort();
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            try
            {
                dataInputStream.close();
            }
            catch (Exception ex2)
            {
                Cout.LogError("Loi ham readArrow: " + ex2.ToString());
            }
        }
    }

    public void readDart()
    {
        DataInputStream dataInputStream = null;
        try
        {
            dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
            int num = dataInputStream.readShort();
            darts = new DartInfo[num];
            for (int i = 0; i < num; i++)
            {
                darts[i] = new DartInfo();
                darts[i].id = dataInputStream.readShort();
                darts[i].nUpdate = dataInputStream.readShort();
                darts[i].va = dataInputStream.readShort() * 256;
                darts[i].xdPercent = dataInputStream.readShort();
                int num2 = dataInputStream.readShort();
                darts[i].tail = new short[num2];
                for (int j = 0; j < num2; j++)
                {
                    darts[i].tail[j] = dataInputStream.readShort();
                }
                num2 = dataInputStream.readShort();
                darts[i].tailBorder = new short[num2];
                for (int k = 0; k < num2; k++)
                {
                    darts[i].tailBorder[k] = dataInputStream.readShort();
                }
                num2 = dataInputStream.readShort();
                darts[i].xd1 = new short[num2];
                for (int l = 0; l < num2; l++)
                {
                    darts[i].xd1[l] = dataInputStream.readShort();
                }
                num2 = dataInputStream.readShort();
                darts[i].xd2 = new short[num2];
                for (int m = 0; m < num2; m++)
                {
                    darts[i].xd2[m] = dataInputStream.readShort();
                }
                num2 = dataInputStream.readShort();
                darts[i].head = new short[num2][];
                for (int n = 0; n < num2; n++)
                {
                    short num3 = dataInputStream.readShort();
                    darts[i].head[n] = new short[num3];
                    for (int num4 = 0; num4 < num3; num4++)
                    {
                        darts[i].head[n][num4] = dataInputStream.readShort();
                    }
                }
                num2 = dataInputStream.readShort();
                darts[i].headBorder = new short[num2][];
                for (int num5 = 0; num5 < num2; num5++)
                {
                    short num6 = dataInputStream.readShort();
                    darts[i].headBorder[num5] = new short[num6];
                    for (int num7 = 0; num7 < num6; num7++)
                    {
                        darts[i].headBorder[num5][num7] = dataInputStream.readShort();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi ham ReadDart: " + ex.ToString());
        }
        finally
        {
            try
            {
                dataInputStream.close();
            }
            catch (Exception ex2)
            {
                Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
            }
        }
    }

    public void readSkill()
    {
        DataInputStream dataInputStream = null;
        try
        {
            dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
            int dataSkillSz = dataInputStream.readShort();
            int skillSz = Skills.skills.size();

            sks = new SkillPaint[skillSz];
            for (int i = 0; i < dataSkillSz; i++)
            {
                short levelId = dataInputStream.readShort();
                if (levelId == 1111)
                {
                    levelId = (short)(dataSkillSz - 1);
                }
                sks[levelId] = new SkillPaint
                {
                    id = levelId,
                    effectHappenOnMob = dataInputStream.readShort()
                };
                if (sks[levelId].effectHappenOnMob <= 0)
                {
                    sks[levelId].effectHappenOnMob = 80;
                }
                sks[levelId].numEff = dataInputStream.readByte();
                sks[levelId].skillStand = new SkillInfoPaint[dataInputStream.readByte()];
                for (int j = 0; j < sks[levelId].skillStand.Length; j++)
                {
                    sks[levelId].skillStand[j] = new SkillInfoPaint
                    {
                        status = dataInputStream.readByte(),
                        effS0Id = dataInputStream.readShort(),
                        e0dx = dataInputStream.readShort(),
                        e0dy = dataInputStream.readShort(),
                        effS1Id = dataInputStream.readShort(),
                        e1dx = dataInputStream.readShort(),
                        e1dy = dataInputStream.readShort(),
                        effS2Id = dataInputStream.readShort(),
                        e2dx = dataInputStream.readShort(),
                        e2dy = dataInputStream.readShort(),
                        arrowId = dataInputStream.readShort(),
                        adx = dataInputStream.readShort(),
                        ady = dataInputStream.readShort()
                    };
                }
                sks[levelId].skillfly = new SkillInfoPaint[dataInputStream.readByte()];
                for (int k = 0; k < sks[levelId].skillfly.Length; k++)
                {
                    sks[levelId].skillfly[k] = new SkillInfoPaint
                    {
                        status = dataInputStream.readByte(),
                        effS0Id = dataInputStream.readShort(),
                        e0dx = dataInputStream.readShort(),
                        e0dy = dataInputStream.readShort(),
                        effS1Id = dataInputStream.readShort(),
                        e1dx = dataInputStream.readShort(),
                        e1dy = dataInputStream.readShort(),
                        effS2Id = dataInputStream.readShort(),
                        e2dx = dataInputStream.readShort(),
                        e2dy = dataInputStream.readShort(),
                        arrowId = dataInputStream.readShort(),
                        adx = dataInputStream.readShort(),
                        ady = dataInputStream.readShort()
                    };
                }
            }
        }
        catch (Exception ex)
        {
            ModFunc.Log("Loi ham readSkill: " + ex.ToString());
        }
        finally
        {
            try
            {
                dataInputStream.close();
            }
            catch (Exception ex2)
            {
                ModFunc.Log("Loi ham readskill 1: " + ex2.ToString());
            }
        }
    }

    public static GameScr gI()
    {
        if (instance == null)
        {
            instance = new GameScr();
        }
        return instance;
    }

    public static void clearGameScr()
    {
        instance = null;
    }

    public void loadGameScr()
    {
        loadSplash();
        Res.init();
        loadInforBar();
    }

    // menusInfor extracted to GameScr.Actions.cs

    // camera extracted to GameScr.Camera.cs

    public bool testAct()
    {
        for (sbyte b = 2; b < 9; b += 2)
        {
            if (GameCanvas.keyHold[b])
            {
                return false;
            }
        }
        return true;
    }

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

    public void updateOpen()
    {
        if (isstarOpen)
        {
            if (moveUp > -3)
            {
                moveUp -= 4;
            }
            else
            {
                moveUp = -2;
            }
            if (moveDow < GameCanvas.h + 3)
            {
                moveDow += 4;
            }
            else
            {
                moveDow = GameCanvas.h + 2;
            }
            if (moveUp <= -2 && moveDow >= GameCanvas.h + 2)
            {
                isstarOpen = false;
            }
        }
    }

    public void initCreateCommand()
    {
    }

    // checkCharFocus extracted to GameScr.Targeting.cs

    public void updateXoSo()
    {
        if (tShow == 0)
        {
            return;
        }
        currXS = mSystem.currentTimeMillis();
        if (currXS - lastXS > 1000)
        {
            lastXS = mSystem.currentTimeMillis();
            secondXS++;
        }
        if (secondXS > 20)
        {
            for (int i = 0; i < winnumber.Length; i++)
            {
                randomNumber[i] = winnumber[i];
            }
            tShow--;
            if (tShow == 0)
            {
                yourNumber = string.Empty;
                info1.addInfo(strFinish, 0);
                secondXS = 0;
            }
            return;
        }
        if (moveIndex > winnumber.Length - 1)
        {
            tShow--;
            if (tShow == 0)
            {
                yourNumber = string.Empty;
                info1.addInfo(strFinish, 0);
            }
            return;
        }
        if (moveIndex < randomNumber.Length)
        {
            if (tMove[moveIndex] == 15)
            {
                if (randomNumber[moveIndex] == winnumber[moveIndex] - 1)
                {
                    delayMove[moveIndex] = 10;
                }
                if (randomNumber[moveIndex] == winnumber[moveIndex])
                {
                    tMove[moveIndex] = -1;
                    moveIndex++;
                }
            }
            else if (GameCanvas.gameTick % 5 == 0)
            {
                tMove[moveIndex]++;
            }
        }
        for (int j = 0; j < winnumber.Length; j++)
        {
            if (tMove[j] == -1)
            {
                continue;
            }
            moveCount[j]++;
            if (moveCount[j] > tMove[j] + delayMove[j])
            {
                moveCount[j] = 0;
                randomNumber[j]++;
                if (randomNumber[j] >= 10)
                {
                    randomNumber[j] = 0;
                }
            }
        }
    }

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

    public void paintCapcha(mGraphics g)
    {
        MobCapcha.paint(g, Char.myCharz().cx, Char.myCharz().cy);
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if (GameCanvas.menu.showMenu || GameCanvas.panel.isShow || ChatPopup.currChatPopup != null || !GameCanvas.isTouch)
        {
            return;
        }
        for (int i = 0; i < strCapcha.Length; i++)
        {
            int x = (GameCanvas.w - strCapcha.Length * disXC) / 2 + i * disXC + disXC / 2;
            if (keyCapcha[i] == -1)
            {
                g.drawImage(imgNut, x, GameCanvas.h - 25, 3);
                mFont.tahoma_7b_dark.drawString(g, strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
            }
            else
            {
                g.drawImage(imgNutF, x, GameCanvas.h - 25, 3);
                mFont.tahoma_7b_green2.drawString(g, strCapcha[i] + string.Empty, x, GameCanvas.h - 30, 2);
            }
        }
    }

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

    private void paintXoSo(mGraphics g)
    {
        if (tShow != 0)
        {
            string text = string.Empty;
            for (int i = 0; i < winnumber.Length; i++)
            {
                text = text + randomNumber[i] + " ";
            }
            PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, isButton: false);
            mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
            mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
        }
    }

    // targetArrows_npcSearch extracted to GameScr.Targeting.cs

    public static void resetTranslate(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
    }

    private void paintTouchControl(mGraphics g)
    {
        if (isNotPaintTouchControl())
        {
            return;
        }
        resetTranslate(g);
        if (!TileMap.isOfflineMap() && !isVS())
        {
            if (keyTouch == 15 || keyMouse == 15)
            {
                g.drawImage(imgChat2, xC + 17, yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            else
            {
                g.drawImage(imgChat, xC + 17, yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
            }
        }
        if (isUseTouch)
        {
        }
    }

    public void paintImageBarRight(mGraphics g, Char c)
    {
        if (c == null) return;
        long hpFull = (c.cHPFull > 0) ? c.cHPFull : 1;
        long mpFull = (c.cMPFull > 0) ? c.cMPFull : 1;
        int num = (int)(c.cHP * hpBarW / hpFull);
        int num2 = (int)c.cMP * mpBarW;
        int num3 = (int)(dHP * hpBarW / hpFull);
        int num4 = (int)dMP * mpBarW;
        g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(imgPanel), 0, 95, 100);
        g.drawRegion(imgPanel, 0, 0, mGraphics.getImageWidth(imgPanel), mGraphics.getImageHeight(imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - hpBarW + hpBarW - num3), 5, num3, 10);
        g.drawImage(imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - hpBarW + hpBarW - num), 5, num, 10);
        g.drawImage(imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - mpBarW + hpBarW - num4), 20, num4, 6);
        g.drawImage(imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
        g.setClip((int)(GameCanvas.w / 2 + 60 - 83 - mpBarW + hpBarW - num2), 20, num2, 6);
        g.drawImage(imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
    }

    private void paintImageBar(mGraphics g, bool isLeft, Char c)
    {
        if (c != null)
        {
            long hpFull = (c.cHPFull > 0) ? c.cHPFull : 1;
            long mpFull = (c.cMPFull > 0) ? c.cMPFull : 1;
            int num;
            int num4;
            int num3;
            int num2;
            if (c.charID == Char.myCharz().charID)
            {
                num = (int)(dHP * hpBarW / hpFull);
                num2 = (int)(dMP * mpBarW / mpFull);
                num3 = (int)(c.cHP * hpBarW / hpFull);
                num4 = (int)(c.cMP * mpBarW / mpFull);
            }
            else
            {
                num = (int)(c.dHP * hpBarW / hpFull);
                num2 = c.perCentMp * mpBarW / 100;
                num3 = (int)(c.cHP * hpBarW / hpFull);
                num4 = c.perCentMp * mpBarW / 100;
            }
            if (Char.myCharz().secondPower > 0 && Char.myCharz().maxPowerPoint > 0)
            {
                int w = Char.myCharz().powerPoint * spBarW / Char.myCharz().maxPowerPoint;
                g.drawImage(imgPanel2, 58, 29, 0);
                g.setClip(83, 31, w, 10);
                g.drawImage(imgSP, 83, 31, 0);
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
                mFont.tahoma_7_white.drawString(g, Char.myCharz().strInfo + ":" + Char.myCharz().powerPoint + "/" + Char.myCharz().maxPowerPoint, 115, 29, 2);
            }
            if (c.charID != Char.myCharz().charID)
            {
                g.setClip(mGraphics.getImageWidth(imgPanel) - 95, 0, 95, 100);
            }
            g.drawImage(imgPanel, 0, 0, 0);
            if (isLeft)
            {
                g.setClip(83, 5, num, 10);
            }
            else
            {
                g.setClip((int)(83 + hpBarW - num), 5, num, 10);
            }
            g.drawImage(imgHPLost, 83, 5, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 5, num3, 10);
            }
            else
            {
                g.setClip((int)(83 + hpBarW - num3), 5, num3, 10);
            }
            g.drawImage(imgHP, 83, 5, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 20, num2, 6);
            }
            else
            {
                g.setClip(83 + mpBarW - num2, 20, num2, 6);
            }
            g.drawImage(imgMPLost, 83, 20, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (isLeft)
            {
                g.setClip(83, 20, num2, 6);
            }
            else
            {
                g.setClip(83 + mpBarW - num4, 20, num4, 6);
            }
            g.drawImage(imgMP, 83, 20, 0);
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (Char.myCharz().cMP == 0 && GameCanvas.gameTick % 10 > 5)
            {
                g.setClip(83, 20, 2, 6);
                g.drawImage(imgMPLost, 83, 20, 0);
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            }
        }
    }

    // injure_vsCombat extracted to GameScr.Targeting.cs

    private void paintSelectedSkill(mGraphics g)
    {
        if (mobCapcha != null)
        {
            paintCapcha(g);
        }
        else
        {
            if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || isPaintPopup() || GameCanvas.panel.isShow || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
            {
                return;
            }
            long num = mSystem.currentTimeMillis();
            long num2 = num - lastUsePotion;
            int num3 = 0;
            if (num2 < 10000)
            {
                num3 = (int)(num2 * 20 / 10000);
            }
            if (!GameCanvas.isTouch)
            {
                g.drawImage((keyTouch != 10) ? imgSkill : imgSkill2, xSkill + xHP - 1, yHP - 1, 0);
                SmallImage.drawSmallImage(g, 542, xSkill + xHP + 3, yHP + 3, 0, 0);
                mFont.number_gray.drawString(g, string.Empty + hpPotion, xSkill + xHP + 22, yHP + 15, 1);
                if (num2 < 10000)
                {
                    g.setColor(2721889);
                    num3 = (int)(num2 * 20 / 10000);
                    g.fillRect(xSkill + xHP + 3, yHP + 3 + num3, 20, 20 - num3);
                }
            }
            else if (Char.myCharz().statusMe != 14)
            {
                if (gamePad.isSmallGamePad)
                {
                    if (isAnalog != 1)
                    {
                        g.setColor(9670800);
                        g.fillRect(xHP + 9, yHP + 10, 22, 20);
                        g.setColor(16777215);
                        g.fillRect(xHP + 9, yHP + 10 + ((num3 != 0) ? (20 - num3) : 0), 22, (num3 == 0) ? 20 : num3);
                        g.drawImage((keyTouch != 10) ? imgHP1 : imgHP2, xHP, yHP, 0);
                        mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 15, 2);
                    }
                    else if (isAnalog == 1)
                    {
                        g.drawImage((keyTouch != 10) ? imgSkill : imgSkill2, xSkill + xHP - 1, yHP - 1, 0);
                        SmallImage.drawSmallImage(g, 542, xSkill + xHP + 3, yHP + 3, 0, 0);
                        mFont.number_gray.drawString(g, string.Empty + hpPotion, xSkill + xHP + 22, yHP + 13, 1);
                        if (num2 < 10000)
                        {
                            g.setColor(2721889);
                            num3 = (int)(num2 * 20 / 10000);
                            g.fillRect(xSkill + xHP + 3, yHP + 3 + num3, 20, 20 - num3);
                        }
                    }
                }
                else if (isAnalog != 1)
                {
                    g.setColor(9670800);
                    g.fillRect(xHP + 9, yHP + 10 - 6, 22, 20);
                    g.setColor(16777215);
                    g.fillRect(xHP + 9, yHP + 10 + ((num3 != 0) ? (20 - num3) : 0) - 6, 22, (num3 == 0) ? 20 : num3);
                    g.drawImage((keyTouch != 10) ? imgHP1 : imgHP2, xHP, yHP - 6, 0);
                    mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 15 - 6, 2);
                }
                else
                {
                    g.setColor(9670800);
                    g.fillRect(xHP + 10, yHP + 10, 20, 18);
                    g.setColor(16777215);
                    g.fillRect(xHP + 10, yHP + 16 + ((num3 != 0) ? (20 - num3) : 0) - 6, 20, (num3 == 0) ? 18 : num3);
                    g.drawImage((keyTouch != 10) ? imgHP3 : imgHP4, xHP + 20, yHP + 20 - 3, mGraphics.HCENTER | mGraphics.VCENTER);
                    mFont.tahoma_7_green2.drawString(g, string.Empty + hpPotion, xHP + 20, yHP + 11, 2);
                }
            }
            if (isHaveSelectSkill)
            {
                Skill[] array = Main.isPC ? keySkill : ((!GameCanvas.isTouch) ? keySkill : onScreenSkill);
                if (!GameCanvas.isTouch)
                {
                    g.setColor(11152401);
                    g.fillRect(xSkill + xHP + 2, yHP - 10 + 6, 20, 10);
                    mFont.tahoma_7_white.drawString(g, "*", xSkill + xHP + 12, yHP - 8 + 6, mFont.CENTER);
                }
                int num4 = Main.isPC ? array.Length : ((!GameCanvas.isTouch) ? array.Length : nSkill);
                for (int i = 0; i < num4; i++)
                {
                    Skill skill = array[i];
                    if (skill == null)
                    {
                        continue;
                    }
                    if (skill != Char.myCharz().myskill)
                    {
                        g.drawImage(imgSkill, xSkill + xS[i] - 1, yS[i] - 1, 0);
                    }
                    if (skill == Char.myCharz().myskill)
                    {
                        g.drawImage(imgSkill2, xSkill + xS[i] - 1, yS[i] - 1, 0);
                        if (GameCanvas.isTouch && !Main.isPC)
                        {
                            g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, xSkill + xS[i] + 8, yS[i] - 7, 0);
                        }
                    }
                    skill.paint(xSkill + xS[i] + 13, yS[i] + 13, g);
                    if ((i == selectedIndexSkill && !isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == keyTouchSkill)
                    {
                        g.drawImage(ItemMap.imageFlare, xSkill + xS[i] + 13, yS[i] + 14, 3);
                    }
                }
            }
            paintGamePad(g);
        }
    }

    public void paintOpen(mGraphics g)
    {
        if (isstarOpen)
        {
            g.translate(-g.getTranslateX(), -g.getTranslateY());
            g.fillRect(0, 0, GameCanvas.w, moveUp);
            g.setColor(10275899);
            g.fillRect(0, moveUp - 1, GameCanvas.w, 1);
            g.fillRect(0, moveDow + 1, GameCanvas.w, 1);
        }
    }

    public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
    {
        int num = -1;
        for (int i = 0; i < 5; i++)
        {
            if (flyTextState[i] == -1)
            {
                num = i;
                break;
            }
        }
        if (num == -1)
        {
            return;
        }
        flyTextColor[num] = color;
        flyTextString[num] = flyString;
        flyTextX[num] = x;
        flyTextY[num] = y;
        flyTextDx[num] = dx;
        flyTextDy[num] = ((dy >= 0) ? 5 : (-5));
        flyTextState[num] = 0;
        flyTime[num] = 0;
        flyTextYTo[num] = 10;
        for (int j = 0; j < 5; j++)
        {
            if (flyTextState[j] != -1 && num != j && flyTextDy[num] < 0 && Res.abs(flyTextX[num] - flyTextX[j]) <= 20 && flyTextYTo[num] == flyTextYTo[j])
            {
                flyTextYTo[num] += 10;
            }
        }
    }

    public static void updateFlyText()
    {
        for (int i = 0; i < 5; i++)
        {
            if (flyTextState[i] == -1)
            {
                continue;
            }
            if (flyTextState[i] > flyTextYTo[i])
            {
                flyTime[i]++;
                if (flyTime[i] == 25)
                {
                    flyTime[i] = 0;
                    flyTextState[i] = -1;
                    flyTextYTo[i] = 0;
                    flyTextDx[i] = 0;
                    flyTextX[i] = 0;
                }
            }
            else
            {
                flyTextState[i] += Res.abs(flyTextDy[i]);
                flyTextX[i] += flyTextDx[i];
                flyTextY[i] += flyTextDy[i];
            }
        }
    }

    public static void loadSplash()
    {
        if (imgSplash == null)
        {
            imgSplash = new Image[3];
            for (int i = 0; i < 3; i++)
            {
                imgSplash[i] = GameCanvas.loadImage("/e/sp" + i + ".png");
            }
        }
        splashX = new int[2];
        splashY = new int[2];
        splashState = new int[2];
        splashF = new int[2];
        splashDir = new int[2];
        splashState[0] = (splashState[1] = -1);
    }

    public static bool startSplash(int x, int y, int dir)
    {
        int num = ((splashState[0] != -1) ? 1 : 0);
        if (splashState[num] != -1)
        {
            return false;
        }
        splashState[num] = 0;
        splashDir[num] = dir;
        splashX[num] = x;
        splashY[num] = y;
        return true;
    }

    public static void updateSplash()
    {
        for (int i = 0; i < 2; i++)
        {
            if (splashState[i] != -1)
            {
                splashState[i]++;
                splashX[i] += splashDir[i] << 2;
                splashY[i]--;
                if (splashState[i] >= 6)
                {
                    splashState[i] = -1;
                }
                else
                {
                    splashF[i] = (splashState[i] >> 1) % 3;
                }
            }
        }
    }

    public static void paintSplash(mGraphics g)
    {
        for (int i = 0; i < 2; i++)
        {
            if (splashState[i] != -1)
            {
                if (splashDir[i] == 1)
                {
                    g.drawImage(imgSplash[splashF[i]], splashX[i], splashY[i], 3);
                }
                else
                {
                    g.drawRegion(imgSplash[splashF[i]], 0, 0, mGraphics.getImageWidth(imgSplash[splashF[i]]), mGraphics.getImageHeight(imgSplash[splashF[i]]), 2, splashX[i], splashY[i], 3);
                }
            }
        }
    }

    private void loadInforBar()
    {
        imgScrW = 84;
        hpBarW = 66L;
        mpBarW = 59;
        hpBarX = 52;
        hpBarY = 10;
        spBarW = 61;
        expBarW = gW - 61;
    }

    public void updateSS()
    {
        if (indexMenu != -1)
        {
            if (cmySK != cmtoYSK)
            {
                cmvySK = cmtoYSK - cmySK << 2;
                cmdySK += cmvySK;
                cmySK += cmdySK >> 4;
                cmdySK &= 15;
            }
            if (Math.abs(cmtoYSK - cmySK) < 15 && cmySK < 0)
            {
                cmtoYSK = 0;
            }
            if (Math.abs(cmtoYSK - cmySK) < 15 && cmySK > cmyLimSK)
            {
                cmtoYSK = cmyLimSK;
            }
        }
    }

    // updateKeyAlert extracted to GameScr.Input.cs

    public bool isPaintPopup()
    {
        if (isPaintItemInfo || isPaintInfoMe || isPaintStore || isPaintWeapon || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintSplit || isPaintUpPearl || isPaintBox || isPaintTrade || isPaintAlert || isPaintZone || isPaintTeam || isPaintClan || isPaintFindTeam || isPaintTask || isPaintFriend || isPaintEnemies || isPaintCharInMap || isPaintMessage)
        {
            return true;
        }
        return false;
    }

    public bool isNotPaintTouchControl()
    {
        if (!GameCanvas.isTouchControl && GameCanvas.currentScreen == gI())
        {
            return true;
        }
        if (!GameCanvas.isTouch)
        {
            return true;
        }
        if (ChatTextField.gI().isShow)
        {
            return true;
        }
        if (InfoDlg.isShow)
        {
            return true;
        }
        if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || isPaintPopup())
        {
            return true;
        }
        return false;
    }

    public bool isPaintUI()
    {
        if (isPaintStore || isPaintWeapon || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintSplit || isPaintUpPearl || isPaintBox || isPaintTrade)
        {
            return true;
        }
        return false;
    }

    public bool isOpenUI()
    {
        if (isPaintItemInfo || isPaintInfoMe || isPaintStore || isPaintNonNam || isPaintNonNu || isPaintAoNam || isPaintAoNu || isPaintGangTayNam || isPaintGangTayNu || isPaintQuanNam || isPaintQuanNu || isPaintGiayNam || isPaintGiayNu || isPaintLien || isPaintNhan || isPaintNgocBoi || isPaintPhu || isPaintWeapon || isPaintStack || isPaintStackLock || isPaintGrocery || isPaintGroceryLock || isPaintUpGrade || isPaintConvert || isPaintUpPearl || isPaintBox || isPaintSplit || isPaintTrade)
        {
            return true;
        }
        return false;
    }

    public static void setPopupSize(int w, int h)
    {
        if (GameCanvas.w == 128 || GameCanvas.h <= 208)
        {
            w = 126;
            h = 160;
        }
        indexTitle = 0;
        popupW = w;
        popupH = h;
        popupX = gW2 - w / 2;
        popupY = gH2 - h / 2;
        if (GameCanvas.isTouch && !isPaintZone && !isPaintTeam && !isPaintClan && !isPaintCharInMap && !isPaintFindTeam && !isPaintFriend && !isPaintEnemies && !isPaintTask && !isPaintMessage)
        {
            if (GameCanvas.h <= 240)
            {
                popupY -= 10;
            }
            if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
            {
                popupW = 310;
                popupX = gW / 2 - popupW / 2;
                if (isPaintInfoMe && indexMenu > 0)
                {
                    popupW = w;
                    popupX = gW2 - w / 2;
                }
            }
        }
        if (popupY < -10)
        {
            popupY = -10;
        }
        if (GameCanvas.h > 208 && popupY < 0)
        {
            popupY = 0;
        }
        if (GameCanvas.h == 208 && popupY < 10)
        {
            popupY = 10;
        }
    }

    public static void loadImg()
    {
        TileMap.loadTileImage();
    }

    public void paintTitle(mGraphics g, string title, bool arrow)
    {
        int num = 0;
        num = gW / 2;
        g.setColor(Paint.COLORDARK);
        g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
        if ((indexTitle == 0 || GameCanvas.isTouch) && arrow)
        {
            SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), popupY + 16, 2, StaticObj.VCENTER_HCENTER);
            SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), popupY + 16, 0, StaticObj.VCENTER_HCENTER);
        }
        if (indexTitle == 0)
        {
            g.setColor(Paint.COLORFOCUS);
        }
        else
        {
            g.setColor(Paint.COLORBORDER);
        }
        g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
        mFont.tahoma_8b.drawString(g, title, num, popupY + 9, 2);
    }

    public static int getTaskMapId()
    {
        int num = 0;
        if (Char.myCharz().taskMaint == null)
        {
            return -1;
        }
        return mapTasks[Char.myCharz().taskMaint.index];
    }

    public static sbyte getTaskNpcId()
    {
        sbyte result = 0;
        if (Char.myCharz().taskMaint == null)
        {
            result = -1;
        }
        else if (Char.myCharz().taskMaint.index <= tasks.Length - 1)
        {
            result = (sbyte)tasks[Char.myCharz().taskMaint.index];
        }
        return result;
    }

    public void refreshTeam()
    {
    }

    // actionPerform_chat_menus extracted to GameScr.Actions.cs

    // gamepad_touchBtn extracted to GameScr.Touch.cs

    private void paintGamePad(mGraphics g)
    {
        if (isAnalog != 0 && Char.myCharz().statusMe != 14)
        {
            g.drawImage((keyTouch != 5 && keyMouse != 5) ? imgFire0 : imgFire1, xF + 20, yF + 14, mGraphics.HCENTER | mGraphics.VCENTER);
            gamePad.paint(g);
            g.drawImage((keyTouch != 13) ? imgFocus : imgFocus2, xTG + 20, yTG + 14, mGraphics.HCENTER | mGraphics.VCENTER);
            ModFunc.GI().PaintButton(g, xTG, yTG);
        }
    }

    public void showWinNumber(string num, string finish)
    {
        winnumber = new int[num.Length];
        randomNumber = new int[num.Length];
        tMove = new int[num.Length];
        moveCount = new int[num.Length];
        delayMove = new int[num.Length];
        try
        {
            for (int i = 0; i < num.Length; i++)
            {
                winnumber[i] = short.Parse(num[i].ToString());
                randomNumber[i] = Res.random(0, 11);
                tMove[i] = 1;
                delayMove[i] = 0;
            }
        }
        catch (Exception)
        {
        }
        tShow = 100;
        moveIndex = 0;
        strFinish = finish;
        lastXS = (currXS = mSystem.currentTimeMillis());
    }

    public void chatVip(string chatVip)
    {
        if (!startChat)
        {
            currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
            xChatVip = GameCanvas.w;
            startChat = true;
        }
        if (chatVip.StartsWith("!"))
        {
            chatVip = chatVip.Substring(1, chatVip.Length);
            isFireWorks = true;
        }
        vChatVip.addElement(chatVip);
        if (chatVip.Trim().ToLower().Contains("boss") && chatVip.Trim().ToLower().Contains("xuất hiện"))
        {
            ModFunc.bossNotif.addElement(new ShowBoss(chatVip));
            if (ModFunc.bossNotif.size() > 5)
            {
                ModFunc.bossNotif.removeElementAt(0);
            }
        }
    }

    public void clearChatVip()
    {
        vChatVip.removeAllElements();
        xChatVip = GameCanvas.w;
        startChat = false;
    }

    public void paintChatVip(mGraphics g)
    {
        if (vChatVip.size() != 0 && isPaintChatVip)
        {
            g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
            g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
            string st = (string)vChatVip.elementAt(0);
            mFont.tahoma_7b_yellow.drawStringBorder(g, st, xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
        }
    }

    public void updateChatVip()
    {
        if (!startChat)
        {
            return;
        }
        xChatVip -= 2;
        if (xChatVip < -currChatWidth)
        {
            xChatVip = GameCanvas.w;
            vChatVip.removeElementAt(0);
            if (vChatVip.size() == 0)
            {
                isFireWorks = false;
                startChat = false;
            }
            else
            {
                currChatWidth = mFont.tahoma_7b_white.getWidth((string)vChatVip.elementAt(0));
            }
        }
    }

    public void showYourNumber(string strNum)
    {
        yourNumber = strNum;
        strPaint = mFont.tahoma_7.splitFontArray(yourNumber, 500);
    }

    public static void checkRemoveImage()
    {
        ImgByName.checkDelHash(ImgByName.hashImagePath, 10, isTrue: false);
    }

    public static void StartServerPopUp(string strMsg)
    {
        GameCanvas.endDlg();
        int avatar = 1139;
        Npc npc = new Npc(-1, 0, 0, 0, 0, 0);
        npc.avatar = avatar;
        ChatPopup.addBigMessage(strMsg, 100000, npc);
        ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
        ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
        ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
    }

    public static bool ispaintPhubangBar()
    {
        if (TileMap.mapPhuBang() && phuban_Info.type_PB == 0)
        {
            return true;
        }
        return false;
    }

    public void paintPhuBanBar(mGraphics g, int x, int y, int w)
    {
        if (phuban_Info == null || isPaintOther || isPaintRada != 1 || GameCanvas.panel.isShow || !ispaintPhubangBar())
        {
            return;
        }
        if (w < fra_PVE_Bar_1.frameWidth + fra_PVE_Bar_0.frameWidth * 4)
        {
            w = fra_PVE_Bar_1.frameWidth + fra_PVE_Bar_0.frameWidth * 4;
        }
        if (x > GameCanvas.w - w / 2)
        {
            x = GameCanvas.w - w / 2;
        }
        if (x < mGraphics.getImageWidth(imgKhung) + w / 2 + 10)
        {
            x = mGraphics.getImageWidth(imgKhung) + w / 2 + 10;
        }
        int frameHeight = fra_PVE_Bar_0.frameHeight;
        int num = y + frameHeight + mGraphics.getImageHeight(imgBall) / 2 + 2;
        int frameWidth = fra_PVE_Bar_1.frameWidth;
        int num2 = w / 2 - frameWidth / 2;
        int num3 = x - w / 2;
        int num4 = x + frameWidth / 2;
        int y2 = y + 3;
        int num5 = num2 - fra_PVE_Bar_0.frameWidth;
        int num6 = num5 / fra_PVE_Bar_0.frameWidth;
        if (num5 % fra_PVE_Bar_0.frameWidth > 0)
        {
            num6++;
        }
        for (int i = 0; i < num6; i++)
        {
            if (i < num6 - 1)
            {
                fra_PVE_Bar_0.drawFrame(1, num3 + fra_PVE_Bar_0.frameWidth + i * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
            else
            {
                fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
            }
            if (i < num6 - 1)
            {
                fra_PVE_Bar_0.drawFrame(1, num4 + i * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
            else
            {
                fra_PVE_Bar_0.drawFrame(1, num4 + num5 - fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
            }
        }
        fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
        fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
        if (phuban_Info.pointTeam1 > 0)
        {
            int idx = 2;
            int idx2 = 3;
            if (phuban_Info.color_1 == 4)
            {
                idx = 4;
                idx2 = 5;
            }
            int num7 = phuban_Info.pointTeam1 * num2 / phuban_Info.maxPoint;
            if (num7 < 0)
            {
                num7 = 0;
            }
            if (num7 > num2)
            {
                num7 = num2;
            }
            g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
            for (int j = 0; j < num6; j++)
            {
                if (j < num6 - 1)
                {
                    fra_PVE_Bar_0.drawFrame(idx2, num3 + fra_PVE_Bar_0.frameWidth + j * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
                else
                {
                    fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
                }
            }
            fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
            GameCanvas.resetTrans(g);
        }
        if (phuban_Info.pointTeam2 > 0)
        {
            int idx3 = 2;
            int idx4 = 3;
            if (phuban_Info.color_2 == 4)
            {
                idx3 = 4;
                idx4 = 5;
            }
            int num8 = phuban_Info.pointTeam2 * num2 / phuban_Info.maxPoint;
            if (num8 < 0)
            {
                num8 = 0;
            }
            if (num8 > num2)
            {
                num8 = num2;
            }
            g.setClip(num4, y2, num8, frameHeight);
            for (int k = 0; k < num6; k++)
            {
                if (k < num6 - 1)
                {
                    fra_PVE_Bar_0.drawFrame(idx4, num4 + k * fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
                else
                {
                    fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
                }
            }
            fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
            GameCanvas.resetTrans(g);
        }
        fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
        string timeCountDown = mSystem.getTimeCountDown(phuban_Info.timeStart, phuban_Info.timeSecond, isOnlySecond: true, isShortText: false);
        mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
        Panel.setTextColor(phuban_Info.color_1, 1).drawString(g, phuban_Info.nameTeam1, x - 5, num + 5, 1);
        Panel.setTextColor(phuban_Info.color_2, 1).drawString(g, phuban_Info.nameTeam2, x + 5, num + 5, 0);
        if (phuban_Info.type_PB != 0)
        {
            int y3 = y + frameHeight / 2 - 2;
            mFont.bigNumber_While.drawString(g, string.Empty + phuban_Info.pointTeam1, num3 + num2 / 2, y3, 2);
            mFont.bigNumber_While.drawString(g, string.Empty + phuban_Info.pointTeam2, num4 + num2 / 2, y3, 2);
        }
        g.drawImage(imgVS, x, y + fra_PVE_Bar_1.frameHeight + 2, 3);
        if (phuban_Info.type_PB == 0)
        {
            paintChienTruong_Life(g, phuban_Info.maxLife, phuban_Info.color_1, phuban_Info.lifeTeam1, x - 13, phuban_Info.color_2, phuban_Info.lifeTeam2, x + 13, num);
        }
    }

    public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
    {
        if (imgBall == null)
        {
            return;
        }
        int num = mGraphics.getImageHeight(imgBall) / 2;
        for (int i = 0; i < maxLife; i++)
        {
            int num2 = 0;
            if (i < lifeTeam1)
            {
                num2 = 1;
            }
            g.drawRegion(imgBall, 0, num2 * num, mGraphics.getImageWidth(imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
        }
        for (int j = 0; j < maxLife; j++)
        {
            int num3 = 0;
            if (j < lifeTeam2)
            {
                num3 = 1;
            }
            g.drawRegion(imgBall, 0, num3 * num, mGraphics.getImageWidth(imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
        }
    }

    public static void paintHPBar_NEW(mGraphics g, int x, int y, Char c)
    {
        g.drawImage(imgKhung, x, y, 0);
        int x2 = x + 3;
        int num = y + 19;
        int num2 = 0;
        int num3 = 0;
        int width = imgHP_NEW.getWidth();
        int num4 = imgHP_NEW.getHeight() / 2;
        num2 = (int)(c.cHP * width / c.cHPFull);
        if (num2 <= 0)
        {
            num2 = 1;
        }
        else if (num2 > width)
        {
            num2 = width;
        }
        g.drawRegion(imgHP_NEW, 0, num4, num2, num4, 0, x2, num, 0);
        num3 = (int)(c.cMP * width / c.cMPFull);
        if (num3 <= 0)
        {
            num3 = 1;
        }
        else if (num3 > width)
        {
            num3 = width;
        }
        g.drawRegion(imgHP_NEW, 0, 0, num3, num4, 0, x2, num + 6, 0);
        int x3 = x + imgKhung.getWidth() / 2 + 1;
        int y2 = num + 13;
        mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
        if (c.mobFocus != null)
        {
            if (c.mobFocus.getTemplate() != null)
            {
                mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
            }
        }
        else if (c.npcFocus != null)
        {
            mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
        }
        else if (c.charFocus != null)
        {
            mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
        }
    }

    public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
    {
        Effect_End eff = new(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj);
        addEffect2Vector(eff);
    }

    public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj, sbyte level)
    {
        Effect_End eff = new(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj, level);
        addEffect2Vector(eff);
    }

    public static void addEffectEnd_Target(int type, int subtype, int typePaint, Char charUse, Point target, int levelPaint, short timeRemove, short range, sbyte level)
    {
        Effect_End eff = new(type, subtype, typePaint, charUse.clone(), target, levelPaint, timeRemove, range, level);
        addEffect2Vector(eff);
    }

    public static void addEffect2Vector(Effect_End eff)
    {
        if (eff.levelPaint == 0)
        {
            EffectManager.addHiEffect(eff);
        }
        else if (eff.levelPaint == 1)
        {
            EffectManager.addMidEffects(eff);
        }
        else if (eff.levelPaint == 2)
        {
            EffectManager.addMid_2Effects(eff);
        }
        else
        {
            EffectManager.addLowEffect(eff);
        }
    }

    public static bool setIsInScreen(int x, int y, int wOne, int hOne)
    {
        if (x < cmx - wOne || x > cmx + GameCanvas.w + wOne || y < cmy - hOne || y > cmy + GameCanvas.h + hOne * 3 / 2)
        {
            return false;
        }
        return true;
    }

    public static bool isSmallScr()
    {
        if (GameCanvas.w <= 320)
        {
            return true;
        }
        return false;
    }

    private void paint_xp_bar(mGraphics g)
    {
        g.setColor(8421504);
        g.fillRect(0, GameCanvas.h - 2, GameCanvas.w, 2);
        int w = (int)(Char.myCharz().cLevelPercent * GameCanvas.w / 10000);
        g.setColor(16777215);
        g.fillRect(0, GameCanvas.h - 2, w, 2);
        g.setColor(0);
        w = GameCanvas.w / 10;
        for (int i = 1; i < 10; i++)
        {
            g.fillRect(i * w, GameCanvas.h - 2, 1, 2);
        }
    }

    // paint_ios_bg extracted to GameScr.Camera.cs
}
