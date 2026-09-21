using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart6(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case -7:
                    {
                        int num177 = msg.reader().readInt();
                        for (int num180 = 0; num180 < GameScr.vCharInMap.size(); num180++)
                        {
                            Char char14 = null;
                            try
                            {
                                char14 = (Char)GameScr.vCharInMap.elementAt(num180);
                            }
                            catch (Exception)
                            {
                            }
                            if (char14 == null)
                            {
                                break;
                            }
                            if (char14.charID == num177)
                            {
                                GameCanvas.debug("SA8x2y" + num180, 2);
                                char14.moveTo(msg.reader().readShort(), msg.reader().readShort(), 0);
                                char14.lastUpdateTime = mSystem.currentTimeMillis();
                                break;
                            }
                        }
                        GameCanvas.debug("SA80x3", 2);
                        break;
                    }
                case -6:
                    {
                        GameCanvas.debug("SA81", 2);
                        int num177 = msg.reader().readInt();
                        for (int num178 = 0; num178 < GameScr.vCharInMap.size(); num178++)
                        {
                            Char char13 = (Char)GameScr.vCharInMap.elementAt(num178);
                            if (char13 != null && char13.charID == num177)
                            {
                                if (!char13.isInvisiblez && !char13.isUsePlane)
                                {
                                    ServerEffect.addServerEffect(60, char13.cx, char13.cy, 1);
                                }
                                if (!char13.isUsePlane)
                                {
                                    GameScr.vCharInMap.removeElementAt(num178);
                                }
                                return;
                            }
                        }
                        break;
                    }
                case -13:
                    {
                        int num189 = msg.reader().readUnsignedByte();
                        if (num189 > GameScr.vMob.size() - 1 || num189 < 0)
                        {
                            return;
                        }
                        Mob mob9 = (Mob)GameScr.vMob.elementAt(num189);
                        if (mob9.status != 0 && mob9.status != 1)
                        {
                            return;
                        }
                        mob9.sys = msg.reader().readByte();
                        mob9.levelBoss = msg.reader().readByte();
                        if (mob9.levelBoss != 0)
                        {
                            mob9.typeSuperEff = Res.random(0, 3);
                        }
                        mob9.x = mob9.xFirst;
                        mob9.y = mob9.yFirst;
                        mob9.status = 5;
                        mob9.injureThenDie = false;
                        mob9.hp = msg.readLong();
                        mob9.maxHp = mob9.hp;
                        mob9.updateHp_bar();
                        ServerEffect.addServerEffect(60, mob9.x, mob9.y, 1);
                        break;
                    }
                case -75:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 != null)
                        {
                            mob9.levelBoss = msg.reader().readByte();
                            if (mob9.levelBoss > 0)
                            {
                                mob9.typeSuperEff = Res.random(0, 3);
                            }
                        }
                        break;
                    }
                case -9:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 != null)
                        {
                            mob9.hp = msg.readLong();
                            mob9.updateHp_bar();
                            long dame = msg.readLong();
                            if (dame == 1)
                            {
                                return;
                            }
                            if (dame > 1)
                            {
                                mob9.setInjure();
                            }
                            bool flag10 = false;
                            try
                            {
                                flag10 = msg.reader().readBoolean();
                            }
                            catch (Exception)
                            {
                            }
                            sbyte b72 = msg.reader().readByte();
                            if (b72 != -1)
                            {
                                EffecMn.addEff(new Effect(b72, mob9.x, mob9.getY(), 3, 1, -1));
                            }
                            if (flag10)
                            {
                                GameScr.startFlyText("-" + dame, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.FATAL);
                            }
                            else if (dame == 0)
                            {
                                mob9.x = mob9.xFirst;
                                mob9.y = mob9.yFirst;
                                GameScr.startFlyText(mResources.miss, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.MISS);
                            }
                            else if (dame > 1)
                            {
                                GameScr.startFlyText("-" + dame, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.ORANGE);
                            }
                        }
                        break;
                    }
                case 45:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception ex28)
                        {
                        }
                        if (mob9 != null)
                        {
                            mob9.hp = msg.reader().readInt();
                            mob9.updateHp_bar();
                            GameScr.startFlyText(mResources.miss, mob9.x, mob9.y - mob9.h, 0, -2, mFont.MISS);
                        }
                        break;
                    }
                case -12:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 == null || mob9.status == 0 || mob9.status == 0)
                        {
                            break;
                        }
                        mob9.startDie();
                        try
                        {
                            long dameHit = msg.readLong();
                            if (msg.reader().readBool())
                            {
                                GameScr.startFlyText("-" + dameHit, mob9.x, mob9.y - mob9.h, 0, -2, mFont.FATAL);
                            }
                            else
                            {
                                GameScr.startFlyText("-" + dameHit, mob9.x, mob9.y - mob9.h, 0, -2, mFont.ORANGE);
                            }
                            sbyte b76 = msg.reader().readByte();
                            for (int num191 = 0; num191 < b76; num191++)
                            {
                                ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob9.x, mob9.y, msg.reader().readShort(), msg.reader().readShort());
                                int num192 = (itemMap4.playerId = msg.reader().readInt());
                                GameScr.vItemMap.addElement(itemMap4);
                                if (Res.abs(itemMap4.y - Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - Char.myCharz().cx) < 24)
                                {
                                    Char.myCharz().charFocus = null;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    }
                case 74:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 != null && mob9.status != 0 && mob9.status != 0)
                        {
                            mob9.status = 0;
                            ServerEffect.addServerEffect(60, mob9.x, mob9.y, 1);
                            ItemMap itemMap3 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob9.x, mob9.y, msg.reader().readShort(), msg.reader().readShort());
                            GameScr.vItemMap.addElement(itemMap3);
                            if (Res.abs(itemMap3.y - Char.myCharz().cy) < 24 && Res.abs(itemMap3.x - Char.myCharz().cx) < 24)
                            {
                                Char.myCharz().charFocus = null;
                            }
                        }
                        break;
                    }
                case -11:
                    {
                        Mob mob9 = null;
                        try
                        {
                            int index4 = msg.reader().readUnsignedByte();
                            mob9 = (Mob)GameScr.vMob.elementAt(index4);
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 != null)
                        {
                            Char.myCharz().isDie = false;
                            Char.isLockKey = false;
                            long dame = msg.readLong();
                            int num175;
                            try
                            {
                                num175 = msg.readInt3Byte();
                            }
                            catch (Exception)
                            {
                                num175 = 0;
                            }
                            if (mob9.isBusyAttackSomeOne)
                            {
                                Char.myCharz().doInjure(dame, num175, isCrit: false, isMob: true);
                                break;
                            }
                            mob9.dame = dame;
                            mob9.dameMp = num175;
                            mob9.setAttack(Char.myCharz());
                        }
                        break;
                    }
                case -10:
                    {
                        Mob mob9 = null;
                        try
                        {
                            mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
                        }
                        catch (Exception)
                        {
                        }
                        if (mob9 != null)
                        {
                            @char = GameScr.findCharInMap(msg.reader().readInt());
                            if (@char == null)
                            {
                                return;
                            }
                            long cHP = msg.readLong();
                            mob9.dame = @char.cHP - cHP;
                            @char.cHPNew = cHP;
                            try
                            {
                                @char.cMP = msg.readInt3Byte();
                            }
                            catch (Exception)
                            {
                            }
                            if (mob9.isBusyAttackSomeOne)
                            {
                                @char.doInjure(mob9.dame, 0, isCrit: false, isMob: true);
                            }
                            else
                            {
                                mob9.setAttack(@char);
                            }
                        }
                        break;
                    }
                case -17:
                    Char.myCharz().meDead = true;
                    Char.myCharz().cPk = msg.reader().readByte();
                    Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
                    try
                    {
                        Char.myCharz().cPower = msg.reader().readLong();
                        Char.myCharz().applyCharLevelPercent();
                    }
                    catch (Exception)
                    {
                        Cout.println("Loi tai ME_DIE " + msg.command);
                    }
                    Char.myCharz().countKill = 0;
                    break;
                case 66:
                    break;
                case -8:
                    @char = GameScr.findCharInMap(msg.reader().readInt());
                    if (@char == null)
                    {
                        return;
                    }
                    @char.cPk = msg.reader().readByte();
                    @char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
                    break;
                case -16:
                    if (Char.myCharz().wdx != 0 || Char.myCharz().wdy != 0)
                    {
                        Char.myCharz().cx = Char.myCharz().wdx;
                        Char.myCharz().cy = Char.myCharz().wdy;
                        Char.myCharz().wdx = (Char.myCharz().wdy = 0);
                    }
                    Char.myCharz().liveFromDead();
                    Char.myCharz().isLockMove = false;
                    Char.myCharz().meDead = false;
                    break;
                case 44:
                    {
                        int num176 = msg.reader().readInt();
                        string text8 = msg.reader().readUTF();
                        @char = ((Char.myCharz().charID != num176) ? GameScr.findCharInMap(num176) : Char.myCharz());
                        if (@char == null)
                        {
                            return;
                        }
                        @char.addInfo(text8);
                        break;
                    }
                case 18:
                    {
                        sbyte b70 = msg.reader().readByte();
                        for (int num173 = 0; num173 < b70; num173++)
                        {
                            int charId = msg.reader().readInt();
                            int cx = msg.reader().readShort();
                            int cy = msg.reader().readShort();
                            int cHPShow = msg.readInt3Byte();
                            Char char12 = GameScr.findCharInMap(charId);
                            if (char12 != null)
                            {
                                char12.cx = cx;
                                char12.cy = cy;
                                char12.cHP = (char12.cHPShow = cHPShow);
                                char12.lastUpdateTime = mSystem.currentTimeMillis();
                            }
                        }
                        break;
                    }
                case 19:
                    Char.myCharz().countKill = msg.reader().readUnsignedShort();
                    Char.myCharz().countKillMax = msg.reader().readUnsignedShort();
                    break;
            }
    }
}

