using System;

public partial class Panel
{
    // ==================== setTypeBodyOnly ====================
    // setTypeBodyOnly() extracted to Panel.Body.cs


    // ==================== setTabBody ====================
    // setTabBody() extracted to Panel.Body.cs


    // ==================== setTabInventory ====================
    private void setTabInventory(bool resetSelect)
    {
        if (isnewInventory)
        {
            int num = Char.myCharz().arrItemBag.Length;
            currentListLength = checkCurrentListLength(num);
            currentListLength = 3;
            newSelected = 0;
            size_tab = (sbyte)(num / 20 + ((num % 20 > 0) ? 1 : 0));
            Res.outz("sizeTab = " + size_tab);
            return;
        }
        int len = isCurrentTabBody() ? Char.myCharz().arrItemBody.Length : Char.myCharz().arrItemBag.Length;
        currentListLength = checkCurrentListLength(len);
        ITEM_HEIGHT = 29;
        int numScrollH = ((type == 0 || type == 2) ? (hScroll - 22) : hScroll);
        cmyLim = (currentListLength - 1) * ITEM_HEIGHT - numScrollH;
        cmy = (cmtoY = cmyLast[currentTabIndex]);
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
            cmy = (cmtoY = 0);
        }
        if (resetSelect)
        {
            selected = (GameCanvas.isTouch ? (-1) : 1);
        }
    }


    // ==================== paintInventory ====================
    private void paintInventory(mGraphics g)
    {
        bool flag = true;
        if (flag && isnewInventory)
        {
            Item[] arrItemBody = Char.myCharz().arrItemBody;
            Item[] arrItemBag = Char.myCharz().arrItemBag;
            g.setColor(16711680);
            int num = arrItemBody.Length + arrItemBag.Length;
            int num2 = num / 20 + ((num % 20 > 0) ? 1 : 0) + 1;
            int num3 = 0;
            num3 = 0;
            int num6 = xScroll;
            int num7 = yScroll + num3 * ITEM_HEIGHT;
            int num8 = 34;
            int num9 = ITEM_HEIGHT - 1;
            for (int j = 0; j < 4; j++)
            {
                num6 = xScroll;
                num7 = yScroll + (j + num3) * ITEM_HEIGHT;
                bool flag2 = true;
                for (int k = 0; k < 5; k++)
                {
                    Item item = null;
                    int num10 = 0;
                    if (newSelected > 0)
                    {
                        num10 = (newSelected - 1) * 20;
                        if (j * 5 + k + num10 < arrItemBag.Length)
                        {
                            item = arrItemBag[j * 5 + k + num10];
                            num6 = xScroll + num8 * k;
                            int num11 = sellectInventory % 5;
                            int num12 = sellectInventory / 5;
                            if (newSelected > 0)
                            {
                                g.setColor(15196114);
                            }
                            else
                            {
                                g.setColor(9993045);
                            }
                            g.drawRect(num6, num7, num8, num9);
                            if (j == num12 && k == num11 && selected > 0)
                            {
                                g.setColor(16383818);
                                itemInvenNew = item;
                            }
                            g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
                            if (item != null)
                            {
                                int x2 = num6 + imgNew.getWidth() / 2;
                                int y = num7;
                                int num13 = 34;
                                int h = ITEM_HEIGHT - 1;
                                SmallImage.drawSmallImage(g, item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
                                if (item.quantity > 1)
                                {
                                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num6, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
                                }
                                if (item.newItem && GameCanvas.gameTick % 10 > 5)
                                {
                                    g.drawImage(imgNew, x2, y, 3);
                                }
                                for (int l = 0; l < item.itemOption.Length; l++)
                                {
                                    paintOptSlotItem(g, item.itemOption[l].optionTemplate.id, item.itemOption[l].param, x2, y, num13, h);
                                }
                            }
                            if (!flag2)
                            {
                                break;
                            }
                            continue;
                        }
                        flag2 = false;
                        break;
                    }
                    if (j * 5 + k < arrItemBody.Length)
                    {
                        item = arrItemBody[j * 5 + k];
                        flag2 = false;
                    }
                    else
                    {
                        flag2 = false;
                    }
                    break;
                }
            }
            num3 = ((newSelected != 0) ? 5 : 3);
            int num14 = yScroll + num3 * ITEM_HEIGHT + 5;
            int num15 = 2;
            if (newSelected == 0)
            {
                num15 = 4;
            }
            num6 = xScroll;
            num7 = yScroll + num3 * ITEM_HEIGHT;
            num8 = 34;
            num9 = ITEM_HEIGHT - 1;
            if (newSelected == 0)
            {
                g.setColor(15196114);
                num3 = 1;
                nTableItem = 10;
                int num16 = 5;
                if (eBanner != null)
                {
                    eBanner.paint(g);
                    eBanner.x = num6 + 34 + 34;
                    eBanner.y = num7 + num9 - 25;
                }
                for (int m = 0; m < 10; m++)
                {
                    Item item2 = null;
                    item2 = arrItemBody[m];
                    if (m < 5)
                    {
                        num16 = 0;
                        num6 = xScroll;
                        num7 = yScroll + (m + num3) * ITEM_HEIGHT;
                    }
                    else
                    {
                        num16 = 5;
                        num6 = xScroll + 4 * num8;
                        num7 = yScroll + (m - num16 + num3) * ITEM_HEIGHT;
                    }
                    g.setColor(15196114);
                    g.drawRect(num6, num7, num8, num9);
                    if (sellectInventory == m)
                    {
                        itemInvenNew = item2;
                        g.setColor(16383818);
                    }
                    else
                    {
                        g.setColor(9993045);
                    }
                    g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
                    if (item2 == null)
                    {
                        screenTab6.drawFrame(m, num6 + num8 / 2 - 8, num7 + num9 / 2 - 8, 0, mGraphics.TOP | mGraphics.LEFT, g);
                    }
                    if (item2 != null)
                    {
                        SmallImage.drawSmallImage(g, item2.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
                        if (item2.quantity > 1)
                        {
                            mFont.tahoma_7_yellow.drawString(g, string.Empty + item2.quantity, num6 + 4 * num8, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
                        }
                    }
                }
                num3 = 1;
                num6 = xScroll + 34;
                num7 = yScroll + num3 * ITEM_HEIGHT;
                num8 = 102;
                num9 = 4 * (ITEM_HEIGHT - 1);
                Char.myCharz().paintCharBody(g, num6 + 34 + 17, num7 + num9 - 25, 1, 0, isPaintBag: true);
                num3 = 3;
                num15 = 2;
                num6 = xScroll + 34;
                num7 = yScroll + (1 + num3) * ITEM_HEIGHT - 1;
                num8 = 102;
                num9 = ITEM_HEIGHT * num15;
                g.setColor(15196114);
                g.drawRect(num6, num7, num8, num9);
                g.setColor(9993045);
                g.fillRect(num6 + 1, num7 + 1, num8 - 2, num9 - 2);
                paintItemBodyBagInfo(g, num6 + 3, num7 - 2);
                num3 = ((newSelected != 0) ? 5 : 6);
                num14 = yScroll + num3 * ITEM_HEIGHT;
                g.setColor(15196114);
                if (newSelected == 0)
                {
                    num15 = 1;
                }
                g.drawRect(xScroll, num14, wScroll, ITEM_HEIGHT * num15);
                g.setColor(16777215);
                g.fillRect(xScroll + 1, num14 + 1, wScroll - 2, ITEM_HEIGHT * num15 - 2);
            }
            if (itemInvenNew != null && itemInvenNew.itemOption != null)
            {
                string text = string.Empty;
                mFont mFont2 = mFont.tahoma_7_green2;
                if (itemInvenNew.itemOption != null)
                {
                    for (int n = 0; n < itemInvenNew.itemOption.Length; n++)
                    {
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 72)
                        {
                            text = " [+" + itemInvenNew.itemOption[n].param + "]";
                        }
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 225)
                        {
                            text = " [+" + itemInvenNew.itemOption[n].param + "]";
                        }
                     
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 225)
                        {
                            if (itemInvenNew.itemOption[n].param >= 1 && itemInvenNew.itemOption[n].param <= 5)
                            {
                                mFont2 = GetFont(2);
                            }                          
                            else if (itemInvenNew.itemOption[n].param >= 6 && itemInvenNew.itemOption[n].param <= 7)
                            {
                                mFont2 = GetFont(8);
                            }
                            else if (itemInvenNew.itemOption[n].param >= 8 && itemInvenNew.itemOption[n].param <= 10)
                            {
                                mFont2 = GetFont(7);
                            }
                        }
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 72)
                        {
                            if (itemInvenNew.itemOption[n].param >= 1 && itemInvenNew.itemOption[n].param <= 5)
                            {
                                mFont2 = GetFont(2);
                            }

                            else if (itemInvenNew.itemOption[n].param >= 6 && itemInvenNew.itemOption[n].param <= 7)
                            {
                                mFont2 = GetFont(8);
                            }
                            else if (itemInvenNew.itemOption[n].param >= 8 && itemInvenNew.itemOption[n].param <= 10)
                            {
                                mFont2 = GetFont(7);
                            }
                        }
                    }
                }
                mFont2.drawString(g, itemInvenNew.template.name + text, xScroll + 5, num14 + 1, 0);
                string text2 = string.Empty;
                if (itemInvenNew.itemOption != null)
                {
                    if (itemInvenNew.itemOption.Length > 0 && itemInvenNew.itemOption[0] != null
                          && itemInvenNew.itemOption[0].optionTemplate.id != 72
                          && itemInvenNew.itemOption[0].optionTemplate.id != 206
                            && itemInvenNew.itemOption[0].optionTemplate.id != 225
                              && itemInvenNew.itemOption[0].optionTemplate.id != 34
                                && itemInvenNew.itemOption[0].optionTemplate.id != 35
                                && itemInvenNew.itemOption[0].optionTemplate.id != 36
                        && itemInvenNew.itemOption[0].optionTemplate.id != 102 
                        && itemInvenNew.itemOption[0].optionTemplate.id != 107)
                    {
                        text2 += itemInvenNew.itemOption[0].getOptionString();
                    }
                    mFont mFont3 = mFont.tahoma_7_blue;
                    if (itemInvenNew.compare < 0 && itemInvenNew.template.type != 5)
                    {
                        mFont3 = mFont.tahoma_7_red;
                    }
                    if (itemInvenNew.itemOption.Length > 1)
                    {
                        for (int num17 = 1; num17 < Math.min(itemInvenNew.itemOption.Length,3); num17++)
                        {
                            if (itemInvenNew.itemOption[num17] != null
                                  && itemInvenNew.itemOption[num17].optionTemplate.id != 72
                          && itemInvenNew.itemOption[num17].optionTemplate.id != 206
                            && itemInvenNew.itemOption[num17].optionTemplate.id != 225
                              && itemInvenNew.itemOption[num17].optionTemplate.id != 34
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 35
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 36
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 102
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 107)
                            {
                                text2 = text2 + "| " + itemInvenNew.itemOption[num17].getOptionString();
                            }
                        }
                    }
                    try
                    {
                        if (mFont3.getWidth(text2) > wScroll)
                        {
                            text2 = mFont3.splitFontArray(text2, wScroll)[0];
                        }
                    }
                    catch (Exception)
                    {
                    }
                    mFont3.drawString(g, text2, xScroll + 5, num14 + 10, mFont.LEFT);
                }
            }
        }
        if (flag && isnewInventory)
        {
            return;
        }
        g.setColor(16711680);
        Item[] arrItemBody2 = Char.myCharz().arrItemBody;
        Item[] arrItemBag2 = Char.myCharz().arrItemBag;
        bool isBody = isCurrentTabBody();
        Item[] currentItems = isBody ? arrItemBody2 : arrItemBag2;
        currentListLength = checkCurrentListLength(currentItems.Length);
        bool hasSubTabs = (type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1);
        int startY = hasSubTabs ? (yScroll + 22) : yScroll;
        int scrollH = hasSubTabs ? (hScroll - 22) : hScroll;
        if (hasSubTabs)
        {
            int subTabY = yScroll;
            int subTabH = UITheme.SUB_TAB_HEIGHT;
            int subTabW = (wScroll - 2) / 2;
            int subTab0_x = xScroll + 1;
            int subTab1_x = xScroll + 1 + subTabW;
            // Tab 0: Túi đồ
            g.setColor((subTabInventory == 0) ? UITheme.COLOR_TAB_INACTIVE : UITheme.COLOR_SUBTAB_INACTIVE);
            g.fillRect(subTab0_x, subTabY, subTabW, subTabH);
            if (subTabInventory == 0)
            {
                g.setColor(UITheme.COLOR_TAB_ACTIVE_GREEN);
                g.fillRect(subTab0_x, subTabY + subTabH - 2, subTabW, 2);
            }
            mFont font0 = (subTabInventory == 0) ? mFont.tahoma_7_green2 : mFont.tahoma_7_grey;
            font0.drawString(g, "Túi đồ", subTab0_x + subTabW / 2, subTabY + 4, mFont.CENTER);
            // Tab 1: Trang bị
            g.setColor((subTabInventory == 1) ? UITheme.COLOR_TAB_INACTIVE : UITheme.COLOR_SUBTAB_INACTIVE);
            g.fillRect(subTab1_x, subTabY, subTabW, subTabH);
            if (subTabInventory == 1)
            {
                g.setColor(UITheme.COLOR_TAB_ACTIVE_GREEN);
                g.fillRect(subTab1_x, subTabY + subTabH - 2, subTabW, 2);
            }
            mFont font1 = (subTabInventory == 1) ? mFont.tahoma_7_green2 : mFont.tahoma_7_grey;
            font1.drawString(g, "Trang bị", subTab1_x + subTabW / 2, subTabY + 4, mFont.CENTER);
        }
        g.setClip(xScroll, startY, wScroll, scrollH);
        g.translate(0, -cmy);
        try
        {
            for (int num22 = 1; num22 < currentListLength; num22++)
            {
                int num23 = xScroll + 29;
                int num24 = startY + (num22 - 1) * ITEM_HEIGHT;
                int num25 = wScroll - 29;
                int h2 = ITEM_HEIGHT - 1;
                int num26 = xScroll;
                int num27 = startY + (num22 - 1) * ITEM_HEIGHT;
                int num28 = ITEM_HEIGHT - 1;
                int num29 = ITEM_HEIGHT - 1;
                if (num24 - cmy > startY + scrollH || num24 - cmy < startY - ITEM_HEIGHT)
                {
                    continue;
                }
                int itemIndex = num22 - 1;
                Item item3 = (itemIndex >= 0 && itemIndex < currentItems.Length) ? currentItems[itemIndex] : null;
                g.setColor((num22 == selected) ? 16383818 : ((!isBody) ? 15723751 : 15196114));
                g.fillRect(num23, num24, num25, h2);
                g.setColor((num22 == selected) ? 9541120 : ((!isBody) ? 11837316 : 9993045));
                if (item3 != null)
                {
                    for (int num30 = 0; num30 < item3.itemOption.Length; num30++)
                    {
                        if (item3.itemOption[num30].optionTemplate.id == 72 && item3.itemOption[num30].param > 0)
                        {
                            byte id = (byte)GetColor_Item_Upgrade(item3.itemOption[num30].param);
                            int color_ItemBg = GetColor_ItemBg(id);
                            if (color_ItemBg != -1)
                            {
                                g.setColor((num22 != selected) ? GetColor_ItemBg(id) : GetColor_ItemBg(id));
                            }
                        }
                    }
                    foreach (ItemAuto itemAuto in ModFunc.GI().listItemAuto)
                    {
                        if (item3.template.id == itemAuto.id && item3.template.iconID == itemAuto.iconID)
                        {
                            g.setColor((num22 != selected) ? color1[1] : color2[1]);
                        }
                    }
                }
                
                g.setColor(6047789,0.5f);
                g.fillRect(num26, num27, num28, num29);paintEffectItem(g, item3, num26, num27);
                if (item3 != null && item3.isSelect && GameCanvas.panel.type == 12)
                {
                    g.setColor((num22 != selected) ? 6047789 : 7040779);
                    g.fillRect(num26, num27, num28, num29);
                }
                if (item3 == null)
                {
                    if (isBody)
                    {
                        mFont.tahoma_7_grey.drawString(g, "[" + getBodySlotName(itemIndex) + "]", num23 + 5, num24 + 7, 0);
                    }
                    continue;
                }
                string text3 = string.Empty;
                mFont mFont4 = mFont.tahoma_7_green2;
                if (item3.itemOption != null)
                {
                    for (int num31 = 0; num31 < item3.itemOption.Length; num31++)
                    {
                        if (item3.itemOption[num31].optionTemplate.id == 72)
                        {
                            text3 = " [+" + item3.itemOption[num31].param + "]";
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 225)
                        {
                            text3 = " [+" + item3.itemOption[num31].param + "]";
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 225)
                        {
                            if (item3.itemOption[num31].param >= 1 && item3.itemOption[num31].param <= 2)
                            {
                                mFont4 = GetFont(0);
                            }
                            else if (item3.itemOption[num31].param >= 3 && item3.itemOption[num31].param <= 4)
                            {
                                mFont4 = GetFont(2);
                            }
                            else if (item3.itemOption[num31].param >= 5 && item3.itemOption[num31].param <= 6)
                            {
                                mFont4 = GetFont(8);
                            }
                            else if (item3.itemOption[num31].param >= 7 && item3.itemOption[num31].param <= 10)
                            {
                                mFont4 = GetFont(7);
                            }
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 72)
                        {
                            if (item3.itemOption[num31].param >= 1 && item3.itemOption[num31].param <= 5)
                            {
                                mFont4 = GetFont(2);
                            }
                            
                            else if (item3.itemOption[num31].param >= 6 && item3.itemOption[num31].param <= 7)
                            {
                                mFont4 = GetFont(8);
                            }
                            else if (item3.itemOption[num31].param >= 8 && item3.itemOption[num31].param <= 10)
                            {
                                mFont4 = GetFont(7);
                            }
                        }
                    }
                }
                string itemNamePrefix = isBody ? ("[" + getBodySlotName(itemIndex) + "] ") : "";
                mFont4.drawString(g, itemNamePrefix + item3.template.name + text3, num23 + 5, num24 + 1, 0);
                string text4 = string.Empty;
                if (item3.itemOption != null)
                {
                    if (item3.itemOption.Length > 0 && item3.itemOption[0] != null
                        && item3.itemOption[0].optionTemplate.id != 72
                          && item3.itemOption[0].optionTemplate.id != 206
                            && item3.itemOption[0].optionTemplate.id != 225
                              && item3.itemOption[0].optionTemplate.id != 34
                                && item3.itemOption[0].optionTemplate.id != 35 
                                && item3.itemOption[0].optionTemplate.id != 36
                                  && item3.itemOption[0].optionTemplate.id != 102
                        && item3.itemOption[0].optionTemplate.id != 107)
                    {
                        text4 += item3.itemOption[0].getOptionString();
                    }
                    mFont mFont5 = mFont.tahoma_7_blue;
                    if (item3.compare < 0 && item3.template.type != 5)
                    {
                        mFont5 = mFont.tahoma_7_red;
                    }
                    if (item3.itemOption.Length > 1)
                    {
                        for (int num32 = 1; num32 < Math.min(item3.itemOption.Length , 3); num32++)
                        {
                            if (item3.itemOption[num32] != null
                                 && item3.itemOption[num32].optionTemplate.id != 72
                          && item3.itemOption[num32].optionTemplate.id != 206
                            && item3.itemOption[num32].optionTemplate.id != 225
                              && item3.itemOption[num32].optionTemplate.id != 34
                                && item3.itemOption[num32].optionTemplate.id != 35
                                && item3.itemOption[num32].optionTemplate.id != 36
                                && item3.itemOption[num32].optionTemplate.id != 102 
                                && item3.itemOption[num32].optionTemplate.id != 107)
                            {
                                text4 = text4 + "| " + item3.itemOption[num32].getOptionString();
                            }
                        }
                    }
                    mFont5.drawString(g, text4, num23 + 5, num24 + 10, mFont.LEFT);
                }
                SmallImage.drawSmallImage(g, item3.template.iconID, num26 + num28 / 2, num27 + num29 / 2, 0, 3);
                if (item3.itemOption != null)
                {
                    for (int num33 = 0; num33 < item3.itemOption.Length; num33++)
                    {
                        paintOptItemInventory(g, item3.itemOption[num33].optionTemplate.id, item3.itemOption[num33].param, num26, num27, num28, num29, item3);
                    }
                    for (int num34 = 0; num34 < item3.itemOption.Length; num34++)
                    {
                        paintOptSlotItem(g, item3.itemOption[num34].optionTemplate.id, item3.itemOption[num34].param, num26, num27, num28, num29);
                    }
                }
                if (item3.quantity > 1)
                {
                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item3.quantity, num26 + num28, num27 + num29 - mFont.tahoma_7_yellow.getHeight(), 1);
                }
            }
        }
        catch (Exception)
        {
        }
        g.translate(0, cmy);
        paintScrollArrow(g);
    }


    // ==================== paintBottomMoneyInfo ====================
    private void paintBottomMoneyInfo(mGraphics g)
    {
        try
        {
            if (type != 13 || (currentTabIndex != 2 && !Equals(GameCanvas.panel2)))
            {
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
                g.setColor(11837316);
                g.fillRect(X + 1, H - 15, W - 2, 14);
                g.setColor(13524492);
                g.fillRect(X + 1, H - 15, W - 2, 1);
                if (imgXu != null)
                {
                    g.drawImage(imgXu, X + 11, H - 7, 3);
                }
                if (imgLuong != null)
                {
                    g.drawImage(imgLuong, X + 75, H - 8, 3);
                }
                if (Char.myCharz() != null)
                {
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().xuStr + string.Empty, X + 24, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongStr + string.Empty, X + 85, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                    if (imgLuongKhoa != null)
                    {
                        g.drawImage(imgLuongKhoa, X + 130, H - 8, 3);
                    }
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongKhoaStr + string.Empty, X + 140, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                }
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintBottomMoneyInfo: " + ex.ToString());
        }
    }


    // ==================== getCompare ====================
    public int getCompare(Item item)
    {
        if (item == null)
        {
            return -1;
        }
        if (item.isTypeBody())
        {
            if (item.itemOption == null)
            {
                return -1;
            }
            ItemOption itemOption = item.itemOption[0];
            if (itemOption.optionTemplate.id == 22)
            {
                itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
                itemOption.param *= 1000;
            }
            if (itemOption.optionTemplate.id == 23)
            {
                itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
                itemOption.param *= 1000;
            }
            Item item2 = null;
            for (int i = 0; i < Char.myCharz().arrItemBody.Length; i++)
            {
                Item item3 = Char.myCharz().arrItemBody[i];
                if (itemOption.optionTemplate.id == 22)
                {
                    itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
                    itemOption.param *= 1000;
                }
                if (itemOption.optionTemplate.id == 23)
                {
                    itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
                    itemOption.param *= 1000;
                }
                if (item3 != null && item3.itemOption != null && item3.template.type == item.template.type)
                {
                    item2 = item3;
                    break;
                }
            }
            if (item2 == null)
            {
                isUp = true;
                return itemOption.param;
            }
            int num = 0;
            num = ((item2 == null || item2.itemOption == null) ? itemOption.param : (itemOption.param - item2.itemOption[0].param));
            if (num < 0)
            {
                isUp = false;
            }
            else
            {
                isUp = true;
            }
            return num;
        }
        return 0;
    }


    // ==================== formatLargeNumber ====================
    public static String formatLargeNumber(long value)
    {
        if (value >= 1_000_000_000_000_000_000L)
        {
            return (value / 1_000_000_000_000_000_000L) + " B Tỷ";
        }else
        if (value >= 1_000_000_000_000_000L)
        {
            return (value / 1_000_000_000_000_000L) + " M Tỷ";
        }
        else if (value >= 1_000_000_000_000L)
        {
            return (value / 1_000_000_000_000L) + " K Tỷ";
        }
        //else if (value >= 1_000_000_000L)
        //{
        //    return (value / 1_000_000_000L) + " Tỷ";
        //}
        else
        {
            return value + ""; // Chuyển đổi số thành chuỗi nếu dưới 1 tỷ
        }
    }


    // ==================== paintItemBodyBagInfo1 ====================
    // paintItemBodyBagInfo1() extracted to Panel.Body.cs


    // ==================== paintItemBodyBagInfo2 ====================
    // paintItemBodyBagInfo2() extracted to Panel.Body.cs


    // ==================== doFireInventory ====================
    private void doFireInventory()
    {
        if (Char.myCharz().statusMe == 14)
        {
            GameCanvas.startOKDlg(mResources.can_not_do_when_die);
        }
        else
        {
            if (selected == -1)
            {
                return;
            }
            if (selected == 0)
            {
                setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, resetSelect: false);
                return;
            }
            currItem = null;
            MyVector myVector = new MyVector();
            if (isnewInventory && isnewInventory)
            {
                currItem = itemInvenNew;
                if (newSelected == 0)
                {
                    myVector.addElement(new Command(mResources.GETOUT, this, 2002, currItem));
                }
                else if (GameCanvas.panel.type == 12)
                {
                    myVector.addElement(new Command(mResources.use_for_combine, this, 6000, currItem));
                }
                else if (GameCanvas.panel.type == 13)
                {
                    myVector.addElement(new Command(mResources.use_for_trade, this, 7000, currItem));
                }
                else if (currItem.isTypeBody())
                {
                    myVector.addElement(new Command(mResources.USE, this, 2000, currItem));
                    if (Char.myCharz().havePet)
                    {
                        myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                    }
                    if (Char.myCharz().havePet2)
                    {
                        myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                    }
                }
                else
                {
                    myVector.addElement(new Command(mResources.USE, this, 2001, currItem));
                    if (Char.myCharz().havePet)
                    {
                        myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                    }
                    if (Char.myCharz().havePet2)
                    {
                        myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                    }
                }
            }
            else if (!GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody))
            {
                Item item = Char.myCharz().arrItemBag[GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)];
                if (item != null)
                {
                    currItem = item;
                    if (GameCanvas.panel.type == 12)
                    {
                        myVector.addElement(new Command(mResources.use_for_combine, this, 6000, currItem));
                    }
                    else if (GameCanvas.panel.type == 13)
                    {
                        myVector.addElement(new Command(mResources.use_for_trade, this, 7000, currItem));
                    }
                    else if (item.isTypeBody())
                    {
                        myVector.addElement(new Command(mResources.USE, this, 2000, currItem));
                        if (Char.myCharz().havePet)
                        {
                            myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                        }
                        if (Char.myCharz().havePet2)
                        {
                            myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                        }
                    }
                    else
                    {
                        myVector.addElement(new Command(mResources.USE, this, 2001, currItem));
                        if (Char.myCharz().havePet)
                        {
                            myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                        }
                        if (Char.myCharz().havePet2)
                        {
                            myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                        }
                    }
                }
            }
            else
            {
                Item item2 = Char.myCharz().arrItemBody[GetInventorySelect_body(selected, newSelected)];
                if (item2 != null)
                {
                    currItem = item2;
                    myVector.addElement(new Command(mResources.GETOUT, this, 2002, currItem));
                }
            }
            if (currItem != null)
            {
                Char.myCharz().setPartTemp(currItem.headTemp, currItem.bodyTemp, currItem.legTemp, currItem.bagTemp);
                if (GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13)
                {
                    if (position == 0)
                    {
                        myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, currItem));
                        if (this.currItem.template.type == 29 || this.currItem.template.type == 33 || this.currItem.template.id == 380 || this.currItem.quantity >= 2)
                        {
                            if (ModFunc.GI().listItemAuto.Exists(i => i.id == currItem.template.id))
                            {
                                myVector.addElement(new Command(ModFunc.strRemoveAutoItem, ModFunc.GI(), 501, currItem));
                            }
                            else
                            {
                                myVector.addElement(new Command(ModFunc.strAddAutoItem, ModFunc.GI(), 500, currItem));
                            }
                        }
                    }
                    if (position == 1)
                    {
                        myVector.addElement(new Command(mResources.SALE, this, 3002, currItem));
                    }
                }
                int numScrollY = ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1)) ? (yScroll + 22) : yScroll;
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + numScrollY);
                addItemDetail(currItem);
            }
            else
            {
                cp = null;
            }
        }
    }


    // ==================== isCurrentTabBody ====================
    // isCurrentTabBody() extracted to Panel.Body.cs


    // ==================== getBodySlotName ====================
    // getBodySlotName() extracted to Panel.Body.cs


    // ==================== GetInventorySelect_isbody ====================
    // GetInventorySelect_isbody() extracted to Panel.Body.cs


    // ==================== GetInventorySelect_body ====================
    // GetInventorySelect_body() extracted to Panel.Body.cs


    // ==================== GetInventorySelect_bag ====================
    // GetInventorySelect_bag() extracted to Panel.Body.cs


    // ==================== isTabInven ====================
    private bool isTabInven()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 7 && currentTabIndex == 0) || (type == 2 && (currentTabIndex == 0 || currentTabIndex == 1)) || (type == 12 && currentTabIndex == 1) || ((type == 21 || type == 28) && currentTabIndex == 3))
        {
            return true;
        }
        return false;
    }


    // ==================== updateKeyInvenTab ====================
    private void updateKeyInvenTab()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            subTabInventory = (subTabInventory == 0) ? 1 : 0;
            setTabInventory(resetSelect: true);
            SoundMn.gI().panelClick();
        }
    }


    // ==================== updateKeyInventory ====================
    private void updateKeyInventory()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            int subTabY = yScroll;
            int subTabH = 20;
            int subTabW = (wScroll - 2) / 2;
            int subTab0_x = xScroll + 1;
            int subTab1_x = xScroll + 1 + subTabW;
            if (GameCanvas.isPointerHoldIn(subTab0_x, subTabY, subTabW, subTabH))
            {
                if (GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.isPointerJustRelease = false;
                    if (subTabInventory != 0)
                    {
                        subTabInventory = 0;
                        setTabInventory(resetSelect: true);
                    }
                    SoundMn.gI().panelClick();
                }
                return;
            }
            if (GameCanvas.isPointerHoldIn(subTab1_x, subTabY, subTabW, subTabH))
            {
                if (GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.isPointerJustRelease = false;
                    if (subTabInventory != 1)
                    {
                        subTabInventory = 1;
                        setTabInventory(resetSelect: true);
                    }
                    SoundMn.gI().panelClick();
                }
                return;
            }
        }
        updateKeyScrollView();
    }


    // ==================== checkCurrentListLength ====================
    private int checkCurrentListLength(int arrLength)
    {
        newSelected = 0;
        size_tab = 1;
        return arrLength + 1;
    }


    // ==================== setNewSelected ====================
    private void setNewSelected(int arrLength, bool resetSelect)
    {
        newSelected = 0;
        setTabInventory(resetSelect);
    }


}
