using System;
using Mod;

public partial class Panel
{
    // ==================== setTypePetMain ====================
    public void setTypePetMain()
    {
        type = 21;
        if (GameCanvas.panel2 != null)
        {
            boxPet = mResources.petMainTab2;
        }
        else
        {
            boxPet = mResources.petMainTab;
        }
        tabName[21] = boxPet;
        if (Char.myCharz().cgender == 1)
        {
            strStatus = new string[6]
            {
                mResources.follow,
                mResources.defend,
                mResources.attack,
                mResources.gohome,
                mResources.fusion,
                mResources.fusionForever
            };
        }
        else
        {
            strStatus = new string[5]
            {
                mResources.follow,
                mResources.defend,
                mResources.attack,
                mResources.gohome,
                mResources.fusion
            };
        }
        setType(2);
        if (currentTabIndex == 0)
        {
            setTabPetInventory(false);
        }
        else if (currentTabIndex == 1)
        {
            setTabPetSkill(false);
        }
        else if (currentTabIndex == 2)
        {
            setTabPetStatus();
        }
        else if (currentTabIndex == 3)
        {
            setTabInventory(resetSelect: true);
        }
    }


    // ==================== setTypePet2Main ====================
    public void setTypePet2Main()
    {
        type = 28;
        if (GameCanvas.panel2 != null)
        {
            boxPet = mResources.petMainTab2;
        }
        else
        {
            boxPet = mResources.petMainTab;
        }
        tabName[28] = boxPet;
        if (Char.myCharz().cgender == 1)
        {
            strStatus = new string[6]
            {
                mResources.follow,
                mResources.defend,
                mResources.attack,
                mResources.gohome,
                mResources.fusion,
                mResources.fusionForever
            };
        }
        else
        {
            strStatus = new string[5]
            {
                mResources.follow,
                mResources.defend,
                mResources.attack,
                mResources.gohome,
                mResources.fusion
            };
        }
        setType(2);
        if (currentTabIndex == 0)
        {
            setTabPetInventory(true);
        }
        else if (currentTabIndex == 1)
        {
            setTabPetSkill(true);
        }
        else if (currentTabIndex == 2)
        {
            setTabPetStatus();
        }
        else if (currentTabIndex == 3)
        {
            setTabInventory(resetSelect: true);
        }
    }


    // ==================== updateKeyPetStatus ====================
    private void updateKeyPetStatus()
    {
        updateKeyScrollView();
    }


    // ==================== updateKeyPetSkill ====================
    private void updateKeyPetSkill()
    {
    }


    // ==================== setTabPetStatus ====================
    private void setTabPetStatus()
    {
        currentListLength = strStatus.Length;
        ITEM_HEIGHT = 24;
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


    // ==================== setTabPetSkill ====================
    private void setTabPetSkill(bool isPet2)
    {
        ITEM_HEIGHT = 30;
        currentListLength = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrPetSkill.Length + 5;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = cmtoY = cmyLast[currentTabIndex];
        if (cmy < 0)
        {
            cmy = cmtoY = 0;
        }
        if (cmy > cmyLim)
        {
            cmy = cmyLim;
        }
        selected = GameCanvas.isTouch ? (-1) : 0;
    }


    // ==================== setTabPetInventory ====================
    private void setTabPetInventory(bool isPet2)
    {
        ITEM_HEIGHT = 29;
        Item[] arrItemBody = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrItemBody;
        currentListLength = arrItemBody.Length;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
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
        selected = (GameCanvas.isTouch ? (-1) : 0);
    }


    // ==================== paintPetStatus ====================
    private void paintPetStatus(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strStatus.Length; i++)
        {
            int x = xScroll;
            int num = yScroll + i * ITEM_HEIGHT;
            int num2 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num - cmy <= yScroll + hScroll && num - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(x, num, num2, h);
                mFont.tahoma_7b_dark.drawString(g, strStatus[i], xScroll + wScroll / 2, num + 6, mFont.CENTER);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintPetSkill ====================
    private void paintPetSkill(mGraphics g, bool isPet2)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
        int num = 5 + pet.arrPetSkill.Length;
        for (int i = 0; i < num; i++)
        {
            int num2 = xScroll + 30;
            int num3 = yScroll + i * ITEM_HEIGHT;
            int num4 = wScroll - 30;
            int h = ITEM_HEIGHT - 1;
            int num5 = xScroll;
            int num6 = yScroll + i * ITEM_HEIGHT;
            _ = ITEM_HEIGHT - 1;
            if (num3 - cmy > yScroll + hScroll || num3 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            if (i >= 5)
            {
                g.setColor((i != selected) ? 16765060 : 16776068);
            }
            g.fillRect(num2, num3, num4, h);
            g.drawImage(GameScr.imgSkill, num5, num6, 0);
            if (i == 0)
            {
                SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
                string st = mResources.HP + " " + mResources.root + ": " + NinjaUtil.getMoneys(pet.cHPGoc);
                mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(pet.cHPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + pet.hpFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 1)
            {
                SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
                string st2 = mResources.KI + " " + mResources.root + ": " + NinjaUtil.getMoneys(pet.cMPGoc);
                mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(pet.cMPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + pet.mpFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 2)
            {
                SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
                string st3 = mResources.hit_point + " " + mResources.root + ": " + NinjaUtil.getMoneys(pet.cDamGoc);
                mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(pet.cDamGoc * 100) + " " + mResources.potential + ": " + mResources.increase + " " + pet.damFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 3)
            {
                SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
                string st4 = mResources.armor + " " + mResources.root + ": " + NinjaUtil.getMoneys(pet.cDefGoc);
                mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(500000 + pet.cDefGoc * 100000) + " " + mResources.potential + ": " + mResources.increase + " " + pet.defFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 4)
            {
                SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
                string st5 = mResources.critical + " " + mResources.root + ": " + pet.cCriticalGoc + "%";
                int num10 = pet.cCriticalGoc;
                if (num10 > t_tiemnang.Length - 1)
                {
                    num10 = t_tiemnang.Length - 1;
                }
                long num9 = t_tiemnang[num10];
                mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
                long number = num9;
                mFont.tahoma_7_green2.drawString(g, Res.formatNumber2(number) + " " + mResources.potential + ": " + mResources.increase + " " + pet.criticalFrom1000Tiemnang, num2 + 5, num3 + 15, 0);
            }
            if (i < 5)
            {
                continue;
            }
            Skill skill = pet.arrPetSkill[i - 5];
            g.drawImage(GameScr.imgSkill2, num5, num6, 0);
            if (skill.template != null)
            {
                mFont.tahoma_7_blue.drawString(g, skill.template.name, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, mResources.level + ": " + skill.point, num2 + 5, num3 + 15, 0);
                SmallImage.drawSmallImage(g, skill.template.iconId, num5 + 4, num6 + 4, 0, 0);
            }
            else
            {
                mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, mResources.level + ": " + 0, num2 + 5, num3 + 15, 0);
                SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num5 + 8, num6 + 7, 0, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintPetInventory ====================
    private void paintPetInventory(mGraphics g, bool isPet2)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        Item[] arrItemBody = (isPet2 ? Char.MyPet2z() : Char.myPetz()).arrItemBody;
        for (int i = 0; i < arrItemBody.Length; i++)
        {
            int num = i;
            _ = i - arrItemBody.Length;
            int num3 = xScroll + 29;
            int num4 = yScroll + i * ITEM_HEIGHT;
            int num5 = wScroll - 29;
            int h = ITEM_HEIGHT - 1;
            int num6 = xScroll;
            int num7 = yScroll + i * ITEM_HEIGHT;
            int num8 = ITEM_HEIGHT - 1;
            int num9 = ITEM_HEIGHT - 1;
            if (num4 - cmy > yScroll + hScroll || num4 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            Item item = arrItemBody[num];
            g.setColor((i == selected) ? 16383818 : 15196114);
            g.fillRect(num3, num4, num5, h);
            g.setColor((i == selected) ? 9541120 : 9993045);
            if (item != null)
            {
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
                    {
                        sbyte color_Item_Upgrade = GetColor_Item_Upgrade(item.itemOption[j].param);
                        int color_ItemBg = GetColor_ItemBg(color_Item_Upgrade);
                        if (color_ItemBg != -1)
                        {
                            g.setColor((i != selected) ? GetColor_ItemBg(color_Item_Upgrade) : GetColor_ItemBg(color_Item_Upgrade));
                        }
                    }
                }
            }
            //g.fillRect(num6, num7, num8, num9);
            g.setColor(6047789, 0.5f);
            g.fillRect(num6, num7, num8, num9); paintEffectItem(g, item, num6, num7);
            if (item != null && item.isSelect && GameCanvas.panel.type == 12)
            {
                g.setColor((i != selected) ? 6047789 : 7040779);
                g.fillRect(num6, num7, num8, num9);
            }
            if (item != null)
            {
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
                mFont2.drawString(g, "[" + item.template.id + "] " + item.template.name + text, num3 + 5, num4 + 1, 0);
                string text2 = string.Empty;
                if (item.itemOption != null)
                {
                    if (item.itemOption.Length > 0 && item.itemOption[0] != null      
                        && item.itemOption[0].optionTemplate.id != 72 
                        && item.itemOption[0].optionTemplate.id != 225 
                        && item.itemOption[0].optionTemplate.id != 206 
                        && item.itemOption[0].optionTemplate.id != 34 
                        && item.itemOption[0].optionTemplate.id != 35 
                        && item.itemOption[0].optionTemplate.id != 36 
                        && item.itemOption[0].optionTemplate.id != 102 
                        &&item.itemOption[0].optionTemplate.id != 107)
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
                        for (int l = 1; l < Math.min(item.itemOption.Length,3); l++)
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
                    mFont3.drawString(g, text2, num3 + 5, num4 + 10, mFont.LEFT);
                }
                SmallImage.drawSmallImage(g, item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
                if (item.itemOption != null)
                {
                    for (int m = 0; m < item.itemOption.Length; m++)
                    {
                        paintOptItemInventory(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num6, num7, num8, num9, item);
                    }
                    for (int n = 0; n < item.itemOption.Length; n++)
                    {
                        paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num6, num7, num8, num9);
                    }
                }
                if (item.quantity > 1)
                {
                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
                }
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintPetInfo ====================
    private void paintPetInfo(mGraphics g, bool isPet2)
    {
        Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
        mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(pet.cPower), X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
        if (pet.cPower > 0)
        {
            mFont.tahoma_7_yellow.drawString(g, (!pet.me) ? pet.currStrLevel : pet.getStrLevel(), X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
        }
        if (pet.cDamFull > 0)
        {
            mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + pet.cDamFull, X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
        }
        if (pet.cMaxStamina > 0)
        {
            mFont.tahoma_7_yellow.drawString(g, mResources.vitality, X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
            g.drawImage(GameScr.imgMPLost, X + 100, 41, 0);
            int num = pet.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / pet.cMaxStamina;
            g.setClip(100, X + 41, num, 20);
            g.drawImage(GameScr.imgMP, X + 100, 41, 0);
        }
        g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
    }


    // ==================== paintPetSkillInfo ====================
    private void paintPetSkillInfo(mGraphics g, bool isPet2)
    {
        Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
        mFont.tahoma_7b_white.drawString(g, "HP: " + pet.cHP + "/" + pet.cHPFull, X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7b_white.drawString(g, "MP: " + pet.cMP + "/" + pet.cMPFull, X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7_yellow.drawString(g, mResources.critical + ": " + pet.cCriticalFull + "   " + mResources.armor + ": " + pet.cDefull, X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.potential2 + ": " + NinjaUtil.getMoneys(pet.cTiemNang), X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== paintPetStatusInfo ====================
    private void paintPetStatusInfo(mGraphics g, bool isPet2)
    {
        Char pet = isPet2 ? Char.MyPet2z() : Char.myPetz();
        mFont.tahoma_7b_white.drawString(g, "HP: " + formatLargeNumber(pet.cHP) + "/" + formatLargeNumber(pet.cHPFull), X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7b_white.drawString(g, "MP: " + formatLargeNumber(pet.cMP) + "/" + formatLargeNumber(pet.cMPFull), X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7_yellow.drawString(g, mResources.critical + ": " + formatLargeNumber(pet.cCriticalFull) + "   " + mResources.armor + ": " + formatLargeNumber(pet.cDefull), X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.status + ": " + strStatus[pet.petStatus], X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== DoFirePet2Main ====================
    private void DoFirePet2Main()
    {
        if (currentTabIndex == 0)
        {
            if (selected == -1 || selected > Char.MyPet2z().arrItemBody.Length - 1)
            {
                return;
            }
            MyVector myVector = new(string.Empty);
            Item item = Char.MyPet2z().arrItemBody[selected];
            currItem = item;
            if (currItem != null)
            {
                myVector.addElement(new Command(mResources.MOVEOUT, this, 2008, currItem));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addItemDetail(currItem);
            }
            else
            {
                cp = null;
            }
        }
        else if (currentTabIndex == 1)
        {
            //DoFirePetSkill();
        }
        else if (currentTabIndex == 2)
        {
            doFirePetStatus();
        }
        else if (currentTabIndex == 3)
        {
            doFireInventory();
        }
    }


    // ==================== doFirePetMain ====================
    private void doFirePetMain()
    {
        if (currentTabIndex == 0)
        {
            if (selected == -1 || selected > Char.myPetz().arrItemBody.Length - 1)
            {
                return;
            }
            MyVector myVector = new MyVector(string.Empty);
            Item item = Char.myPetz().arrItemBody[selected];
            currItem = item;
            if (currItem != null)
            {
                myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, currItem));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addItemDetail(currItem);
            }
            else
            {
                cp = null;
            }
        }
        else if (currentTabIndex == 1)
        {
            DoFirePetSkill();
        }
        else if (currentTabIndex == 2)
        {
            doFirePetStatus();
        }
        else if (currentTabIndex == 3)
        {
            doFireInventory();
        }
    }


    // ==================== doFirePetStatus ====================
    private void doFirePetStatus()
    {
        if (selected == -1)
        {
            return;
        }
        if (selected == 5)
        {
            GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, type == 28 ? 888352 : 888351), new Command(mResources.NO, 2001));
            return;
        }
        if (type == 28)
        {
            Service.gI().pet2Status((sbyte)selected);
            if (selected < 4)
            {
                Char.MyPet2z().petStatus = (sbyte)selected;
            }
        }
        else
        {
            Service.gI().petStatus((sbyte)selected);
            if (selected < 4)
            {
                Char.myPetz().petStatus = (sbyte)selected;
            }
        }
    }


    // ==================== doFirePet ====================
    private void doFirePet()
    {
        InfoDlg.showWait();
        Service.gI().petInfo();
        timeShow = 20;
        ModFunc.userOpenPet = true;
    }


    // ==================== doFirePet2 ====================
    private void doFirePet2()
    {
        InfoDlg.showWait();
        Service.gI().PetInfo2();
        timeShow = 20;
    }


    // ==================== DoFirePetSkill ====================
    private void DoFirePetSkill()
    {
        if (selected < 0)
        {
            return;
        }
        if (selected == 0 || selected == 1 || selected == 2 || selected == 3 || selected == 4)
        {
            long cTiemNang = Char.myPetz().cTiemNang;
            long cHPGoc = Char.myPetz().cHPGoc;
            long cMPGoc = Char.myPetz().cMPGoc;
            long cDamGoc = Char.myPetz().cDamGoc;
            int cDefGoc = Char.myPetz().cDefGoc;
            int cCriticalGoc = Char.myPetz().cCriticalGoc;

            int num2 = 1000;
            if (selected == 0)
            {
                if (cTiemNang < cHPGoc + num2)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang + mResources.not_enough_potential_point2 + (cHPGoc + num2), isError: false);
                    return;
                }
                if (cTiemNang > cHPGoc && cTiemNang < 10 * (2 * (cHPGoc + num2) + 180) / 2)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + (cHPGoc + num2) + mResources.use_potential_point_for2 + Char.myCharz().hpFrom1000TiemNang + mResources.for_HP, new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * (cHPGoc + num2) + 180) / 2 && cTiemNang < 100 * (2 * (cHPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, true));
                }
                else if (cTiemNang >= 100 * (2 * (cHPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(100 * (2 * (cHPGoc + num2) + 1980) / 2), this, 9007, true));

                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + true));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 1)
            {
                if (cTiemNang < cMPGoc + num2)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang + mResources.not_enough_potential_point2 + (cMPGoc + num2), isError: false);
                    return;
                }
                if (cTiemNang > cMPGoc && cTiemNang < 10 * (2 * (cMPGoc + num2) + 180) / 2)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + (cMPGoc + num2) + mResources.use_potential_point_for2 + Char.myCharz().mpFrom1000TiemNang + mResources.for_KI, new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * (cMPGoc + num2) + 180) / 2 && cTiemNang < 100 * (2 * (cMPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, true));
                }
                else if (cTiemNang >= 100 * (2 * (cMPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(cMPGoc + num2), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(10 * (2 * (cMPGoc + num2) + 180) / 2), this, 9006, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(100 * (2 * (cMPGoc + num2) + 1980) / 2), this, 9007, true));
                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + true));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 2)
            {
                if (cTiemNang < cDamGoc * Char.myCharz().expForOneAdd)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + cTiemNang + mResources.not_enough_potential_point2 + cDamGoc * 100, isError: false);
                    return;
                }
                if (cTiemNang > cDamGoc && cTiemNang < 10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + cDamGoc * 100 + mResources.use_potential_point_for2 + Char.myCharz().damFrom1000TiemNang + mResources.for_hit_point, new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd && cTiemNang < 100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(cDamGoc * 100), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd), this, 9006, true));
                }
                else if (cTiemNang >= 100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(cDamGoc * 100), this, 9000, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd), this, 9006, true));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd), this, 9007, true));
                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + true));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 3)
            {
                if (cTiemNang < 50000 + cDefGoc * 1000)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys(50000 + cDefGoc * 1000), isError: false);
                    return;
                }
                long number = 2 * (cDefGoc + 5) / 2L * 100000;
                long number2 = 10L * (2 * (cDefGoc + 5) + 9) / 2 * 100000;
                long number3 = 100L * (2 * (cDefGoc + 5) + 99) / 2 * 100000;
                MyVector myVector = new(string.Empty);
                myVector.addElement(new Command(mResources.increase_upper + "\n1 " + mResources.armor + "\n" + Res.formatNumber2(number), this, 9000, true));
                myVector.addElement(new Command(mResources.increase_upper + "\n10 " + mResources.armor + "\n" + Res.formatNumber2(number2), this, 9006, true));
                myVector.addElement(new Command(mResources.increase_upper + "\n100 " + mResources.armor + "\n" + Res.formatNumber2(number3), this, 9007, true));
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + true));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            else if (selected == 4)
            {
                int crit = cCriticalGoc;
                if (crit > t_tiemnang.Length - 1)
                {
                    crit = t_tiemnang.Length - 1;
                }
                long num3 = t_tiemnang[crit];
                if (cTiemNang < num3)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num3), isError: false);
                    return;
                }
                GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + Res.formatNumber(num3) + mResources.use_potential_point_for2 + Char.myCharz().criticalFrom1000Tiemnang + mResources.for_crit, new Command(mResources.increase_upper, this, 9000, true), new Command(mResources.CANCEL, this, 4007, null));
            }
            return;
        }

        //int index = selected - 6;
        //SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[index];
        //Skill skill = Char.myCharz().getSkill(skillTemplate);
        //Skill skill2 = null;
        //MyVector myVector8 = new(string.Empty);
        //if (skill != null)
        //{
        //    if (skill.point == skillTemplate.maxPoint)
        //    {
        //        myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
        //        myVector8.addElement(new Command(mResources.CLOSE, 2));
        //    }
        //    else
        //    {
        //        skill2 = skillTemplate.skills[skill.point];
        //        myVector8.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
        //        myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
        //    }
        //}
        //else
        //{
        //    skill2 = skillTemplate.skills[0];
        //    myVector8.addElement(new Command(mResources.learn, this, 9004, skill2));
        //}
        //GameCanvas.menu.startAt(myVector8, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
        //addSkillDetail(skillTemplate, skill, skill2);
    }


    // ==================== addSkillDetail2 ====================
    private void addSkillDetail2(int type, bool isPet)
    {
        string empty = string.Empty;
        long num = 0;
        Char @char = isPet ? Char.myPetz() : Char.myCharz();
        if (selected == 0)
        {
            num = @char.cHPGoc + 1000;
        }
        if (selected == 1)
        {
            num = @char.cMPGoc + 1000;
        }
        if (selected == 2)
        {
            num = @char.cDamGoc * @char.expForOneAdd;
        }
        if (selected == 3)
        {
            num = 500000 + @char.cDefGoc * 100000;
        }
        string text = empty;
        empty = text + "|5|2|" + mResources.USE + " " + num + " " + mResources.potential;
        if (type == 0)
        {
            empty = empty + "\n|5|2|" + mResources.to_gain_20hp;
        }
        if (type == 1)
        {
            empty = empty + "\n|5|2|" + mResources.to_gain_20mp;
        }
        if (type == 2)
        {
            empty = empty + "\n|5|2|" + mResources.to_gain_1pow;
        }
        if (type == 3)
        {
            empty = empty + "\n|5|2|" + mResources.to_gain_1pow;
        }
        currItem = null;
        partID = null;
        charInfo = null;
        idIcon = -1;
        cp = new ChatPopup();
        popUpDetailInit(cp, empty);
    }


}
