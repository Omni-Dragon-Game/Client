using System;
using Assets.src.g;
using Mod;

/// <summary>
/// Quản lý việc khởi tạo nội dung và vẽ tooltip chi tiết (Item, Skill, Bang hội, Top, Member).
/// Tách ra từ Panel.cs để file chính tinh gọn theo chuẩn Clean Code (< 1.000 dòng).
/// </summary>
public partial class Panel
{
    public void addItemDetail(Item item)
    {
        try
        {
            cp = new ChatPopup();
            string empty = string.Empty;
            string text = string.Empty;
            if (item.template.gender != Char.myCharz().cgender)
            {
                if (item.template.gender == 0)
                {
                    text = text + "\n|7|1|" + mResources.from_earth;
                }
                else if (item.template.gender == 1)
                {
                    text = text + "\n|7|1|" + mResources.from_namec;
                }
                else if (item.template.gender == 2)
                {
                    text = text + "\n|7|1|" + mResources.from_sayda;
                }
            }

            mFont mFont2 = mFont.tahoma_7b_dark;
            string text2 = string.Empty;
            if (item.itemOption != null)
            {
                for (int i = 0; i < item.itemOption.Length; i++)
                {
                    if (item.itemOption[i].optionTemplate.id == 72)
                    {
                        text2 = " [+" + item.itemOption[i].param + "]";
                    }
                    if (item.itemOption[i].optionTemplate.id == 225)
                    {
                        text2 = " [+" + item.itemOption[i].param + "]";
                    }
                }
            }
            bool flag = false;
            if (item.itemOption != null)
            {
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id == 225)
                    {
                        flag = true;
                        if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 2)
                        {
                            text = text + "|0|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param >= 3 && item.itemOption[j].param <= 4)
                        {
                            text = text + "|2|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param >= 5 && item.itemOption[j].param <= 6)
                        {
                            text = text + "|8|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param >= 7 && item.itemOption[j].param <= 10)
                        {
                            text = text + "|7|1|" + item.template.name + text2;
                        }
                    }
                    if (item.itemOption[j].optionTemplate.id == 72)
                    {
                        flag = true;
                        if (item.itemOption[j].param >= 1 && item.itemOption[j].param <= 5)
                        {
                            text = text + "|2|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param >= 6 && item.itemOption[j].param <= 7)
                        {
                            text = text + "|8|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param >= 8 && item.itemOption[j].param <= 10)
                        {
                            text = text + "|7|1|" + item.template.name + text2;
                        }
                    }
                }
            }
            if (!flag)
            {
                text = text + "|0|1|" + item.template.name + text2;
            }
            if (item.itemOption != null)
            {
                for (int k = 0; k < item.itemOption.Length; k++)
                {
                    if (item.itemOption[k].optionTemplate.name.StartsWith("$") ? true : false)
                    {
                        empty = item.itemOption[k].getOptiongColor();
                        if (item.itemOption[k].param == 1)
                        {
                            text = text + "\n|1|1|" + empty;
                        }
                        if (item.itemOption[k].param == 0)
                        {
                            text = text + "\n|0|1|" + empty;
                        }
                        continue;
                    }
                    empty = item.itemOption[k].getOptionString();
                    if (!empty.Equals(string.Empty) && item.itemOption[k].optionTemplate.id != 72)
                    {
                        if (item.itemOption[k].optionTemplate.id == 102)
                        {
                            cp.starSlot = (sbyte)item.itemOption[k].param;
                            Res.outz("STAR SLOT= " + cp.starSlot);
                        }
                        else if (item.itemOption[k].optionTemplate.id == 107)
                        {
                            cp.maxStarSlot = (sbyte)item.itemOption[k].param;
                            Res.outz("STAR SLOT= " + cp.maxStarSlot);
                        }
                        else
                        {
                            text = text + "\n|1|1|" + empty;
                        }
                    }
                }
            }
            if (currItem.template.strRequire > 1)
            {
                string text3 = mResources.pow_request + ": " + currItem.template.strRequire;
                if (currItem.template.strRequire > Char.myCharz().cPower)
                {
                    text = text + "\n|3|1|" + text3;
                    string text4 = text;
                    text = text4 + "\n|3|1|" + mResources.your_pow + ": " + Char.myCharz().cPower;
                }
                else
                {
                    text = text + "\n|6|1|" + text3;
                }
            }
            else
            {
                text += "\n|6|1|";
            }
            currItem.compare = getCompare(currItem);
            text += "\n--";
            text = text + "\n|6|" + item.template.description;
            if (!item.reason.Equals(string.Empty))
            {
                if (!item.template.description.Equals(string.Empty))
                {
                    text += "\n--";
                }
                text = text + "\n|2|" + item.reason;
            }
            if (cp.maxStarSlot > 0)
            {
                text += "\n\n";
            }
            popUpDetailInit(cp, text);
            idIcon = item.template.iconID;
            partID = null;
            charInfo = null;
        }
        catch (Exception ex)
        {
            Res.outz("ex " + ex.StackTrace);
        }
    }

    public void popUpDetailInit(ChatPopup cp, string chat)
    {
        cp.isClip = false;
        cp.sayWidth = 180;
        cp.cx = 3 + X - ((X != 0) ? (Res.abs(cp.sayWidth - W) + 8) : 0);
        cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
        cp.delay = 10000000;
        cp.c = null;
        cp.sayRun = 7;
        cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
        if (cp.ch > GameCanvas.h - 80)
        {
            cp.ch = GameCanvas.h - 80;
            cp.lim = cp.says.Length * 12 - cp.ch + 17;
            if (cp.lim < 0)
            {
                cp.lim = 0;
            }
            ChatPopup.cmyText = 0;
            cp.isClip = true;
        }
        cp.cy = GameCanvas.menu.menuY - cp.ch;
        while (cp.cy < 10)
        {
            cp.cy++;
            GameCanvas.menu.menuY++;
        }
        cp.mH = 0;
        cp.strY = 10;
    }

    public void popUpDetailInitArray(ChatPopup cp, string[] chat)
    {
        cp.sayWidth = 160;
        cp.cx = 3 + X;
        cp.says = chat;
        cp.delay = 10000000;
        cp.c = null;
        cp.sayRun = 7;
        cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
        cp.cy = GameCanvas.menu.menuY - cp.ch;
        cp.mH = 0;
        cp.strY = 10;
    }

    public void addMessageDetail(ClanMessage cm)
    {
        cp = new ChatPopup();
        string text = "|0|" + cm.playerName;
        text = text + "\n|1|" + Member.getRole(cm.role);
        for (int i = 0; i < myMember.size(); i++)
        {
            Member member = (Member)myMember.elementAt(i);
            if (cm.playerId == member.ID)
            {
                string text2 = text;
                text = text2 + "\n|5|" + mResources.clan_capsuledonate + ": " + member.clanPoint;
                text2 = text;
                text = text2 + "\n|5|" + mResources.clan_capsuleself + ": " + member.curClanPoint;
                text2 = text;
                text = text2 + "\n|4|" + mResources.give_pea + ": " + member.donate + mResources.time;
                text2 = text;
                text = text2 + "\n|4|" + mResources.receive_pea + ": " + member.receive_donate + mResources.time;
                partID = new int[3] { member.head, member.leg, member.body };
                break;
            }
        }
        text += "\n--";
        for (int j = 0; j < cm.chat.Length; j++)
        {
            text = text + "\n" + cm.chat[j];
        }
        if (cm.type == 1)
        {
            string text2 = text;
            text = text2 + "\n|6|" + mResources.received + " " + cm.recieve + "/" + cm.maxCap;
        }
        popUpDetailInit(cp, text);
        charInfo = null;
    }

    public void addThachDauDetail(TopInfo t)
    {
        string text = "|0|1|" + t.name;
        text = text + "\n|1|Top " + t.rank;
        text = text + "\n|1|" + t.info;
        text = text + "\n|2|" + t.info2;
        cp = new ChatPopup();
        popUpDetailInit(cp, text);
        partID = new int[3] { t.headID, t.leg, t.body };
        currItem = null;
        charInfo = null;
    }

    public void addClanMemberDetail(Member m)
    {
        string text = "|0|1|" + m.name;
        string text2 = "\n|2|1|";
        if (m.role == 0)
        {
            text2 = "\n|7|1|";
        }
        if (m.role == 1)
        {
            text2 = "\n|1|1|";
        }
        if (m.role == 2)
        {
            text2 = "\n|0|1|";
        }
        text = text + text2 + Member.getRole(m.role);
        string text3 = text;
        text = text3 + "\n|2|1|" + mResources.power + ": " + m.powerPoint;
        text += "\n--";
        text3 = text;
        text = text3 + "\n|5|" + mResources.clan_capsuledonate + ": " + m.clanPoint;
        text3 = text;
        text = text3 + "\n|5|" + mResources.clan_capsuleself + ": " + m.curClanPoint;
        text3 = text;
        text = text3 + "\n|4|" + mResources.give_pea + ": " + m.donate + mResources.time;
        text3 = text;
        text = text3 + "\n|4|" + mResources.receive_pea + ": " + m.receive_donate + mResources.time;
        text3 = text;
        text = text3 + "\n|6|" + mResources.join_date + ": " + m.joinTime;
        cp = new ChatPopup();
        popUpDetailInit(cp, text);
        partID = new int[3] { m.head, m.leg, m.body };
        currItem = null;
        charInfo = null;
    }

    public void addClanDetail(Clan cl)
    {
        try
        {
            string text = "|0|" + cl.name;
            string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, wScroll - 60);
            for (int i = 0; i < array.Length; i++)
            {
                text = text + "\n|2|" + array[i];
            }
            text += "\n--";
            string text2 = text;
            text = text2 + "\n|7|" + mResources.clan_leader + ": " + cl.leaderName;
            text2 = text;
            text = text2 + "\n|1|" + mResources.power_point + ": " + cl.powerPoint;
            text2 = text;
            text = text2 + "\n|4|" + mResources.member + ": " + cl.currMember + "/" + cl.maxMember;
            text2 = text;
            text = text2 + "\n|4|" + mResources.level + ": " + cl.level;
            text2 = text;
            text = text2 + "\n|4|" + mResources.clan_birthday + ": " + NinjaUtil.getDate(cl.date);
            cp = new ChatPopup();
            popUpDetailInit(cp, text);
            idIcon = ClanImage.getClanImage((short)cl.imgID).idImage[0];
            currItem = null;
        }
        catch (Exception ex)
        {
            Res.outz("Throw  exception " + ex.StackTrace);
        }
    }

    public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
    {
        string text = "|0|" + tp.name;
        for (int i = 0; i < tp.description.Length; i++)
        {
            text = text + "\n|4|" + tp.description[i];
        }
        text += "\n--";
        if (skill != null)
        {
            string text2 = text;
            text = text2 + "\n|2|" + mResources.cap_do + ": " + skill.point;
            text = text + "\n|5|" + NinjaUtil.Replace(tp.damInfo, "#", skill.damage + string.Empty);
            text2 = text;
            text = text2 + "\n|5|" + mResources.KI_consume + skill.manaUse + ((tp.manaUseType != 1) ? string.Empty : "%");
            text2 = text;
            text = text2 + "\n|5|" + mResources.cooldown + ": " + skill.strTimeReplay() + "s";
            text += "\n--";
            if (skill.point == tp.maxPoint)
            {
                text = text + "\n|0|" + mResources.max_level_reach;
            }
            else
            {
                if (!skill.template.isSkillSpec())
                {
                    text2 = text;
                    text = text2 + "\n|1|" + mResources.next_level_require + Res.formatNumber(nextSkill.powRequire) + " " + mResources.potential;
                }
                text = text + "\n|4|" + NinjaUtil.Replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
            }
        }
        else
        {
            text = text + "\n|2|" + mResources.not_learn;
            string text2 = text;
            text = text2 + "\n|1|" + mResources.learn_require + Res.formatNumber(nextSkill.powRequire) + " " + mResources.potential;
            text = text + "\n|4|" + NinjaUtil.Replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
            text2 = text;
            text = text2 + "\n|4|" + mResources.KI_consume + nextSkill.manaUse + ((tp.manaUseType != 1) ? string.Empty : "%");
            text2 = text;
            text = text2 + "\n|4|" + mResources.cooldown + ": " + nextSkill.strTimeReplay() + "s";
        }
        currItem = null;
        partID = null;
        charInfo = null;
        cp = new ChatPopup();
        popUpDetailInit(cp, text);
        idIcon = 0;
    }

    public void paintDetail(mGraphics g)
    {
        if (cp == null || cp.says == null)
        {
            return;
        }
        cp.paint(g);
        int num = cp.cx + 13;
        int num2 = cp.cy + 11;
        if (type == 15)
        {
            num += 5;
            num2 += 26;
        }
        if (type == 0 && currentTabIndex == 3)
        {
            if (isSearchClan)
            {
                num -= 5;
            }
            else if (partID != null || charInfo != null)
            {
                num = cp.cx + 21;
                num2 = cp.cy + 40;
            }
        }
        if (partID != null)
        {
            Part part = GameScr.parts[partID[0]];
            Part part2 = GameScr.parts[partID[1]];
            Part part3 = GameScr.parts[partID[2]];
            SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num + Char.CharInfo[0][0][1] + part.pi[Char.CharInfo[0][0][0]].dx, num2 - Char.CharInfo[0][0][2] + part.pi[Char.CharInfo[0][0][0]].dy, 0, 0);
            SmallImage.drawSmallImage(g, part2.pi[Char.CharInfo[0][1][0]].id, num + Char.CharInfo[0][1][1] + part2.pi[Char.CharInfo[0][1][0]].dx, num2 - Char.CharInfo[0][1][2] + part2.pi[Char.CharInfo[0][1][0]].dy, 0, 0);
            SmallImage.drawSmallImage(g, part3.pi[Char.CharInfo[0][2][0]].id, num + Char.CharInfo[0][2][1] + part3.pi[Char.CharInfo[0][2][0]].dx, num2 - Char.CharInfo[0][2][2] + part3.pi[Char.CharInfo[0][2][0]].dy, 0, 0);
        }
        else if (charInfo != null)
        {
            charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, isPaintBag: true);
        }
        else if (idIcon != -1)
        {
            SmallImage.drawSmallImage(g, idIcon, num, num2, 0, 3);
        }
        if (currItem != null && currItem.template.type != 5)
        {
            if (currItem.compare > 0)
            {
                g.drawImage(imgUp, num - 7, num2 + 13, 3);
                mFont.tahoma_7b_green.drawString(g, Res.abs(currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
            }
            else if (currItem.compare < 0 && currItem.compare != -1)
            {
                g.drawImage(imgDown, num - 7, num2 + 13, 3);
                mFont.tahoma_7b_red.drawString(g, Res.abs(currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
            }
        }
    }
}
