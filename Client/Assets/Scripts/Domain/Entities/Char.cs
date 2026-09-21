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

    // Lifecycle & frame update logic extracted to Char.Update.cs


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
