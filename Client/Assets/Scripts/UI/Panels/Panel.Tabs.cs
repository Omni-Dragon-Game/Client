using System;
using Assets.src.g;
using Mod;

/// <summary>
/// Quản lý việc hiển thị tab chính (paintTab) và tương tác với thanh Tab của Panel.
/// Được tách ra từ Panel.cs để tối ưu hóa kiến trúc, giúp dễ dàng chỉnh sửa giao diện Tab.
/// </summary>
public partial class Panel
{
    private void paintTab(mGraphics g)
    {
        try
        {
            if (currentTabName == null)
            {
                return;
            }
            if (type == 27)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, ModFunc.strPlayerInfo, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 23 || type == 24)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 20)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.account, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 22)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 19 || type == 26)
            {
                string s = type == 19 ? mResources.option : type == 26 ? ModFunc.strModFunc : ModFunc.strPlayerInfo;
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, s, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 18)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 13 && Equals(GameCanvas.panel2))
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 12 && GameCanvas.panel2 != null)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 11)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.friend, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 16)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.enemy, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 15)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, topName, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 2 && GameCanvas.panel2 != null)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.chest, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 9)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, xScroll + wScroll / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 3)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 14)
            {
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                mFont.tahoma_7b_dark.drawString(g, mResources.select_map, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                return;
            }
            if (type == 4)
            {
                mFont.tahoma_7b_dark.drawString(g, mResources.map, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                return;
            }
            if (type == 7)
            {
                mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                return;
            }
            if (type == 17)
            {
                mFont.tahoma_7b_dark.drawString(g, mResources.kigui, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                return;
            }
            if (type == 8)
            {
                mFont.tahoma_7b_dark.drawString(g, mResources.msg + ModFunc.strClickToChat, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                return;
            }
            if (type == 10)
            {
                mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, startTabPos + TAB_W / 2, 59, mFont.CENTER);
                g.setColor(UITheme.COLOR_ORANGE_LINE);
                g.fillRect(X + 1, 78, W - 2, 1);
                return;
            }
            if (currentTabIndex == 3 && mainTabName.Length == 5)
            {
                g.translate(-cmx, 0);
            }
            // 1. Vẽ các tab không được chọn (Inactive tabs)
            for (int i = 0; i < currentTabName.Length; i++)
            {
                if (i == currentTabIndex) continue;
                int tabX = startTabPos + i * TAB_W + 1;
                int tabW = TAB_W - 2;
                g.setColor(UITheme.COLOR_TAB_INACTIVE);
                PopUp.paintPopUp(g, tabX, 52, tabW, UITheme.MAIN_TAB_HEIGHT, 0, isButton: true);
                if (i == keyTouchTab)
                {
                    g.drawImage(ItemMap.imageFlare, startTabPos + i * TAB_W + TAB_W / 2, 62, 3);
                }
                mFont mFont2 = mFont.tahoma_7_grey;
                if (!currentTabName[i][1].Equals(string.Empty))
                {
                    mFont2.drawString(g, currentTabName[i][0], startTabPos + i * TAB_W + TAB_W / 2, 53, mFont.CENTER);
                    mFont2.drawString(g, currentTabName[i][1], startTabPos + i * TAB_W + TAB_W / 2, 64, mFont.CENTER);
                }
                else
                {
                    mFont2.drawString(g, currentTabName[i][0], startTabPos + i * TAB_W + TAB_W / 2, 59, mFont.CENTER);
                }
                if (type == 0 && currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0)
                {
                    g.drawImage(ItemMap.imageFlare, startTabPos + 3 * TAB_W + TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
            }
            // 2. Vẽ tab đang được chọn lên trên cùng (Active tab)
            {
                int tabX = startTabPos + currentTabIndex * TAB_W + 1;
                int tabW = TAB_W - 2;
                g.setColor(UITheme.COLOR_TAB_ACTIVE_GREEN);
                PopUp.paintPopUp(g, tabX, 52, tabW, UITheme.MAIN_TAB_HEIGHT, 1, isButton: true);
                if (currentTabIndex == keyTouchTab)
                {
                    g.drawImage(ItemMap.imageFlare, startTabPos + currentTabIndex * TAB_W + TAB_W / 2, 62, 3);
                }
                mFont mFont2 = mFont.tahoma_7_green2;
                if (!currentTabName[currentTabIndex][1].Equals(string.Empty))
                {
                    mFont2.drawString(g, currentTabName[currentTabIndex][0], startTabPos + currentTabIndex * TAB_W + TAB_W / 2, 53, mFont.CENTER);
                    mFont2.drawString(g, currentTabName[currentTabIndex][1], startTabPos + currentTabIndex * TAB_W + TAB_W / 2, 64, mFont.CENTER);
                }
                else
                {
                    mFont2.drawString(g, currentTabName[currentTabIndex][0], startTabPos + currentTabIndex * TAB_W + TAB_W / 2, 59, mFont.CENTER);
                }
            }
            g.setColor(UITheme.COLOR_ORANGE_LINE);
            g.fillRect(X + 1, 78, W - 2, 1);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTab: " + ex.ToString());
        }
    }
}
