using System;

public partial class Panel
{
    // ==================== setTypeInfomatioin ====================
    public void setTypeInfomatioin()
    {
        type = 6;
        cmx = wScroll;
        cmtoX = 0;
    }


    // ==================== setTypeArchivement ====================
    public void setTypeArchivement()
    {
        currentListLength = Char.myCharz().arrArchive.Length;
        setType(0);
        type = 9;
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


    // ==================== setTabTask ====================
    private void setTabTask()
    {
        cmyQuest = 0;
    }


    // ==================== paintArchivement ====================
    private void paintArchivement(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (currentListLength == 0)
        {
            mFont.tahoma_7_green2.drawString(g, mResources.no_mission, xScroll + wScroll / 2, yScroll + hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
        }
        else
        {
            if (Char.myCharz().arrArchive == null || Char.myCharz().arrArchive.Length != currentListLength)
            {
                return;
            }
            for (int i = 0; i < currentListLength; i++)
            {
                int num = xScroll;
                int num2 = yScroll + i * ITEM_HEIGHT;
                int num3 = wScroll;
                int num4 = ITEM_HEIGHT - 1;
                Archivement archivement = Char.myCharz().arrArchive[i];
                g.setColor(i != selected ? 15196114 : 16383818);
                g.fillRect(num, num2, num3, num4);
                if (archivement == null)
                {
                    continue;
                }
                if (!archivement.isFinish)
                {
                    mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
                    mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
                    mFont.tahoma_7_green.drawString(g, archivement.money + " " + mResources.RUBY, num + num3 - 5, num2, mFont.RIGHT);
                }
                else if (archivement.isFinish && !archivement.isRecieve)
                {
                    mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
                    mFont.tahoma_7_blue.drawString(g, mResources.reward_mission + archivement.money + " " + mResources.RUBY, num + 5, num2 + 11, 0);
                    g.drawImage(i == selected ? GameScr.imgLbtnFocus2 : GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
                    mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
                }
                else if (archivement.isFinish && archivement.isRecieve)
                {
                    mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
                    mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
                    mFont.tahoma_7_green.drawString(g, mResources.received, num + num3 - 5, num2, mFont.RIGHT);
                }
            }
            paintScrollArrow(g);
        }
    }


    // ==================== paintInfomation ====================
    private void paintInfomation(mGraphics g)
    {
    }


    // ==================== paintTask ====================
    public void paintTask(mGraphics g)
    {
        try
        {
            int num = ((GameCanvas.h <= 300) ? 15 : 20);
            if (isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize())
            {
                g.drawImage((keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, xScroll + wScroll / 2, yScroll + hScroll - num, 3);
                mFont.tahoma_7b_dark.drawString(g, mResources.map, xScroll + wScroll / 2, yScroll + hScroll - (num + 5), mFont.CENTER);
            }
            xstart = xScroll + 5;
            ystart = yScroll + 14;
            yPaint = ystart;
            g.setClip(xScroll, yScroll, wScroll, hScroll - 35);
            if (scroll != null)
            {
                if (scroll.cmy > 0 && Mob.imgHP != null)
                {
                    g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, xScroll + wScroll - 12, yScroll + 3, 0);
                }
                if (scroll.cmy < scroll.cmyLim && Mob.imgHP != null)
                {
                    g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, xScroll + wScroll - 12, yScroll + hScroll - 45, 0);
                }
                g.translate(0, -scroll.cmy);
            }
            indexRowMax = 0;
            if (indexMenu == 0)
            {
                bool flag = false;
                if (Char.myCharz() != null && Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.names != null)
                {
                    for (int i = 0; i < Char.myCharz().taskMaint.names.Length; i++)
                    {
                        if (Char.myCharz().taskMaint.names[i] != null)
                        {
                            mFont.tahoma_7_grey.drawString(g, Char.myCharz().taskMaint.names[i], xScroll + wScroll / 2, yPaint - 5 + i * 12, mFont.CENTER);
                            indexRowMax++;
                        }
                    }
                    yPaint += (Char.myCharz().taskMaint.names.Length - 1) * 12;
                    int num2 = 0;
                    string empty = string.Empty;
                    if (Char.myCharz().taskMaint.subNames != null)
                    {
                        for (int j = 0; j < Char.myCharz().taskMaint.subNames.Length; j++)
                        {
                            if (Char.myCharz().taskMaint.subNames[j] != null)
                            {
                                num2 = j;
                                empty = "- " + Char.myCharz().taskMaint.subNames[j];
                                if (Char.myCharz().taskMaint.counts != null && j < Char.myCharz().taskMaint.counts.Length && Char.myCharz().taskMaint.counts[j] != -1)
                                {
                                    if (Char.myCharz().taskMaint.index == j)
                                    {
                                        if (Char.myCharz().taskMaint.counts[j] != 1)
                                        {
                                            string text = empty;
                                            empty = text + " (" + Char.myCharz().taskMaint.count + "/" + Char.myCharz().taskMaint.counts[j] + ")";
                                        }
                                        if (Char.myCharz().taskMaint.count == Char.myCharz().taskMaint.counts[j])
                                        {
                                            mFont.tahoma_7.drawString(g, empty, xstart + 5, yPaint += 12, 0);
                                        }
                                        else
                                        {
                                            mFont tahoma_7_grey = mFont.tahoma_7_grey;
                                            if (!flag)
                                            {
                                                flag = true;
                                                tahoma_7_grey = mFont.tahoma_7_blue;
                                                tahoma_7_grey.drawString(g, empty, xstart + 5 + ((tahoma_7_grey == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                            }
                                            else
                                            {
                                                tahoma_7_grey.drawString(g, "- ...", xstart + 5 + ((tahoma_7_grey == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                            }
                                        }
                                    }
                                    else if (Char.myCharz().taskMaint.index > j)
                                    {
                                        if (Char.myCharz().taskMaint.counts[j] != 1)
                                        {
                                            string text = empty;
                                            empty = text + " (" + Char.myCharz().taskMaint.counts[j] + "/" + Char.myCharz().taskMaint.counts[j] + ")";
                                        }
                                        mFont.tahoma_7_white.drawString(g, empty, xstart + 5, yPaint += 12, 0);
                                    }
                                    else
                                    {
                                        if (Char.myCharz().taskMaint.counts[j] != 1)
                                        {
                                            empty = empty + " 0/" + Char.myCharz().taskMaint.counts[j];
                                        }
                                        mFont tahoma_7_grey2 = mFont.tahoma_7_grey;
                                        if (!flag)
                                        {
                                            flag = true;
                                            tahoma_7_grey2 = mFont.tahoma_7_blue;
                                            tahoma_7_grey2.drawString(g, empty, xstart + 5 + ((tahoma_7_grey2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                        }
                                        else
                                        {
                                            tahoma_7_grey2.drawString(g, "- ...", xstart + 5 + ((tahoma_7_grey2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                        }
                                    }
                                }
                                else if (Char.myCharz().taskMaint.index > j)
                                {
                                    mFont.tahoma_7_white.drawString(g, empty, xstart + 5, yPaint += 12, 0);
                                }
                                else
                                {
                                    mFont tahoma_7_grey3 = mFont.tahoma_7_grey;
                                    if (!flag)
                                    {
                                        flag = true;
                                        tahoma_7_grey3 = mFont.tahoma_7_blue;
                                        tahoma_7_grey3.drawString(g, empty, xstart + 5 + ((tahoma_7_grey3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                    }
                                    else
                                    {
                                        tahoma_7_grey3.drawString(g, "- ...", xstart + 5 + ((tahoma_7_grey3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                                    }
                                }
                                indexRowMax++;
                            }
                            else if (Char.myCharz().taskMaint.index <= j && num2 < Char.myCharz().taskMaint.subNames.Length && Char.myCharz().taskMaint.subNames[num2] != null)
                            {
                                empty = "- " + Char.myCharz().taskMaint.subNames[num2];
                                mFont mFont2 = mFont.tahoma_7_grey;
                                if (!flag)
                                {
                                    flag = true;
                                    mFont2 = mFont.tahoma_7_blue;
                                }
                                mFont2.drawString(g, empty, xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), yPaint += 12, 0);
                            }
                        }
                    }
                    yPaint += 5;
                    if (Char.myCharz().taskMaint.details != null)
                    {
                        for (int k = 0; k < Char.myCharz().taskMaint.details.Length; k++)
                        {
                            if (Char.myCharz().taskMaint.details[k] != null)
                            {
                                mFont.tahoma_7_green2.drawString(g, Char.myCharz().taskMaint.details[k], xstart + 5, yPaint += 12, 0);
                                indexRowMax++;
                            }
                        }
                    }
                }
                else
                {
                    int taskMapId = GameScr.getTaskMapId();
                    sbyte taskNpcId = GameScr.getTaskNpcId();
                    string empty2 = string.Empty;
                    if (taskMapId == -3 || taskNpcId == -3)
                    {
                        empty2 = mResources.DES_TASK[3];
                    }
                    else if (Char.myCharz() != null && Char.myCharz().taskMaint == null && Char.myCharz().ctaskId == 9 && Char.myCharz().nClass != null && Char.myCharz().nClass.classId == 0)
                    {
                        empty2 = mResources.TASK_INPUT_CLASS;
                    }
                    else
                    {
                        if (taskNpcId < 0 || taskMapId < 0 || Npc.arrNpcTemplate == null || taskNpcId >= Npc.arrNpcTemplate.Length || Npc.arrNpcTemplate[taskNpcId] == null || TileMap.mapNames == null || taskMapId >= TileMap.mapNames.Length)
                        {
                            return;
                        }
                        empty2 = mResources.DES_TASK[0] + Npc.arrNpcTemplate[taskNpcId].name + mResources.DES_TASK[1] + TileMap.mapNames[taskMapId] + mResources.DES_TASK[2];
                    }
                    string[] array = mFont.tahoma_7_white.splitFontArray(empty2, 150);
                    for (int l = 0; l < array.Length; l++)
                    {
                        if (l == 0)
                        {
                            mFont.tahoma_7_white.drawString(g, array[l], xstart + 5, yPaint = ystart, 0);
                        }
                        else
                        {
                            mFont.tahoma_7_white.drawString(g, array[l], xstart + 5, yPaint += 12, 0);
                        }
                    }
                }
            }
            else if (indexMenu == 1)
            {
                yPaint = ystart - 12;
                if (Char.myCharz() != null && Char.myCharz().taskOrders != null)
                {
                    for (int m = 0; m < Char.myCharz().taskOrders.size(); m++)
                    {
                        TaskOrder taskOrder = (TaskOrder)Char.myCharz().taskOrders.elementAt(m);
                        if (taskOrder != null)
                        {
                            mFont.tahoma_7_white.drawString(g, taskOrder.name, xstart + 5, yPaint += 12, 0);
                            string mobName = (Mob.arrMobTemplate != null && taskOrder.killId >= 0 && taskOrder.killId < Mob.arrMobTemplate.Length && Mob.arrMobTemplate[taskOrder.killId] != null) ? Mob.arrMobTemplate[taskOrder.killId].name : "";
                            if (taskOrder.count == taskOrder.maxCount)
                            {
                                mFont.tahoma_7_white.drawString(g, ((taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL) + " " + mobName + " (" + taskOrder.count + "/" + taskOrder.maxCount + ")", xstart + 5, yPaint += 12, 0);
                            }
                            else
                            {
                                mFont.tahoma_7_blue.drawString(g, ((taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL) + " " + mobName + " (" + taskOrder.count + "/" + taskOrder.maxCount + ")", xstart + 5, yPaint += 12, 0);
                            }
                            indexRowMax += 3;
                            inforW = popupW - 25;
                            paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, xstart + 5, yPaint += 12, 0);
                            yPaint += 12;
                        }
                    }
                }
            }
            if (scroll == null)
            {
                scroll = new Scroll();
                scroll.setStyle(indexRowMax, 12, xScroll, yScroll, wScroll, hScroll - num - 40, styleUPDOWN: true, 1);
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTask: " + ex.ToString());
        }
    }


    // ==================== doFireArchivement ====================
    private void doFireArchivement()
    {
        if (selected >= 0 && Char.myCharz().arrArchive[selected].isFinish && !Char.myCharz().arrArchive[selected].isRecieve)
        {
            if (!GameCanvas.isTouch)
            {
                Service.gI().getArchivemnt(selected);
            }
            else if (GameCanvas.px > xScroll + wScroll - 40)
            {
                Service.gI().getArchivemnt(selected);
            }
        }
    }


}
