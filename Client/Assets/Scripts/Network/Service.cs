using System;
using Assets.src.g;
using UnityEngine;

public partial class Service
{
    private ISession session = Session_ME.gI();

    protected static Service instance;

    public static long curCheckController;

    public static long curCheckMap;

    public static long logController;

    public static long logMap;

    public int demGui;

    public static bool reciveFromMainSession;

    public static Service gI()
    {
        if (instance == null)
        {
            instance = new Service();
        }
        return instance;
    }


    public void androidPack()
    {
        if (mSystem.android_pack == null)
        {
            return;
        }
        Message message = null;
        try
        {
            message = new Message((sbyte)126);
            message.writer().writeUTF(mSystem.android_pack);
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

    public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)42);
            message.writer().writeUTF(day);
            message.writer().writeUTF(month);
            message.writer().writeUTF(year);
            message.writer().writeUTF(address);
            message.writer().writeUTF(cmnd);
            message.writer().writeUTF(dayCmnd);
            message.writer().writeUTF(noiCapCmnd);
            message.writer().writeUTF(sdt);
            message.writer().writeUTF(name);
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

    public void androidPack2()
    {
        if (mSystem.android_pack == null)
        {
            return;
        }
        Message message = null;
        try
        {
            message = new Message((sbyte)126);
            message.writer().writeUTF(mSystem.android_pack);
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

    public void checkAd(sbyte status)
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-44));
            message.writer().writeByte(status);
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





    public void test2()
    {
        Res.outz("gui test1");
        Message message = null;
        try
        {
            message = new Message(1);
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

    public void testJoint()
    {
    }

    public void mobCapcha(char ch)
    {
        Res.outz("cap char c= " + ch);
        Message message = null;
        try
        {
            message = new Message((sbyte)(-85));
            message.writer().writeChar(ch);
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




















    public Message messageNotLogin(sbyte command)
    {
        Message message = new Message((sbyte)(-29));
        message.writer().writeByte(command);
        return message;
    }

    public Message messageNotMap(sbyte command)
    {
        Message message = new Message((sbyte)(-28));
        message.writer().writeByte(command);
        return message;
    }

    public static Message messageSubCommand(sbyte command)
    {
        Message message = new Message((sbyte)(-30));
        message.writer().writeByte(command);
        return message;
    }

    public void setClientType()
    {
        if (Rms.loadRMSInt("clienttype") != -1)
        {
            Main.typeClient = Rms.loadRMSInt("clienttype");
        }
        try
        {
            Message message = messageNotLogin(2);
            message.writer().writeByte(Main.typeClient);
            message.writer().writeByte(mGraphics.zoomLevel);
            message.writer().writeBoolean(value: false);
            message.writer().writeInt(GameCanvas.w);
            message.writer().writeInt(GameCanvas.h);
            message.writer().writeBoolean(TField.isQwerty);
            message.writer().writeBoolean(GameCanvas.isTouch);
            message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
            DataInputStream dataInputStream = MyStream.readFile("/info");
            if (dataInputStream != null)
            {
                sbyte[] data = new sbyte[dataInputStream.r.buffer.Length];
                dataInputStream.read(ref data);
                if (data != null)
                {
                    message.writer().writeShort(data.Length);
                    message.writer().write(data);
                    Res.err("write " + data.Length + "|" + GameMidlet.VERSION);
                }
            }
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        SendRemoteAddress();
    }

    public void setClientType2()
    {
        Res.outz("SET CLIENT TYPE");
        if (Rms.loadRMSInt("clienttype") != -1)
        {
            mSystem.clientType = Rms.loadRMSInt("clienttype");
        }
        try
        {
            Res.outz("setType");
            Message message = messageNotLogin(2);
            message.writer().writeByte(mSystem.clientType);
            message.writer().writeByte(mGraphics.zoomLevel);
            Res.outz("gui zoomlevel = " + mGraphics.zoomLevel);
            message.writer().writeBoolean(value: false);
            message.writer().writeInt(GameCanvas.w);
            message.writer().writeInt(GameCanvas.h);
            message.writer().writeBoolean(TField.isQwerty);
            message.writer().writeBoolean(GameCanvas.isTouch);
            message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
            DataInputStream dataInputStream = MyStream.readFile("/info");
            if (dataInputStream != null)
            {
                sbyte[] data = new sbyte[dataInputStream.r.buffer.Length];
                dataInputStream.read(ref data);
                if (data != null)
                {
                    message.writer().writeShort(data.Length);
                    message.writer().write(data);
                    Res.err("write " + data.Length + "|" + GameMidlet.VERSION);
                }
            }
            session = Session_ME2.gI();
            session.sendMessage(message);
            session = Session_ME.gI();
            message.cleanup();
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
        }
    }

    public void sendCheckController()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-120));
            session.sendMessage(message);
        }
        catch (Exception)
        {
        }
        finally
        {
            curCheckController = mSystem.currentTimeMillis();
            message.cleanup();
        }
    }

    public void sendCheckMap()
    {
        Message message = null;
        try
        {
            message = new Message((sbyte)(-121));
            session.sendMessage(message);
        }
        catch (Exception)
        {
        }
        finally
        {
            curCheckMap = mSystem.currentTimeMillis();
            message.cleanup();
        }
    }

    public void login(string username, string pass, string version, sbyte type)
    {
        try
        {
            Message message = messageNotLogin(0);
            message.writer().writeUTF(username);
            message.writer().writeUTF(pass);
            message.writer().writeUTF(version);
            message.writer().writeByte(type);
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
    }

    public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
    {
        try
        {
            Message message = messageNotLogin(1);
            message.writer().writeUTF(username);
            message.writer().writeUTF(pass);
            if (usernameAo != null && !usernameAo.Equals(string.Empty))
            {
                message.writer().writeUTF(usernameAo);
                message.writer().writeUTF("a");
            }
            session.sendMessage(message);
            message.cleanup();
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
    }






    public void selectCharToPlay(string charname)
    {
        Message message = new Message((sbyte)(-28));
        try
        {
            message.writer().writeByte((sbyte)1);
            message.writer().writeUTF(charname);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        session.sendMessage(message);
    }


    public void createChar(string name, int gender, int hair)
    {
        Message message = new Message((sbyte)(-28));
        try
        {
            message.writer().writeByte((sbyte)2);
            message.writer().writeUTF(name);
            message.writer().writeByte(gender);
            message.writer().writeByte(hair);
        }
        catch (Exception ex)
        {
            Cout.println(ex.Message + ex.StackTrace);
        }
        session.sendMessage(message);
    }




































    public void clientOk()
    {
        Message message = null;
        try
        {
            message = messageNotMap(13);
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


















    public void saveRms(string key, sbyte[] data)
    {
        Message message = null;
        try
        {
            message = messageSubCommand(60);
            message.writer().writeUTF(key);
            message.writer().writeInt(data.Length);
            message.writer().write(data);
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

    public void loadRMS(string key)
    {
        Cout.println("REQUEST RMS");
        Message message = null;
        try
        {
            message = messageSubCommand(61);
            message.writer().writeUTF(key);
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







    public void activeAccProtect(int pass)
    {
        Message message = null;
        try
        {
            message = messageNotMap(37);
            message.writer().writeInt(pass);
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

    public void clearAccProtect(int pass)
    {
        Message message = null;
        try
        {
            message = messageNotMap(41);
            message.writer().writeInt(pass);
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

    public void updateActive(int passOld, int passNew)
    {
        Message message = null;
        try
        {
            message = messageNotMap(38);
            message.writer().writeInt(passOld);
            message.writer().writeInt(passNew);
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

    public void openLockAccProtect(int pass2)
    {
        Message message = null;
        try
        {
            message = messageNotMap(39);
            message.writer().writeInt(pass2);
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










    public void login2(string user)
    {
        Res.outz("Login 2");
        Message message = null;
        try
        {
            message = new Message((sbyte)(-101));
            message.writer().writeUTF(user);
            message.writer().writeByte(1);
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




























    public void sendDelAcc()
    {
        Message message = new Message((sbyte)69);
        try
        {
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


    public void SendRemoteAddress()
    {
        
    }
}
