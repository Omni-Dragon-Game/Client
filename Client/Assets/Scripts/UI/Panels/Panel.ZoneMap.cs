using System;
using UnityEngine;

public partial class Panel
{
    // ==================== getXMap ====================
    public int getXMap()
    {
        for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
        {
            if (TileMap.mapID == mapId[TileMap.planetID][i])
            {
                return mapX[TileMap.planetID][i];
            }
        }
        return -1;
    }


    // ==================== getYMap ====================
    public int getYMap()
    {
        for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
        {
            if (TileMap.mapID == mapId[TileMap.planetID][i])
            {
                return mapY[TileMap.planetID][i];
            }
        }
        return -1;
    }


    // ==================== getXMapTask ====================
    public int getXMapTask()
    {
        if (Char.myCharz().taskMaint == null)
        {
            return -1;
        }
        for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
        {
            if (GameScr.mapTasks[Char.myCharz().taskMaint.index] == mapId[TileMap.planetID][i])
            {
                return mapX[TileMap.planetID][i];
            }
        }
        return -1;
    }


    // ==================== getYMapTask ====================
    public int getYMapTask()
    {
        if (Char.myCharz().taskMaint == null)
        {
            return -1;
        }
        for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
        {
            if (GameScr.mapTasks[Char.myCharz().taskMaint.index] == mapId[TileMap.planetID][i])
            {
                return mapY[TileMap.planetID][i];
            }
        }
        return -1;
    }


    // ==================== setTypeMapTrans ====================
    public void setTypeMapTrans()
    {
        type = 14;
        setType(0);
        setTabMapTrans();
        cmx = (cmtoX = 0);
    }


    // ==================== setTypeMap ====================
    public void setTypeMap()
    {
        if (!GameScr.gI().isMapFize() && isPaintMap)
        {
            if (Hint.isOnTask(2, 0))
            {
                Hint.isViewMap = true;
                GameScr.info1.addInfo(mResources.go_to_quest, 0);
            }
            if (Hint.isOnTask(3, 0))
            {
                Hint.isViewPotential = true;
            }
            type = 4;
            currentTabName = tabName[type];
            startTabPos = xScroll + wScroll / 2 - currentTabName.Length * TAB_W / 2;
            cmx = (cmtoX = 0);
            setTabMap();
        }
    }


    // ==================== setTypeZone ====================
    public void setTypeZone()
    {
        type = 3;
        setType(0);
        setTabZone();
        cmx = (cmtoX = 0);
    }


    // ==================== updateKeyMap ====================
    private void updateKeyMap()
    {
        if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
        {
            yMove -= 5;
            cmyMap = yMove - (yScroll + hScroll / 2);
            if (yMove < yScroll)
            {
                yMove = yScroll;
            }
        }
        if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
        {
            yMove += 5;
            cmyMap = yMove - (yScroll + hScroll / 2);
            if (yMove > yScroll + 200)
            {
                yMove = yScroll + 200;
            }
        }
        if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
        {
            xMove -= 5;
            cmxMap = xMove - wScroll / 2;
            if (xMove < 16)
            {
                xMove = 16;
            }
        }
        if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
        {
            xMove += 5;
            cmxMap = xMove - wScroll / 2;
            if (xMove > 250)
            {
                xMove = 250;
            }
        }
        if (GameCanvas.isPointerDown)
        {
            pointerIsDowning = true;
            if (!trans)
            {
                pa1 = cmxMap;
                pa2 = cmyMap;
                trans = true;
            }
            cmxMap = pa1 + (GameCanvas.pxLast - GameCanvas.px);
            cmyMap = pa2 + (GameCanvas.pyLast - GameCanvas.py);
        }
        if (GameCanvas.isPointerJustRelease)
        {
            trans = false;
            GameCanvas.pxLast = GameCanvas.px;
            GameCanvas.pyLast = GameCanvas.py;
            pX = GameCanvas.pxLast + cmxMap;
            pY = GameCanvas.pyLast + cmyMap;
        }
        if (GameCanvas.isPointerClick)
        {
            pointerIsDowning = false;
        }
        if (cmxMap < 0)
        {
            cmxMap = 0;
        }
        if (cmxMap > cmxMapLim)
        {
            cmxMap = cmxMapLim;
        }
        if (cmyMap < 0)
        {
            cmyMap = 0;
        }
        if (cmyMap > cmyMapLim)
        {
            cmyMap = cmyMapLim;
        }
    }


    // ==================== setTabMapTrans ====================
    private void setTabMapTrans()
    {
        ITEM_HEIGHT = 29;
        currentListLength = mapNames.Length;
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


    // ==================== setTabZone ====================
    private void setTabZone()
    {
        ITEM_HEIGHT = 29;
        currentListLength = GameScr.gI().zones.Length;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        cmy = (cmtoY = 0);
        selected = (GameCanvas.isTouch ? (-1) : 0);
    }


    // ==================== setTabMap ====================
    private void setTabMap()
    {
        if (!isPaintMap)
        {
            return;
        }
        if (TileMap.lastPlanetId != TileMap.planetID)
        {
            Res.outz("LOAD TAM HINH");
            imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID + ".png");
            TileMap.lastPlanetId = TileMap.planetID;
        }
        cmxMap = getXMap() - wScroll / 2;
        cmyMap = getYMap() + yScroll - (yScroll + hScroll / 2);
        pa1 = cmxMap;
        pa2 = cmyMap;
        cmxMapLim = 250 - wScroll;
        cmyMapLim = 220 - hScroll;
        if (cmxMapLim < 0)
        {
            cmxMapLim = 0;
        }
        if (cmyMapLim < 0)
        {
            cmyMapLim = 0;
        }
        for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
        {
            if (TileMap.mapID == mapId[TileMap.planetID][i])
            {
                xMove = mapX[TileMap.planetID][i] + xScroll;
                yMove = mapY[TileMap.planetID][i] + yScroll + 5;
                break;
            }
        }
        xMap = getXMap() + xScroll;
        yMap = getYMap() + yScroll;
        xMapTask = getXMapTask() + xScroll;
        yMapTask = getYMapTask() + yScroll;
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }


    // ==================== paintMapTrans ====================
    private void paintMapTrans(mGraphics g)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < mapNames.Length; i++)
        {
            int num = xScroll + 29;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 29;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = ITEM_HEIGHT - 1;
            int num7 = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(xScroll, num2, wScroll, h);
                mFont.tahoma_7b_blue.drawString(g, mapNames[i], 5, num2 + 1, 0);
                mFont.tahoma_7_grey.drawString(g, planetNames[i], 5, num2 + 11, 0);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintZone ====================
    private void paintZone(mGraphics g)
    {
        g.setColor(16711680);
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        int[] zones = GameScr.gI().zones;
        int[] pts = GameScr.gI().pts;
        for (int i = 0; i < pts.Length; i++)
        {
            int num = xScroll + 29;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 29;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll;
            int y = yScroll + i * ITEM_HEIGHT;
            int num5 = 26;
            int h2 = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, h);
            g.setColor(zoneColor[pts[i]]);
            g.fillRect(num4, y, num5, h2);
            if (zones[i] != -1)
            {
                if (pts[i] != 1)
                {
                    mFont.tahoma_7_yellow.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
                }
                else
                {
                    mFont.tahoma_7_grey.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
                }
                mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i] + "/" + GameScr.gI().maxPlayer[i], num + 5, num2 + 6, 0);
            }
            if (GameScr.gI().rankName1[i] != null)
            {
                mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName1[i] + "(Top " + GameScr.gI().rank1[i] + ")", num + num3 - 2, num2 + 1, mFont.RIGHT);
                mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName2[i] + "(Top " + GameScr.gI().rank2[i] + ")", num + num3 - 2, num2 + 11, mFont.RIGHT);
            }
        }
        paintScrollArrow(g);
    }


    // ==================== paintZoneInfo ====================
    private void paintZoneInfo(mGraphics g)
    {
        mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, 25, 27, mFont.CENTER);
    }


    // ==================== paintMapInfo ====================
    private void paintMapInfo(mGraphics g)
    {
        mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[TileMap.planetID], 60, 4, mFont.LEFT);
        string text = string.Empty;
        if (TileMap.mapID >= 135 && TileMap.mapID <= 138)
        {
            text = " " + mResources.tang + TileMap.zoneID;
        }
        mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + text, 60, 16, mFont.LEFT);
        mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
        if (GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1)
        {
            mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
        }
        else
        {
            mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
        }
    }


    // ==================== paintMap ====================
    public void paintMap(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(-cmxMap, -cmyMap);
        g.drawImage(imgMap, xScroll, yScroll, 0);
        int head = Char.myCharz().head;
        Part part = GameScr.parts[head];
        SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, xMap, yMap + 5, 0, 3);
        int align = mFont.CENTER;
        if (xMap <= 40)
        {
            align = mFont.LEFT;
        }
        if (xMap >= 220)
        {
            align = mFont.RIGHT;
        }
        mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, xMap, yMap - 12, align, mFont.tahoma_7_grey);
        int num = -1;
        if (GameScr.getTaskMapId() != -1)
        {
            for (int i = 0; i < mapId[TileMap.planetID].Length; i++)
            {
                if (mapId[TileMap.planetID][i] == GameScr.getTaskMapId())
                {
                    num = i;
                    break;
                }
                num = 4;
            }
            if (GameCanvas.gameTick % 4 > 0)
            {
                g.drawImage(ItemMap.imageFlare, xScroll + mapX[TileMap.planetID][num], yScroll + mapY[TileMap.planetID][num], 3);
            }
        }
        if (!GameCanvas.isTouch)
        {
            g.drawImage(imgBantay, xMove, yMove, StaticObj.TOP_RIGHT);
            for (int j = 0; j < mapX[TileMap.planetID].Length; j++)
            {
                int num2 = mapX[TileMap.planetID][j] + xScroll;
                int num3 = mapY[TileMap.planetID][j] + yScroll;
                if (Res.inRect(num2 - 15, num3 - 15, 30, 30, xMove, yMove))
                {
                    align = mFont.CENTER;
                    if (num2 <= 20)
                    {
                        align = mFont.LEFT;
                    }
                    if (num2 >= 220)
                    {
                        align = mFont.RIGHT;
                    }
                    mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[mapId[TileMap.planetID][j]], num2, num3 - 12, align, mFont.tahoma_7_grey);
                    break;
                }
            }
        }
        else if (!trans)
        {
            for (int k = 0; k < mapX[TileMap.planetID].Length; k++)
            {
                int num4 = mapX[TileMap.planetID][k] + xScroll;
                int num5 = mapY[TileMap.planetID][k] + yScroll;
                if (Res.inRect(num4 - 15, num5 - 15, 30, 30, pX, pY))
                {
                    align = mFont.CENTER;
                    if (num4 <= 30)
                    {
                        align = mFont.LEFT;
                    }
                    if (num4 >= 220)
                    {
                        align = mFont.RIGHT;
                    }
                    g.drawImage(imgBantay, num4, num5, StaticObj.TOP_RIGHT);
                    mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[mapId[TileMap.planetID][k]], num4, num5 - 12, align, mFont.tahoma_7_grey);
                    break;
                }
            }
        }
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if (num != -1)
        {
            if (mapX[TileMap.planetID][num] + xScroll < cmxMap)
            {
                g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, xScroll + 5, yScroll + hScroll / 2 - 4, 0);
            }
            if (cmxMap + wScroll < mapX[TileMap.planetID][num] + xScroll)
            {
                g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, xScroll + wScroll - 5, yScroll + hScroll / 2 - 4, StaticObj.TOP_RIGHT);
            }
            if (mapY[TileMap.planetID][num] < cmyMap)
            {
                g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, xScroll + wScroll / 2, yScroll + 5, StaticObj.TOP_CENTER);
            }
            if (mapY[TileMap.planetID][num] > cmyMap + hScroll)
            {
                g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, xScroll + wScroll / 2, yScroll + hScroll - 5, StaticObj.BOTTOM_HCENTER);
            }
        }
    }


    // ==================== doFireMapTrans ====================
    private void doFireMapTrans()
    {
        doFireZone();
    }


    // ==================== doFireMap ====================
    private void doFireMap()
    {
        if (imgMap != null)
        {
            imgMap.texture = null;
            imgMap = null;
        }
        TileMap.lastPlanetId = -1;
        mSystem.gcc();
        SmallImage.loadBigRMS();
        setTypeMain();
        cmx = (cmtoX = 0);
    }


    // ==================== doFireZone ====================
    private void doFireZone()
    {
        if (selected != -1)
        {
            Res.outz("FIRE ZONE");
            isChangeZone = true;
            GameCanvas.panel.hide();
        }
    }


}
