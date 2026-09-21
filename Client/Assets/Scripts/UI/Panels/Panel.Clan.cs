using System;

public partial class Panel
{
    // ==================== updateKeyClanIcon ====================
    private void updateKeyClanIcon()
    {
        updateKeyScrollView();
    }


    // ==================== getCurrClanOtion ====================
    private void getCurrClanOtion()
    {
        isClanOption = false;
        if (type != 0 || mainTabName.Length != 5 || currentTabIndex != 3)
        {
            return;
        }
        isClanOption = false;
        if (selected == 0)
        {
            currClanOption = new int[clansOption.Length];
            for (int i = 0; i < currClanOption.Length; i++)
            {
                currClanOption[i] = i;
            }
            if (!isViewMember)
            {
                isClanOption = true;
            }
        }
        else if (selected != 1 && !isSearchClan && selected > 0)
        {
            currClanOption = new int[1];
            for (int j = 0; j < currClanOption.Length; j++)
            {
                currClanOption[j] = j;
            }
            isClanOption = true;
        }
    }


    // ==================== updateKeyClansOption ====================
    private void updateKeyClansOption()
    {
        if (currClanOption == null)
        {
            return;
        }
        if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
        {
            currMess = getCurrMessage();
            cSelected--;
            if (selected == 0 && cSelected < 0)
            {
                cSelected = currClanOption.Length - 1;
            }
            if (selected > 1 && isMessage && currMess.option != null && cSelected < 0)
            {
                cSelected = currMess.option.Length - 1;
            }
        }
        else if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
        {
            currMess = getCurrMessage();
            cSelected++;
            if (selected == 0 && cSelected > currClanOption.Length - 1)
            {
                cSelected = 0;
            }
            if (selected > 1 && isMessage && currMess.option != null && cSelected > currMess.option.Length - 1)
            {
                cSelected = 0;
            }
        }
    }


    // ==================== updateKeyClans ====================
    private void updateKeyClans()
    {
        updateKeyScrollView();
        updateKeyClansOption();
    }


    // ==================== checkOptionSelect ====================
    private void checkOptionSelect()
    {
        try
        {
            if (type != 0 || currentTabIndex != 3 || mainTabName.Length != 5 || selected == -1)
            {
                return;
            }
            int num = 0;
            if (selected == 0)
            {
                num = xScroll + wScroll / 2 - clansOption.Length * TAB_W / 2;
                cSelected = (GameCanvas.px - num) / TAB_W;
            }
            else
            {
                currMess = getCurrMessage();
                if (currMess != null && currMess.option != null)
                {
                    num = xScroll + wScroll - 2 - currMess.option.Length * 40;
                    cSelected = (GameCanvas.px - num) / 40;
                }
            }
            if (GameCanvas.px < num)
            {
                cSelected = -1;
            }
        }
        catch (Exception ex)
        {
            Res.outz("Throw err " + ex.StackTrace);
        }
    }


    // ==================== initTabClans ====================
    public void initTabClans()
    {
        if (isSearchClan)
        {
            currentListLength = ((clans != null) ? (clans.Length + 2) : 2);
            clanInfo = mResources.clan_list;
        }
        else if (isViewMember)
        {
            clanReport = string.Empty;
            currentListLength = ((member != null) ? member.size() : myMember.size()) + 2;
            clanInfo = mResources.member + " " + ((currClan == null) ? Char.myCharz().clan.name : currClan.name);
        }
        else if (isMessage)
        {
            currentListLength = ClanMessage.vMessage.size() + 2;
            clanInfo = mResources.msg;
            clanReport = string.Empty;
        }
        if (Char.myCharz().clan == null)
        {
            clansOption = new string[2][]
            {
                mResources.findClan,
                mResources.createClan
            };
        }
        else if (!isViewMember)
        {
            if (myMember.size() > 1)
            {
                clansOption = new string[3][]
                {
                    mResources.chatClan,
                    mResources.request_pea2,
                    mResources.memberr
                };
            }
            else
            {
                clansOption = new string[1][] { mResources.memberr };
            }
        }
        else if (Char.myCharz().role > 0)
        {
            clansOption = new string[2][]
            {
                mResources.msgg,
                mResources.leaveClan
            };
        }
        else if (myMember.size() > 1)
        {
            clansOption = new string[4][]
            {
                mResources.msgg,
                mResources.leaveClan,
                mResources.khau_hieuu,
                mResources.bieu_tuongg
            };
        }
        else
        {
            clansOption = new string[3][]
            {
                mResources.msgg,
                mResources.khau_hieuu,
                mResources.bieu_tuongg
            };
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
    }


    // ==================== setTabClans ====================
    public void setTabClans()
    {
        GameScr.isNewClanMessage = false;
        ITEM_HEIGHT = 24;
        if (lastSelect != null && lastSelect[3] == 0)
        {
            lastSelect[3] = -1;
        }
        currentListLength = 2;
        if (Char.myCharz().clan != null)
        {
            isMessage = true;
            isViewMember = false;
            isSearchClan = false;
        }
        else
        {
            isMessage = false;
            isViewMember = false;
            isSearchClan = true;
        }
        if (Char.myCharz().clan != null)
        {
            currentListLength = ClanMessage.vMessage.size() + 2;
        }
        initTabClans();
        cSelected = -1;
        if (chatTField == null)
        {
            chatTField = new ChatTextField();
            chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
            chatTField.initChatTextField();
            chatTField.parentScreen = GameCanvas.panel;
        }
        if (Char.myCharz().clan == null)
        {
            clanReport = mResources.findingClan;
            Service.gI().searchClan(string.Empty);
        }
        selected = lastSelect[currentTabIndex];
        if (GameCanvas.isTouch)
        {
            selected = -1;
        }
    }


    // ==================== paintClans ====================
    private void paintClans(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(-cmx, -cmy);
        g.setColor(0);
        int num = xScroll + wScroll / 2 - clansOption.Length * TAB_W / 2;
        if (currentListLength == 2)
        {
            mFont.tahoma_7_green2.drawString(g, clanReport, xScroll + wScroll / 2, yScroll + 24 + hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
            if (isMessage && myMember.size() == 1)
            {
                for (int i = 0; i < mResources.clanEmpty.Length; i++)
                {
                    mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], xScroll + wScroll / 2, yScroll + 24 + hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
                }
            }
        }
        if (isMessage)
        {
            currentListLength = ClanMessage.vMessage.size() + 2;
        }
        for (int j = 0; j < currentListLength; j++)
        {
            int num2 = xScroll;
            int num3 = yScroll + j * ITEM_HEIGHT;
            int num4 = 24;
            int num5 = ITEM_HEIGHT - 1;
            int num6 = xScroll + num4;
            int num7 = yScroll + j * ITEM_HEIGHT;
            int num8 = wScroll - num4;
            int num9 = ITEM_HEIGHT - 1;
            if (num7 - cmy > yScroll + hScroll || num7 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            switch (j)
            {
                case 0:
                    {
                        for (int k = 0; k < clansOption.Length; k++)
                        {
                            g.setColor((k != cSelected || j != selected) ? 15723751 : 16383818);
                            g.fillRect(num + k * TAB_W, num7, TAB_W - 1, 23);
                            for (int l = 0; l < clansOption[k].Length; l++)
                            {
                                mFont.tahoma_7_grey.drawString(g, clansOption[k][l], num + k * TAB_W + TAB_W / 2, yScroll + l * 11, mFont.CENTER);
                            }
                        }
                        continue;
                    }
                case 1:
                    g.setColor((j != selected) ? 15196114 : 16383818);
                    g.fillRect(xScroll, num7, wScroll, num9);
                    if (clanInfo != null)
                    {
                        mFont.tahoma_7b_dark.drawString(g, clanInfo, xScroll + wScroll / 2, num7 + 6, mFont.CENTER);
                    }
                    continue;
            }
            if (isSearchClan)
            {
                if (clans == null || clans.Length == 0)
                {
                    continue;
                }
                g.setColor((j != selected) ? 15196114 : 16383818);
                g.fillRect(num6, num7, num8, num9);
                g.setColor((j != selected) ? 9993045 : 9541120);
                g.fillRect(num2, num3, num4, num5);
                if (ClanImage.isExistClanImage(clans[j - 2].imgID))
                {
                    if (ClanImage.getClanImage((short)clans[j - 2].imgID).idImage != null)
                    {
                        SmallImage.drawSmallImage(g, ClanImage.getClanImage((short)clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
                    }
                }
                else
                {
                    ClanImage clanImage = new ClanImage();
                    clanImage.ID = clans[j - 2].imgID;
                    if (!ClanImage.isExistClanImage(clanImage.ID))
                    {
                        ClanImage.addClanImage(clanImage);
                    }
                }
                string st = ((clans[j - 2].name.Length <= 23) ? clans[j - 2].name : (clans[j - 2].name.Substring(0, 23) + "..."));
                mFont.tahoma_7b_green2.drawString(g, st, num6 + 5, num7, 0);
                g.setClip(num6, num7, num8 - 10, num9);
                mFont.tahoma_7_blue.drawString(g, clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
                g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
                mFont.tahoma_7_green2.drawString(g, clans[j - 2].currMember + "/" + clans[j - 2].maxMember, num6 + num8 - 5, num7, mFont.RIGHT);
                continue;
            }
            if (isViewMember)
            {
                g.setColor((j != selected) ? 15196114 : 16383818);
                g.fillRect(num6, num7, num8, num9);
                g.setColor((j != selected) ? 9993045 : 9541120);
                g.fillRect(num2, num3, num4, num5);
                Member member = ((this.member == null) ? ((Member)myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2)));
                if (member.headICON != -1)
                {
                    SmallImage.drawSmallImage(g, member.headICON, num2, num3, 0, 0);
                }
                else
                {
                    Part part = GameScr.parts[member.head];
                    SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num2 + part.pi[Char.CharInfo[0][0][0]].dx, num3 + 3 + part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
                }
                g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
                mFont mFont2 = mFont.tahoma_7b_dark;
                if (member.role == 0)
                {
                    mFont2 = mFont.tahoma_7b_red;
                }
                else if (member.role == 1)
                {
                    mFont2 = mFont.tahoma_7b_green;
                }
                else if (member.role == 2)
                {
                    mFont2 = mFont.tahoma_7b_green2;
                }
                mFont2.drawString(g, member.name, num6 + 5, num7, 0);
                mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
                SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
                mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint, num6 + num8 - 15, num7 + 6, mFont.RIGHT);
                continue;
            }
            if (!isMessage || ClanMessage.vMessage.size() == 0)
            {
                continue;
            }
            ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
            g.setColor((j != selected || clanMessage.option != null) ? 15196114 : 16383818);
            g.fillRect(num2, num3, num8 + num4, num9);
            clanMessage.paint(g, num2, num3);
            if (clanMessage.option == null)
            {
                continue;
            }
            int num10 = xScroll + wScroll - 2 - clanMessage.option.Length * 40;
            for (int m = 0; m < clanMessage.option.Length; m++)
            {
                if (m == cSelected && j == selected)
                {
                    g.drawImage(GameScr.imgLbtnFocus2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
                    mFont.tahoma_7b_green2.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
                }
                else
                {
                    g.drawImage(GameScr.imgLbtn2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
                    mFont.tahoma_7b_dark.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
                }
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintClanInfo ====================
    private void paintClanInfo(mGraphics g)
    {
        if (Char.myCharz().clan == null)
        {
            SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
            mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (wScroll - 50) / 2 + 50, 20, mFont.CENTER);
        }
        else if (!isViewMember)
        {
            Clan clan = Char.myCharz().clan;
            if (clan != null)
            {
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
                mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
                mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
                mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
                mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
                TextInfo.paint(g, clan.slogan, 60, 38, wScroll - 70, ITEM_HEIGHT, mFont.tahoma_7_yellow);
            }
        }
        else
        {
            Clan clan2 = ((currClan == null) ? Char.myCharz().clan : currClan);
            SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 25, 50, 0, 33);
            mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
            mFont.tahoma_7_yellow.drawString(g, mResources.member + ": " + clan2.currMember + "/" + clan2.maxMember, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
            mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
            TextInfo.paint(g, clan2.slogan, 60, 38, wScroll - 70, ITEM_HEIGHT, mFont.tahoma_7_yellow);
        }
    }


    // ==================== searchClan ====================
    private void searchClan()
    {
        chatTField.strChat = mResources.input_clan_name;
        chatTField.tfChat.name = mResources.clan_name;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.isFocus = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }


    // ==================== chatClan ====================
    private void chatClan()
    {
        chatTField.strChat = mResources.chat_clan;
        chatTField.tfChat.name = mResources.CHAT;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.isFocus = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }


    // ==================== creatClan ====================
    public void creatClan()
    {
        chatTField.strChat = mResources.input_clan_name_to_create;
        chatTField.tfChat.name = mResources.input_clan_name;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }


    // ==================== chagenSlogan ====================
    public void chagenSlogan()
    {
        chatTField.strChat = mResources.input_clan_slogan;
        chatTField.tfChat.name = mResources.input_clan_slogan;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.isFocus = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }


    // ==================== changeIcon ====================
    public void changeIcon()
    {
        if (tabIcon == null)
        {
            tabIcon = new TabClanIcon();
        }
        tabIcon.text = chatTField.tfChat.getText();
        tabIcon.show(isGetName: false);
        chatTField.isShow = false;
    }


    // ==================== doFireClanOption ====================
    private void doFireClanOption()
    {
        try
        {
            partID = null;
            charInfo = null;
            if (selected < 0)
            {
                cSelected = -1;
                return;
            }
            if (Char.myCharz().clan == null)
            {
                if (selected == 0)
                {
                    if (cSelected == 0)
                    {
                        searchClan();
                    }
                    else if (cSelected == 1)
                    {
                        InfoDlg.showWait();
                        creatClan();
                        Service.gI().getClan(1, -1, null);
                    }
                }
                else if (selected != -1)
                {
                    if (selected == 1)
                    {
                        if (isSearchClan)
                        {
                            Service.gI().searchClan(string.Empty);
                        }
                        else if (isViewMember && currClan != null)
                        {
                            GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + currClan.name, new Command(mResources.YES, this, 4000, currClan), new Command(mResources.NO, this, 4005, currClan));
                        }
                    }
                    else if (isSearchClan)
                    {
                        currClan = getCurrClan();
                        if (currClan != null)
                        {
                            MyVector myVector = new MyVector();
                            myVector.addElement(new Command(mResources.request_join_clan, this, 4000, currClan));
                            myVector.addElement(new Command(mResources.view_clan_member, this, 4001, currClan));
                            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                            addClanDetail(getCurrClan());
                        }
                    }
                    else if (isViewMember)
                    {
                        currMem = getCurrMember();
                        if (currMem != null)
                        {
                            MyVector myVector2 = new MyVector();
                            myVector2.addElement(new Command(mResources.CLOSE, this, 8000, currClan));
                            GameCanvas.menu.startAt(myVector2, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                            GameCanvas.menu.startAt(myVector2, 0, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                            addClanMemberDetail(currMem);
                        }
                    }
                }
            }
            else if (selected == 0)
            {
                if (isMessage)
                {
                    if (cSelected == 0)
                    {
                        if (myMember.size() > 1)
                        {
                            chatClan();
                        }
                        else
                        {
                            member = null;
                            isSearchClan = false;
                            isViewMember = true;
                            isMessage = false;
                            currentListLength = myMember.size() + 2;
                            initTabClans();
                        }
                    }
                    if (cSelected == 1)
                    {
                        Service.gI().clanMessage(1, null, -1);
                    }
                    if (cSelected == 2)
                    {
                        member = null;
                        isSearchClan = false;
                        isViewMember = true;
                        isMessage = false;
                        currentListLength = myMember.size() + 2;
                        initTabClans();
                        getCurrClanOtion();
                    }
                }
                else if (isViewMember)
                {
                    if (cSelected == 0)
                    {
                        isSearchClan = false;
                        isViewMember = false;
                        isMessage = true;
                        currentListLength = ClanMessage.vMessage.size() + 2;
                        initTabClans();
                    }
                    if (cSelected == 1)
                    {
                        if (myMember.size() > 1)
                        {
                            Service.gI().leaveClan();
                        }
                        else
                        {
                            chagenSlogan();
                        }
                    }
                    if (cSelected == 2)
                    {
                        if (myMember.size() > 1)
                        {
                            chagenSlogan();
                        }
                        else
                        {
                            Service.gI().getClan(3, -1, null);
                        }
                    }
                    if (cSelected == 3)
                    {
                        Service.gI().getClan(3, -1, null);
                    }
                }
            }
            else if (selected == 1)
            {
                if (isSearchClan)
                {
                    Service.gI().searchClan(string.Empty);
                }
            }
            else if (isSearchClan)
            {
                currClan = getCurrClan();
                if (currClan != null)
                {
                    MyVector myVector3 = new MyVector();
                    myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, currClan));
                    GameCanvas.menu.startAt(myVector3, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                    addClanDetail(getCurrClan());
                }
            }
            else if (isViewMember)
            {
                currMem = getCurrMember();
                if (currMem != null)
                {
                    MyVector myVector4 = new MyVector();
                    if (member != null)
                    {
                        myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
                    }
                    else if (myMember != null)
                    {
                        if (Char.myCharz().charID == currMem.ID || Char.myCharz().role == 2)
                        {
                            myVector4.addElement(new Command(mResources.CLOSE, this, 8000, currMem));
                        }
                        if (Char.myCharz().role < 2 && Char.myCharz().charID != currMem.ID)
                        {
                            if (currMem.role == 0 || currMem.role == 1)
                            {
                                myVector4.addElement(new Command(mResources.CLOSE, this, 8000, currMem));
                            }
                            if (currMem.role == 2)
                            {
                                myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, currMem));
                            }
                            if (Char.myCharz().role == 0)
                            {
                                myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, currMem));
                                if (currMem.role == 1)
                                {
                                    myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, currMem));
                                }
                            }
                        }
                        if (Char.myCharz().role < currMem.role)
                        {
                            myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, currMem));
                        }
                    }
                    myVector4.addElement(new Command(ModFunc.strTeleportTo, this, 8004, currMem.ID));
                    GameCanvas.menu.startAt(myVector4, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                    addClanMemberDetail(currMem);
                }
            }
            else if (isMessage)
            {
                currMess = getCurrMessage();
                if (currMess != null)
                {
                    if (currMess.type == 0)
                    {
                        MyVector myVector5 = new MyVector();
                        myVector5.addElement(new Command(mResources.CLOSE, this, 8000, currMess));
                        GameCanvas.menu.startAt(myVector5, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
                        addMessageDetail(currMess);
                    }
                    else if (currMess.type == 1)
                    {
                        if (currMess.playerId != Char.myCharz().charID && cSelected != -1)
                        {
                            Service.gI().clanDonate(currMess.id);
                        }
                    }
                    else if (currMess.type == 2 && currMess.option != null)
                    {
                        if (cSelected == 0)
                        {
                            Service.gI().joinClan(currMess.id, 1);
                        }
                        else if (cSelected == 1)
                        {
                            Service.gI().joinClan(currMess.id, 0);
                        }
                    }
                }
            }
            if (GameCanvas.isTouch)
            {
                cSelected = -1;
                selected = -1;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    // ==================== doFireClanIcon ====================
    private void doFireClanIcon()
    {
    }


}
