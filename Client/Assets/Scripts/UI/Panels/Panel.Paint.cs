using System;

public partial class Panel
{
    // ==================== paint ====================
    public void paint(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
        g.translate(-cmx, 0);
        g.translate(X, Y);
        if (GameCanvas.panel.combineSuccess != -1)
        {
            if (Equals(GameCanvas.panel))
            {
                paintCombineEff(g);
            }
            return;
        }
        GameCanvas.paintz.paintFrameSimple(X, Y, W, H, g);
        try
        {
            paintTopInfo(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTopInfo: " + ex.ToString());
        }
        try
        {
            paintBottomMoneyInfo(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintBottomMoneyInfo: " + ex.ToString());
        }
        try
        {
            paintTab(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTab: " + ex.ToString());
        }
        try
        {
            switch (type)
            {
            case 9:
                paintArchivement(g);
                break;
            case 21:
            case 28:
                if (currentTabIndex == 0)
                {
                    paintPetInventory(g, type == 28);
                }
                else if (currentTabIndex == 1)
                {
                    paintPetSkill(g, type == 28);
                }
                else if (currentTabIndex == 2)
                {
                    paintPetStatus(g);
                }
                else if (currentTabIndex == 3)
                {
                    paintInventory(g);
                }
                break;
            case 24:
                paintGameSubInfo(g);
                break;
            case 23:
                paintGameInfo(g);
                break;
            case 0:
                if (currentTabIndex == 0)
                {
                    paintTask(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                if (currentTabIndex == 2)
                {
                    paintSkill(g);
                }
                if (currentTabIndex == 3)
                {
                    if (mainTabName.Length == 4)
                    {
                        paintTools(g);
                    }
                    else
                    {
                        paintClans(g);
                    }
                }
                if (currentTabIndex == 4)
                {
                    paintTools(g);
                }
                break;
            case 2:
                if (currentTabIndex == 0)
                {
                    paintBox(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                break;
            case 3:
                paintZone(g);
                break;
            case 1:
                paintShop(g);
                break;
            case 25:
                paintSpeacialSkill(g);
                break;
            case 4:
                paintMap(g);
                break;
            case 7:
                paintInventory(g);
                break;
            case 17:
                paintShop(g);
                break;
            case 8:
                paintLogChat(g);
                break;
            case 10:
                paintPlayerMenu(g);
                break;
            case 11:
                paintFriend(g);
                break;
            case 16:
                paintEnemy(g);
                break;
            case 15:
                paintTop(g);
                break;
            case 12:
                if (currentTabIndex == 0)
                {
                    paintCombine(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                break;
            case 13:
                if (currentTabIndex == 0)
                {
                    if (Equals(GameCanvas.panel))
                    {
                        paintInventory(g);
                    }
                    else
                    {
                        paintGiaoDich(g, isMe: false);
                    }
                }
                if (currentTabIndex == 1)
                {
                    paintGiaoDich(g, isMe: true);
                }
                if (currentTabIndex == 2)
                {
                    paintGiaoDich(g, isMe: false);
                }
                break;
            case 14:
                paintMapTrans(g);
                break;
            case 18:
                paintFlagChange(g);
                break;
            case 19:
                paintOption(g);
                break;
            case 20:
                paintAccount(g);
                break;
            case 22:
                paintAuto(g);
                break;
            case 26:
                PaintModFunc(g);
                break;
            case 27:
                paintPlayerInfo(g);
                break;
        }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paint type " + type + ": " + ex.ToString());
        }
        GameScr.resetTranslate(g);
        try
        {
            paintDetail(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintDetail: " + ex.ToString());
        }
        if (cmx == cmtoX)
        {
            cmdClose.paint(g);
        }
        if (tabIcon != null && tabIcon.isShow)
        {
            tabIcon.paint(g);
        }
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.translate(X, Y);
        g.translate(-cmx, 0);
    }


    // ==================== paintScrollArrow ====================
    private void paintScrollArrow(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if ((cmy > 24 && currentListLength > 0) || (Equals(GameCanvas.panel) && typeShop == 2 && maxPageShop[currentTabIndex] > 1))
        {
            g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, xScroll + wScroll - 12, yScroll + 3, 0);
        }
        if ((cmy < cmyLim && currentListLength > 0) || (Equals(GameCanvas.panel) && typeShop == 2 && maxPageShop[currentTabIndex] > 1))
        {
            int arrowY = yScroll + hScroll - 8;
            g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, xScroll + wScroll - 12, arrowY, 0);
        }
    }


    // ==================== paintMyInfo ====================
    private void paintMyInfo(mGraphics g)
    {
        paintCharInfo(g, Char.myCharz());
    }


    // ==================== paintCharInfo ====================
    private void paintCharInfo(mGraphics g, Char c)
    {
        if (c == null)
        {
            return;
        }
        try
        {
            mFont.tahoma_7b_white.drawString(g, (c.isTichXanh ? "     " : string.Empty) + c.cName, X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
            if (c.isTichXanh)
            {
                ModFunc.PaintTicks(g, X + 60, 5);
            }
            if (c.cMaxStamina > 0 && GameScr.imgMP != null)
            {
                mFont.tahoma_7_yellow.drawString(g, mResources.vitality, X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
                if (GameScr.imgMPLost != null)
                {
                    g.drawImage(GameScr.imgMPLost, X + 95, 19, 0);
                }
                int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / c.cMaxStamina;
                g.setClip(95, X + 19, num, 20);
                g.drawImage(GameScr.imgMP, X + 95, 19, 0);
            }
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (c.cPower > 0)
            {
                mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
            }
            mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintCharInfo: " + ex.ToString());
        }
    }


    // ==================== getStatus ====================
    private string getStatus(int status)
    {
        return status switch
        {
            0 => mResources.follow,
            1 => mResources.defend,
            2 => mResources.attack,
            3 => mResources.gohome,
            _ => "aaa",
        };
    }


    // ==================== paintMultiLine1 ====================
    public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            string text = arr[i];
            if (text.StartsWith("c"))
            {
                if (text.StartsWith("c0"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_dark;
                }
                else if (text.StartsWith("c1"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_yellow;
                }
                else if (text.StartsWith("c2"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_green;
                }
            }
            if (i == 0)
            {
                f.drawString(g, text, x, y, align);
                continue;
            }
            if (i < indexRow + 30 && i > indexRow - 30)
            {
                f.drawString(g, text, x, y += 12, align);
            }
            else
            {
                y += 12;
            }
            yPaint += 12;
            indexRowMax++;
        }
    }


    // ==================== paintMultiLine2 ====================
    public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
    {
        int num = ((!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20);
        string[] array = f.splitFontArray(str, inforW - num);
        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0)
            {
                f.drawString(g, array[i], x, y, align);
                continue;
            }
            if (i < indexRow + 15 && i > indexRow - 15)
            {
                f.drawString(g, array[i], x, y += 12, align);
            }
            else
            {
                y += 12;
            }
            yPaint += 12;
            indexRowMax++;
        }
    }


}
