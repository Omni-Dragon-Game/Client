using System;

public partial class Panel
{
    // ==================== setTypeTop ====================
    public void setTypeTop(sbyte t)
    {
        type = 15;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabTop();
        isThachDau = t != 0;
    }


    // ==================== setTabTop ====================
    public void setTabTop()
    {
        currentListLength = vTop.size();
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


    // ==================== paintTop ====================
    public void paintTop(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (currentListLength == 0)
        {
            return;
        }
        int num = (cmy + hScroll) / 24 + 1;
        if (num < hScroll / 24 + 1)
        {
            num = hScroll / 24 + 1;
        }
        if (num > currentListLength)
        {
            num = currentListLength;
        }
        int num2 = cmy / 24;
        if (num2 >= num)
        {
            num2 = num - 1;
        }
        if (num2 < 0)
        {
            num2 = 0;
        }
        for (int i = num2; i < num; i++)
        {
            int num3 = xScroll;
            int num4 = yScroll + i * ITEM_HEIGHT;
            int num5 = 29;
            int h = ITEM_HEIGHT - 1;
            int num6 = xScroll + num5;
            int num7 = yScroll + i * ITEM_HEIGHT;
            int num8 = wScroll - num5;
            int num9 = ITEM_HEIGHT - 1;
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num6, num7, num8, num9);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num3, num4, num5, h);
            TopInfo topInfo = (TopInfo)vTop.elementAt(i);
            if (topInfo.headICON != -1)
            {
                SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
            }
            else
            {
                Part part = GameScr.parts[topInfo.headID];
                SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num3 + part.pi[Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
            }
            g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
            if (topInfo.pId != Char.myCharz().charID)
            {
                mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
            }
            else
            {
                mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
            }
            mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
            mFont.tahoma_7_green2.drawString(g, mResources.rank + ": " + topInfo.rank + string.Empty, num6 + 5, num7 + 11, 0);
        }
        paintScrollArrow(g);
    }


    // ==================== paintPageBar ====================
    private void paintPageBar(mGraphics g, int pageBarY, int pageBarH, int totalPages)
    {
    }


    // ==================== paintTopInfo ====================
    private void paintTopInfo(mGraphics g)
    {
        try
        {
            g.setClip(X + 1, Y, W - 2, yScroll - 2);
            g.setColor(9993045);
            g.fillRect(X, Y, W - 2, 50);
            switch (type)
            {
            case 13:
                if (currentTabIndex == 0 || currentTabIndex == 1)
                {
                    if (Equals(GameCanvas.panel))
                    {
                        SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                        paintGiaoDichInfo(g);
                    }
                    if (Equals(GameCanvas.panel2) && charMenu != null)
                    {
                        SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                        paintCharInfo(g, charMenu);
                    }
                }
                if (currentTabIndex == 2 && charMenu != null)
                {
                    SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                    paintCharInfo(g, charMenu);
                }
                break;
            case 12:
                if (currentTabIndex == 0)
                {
                    int id = 1410;
                    for (int i = 0; i < GameScr.vNpc.size(); i++)
                    {
                        Npc npc = (Npc)GameScr.vNpc.elementAt(i);
                        if (npc.template.npcTemplateId == idNPC)
                        {
                            id = npc.avatar;
                        }
                    }
                    SmallImage.drawSmallImage(g, id, X + 25, 50, 0, 33);
                    paintCombineInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintMyInfo(g);
                }
                break;
            case 11:
            case 16:
            case 23:
            case 24:
            case 27:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 15:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 9:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 21:
            case 28:
                Char pet = type == 28 ? Char.MyPet2z() : Char.myPetz();
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetInfo(g, type == 28);
                }
                else if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetSkillInfo(g, type == 28);
                }
                else if (currentTabIndex == 2)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetStatusInfo(g, type == 28);
                }
                else if (currentTabIndex == 3)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintItemBodyBagInfo(g);
                }
                break;
            case 0:
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintMyInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    if (isnewInventory)
                    {
                        paintCharInfo(g, Char.myCharz());
                    }
                    else
                    {
                        paintItemBodyBagInfo(g);
                    }
                }
                if (currentTabIndex == 2)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintSkillInfo(g);
                }
                if (currentTabIndex == 3)
                {
                    if (mainTabName.Length == 5)
                    {
                        paintClanInfo(g);
                    }
                    else
                    {
                        SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                        paintToolInfo(g);
                    }
                }
                if (currentTabIndex == 4)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintToolInfo(g);
                }
                break;
            case 25:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 2:
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, 526, X + 25, 50, 0, 33);
                    paintItemBoxInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintItemBodyBagInfo(g);
                }
                break;
            case 3:
                SmallImage.drawSmallImage(g, 561, X + 25, 50, 0, 33);
                paintZoneInfo(g);
                break;
            case 1:
                if (currentTabIndex == currentTabName.Length - 1 && GameCanvas.panel2 == null)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                }
                else
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().npcFocus.avatar, X + 25, 50, 0, 33);
                }
                paintShopInfo(g);
                break;
            case 4:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMapInfo(g);
                break;
            case 7:
            case 17:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 8:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 10:
                if (charMenu != null)
                {
                    SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                    paintCharInfo(g, charMenu);
                }
                break;
            case 14:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMapInfo(g);
                break;
            case 18:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 19:
            case 26:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 20:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 22:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 5:
            case 6:
                break;
        }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTopInfo inner: " + ex.ToString());
        }
    }


    // ==================== doFireTop ====================
    private void doFireTop()
    {
        if (selected >= -1)
        {
            if (isThachDau)
            {
                Service.gI().sendTop(topName, (sbyte)selected);
                return;
            }
            MyVector myVector = new(string.Empty);
            myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)vTop.elementAt(selected)));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addThachDauDetail((TopInfo)vTop.elementAt(selected));
        }
    }


}
