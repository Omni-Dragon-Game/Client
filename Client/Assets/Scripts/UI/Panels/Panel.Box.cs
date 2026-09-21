using System;

public partial class Panel
{
    // ==================== setTypeBox ====================
    public void setTypeBox()
    {
        type = 2;
        if (GameCanvas.w > 2 * WIDTH_PANEL)
        {
            boxTabName = new string[1][] { mResources.chestt };
        }
        else
        {
            boxTabName = new string[2][]
            {
                mResources.chestt,
                mResources.inventory
            };
        }
        tabName[2] = boxTabName;
        subTabInventory = 0;
        setType(0);
        if (currentTabIndex == 0)
        {
            setTabBox();
        }
        if (currentTabIndex == 1)
        {
            setTabInventory(resetSelect: true);
        }
        if (GameCanvas.w > 2 * WIDTH_PANEL)
        {
            GameCanvas.panel2 = new Panel();
            GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
            GameCanvas.panel2.setTypeBodyOnly();
            GameCanvas.panel2.show();
        }
    }


    // ==================== setTabBox ====================
    private void setTabBox()
    {
        currentListLength = checkCurrentListLength(Char.myCharz().arrItemBox.Length);
        ITEM_HEIGHT = 29;
        cmyLim = (currentListLength - 1) * ITEM_HEIGHT - hScroll;
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
        selected = (GameCanvas.isTouch ? (-1) : 1);
    }


    // ==================== paintBox ====================
    private void paintBox(mGraphics g)
    {
        g.setColor(16711680);
        Item[] arrItemBox = Char.myCharz().arrItemBox;
        currentListLength = checkCurrentListLength(arrItemBox.Length);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        try
        {
            for (int i = 1; i < currentListLength; i++)
            {
                int num2 = xScroll + 29;
                int num3 = yScroll + (i - 1) * ITEM_HEIGHT;
                int num4 = wScroll - 29;
                int h = ITEM_HEIGHT - 1;
                int num5 = xScroll;
                int num6 = yScroll + (i - 1) * ITEM_HEIGHT;
                int num7 = ITEM_HEIGHT - 1;
                int num8 = ITEM_HEIGHT - 1;

                if (num3 - cmy > yScroll + hScroll || num3 - cmy < yScroll - ITEM_HEIGHT)
                {
                    continue;
                }
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(num2, num3, num4, h);
                g.setColor((i != selected) ? 9993045 : 9541120);

                int inventorySelect_body = GetInventorySelect_body(i, newSelected);
                Item item = arrItemBox[inventorySelect_body];
                if (item != null)
                {
                    for (int k = 0; k < item.itemOption.Length; k++)
                    {
                        if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
                        {
                            sbyte color_Item_Upgrade = GetColor_Item_Upgrade(item.itemOption[k].param);
                            int color_ItemBg = GetColor_ItemBg(color_Item_Upgrade);
                            if (color_ItemBg != -1)
                            {
                                g.setColor((i != selected) ? GetColor_ItemBg(color_Item_Upgrade) : GetColor_ItemBg(color_Item_Upgrade));
                            }
                        }
                    }
                }
               // g.fillRect(num5, num6, num7, num8);
                g.setColor(6047789, 0.5f);
                g.fillRect(num5, num6, num7, num8); paintEffectItem(g, item, num5, num6);
                if (item == null)
                {
                    continue;
                }
                string text = string.Empty;
                mFont mFont2 = mFont.tahoma_7_green2;
                if (item.itemOption != null)
                {
                    for (int l = 0; l < item.itemOption.Length; l++)
                    {
                        if (item.itemOption[l].optionTemplate.id == 72)
                        {
                            text = " [+" + item.itemOption[l].getOptionString() + "]";
                        }
                        
                        if (item.itemOption[l].optionTemplate.id == 225)
                        {
                            if (item.itemOption[l].param >= 1 && item.itemOption[l].param <= 2)
                            {
                                mFont2 = GetFont(0);
                            }
                            else if (item.itemOption[l].param >= 3 && item.itemOption[l].param <= 4)
                            {
                                mFont2 = GetFont(2);
                            }
                            else if (item.itemOption[l].param >= 5 && item.itemOption[l].param <= 6)
                            {
                                mFont2 = GetFont(8);
                            }
                            else if (item.itemOption[l].param >= 7 && item.itemOption[l].param <= 10)
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
                        for (int m = 1; m < Math.min(item.itemOption.Length,3); m++)
                        {
                            if (item.itemOption[m] != null
                                && item.itemOption[m].optionTemplate.id != 72
                                && item.itemOption[m].optionTemplate.id != 225
                                && item.itemOption[m].optionTemplate.id != 206
                                && item.itemOption[m].optionTemplate.id != 34
                                && item.itemOption[m].optionTemplate.id != 35
                                && item.itemOption[m].optionTemplate.id != 36
                                && item.itemOption[m].optionTemplate.id != 102
                                && item.itemOption[m].optionTemplate.id != 107)
                            {
                                text2 = text2 + "| " + item.itemOption[m].getOptionString();
                            }
                        }
                    }
                    mFont3.drawString(g, text2, num2 + 5, num3 + 10, mFont.LEFT);
                }
                SmallImage.drawSmallImage(g, item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
                if (item.itemOption != null)
                {
                    for (int n = 0; n < item.itemOption.Length; n++)
                    {
                        paintOptItemInventory(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8, item);
                    }
                    for (int num10 = 0; num10 < item.itemOption.Length; num10++)
                    {
                        paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
                    }
                }
                if (item.quantity > 1)
                {
                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
                }
            }
        }
        catch (Exception)
        {
        }
        g.translate(0, cmy);
        paintScrollArrow(g);
    }


    // ==================== paintItemBoxInfo ====================
    private void paintItemBoxInfo(mGraphics g)
    {
        string st = mResources.used + ": " + hasUse + "/" + Char.myCharz().arrItemBox.Length + " " + mResources.place;
        mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
        mFont.tahoma_7_yellow.drawString(g, st, 60, 16, 0);
    }


    // ==================== doFireBox ====================
    private void doFireBox()
    {
        if (selected < 0)
        {
            return;
        }
        currItem = null;
        MyVector myVector = new MyVector();
        if (currentTabIndex == 0 && !Equals(GameCanvas.panel2))
        {
            if (selected == 0)
            {
                setNewSelected(Char.myCharz().arrItemBox.Length, resetSelect: false);
            }
            else
            {
                sbyte b = (sbyte)GetInventorySelect_body(selected, newSelected);
                Item item = Char.myCharz().arrItemBox[b];
                if (item != null)
                {
                    if (isBoxClan)
                    {
                        myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
                        myVector.addElement(new Command(mResources.USE, this, 2010, item));
                    }
                    else if (item.isTypeBody())
                    {
                        myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
                    }
                    else
                    {
                        myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
                    }
                    currItem = item;
                }
            }
        }
        if (currentTabIndex == 1 || Equals(GameCanvas.panel2))
        {
            if (selected == 0)
            {
                setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, resetSelect: true);
            }
            else
            {
                Item[] arrItemBody = Char.myCharz().arrItemBody;
                if (!GetInventorySelect_isbody(selected, newSelected, arrItemBody))
                {
                    sbyte b2 = (sbyte)GetInventorySelect_bag(selected, newSelected, arrItemBody);
                    if (b2 >= 0 && b2 < Char.myCharz().arrItemBag.Length)
                    {
                        Item item2 = Char.myCharz().arrItemBag[b2];
                        if (item2 != null)
                        {
                            myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
                            if (item2.isTypeBody())
                            {
                                myVector.addElement(new Command(mResources.USE, this, 2000, item2));
                            }
                            else
                            {
                                myVector.addElement(new Command(mResources.USE, this, 2001, item2));
                            }
                            currItem = item2;
                        }
                    }
                }
                else
                {
                    int b3 = GetInventorySelect_body(selected, newSelected);
                    if (b3 >= 0 && b3 < Char.myCharz().arrItemBody.Length)
                    {
                        Item item3 = Char.myCharz().arrItemBody[b3];
                        if (item3 != null)
                        {
                            myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
                            currItem = item3;
                        }
                    }
                }
            }
        }
        if (currItem != null)
        {
            Char.myCharz().setPartTemp(currItem.headTemp, currItem.bodyTemp, currItem.legTemp, currItem.bagTemp);
            if (isBoxClan)
            {
                myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, currItem));
            }
            int numScrollY = (currentTabIndex == 1) ? (yScroll + 22) : yScroll;
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + numScrollY);
            addItemDetail(currItem);
        }
        else
        {
            cp = null;
        }
        int numScrollH = (currentTabIndex == 1) ? (hScroll - 22) : hScroll;
        cmyLim = currentListLength * ITEM_HEIGHT - numScrollH;
    }


}
