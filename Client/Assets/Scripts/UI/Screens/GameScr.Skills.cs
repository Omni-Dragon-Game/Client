using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- shortcuts ---
    public void loadSkillShortcut()
    {
    }

    public void onOSkill(sbyte[] oSkillID)
    {
        Cout.println("GET onScreenSkill!");
        onScreenSkill = new Skill[10];
        if (oSkillID == null)
        {
            loadDefaultonScreenSkill();
            return;
        }
        for (int i = 0; i < oSkillID.Length; i++)
        {
            for (int j = 0; j < Char.myCharz().vSkillFight.size(); j++)
            {
                Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(j);
                if (skill.template.id == oSkillID[i])
                {
                    onScreenSkill[i] = skill;
                    break;
                }
            }
        }
    }

    public void onKSkill(sbyte[] kSkillID)
    {
        Cout.println("GET KEYSKILL!");
        keySkill = new Skill[10];
        if (kSkillID == null)
        {
            loadDefaultKeySkill();
            return;
        }
        for (int i = 0; i < kSkillID.Length; i++)
        {
            for (int j = 0; j < Char.myCharz().vSkillFight.size(); j++)
            {
                Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(j);
                if (skill.template.id == kSkillID[i])
                {
                    keySkill[i] = skill;
                    break;
                }
            }
        }
    }

    public void onCSkill(sbyte[] cSkillID)
    {
        Cout.println("GET CURRENTSKILL!");
        if (cSkillID == null || cSkillID.Length == 0)
        {
            if (Char.myCharz().vSkillFight.size() > 0)
            {
                Char.myCharz().myskill = (Skill)Char.myCharz().vSkillFight.elementAt(0);
            }
        }
        else
        {
            for (int i = 0; i < Char.myCharz().vSkillFight.size(); i++)
            {
                Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
                if (skill.template.id == cSkillID[0])
                {
                    Char.myCharz().myskill = skill;
                    break;
                }
            }
        }
        if (Char.myCharz().myskill != null)
        {
            Service.gI().selectSkill(Char.myCharz().myskill.template.id);
            saveRMSCurrentSkill(Char.myCharz().myskill.template.id);
        }
    }

    private void loadDefaultonScreenSkill()
    {
        Cout.println("LOAD DEFAULT ONmScreen SKILL");
        for (int i = 0; i < onScreenSkill.Length && i < Char.myCharz().vSkillFight.size(); i++)
        {
            Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
            onScreenSkill[i] = skill;
        }
        saveonScreenSkillToRMS();
    }

    private void loadDefaultKeySkill()
    {
        Cout.println("LOAD DEFAULT KEY SKILL");
        for (int i = 0; i < keySkill.Length && i < Char.myCharz().vSkillFight.size(); i++)
        {
            Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
            keySkill[i] = skill;
        }
        saveKeySkillToRMS();
    }

    public void doSetOnScreenSkill(SkillTemplate skillTemplate)
    {
        Skill skill = Char.myCharz().getSkill(skillTemplate);
        MyVector myVector = new MyVector();
        for (int i = 0; i < 10; i++)
        {
            Command command = new(p: new object[2]
            {
                skill,
                i + string.Empty
            }, caption: mResources.into_place + (i + 1), action: 11120);
            Skill skill2 = onScreenSkill[i];
            if (skill2 != null)
            {
                command.isDisplay = true;
            }
            myVector.addElement(command);
        }
        GameCanvas.menu.startAt(myVector, 0);
    }

    public void doSetKeySkill(SkillTemplate skillTemplate)
    {
        Skill skill = Char.myCharz().getSkill(skillTemplate);
        string[] array = ((!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty);
        MyVector myVector = new();
        for (int i = 0; i < 10; i++)
        {
            myVector.addElement(new Command(p: new object[2]
            {
                skill,
                i + string.Empty
            }, caption: array[i], action: 11121));
        }
        GameCanvas.menu.startAt(myVector, 0);
    }

    public void saveonScreenSkillToRMS()
    {
        sbyte[] array = new sbyte[onScreenSkill.Length];
        for (int i = 0; i < onScreenSkill.Length; i++)
        {
            if (onScreenSkill[i] == null)
            {
                array[i] = -1;
            }
            else
            {
                array[i] = onScreenSkill[i].template.id;
            }
        }
        Service.gI().changeOnKeyScr(array);
    }

    public void saveKeySkillToRMS()
    {
        sbyte[] array = new sbyte[keySkill.Length];
        for (int i = 0; i < keySkill.Length; i++)
        {
            if (keySkill[i] == null)
            {
                array[i] = -1;
            }
            else
            {
                array[i] = keySkill[i].template.id;
            }
        }
        Service.gI().changeOnKeyScr(array);
    }

    public void saveRMSCurrentSkill(sbyte id)
    {
    }

    public void addSkillShortcut(Skill skill)
    {
        Cout.println("ADD SKILL SHORTCUT TO SKILL " + skill.template.id);
        for (int i = 0; i < onScreenSkill.Length; i++)
        {
            if (onScreenSkill[i] == null)
            {
                onScreenSkill[i] = skill;
                break;
            }
        }
        for (int j = 0; j < keySkill.Length; j++)
        {
            if (keySkill[j] == null)
            {
                keySkill[j] = skill;
                break;
            }
        }
        if (Char.myCharz().myskill == null)
        {
            Char.myCharz().myskill = skill;
        }
        saveKeySkillToRMS();
        saveonScreenSkillToRMS();
    }

    // --- skillBarPosition ---
    public static void setSkillBarPosition()
    {
        Skill[] array = ((!GameCanvas.isTouch) ? keySkill : onScreenSkill);
        xS = new int[array.Length];
        yS = new int[array.Length];
        if (GameCanvas.isTouchControlSmallScreen && isUseTouch)
        {
            xSkill = 23;
            ySkill = 52;
            padSkill = 5;
            for (int i = 0; i < xS.Length; i++)
            {
                xS[i] = i * (25 + padSkill);
                yS[i] = ySkill;
                if (xS.Length > 5 && i >= xS.Length / 2)
                {
                    xS[i] = (i - xS.Length / 2) * (25 + padSkill);
                    yS[i] = ySkill - 32;
                }
            }
            xHP = array.Length * (25 + padSkill);
            yHP = ySkill;
        }
        else
        {
            wSkill = 30;
            if (GameCanvas.w <= 320)
            {
                ySkill = gH - wSkill - 6;
                xSkill = gW2 - array.Length * wSkill / 2 - 25;
            }
            else
            {
                wSkill = 40;
                xSkill = 10;
                ySkill = GameCanvas.h - wSkill + 7;
            }
            for (int j = 0; j < xS.Length; j++)
            {
                xS[j] = j * wSkill;
                yS[j] = ySkill;
                if (xS.Length > 5 && j >= xS.Length / 2)
                {
                    xS[j] = (j - xS.Length / 2) * wSkill;
                    yS[j] = ySkill - 32;
                }
            }
            xHP = array.Length * wSkill;
            yHP = ySkill;
        }
        if (!GameCanvas.isTouch)
        {
            return;
        }
        xSkill = 17;
        ySkill = GameCanvas.h - 40;
        if (gamePad.isSmallGamePad && isAnalog == 1)
        {
            xHP = array.Length * wSkill;
            yHP = ySkill;
        }
        else
        {
            xHP = GameCanvas.w - 45;
            yHP = GameCanvas.h - 45;
        }
        setTouchBtn();
        for (int k = 0; k < xS.Length; k++)
        {
            xS[k] = k * wSkill;
            yS[k] = ySkill;
            if (xS.Length > 5 && k >= xS.Length / 2)
            {
                xS[k] = (k - xS.Length / 2) * wSkill;
                yS[k] = ySkill - 32;
            }
        }
    }

    // --- attackValidation ---
    public bool isAttack()
    {
        if (checkClickToBotton(Char.myCharz().charFocus))
        {
            return false;
        }
        if (checkClickToBotton(Char.myCharz().mobFocus))
        {
            return false;
        }
        if (checkClickToBotton(Char.myCharz().npcFocus))
        {
            return false;
        }
        if (ChatTextField.gI().isShow)
        {
            return false;
        }
        if (InfoDlg.isLock || Char.myCharz().isLockAttack || Char.isLockKey)
        {
            return false;
        }
        if (Char.myCharz().myskill != null && Char.myCharz().myskill.template.id == 6 && Char.myCharz().itemFocus != null)
        {
            pickItem();
            return false;
        }
        if (Char.myCharz().myskill != null && Char.myCharz().myskill.template.type == 2 && Char.myCharz().npcFocus == null && Char.myCharz().myskill.template.id != 6)
        {
            if (!checkSkillValid())
            {
                return false;
            }
            return true;
        }
        if (Char.myCharz().skillPaint != null || (Char.myCharz().mobFocus == null && Char.myCharz().npcFocus == null && Char.myCharz().charFocus == null && Char.myCharz().itemFocus == null))
        {
            return false;
        }
        if (Char.myCharz().mobFocus != null)
        {
            if (Char.myCharz().mobFocus.isBigBoss() && Char.myCharz().mobFocus.status == 4)
            {
                Char.myCharz().mobFocus = null;
                Char.myCharz().currentMovePoint = null;
            }
            isAutoPlay = true;
            if (!isMeCanAttackMob(Char.myCharz().mobFocus))
            {
                return false;
            }
            if (mobCapcha != null)
            {
                return false;
            }
            if (Char.myCharz().myskill == null)
            {
                return false;
            }
            if (Char.myCharz().isSelectingSkillUseAlone())
            {
                return false;
            }
            int num = -1;
            int num2 = Res.abs(Char.myCharz().cx - cmx) * mGraphics.zoomLevel;
            if (Char.myCharz().charFocus != null)
            {
                num = Res.abs(Char.myCharz().cx - Char.myCharz().charFocus.cx) * mGraphics.zoomLevel;
            }
            else if (Char.myCharz().mobFocus != null)
            {
                num = Res.abs(Char.myCharz().cx - Char.myCharz().mobFocus.x) * mGraphics.zoomLevel;
            }
            if (Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0 || Char.myCharz().myskill.template.type == 4 || num == -1 || num > num2)
            {
                if (Char.myCharz().myskill.template.type == 4)
                {
                    if (Char.myCharz().mobFocus.x < Char.myCharz().cx)
                    {
                        Char.myCharz().cdir = -1;
                    }
                    else
                    {
                        Char.myCharz().cdir = 1;
                    }
                    doSelectSkill(Char.myCharz().myskill, isShortcut: true);
                }
                return false;
            }
            if (!checkSkillValid())
            {
                return false;
            }
            if (Char.myCharz().cx < Char.myCharz().mobFocus.getX())
            {
                Char.myCharz().cdir = 1;
            }
            else
            {
                Char.myCharz().cdir = -1;
            }
            int num3 = Math.abs(Char.myCharz().cx - Char.myCharz().mobFocus.getX());
            int num4 = Math.abs(Char.myCharz().cy - Char.myCharz().mobFocus.getY());
            Char.myCharz().cvx = 0;
            if (num3 <= Char.myCharz().myskill.dx && num4 <= Char.myCharz().myskill.dy)
            {
                if (Char.myCharz().myskill.template.id == 20)
                {
                    return true;
                }
                if (num4 > num3 && Res.abs(Char.myCharz().cy - Char.myCharz().mobFocus.getY()) > 30 && Char.myCharz().mobFocus.getTemplate().type == 4)
                {
                    Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().cx + Char.myCharz().cdir, Char.myCharz().mobFocus.getY());
                    Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
                    GameCanvas.clearKeyHold();
                    GameCanvas.clearKeyPressed();
                    return false;
                }
                int num5 = 20;
                bool flag = false;
                if (Char.myCharz().mobFocus is BigBoss || Char.myCharz().mobFocus is BigBoss2)
                {
                    flag = true;
                }
                if (Char.myCharz().myskill.dx > 100)
                {
                    num5 = 60;
                    if (num3 < 20)
                    {
                        Char.myCharz().createShadow(Char.myCharz().cx, Char.myCharz().cy, 10);
                    }
                }
                bool flag2 = false;
                if ((TileMap.tileTypeAtPixel(Char.myCharz().cx, Char.myCharz().cy + 3) & 2) == 2)
                {
                    int num6 = ((Char.myCharz().cx > Char.myCharz().mobFocus.getX()) ? 1 : (-1));
                    if ((TileMap.tileTypeAtPixel(Char.myCharz().mobFocus.getX() + num5 * num6, Char.myCharz().cy + 3) & 2) != 2)
                    {
                        flag2 = true;
                    }
                }
                if (num3 <= num5 && !flag2)
                {
                    if (Char.myCharz().cx > Char.myCharz().mobFocus.getX())
                    {
                        Char.myCharz().cx = Char.myCharz().mobFocus.getX() + num5 + (flag ? 30 : 0);
                        Char.myCharz().cdir = -1;
                    }
                    else
                    {
                        Char.myCharz().cx = Char.myCharz().mobFocus.getX() - num5 - (flag ? 30 : 0);
                        Char.myCharz().cdir = 1;
                    }
                    Service.gI().charMove();
                }
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
                return true;
            }
            bool flag3 = false;
            if (Char.myCharz().mobFocus is BigBoss || Char.myCharz().mobFocus is BigBoss2)
            {
                flag3 = true;
            }
            int num7 = (Char.myCharz().myskill.dx - ((!flag3) ? 20 : 50)) * ((Char.myCharz().cx > Char.myCharz().mobFocus.getX()) ? 1 : (-1));
            if (num3 <= Char.myCharz().myskill.dx)
            {
                num7 = 0;
            }
            Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().mobFocus.getX() + num7, Char.myCharz().mobFocus.getY());
            Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
            return false;
        }
        if (Char.myCharz().npcFocus != null)
        {
            if (Char.myCharz().npcFocus.isHide)
            {
                return false;
            }
            if (Char.myCharz().cx < Char.myCharz().npcFocus.cx)
            {
                Char.myCharz().cdir = 1;
            }
            else
            {
                Char.myCharz().cdir = -1;
            }
            if (Char.myCharz().cx < Char.myCharz().npcFocus.cx)
            {
                Char.myCharz().npcFocus.cdir = -1;
            }
            else
            {
                Char.myCharz().npcFocus.cdir = 1;
            }
            int num8 = Math.abs(Char.myCharz().cx - Char.myCharz().npcFocus.cx);
            int num9 = Math.abs(Char.myCharz().cy - Char.myCharz().npcFocus.cy);
            if (num9 > 40)
            {
                Char.myCharz().cy = Char.myCharz().npcFocus.cy - 40;
            }
            if (num8 < 60)
            {
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
                if (tMenuDelay == 0)
                {
                    if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId == 0)
                    {
                        if (Char.myCharz().taskMaint.index < 4 && Char.myCharz().npcFocus.template.npcTemplateId == 4)
                        {
                            return false;
                        }
                        if (Char.myCharz().taskMaint.index < 3 && Char.myCharz().npcFocus.template.npcTemplateId == 3)
                        {
                            return false;
                        }
                    }
                    tMenuDelay = 50;
                    InfoDlg.showWait();
                    Service.gI().charMove();
                    Service.gI().openMenu(Char.myCharz().npcFocus.template.npcTemplateId);
                }
            }
            else
            {
                int num10 = (20 + Res.r.nextInt(20)) * ((Char.myCharz().cx > Char.myCharz().npcFocus.cx) ? 1 : (-1));
                Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().npcFocus.cx + num10, Char.myCharz().cy);
                Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
            }
            return false;
        }
        if (Char.myCharz().charFocus != null)
        {
            if (mobCapcha != null)
            {
                return false;
            }
            if (Char.myCharz().cx < Char.myCharz().charFocus.cx)
            {
                Char.myCharz().cdir = 1;
            }
            else
            {
                Char.myCharz().cdir = -1;
            }
            int num11 = Math.abs(Char.myCharz().cx - Char.myCharz().charFocus.cx);
            int num12 = Math.abs(Char.myCharz().cy - Char.myCharz().charFocus.cy);
            if (Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus) || Char.myCharz().isSelectingSkillBuffToPlayer())
            {
                if (Char.myCharz().myskill == null)
                {
                    return false;
                }
                if (!checkSkillValid())
                {
                    return false;
                }
                if (Char.myCharz().cx < Char.myCharz().charFocus.cx)
                {
                    Char.myCharz().cdir = 1;
                }
                else
                {
                    Char.myCharz().cdir = -1;
                }
                Char.myCharz().cvx = 0;
                if (num11 <= Char.myCharz().myskill.dx && num12 <= Char.myCharz().myskill.dy)
                {
                    if (Char.myCharz().myskill.template.id == 20)
                    {
                        return true;
                    }
                    int num13 = 20;
                    if (Char.myCharz().myskill.dx > 60)
                    {
                        num13 = 60;
                        if (num11 < 20)
                        {
                            Char.myCharz().createShadow(Char.myCharz().cx, Char.myCharz().cy, 10);
                        }
                    }
                    bool flag4 = false;
                    if ((TileMap.tileTypeAtPixel(Char.myCharz().cx, Char.myCharz().cy + 3) & 2) == 2)
                    {
                        int num14 = ((Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : (-1));
                        if ((TileMap.tileTypeAtPixel(Char.myCharz().charFocus.cx + num13 * num14, Char.myCharz().cy + 3) & 2) != 2)
                        {
                            flag4 = true;
                        }
                    }
                    if (num11 <= num13 && !flag4)
                    {
                        if (Char.myCharz().cx > Char.myCharz().charFocus.cx)
                        {
                            Char.myCharz().cx = Char.myCharz().charFocus.cx + num13;
                            Char.myCharz().cdir = -1;
                        }
                        else
                        {
                            Char.myCharz().cx = Char.myCharz().charFocus.cx - num13;
                            Char.myCharz().cdir = 1;
                        }
                        Service.gI().charMove();
                    }
                    GameCanvas.clearKeyHold();
                    GameCanvas.clearKeyPressed();
                    return true;
                }
                int num15 = (Char.myCharz().myskill.dx - 20) * ((Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : (-1));
                if (num11 <= Char.myCharz().myskill.dx)
                {
                    num15 = 0;
                }
                Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().charFocus.cx + num15, Char.myCharz().charFocus.cy);
                Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
                return false;
            }
            if (num11 < 60 && num12 < 40)
            {
                playerMenu(Char.myCharz().charFocus);
                if (!GameCanvas.isTouch && Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && popUpYesNo == null)
                {
                    GameCanvas.panel.setTypePlayerMenu(Char.myCharz().charFocus);
                    GameCanvas.panel.show();
                    Service.gI().getPlayerMenu(Char.myCharz().charFocus.charID);
                    Service.gI().messagePlayerMenu(Char.myCharz().charFocus.charID);
                }
            }
            else
            {
                int num16 = (20 + Res.r.nextInt(20)) * ((Char.myCharz().cx > Char.myCharz().charFocus.cx) ? 1 : (-1));
                Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().charFocus.cx + num16, Char.myCharz().charFocus.cy);
                Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
                GameCanvas.clearKeyHold();
                GameCanvas.clearKeyPressed();
            }
            return false;
        }
        if (Char.myCharz().itemFocus != null)
        {
            pickItem();
            return false;
        }
        return true;
    }

    public bool isMeCanAttackMob(Mob m)
    {
        if (m == null)
        {
            return false;
        }
        if (Char.myCharz().cTypePk == 5)
        {
            return true;
        }
        if (Char.myCharz().isAttacPlayerStatus() && !m.isMobMe)
        {
            return false;
        }
        if (Char.myCharz().mobMe != null && m.Equals(Char.myCharz().mobMe))
        {
            return false;
        }
        Char @char = findCharInMap(m.mobId);
        if (@char == null)
        {
            return true;
        }
        if (@char.cTypePk == 5)
        {
            return true;
        }
        if (Char.myCharz().isMeCanAttackOtherPlayer(@char))
        {
            return true;
        }
        return false;
    }

    private bool checkSkillValid()
    {
        if (Char.myCharz().myskill != null && ((Char.myCharz().myskill.template.manaUseType != 1 && Char.myCharz().cMP < Char.myCharz().myskill.manaUse) || (Char.myCharz().myskill.template.manaUseType == 1 && Char.myCharz().cMP < Char.myCharz().cMPFull * Char.myCharz().myskill.manaUse / 100)))
        {
            info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
            auto = 0;
            return false;
        }
        if (Char.myCharz().myskill == null || (Char.myCharz().myskill.template.maxPoint > 0 && Char.myCharz().myskill.point == 0))
        {
            GameCanvas.startOKDlg(mResources.SKILL_FAIL);
            return false;
        }
        return true;
    }

    private bool checkSkillValid2()
    {
        if (Char.myCharz().myskill != null && ((Char.myCharz().myskill.template.manaUseType != 1 && Char.myCharz().cMP < Char.myCharz().myskill.manaUse) || (Char.myCharz().myskill.template.manaUseType == 1 && Char.myCharz().cMP < Char.myCharz().cMPFull * Char.myCharz().myskill.manaUse / 100)))
        {
            return false;
        }
        if (Char.myCharz().myskill == null || (Char.myCharz().myskill.template.maxPoint > 0 && Char.myCharz().myskill.point == 0))
        {
            return false;
        }
        return true;
    }

    // --- doFire_useSkills ---
    public void doFire(bool isFireByShortCut, bool skipWaypoint)
    {
        tam++;
        Waypoint waypoint = Char.myCharz().isInEnterOfflinePoint();
        Waypoint waypoint2 = Char.myCharz().isInEnterOnlinePoint();
        if (!skipWaypoint && waypoint != null && (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.templateId == 0)))
        {
            waypoint.popup.command.performAction();
        }
        else if (!skipWaypoint && waypoint2 != null && (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.templateId == 0)))
        {
            waypoint2.popup.command.performAction();
        }
        else
        {
            if ((TileMap.mapID == 51 && Char.myCharz().npcFocus != null) || Char.myCharz().statusMe == 14)
            {
                return;
            }
            Char.myCharz().cvx = (Char.myCharz().cvy = 0);
            if (Char.myCharz().isSelectingSkillUseAlone() && Char.myCharz().focusToAttack())
            {
                if (checkSkillValid())
                {
                    Char.myCharz().currentFireByShortcut = isFireByShortCut;
                    Char.myCharz().useSkillNotFocus();
                }
            }
            else if (isAttack())
            {
                if (Char.myCharz().isUseChargeSkill() && Char.myCharz().focusToAttack())
                {
                    if (checkSkillValid())
                    {
                        Char.myCharz().currentFireByShortcut = isFireByShortCut;
                        Char.myCharz().sendUseChargeSkill();
                    }
                    else
                    {
                        Char.myCharz().stopUseChargeSkill();
                    }
                }
                else
                {
                    bool flag = TileMap.tileTypeAt(Char.myCharz().cx, Char.myCharz().cy, 2);
                    Char.myCharz().setSkillPaint(sks[Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
                    if (flag)
                    {
                        Char.myCharz().delayFall = 20;
                    }
                    Char.myCharz().currentFireByShortcut = isFireByShortCut;
                }
            }
            if (Char.myCharz().isSelectingSkillBuffToPlayer())
            {
                auto = 0;
            }
        }
    }

    private void askToPick()
    {
        Npc npc = new Npc(5, 0, -100, 100, 5, info1.charId[Char.myCharz().cgender][2]);
        string nhatvatpham = mResources.nhatvatpham;
        string[] menu = new string[2]
        {
            mResources.YES,
            mResources.NO
        };
        npc.idItem = 673;
        gI().createMenu(menu, npc);
        ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
    }

    private void pickItem()
    {
        if (Char.myCharz().itemFocus == null)
        {
            return;
        }
        if (Char.myCharz().cx < Char.myCharz().itemFocus.x)
        {
            Char.myCharz().cdir = 1;
        }
        else
        {
            Char.myCharz().cdir = -1;
        }
        int num = Math.abs(Char.myCharz().cx - Char.myCharz().itemFocus.x);
        int num2 = Math.abs(Char.myCharz().cy - Char.myCharz().itemFocus.y);
        if (num <= 40 && num2 < 40)
        {
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
            if (Char.myCharz().itemFocus.template.id != 673)
            {
                Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
            }
            else
            {
                askToPick();
            }
        }
        else
        {
            Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
            Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
        }
    }

    public bool isCharging()
    {
        if (Char.myCharz().isFlyAndCharge || Char.myCharz().isUseSkillAfterCharge || Char.myCharz().isStandAndCharge || Char.myCharz().isWaitMonkey || isSuperPower || Char.myCharz().isFreez)
        {
            return true;
        }
        return false;
    }

    public void doSelectSkill(Skill skill, bool isShortcut)
    {
        if (Char.myCharz().isCreateDark || isCharging() || Char.myCharz().taskMaint.taskId <= 1)
        {
            return;
        }
        Char.myCharz().myskill = skill;
        if (lastSkill != skill && lastSkill != null)
        {
            Service.gI().selectSkill(skill.template.id);
            saveRMSCurrentSkill(skill.template.id);
            lastSkill = skill;
            selectedIndexSkill = -1;
            gI().auto = 0;
            return;
        }
        if (Char.myCharz().isUseSkillSpec())
        {
            Char.myCharz().sendNewAttack(skill.template.id);
            saveRMSCurrentSkill(skill.template.id);
            lastSkill = skill;
            selectedIndexSkill = -1;
            gI().auto = 0;
            return;
        }
        if (Char.myCharz().isSelectingSkillUseAlone())
        {
            doUseSkillNotFocus(skill);
            lastSkill = skill;
            return;
        }
        selectedIndexSkill = -1;
        if (skill == null)
        {
            return;
        }
        if (lastSkill != skill)
        {
            Service.gI().selectSkill(skill.template.id);
            saveRMSCurrentSkill(skill.template.id);
        }
        if (Char.myCharz().charFocus != null || !Char.myCharz().isSelectingSkillBuffToPlayer())
        {
            if (Char.myCharz().focusToAttack())
            {
                doFire(isShortcut, skipWaypoint: true);
                doSeleckSkillFlag = true;
            }
            lastSkill = skill;
        }
    }

    public void doUseSkill(Skill skill, bool isShortcut)
    {
        if ((TileMap.mapID == 112 || TileMap.mapID == 113) && Char.myCharz().cTypePk == 0)
        {
            return;
        }
        if (Char.myCharz().isSelectingSkillUseAlone())
        {
            doUseSkillNotFocus(skill);
            return;
        }
        selectedIndexSkill = -1;
        if (skill != null)
        {
            Service.gI().selectSkill(skill.template.id);
            saveRMSCurrentSkill(skill.template.id);
            resetButton();
            Char.myCharz().myskill = skill;
            doFire(isShortcut, skipWaypoint: true);
        }
    }

    public void doUseSkillNotFocus(Skill skill)
    {
        if (((TileMap.mapID != 112 && TileMap.mapID != 113) || Char.myCharz().cTypePk != 0) && checkSkillValid())
        {
            selectedIndexSkill = -1;
            if (skill != null)
            {
                Service.gI().selectSkill(skill.template.id);
                saveRMSCurrentSkill(skill.template.id);
                resetButton();
                Char.myCharz().myskill = skill;
                Char.myCharz().useSkillNotFocus();
                Char.myCharz().currentFireByShortcut = true;
                auto = 0;
            }
        }
    }

    public void sortSkill()
    {
        for (int i = 0; i < Char.myCharz().vSkillFight.size() - 1; i++)
        {
            Skill skill = (Skill)Char.myCharz().vSkillFight.elementAt(i);
            for (int j = i + 1; j < Char.myCharz().vSkillFight.size(); j++)
            {
                Skill skill2 = (Skill)Char.myCharz().vSkillFight.elementAt(j);
                if (skill2.template.id < skill.template.id)
                {
                    Skill skill3 = skill2;
                    skill2 = skill;
                    skill = skill3;
                    Char.myCharz().vSkillFight.setElementAt(skill, i);
                    Char.myCharz().vSkillFight.setElementAt(skill2, j);
                }
            }
        }
    }

    // updateKeyTouchCapcha extracted to GameScr.Input.cs

    // touchControl_capcha_mouseChat extracted to GameScr.Touch.cs

    public void setCharJumpAtt()
    {
        Char.myCharz().cvy = -10;
        Char.myCharz().statusMe = 3;
        Char.myCharz().cp1 = 0;
    }

    public void setCharJump(int cvx)
    {
        if (Char.myCharz().cx - Char.myCharz().cxSend != 0 || Char.myCharz().cy - Char.myCharz().cySend != 0)
        {
            Service.gI().charMove();
        }
        Char.myCharz().cvy = -10;
        Char.myCharz().cvx = cvx;
        Char.myCharz().statusMe = 3;
        Char.myCharz().cp1 = 0;
    }
}
