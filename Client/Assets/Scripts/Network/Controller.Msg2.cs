using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart2(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case -80:
                    {
                        sbyte b48 = msg.reader().readByte();
                        InfoDlg.hide();
                        if (b48 == 0)
                        {
                            GameCanvas.panel.vFriend.removeAllElements();
                            int num108 = msg.reader().readUnsignedByte();
                            for (int num109 = 0; num109 < num108; num109++)
                            {
                                Char char9 = new Char();
                                char9.charID = msg.reader().readInt();
                                char9.head = msg.reader().readShort();
                                char9.headICON = msg.reader().readShort();
                                char9.body = msg.reader().readShort();
                                char9.leg = msg.reader().readShort();
                                char9.bag = msg.reader().readUnsignedByte();
                                char9.cName = msg.reader().readUTF();
                                bool isOnline = msg.reader().readBoolean();
                                InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
                                infoItem2.charInfo = char9;
                                infoItem2.isOnline = isOnline;
                                GameCanvas.panel.vFriend.addElement(infoItem2);
                            }
                            GameCanvas.panel.setTypeFriend();
                            GameCanvas.panel.show();
                        }
                        if (b48 == 3)
                        {
                            MyVector vFriend = GameCanvas.panel.vFriend;
                            int num110 = msg.reader().readInt();
                            for (int num111 = 0; num111 < vFriend.size(); num111++)
                            {
                                InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num111);
                                if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num110)
                                {
                                    infoItem3.isOnline = msg.reader().readBoolean();
                                    break;
                                }
                            }
                        }
                        if (b48 != 2)
                        {
                            break;
                        }
                        MyVector vFriend2 = GameCanvas.panel.vFriend;
                        int num112 = msg.reader().readInt();
                        for (int num113 = 0; num113 < vFriend2.size(); num113++)
                        {
                            InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num113);
                            if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num112)
                            {
                                vFriend2.removeElement(infoItem4);
                                break;
                            }
                        }
                        if (GameCanvas.panel.isShow)
                        {
                            GameCanvas.panel.setTabFriend();
                        }
                        break;
                    }
                case -99:
                    {
                        InfoDlg.hide();
                        sbyte b41 = msg.reader().readByte();
                        if (b41 == 0)
                        {
                            GameCanvas.panel.vEnemy.removeAllElements();
                            int num95 = msg.reader().readUnsignedByte();
                            for (int num96 = 0; num96 < num95; num96++)
                            {
                                Char char6 = new Char();
                                char6.charID = msg.reader().readInt();
                                char6.head = msg.reader().readShort();
                                char6.headICON = msg.reader().readShort();
                                char6.body = msg.reader().readShort();
                                char6.leg = msg.reader().readShort();
                                char6.bag = msg.reader().readShort();
                                char6.cName = msg.reader().readUTF();
                                InfoItem infoItem = new InfoItem(msg.reader().readUTF());
                                bool flag8 = msg.reader().readBoolean();
                                infoItem.charInfo = char6;
                                infoItem.isOnline = flag8;
                                GameCanvas.panel.vEnemy.addElement(infoItem);
                            }
                            GameCanvas.panel.setTypeEnemy();
                            GameCanvas.panel.show();
                        }
                        break;
                    }
                case -79:
                    {
                        InfoDlg.hide();
                        int num104 = msg.reader().readInt();
                        Char charMenu = GameCanvas.panel.charMenu;
                        if (charMenu == null)
                        {
                            return;
                        }
                        charMenu.cPower = msg.reader().readLong();
                        charMenu.currStrLevel = msg.reader().readUTF();
                        break;
                    }
                case -93:
                    {
                        short num15 = msg.reader().readShort();
                        BgItem.newSmallVersion = new sbyte[num15];
                        for (int l = 0; l < num15; l++)
                        {
                            BgItem.newSmallVersion[l] = msg.reader().readByte();
                        }
                        break;
                    }
                case -77:
                    {
                        short num93 = msg.reader().readShort();
                        SmallImage.newSmallVersion = new sbyte[num93];
                        SmallImage.maxSmall = num93;
                        SmallImage.imgNew = new Small[num93];
                        for (int num94 = 0; num94 < num93; num94++)
                        {
                            SmallImage.newSmallVersion[num94] = msg.reader().readByte();
                        }
                        break;
                    }
                case -76:
                    {
                        sbyte type = msg.reader().readByte();
                        if (type == 0)
                        {
                            sbyte sz = msg.reader().readByte();
                            if (sz <= 0)
                            {
                                return;
                            }
                            Char.myCharz().arrArchive = new Archivement[sz];
                            for (int m = 0; m < sz; m++)
                            {
                                Char.myCharz().arrArchive[m] = new Archivement
                                {
                                    info1 = m + 1 + ". " + msg.reader().readUTF(),
                                    info2 = msg.reader().readUTF(),
                                    money = msg.reader().readShort(),
                                    isFinish = msg.reader().readBoolean(),
                                    isRecieve = msg.reader().readBoolean()
                                };
                            }
                            GameCanvas.panel.setTypeArchivement();
                            GameCanvas.panel.show();
                        }
                        else if (type == 1)
                        {
                            int idArchive = msg.reader().readUnsignedByte();
                            if (Char.myCharz().arrArchive[idArchive] != null)
                            {
                                Char.myCharz().arrArchive[idArchive].isRecieve = true;
                            }
                        }
                        break;
                    }
                case -74:
                    {
                        if (ServerListScreen.stopDownload)
                        {
                            return;
                        }
                        if (!GameCanvas.isGetResourceFromServer())
                        {
                            Service.gI().getResource(3, null);
                            SmallImage.loadBigRMS();
                            if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null)
                            {
                                LoginScr.isContinueToLogin = true;
                            }
                            GameCanvas.loginScr = new LoginScr();
                            GameCanvas.loginScr.switchToMe();
                            return;
                        }
                        sbyte b38 = msg.reader().readByte();
                        Debug.Log("<<<cmd -74 b38=" + b38);
                        if (b38 == 0)
                        {
                            int num89 = msg.reader().readInt();
                            string text3 = Rms.loadRMSString("ResVersion");
                            int num90 = ((text3 == null || !(text3 != string.Empty)) ? (-1) : int.Parse(text3));
                            Debug.Log("ResVersion: cached=" + num90 + " server=" + num89);
                            if (Session_ME.gI().isCompareIPConnect())
                            {
                                if (num90 == -1 || num90 != num89)
                                {
                                    Debug.Log("Version mismatch -> show2()");
                                    GameCanvas.serverScreen.show2();
                                }
                                else
                                {
                                    SmallImage.loadBigRMS();
                                    ServerListScreen.loadScreen = true;
                                    GameCanvas.serverScreen.switchToMe();
                                }
                            }
                            else
                            {
                                Session_ME.gI().close();
                                ServerListScreen.loadScreen = true;
                                ServerListScreen.isAutoConect = false;
                                ServerListScreen.countDieConnect = 1000;
                                GameCanvas.serverScreen.switchToMe();
                            }
                 //     ServerListScreen.keyDecryptString = msg.reader().readUTF();
                        }
                        if (b38 == 1)
                        {
                            ServerListScreen.strWait = mResources.downloading_data;
                            short nBig = msg.reader().readShort();
                            ServerListScreen.nBig = nBig;
                            Service.gI().getResource(2, null);
                        }
                        if (b38 == 2)
                        {
                            try
                            {
                                isLoadingData = true;
                                GameCanvas.endDlg();
                                ServerListScreen.demPercent++;
                                ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
                                string original = msg.reader().readUTF();
                                string[] array8 = Res.split(original, "/", 0);
                                string filename = "x" + mGraphics.zoomLevel + array8[array8.Length - 1];
                                int num91 = msg.reader().readInt();
                                sbyte[] data = new sbyte[num91];
                                msg.reader().read(ref data, 0, num91);
                                Rms.saveRMS(filename, data);
                            }
                            catch (Exception)
                            {
                                GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
                            }
                        }
                        if (b38 == 3)
                        {
                            Rms.saveRMSInt("musicSize", ModFunc.musicCount);
                            ModFunc.InitMusic();
                            isLoadingData = false;
                            int num92 = msg.reader().readInt();
                            Rms.saveRMSString("ResVersion", num92 + string.Empty);
                            Service.gI().getResource(3, null);
                            GameCanvas.endDlg();
                            SmallImage.loadBigRMS();
                            mSystem.gcc();
                            ServerListScreen.bigOk = true;
                            ServerListScreen.loadScreen = true;
                            GameCanvas.serverScreen.switchToMe();
                        }
                        if (b38 == 4)
                        {
                            string name = msg.reader().readUTF();
                            sbyte[] data = null;
                            try
                            {
                                data = NinjaUtil.readByteArray(msg);
                            }
                            catch (Exception)
                            {
                                data = null;
                            }
                            if (data != null)
                            {
                                ModFunc.musicCount += 1;
                                Rms.saveRMS("music_" + name, data);
                            }
                        }
                        break;
                    }
                case -43:
                    {
                        sbyte itemAction = msg.reader().readByte();
                        sbyte where = msg.reader().readByte();
                        sbyte index = msg.reader().readByte();
                        string info = msg.reader().readUTF();
                        GameCanvas.panel.itemRequest(itemAction, info, where, index);
                        break;
                    }
                case -59:
                    {
                        sbyte typePK = msg.reader().readByte();
                        GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
                        break;
                    }
                case -62:
                    {
                        byte id = msg.reader().readUnsignedByte();
                        sbyte size = msg.reader().readByte();
                        int[] idImage = new int[size];

                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                idImage[i] = msg.reader().readShort();
                                if (idImage[i] > 0)
                                {
                                    SmallImage.vKeys.addElement(idImage[i] + string.Empty);
                                }
                            }
                        }

                        short idNew;
                        try
                        {
                            idNew = msg.reader().readShort();
                        }
                        catch
                        {
                            idNew = id;
                        }

                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                try
                                {
                                    idImage[i] = msg.reader().readInt();
                                }
                                catch
                                {
                                }
                                if (idImage[i] > 0)
                                {
                                    SmallImage.vKeys.addElement(idImage[i] + string.Empty);
                                }
                            }
                        }

                        ClanImage clanImage = ClanImage.getClanImage(idNew);
                        if (clanImage == null)
                        {
                            break;
                        }
                        clanImage.idImage = idImage;
                        break;
                    }
                case -65:
                    {
                        InfoDlg.hide();
                        int num83 = msg.reader().readInt();
                        sbyte b36 = msg.reader().readByte();
                        if (b36 == 0)
                        {
                            break;
                        }
                        if (Char.myCharz().charID == num83)
                        {
                            isStopReadMessage = true;
                            GameScr.lockTick = 500;
                            GameScr.gI().center = null;
                            if (b36 == 0 || b36 == 1 || b36 == 3)
                            {
                                Teleport p = new Teleport(Char.myCharz().cx, Char.myCharz().cy, Char.myCharz().head, Char.myCharz().cdir, 0, isMe: true, (b36 != 1) ? b36 : Char.myCharz().cgender);
                                Teleport.addTeleport(p);
                            }
                            if (b36 == 2)
                            {
                                GameScr.lockTick = 50;
                                Char.myCharz().hide();
                            }
                        }
                        else
                        {
                            Char char5 = GameScr.findCharInMap(num83);
                            if ((b36 == 0 || b36 == 1 || b36 == 3) && char5 != null)
                            {
                                char5.isUsePlane = true;
                                Teleport teleport = new Teleport(char5.cx, char5.cy, char5.head, char5.cdir, 0, isMe: false, (b36 != 1) ? b36 : char5.cgender);
                                teleport.id = num83;
                                Teleport.addTeleport(teleport);
                            }
                            if (b36 == 2)
                            {
                                char5.hide();
                            }
                        }
                        break;
                    }
                case -64:
                    {
                        int num19 = msg.reader().readInt();
                        int num20 = msg.reader().readUnsignedByte();
                        @char = null;
                        @char = ((num19 != Char.myCharz().charID) ? GameScr.findCharInMap(num19) : Char.myCharz());
                        if (@char == null)
                        {
                            return;
                        }
                        @char.bag = num20;
                        for (int num21 = 0; num21 < 54; num21++)
                        {
                            @char.removeEffChar(0, 201 + num21);
                        }
                        if (@char.bag >= 201 && @char.bag < 255)
                        {
                            Effect effect = new Effect(@char.bag, @char, 2, -1, 10, 1);
                            effect.typeEff = 5;
                            @char.addEffChar(effect);
                        }
                        break;
                    }
                case -63:
                    {
                        byte id = msg.reader().readUnsignedByte();
                        sbyte size = msg.reader().readByte();
                        int[] idImages = new int[size];

                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                idImages[i] = msg.reader().readShort();
                                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
                            }
                            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");

                        }

                        short idNew;
                        try
                        {
                            idNew = msg.reader().readShort();
                        }
                        catch
                        {
                            idNew = id;
                        }
                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                try
                                {
                                    idImages[i] = msg.reader().readInt();
                                }
                                catch
                                {
                                }
                            }
                        }

                        ClanImage clanImage3 = new()
                        {
                            ID = idNew,
                            idImage = idImages
                        };
                        if (size > 0)
                        {
                            ClanImage.idImages.put(id + string.Empty, clanImage3);
                        }
                        break;
                    }
                case -57:
                    {
                        string strInvite = msg.reader().readUTF();
                        int clanID = msg.reader().readInt();
                        int code = msg.reader().readInt();
                        GameScr.gI().clanInvite(strInvite, clanID, code);
                        break;
                    }
                case -51:
                    InfoDlg.hide();
                    readClanMsg(msg, 0);
                    if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
                    {
                        GameCanvas.panel.initTabClans();
                    }
                    break;
                case -53:
                    {
                        InfoDlg.hide();
                        bool flag6 = false;
                        int num74 = msg.reader().readInt();
                        if (num74 == -1)
                        {
                            flag6 = true;
                            Char.myCharz().clan = null;
                            ClanMessage.vMessage.removeAllElements();
                            if (GameCanvas.panel.member != null)
                            {
                                GameCanvas.panel.member.removeAllElements();
                            }
                            if (GameCanvas.panel.myMember != null)
                            {
                                GameCanvas.panel.myMember.removeAllElements();
                            }
                            if (GameCanvas.currentScreen == GameScr.gI())
                            {
                                GameCanvas.panel.setTabClans();
                            }
                            return;
                        }
                        GameCanvas.panel.tabIcon = null;
                        if (Char.myCharz().clan == null)
                        {
                            Char.myCharz().clan = new Clan();
                        }
                        Char.myCharz().clan.ID = num74;
                        Char.myCharz().clan.name = msg.reader().readUTF();
                        Char.myCharz().clan.slogan = msg.reader().readUTF();
                        Char.myCharz().clan.imgID = msg.reader().readUnsignedByte();
                        Char.myCharz().clan.powerPoint = msg.reader().readUTF();
                        Char.myCharz().clan.leaderName = msg.reader().readUTF();
                        Char.myCharz().clan.currMember = msg.reader().readUnsignedByte();
                        Char.myCharz().clan.maxMember = msg.reader().readUnsignedByte();
                        Char.myCharz().role = msg.reader().readByte();
                        Char.myCharz().clan.clanPoint = msg.reader().readInt();
                        Char.myCharz().clan.level = msg.reader().readByte();
                        GameCanvas.panel.myMember = new MyVector();
                        for (int num75 = 0; num75 < Char.myCharz().clan.currMember; num75++)
                        {
                            Member member2 = new Member();
                            member2.ID = msg.reader().readInt();
                            member2.head = msg.reader().readShort();
                            member2.headICON = msg.reader().readShort();
                            member2.leg = msg.reader().readShort();
                            member2.body = msg.reader().readShort();
                            member2.name = msg.reader().readUTF();
                            member2.role = msg.reader().readByte();
                            member2.powerPoint = msg.reader().readUTF();
                            member2.donate = msg.reader().readInt();
                            member2.receive_donate = msg.reader().readInt();
                            member2.clanPoint = msg.reader().readInt();
                            member2.curClanPoint = msg.reader().readInt();
                            member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
                            GameCanvas.panel.myMember.addElement(member2);
                        }
                        int num76 = msg.reader().readUnsignedByte();
                        for (int num77 = 0; num77 < num76; num77++)
                        {
                            readClanMsg(msg, -1);
                        }
                        if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
                        {
                            GameCanvas.panel.setTabClans();
                        }
                        if (flag6)
                        {
                            GameCanvas.panel.setTabClans();
                        }
                        break;
                    }
                case -52:
                    {
                        sbyte b34 = msg.reader().readByte();
                        if (b34 == 0)
                        {
                            Member member3 = new Member();
                            member3.ID = msg.reader().readInt();
                            member3.head = msg.reader().readShort();
                            member3.headICON = msg.reader().readShort();
                            member3.leg = msg.reader().readShort();
                            member3.body = msg.reader().readShort();
                            member3.name = msg.reader().readUTF();
                            member3.role = msg.reader().readByte();
                            member3.powerPoint = msg.reader().readUTF();
                            member3.donate = msg.reader().readInt();
                            member3.receive_donate = msg.reader().readInt();
                            member3.clanPoint = msg.reader().readInt();
                            member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
                            if (GameCanvas.panel.myMember == null)
                            {
                                GameCanvas.panel.myMember = new MyVector();
                            }
                            GameCanvas.panel.myMember.addElement(member3);
                            GameCanvas.panel.initTabClans();
                        }
                        if (b34 == 1)
                        {
                            GameCanvas.panel.myMember.removeElementAt(msg.reader().readByte());
                            GameCanvas.panel.currentListLength--;
                            GameCanvas.panel.initTabClans();
                        }
                        if (b34 == 2)
                        {
                            Member member4 = new Member();
                            member4.ID = msg.reader().readInt();
                            member4.head = msg.reader().readShort();
                            member4.headICON = msg.reader().readShort();
                            member4.leg = msg.reader().readShort();
                            member4.body = msg.reader().readShort();
                            member4.name = msg.reader().readUTF();
                            member4.role = msg.reader().readByte();
                            member4.powerPoint = msg.reader().readUTF();
                            member4.donate = msg.reader().readInt();
                            member4.receive_donate = msg.reader().readInt();
                            member4.clanPoint = msg.reader().readInt();
                            member4.joinTime = NinjaUtil.getDate(msg.reader().readInt());
                            for (int num78 = 0; num78 < GameCanvas.panel.myMember.size(); num78++)
                            {
                                Member member5 = (Member)GameCanvas.panel.myMember.elementAt(num78);
                                if (member5.ID == member4.ID)
                                {
                                    if (Char.myCharz().charID == member4.ID)
                                    {
                                        Char.myCharz().role = member4.role;
                                    }
                                    Member o = member4;
                                    GameCanvas.panel.myMember.removeElement(member5);
                                    GameCanvas.panel.myMember.insertElementAt(o, num78);
                                    return;
                                }
                            }
                        }
                        break;
                    }
                case -50:
                    {
                        InfoDlg.hide();
                        GameCanvas.panel.member = new MyVector();
                        sbyte b22 = msg.reader().readByte();
                        for (int num49 = 0; num49 < b22; num49++)
                        {
                            Member member = new Member();
                            member.ID = msg.reader().readInt();
                            member.head = msg.reader().readShort();
                            member.headICON = msg.reader().readShort();
                            member.leg = msg.reader().readShort();
                            member.body = msg.reader().readShort();
                            member.name = msg.reader().readUTF();
                            member.role = msg.reader().readByte();
                            member.powerPoint = msg.reader().readUTF();
                            member.donate = msg.reader().readInt();
                            member.receive_donate = msg.reader().readInt();
                            member.clanPoint = msg.reader().readInt();
                            member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
                            GameCanvas.panel.member.addElement(member);
                        }
                        GameCanvas.panel.isViewMember = true;
                        GameCanvas.panel.isSearchClan = false;
                        GameCanvas.panel.isMessage = false;
                        GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
                        GameCanvas.panel.initTabClans();
                        break;
                    }
                case -47:
                    {
                        InfoDlg.hide();
                        sbyte b69 = msg.reader().readByte();
                        if (b69 == 0)
                        {
                            GameCanvas.panel.clanReport = mResources.cannot_find_clan;
                            GameCanvas.panel.clans = null;
                        }
                        else
                        {
                            GameCanvas.panel.clans = new Clan[b69];
                            for (int num169 = 0; num169 < GameCanvas.panel.clans.Length; num169++)
                            {
                                GameCanvas.panel.clans[num169] = new Clan();
                                GameCanvas.panel.clans[num169].ID = msg.reader().readInt();
                                GameCanvas.panel.clans[num169].name = msg.reader().readUTF();
                                GameCanvas.panel.clans[num169].slogan = msg.reader().readUTF();
                                GameCanvas.panel.clans[num169].imgID = msg.reader().readUnsignedByte();
                                GameCanvas.panel.clans[num169].powerPoint = msg.reader().readUTF();
                                GameCanvas.panel.clans[num169].leaderName = msg.reader().readUTF();
                                GameCanvas.panel.clans[num169].currMember = msg.reader().readUnsignedByte();
                                GameCanvas.panel.clans[num169].maxMember = msg.reader().readUnsignedByte();
                                GameCanvas.panel.clans[num169].date = msg.reader().readInt();
                            }
                        }
                        GameCanvas.panel.isSearchClan = true;
                        GameCanvas.panel.isViewMember = false;
                        GameCanvas.panel.isMessage = false;
                        if (GameCanvas.panel.isSearchClan)
                        {
                            GameCanvas.panel.initTabClans();
                        }
                        break;
                    }
                case -46:
                    {
                        InfoDlg.hide();
                        sbyte type = msg.reader().readByte();
                        if (type == 1 || type == 3)
                        {
                            GameCanvas.endDlg();
                            ClanImage.vClanImage.removeAllElements();
                            int size = msg.reader().readUnsignedByte();
                            for (int i = 0; i < size; i++)
                            {
                                byte id = msg.reader().readUnsignedByte();
                                string name = msg.reader().readUTF();
                                int xu = msg.reader().readInt();
                                int luong = msg.reader().readInt();

                                ClanImage clanImage2 = new()
                                {
                                    ID = id,
                                    name = name,
                                    xu = xu,
                                    luong = luong
                                };

                                if (!ClanImage.isExistClanImage(clanImage2.ID))
                                {
                                    ClanImage.addClanImage(clanImage2);
                                    continue;
                                }
                                ClanImage.getClanImage((short)clanImage2.ID).name = clanImage2.name;
                                ClanImage.getClanImage((short)clanImage2.ID).xu = clanImage2.xu;
                                ClanImage.getClanImage((short)clanImage2.ID).luong = clanImage2.luong;
                            }
                            if (Char.myCharz().clan != null)
                            {
                                GameCanvas.panel.changeIcon();
                            }
                        }
                        if (type == 4)
                        {
                            Char.myCharz().clan.imgID = msg.reader().readUnsignedByte();
                            Char.myCharz().clan.slogan = msg.reader().readUTF();
                        }
                        break;
                    }
                case -61:
                    {
                        int num105 = msg.reader().readInt();
                        if (num105 != Char.myCharz().charID)
                        {
                            if (GameScr.findCharInMap(num105) != null)
                            {
                                GameScr.findCharInMap(num105).clanID = msg.reader().readInt();
                                if (GameScr.findCharInMap(num105).clanID == -2)
                                {
                                    GameScr.findCharInMap(num105).isCopy = true;
                                }
                            }
                        }
                        else if (Char.myCharz().clan != null)
                        {
                            Char.myCharz().clan.ID = msg.reader().readInt();
                        }
                        break;
                    }
                case -42:
                    Char.myCharz().cHPGoc = msg.readLong();
                    Char.myCharz().cMPGoc = msg.readLong();
                    Char.myCharz().cDamGoc = msg.readLong();
                    Char.myCharz().cHPFull = msg.readLong();
                    Char.myCharz().cMPFull = msg.readLong();
                    Char.myCharz().cHP = msg.readLong();
                    Char.myCharz().cMP = msg.readLong();
                    Char.myCharz().cspeed = msg.reader().readByte();
                    if (Char.myCharz().cspeed <= 0)
                    {
                        Char.myCharz().cspeed = 4;
                    }
                    Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
                    Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
                    Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
                    Char.myCharz().cDamFull = msg.readLong();
                    Char.myCharz().cDefull = msg.reader().readInt();
                    Char.myCharz().cCriticalFull = msg.reader().readByte();
                    Char.myCharz().cTiemNang = msg.reader().readLong();
                    Char.myCharz().expForOneAdd = msg.reader().readShort();
                    Char.myCharz().cDefGoc = msg.reader().readInt();
                    Char.myCharz().cCriticalGoc = msg.reader().readByte();

                    try
                    {
                        Char.myCharz().tlDef = msg.readInt3Byte();
                        Char.myCharz().tlPst = msg.readInt3Byte();
                        Char.myCharz().tlNeDon = msg.readInt3Byte();
                        Char.myCharz().tlHutHp = msg.readInt3Byte();
                        Char.myCharz().tlHutMp = msg.readInt3Byte();

                        Char.myCharz().tileGiamTDHS = msg.readInt3Byte();
                        Char.myCharz().timeGiamTDHS = msg.readInt3Byte();
                        Char.myCharz().khangTDHS = msg.reader().readBool();
                        Char.myCharz().isKhongLanh = msg.reader().readBool();
                        Char.myCharz().wearingVoHinh = msg.reader().readBool();
                        Char.myCharz().teleport = msg.reader().readBool();
                    }
                    catch
                    {
                        Char.myCharz().tlDef = 0;
                        Char.myCharz().tlPst = 0;
                        Char.myCharz().tlNeDon = 0;
                        Char.myCharz().tlHutHp = 0;
                        Char.myCharz().tlHutMp = 0;

                        Char.myCharz().tileGiamTDHS = 0;
                        Char.myCharz().timeGiamTDHS = 0;
                        Char.myCharz().khangTDHS = false;
                        Char.myCharz().isKhongLanh = false;
                        Char.myCharz().wearingVoHinh = false;
                        Char.myCharz().teleport = false;
                    }

                    InfoDlg.hide();
                    break;
        }
    }
}
