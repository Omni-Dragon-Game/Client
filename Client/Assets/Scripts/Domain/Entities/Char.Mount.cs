using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // ==================== setMount ====================
    public void setMount(int cid, int ctrans, int cgender)
    {
        idcharMount = cid;
        transMount = ctrans;
        genderMount = cgender;
        speedMount = 30;
        if (transMount < 0)
        {
            transMount = 0;
            xMount = GameScr.cmx + GameCanvas.w + 50;
            dxMount = -19;
        }
        else if (transMount == 1)
        {
            transMount = 2;
            xMount = GameScr.cmx - 100;
            dxMount = -33;
        }
        dyMount = -17;
        yMount = cy;
        frameMount = 0;
        frameNewMount = 0;
        isMount = false;
        isEndMount = false;
    }


    // ==================== updateMount ====================
    public void updateMount()
    {
        frameMount++;
        if (frameMount > FrameMount.Length - 1)
        {
            frameMount = 0;
        }
        frameNewMount++;
        if (frameNewMount > 1000)
        {
            frameNewMount = 0;
        }
        if (isStartMount && !isMount)
        {
            yMount = cy;
            if (transMount == 0)
            {
                if (xMount - cx >= speedMount)
                {
                    xMount -= speedMount;
                    return;
                }
                xMount = cx;
                isMount = true;
                isEndMount = false;
            }
            else if (transMount == 2)
            {
                if (cx - xMount >= speedMount)
                {
                    xMount += speedMount;
                    return;
                }
                xMount = cx;
                isMount = true;
                isEndMount = false;
            }
        }
        else if (isMount)
        {
            if (statusMe == 14 || ySd - cy < 24)
            {
                setMountIsEnd();
            }
            if (cp1 % 15 < 5)
            {
                cf = 0;
            }
            else
            {
                cf = 1;
            }
            transMount = cdir;
            updateSuperEff();
            if (transMount < 0)
            {
                transMount = 0;
                dxMount = -19;
            }
            else if (transMount == 1)
            {
                transMount = 2;
                dxMount = -31;
                if (isEventMount)
                {
                    dxMount = -38;
                }
            }
            if (skillInfoPaint() != null)
            {
                dyMount = -15;
            }
            else
            {
                dyMount = -17;
            }
            yMount = cy;
            xMount = cx;
        }
        else if (isEndMount)
        {
            if (transMount == 0)
            {
                if (xMount > GameScr.cmx - 100)
                {
                    xMount -= 20;
                    return;
                }
                isStartMount = false;
                isMount = false;
                isEndMount = false;
            }
            else if (transMount == 2)
            {
                if (xMount < GameScr.cmx + GameCanvas.w + 50)
                {
                    xMount += 20;
                    return;
                }
                isStartMount = false;
                isMount = false;
                isEndMount = false;
            }
        }
        else if (!isStartMount || !isMount || !isEndMount)
        {
            xMount = GameScr.cmx - 100;
            yMount = GameScr.cmy - 100;
        }
    }


    // ==================== getMountData ====================
    public void getMountData()
    {
        if (Mob.arrMobTemplate[50].data == null)
        {
            Mob.arrMobTemplate[50].data = new EffectData();
            string text = "/Mob/" + 50;
            DataInputStream dataInputStream = null;
            dataInputStream = MyStream.readFile(text);
            if (dataInputStream != null)
            {
                Mob.arrMobTemplate[50].data.readData(text + "/data");
                Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
            }
            else
            {
                Service.gI().requestModTemplate(50);
            }
            Mob.lastMob.addElement(50 + string.Empty);
        }
    }


    // ==================== checkFrameTick ====================
    public void checkFrameTick(int[] array)
    {
        t++;
        if (t > array.Length - 1)
        {
            t = 0;
        }
        fM = array[t];
    }


    // ==================== paintMount1 ====================
    public void paintMount1(mGraphics g)
    {
        if (xMount <= GameScr.cmx || xMount >= GameScr.cmx + GameCanvas.w)
        {
            return;
        }
        if (me)
        {
            if (!isEndMount && !isStartMount && !isMount)
            {
                return;
            }
            if (idMount >= ID_NEW_MOUNT)
            {
                string nameImg = strMount + (idMount - ID_NEW_MOUNT) + "_0";
                FrameImage fraImage = mSystem.getFraImage(nameImg);
                fraImage?.drawFrame(frameNewMount / 2 % fraImage.nFrame, xMount, yMount + fy, transMount, 3, g);
            }
            else
            {
                if (isSpeacialMount)
                {
                    return;
                }
                if (isEventMount)
                {
                    g.drawRegion(imgEventMountWing, 0, FrameMount[frameMount] * 60, 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else if (genderMount == 2)
                {
                    if (!isMountVip)
                    {
                        g.drawRegion(imgMount_XD, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                    }
                    else
                    {
                        g.drawRegion(imgMount_XD_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                    }
                }
                else if (genderMount == 1)
                {
                    if (!isMountVip)
                    {
                        g.drawRegion(imgMount_NM, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                    }
                    else
                    {
                        g.drawRegion(imgMount_NM_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                    }
                }
            }
        }
        else
        {
            if (me)
            {
                return;
            }
            if (idMount >= ID_NEW_MOUNT)
            {
                string nameImg2 = strMount + (idMount - ID_NEW_MOUNT) + "_0";
                FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
                fraImage2?.drawFrame(frameNewMount / 2 % fraImage2.nFrame, xMount, yMount + fy, transMount, 3, g);
            }
            else
            {
                if (isSpeacialMount)
                {
                    return;
                }
                if (isEventMount)
                {
                    g.drawRegion(imgEventMountWing, 0, FrameMount[frameMount] * 60, 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else
                {
                    if (!isMount)
                    {
                        return;
                    }
                    if (genderMount == 2)
                    {
                        if (!isMountVip)
                        {
                            g.drawRegion(imgMount_XD, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                        }
                        else
                        {
                            g.drawRegion(imgMount_XD_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                        }
                    }
                    else if (genderMount == 1)
                    {
                        if (!isMountVip)
                        {
                            g.drawRegion(imgMount_NM, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                        }
                        else
                        {
                            g.drawRegion(imgMount_NM_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                        }
                    }
                }
            }
        }
    }


    // ==================== paintMount2 ====================
    public void paintMount2(mGraphics g)
    {
        if (xMount <= GameScr.cmx || xMount >= GameScr.cmx + GameCanvas.w)
        {
            return;
        }
        if (me)
        {
            if (!isEndMount && !isStartMount && !isMount)
            {
                return;
            }
            if (idMount >= ID_NEW_MOUNT)
            {
                string nameImg = strMount + (idMount - ID_NEW_MOUNT) + "_1";
                FrameImage fraImage = mSystem.getFraImage(nameImg);
                fraImage?.drawFrame(frameNewMount / 2 % fraImage.nFrame, xMount, yMount + fy, transMount, 3, g);
            }
            else if (isSpeacialMount)
            {
                checkFrameTick(move);
                if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
                {
                    Mob.arrMobTemplate[50].data.paintFrame(g, fM, xMount + ((cdir != 1) ? 8 : (-8)), yMount + 35, (cdir != 1) ? 1 : 0, 0);
                }
                else
                {
                    getMountData();
                }
            }
            else if (isEventMount)
            {
                g.drawRegion(imgEventMount, 0, FrameMount[frameMount] * 60, 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
            }
            else if (genderMount == 0)
            {
                if (!isMountVip)
                {
                    g.drawRegion(imgMount_TD, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else
                {
                    g.drawRegion(imgMount_TD_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
            }
            else if (genderMount == 1)
            {
                if (!isMountVip)
                {
                    g.drawRegion(imgMount_NM_1, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else
                {
                    g.drawRegion(imgMount_NM_1_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
            }
        }
        else
        {
            if (me)
            {
                return;
            }
            if (idMount >= ID_NEW_MOUNT)
            {
                string nameImg2 = strMount + (idMount - ID_NEW_MOUNT) + "_1";
                FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
                fraImage2?.drawFrame(frameNewMount / 2 % fraImage2.nFrame, xMount, yMount + fy, transMount, 3, g);
                return;
            }
            if (isSpeacialMount)
            {
                checkFrameTick(move);
                if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
                {
                    Mob.arrMobTemplate[50].data.paintFrame(g, fM, xMount + ((cdir != 1) ? 8 : (-8)), yMount + 35, (cdir != 1) ? 1 : 0, 0);
                }
                else
                {
                    getMountData();
                }
                return;
            }
            if (isEventMount)
            {
                g.drawRegion(imgEventMount, 0, FrameMount[frameMount] * 60, 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
            }
            if (!isMount)
            {
                return;
            }
            if (genderMount == 0)
            {
                if (!isMountVip)
                {
                    g.drawRegion(imgMount_TD, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else
                {
                    g.drawRegion(imgMount_TD_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
            }
            else if (genderMount == 1)
            {
                if (!isMountVip)
                {
                    g.drawRegion(imgMount_NM_1, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
                else
                {
                    g.drawRegion(imgMount_NM_1_VIP, 0, FrameMount[frameMount] * 40, 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
                }
            }
        }
    }


    // ==================== setMountIsStart ====================
    public void setMountIsStart()
    {
        if (me)
        {
            isHaveMount = checkHaveMount();
            if (TileMap.isVoDaiMap())
            {
                isHaveMount = false;
            }
        }
        if (isHaveMount)
        {
            if (ySd - cy <= 20)
            {
                xChar = cx;
            }
            if (xdis < 100)
            {
                xdis = Res.abs(xChar - cx);
            }
            if (xdis >= 70 && ySd - cy > 30 && !isStartMount && !isEndMount)
            {
                setMount(charID, cdir, cgender);
                isStartMount = true;
            }
        }
    }


    // ==================== setMountIsEnd ====================
    public void setMountIsEnd()
    {
        if (ySd - cy < 24 && !isEndMount)
        {
            isStartMount = false;
            isMount = false;
            isEndMount = true;
            xdis = 0;
        }
    }


    // ==================== checkHaveMount ====================
    public bool checkHaveMount()
    {
        bool result = false;
        short num = -1;
        Item[] array = arrItemBody;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null && (array[i].template.type == 24 || array[i].template.type == 23))
            {
                num = ((array[i].template.part < 0) ? array[i].template.id : ((short)(ID_NEW_MOUNT + array[i].template.part)));
                result = true;
                break;
            }
        }
        isMountVip = false;
        isSpeacialMount = false;
        isEventMount = false;
        idMount = -1;
        switch (num)
        {
            case 349:
            case 350:
            case 351:
                isMountVip = true;
                break;
            case 396:
                isEventMount = true;
                break;
            case 532:
                isSpeacialMount = true;
                break;
            default:
                if (num >= ID_NEW_MOUNT)
                {
                    idMount = num;
                }
                break;
        }
        return result;
    }


}
