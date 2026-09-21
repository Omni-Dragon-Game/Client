using System;
using Assets.src.g;
using Mod;

/// <summary>
/// Quản lý toàn bộ tính năng Luyện tập / Nâng cấp / Kết hợp trang bị (Bà Hạt Mít).
/// Bao gồm vẽ giao diện combine, hiệu ứng xoay pha lê, đối thoại NPC và xử lý phím.
/// Tách ra từ Panel.cs để file chính tinh gọn theo chuẩn Clean Code (< 1.000 dòng).
/// </summary>
public partial class Panel
{
    public void setTypeCombine()
    {
        type = 12;
        if (GameCanvas.w > 2 * WIDTH_PANEL)
        {
            boxCombine = new string[1][] { mResources.combine };
        }
        else
        {
            boxCombine = new string[2][]
            {
                mResources.combine,
                mResources.inventory
            };
        }
        tabName[type] = boxCombine;
        setType(0);
        if (currentTabIndex == 0)
        {
            setTabCombine();
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
        combineSuccess = -1;
        isDoneCombine = true;
    }

    public void setTabCombine()
    {
        currentListLength = vItemCombine.size() + 1;
        ITEM_HEIGHT = 29;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 9;
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

    private void updateKeyCombine()
    {
        if (currentTabIndex == 0)
        {
            updateKeyScrollView();
            keyTouchCombine = -1;
            if (selected == vItemCombine.size() && GameCanvas.isPointerClick)
            {
                GameCanvas.isPointerClick = false;
                keyTouchCombine = 1;
            }
        }
        if (currentTabIndex == 1)
        {
            updateKeyScrollView();
        }
    }

    private void paintCombine(mGraphics g)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        if (vItemCombine.size() == 0)
        {
            if (combineInfo != null)
            {
                for (int i = 0; i < combineInfo.Length; i++)
                {
                    mFont.tahoma_7b_dark.drawString(g, combineInfo[i], xScroll + wScroll / 2, yScroll + hScroll / 2 - combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
                }
            }
            return;
        }
        for (int j = 0; j < vItemCombine.size() + 1; j++)
        {
            int num = xScroll + 29;
            int num2 = yScroll + j * ITEM_HEIGHT;
            int num3 = wScroll - 29;
            int num4 = ITEM_HEIGHT - 1;
            int num5 = xScroll;
            int num6 = yScroll + j * ITEM_HEIGHT;
            int num7 = ITEM_HEIGHT - 1;
            int num8 = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            if (j == vItemCombine.size())
            {
                if (vItemCombine.size() > 0)
                {
                    if (!GameCanvas.isTouch && j == selected)
                    {
                        g.setColor(16383818);
                        g.fillRect(num5, num2, wScroll, num4 + 2);
                    }
                    if ((j == selected && keyTouchCombine == 1) || (!GameCanvas.isTouch && j == selected))
                    {
                        g.drawImage(GameScr.imgLbtnFocus, xScroll + wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
                        mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                    }
                    else
                    {
                        g.drawImage(GameScr.imgLbtn, xScroll + wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
                        mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, xScroll + wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
                    }
                }
                continue;
            }
            g.setColor((j != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, num4);
            g.setColor((j != selected) ? 9993045 : 9541120);
            Item item = (Item)vItemCombine.elementAt(j);
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
                            g.setColor((j != selected) ? GetColor_ItemBg(color_Item_Upgrade) : GetColor_ItemBg(color_Item_Upgrade));
                        }
                    }
                }
            }
            g.fillRect(num5, num6, num7, num8); paintEffectItem(g, item, num5, num6);
            if (item == null)
            {
                continue;
            }
            string text = string.Empty;
            mFont mFont2 = mFont.tahoma_7b_dark;
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
                }
                for (int l = 0; l < item.itemOption.Length; l++)
                {
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
                    if (item.itemOption[l].optionTemplate.id == 72)
                    {
                        if (item.itemOption[l].param >= 1 && item.itemOption[l].param <= 5)
                        {
                            mFont2 = GetFont(2);
                        }
                        else if (item.itemOption[l].param >= 6 && item.itemOption[l].param <= 7)
                        {
                            mFont2 = GetFont(8);
                        }
                        else if (item.itemOption[l].param >= 8 && item.itemOption[l].param <= 10)
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
                if (item.itemOption.Length > 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
                {
                    text2 += item.itemOption[0].getOptionString();
                }
                mFont mFont3 = mFont.tahoma_7_blue;
                if (item.itemOption.Length > 1)
                {
                    for (int m = 1; m < 2; m++)
                    {
                        if (item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107)
                        {
                            text2 = text2 + "," + item.itemOption[m].getOptionString();
                        }
                    }
                }
                mFont3.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
            }
            SmallImage.drawSmallImage(g, item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
            if (item.itemOption != null)
            {
                for (int n = 0; n < item.itemOption.Length; n++)
                {
                    paintOptItemInventory(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8, item);
                }
                for (int num9 = 0; num9 < item.itemOption.Length; num9++)
                {
                    paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
                }
            }
            if (item.quantity > 1)
            {
                mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
            }
        }
        paintScrollArrow(g);
    }

    private void paintCombineInfo(mGraphics g)
    {
        if (combineTopInfo != null)
        {
            for (int i = 0; i < combineTopInfo.Length; i++)
            {
                mFont.tahoma_7_white.drawString(g, combineTopInfo[i], X + 45 + (W - 50) / 2, 5 + i * 14, mFont.CENTER);
            }
        }
    }

    public void cleanCombine()
    {
        for (int i = 0; i < vItemCombine.size(); i++)
        {
            ((Item)vItemCombine.elementAt(i)).isSelect = false;
        }
        vItemCombine.removeAllElements();
    }

    private void doFireCombine()
    {
        if (currentTabIndex == 0)
        {
            if (selected == -1 || vItemCombine.size() == 0)
            {
                return;
            }
            if (selected == vItemCombine.size())
            {
                keyTouchCombine = -1;
                selected = (GameCanvas.isTouch ? (-1) : 0);
                InfoDlg.showWait();
                Service.gI().combine(1, vItemCombine);
                return;
            }
            if (selected > vItemCombine.size() - 1)
            {
                return;
            }
            currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(selected);
            MyVector myVector = new MyVector();
            myVector.addElement(new Command(mResources.GETOUT, this, 6001, currItem));
            if (ModFunc.GI().isAutoPhaLe)
            {
                myVector.addElement(new Command("Nhập số sao", this, 8010, this.currItem));
            }
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
            doFireInventory();
        }
    }

    public void setCombineEff(int type)
    {
        typeCombine = type;
        rS = 90;
        if (typeCombine == 0)
        {
            iDotS = 5;
            angleS = (angleO = 90);
            time = 2;
            for (int i = 0; i < vItemCombine.size(); i++)
            {
                Item item = (Item)vItemCombine.elementAt(i);
                if (item != null)
                {
                    if (item.template.type == 14)
                    {
                        iconID2 = item.template.iconID;
                    }
                    else
                    {
                        iconID1 = item.template.iconID;
                    }
                }
            }
        }
        else if (typeCombine == 1)
        {
            iDotS = 2;
            angleS = (angleO = 0);
            time = 1;
            for (int j = 0; j < vItemCombine.size(); j++)
            {
                Item item2 = (Item)vItemCombine.elementAt(j);
                if (item2 != null)
                {
                    if (j == 0)
                    {
                        iconID1 = item2.template.iconID;
                    }
                    else
                    {
                        iconID2 = item2.template.iconID;
                    }
                }
            }
        }
        else if (typeCombine == 2)
        {
            iDotS = 7;
            angleS = (angleO = 25);
            time = 1;
            for (int k = 0; k < vItemCombine.size(); k++)
            {
                Item item3 = (Item)vItemCombine.elementAt(k);
                if (item3 != null)
                {
                    iconID1 = item3.template.iconID;
                }
            }
        }
        else if (typeCombine == 3)
        {
            xS = GameCanvas.hw;
            yS = GameCanvas.hh;
            iDotS = 1;
            angleS = (angleO = 1);
            time = 4;
            for (int l = 0; l < vItemCombine.size(); l++)
            {
                Item item4 = (Item)vItemCombine.elementAt(l);
                if (item4 != null)
                {
                    iconID1 = item4.template.iconID;
                }
            }
        }
        else if (typeCombine == 4)
        {
            iDotS = vItemCombine.size();
            iconID = new short[iDotS];
            angleS = (angleO = 25);
            time = 1;
            for (int m = 0; m < vItemCombine.size(); m++)
            {
                Item item5 = (Item)vItemCombine.elementAt(m);
                if (item5 != null)
                {
                    iconID[m] = item5.template.iconID;
                }
            }
        }
        speed = 1;
        isSpeedCombine = true;
        isDoneCombine = false;
        isCompleteEffCombine = false;
        iAngleS = 360 / iDotS;
        xArgS = new int[iDotS];
        yArgS = new int[iDotS];
        xDotS = new int[iDotS];
        yDotS = new int[iDotS];
        setDotStar();
        isPaintCombine = true;
        countUpdate = 10;
        countR = 30;
        countWait = 10;
        addTextCombineNPC(idNPC, mResources.combineSpell);
    }

    private void updateCombineEff()
    {
        countUpdate--;
        if (countUpdate < 0)
        {
            countUpdate = 0;
        }
        countR--;
        if (countR < 0)
        {
            countR = 0;
        }
        if (countUpdate != 0)
        {
            return;
        }
        if (!isCompleteEffCombine)
        {
            if (time > 0)
            {
                if (combineSuccess != -1)
                {
                    if (typeCombine == 3)
                    {
                        if (GameCanvas.gameTick % 10 == 0)
                        {
                            Effect me = new Effect(21, xS - 10, yS + 25, 4, 1, 1);
                            EffecMn.addEff(me);
                            time--;
                        }
                    }
                    else
                    {
                        if (GameCanvas.gameTick % 2 == 0)
                        {
                            if (isSpeedCombine)
                            {
                                if (speed < 40)
                                {
                                    speed += 2;
                                }
                            }
                            else if (speed > 10)
                            {
                                speed -= 2;
                            }
                        }
                        if (countR == 0)
                        {
                            if (isSpeedCombine)
                            {
                                if (rS > 0)
                                {
                                    rS -= 5;
                                }
                                else if (GameCanvas.gameTick % 10 == 0)
                                {
                                    isSpeedCombine = false;
                                    time--;
                                    countR = 5;
                                    countWait = 10;
                                }
                            }
                            else if (rS < 90)
                            {
                                rS += 5;
                            }
                            else if (GameCanvas.gameTick % 10 == 0)
                            {
                                isSpeedCombine = true;
                                countR = 10;
                            }
                        }
                        angleS = angleO;
                        angleS -= speed;
                        if (angleS >= 360)
                        {
                            angleS -= 360;
                        }
                        if (angleS < 0)
                        {
                            angleS = 360 + angleS;
                        }
                        angleO = angleS;
                        setDotStar();
                    }
                }
            }
            else if (GameCanvas.gameTick % 20 == 0)
            {
                isCompleteEffCombine = true;
            }
            if (GameCanvas.gameTick % 20 == 0)
            {
                if (typeCombine != 3)
                {
                    EffectPanel.addServerEffect(132, xS, yS, 2);
                }
                EffectPanel.addServerEffect(114, xS, yS + 20, 2);
            }
        }
        else
        {
            if (!isCompleteEffCombine)
            {
                return;
            }
            if (combineSuccess == 1)
            {
                if (countWait == 10)
                {
                    Effect me2 = new Effect(22, xS - 3, yS + 25, 4, 1, 1);
                    EffecMn.addEff(me2);
                }
                countWait--;
                if (countWait < 0)
                {
                    countWait = 0;
                }
                if (rS < 300)
                {
                    rS = Res.abs(rS + 10);
                    if (rS == 20)
                    {
                        addTextCombineNPC(idNPC, mResources.combineFail);
                    }
                }
                else if (GameCanvas.gameTick % 20 == 0)
                {
                    if (GameCanvas.w > 2 * WIDTH_PANEL)
                    {
                        GameCanvas.panel2 = new Panel();
                        GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
                        GameCanvas.panel2.setTypeBodyOnly();
                        GameCanvas.panel2.show();
                    }
                    combineSuccess = -1;
                    isDoneCombine = true;
                    if (typeCombine == 4)
                    {
                        GameCanvas.panel.hideNow();
                    }
                }
                setDotStar();
            }
            else
            {
                if (combineSuccess != 0)
                {
                    return;
                }
                if (countWait == 10)
                {
                    if (typeCombine == 2)
                    {
                        Effect me3 = new Effect(20, xS - 3, yS + 15, 4, 2, 1);
                        EffecMn.addEff(me3);
                    }
                    else
                    {
                        Effect me4 = new Effect(21, xS - 10, yS + 25, 4, 1, 1);
                        EffecMn.addEff(me4);
                    }
                    addTextCombineNPC(idNPC, mResources.combineSuccess);
                    isPaintCombine = false;
                }
                if (isPaintCombine)
                {
                    return;
                }
                countWait--;
                if (countWait < -50)
                {
                    countWait = -50;
                    if (typeCombine < 3 && GameCanvas.w > 2 * WIDTH_PANEL)
                    {
                        GameCanvas.panel2 = new Panel();
                        GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
                        GameCanvas.panel2.setTypeBodyOnly();
                        GameCanvas.panel2.show();
                    }
                    combineSuccess = -1;
                    isDoneCombine = true;
                    if (typeCombine == 4)
                    {
                        GameCanvas.panel.hideNow();
                    }
                }
            }
        }
    }

    public void paintCombineEff(mGraphics g)
    {
        GameScr.gI().paintBlackSky(g);
        paintCombineNPC(g);
        if (GameCanvas.gameTick % 4 == 0)
        {
            g.drawImage(ItemMap.imageFlare, xS, yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
        }
        if (typeCombine == 0)
        {
            for (int i = 0; i < yArgS.Length; i++)
            {
                SmallImage.drawSmallImage(g, iconID1, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                if (isPaintCombine)
                {
                    SmallImage.drawSmallImage(g, iconID2, xDotS[i], yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
                }
            }
        }
        else if (typeCombine == 1)
        {
            if (!isPaintCombine)
            {
                SmallImage.drawSmallImage(g, iconID3, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                return;
            }
            for (int j = 0; j < yArgS.Length; j++)
            {
                SmallImage.drawSmallImage(g, iconID1, xDotS[0], yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
                SmallImage.drawSmallImage(g, iconID2, xDotS[1], yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
            }
        }
        else if (typeCombine == 2)
        {
            if (!isPaintCombine)
            {
                SmallImage.drawSmallImage(g, iconID3, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                return;
            }
            for (int k = 0; k < yArgS.Length; k++)
            {
                SmallImage.drawSmallImage(g, iconID1, xDotS[k], yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
            }
        }
        else if (typeCombine == 3)
        {
            if (!isPaintCombine)
            {
                SmallImage.drawSmallImage(g, iconID3, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
            }
            else
            {
                SmallImage.drawSmallImage(g, iconID1, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
            }
        }
        else
        {
            if (typeCombine != 4)
            {
                return;
            }
            if (!isPaintCombine)
            {
                if (iconID3 != -1)
                {
                    SmallImage.drawSmallImage(g, iconID3, xS, yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                }
            }
            else
            {
                for (int l = 0; l < iconID.Length; l++)
                {
                    SmallImage.drawSmallImage(g, iconID[l], xDotS[l], yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
                }
            }
        }
    }

    private void setDotStar()
    {
        for (int i = 0; i < yArgS.Length; i++)
        {
            if (angleS >= 360)
            {
                angleS -= 360;
            }
            if (angleS < 0)
            {
                angleS = 360 + angleS;
            }
            yArgS[i] = Res.abs(rS * Res.sin(angleS) / 1024);
            xArgS[i] = Res.abs(rS * Res.cos(angleS) / 1024);
            if (angleS < 90)
            {
                xDotS[i] = xS + xArgS[i];
                yDotS[i] = yS - yArgS[i];
            }
            else if (angleS >= 90 && angleS < 180)
            {
                xDotS[i] = xS - xArgS[i];
                yDotS[i] = yS - yArgS[i];
            }
            else if (angleS >= 180 && angleS < 270)
            {
                xDotS[i] = xS - xArgS[i];
                yDotS[i] = yS + yArgS[i];
            }
            else
            {
                xDotS[i] = xS + xArgS[i];
                yDotS[i] = yS + yArgS[i];
            }
            angleS -= iAngleS;
        }
    }

    public void paintCombineNPC(mGraphics g)
    {
        g.translate(-GameScr.cmx, -GameScr.cmy);
        if (typeCombine < 3)
        {
            for (int i = 0; i < GameScr.vNpc.size(); i++)
            {
                Npc npc = (Npc)GameScr.vNpc.elementAt(i);
                if (npc.template.npcTemplateId == idNPC)
                {
                    npc.paint(g);
                    if (npc.chatInfo != null)
                    {
                        npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
                    }
                }
            }
        }
        GameCanvas.resetTrans(g);
        if (GameCanvas.gameTick % 4 == 0)
        {
            g.drawImage(ItemMap.imageFlare, xS - 5, yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
            g.drawImage(ItemMap.imageFlare, xS + 5, yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
            g.drawImage(ItemMap.imageFlare, xS, yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
        }
        for (int j = 0; j < Effect2.vEffect3.size(); j++)
        {
            Effect2 effect = (Effect2)Effect2.vEffect3.elementAt(j);
            effect.paint(g);
        }
    }

    public void addTextCombineNPC(int idNPC, string text)
    {
        if (typeCombine >= 3)
        {
            return;
        }
        for (int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
            if (npc.template.npcTemplateId == idNPC)
            {
                npc.addInfo(text);
            }
        }
    }
}
