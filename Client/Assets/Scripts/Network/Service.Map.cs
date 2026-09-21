using System;
using Assets.src.g;
using UnityEngine;

public partial class Service
{
    public void gotoPlayer(int id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)18);
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

    public void getTask(int npcTemplateId, int menuId, int optionId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)40);
            message.writer().writeByte(npcTemplateId);
            message.writer().writeByte(menuId);
            if (optionId >= 0)
            {
                message.writer().writeByte(optionId);
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

    public void requestChangeMap()
    {
        Message message = new Message((sbyte)(-23));
        session.sendMessage(message);
        message.cleanup();
    }

    public void magicTree(sbyte type)
    {
        Message message = new Message((sbyte)(-34));
        try
        {
            message.writer().writeByte(type);
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception)
        {
        }
    }

    public void requestChangeZone(int zoneId, int indexUI)
    {
        Message message = new Message((sbyte)21);
        try
        {
            message.writer().writeByte(zoneId);
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception)
        {
        }
    }

    public void selectZone(sbyte sub, int value)
    {
    }

    public void requestModTemplate(int modTemplateId)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)11);
            message.writer().writeByte(modTemplateId);
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

    public void requestNpcTemplate(int npcTemplateId)
    {
        Message message = null;
        try
        {
            message = messageNotMap(12);
            message.writer().writeByte(npcTemplateId);
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

    public void getEffData(short id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-66));
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

    public void openUIZone()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)29);
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

    public void updateData()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)-87);
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
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void updateMap()
    {
        Message message = null;
        try
        {
            message = messageNotMap(6);
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
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void requestMaptemplate(int maptemplateId)
    {
        Message message = null;
        try
        {
            message = messageNotMap(10);
            message.writer().writeByte(maptemplateId);
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

    public void requestPlayerInfo(MyVector chars)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)18);
            message.writer().writeByte(chars.size());
            for (int i = 0; i < chars.size(); i++)
            {
                Char @char = (Char)chars.elementAt(i);
                message.writer().writeInt(@char.charID);
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

    public void clearTask()
    {
        Message message = null;
        try
        {
            message = messageNotMap(17);
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

    public void changeName(string name, int id)
    {
        Message message = null;
        try
        {
            message = messageNotMap(18);
            message.writer().writeInt(id);
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

    public void requestIcon(int id)
    {
        GameCanvas.connect();
        Message message = null;
        try
        {
            message = new Message((sbyte)-67);
            message.writer().writeInt(id);
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
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void getBgTemplate(short id)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-32));
            message.writer().writeShort(id);
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
            Cout.println(ex.Message + ex.StackTrace);
        }
        finally
        {
            message.cleanup();
        }
    }

    public void getMapOffline()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-33));
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

    public void finishUpdate()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-38));
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

    public void finishUpdate(int playerID)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-38));
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

    public void finishLoadMap()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-39));
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

    public void getMagicTree(sbyte action)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-34));
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

    public void getResource(sbyte action, MyVector vResourceIndex)
    {
        Res.outz("request resource action= " + action);
        Message message = null;
        try
        {
            message = new Message((sbyte)(-74));
            message.writer().writeByte(action);
            if (action == 2 && vResourceIndex != null)
            {
                message.writer().writeShort(vResourceIndex.size());
                for (int i = 0; i < vResourceIndex.size(); i++)
                {
                    message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
                }
            }
            if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
            {
                session = Session_ME2.gI();
            }
            else
            {
                reciveFromMainSession = true;
                session = Session_ME.gI();
            }
            session.sendMessage(message);
            session = Session_ME.gI();
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

    public void requestMapSelect(int selected)
    {
        Res.outz("request magic tree");
        Message message = null;
        try
        {
            message = new Message((sbyte)(-91));
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

    public void petInfo()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-107));
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

    public void PetInfo2()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)3);
            message.writer().writeByte(0);  
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

    public void petStatus(sbyte status)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-108));
            message.writer().writeByte(status);
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

    public void pet2Status(sbyte status)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)3);
            message.writer().writeByte(1);
            message.writer().writeByte(status);
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

    public void imageSource(MyVector vID)
    {
        Message message = null;
        try
        {
            Res.outz("IMAGE SOURCE size= " + vID.size());
            message = new Message((sbyte)(-111));
            message.writer().writeShort(vID.size());
            if (vID.size() > 0)
            {
                for (int i = 0; i < vID.size(); i++)
                {
                    Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
                    message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
                }
            }
            if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
            {
                session = Session_ME2.gI();
            }
            else
            {
                session = Session_ME.gI();
                reciveFromMainSession = true;
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

    public void sendServerData(sbyte action, int id, sbyte[] data)
    {
        Message message = null;
        try
        {
            Res.outz("SERVER DATA");
            message = new Message((sbyte)(-110));
            message.writer().writeByte(action);
            if (action == 1)
            {
                message.writer().writeInt(id);
                if (data != null)
                {
                    int num = data.Length;
                    message.writer().writeShort(num);
                    message.writer().write(ref data, 0, num);
                }
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

    public void requestPean()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-114));
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

    public void getImgByName(string nameImg)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)66);
            message.writer().writeUTF(nameImg);
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

}
