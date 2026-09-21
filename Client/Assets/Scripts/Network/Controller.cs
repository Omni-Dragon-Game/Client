using System;
using System.Security.Cryptography;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller : IMessageHandler
{
    // Fields and connection lifecycle callbacks extracted to Controller.Fields.cs


    public void onMessage(Message msg)
    {
        GameCanvas.debugSession.removeAllElements();
        GameCanvas.debug("SA1", 2);
        
        try
        {
            Debug.Log("<<<Read cmd= " + msg.command);
           
            Char @char = null;
            Mob mob = null;
            MyVector myVector = new MyVector();
            int num = 0;
            GameCanvas.timeLoading = 15;
            Controller2.readMessage(msg);
            switch (msg.command)
            {
                case 70:
                case 0:
                case 24:
                case 20:
                case 66:
                case 65:
                case 112:
                case -98:
                case -97:
                case -96:
                case -94:
                case -95:
                case -92:
                case -91:
                case -90:
                case -88:
                case -87:
                case -86:
                case -85:
                case -112:
                case -84:
                case -83:
                case -82:
                case -81:
                    onMessagePart1(msg);
                    break;
                case -80:
                case -99:
                case -79:
                case -93:
                case -77:
                case -76:
                case -74:
                case -43:
                case -59:
                case -62:
                case -65:
                case -64:
                case -63:
                case -57:
                case -51:
                case -53:
                case -52:
                case -50:
                case -47:
                case -46:
                case -61:
                case -42:
                    onMessagePart2(msg);
                    break;
                case 1:
                case 2:
                case -107:
                case 3:
                case -109:
                case -37:
                case -36:
                case -35:
                case -45:
                case -44:
                    onMessagePart3(msg);
                    break;
                case -41:
                case -34:
                case 11:
                case -69:
                case -68:
                case -67:
                case -66:
                case -32:
                case 92:
                case -26:
                case -25:
                case 94:
                case 47:
                case 81:
                case 82:
                case 85:
                case 86:
                case 87:
                case 56:
                case 83:
                case 84:
                case 46:
                case -29:
                case -28:
                case -30:
                case 62:
                case 63:
                case 64:
                case 39:
                case 57:
                case 58:
                case 88:
                case 27:
                case 33:
                case 40:
                case 41:
                case 50:
                case 43:
                case 90:
                case 29:
                case -21:
                case -20:
                    onMessagePart4(msg);
                    break;
                case -19:
                case -18:
                case 68:
                case 69:
                case -14:
                case -22:
                case -70:
                case 38:
                case 32:
                case 7:
                case 6:
                case -23:
                case -24:
                case -31:
                case -4:
                case 54:
                case -60:
                case -2:
                case 95:
                case 96:
                case 97:
                case -1:
                case -3:
                case -73:
                case -5:
                    onMessagePart5(msg);
                    break;
                case -7:
                case -6:
                case -13:
                case -75:
                case -9:
                case 45:
                case -12:
                case 74:
                case -11:
                case -10:
                case -17:
                case -8:
                case -16:
                case 44:
                case 18:
                case 19:
                    onMessagePart6(msg);
                    break;
            }
        }
        catch (Exception ex40)
        {
        }
        finally
        {
            msg?.cleanup();
        }
    }













    // Message subcommands extracted to Controller.SubCommand.cs





















}
