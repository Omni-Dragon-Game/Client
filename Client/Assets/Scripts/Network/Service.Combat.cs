using System;
using Assets.src.g;
using UnityEngine;

public partial class Service
{
    public void speacialSkill(sbyte index)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)112);
            message.writer().writeByte(index);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
        }
        finally
        {
            message.cleanup();
        }
    }

    public void skill_not_focus(sbyte status)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)-45);
            message.writer().writeByte(status);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void checkMMove(int second)
    {
        Message message = new Message((sbyte)(-78));
        try
        {
            message.writer().writeInt(second);
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception)
        {
        }
    }

    public void charMove()
    {
        int num = Char.myCharz().cx - Char.myCharz().cxSend;
        int num2 = Char.myCharz().cy - Char.myCharz().cySend;
        if (Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || Char.myCharz().isTeleport || Char.myCharz().cy <= 0 || Char.myCharz().telePortSkill)
        {
            return;
        }
        try
        {
            Message message = new Message((sbyte)(-7));
            Char.myCharz().cxSend = Char.myCharz().cx;
            Char.myCharz().cySend = Char.myCharz().cy;
            Char.myCharz().cdirSend = Char.myCharz().cdir;
            Char.myCharz().cactFirst = Char.myCharz().statusMe;
            if (TileMap.tileTypeAt(Char.myCharz().cx / TileMap.size, Char.myCharz().cy / TileMap.size) == 0)
            {
                message.writer().writeByte((sbyte)1);
                if (Char.myCharz().canFly)
                {
                    if (!Char.myCharz().isHaveMount)
                    {
                        Char.myCharz().cMP -= Char.myCharz().cMPGoc / 100 * ((Char.myCharz().isMonkey != 1) ? 1 : 2);
                    }
                    if (Char.myCharz().cMP < 0)
                    {
                        Char.myCharz().cMP = 0;
                    }
                    GameScr.gI().isInjureMp = true;
                    GameScr.gI().twMp = 0;
                }
            }
            else
            {
                message.writer().writeByte((sbyte)0);
            }
            message.writer().writeShort(Char.myCharz().cx);
            if (num2 != 0)
            {
                message.writer().writeShort(Char.myCharz().cy);
            }
            session.sendMessage(message);
            GameScr.tickMove++;
            message.cleanup();
        }
        catch (Exception ex)
        {
            Cout.LogError("LOI CHAR MOVE " + ex.ToString());
        }
    }

    public void requestSkill(int skillId)
    {
        Message message = null;
        try
        {
            message = messageNotMap(9);
            message.writer().writeShort(skillId);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void upSkill(int skillTemplateId, int point)
    {
        Message message = null;
        try
        {
            message = messageSubCommand(17);
            message.writer().writeShort(skillTemplateId);
            message.writer().writeByte(point);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void selectSkill(int skillTemplateId)
    {
        Cout.println(Char.myCharz().cName + " SELECT SKILL " + skillTemplateId);
        Message message = null;
        try
        {
            message = new Message((sbyte)34);
            message.writer().writeShort(skillTemplateId);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
    {
        try
        {
            Message message = null;
            if (type == 0)
            {
                return;
            }
            if (vMob.size() > 0 && vChar.size() > 0)
            {
                switch (type)
                {
                    case 1:
                        message = new Message((sbyte)-4);
                        break;
                    case 2:
                        message = new Message((sbyte)67);
                        break;
                }
                message.writer().writeByte(vMob.size());
                for (int i = 0; i < vMob.size(); i++)
                {
                    Mob mob = (Mob)vMob.elementAt(i);
                    message.writer().writeByte(mob.mobId);
                }
                for (int j = 0; j < vChar.size(); j++)
                {
                    Char @char = (Char)vChar.elementAt(j);
                    if (@char != null)
                    {
                        message.writer().writeInt(@char.charID);
                    }
                    else
                    {
                        message.writer().writeInt(-1);
                    }
                }
            }
            else if (vMob.size() > 0)
            {
                message = new Message((sbyte)54);
                for (int k = 0; k < vMob.size(); k++)
                {
                    Mob mob2 = (Mob)vMob.elementAt(k);
                    if (!mob2.isMobMe)
                    {
                        message.writer().writeByte(mob2.mobId);
                        continue;
                    }
                    message.writer().writeByte((sbyte)-1);
                    message.writer().writeInt(mob2.mobId);
                }
            }
            else if (vChar.size() > 0)
            {
                message = new Message((sbyte)-60);
                for (int l = 0; l < vChar.size(); l++)
                {
                    Char char2 = (Char)vChar.elementAt(l);
                    message.writer().writeInt(char2.charID);
                }
            }
            message.writer().writeSByte((sbyte)Char.myCharz().cdir);
            if (message != null)
            {
                session.sendMessage(message);
            }
        }
        catch (Exception)
        {
        }
    }

    public void returnTownFromDead()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-15));
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void wakeUpFromDead()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-16));
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void updateSkill()
    {
        Message message = null;
        try
        {
            message = messageNotMap(7);
            if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
            {
                session = Session_ME2.gI();
            }
            else
            {
                session = Session_ME.gI();
            }
            session.sendMessage(message);
            session = Session_ME.gI();
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
        }
        finally
        {
            message.cleanup();
        }
    }

    public void addCuuSat(int charId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)62);
            message.writer().writeInt(charId);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void player_vs_player(sbyte action, sbyte type, int playerId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-59));
            message.writer().writeByte(action);
            message.writer().writeByte(type);
            message.writer().writeInt(playerId);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void upPotential(bool forPet, int typePotential, int num)
    {
        Message message = null;
        try
        {
            message = messageSubCommand((sbyte) (forPet ? 18 : 16));
            message.writer().writeByte(typePotential);
            message.writer().writeShort(num);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void transportNow()
    {
        Message message = null;
        try
        {
            Res.outz("------------transportNow  ");
            message = new Message((sbyte)(-105));
            session.sendMessage(message);
        }
        catch (Exception)
        {
        }
        finally
        {
            message.cleanup();
        }
    }

    public void funsion(sbyte type)
    {
        Message message = null;
        try
        {
            Res.outz("FUNSION");
            message = new Message((sbyte)125);
            message.writer().writeByte(type);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
        }
        finally
        {
            message.cleanup();
        }
    }

    public void sendThachDau(int id)
    {
        Res.outz("GUI THACH DAU");
        Message message = null;
        try
        {
            message = new Message((sbyte)(-118));
            message.writer().writeInt(id);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
        }
        finally
        {
            message.cleanup();
        }
    }

    public void SendCrackBall(byte type, byte soluong)
    {
        Message message = new Message((sbyte)(-127));
        try
        {
            message.writer().writeByte(type);
            if (soluong > 0)
            {
                message.writer().writeByte(soluong);
            }
            session.sendMessage(message);
        }
        catch (Exception)
        {
        }
        finally
        {
            message.cleanup();
        }
    }

    public void SendRada(int i, int id)
    {
        Message message = new Message(sbyte.MaxValue);
        try
        {
            message.writer().writeByte(i);
            if (id != -1)
            {
                message.writer().writeShort(id);
            }
            session.sendMessage(message);
        }
        catch (Exception)
        {
        }
        finally
        {
            message.cleanup();
        }
    }

    public void new_skill_not_focus(sbyte idTemplateSkill, sbyte dir, short x, short y)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)-45);
            message.writer().writeSByte(20);
            message.writer().writeSByte(idTemplateSkill);
            message.writer().writeShort(Char.myCharz().cx);
            message.writer().writeShort(Char.myCharz().cy);
            message.writer().writeSByte(dir);
            message.writer().writeShort(x);
            message.writer().writeShort(y);
            session.sendMessage(message);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

}
