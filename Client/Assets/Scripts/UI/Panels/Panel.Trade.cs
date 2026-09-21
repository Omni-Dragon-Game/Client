using System;

public partial class Panel
{
    // ==================== keyGiaodich ====================
    private void keyGiaodich()
    {
        updateKeyScrollView();
    }


    // ==================== updateKeyGiaoDich ====================
    private void updateKeyGiaoDich()
    {
        if (currentTabIndex == 0)
        {
            if (Equals(GameCanvas.panel))
            {
                updateKeyInventory();
            }
            if (Equals(GameCanvas.panel2))
            {
                keyGiaodich();
            }
        }
        if (currentTabIndex == 1 || currentTabIndex == 2)
        {
            keyGiaodich();
        }
    }


    // ==================== setTabGiaoDich ====================
    public void setTabGiaoDich(bool isMe)
    {
        currentListLength = ((!isMe) ? (vFriendGD.size() + 3) : (vMyGD.size() + 3));
        ITEM_HEIGHT = 29;
        selected = (GameCanvas.isTouch ? (-1) : 0);
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
    }


    // ==================== setTypeGiaoDich ====================
    public void setTypeGiaoDich(Char cGD)
    {
        type = 13;
        tabName[type] = boxGD;
        isAccept = false;
        isLock = false;
        isFriendLock = false;
        vMyGD.removeAllElements();
        vFriendGD.removeAllElements();
        moneyGD = 0;
        friendMoneyGD = 0;
        if (GameCanvas.w > 2 * WIDTH_PANEL)
        {
            GameCanvas.panel2 = new Panel();
            GameCanvas.panel2.type = 13;
            GameCanvas.panel2.tabName[type] = new string[1][] { mResources.item_receive };
            GameCanvas.panel2.setType(1);
            GameCanvas.panel2.setTabGiaoDich(isMe: false);
            GameCanvas.panel.tabName[type] = new string[2][]
            {
                mResources.inventory,
                mResources.item_give
            };
            GameCanvas.panel2.show();
            GameCanvas.panel2.charMenu = cGD;
        }
        if (Equals(GameCanvas.panel))
        {
            setType(0);
        }
        if (currentTabIndex == 0)
        {
            setTabInventory(resetSelect: true);
        }
        if (currentTabIndex == 1)
        {
            setTabGiaoDich(isMe: true);
        }
        if (currentTabIndex == 2)
        {
            setTabGiaoDich(isMe: false);
        }
        charMenu = cGD;
    }


    // ==================== paintGiaoDich ====================
    private void paintGiaoDich(mGraphics g, bool isMe)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        MyVector myVector = ((!isMe) ? vFriendGD : vMyGD);
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll + 29;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 29;
            int num4 = ITEM_HEIGHT - 1;
            int num5 = xScroll;
            int num6 = yScroll + i * ITEM_HEIGHT;
            int num7 = ITEM_HEIGHT - 1;
            int num8 = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            if (i == currentListLength - 1)
            {
                if (!isMe)
                {
                    continue;
                }
                g.setColor(15196114);
                g.fillRect(num5, num2, wScroll, num4);
                if (!isLock)
                {
                    if (!isFriendLock)
                    {
                        mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                    }
                    else
                    {
                        mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                    }
                }
                else if (isFriendLock)
                {
                    g.setColor(15196114);
                    g.fillRect(num5, num2, wScroll, num4);
                    g.drawImage((i != selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, xScroll + wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
                    ((i != selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, xScroll + wScroll - 22, num2 + 7, 2);
                    mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
                }
                else
                {
                    mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                }
                continue;
            }
            if (i == currentListLength - 2)
            {
                if (isMe)
                {
                    g.setColor(15196114);
                    g.fillRect(num5, num2, wScroll, num4);
                    if (!isAccept)
                    {
                        if (!isLock)
                        {
                            g.drawImage((i != selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, xScroll + wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
                            ((i != selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, xScroll + wScroll - 22, num2 + 7, 2);
                            mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
                        }
                        else
                        {
                            g.drawImage((i != selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, xScroll + wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
                            ((i != selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, xScroll + wScroll - 22, num2 + 7, 2);
                            mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
                        }
                    }
                }
                else if (!isFriendLock)
                {
                    mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                }
                else
                {
                    mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                }
                continue;
            }
            if (i == currentListLength - 3)
            {
                if (isLock)
                {
                    g.setColor(13748667);
                }
                else
                {
                    g.setColor((i != selected) ? 15196114 : 16383818);
                }
                g.fillRect(num, num2, num3, num4);
                if (isLock)
                {
                    g.setColor(13748667);
                }
                else
                {
                    g.setColor((i != selected) ? 9993045 : 7300181);
                }
                g.fillRect(num5, num6, num7, num8);
                g.drawImage(imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((!isMe) ? friendMoneyGD : moneyGD) + " " + mResources.XU, num + 5, num2 + 11, 0);
                mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
                continue;
            }
            if (myVector.size() == 0)
            {
                return;
            }
            if (isLock)
            {
                g.setColor(13748667);
            }
            else
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
            }
            g.fillRect(num, num2, num3, num4);
            if (isLock)
            {
                g.setColor(13748667);
            }
            else
            {
                g.setColor((i != selected) ? 9993045 : 9541120);
            }
            Item item = (Item)myVector.elementAt(i);
            if (item != null)
            {
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id != 72 || item.itemOption[j].param <= 0)
                    {
                        continue;
                    }
                    sbyte color_Item_Upgrade = GetColor_Item_Upgrade(item.itemOption[j].param);
                    int color_ItemBg = GetColor_ItemBg(color_Item_Upgrade);
                    if (color_ItemBg != -1)
                    {
                        if (isLock)
                        {
                            g.setColor(13748667);
                        }
                        else
                        {
                            g.setColor((i != selected) ? GetColor_ItemBg(color_Item_Upgrade) : GetColor_ItemBg(color_Item_Upgrade));
                        }
                    }
                }
            }
            g.fillRect(num5, num6, num7, num8);
            if (item == null)
            {
                continue;
            }
            string text = string.Empty;
            mFont mFont2 = mFont.tahoma_7_green2;
            if (item.itemOption != null)
            {
                for (int k = 0; k < item.itemOption.Length; k++)
                {
                    if (item.itemOption[k].optionTemplate.id == 72)
                    {
                        text = " [+" + item.itemOption[k].param + "]";
                    }
                    if (item.itemOption[k].optionTemplate.id == 225)
                    {
                        text = " [+" + item.itemOption[k].param + "]";
                    }
                    if (item.itemOption[k].optionTemplate.id == 225)
                    {
                        if (item.itemOption[k].param >= 1 && item.itemOption[k].param <= 2)
                        {
                            mFont2 = GetFont(0);
                        }
                        else if (item.itemOption[k].param >= 3 && item.itemOption[k].param <= 4)
                        {
                            mFont2 = GetFont(2);
                        }
                        else if (item.itemOption[k].param >= 5 && item.itemOption[k].param <= 6)
                        {
                            mFont2 = GetFont(8);
                        }
                        else if (item.itemOption[k].param >= 7 && item.itemOption[k].param <= 10)
                        {
                            mFont2 = GetFont(7);
                        }
                    }
                    if (item.itemOption[k].optionTemplate.id == 72)
                    {
                        
                         if (item.itemOption[k].param >= 1 && item.itemOption[k].param <= 5)
                        {
                            mFont2 = GetFont(2);
                        }
                        else if (item.itemOption[k].param >= 6 && item.itemOption[k].param <= 7)
                        {
                            mFont2 = GetFont(8);
                        }
                        else if (item.itemOption[k].param >= 8 && item.itemOption[k].param <= 10)
                        {
                            mFont2 = GetFont(7);
                        }
                    }

                }
            }
            mFont2.drawString(g, "[" + item.template.id + "] " + item.template.name + text, num + 5, num2 + 1, 0);
            string text2 = string.Empty;
            if (item.itemOption != null)
            {
                if (item.itemOption.Length > 0 && item.itemOption[0] != null)
                {
                    text2 += item.itemOption[0].getOptionString();
                }
                mFont mFont3 = mFont.tahoma_7_blue;
                if (item.compare < 0 && item.template.type != 5)
                {
                    mFont3 = mFont.tahoma_7_red;
                }
                if (item.itemOption.Length > 1)
                {
                    for (int l = 1; l < Math.min(item.itemOption.Length, 3); l++)
                    {
                        if (item.itemOption[l] != null 
                            && item.itemOption[l].optionTemplate.id != 72
                            && item.itemOption[l].optionTemplate.id != 225
                            && item.itemOption[l].optionTemplate.id != 206
                            && item.itemOption[l].optionTemplate.id != 34
                            && item.itemOption[l].optionTemplate.id != 35
                            && item.itemOption[l].optionTemplate.id != 36
                            && item.itemOption[l].optionTemplate.id != 102
                            && item.itemOption[l].optionTemplate.id != 107)
                        {
                            text2 = text2 + "| " + item.itemOption[l].getOptionString();
                        }
                    }
                }
                mFont3.drawString(g, text2, num + 5, num2 + 10, mFont.LEFT);
            }
            SmallImage.drawSmallImage(g, item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
            if (item.itemOption != null)
            {
                for (int m = 0; m < item.itemOption.Length; m++)
                {
                    paintOptItemInventory(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8, item);
                   
                }
                for (int n = 0; n < item.itemOption.Length; n++)
                {
                    paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
                }
            }
            if (item.quantity > 1)
            {
                mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintGiaoDichInfo ====================
    private void paintGiaoDichInfo(mGraphics g)
    {
        mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== doFireGiaoDich ====================
    private void doFireGiaoDich()
    {
        if (currentTabIndex == 0 && Equals(GameCanvas.panel))
        {
            doFireInventory();
            return;
        }
        if ((currentTabIndex == 0 && Equals(GameCanvas.panel2)) || currentTabIndex == 2)
        {
            if (Equals(GameCanvas.panel2))
            {
                currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(selected);
            }
            else
            {
                currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(selected);
            }
            Res.outz2("toi day select= " + selected);
            MyVector myVector = new MyVector();
            myVector.addElement(new Command(mResources.CLOSE, this, 8000, currItem));
            if (currItem != null)
            {
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addItemDetail(currItem);
            }
            else
            {
                cp = null;
            }
        }
        if (currentTabIndex == 1)
        {
            if (selected == currentListLength - 3)
            {
                if (isLock)
                {
                    return;
                }
                putMoney();
            }
            else if (selected == currentListLength - 2)
            {
                if (!isAccept)
                {
                    isLock = !isLock;
                    if (isLock)
                    {
                        Service.gI().giaodich(5, -1, -1, -1);
                    }
                    else
                    {
                        hide();
                        InfoDlg.showWait();
                        Service.gI().giaodich(3, -1, -1, -1);
                    }
                }
                else
                {
                    isAccept = false;
                }
            }
            else if (selected == currentListLength - 1)
            {
                if (isLock && !isAccept && isFriendLock)
                {
                    GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
                }
            }
            else
            {
                if (isLock)
                {
                    return;
                }
                currItem = (Item)GameCanvas.panel.vMyGD.elementAt(selected);
                MyVector myVector2 = new MyVector();
                myVector2.addElement(new Command(mResources.CLOSE, this, 8000, currItem));
                if (currItem != null)
                {
                    GameCanvas.menu.startAt(myVector2, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                    addItemDetail(currItem);
                }
                else
                {
                    cp = null;
                }
            }
        }
        if (GameCanvas.isTouch)
        {
            selected = -1;
        }
    }


}
