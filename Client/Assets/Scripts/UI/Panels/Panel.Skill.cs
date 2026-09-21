using System;
using Mod;

public partial class Panel
{
    // ==================== setTypeSpeacialSkill ====================
    public void setTypeSpeacialSkill()
    {
        type = 25;
        setType(0);
        setTabSpeacialSkill();
        currentTabIndex = 0;
    }


    // ==================== setTabSpeacialSkill ====================
    private void setTabSpeacialSkill()
    {
        ITEM_HEIGHT = 24;
        currentListLength = Char.myCharz().infoSpeacialSkill[currentTabIndex].Length;
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


    // ==================== setTabSkill ====================
    private void setTabSkill()
    {
        ITEM_HEIGHT = 30;
        currentListLength = Char.myCharz().nClass.skillTemplates.Length + 6;
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


    // ==================== updateKeySkill ====================
    private void updateKeySkill()
    {
        updateKeyScrollView();
    }


    // ==================== paintSkill ====================
    private void paintSkill(mGraphics g)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        int num = Char.myCharz().nClass.skillTemplates.Length;
        for (int i = 0; i < num + 6; i++)
        {
            int num2 = xScroll + 30;
            int num3 = yScroll + i * ITEM_HEIGHT;
            int num4 = wScroll - 30;
            int h = ITEM_HEIGHT - 1;
            int num5 = xScroll;
            int num6 = yScroll + i * ITEM_HEIGHT;
            int num7 = 34;
            int num8 = ITEM_HEIGHT - 1;
            if (num3 - cmy > yScroll + hScroll || num3 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            if (i == 5)
            {
                g.setColor((i != selected) ? 16765060 : 16776068);
            }
            g.fillRect(num2, num3, num4, h);
            g.drawImage(GameScr.imgSkill, num5, num6, 0);
            if (i == 0)
            {
                SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
                string st = mResources.HP + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cHPGoc);
                mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cHPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().hpFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 1)
            {
                SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
                string st2 = mResources.KI + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cMPGoc);
                mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cMPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().mpFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 2)
            {
                SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
                string st3 = mResources.hit_point + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cDamGoc);
                mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cDamGoc * 100) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().damFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 3)
            {
                SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
                string st4 = mResources.armor + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cDefGoc);
                mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys(500000 + Char.myCharz().cDefGoc * 100000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().defFrom1000TiemNang, num2 + 5, num3 + 15, 0);
            }
            if (i == 4)
            {
                SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
                string st5 = mResources.critical + " " + mResources.root + ": " + Char.myCharz().cCriticalGoc + "%";
                long num9 = 50000000L;
                int num10 = Char.myCharz().cCriticalGoc;
                if (num10 > t_tiemnang.Length - 1)
                {
                    num10 = t_tiemnang.Length - 1;
                }
                num9 = t_tiemnang[num10];
                mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
                long number = num9;
                mFont.tahoma_7_green2.drawString(g, Res.formatNumber2(number) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().criticalFrom1000Tiemnang, num2 + 5, num3 + 15, 0);
            }
            if (i == 5)
            {
                if (specialInfo != null)
                {
                    SmallImage.drawSmallImage(g, spearcialImage, num5 + 4, num6 + 4, 0, 0);
                    string[] array = mFont.tahoma_7.splitFontArray(specialInfo, 120);
                    for (int j = 0; j < array.Length; j++)
                    {
                        mFont.tahoma_7_green2.drawString(g, array[j], num2 + 5, num3 + 3 + j * 12, 0);
                    }
                }
                else
                {
                    mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
                }
            }
            if (i < 6)
            {
                continue;
            }
            int num11 = i - 6;
            SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[num11];
            SmallImage.drawSmallImage(g, skillTemplate.iconId, num5 + 4, num6 + 4, 0, 0);
            Skill skill = Char.myCharz().getSkill(skillTemplate);
            if (skill != null)
            {
                mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point, num2 + num4 - 5, num3 + 3, mFont.RIGHT);
                if (skill.point == skillTemplate.maxPoint)
                {
                    mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
                }
                else if (skill.template.isSkillSpec())
                {
                    string text = mResources.proficiency + ": ";
                    int x = mFont.tahoma_7_green2.getWidthExactOf(text) + num2 + 5;
                    int num12 = num3 + 15;
                    mFont.tahoma_7_green2.drawString(g, text, num2 + 5, num12, 0);
                    mFont.tahoma_7_green2.drawString(g, "(" + skill.strCurExp() + ")", num2 + num4 - 5, num12, mFont.RIGHT);
                    num12 += 4;
                    g.setColor(7169134);
                    g.fillRect(x, num12, 50, 5);
                    int num13 = skill.curExp * 50 / 1000;
                    g.setColor(11992374);
                    g.fillRect(x, num12, num13, 5);
                    if (skill.curExp < 1000)
                    {
                    }
                }
                else
                {
                    Skill skill2 = skillTemplate.skills[skill.point];
                    mFont.tahoma_7_green2.drawString(g, mResources.level + " " + (skill.point + 1) + " " + mResources.need + " " + Res.formatNumber2(skill2.powRequire) + " " + mResources.potential, num2 + 5, num3 + 15, 0);
                }
            }
            else
            {
                Skill skill3 = skillTemplate.skills[0];
                mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
                mFont.tahoma_7_green2.drawString(g, mResources.need_upper + " " + Res.formatNumber2(skill3.powRequire) + " " + mResources.potential_to_learn, num2 + 5, num3 + 15, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintSpeacialSkill ====================
    private void paintSpeacialSkill(mGraphics g)
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
            int num5 = 24;
            int num6 = ITEM_HEIGHT - 1;
            int num7 = xScroll + num5;
            int num8 = yScroll + i * ITEM_HEIGHT;
            int num9 = wScroll - num5;
            int h = ITEM_HEIGHT - 1;
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num7, num8, num9, h);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num3, num4, num5, num6);
            SmallImage.drawSmallImage(g, Char.myCharz().imgSpeacialSkill[currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
            string[] array = mFont.tahoma_7_grey.splitFontArray(Char.myCharz().infoSpeacialSkill[currentTabIndex][i], 140);
            for (int j = 0; j < array.Length; j++)
            {
                mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintSkillInfo ====================
    private void paintSkillInfo(mGraphics g)
    {
        mFont.tahoma_7_white.drawString(g, "Top " + Char.myCharz().rank, X + 45 + (W - 50) / 2, 2, mFont.CENTER);
        mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, X + 45 + (W - 50) / 2, 14, mFont.CENTER);
        mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (W - 50) / 2, 26, mFont.CENTER);
        mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(Char.myCharz().cNangdong), X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (W - 50) / 2, 38, mFont.CENTER);
    }


    // ==================== doSpeacialSkill ====================
    private void doSpeacialSkill()
    {
        string info = Char.myCharz().infoSpeacialSkill[0][selected];
        GameScr.info1.addInfo(info, 0);
        MyVector myVector8 = new();
        myVector8.addElement(new Command(ModFunc.strChooseIntrinsic, this, 8011, info));
        GameCanvas.menu.startAt(myVector8, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
    }


    // ==================== doFireSkill ====================
    private void doFireSkill()
    {
        if (selected < 0)
        {
            return;
        }
        if (Char.myCharz().statusMe == 14)
        {
            GameCanvas.startOKDlg(mResources.can_not_do_when_die);
            return;
        }
        if (selected == 0 || selected == 1 || selected == 2 || selected == 3 || selected == 4 || selected == 5)
        {
            long cTiemNang = Char.myCharz().cTiemNang;
            long cHPGoc = Char.myCharz().cHPGoc;
            long cMPGoc = Char.myCharz().cMPGoc;
            long cDamGoc = Char.myCharz().cDamGoc;
            long cDefGoc = Char.myCharz().cDefGoc;
            _ = Char.myCharz().cCriticalGoc;
            int num2 = 1000;
            if (selected == 0)
            {
                if (cTiemNang < Char.myCharz().cHPGoc + num2)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang + mResources.not_enough_potential_point2 + (Char.myCharz().cHPGoc + num2), isError: false);
                    return;
                }
                if (cTiemNang > cHPGoc && cTiemNang < 10 * (2 * (cHPGoc + num2) + 180) / 2)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + (cHPGoc + num2) + mResources.use_potential_point_for2 + Char.myCharz().hpFrom1000TiemNang + mResources.for_HP, new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * (cHPGoc + num2) + 180) / 2 && cTiemNang < 100 * (2 * (cHPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, null));
                }
                else if (cTiemNang >= 100 * (2 * (cHPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().hpFrom1000TiemNang + mResources.HP + "\n-" + Res.formatNumber2(100 * (2 * (cHPGoc + num2) + 1980) / 2), this, 9007, null));

                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + false));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 1)
            {
                if (Char.myCharz().cTiemNang < Char.myCharz().cMPGoc + num2)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang + mResources.not_enough_potential_point2 + (Char.myCharz().cMPGoc + num2));
                    return;
                }
                if (cTiemNang > cMPGoc && cTiemNang < 10 * (2 * (cMPGoc + num2) + 180) / 2)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + (cMPGoc + num2) + mResources.use_potential_point_for2 + Char.myCharz().mpFrom1000TiemNang + mResources.for_KI, new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * (cMPGoc + num2) + 180) / 2 && cTiemNang < 100 * (2 * (cMPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(cHPGoc + num2), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(10 * (2 * (cHPGoc + num2) + 180) / 2), this, 9006, null));
                }
                else if (cTiemNang >= 100 * (2 * (cMPGoc + num2) + 1980) / 2)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(cMPGoc + num2), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(10 * (2 * (cMPGoc + num2) + 180) / 2), this, 9006, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().mpFrom1000TiemNang + mResources.KI + "\n-" + Res.formatNumber2(100 * (2 * (cMPGoc + num2) + 1980) / 2), this, 9007, null));
                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + false));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 2)
            {
                if (Char.myCharz().cTiemNang < Char.myCharz().cDamGoc * Char.myCharz().expForOneAdd)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Char.myCharz().cTiemNang + mResources.not_enough_potential_point2 + cDamGoc * 100);
                    return;
                }
                if (cTiemNang > cDamGoc && cTiemNang < 10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd)
                {
                    GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + cDamGoc * 100 + mResources.use_potential_point_for2 + Char.myCharz().damFrom1000TiemNang + mResources.for_hit_point, new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
                    return;
                }
                MyVector myVector = new(string.Empty);
                if (cTiemNang >= 10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd && cTiemNang < 100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(cDamGoc * 100), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd), this, 9006, null));
                }
                else if (cTiemNang >= 100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd)
                {
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(cDamGoc * 100), this, 9000, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 10 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(10 * (2 * cDamGoc + 9) / 2 * Char.myCharz().expForOneAdd), this, 9006, null));
                    myVector.addElement(new Command(mResources.increase_upper + "\n" + 100 * Char.myCharz().damFrom1000TiemNang + "\n" + mResources.hit_point + "\n-" + Res.formatNumber2(100 * (2 * cDamGoc + 99) / 2 * Char.myCharz().expForOneAdd), this, 9007, null));
                }
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + false));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            if (selected == 3)
            {
                if (Char.myCharz().cTiemNang < 50000 + Char.myCharz().cDefGoc * 1000)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys(50000 + Char.myCharz().cDefGoc * 1000));
                    return;
                }
                long number = 2 * (cDefGoc + 5) / 2L * 100000;
                long number2 = 10L * (2 * (cDefGoc + 5) + 9) / 2 * 100000;
                long number3 = 100L * (2 * (cDefGoc + 5) + 99) / 2 * 100000;
                MyVector myVector = new(string.Empty);
                myVector.addElement(new Command(mResources.increase_upper + "\n1 " + mResources.armor + "\n" + Res.formatNumber2(number), this, 9000, null));
                myVector.addElement(new Command(mResources.increase_upper + "\n10 " + mResources.armor + "\n" + Res.formatNumber2(number2), this, 9006, null));
                myVector.addElement(new Command(mResources.increase_upper + "\n100 " + mResources.armor + "\n" + Res.formatNumber2(number3), this, 9007, null));
                myVector.addElement(new Command(ModFunc.strInCrease, ModFunc.GI(), 100, selected + "-" + false));
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                addSkillDetail2(selected, false);
            }
            else if (selected == 4)
            {
                int num4 = Char.myCharz().cCriticalGoc;
                if (num4 > t_tiemnang.Length - 1)
                {
                    num4 = t_tiemnang.Length - 1;
                }
                long num3 = t_tiemnang[num4];
                if (Char.myCharz().cTiemNang < num3)
                {
                    GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num3));
                    return;
                }
                GameCanvas.startYesNoDlg(mResources.use_potential_point_for1 + Res.formatNumber(num3) + mResources.use_potential_point_for2 + Char.myCharz().criticalFrom1000Tiemnang + mResources.for_crit, new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
            }
            else if (selected == 5)
            {
                Service.gI().speacialSkill(0);
            }
            return;
        }
        int index = selected - 6;
        SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[index];
        Skill skill = Char.myCharz().getSkill(skillTemplate);
        Skill skill2 = null;
        MyVector myVector8 = new(string.Empty);
        if (skill != null)
        {
            if (skill.point == skillTemplate.maxPoint)
            {
                myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
                myVector8.addElement(new Command(mResources.CLOSE, 2));
            }
            else
            {
                skill2 = skillTemplate.skills[skill.point];
                myVector8.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
                myVector8.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
            }
        }
        else
        {
            skill2 = skillTemplate.skills[0];
            myVector8.addElement(new Command(mResources.learn, this, 9004, skill2));
        }
        GameCanvas.menu.startAt(myVector8, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
        addSkillDetail(skillTemplate, skill, skill2);
    }


}
