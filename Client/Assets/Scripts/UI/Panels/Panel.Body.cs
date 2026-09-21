using System;

public partial class Panel
{
    // ==================== setTypeBodyOnly ====================
    public void setTypeBodyOnly()
    {
        type = 7;
        setType(1);
        setTabBody(resetSelect: true);
        currentTabIndex = 0;
    }


    // ==================== setTabBody ====================
    private void setTabBody(bool resetSelect)
    {
        currentListLength = checkCurrentListLength(Char.myCharz().arrItemBody.Length);
        ITEM_HEIGHT = 29;
        cmyLim = (currentListLength - 1) * ITEM_HEIGHT - hScroll;
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


    // ==================== paintItemBodyBagInfo1 ====================
    private void paintItemBodyBagInfo(mGraphics g)
    {
        //mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + Char.myCharz().cHP + " / " + Char.myCharz().cHPFull, X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + Char.myCharz().cMP + " / " + Char.myCharz().cMPFull, X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + Char.myCharz().cDamFull, X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + Char.myCharz().cDefull + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
        // Sử dụng hàm định dạng trong việc vẽ chuỗi
        mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + formatLargeNumber(Char.myCharz().cHP) + " / " + formatLargeNumber(Char.myCharz().cHPFull), X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + formatLargeNumber(Char.myCharz().cMP) + " / " + formatLargeNumber(Char.myCharz().cMPFull), X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + formatLargeNumber(Char.myCharz().cDamFull), X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + formatLargeNumber(Char.myCharz().cDefull) + ", " + mResources.critical + ": " + formatLargeNumber(Char.myCharz().cCriticalFull) + "%", X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== paintItemBodyBagInfo2 ====================
    private void paintItemBodyBagInfo(mGraphics g, int x, int y)
    {
        //mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + Char.myCharz().cHP + " / " + Char.myCharz().cHPFull, x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + Char.myCharz().cMP + " / " + Char.myCharz().cMPFull, x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + Char.myCharz().cDamFull, x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + Char.myCharz().cDefull + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);

        mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + formatLargeNumber(Char.myCharz().cHP) + " / " + formatLargeNumber(Char.myCharz().cHPFull), x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + formatLargeNumber(Char.myCharz().cMP) + " / " + formatLargeNumber(Char.myCharz().cMPFull), x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + formatLargeNumber(Char.myCharz().cDamFull), x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + formatLargeNumber(Char.myCharz().cDefull) + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
    }


    // ==================== isCurrentTabBody ====================
    public bool isCurrentTabBody()
    {
        if (type == 7)
        {
            return true;
        }
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            return subTabInventory == 1;
        }
        return false;
    }


    // ==================== getBodySlotName ====================
    public static string getBodySlotName(int index)
    {
        return index switch
        {
            0 => "Áo",
            1 => "Quần",
            2 => "Găng tay",
            3 => "Giày",
            4 => "Rada",
            5 => "Cải trang",
            6 => "Giáp tập luyện",
            7 => "Phụ kiện",
            8 => "Bông tai",
            9 => "Thú cưỡi",
            10 => "Pet theo sau",
            11 => "Pet bay",
            12 => "Danh hiệu",
            13 => "Ngọc bội",
            14 => "Hào quang",
            _ => "Trang bị " + index
        };
    }


    // ==================== GetInventorySelect_isbody ====================
    private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
    {
        return isCurrentTabBody();
    }


    // ==================== GetInventorySelect_body ====================
    private int GetInventorySelect_body(int select, int subSelect)
    {
        return select - 1;
    }


    // ==================== GetInventorySelect_bag ====================
    private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
    {
        return select - 1;
    }


}
