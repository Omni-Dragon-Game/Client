using System;
using System.Threading;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- dataInit_readers ---
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

    // paintOngMauPercent extracted to GameScr.HUD.cs

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

    // --- effectEnd_screenChecks ---
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
}
