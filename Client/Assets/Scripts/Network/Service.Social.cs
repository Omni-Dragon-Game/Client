using System;
using Assets.src.g;
using UnityEngine;

public partial class Service
{
    public void sendClientInput(TField[] t)
    {
        Message message = null;
        try
        {
            Res.outz(" gui input ");
            message = new Message((sbyte)(-125));
            Res.outz("byte lent = " + t.Length);
            message.writer().writeByte(t.Length);
            for (int i = 0; i < t.Length; i++)
            {
                message.writer().writeUTF(t[i].getText());
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

    public void friend(sbyte action, int playerId)
    {
        Res.outz("add friend");
        Message message = null;
        try
        {
            message = new Message((sbyte)(-80));
            message.writer().writeByte(action);
            if (playerId != -1)
            {
                message.writer().writeInt(playerId);
            }
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

    public void getArchivemnt(int index)
    {
        Res.outz("get ngoc");
        Message message = null;
        try
        {
            message = new Message((sbyte)(-76));
            message.writer().writeByte(index);
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

    public void getPlayerMenu(int playerID)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-79));
            message.writer().writeInt(playerID);
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

    public void clanImage(sbyte id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-62));
            message.writer().writeByte(id);
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

    public void clanDonate(int id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-54));
            message.writer().writeInt(id);
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

    public void clanMessage(int type, string text, int clanID)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-51));
            message.writer().writeByte(type);
            if (type == 0)
            {
                message.writer().writeUTF(text);
            }
            if (type == 2)
            {
                message.writer().writeInt(clanID);
            }
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

    public void joinClan(int id, sbyte action)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-49));
            message.writer().writeInt(id);
            message.writer().writeByte(action);
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

    public void clanMember(int id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-50));
            message.writer().writeInt(id);
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

    public void searchClan(string text)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-47));
            message.writer().writeUTF(text);
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

    public void requestClan(short id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-53));
            message.writer().writeShort(id);
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

    public void clanRemote(int id, sbyte role)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-56));
            message.writer().writeInt(id);
            message.writer().writeByte(role);
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

    public void leaveClan()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-55));
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

    public void clanInvite(sbyte action, int playerID, int clanID, int code)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-57));
            message.writer().writeByte(action);
            if (action == 0)
            {
                message.writer().writeInt(playerID);
            }
            if (action == 1 || action == 2)
            {
                message.writer().writeInt(clanID);
                message.writer().writeInt(code);
            }
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

    public void getClan(sbyte action, sbyte id, string text)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-46));
            message.writer().writeByte(action);
            if (action == 2 || action == 4)
            {
                message.writer().writeByte(id);
                message.writer().writeUTF(text);
            }
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

    public void updateCaption(sbyte gender)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-41));
            message.writer().writeByte(gender);
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

    public void confirmMenu(short npcID, sbyte select)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)32);
            message.writer().writeShort(npcID);
            message.writer().writeByte(select);
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

    public void openMenu(int npcId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)33);
            message.writer().writeShort(npcId);
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

    public void menu(int npcId, int menuId, int optionId)
    {
        Cout.println("menuid: " + menuId);
        Message message = null;
        try
        {
            message = new Message((sbyte)22);
            message.writer().writeByte(npcId);
            message.writer().writeByte(menuId);
            message.writer().writeByte(optionId);
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

    public void menuId(short menuId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)27);
            message.writer().writeShort(menuId);
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

    public void textBoxId(short menuId, string str)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)88);
            message.writer().writeShort(menuId);
            message.writer().writeUTF(str);
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

    public void chat(string text)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)44);
            message.writer().writeUTF(text);
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

    public void addFriend(string name)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)53);
            message.writer().writeUTF(name);
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

    public void addPartyAccept(int charId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)76);
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

    public void addPartyCancel(int charId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)77);
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

    public void testInvite(int charId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)59);
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

    public void addParty(string name)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)75);
            message.writer().writeUTF(name);
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

    public void outParty()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)79);
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

    public void pleaseInputParty(string str)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)16);
            message.writer().writeUTF(str);
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

    public void acceptPleaseParty(string str)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)17);
            message.writer().writeUTF(str);
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

    public void chatPlayer(string text, int id)
    {
        Res.outz("chat player text = " + text);
        Message message = null;
        try
        {
            message = new Message((sbyte)(-72));
            message.writer().writeInt(id);
            message.writer().writeUTF(text);
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

    public void chatGlobal(string text)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-71));
            message.writer().writeUTF(text);
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

    public void chatPrivate(string to, string text)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)91);
            message.writer().writeUTF(to);
            message.writer().writeUTF(text);
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

    public void sendCardInfo(string NAP, string PIN)
    {
        Message message = null;
        try
        {
            message = messageNotMap(16);
            message.writer().writeUTF(NAP);
            message.writer().writeUTF(PIN);
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

    public void inviteClanDun(string name)
    {
        Message message = null;
        try
        {
            message = messageNotMap(34);
            message.writer().writeUTF(name);
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

    public void sendTop(string topName, sbyte selected)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)-96);
            message.writer().writeUTF(topName);
            message.writer().writeByte(selected);
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

    public void enemy(sbyte b, int charID)
    {
        Message message = null;
        Res.outz("add enemy");
        try
        {
            message = new Message((sbyte)(-99));
            message.writer().writeByte(b);
            if (b == 1 || b == 2)
            {
                message.writer().writeInt(charID);
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

    public void getFlag(sbyte action, sbyte flagType)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-103));
            message.writer().writeByte(action);
            Res.outz("------------service--  " + action + "   " + flagType);
            if (action != 0)
            {
                message.writer().writeByte(flagType);
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

    public void messagePlayerMenu(int charId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-30));
            message.writer().writeByte((sbyte)63);
            message.writer().writeInt(charId);
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

    public void playerMenuAction(int charId, short select)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-30));
            message.writer().writeByte((sbyte)64);
            message.writer().writeInt(charId);
            message.writer().writeShort(select);
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

}
