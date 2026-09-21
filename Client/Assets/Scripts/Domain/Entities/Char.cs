using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char : IMapObject
{
    // All fields declared in Char.Fields.cs and Char.Tables.cs


    public Char()
    {
        statusMe = 6;
    }

    public void applyCharLevelPercent()
    {
        try
        {
            long num = 1L;
            long num2 = 0L;
            int num3 = 0;
            for (int num4 = GameScr.exps.Length - 1; num4 >= 0; num4--)
            {
                if (cPower >= GameScr.exps[num4])
                {
                    num = ((num4 != GameScr.exps.Length - 1) ? (GameScr.exps[num4 + 1] - GameScr.exps[num4]) : 1);
                    num2 = cPower - GameScr.exps[num4];
                    num3 = num4;
                    break;
                }
            }
            clevel = num3;
            cLevelPercent = num2 * 10000 / num;
            if (cLevelPercent > 10000)
            {
                cLevelPercent = 10000;
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi char level percent: " + ex.ToString());
        }
    }

    // getdx_dySkill extracted to Char.Skills.cs

    public static void taskAction(bool isNextStep)
    {
        Task task = myCharz().taskMaint;
        if (task.index > task.contentInfo.Length - 1)
        {
            task.index = task.contentInfo.Length - 1;
        }
        string text = task.contentInfo[task.index];
        if (text != null && !text.Equals(string.Empty))
        {
            if (text.StartsWith("#"))
            {
                text = NinjaUtil.Replace(text, "#", string.Empty);
                Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[myCharz().cgender][2]);
                npc.cx = (npc.cy = -100);
                npc.avatar = GameScr.info1.charId[myCharz().cgender][2];
                npc.charID = 5;
                if (GameCanvas.currentScreen == GameScr.instance)
                {
                    ChatPopup.addNextPopUpMultiLine(text, npc);
                }
            }
            else if (isNextStep)
            {
                GameScr.info1.addInfo(text, 0);
            }
        }
        GameScr.isHaveSelectSkill = true;
        Cout.println("TASKx " + myCharz().taskMaint.taskId);
        if (myCharz().taskMaint.taskId <= 2)
        {
            myCharz().canFly = false;
        }
        else
        {
            myCharz().canFly = true;
        }
        GameScr.gI().left = GameScr.gI().cmdMenu;
        GameScr.gI().right = GameScr.gI().cmdFocus;
        GameScr.isHaveSelectSkill = true;
        GameScr.isPaintRada = 1;
        MagicTree.isPaint = true;
        Hint.isViewMap = true;
        Hint.isViewPotential = true;
        if (task.taskId >= 0)
        {
            Panel.isPaintMap = true;
        }
        else
        {
            Panel.isPaintMap = false;
        }
        if (task.taskId < 12)
        {
            GameCanvas.panel.mainTabName = mResources.mainTab1;
        }
        else
        {
            GameCanvas.panel.mainTabName = mResources.mainTab2;
        }
        GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
        if (myChar.taskMaint.taskId > 10)
        {
            Rms.saveRMSString("fake", "aa");
        }
    }

    public string getStrLevel()
    {
        if (clevel >= strLevel.Length)
        {
            clevel = strLevel.Length - 1;
        }
        string text = strLevel[clevel] + "+" + cLevelPercent / 100 + "." + cLevelPercent % 100 + "%";
        if (text.Length > 23 && text.IndexOf("cấp ") >= 0)
        {
            text = Res.replace(text, "cấp ", "c");
        }
        return text;
    }

    public int avatarz()
    {
        return getAvatar(head);
    }

    public int getAvatar(int headId)
    {
        if (idHead == null || idAvatar == null)
        {
            return -1;
        }
        for (int i = 0; i < idHead.Length && i < idAvatar.Length; i++)
        {
            if (headId == idHead[i])
            {
                return idAvatar[i];
            }
        }
        return -1;
    }

    public void setPowerInfo(string info, short p, short maxP, short sc)
    {
        powerPoint = p;
        strInfo = info;
        maxPowerPoint = maxP;
        secondPower = sc;
        lastS = (currS = mSystem.currentTimeMillis());
    }

    public void addInfo(string info)
    {
        if (chatInfo == null)
        {
            chatInfo = new Info();
        }
        Char cInfo = null;
        chatInfo.addInfo(info, 0, cInfo, isChatServer: false);
    }

    public int getSys()
    {
        if (nClass.classId == 1 || nClass.classId == 2)
        {
            return 1;
        }
        if (nClass.classId == 3 || nClass.classId == 4)
        {
            return 2;
        }
        if (nClass.classId == 5 || nClass.classId == 6)
        {
            return 3;
        }
        return 0;
    }

    public static Char myCharz()
    {
        if (myChar == null)
        {
            myChar = new Char();
            myChar.me = true;
            myChar.cmtoChar = true;
        }
        return myChar;
    }

    public static Char myPetz()
    {
        if (myPet == null)
        {
            myPet = new Char();
            myPet.me = false;
        }
        return myPet;
    }

    public static Char MyPet2z()
    {
        myPet2 ??= new Char
            {
                me = false
            };
        return myPet2;
    }

    public static void clearMyChar()
    {
        myChar = null;
    }

    // bagBoxSort_useItem extracted to Char.Items.cs

    // getSkill_isPunchKick extracted to Char.Skills.cs

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

    public virtual void update()
    {
        if (isMafuba)
        {
            cf = 23;
            countMafuba++;
            if (countMafuba > 150)
            {
                isMafuba = false;
            }
            return;
        }
        countMafuba = 0;
        if (isHide || isMabuHold)
        {
            return;
        }
        if ((!isCopy && clevel < 14) || statusMe == 1 || statusMe == 6)
        {
        }
        if (petFollow != null)
        {
            if (GameCanvas.gameTick % 3 == 0)
            {
                if (myCharz().cdir == 1)
                {
                    petFollow.cmtoX = cx - 20;
                }
                if (myCharz().cdir == -1)
                {
                    petFollow.cmtoX = cx + 20;
                }
                petFollow.cmtoY = cy - 40;
                if (petFollow.cmx > cx)
                {
                    petFollow.dir = -1;
                }
                else
                {
                    petFollow.dir = 1;
                }
                if (petFollow.cmtoX < 100)
                {
                    petFollow.cmtoX = 100;
                }
                if (petFollow.cmtoX > TileMap.pxw - 100)
                {
                    petFollow.cmtoX = TileMap.pxw - 100;
                }
            }
            petFollow.update();
        }
        if (!me && cHP <= 0 && clanID != -100 && statusMe != 14 && statusMe != 5)
        {
            startDie((short)cx, (short)cy);
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
        else if (dHP > cHP)
        {
            long num = dHP - cHP >> 1;
            if (num < 1)
            {
                num = 1;
            }
            dHP -= num;
        }
        else
        {
            dHP = cHP;
        }
        if (secondPower != 0)
        {
            currS = mSystem.currentTimeMillis();
            if (currS - lastS >= 1000)
            {
                lastS = mSystem.currentTimeMillis();
                secondPower--;
            }
        }
        if (isPaintNewSkill)
        {
            if (GameCanvas.timeNow > timeReset_newSkill || statusMe == 14 || statusMe == 5)
            {
                timeReset_newSkill = 0L;
                isPaintNewSkill = false;
            }
            UpdSkillPaint_NEW();
            if (isShadown)
            {
                updateShadown();
            }
        }
        else
        {
            if (!me && GameScr.notPaint)
            {
                return;
            }
            if (sleepEff && GameCanvas.gameTick % 10 == 0)
            {
                EffecMn.addEff(new Effect(41, cx, cy, 3, 1, 1));
            }
            if (huytSao)
            {
                huytSao = false;
                EffecMn.addEff(new Effect(39, cx, cy, 3, 3, 1));
            }
            if (blindEff && GameCanvas.gameTick % 5 == 0)
            {
                ServerEffect.addServerEffect(113, this, 1);
            }
            if (protectEff)
            {
                int y = cH_new + 73;
                if (GameCanvas.gameTick % 5 == 0)
                {
                    eProtect = new Effect(33, cx, y, 3, 3, 1);
                }
                if (eProtect != null)
                {
                    eProtect.update();
                    eProtect.x = cx;
                    eProtect.y = y;
                }
            }
            if (danhHieuEff)
            {
                if (eDanhHieu == null)
                {
                    string text = (string)GameCanvas.danhHieu.get(charID + string.Empty);
                    if (text != null)
                    {
                        string[] array = Res.split(text.Trim(), ",", 0);
                        short id = short.Parse(array[0]);
                        short num2 = short.Parse(array[1]);
                        eDanhHieu = new Effect(id, cx, cH_new + 73, 1, -1, -1);
                        eDanhHieu.timeExist = num2 * 1000 + mSystem.currentTimeMillis();
                    }
                }
                if (eDanhHieu != null)
                {
                    eDanhHieu.update();
                    eDanhHieu.x = cx;
                    eDanhHieu.y = cH_new;
                    if (eDanhHieu.timeExist <= mSystem.currentTimeMillis())
                    {
                        eDanhHieu = null;
                        GameCanvas.danhHieu.remove(charID + string.Empty);
                    }
                }
            }
            if (charFocus != null && charFocus.cy < 0)
            {
                charFocus = null;
            }
            if (isFusion)
            {
                tFusion++;
            }
            if (isNhapThe)
            {
                int num3 = 0;
                if (GameCanvas.gameTick % 25 == 0)
                {
                    num3 = 114;
                    ServerEffect.addServerEffect(num3, this, 1);
                }
            }
            if (isSetPos)
            {
                tpos++;
                if (tpos != 1)
                {
                    return;
                }
                tpos = 0;
                isSetPos = false;
                cx = xPos;
                cy = yPos;
                cp1 = (cp2 = (cp3 = 0));
                if (typePos == 1)
                {
                    if (me)
                    {
                        cxSend = cx;
                        cySend = cy;
                    }
                    currentMovePoint = null;
                    telePortSkill = false;
                    ServerEffect.addServerEffect(173, cx, cy, 1);
                }
                else
                {
                    ServerEffect.addServerEffect(60, cx, cy, 1);
                }
                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                {
                    statusMe = 1;
                }
                else
                {
                    statusMe = 4;
                }
                return;
            }
            soundUpdate();
            if (stone)
            {
                return;
            }
            if (isFreez)
            {
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(113, cx, cy, 1);
                }
                cf = 23;
                long num4 = mSystem.currentTimeMillis();
                if (num4 - lastFreez >= 1000)
                {
                    freezSeconds--;
                    lastFreez = num4;
                    if (freezSeconds < 0)
                    {
                        isFreez = false;
                        seconds = 0;
                        if (me)
                        {
                            myCharz().isLockMove = false;
                            GameScr.gI().dem = 0;
                            GameScr.gI().isFreez = false;
                        }
                    }
                }
                if (TileMap.tileTypeAt(cx / TileMap.size, cy / TileMap.size) == 0)
                {
                    ty++;
                    wt++;
                    fy += ((!wy) ? 1 : (-1));
                    if (wt == 10)
                    {
                        wt = 0;
                        wy = !wy;
                    }
                }
                return;
            }
            if (isWaitMonkey)
            {
                isLockMove = true;
                cf = 17;
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(154, cx, cy - 10, 2);
                }
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(1, cx, cy + 10, 1);
                }
                chargeCount++;
                if (chargeCount == 500)
                {
                    isWaitMonkey = false;
                    isLockMove = false;
                }
                return;
            }
            if (isStandAndCharge)
            {
                chargeCount++;
                bool flag = !TileMap.tileTypeAt(myCharz().cx, myCharz().cy, 2);
                updateEffect();
                updateSkillPaint();
                moveFast = null;
                currentMovePoint = null;
                cf = 17;
                if (flag && cgender != 2)
                {
                    cf = 12;
                }
                if (cgender == 2)
                {
                    if (GameCanvas.gameTick % 3 == 0)
                    {
                        ServerEffect.addServerEffect(154, cx, cy - ch / 2 + 10, 1);
                    }
                    if (GameCanvas.gameTick % 5 == 0)
                    {
                        ServerEffect.addServerEffect(114, cx + Res.random(-20, 20), cy + Res.random(-20, 20), 1);
                    }
                }
                if (cgender == 1)
                {
                    if (GameCanvas.gameTick % 4 == 0)
                    {
                    }
                    if (GameCanvas.gameTick % 2 == 0)
                    {
                        if (cdir == 1)
                        {
                            ServerEffect.addServerEffect(70, cx - 18, cy - ch / 2 + 8, 1);
                            ServerEffect.addServerEffect(70, cx + 23, cy - ch / 2 + 15, 1);
                        }
                        else
                        {
                            ServerEffect.addServerEffect(70, cx + 18, cy - ch / 2 + 8, 1);
                            ServerEffect.addServerEffect(70, cx - 23, cy - ch / 2 + 15, 1);
                        }
                    }
                }
                cur = mSystem.currentTimeMillis();
                if (cur - last > seconds || cur - last > 10000)
                {
                    stopUseChargeSkill();
                    if (me)
                    {
                        GameScr.gI().auto = 0;
                        if (cgender == 2)
                        {
                            myCharz().setAutoSkillPaint(GameScr.sks[myCharz().myskill.skillId], flag ? 1 : 0);
                            Service.gI().skill_not_focus(8);
                        }
                        if (cgender == 1)
                        {
                            isCreateDark = true;
                            myCharz().setSkillPaint(GameScr.sks[myCharz().myskill.skillId], flag ? 1 : 0);
                        }
                    }
                    else if (cgender == 2)
                    {
                        setAutoSkillPaint(GameScr.sks[skillTemplateId], flag ? 1 : 0);
                    }
                    if (cgender == 2 && statusMe != 14 && statusMe != 5)
                    {
                        GameScr.gI().activeSuperPower(cx, cy);
                    }
                }
                chargeCount++;
                if (chargeCount == 500)
                {
                    stopUseChargeSkill();
                }
                return;
            }
            if (isFlyAndCharge)
            {
                updateEffect();
                updateSkillPaint();
                moveFast = null;
                currentMovePoint = null;
                posDisY++;
                if (TileMap.tileTypeAt(cx, cy - ch, 8192))
                {
                    stopUseChargeSkill();
                    return;
                }
                if (posDisY == 20)
                {
                    last = mSystem.currentTimeMillis();
                }
                if (posDisY > 20)
                {
                    cur = mSystem.currentTimeMillis();
                    if (cur - last > seconds || cur - last > 10000)
                    {
                        isFlyAndCharge = false;
                        if (me)
                        {
                            isCreateDark = true;
                            bool flag2 = TileMap.tileTypeAt(myCharz().cx, myCharz().cy, 2);
                            isUseSkillAfterCharge = true;
                            myCharz().setSkillPaint(GameScr.sks[myCharz().myskill.skillId], (!flag2) ? 1 : 0);
                        }
                        return;
                    }
                    cf = 32;
                    if (cgender == 0 && GameCanvas.gameTick % 3 == 0)
                    {
                        ServerEffect.addServerEffect(153, cx, cy - ch, 2);
                    }
                    chargeCount++;
                    if (chargeCount == 500)
                    {
                        stopUseChargeSkill();
                    }
                }
                else
                {
                    if (statusMe != 14)
                    {
                        statusMe = 3;
                    }
                    cvy = -3;
                    cy += cvy;
                    cf = 7;
                }
                return;
            }
            if (me && GameCanvas.isTouch)
            {
                if (charFocus != null && charFocus.charID >= 0 && charFocus.cx > 100 && charFocus.cx < TileMap.pxw - 100 && isInEnterOnlinePoint() == null && isInEnterOfflinePoint() == null && !isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
                {
                    int num5 = Math.abs(cx - charFocus.cx);
                    int num6 = Math.abs(cy - charFocus.cy);
                    if (num5 < 60 && num6 < 40)
                    {
                        if (cmdMenu == null)
                        {
                            cmdMenu = new Command(mResources.MENU, 11111);
                            cmdMenu.isPlaySoundButton = false;
                        }
                        cmdMenu.x = charFocus.cx - GameScr.cmx;
                        cmdMenu.y = charFocus.cy - charFocus.ch - 30 - GameScr.cmy;
                    }
                    else
                    {
                        cmdMenu = null;
                    }
                }
                else
                {
                    cmdMenu = null;
                }
            }
            if (isShadown)
            {
                updateShadown();
            }
            if (isTeleport)
            {
                return;
            }
            if (chatInfo != null)
            {
                chatInfo.update();
            }
            if (shadowLife > 0)
            {
                shadowLife--;
            }
            if (resultTest > 0 && GameCanvas.gameTick % 2 == 0)
            {
                resultTest--;
                if (resultTest == 30 || resultTest == 60)
                {
                    resultTest = 0;
                }
            }
            updateSkillPaint();
            if (mobMe != null)
            {
                updateMobMe();
            }
            if (arr != null)
            {
                arr.update();
            }
            if (dart != null)
            {
                dart.update();
            }
            updateEffect();
            if (holdEffID != 0)
            {
                if (GameCanvas.gameTick % 5 == 0)
                {
                    EffecMn.addEff(new Effect(32, cx, cy + 24, 3, 5, 1));
                }
            }
            else
            {
                if (blindEff || sleepEff)
                {
                    return;
                }
                if (holder)
                {
                    if (charHold != null && (charHold.statusMe == 14 || charHold.statusMe == 5))
                    {
                        removeHoleEff();
                    }
                    if (mobHold != null && mobHold.status == 1)
                    {
                        removeHoleEff();
                    }
                    if (me && statusMe == 2 && currentMovePoint != null)
                    {
                        holder = false;
                        charHold = null;
                        mobHold = null;
                    }
                    if (TileMap.tileTypeAt(cx, cy, 2))
                    {
                        cf = 16;
                    }
                    else
                    {
                        cf = 31;
                    }
                    return;
                }
                if (cHP > 0)
                {
                    for (int i = 0; i < vEff.size(); i++)
                    {
                        EffectChar effectChar = (EffectChar)vEff.elementAt(i);
                        if (effectChar.template.type == 0 || effectChar.template.type == 12)
                        {
                            if (GameCanvas.isEff1)
                            {
                                cHP += effectChar.param;
                                cMP += effectChar.param;
                            }
                        }
                        else if (effectChar.template.type == 4 || effectChar.template.type == 17)
                        {
                            if (GameCanvas.isEff1)
                            {
                                cHP += effectChar.param;
                            }
                        }
                        else if (effectChar.template.type == 13 && GameCanvas.isEff1)
                        {
                            cHP -= cHPFull * 3 / 100;
                            if (cHP < 1)
                            {
                                cHP = 1;
                            }
                        }
                    }
                    if (eff5BuffHp > 0 && GameCanvas.isEff2)
                    {
                        cHP += eff5BuffHp;
                    }
                    if (eff5BuffMp > 0 && GameCanvas.isEff2)
                    {
                        cMP += eff5BuffMp;
                    }
                    if (cHP > cHPFull)
                    {
                        cHP = cHPFull;
                    }
                    if (cMP > cMPFull)
                    {
                        cMP = cMPFull;
                    }
                }
                if (cmtoChar)
                {
                    GameScr.cmtoX = cx - GameScr.gW2;
                    GameScr.cmtoY = cy - GameScr.gH23;
                    if (!GameCanvas.isTouchControl)
                    {
                        GameScr.cmtoX += GameScr.gW6 * cdir;
                    }
                }
                tick = (tick + 1) % 100;
                if (me)
                {
                    if (charFocus != null && !GameScr.vCharInMap.contains(charFocus))
                    {
                        charFocus = null;
                    }
                    if (cx < 10)
                    {
                        cvx = 0;
                        cx = 10;
                    }
                    else if (cx > TileMap.pxw - 10)
                    {
                        cx = TileMap.pxw - 10;
                        cvx = 0;
                    }
                    if (me && !ischangingMap && isInWaypoint())
                    {
                        Service.gI().charMove();
                        if (TileMap.isTrainingMap())
                        {
                            Service.gI().getMapOffline();
                            ischangingMap = true;
                        }
                        else
                        {
                            Service.gI().requestChangeMap();
                        }
                        isLockKey = true;
                        ischangingMap = true;
                        GameCanvas.clearKeyHold();
                        GameCanvas.clearKeyPressed();
                        InfoDlg.showWait();
                        return;
                    }
                    if (statusMe != 4 && Res.abs(cx - cxSend) + Res.abs(cy - cySend) >= 70 && cy - cySend <= 0 && me)
                    {
                        Service.gI().charMove();
                    }
                    if (isLockMove)
                    {
                        currentMovePoint = null;
                    }
                    if (currentMovePoint != null)
                    {
                        if (abs(cx - currentMovePoint.xEnd) <= 16 && abs(cy - currentMovePoint.yEnd) <= 16)
                        {
                            cx = (currentMovePoint.xEnd + cx) / 2;
                            cy = currentMovePoint.yEnd;
                            currentMovePoint = null;
                            GameScr.instance.clickMoving = false;
                            checkPerformEndMovePointAction();
                            cvx = (cvy = 0);
                            if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                            {
                                statusMe = 1;
                            }
                            else
                            {
                                setCharFallFromJump();
                            }
                            Service.gI().charMove();
                        }
                        else
                        {
                            cdir = ((currentMovePoint.xEnd > cx) ? 1 : (-1));
                            if (TileMap.tileTypeAt(cx, cy, 2))
                            {
                                statusMe = 2;
                                if (currentMovePoint != null)
                                {
                                    cvx = cspeed * cdir;
                                    cvy = 0;
                                }
                                if (abs(cx - currentMovePoint.xEnd) <= 10)
                                {
                                    if (currentMovePoint.yEnd > cy)
                                    {
                                        bool flag3 = false;
                                        sbyte b = 1;
                                        b = (sbyte)((cdir == 1) ? 1 : (-1));
                                        for (int j = 0; j < 2; j++)
                                        {
                                            if (TileMap.tileTypeAt(currentMovePoint.xEnd + chw * b, cy + chh * j, 2))
                                            {
                                                flag3 = true;
                                                break;
                                            }
                                        }
                                        if (flag3)
                                        {
                                            currentMovePoint = null;
                                            GameScr.instance.clickMoving = false;
                                            statusMe = 1;
                                            cvx = (cvy = 0);
                                            checkPerformEndMovePointAction();
                                        }
                                        else
                                        {
                                            SoundMn.gI().charJump();
                                            cx = currentMovePoint.xEnd;
                                            statusMe = 10;
                                            cvy = -5;
                                            cvx = 0;
                                        }
                                    }
                                    else
                                    {
                                        SoundMn.gI().charJump();
                                        cx = currentMovePoint.xEnd;
                                        statusMe = 10;
                                        cvy = -5;
                                        cvx = 0;
                                    }
                                }
                                if (cdir == 1)
                                {
                                    if (TileMap.tileTypeAt(cx + chw, cy - chh, 4))
                                    {
                                        cvx = cspeed * cdir;
                                        statusMe = 10;
                                        cvy = -5;
                                    }
                                }
                                else if (TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8))
                                {
                                    cvx = cspeed * cdir;
                                    statusMe = 10;
                                    cvy = -5;
                                }
                            }
                            else
                            {
                                if (currentMovePoint.yEnd < cy + 10)
                                {
                                    statusMe = 10;
                                    cvy = -5;
                                    if (abs(cy - currentMovePoint.yEnd) <= 10)
                                    {
                                        cy = currentMovePoint.yEnd;
                                        cvy = 0;
                                    }
                                    if (abs(cx - currentMovePoint.xEnd) <= 10)
                                    {
                                        cvx = 0;
                                    }
                                    else
                                    {
                                        cvx = cspeed * cdir;
                                    }
                                }
                                else if (TileMap.tileTypeAt(cx, cy, 2))
                                {
                                    currentMovePoint = null;
                                    GameScr.instance.clickMoving = false;
                                    statusMe = 1;
                                    cvx = (cvy = 0);
                                    checkPerformEndMovePointAction();
                                }
                                else
                                {
                                    if (statusMe == 10 || statusMe == 2)
                                    {
                                        cvy = 0;
                                    }
                                    statusMe = 4;
                                }
                                if (currentMovePoint.yEnd > cy)
                                {
                                    if (cdir == 1)
                                    {
                                        if (TileMap.tileTypeAt(cx + chw, cy - chh, 4))
                                        {
                                            cvx = (cvy = 0);
                                            statusMe = 4;
                                            currentMovePoint = null;
                                            GameScr.instance.clickMoving = false;
                                            checkPerformEndMovePointAction();
                                        }
                                    }
                                    else if (TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8))
                                    {
                                        cvx = (cvy = 0);
                                        statusMe = 4;
                                        currentMovePoint = null;
                                        GameScr.instance.clickMoving = false;
                                        checkPerformEndMovePointAction();
                                    }
                                }
                            }
                        }
                    }
                    searchFocus();
                }
                else
                {
                    checkHideCharName();
                    if (statusMe == 1 || statusMe == 6)
                    {
                        bool flag4 = false;
                        if (currentMovePoint != null)
                        {
                            if (abs(currentMovePoint.xEnd - cx) < 17 && abs(currentMovePoint.yEnd - cy) < 25)
                            {
                                cx = currentMovePoint.xEnd;
                                cy = currentMovePoint.yEnd;
                                currentMovePoint = null;
                                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                                {
                                    statusMe = 1;
                                    cp3 = 0;
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                }
                                else
                                {
                                    statusMe = 4;
                                    cvy = 0;
                                    cp1 = 0;
                                }
                                flag4 = true;
                            }
                            else if ((statusBeforeNothing == 10 || cf == 8) && vMovePoints.size() > 0)
                            {
                                flag4 = true;
                            }
                            else if (cy == currentMovePoint.yEnd)
                            {
                                if (cx != currentMovePoint.xEnd)
                                {
                                    cx = (cx + currentMovePoint.xEnd) / 2;
                                    cf = GameCanvas.gameTick % 5 + 2;
                                }
                            }
                            else if (cy < currentMovePoint.yEnd)
                            {
                                cf = 12;
                                cx = (cx + currentMovePoint.xEnd) / 2;
                                if (cvy < 0)
                                {
                                    cvy = 0;
                                }
                                cy += cvy;
                                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                                {
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                }
                                cvy++;
                                if (cvy > 16)
                                {
                                    cy = (cy + currentMovePoint.yEnd) / 2;
                                }
                            }
                            else
                            {
                                cf = 7;
                                cx = (cx + currentMovePoint.xEnd) / 2;
                                cy = (cy + currentMovePoint.yEnd) / 2;
                            }
                        }
                        else
                        {
                            flag4 = true;
                        }
                        if (flag4 && vMovePoints.size() > 0)
                        {
                            currentMovePoint = (MovePoint)vMovePoints.firstElement();
                            vMovePoints.removeElementAt(0);
                            if (currentMovePoint.status == 2)
                            {
                                if ((TileMap.tileTypeAtPixel(cx, cy + 12) & 2) != 2)
                                {
                                    statusMe = 10;
                                    cp1 = 0;
                                    cp2 = 0;
                                    cvx = -(cx - currentMovePoint.xEnd) / 10;
                                    cvy = -(cy - currentMovePoint.yEnd) / 10;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                }
                                else
                                {
                                    statusMe = 2;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                    cvx = cspeed * cdir;
                                    cvy = 0;
                                }
                            }
                            else if (currentMovePoint.status == 3)
                            {
                                if ((TileMap.tileTypeAtPixel(cx, cy + 23) & 2) != 2)
                                {
                                    statusMe = 10;
                                    cp1 = 0;
                                    cp2 = 0;
                                    cvx = -(cx - currentMovePoint.xEnd) / 10;
                                    cvy = -(cy - currentMovePoint.yEnd) / 10;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                }
                                else
                                {
                                    statusMe = 3;
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                    cvx = abs(cx - currentMovePoint.xEnd) / 10 * cdir;
                                    cvy = -10;
                                }
                            }
                            else if (currentMovePoint.status == 4)
                            {
                                statusMe = 4;
                                if (cx - currentMovePoint.xEnd > 0)
                                {
                                    cdir = -1;
                                }
                                else if (cx - currentMovePoint.xEnd < 0)
                                {
                                    cdir = 1;
                                }
                                cvx = abs(cx - currentMovePoint.xEnd) / 9 * cdir;
                                cvy = 0;
                            }
                            else
                            {
                                cx = currentMovePoint.xEnd;
                                cy = currentMovePoint.yEnd;
                                currentMovePoint = null;
                            }
                        }
                    }
                }
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
        }
    }

    // updateEffect extracted to Char.Effects.cs

    // checkPerformEndMovePointAction extracted to Char.Navigation.cs

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

    // updateSkillPaint extracted to Char.Skills.cs

    // resetPoints_autoJump extracted to Char.Navigation.cs


    // superEff_soundVolumn extracted to Char.Effects.cs


    // defaultParts extracted to Char.Items.cs

    // skillSelection_ChargeSkills extracted to Char.Skills.cs

    // setAttack extracted to Char.Combat.cs

    // isOutX_createShadow extracted to Char.Overhead.cs

    // setMabuHold extracted to Char.Effects.cs

    // paintMaster extracted to Char.Paint.cs

    // paint_map_line extracted to Char.Overhead.cs

    // paintSuperEffects extracted to Char.Effects.cs

    // hp_name_shadow extracted to Char.Overhead.cs

    // charBodyParts_render extracted to Char.Paint.cs

    // moveTo extracted to Char.Navigation.cs

    // getcharInjure extracted to Char.Combat.cs

    // isMagicTree extracted to Char.Overhead.cs

    // searchItem() extracted to Char.Targeting.cs

    // searchFocus() extracted to Char.Targeting.cs

    // ClearFocus() extracted to Char.Targeting.cs

    // isCharInScreen() extracted to Char.Targeting.cs

    // isAttacPlayerStatus() extracted to Char.Targeting.cs

    // setHoldChar() extracted to Char.Targeting.cs

    // setHoldMob() extracted to Char.Targeting.cs

    // findNextFocusByKey() extracted to Char.Targeting.cs

    // deFocusNPC() extracted to Char.Targeting.cs

    // updateCharInBridge extracted to Char.Navigation.cs

    // inventoryUtils_potions extracted to Char.Items.cs

    // isLang_isMeCanAttack extracted to Char.Combat.cs

    public void clearTask()
    {
        myCharz().taskMaint = null;
        for (int i = 0; i < myCharz().arrItemBag.Length; i++)
        {
            if (myCharz().arrItemBag[i] != null && myCharz().arrItemBag[i].template.type == 8)
            {
                myCharz().arrItemBag[i] = null;
            }
        }
        Npc.clearEffTask();
    }

    public int getX()
    {
        return cx;
    }

    public int getY()
    {
        return cy;
    }

    public int getH()
    {
        return 32;
    }

    public int getW()
    {
        return 24;
    }

    // FocusManualTo() extracted to Char.Targeting.cs

    // stopMoving extracted to Char.Navigation.cs

    // cancelAttack extracted to Char.Combat.cs

    public bool isInvisible()
    {
        return false;
    }

    // focusToAttack() extracted to Char.Targeting.cs

    // addDustEff extracted to Char.Effects.cs

    // flagPK extracted to Char.Overhead.cs

    // removeStatusEffects extracted to Char.Effects.cs

    // partTransforms extracted to Char.Appearance.cs

    // effChar_customEffects extracted to Char.Effects.cs

    // checkLuong extracted to Char.Items.cs

    // eyeAuraHat extracted to Char.Appearance.cs

    // isFrNgang extracted to Char.Paint.cs

    // sendNewAttack extracted to Char.Combat.cs

    // skillPaint_NEW extracted to Char.Skills.cs

    public Char clone()
    {
        Char @char = new Char();
        @char.charID = charID;
        @char.cx = cx;
        @char.cy = cy;
        @char.cdir = cdir;
        if (arrItemBody != null)
        {
            @char.arrItemBody = new Item[arrItemBody.Length];
            for (int i = 0; i < arrItemBody.Length; i++)
            {
                if (arrItemBody[i] == null)
                {
                    @char.arrItemBody[i] = null;
                }
                else
                {
                    @char.arrItemBody[i] = arrItemBody[i].clone();
                }
            }
        }
        return @char;
    }

    // containsCaiTrang extracted to Char.Items.cs

    public void printlog()
    {
        string empty = string.Empty;
        string text = empty;
        empty = text + "isInjure " + isInjure + "\n";
        text = empty;
        empty = text + "isInjure " + isMonkey + "\n";
        text = empty;
        empty = text + "isInjure " + isAddChopMat + "\n";
        text = empty;
        empty = text + "isInjure " + isAttack + "\n";
        text = empty;
        empty = text + "isInjure " + isAttFly + "\n";
        text = empty;
        empty = text + "isInjure " + ischangingMap + "\n";
        text = empty;
        empty = text + "isInjure " + isCharge + "\n";
        text = empty;
        empty = text + "isInjure " + isCopy + "\n";
        text = empty;
        empty = text + "isInjure " + isCreateDark + "\n";
        text = empty;
        empty = text + "isInjure " + isCrit + "\n";
        text = empty;
        empty = text + "isInjure " + isDirtyPostion + "\n";
        text = empty;
        empty = text + "isInjure " + isEndMount + "\n";
        text = empty;
        empty = text + "isInjure " + isEventMount + "\n";
        text = empty;
        empty = text + "isInjure " + isMafuba + "\n";
        text = empty;
        empty = text + "isInjure " + isFusion + "\n";
        text = empty;
        empty = text + "isInjure " + isFeetEff + "\n";
        text = empty;
        empty = text + "isInjure " + isFlying + "\n";
        text = empty;
        empty = text + "isInjure " + isWaitMonkey + "\n";
        text = empty;
        empty = text + "isInjure " + isUseSkillSpec() + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
    }

    // setDanhHieu extracted to Char.Overhead.cs
}
