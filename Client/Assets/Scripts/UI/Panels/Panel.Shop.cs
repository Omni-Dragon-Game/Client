using System;
using Assets.src.g;
using Mod;

public partial class Panel
{
    // ==================== setTypeKiGuiOnly ====================
    public void setTypeKiGuiOnly()
    {
        type = 17;
        setType(1);
        setTabKiGui();
        typeShop = 2;
        currentTabIndex = 0;
    }


    // ==================== setTabKiGui ====================
    public void setTabKiGui()
    {
        ITEM_HEIGHT = 29;
        currentListLength = Char.myCharz().arrItemShop[4].Length;
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


    // ==================== setTypeShop ====================
    public void setTypeShop(int typeShop)
    {
        type = 1;
        setType(0);
        setTabShop();
        currentTabIndex = 0;
        this.typeShop = typeShop;
    }


    // ==================== setTabShop ====================
    public void setTabShop()
    {
        ITEM_HEIGHT = 29;
        if (currentTabIndex == currentTabName.Length - 1 && GameCanvas.panel2 == null && typeShop != 2)
        {
            currentListLength = checkCurrentListLength(Char.myCharz().arrItemBag.Length);
        }
        else
        {
            currentListLength = Char.myCharz().arrItemShop[currentTabIndex].Length;
        }
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


    // ==================== paintShop ====================
    private void paintShop(mGraphics g)
    {
        try
        {
            if (type == 1 && currentTabIndex == currentTabName.Length - 1 && GameCanvas.panel2 == null && typeShop != 2)
            {
                paintInventory(g);
                return;
            }
            g.setColor(16711680);
            g.setClip(xScroll, yScroll, wScroll, hScroll);
            if (typeShop == 2 && Equals(GameCanvas.panel))
            {
                if (currentTabIndex <= 3 && GameCanvas.isTouch)
                {
                    if (cmy < -50)
                    {
                        GameCanvas.paintShukiren(xScroll + wScroll / 2, yScroll + 30, g);
                    }
                    else if (cmy < 0)
                    {
                        mFont.tahoma_7_grey.drawString(g, mResources.getDown, xScroll + wScroll / 2, yScroll + 15, 2);
                    }
                    else if (cmyLim >= 0)
                    {
                        if (cmy > cmyLim + 50)
                        {
                            GameCanvas.paintShukiren(xScroll + wScroll / 2, yScroll + hScroll - 30, g);
                        }
                        else if (cmy > cmyLim)
                        {
                            mFont.tahoma_7_grey.drawString(g, mResources.getUp, xScroll + wScroll / 2, yScroll + hScroll - 25, 2);
                        }
                    }
                }
                if (Char.myCharz().arrItemShop[currentTabIndex].Length == 0 && type != 17)
                {
                    mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, xScroll + wScroll / 2, yScroll + hScroll / 2 - 10, 2);
                    return;
                }
            }
            g.translate(0, -cmy);
            Item[] array = Char.myCharz().arrItemShop[currentTabIndex];
            if (typeShop == 2 && (currentTabIndex == 4 || type == 17))
            {
                array = Char.myCharz().arrItemShop[4];
                if (array.Length == 0)
                {
                    mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, xScroll + wScroll / 2, yScroll + hScroll / 2 - 10, 2);
                    return;
                }
            }
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                int num2 = xScroll + 29;
                int num3 = yScroll + i * ITEM_HEIGHT;
                int num4 = wScroll - 29;
                int h = ITEM_HEIGHT - 1;
                int num5 = xScroll;
                int num6 = yScroll + i * ITEM_HEIGHT;
                int num7 = ITEM_HEIGHT - 1;
                int num8 = ITEM_HEIGHT - 1;
                if (num3 - cmy > yScroll + hScroll || num3 - cmy < yScroll - ITEM_HEIGHT)
                {
                    continue;
                }
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(num2, num3, num4, h);
                g.setColor((i != selected) ? 9993045 : 9541120);
                g.fillRect(num5, num6, num7, num8);
                Item item = array[i];
                if (item != null)
                {
                    string text = string.Empty;
                    mFont mFont2 = mFont.tahoma_7_green2;
                    if (item.isMe != 0 && typeShop == 2 && currentTabIndex <= 3 && !Equals(GameCanvas.panel2))
                    {
                        mFont2 = mFont.tahoma_7b_green;
                    }
                    if (item.itemOption != null)
                    {
                        for (int j = 0; j < item.itemOption.Length; j++)
                        {
                            if (item.itemOption[j].optionTemplate.id == 72)
                            {
                                text = " [+" + item.itemOption[j].param + "]";
                            }
                            if (item.itemOption[j].optionTemplate.id == 225)
                            {
                                text = " [+" + item.itemOption[j].param + "]";
                            }
                            if (item.itemOption[j].optionTemplate.id == 225)
                            {
                                if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 2)
                                {
                                    mFont2 = GetFont(0);
                                }
                                else if (item.itemOption[j].param >= 3 && item.itemOption[j].param <= 4)
                                {
                                    mFont2 = GetFont(2);
                                }
                                else if (item.itemOption[j].param >= 5 && item.itemOption[j].param <= 6)
                                {
                                    mFont2 = GetFont(8);
                                }
                                else if (item.itemOption[j].param >= 7 && item.itemOption[j].param <= 10)
                                {
                                    mFont2 = GetFont(7);
                                }
                            }
                            if (item.itemOption[j].optionTemplate.id == 72)
                            {
                                if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 5)
                                {
                                    mFont2 = GetFont(2);
                                }
                                
                                else if (item.itemOption[j].param >= 6 && item.itemOption[j].param <= 7)
                                {
                                    mFont2 = GetFont(8);
                                }
                                else if (item.itemOption[j].param >= 8 && item.itemOption[j].param <= 10)
                                {
                                    mFont2 = GetFont(7);
                                }
                            }
                        }
                    }
                    mFont2.drawString(g, "[" + item.template.id + "] " + item.template.name + text, num2 + 5, num3 + 1, 0);
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
                        if (typeShop == 2 && item.itemOption.Length > 1 && item.buyType != -1)
                        {
                            text2 += string.Empty;
                        }
                        if (typeShop != 2 || (typeShop == 2 && item.buyType <= 1))
                        {
                            mFont3.drawString(g, text2, num2 + 5, num3 + 10, 0);
                        }
                    }
                   
                    if (item.buySpec > 0)
                    {    
                        SmallImage.drawSmallImage(g, item.iconSpec, num2 + num4 - 7, num3 + h - 7, 0, 3);
                        mFont.tahoma_7b_dark.drawString(g, Res.formatNumber(item.buySpec), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                    }
                    if (item.buyCoin != 0 || item.buyGold != 0)
                    {
                        if (typeShop != 2 && item.powerRequire == 0)
                        {
                            if (item.buyCoin > 0 && item.buyGold > 0)
                            {
                                if (item.buyCoin > 0)
                                {
                                    g.drawImage(imgXu, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                                if (item.buyGold > 0)
                                {
                                    g.drawImage(imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_blue.drawString(g, Res.formatNumber(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                            }
                            else
                            {
                                if (item.buyCoin > 0)
                                {
                                    g.drawImage(imgXu, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                                if (item.buyGold > 0)
                                {
                                    g.drawImage(imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_blue.drawString(g, Res.formatNumber(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                            }
                        }
                        if (typeShop == 2 && currentTabIndex <= 3 && !Equals(GameCanvas.panel2))
                        {
                            if (item.buyCoin > 0 && item.buyGold > 0)
                            {
                                if (item.buyCoin > 0)
                                {
                                    g.drawImage(imgThoivang, num2 + num4 - 7, num3 + h - 5, 3); // HERE
                                    mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                                if (item.buyGold > 0)
                                {
                                    g.drawImage(imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_green.drawString(g, Res.formatNumber2(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                            }
                            else
                            {
                                if (item.buyCoin > 0)
                                {
                                    g.drawImage(imgThoivang, num2 + num4 - 7, num3 + h - 5, 3); // HERE
                                    mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                                if (item.buyGold > 0)
                                {
                                    g.drawImage(imgLuong, num2 + num4 - 7, num3 + h - 5, 3);
                                    mFont.tahoma_7b_green.drawString(g, Res.formatNumber2(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                                }
                            }
                        }
                    }
                    SmallImage.drawSmallImage(g, item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
                    if (item.quantity > 1)
                    {
                        mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
                    }
                    if (item.newItem && GameCanvas.gameTick % 10 > 5)
                    {
                        g.drawImage(imgNew, num5 + num7 / 2, num3 + 19, 3);
                    }
                }
                if (typeShop != 2 || (!Equals(GameCanvas.panel2) && currentTabIndex != 4) || item.buyType == 0)
                {
                    continue;
                }
                if (item.buyType == 1)
                {
                    mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
                    if (item.buyCoin != -1)
                    {
                        g.drawImage(imgThoivang, num2 + num4 - 7, num3 + h - 5, 3);
                        mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                    }
                    else if (item.buyGold != -1)
                    {
                        g.drawImage(imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
                        mFont.tahoma_7b_red.drawString(g, Res.formatNumber2(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                    }
                }
                else if (item.buyType == 2)
                {
                    mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
                    if (item.buyCoin != -1)
                    {
                        g.drawImage(imgThoivang, num2 + num4 - 7, num3 + h - 5, 3);
                        mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2(item.buyCoin), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                    }
                    else if (item.buyGold != -1)
                    {
                        g.drawImage(imgLuongKhoa, num2 + num4 - 7, num3 + h - 5, 3);
                        mFont.tahoma_7b_red.drawString(g, Res.formatNumber2(item.buyGold), num2 + num4 - 17, num3 + h - 9, mFont.RIGHT);
                    }
                }
            }
            paintScrollArrow(g);
        }
        catch (Exception)
        {
        }
    }


    // ==================== paintShopInfo ====================
    private void paintShopInfo(mGraphics g)
    {
        if (currentTabIndex == currentTabName.Length - 1 && GameCanvas.panel2 == null)
        {
            paintMyInfo(g);
        }
        else if (selected < 0)
        {
            if (typeShop != 2)
            {
                mFont.tahoma_7_white.drawString(g, mResources.say_hello, X + 60, 14, 0);
                mFont.tahoma_7_white.drawString(g, strWantToBuy, X + 60, 26, 0);
                return;
            }
            mFont.tahoma_7_white.drawString(g, mResources.say_hello, X + 60, 5, 0);
            mFont.tahoma_7_white.drawString(g, strWantToBuy, X + 60, 17, 0);
            mFont.tahoma_7_white.drawString(g, mResources.page + " " + (currPageShop[currentTabIndex] + 1) + "/" + maxPageShop[currentTabIndex], X + 60, 29, 0);
        }
        else
        {
            if (currentTabIndex < 0 || currentTabIndex > Char.myCharz().arrItemShop.Length - 1 || selected < 0 || selected > Char.myCharz().arrItemShop[currentTabIndex].Length - 1)
            {
                return;
            }
            Item item = Char.myCharz().arrItemShop[currentTabIndex][selected];
            if (item != null)
            {
                if (Equals(GameCanvas.panel) && currentTabIndex <= 3 && typeShop == 2)
                {
                    mFont.tahoma_7b_white.drawString(g, mResources.page + " " + (currPageShop[currentTabIndex] + 1) + "/" + maxPageShop[currentTabIndex], X + 55, 4, 0);
                }
                mFont.tahoma_7b_white.drawString(g, item.template.name, X + 55, 24, 0);
                string st = mResources.pow_request + " " + Res.formatNumber(item.template.strRequire);
                if (item.template.strRequire > Char.myCharz().cPower)
                {
                    mFont.tahoma_7_yellow.drawString(g, st, X + 55, 35, 0);
                }
                else
                {
                    mFont.tahoma_7_green.drawString(g, st, X + 55, 35, 0);
                }
            }
        }
    }


    // ==================== doFireShop ====================
    private void doFireShop()
    {
        currItem = null;
        if (selected < 0)
        {
            return;
        }
        MyVector myVector = new MyVector();
        if (currentTabIndex < currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && type != 17)
        {
            currItem = Char.myCharz().arrItemShop[currentTabIndex][selected];
            if (currItem != null)
            {
                if (currItem.isBuySpec)
                {
                    if (currItem.buySpec > 0)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buySpec), this, 3005, currItem));
                        myVector.addElement(new Command(ModFunc.strAutoBuy, this, 3006, currItem));
                    }
                }
                else if (typeShop == 4)
                {
                    myVector.addElement(new Command(mResources.receive_upper, this, 30001, currItem));
                    myVector.addElement(new Command(mResources.DELETE, this, 30002, currItem));
                    myVector.addElement(new Command(mResources.receive_all, this, 30003, currItem));
                }
                else if (currItem.buyCoin == 0 && currItem.buyGold == 0)
                {
                    if (currItem.powerRequire != 0)
                    {
                        myVector.addElement(new Command(mResources.learn_with + "\n" + Res.formatNumber(currItem.powerRequire) + " \n" + mResources.potential, this, 3004, currItem));
                    }
                    else
                    {
                        myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, currItem));
                    }
                }
                else if (typeShop == 8)
                {
                    if (currItem.buyCoin > 0)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyCoin) + "\n" + mResources.XU, this, 30001, currItem));
                    }
                    if (currItem.buyGold > 0)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyGold) + "\n" + mResources.LUONG, this, 30002, currItem));
                    }
                }
                else if (typeShop != 2)
                {
                    if (currItem.buyCoin > 0)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyCoin) + "\n" + mResources.XU, this, 3000, currItem));
                    }
                    if (currItem.buyGold > 0)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyGold) + "\n" + mResources.LUONG, this, 3001, currItem));
                    }
                    myVector.addElement(new Command(ModFunc.strAutoBuy, this, 3006, currItem));
                }
                else // type 2
                {
                    if (currItem.buyCoin != -1)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyCoin) + "\n" + mResources.XU, this, 10016, currItem));
                    }
                    if (currItem.buyGold != -1)
                    {
                        myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2(currItem.buyGold) + "\n" + mResources.LUONG, this, 10017, currItem));
                    }
                }
            }
        }
        else if (typeShop == 0)
        {
            if (selected == 0)
            {
                setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, resetSelect: false);
            }
            else
            {
                currItem = null;
                if (!GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody))
                {
                    Item item = Char.myCharz().arrItemBag[GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)];
                    if (item != null)
                    {
                        currItem = item;
                    }
                }
                else
                {
                    Item item2 = Char.myCharz().arrItemBody[GetInventorySelect_body(selected, newSelected)];
                    if (item2 != null)
                    {
                        currItem = item2;
                    }
                }
                if (currItem != null)
                {
                    myVector.addElement(new Command(mResources.SALE, this, 3002, currItem));
                }
            }
        }
        else
        {
            if (type == 17)
            {
                currItem = Char.myCharz().arrItemShop[4][selected];
            }
            else
            {
                currItem = Char.myCharz().arrItemShop[currentTabIndex][selected];
            }
            if (currItem.buyType == 0)
            {
                if (currItem.isHaveOption(87))
                {
                    myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, currItem));
                }
                else
                {
                    myVector.addElement(new Command(mResources.kiguiXu, this, 10012, currItem));
                }
            }
            else if (currItem.buyType == 1)
            {
                myVector.addElement(new Command(mResources.huykigui, this, 10014, currItem));
                myVector.addElement(new Command(mResources.upTop, this, 10018, currItem));
            }
            else if (currItem.buyType == 2)
            {
                myVector.addElement(new Command(mResources.nhantien, this, 10015, currItem));
            }
        }
        if (currItem != null)
        {
            Char.myCharz().setPartTemp(currItem.headTemp, currItem.bodyTemp, currItem.legTemp, currItem.bagTemp);
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addItemDetail(currItem);
        }
        else
        {
            cp = null;
        }
    }


    // ==================== isTypeShop ====================
    public bool isTypeShop()
    {
        if (type == 1)
        {
            return true;
        }
        return false;
    }


    // ==================== doNotiRuby ====================
    private void doNotiRuby(int type)
    {
        try
        {
            currItem.buyRuby = int.Parse(chatTField.tfChat.getText());
        }
        catch (Exception)
        {
            GameCanvas.startOKDlg(mResources.input_money_wrong);
            chatTField.isShow = false;
            return;
        }
        Command cmdYes = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
        Command cmdNo = new Command(mResources.NO, this, 11002, null);
        GameCanvas.startYesNoDlg(mResources.notiRuby, cmdYes, cmdNo);
    }


}
