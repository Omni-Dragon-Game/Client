using System;
using System.Collections.Generic;
using Mod;
using UnityEngine;

public partial class Panel
{
    // ==================== PlayerChat ====================
    public class PlayerChat
    {
        public string name;

        public int charID;

        public bool isNewMessage;

        public List<InfoItem> chats = new List<InfoItem>();

        public PlayerChat(string name, int charId)
        {
            this.name = name;
            charID = charId;
            isNewMessage = true;
        }
    }


    // ==================== setTabChatManager ====================
    public void setTabChatManager()
    {
        currentListLength = chats.Count;
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
    }


    // ==================== setTabChatPlayer ====================
    public void setTabChatPlayer()
    {
    }


    // ==================== setTypeChatPlayer ====================
    public void setTypeChatPlayer()
    {
    }


    // ==================== addChatMessage ====================
    public void addChatMessage(InfoItem info)
    {
        logChat.insertElementAt(info, 0);
        if (logChat.size() > 20)
        {
            logChat.removeElementAt(logChat.size() - 1);
        }
    }


    // ==================== IsNewMessage ====================
    private bool IsNewMessage(string name)
    {
        return false;
    }


    // ==================== IsHaveNewMessage ====================
    public bool IsHaveNewMessage()
    {
        return false;
    }


    // ==================== ClearNewMessage ====================
    private void ClearNewMessage(string name)
    {
    }


    // ==================== addPlayerMenu ====================
    public void addPlayerMenu(Command pm)
    {
        vPlayerMenu.addElement(pm);
    }


    // ==================== setTabPlayerMenu ====================
    public void setTabPlayerMenu()
    {
        ITEM_HEIGHT = 29;
        currentListLength = vPlayerMenu.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        selected = (GameCanvas.isTouch ? (-1) : 0);
    }


    // ==================== setTypePlayerMenu ====================
    public void setTypePlayerMenu(Char c)
    {
        type = 10;
        setType(0);
        setTabPlayerMenu();
        charMenu = c;
    }


    // ==================== setTypeFriend ====================
    public void setTypeFriend()
    {
        type = 11;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabFriend();
    }


    // ==================== setTypeEnemy ====================
    public void setTypeEnemy()
    {
        type = 16;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabEnemy();
    }


    // ==================== setTabFriend ====================
    public void setTabFriend()
    {
        currentListLength = vFriend.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        if (selected > currentListLength - 1)
        {
            selected = currentListLength - 1;
        }
        cmx = (cmtoX = 0);
    }


    // ==================== setTabEnemy ====================
    public void setTabEnemy()
    {
        currentListLength = vEnemy.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        if (selected > currentListLength - 1)
        {
            selected = currentListLength - 1;
        }
        cmx = (cmtoX = 0);
    }


    // ==================== setTypeMessage ====================
    public void setTypeMessage()
    {
        type = 8;
        setType(0);
        setTabMessage();
        currentTabIndex = 0;
    }


    // ==================== chatTFUpdateKey ====================
    public void chatTFUpdateKey()
    {
        if (chatTField != null && chatTField.isShow)
        {
            if (chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(chatTField.left)) && chatTField.left != null)
            {
                chatTField.left.performAction();
            }
            if (chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(chatTField.right)) && chatTField.right != null)
            {
                chatTField.right.performAction();
            }
            if (chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(chatTField.center)) && chatTField.center != null)
            {
                chatTField.center.performAction();
            }
            if (chatTField.isShow && GameCanvas.keyAsciiPress != 0)
            {
                chatTField.keyPressed(GameCanvas.keyAsciiPress);
                GameCanvas.keyAsciiPress = 0;
            }
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
        }
    }


    // ==================== initLogMessage ====================
    public void initLogMessage()
    {
        currentListLength = logChat.size() + 1;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        cmx = (cmtoX = 0);
    }


    // ==================== setTabMessage ====================
    private void setTabMessage()
    {
        ITEM_HEIGHT = 24;
        initLogMessage();
        selected = (GameCanvas.isTouch ? (-1) : 0);
    }


    // ==================== paintLogChat ====================
    private void paintLogChat(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (logChat.size() == 0)
        {
            mFont.tahoma_7_green2.drawString(g, mResources.no_msg, xScroll + wScroll / 2, yScroll + hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
        }
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = 24;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll + num3;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = wScroll - num3;
            int num7 = ITEM_HEIGHT - 1;
            if (i == 0)
            {
                g.setColor(15196114);
                g.fillRect(num, num5, wScroll, num7);
                g.drawImage((i != selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, xScroll + wScroll - 5, num5 + 2, StaticObj.TOP_RIGHT);
                ((i != selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!isViewChatServer) ? mResources.on : mResources.off, xScroll + wScroll - 22, num5 + 7, 2);
                mFont.tahoma_7_grey.drawString(g, (!isViewChatServer) ? mResources.onPlease : mResources.offPlease, xScroll + 5, num5 + num7 / 2 - 4, mFont.LEFT);
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num4, num5, num6, num7);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num, num2, num3, h);
            InfoItem infoItem = (InfoItem)logChat.elementAt(i - 1);
            if (infoItem.charInfo.headICON != -1)
            {
                SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
            }
            else
            {
                Part part = GameScr.parts[infoItem.charInfo.head];
                SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num + part.pi[Char.CharInfo[0][0][0]].dx, num2 + part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
            }
            g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
            mFont tahoma_7b_dark = mFont.tahoma_7b_dark;
            tahoma_7b_dark = mFont.tahoma_7b_green2;
            tahoma_7b_dark.drawString(g, (infoItem.charInfo.isTichXanh ? "     " : string.Empty) + infoItem.charInfo.cName, num4 + 5, num5, 0);
            //
            if (infoItem.charInfo.isTichXanh)
            {
                ModFunc.PaintTicks(g, num4 + 4, num5 + 1);
            }
            //
            if (!infoItem.isChatServer)
            {
                mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
            }
            else
            {
                mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintEnemy ====================
    private void paintEnemy(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (currentListLength == 0)
        {
            mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, xScroll + wScroll / 2, yScroll + hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
            return;
        }
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = 24;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll + num3;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = wScroll - num3;
            int h2 = ITEM_HEIGHT - 1;
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num4, num5, num6, h2);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num, num2, num3, h);
            InfoItem infoItem = (InfoItem)vEnemy.elementAt(i);
            if (infoItem.charInfo.headICON != -1)
            {
                SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
            }
            else
            {
                Part part = GameScr.parts[infoItem.charInfo.head];
                SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num + part.pi[Char.CharInfo[0][0][0]].dx, num2 + 3 + part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
            }
            g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
            if (infoItem.isOnline)
            {
                mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
                mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
            }
            else
            {
                mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
                mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintFriend ====================
    private void paintFriend(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (currentListLength == 0)
        {
            mFont.tahoma_7_green2.drawString(g, mResources.no_friend, xScroll + wScroll / 2, yScroll + hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
            return;
        }
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = 24;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll + num3;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = wScroll - num3;
            int h2 = ITEM_HEIGHT - 1;
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num4, num5, num6, h2);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num, num2, num3, h);
            InfoItem infoItem = (InfoItem)vFriend.elementAt(i);
            if (infoItem.charInfo.headICON != -1)
            {
                SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
            }
            else
            {
                Part part = GameScr.parts[infoItem.charInfo.head];
                SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num + part.pi[Char.CharInfo[0][0][0]].dx, num2 + 3 + part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
            }
            g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
            if (infoItem.isOnline)
            {
                mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
                mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
            }
            else
            {
                mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
                mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintPlayerMenu ====================
    public void paintPlayerMenu(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < vPlayerMenu.size(); i++)
        {
            int x = xScroll;
            int num = yScroll + i * ITEM_HEIGHT;
            int num2 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num - cmy <= yScroll + hScroll && num - cmy >= yScroll - ITEM_HEIGHT)
            {
                Command command = (Command)vPlayerMenu.elementAt(i);
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(x, num, num2, h);
                if (command.caption2.Equals(string.Empty))
                {
                    mFont.tahoma_7b_dark.drawString(g, command.caption, xScroll + wScroll / 2, num + 6, mFont.CENTER);
                    continue;
                }
                mFont.tahoma_7b_dark.drawString(g, command.caption, xScroll + wScroll / 2, num + 1, mFont.CENTER);
                mFont.tahoma_7b_dark.drawString(g, command.caption2, xScroll + wScroll / 2, num + 11, mFont.CENTER);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintChatManager ====================
    private void paintChatManager(mGraphics g)
    {
    }


    // ==================== paintChatPlayer ====================
    private void paintChatPlayer(mGraphics g)
    {
    }


    // ==================== doFirePlayerMenu ====================
    private void doFirePlayerMenu()
    {
        if (selected != -1)
        {
            isSelectPlayerMenu = true;
            hide();
        }
    }


    // ==================== addFriend ====================
    private void addFriend(InfoItem info)
    {
        string text = "|0|1|" + info.charInfo.cName;
        text += "\n";
        text = ((!info.isOnline) ? (text + "|3|1|" + mResources.is_offline) : (text + "|4|1|" + mResources.is_online));
        text += "\n--";
        string text2 = text;
        text = text2 + "\n|5|" + mResources.power + ": " + info.s;
        cp = new ChatPopup();
        popUpDetailInit(cp, text);
        charInfo = info.charInfo;
        currItem = null;
    }


    // ==================== doFireEnemy ====================
    private void doFireEnemy()
    {
        if (selected >= 0 && vEnemy.size() != 0)
        {
            MyVector myVector = new MyVector();
            currInfoItem = selected;
            myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)vEnemy.elementAt(currInfoItem)));
            myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)vEnemy.elementAt(currInfoItem)));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addFriend((InfoItem)vEnemy.elementAt(selected));
        }
    }


    // ==================== doFireFriend ====================
    private void doFireFriend()
    {
        if (selected >= 0 && vFriend.size() != 0)
        {
            MyVector myVector = new MyVector();
            currInfoItem = selected;
            InfoItem infoItem = (InfoItem)vFriend.elementAt(currInfoItem);
            myVector.addElement(new Command(mResources.CHAT, this, 8001, infoItem));
            myVector.addElement(new Command(mResources.DELETE, this, 8002, infoItem));
            myVector.addElement(new Command(mResources.den, this, 8004, infoItem.charInfo.charID));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addFriend((InfoItem)vFriend.elementAt(selected));
        }
    }


    // ==================== doFireLogMessage ====================
    private void doFireLogMessage()
    {
        if (selected == 0)
        {
            isViewChatServer = !isViewChatServer;
            Rms.saveRMSInt("viewchat", isViewChatServer ? 1 : 0);
            if (GameCanvas.isTouch)
            {
                selected = -1;
            }
        }
        else if (selected >= 0 && logChat.size() != 0)
        {
            MyVector myVector = new MyVector();
            currInfoItem = selected - 1;
            InfoItem infoItem = (InfoItem)logChat.elementAt(currInfoItem);
            myVector.addElement(new Command(mResources.CHAT, this, 8001, infoItem));
            myVector.addElement(new Command(mResources.make_friend, this, 8003, infoItem));
            myVector.addElement(new Command(ModFunc.strTeleportTo, this, 8004, infoItem.charInfo.charID));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addLogMessage((InfoItem)logChat.elementAt(selected - 1));
        }
    }


    // ==================== addLogMessage ====================
    private void addLogMessage(InfoItem info)
    {
        string text = "|0|1|" + info.charInfo.cName;
        text += "\n";
        text += "\n--";
        text = text + "\n|5|" + Res.split(info.s, "|", 0)[2];
        cp = new ChatPopup();
        popUpDetailInit(cp, text);
        charInfo = info.charInfo;
        currItem = null;
    }


    // ==================== onChatFromMe ====================
    public void onChatFromMe(string text, string to)
    {
        if (chatTField.strChat == "Nhập chỉ số mong muốn")
        {
            if (chatTField.tfChat.getText() != string.Empty)
            {
                int param;
                bool success = int.TryParse(text, out param);
                if (success)
                {
                    ModFunc.GI().SetAutoIntrinsic(param);
                }
                else
                {
                    GameScr.info1.addInfo("Chỉ số đã nhập không hợp lệ", 0);
                }
                chatTField.isShow = false;
            }
        }
        else if (chatTField.strChat == "Nhập số sao cần đập")
        {
            if (chatTField.tfChat.getText() != string.Empty)
            {
                int maxPhale;
                bool success = int.TryParse(text, out maxPhale);
                if (success && maxPhale > 0)
                {
                    GameScr.info1.addInfo(string.Concat(new object[]
                    {
                    "Đập ",
                    ModFunc.GI().itemPhale.template.name,
                    " đến ",
                    maxPhale,
                    " sao"
                    }), 0);
                    ModFunc.GI().maxPhale = maxPhale;
                }
                else
                {
                    GameScr.info1.addInfo("Số sao đã nhập không đúng", 0);
                }
                chatTField.isShow = false;
            }
        }
        if (chatTField.tfChat.getText() == null || chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
        {
            chatTField.isShow = false;
            return;
        }
        if (chatTField.strChat.Equals(mResources.input_clan_name))
        {
            InfoDlg.showWait();
            chatTField.isShow = false;
            Service.gI().searchClan(text);
            return;
        }
        if (chatTField.strChat.Equals(mResources.chat_clan))
        {
            InfoDlg.showWait();
            chatTField.isShow = false;
            Service.gI().clanMessage(0, text, -1);
            return;
        }
        if (chatTField.strChat.Equals(mResources.input_clan_name_to_create))
        {
            if (chatTField.tfChat.getText() == string.Empty)
            {
                GameScr.info1.addInfo(mResources.clan_name_blank, 0);
                return;
            }
            if (tabIcon == null)
            {
                tabIcon = new TabClanIcon();
            }
            tabIcon.text = chatTField.tfChat.getText();
            tabIcon.show(isGetName: false);
            chatTField.isShow = false;
            return;
        }
        if (chatTField.strChat.Equals(mResources.input_clan_slogan))
        {
            if (chatTField.tfChat.getText() == string.Empty)
            {
                GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
                return;
            }
            Service.gI().getClan(4, (sbyte)Char.myCharz().clan.imgID, chatTField.tfChat.getText());
            chatTField.isShow = false;
            return;
        }
        if (chatTField.strChat.Equals(mResources.input_Inventory_Pass))
        {
            try
            {
                int lockInventory = int.Parse(chatTField.tfChat.getText());
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                hide();
                if (chatTField.tfChat.getText().Length != 6 || chatTField.tfChat.getText().Equals(string.Empty))
                {
                    GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
                }
                else
                {
                    Service.gI().setLockInventory(lockInventory);
                    chatTField.isShow = false;
                    chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                    hide();
                }
                return;
            }
            catch (Exception)
            {
                GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
                return;
            }
        }
        if (chatTField.strChat.Equals(mResources.world_channel_5_luong))
        {
            if (!chatTField.tfChat.getText().Equals(string.Empty))
            {
                Service.gI().chatGlobal(chatTField.tfChat.getText());
                chatTField.isShow = false;
            }
        }
        else if (chatTField.strChat.Equals(mResources.chat_player))
        {
            chatTField.isShow = false;
            InfoItem infoItem = null;
            if (type == 8)
            {
                infoItem = (InfoItem)logChat.elementAt(currInfoItem);
            }
            else if (type == 11)
            {
                infoItem = (InfoItem)vFriend.elementAt(currInfoItem);
            }
            if (infoItem.charInfo.charID != Char.myCharz().charID)
            {
                Service.gI().chatPlayer(text, infoItem.charInfo.charID);
            }
        }
        else if (chatTField.strChat.Equals(mResources.input_quantity_to_trade))
        {
            int num = 0;
            try
            {
                num = int.Parse(chatTField.tfChat.getText());
            }
            catch (Exception)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                return;
            }
            if (num <= 0 || num > currItem.quantity)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                return;
            }
            currItem.isSelect = true;
            Item item = new()
            {
                template = currItem.template,
                quantity = num,
                indexUI = currItem.indexUI,
                itemOption = currItem.itemOption
            };
            GameCanvas.panel.vMyGD.addElement(item);
            Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
            chatTField.isShow = false;
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        }
        else if (chatTField.strChat == mResources.input_money_to_trade)
        {
            int num2;
            try
            {
                num2 = int.Parse(chatTField.tfChat.getText());
            }
            catch (Exception)
            {
                GameCanvas.startOKDlg(mResources.input_money_wrong);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                return;
            }
            if (num2 > Char.myCharz().xu)
            {
                GameCanvas.startOKDlg(mResources.not_enough_money);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
            }
            else
            {
                moneyGD = num2;
                Service.gI().giaodich(2, -1, -1, num2);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
            }
        }
        else if (chatTField.strChat.Equals(mResources.kiguiXuchat))
        {
            Service.gI().kigui(0, currItem.itemId, 0, int.Parse(chatTField.tfChat.getText()), 1);
            chatTField.isShow = false;
        }
        else if (chatTField.strChat.Equals(mResources.kiguiXuchat + " "))
        {
            Service.gI().kigui(0, currItem.itemId, 0, int.Parse(chatTField.tfChat.getText()), currItem.quantilyToBuy);
            chatTField.isShow = false;
        }
        else if (chatTField.strChat.Equals(mResources.kiguiLuongchat))
        {
            doNotiRuby(0);
            chatTField.isShow = false;
        }
        else if (chatTField.strChat.Equals(mResources.kiguiLuongchat + "  "))
        {
            doNotiRuby(1);
            chatTField.isShow = false;
        }
        else if (chatTField.strChat.Equals(mResources.input_quantity + " "))
        {
            currItem.quantilyToBuy = int.Parse(chatTField.tfChat.getText());
            if (currItem.quantilyToBuy > currItem.quantity)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                return;
            }
            isKiguiXu = true;
            chatTField.isShow = false;
        }
        else if (chatTField.strChat.Equals(mResources.input_quantity + "  "))
        {
            currItem.quantilyToBuy = int.Parse(chatTField.tfChat.getText());
            if (currItem.quantilyToBuy > currItem.quantity)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                return;
            }
            isKiguiLuong = true;
            chatTField.isShow = false;
        }
    }


    // ==================== onCancelChat ====================
    public void onCancelChat()
    {
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
    }


}
