using System;
using Assets.src.g;

public partial class Mob : IMapObject
{
    public bool isBigBoss()
    {
        return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
    }

    public void getData()
    {
        if (arrMobTemplate[templateId].data == null)
        {
            arrMobTemplate[templateId].data = new EffectData();
            string text = "/Mob/" + templateId;
            DataInputStream dataInputStream = MyStream.readFile(text);
            if (dataInputStream != null)
            {
                arrMobTemplate[templateId].data.readData(text + "/data");
                arrMobTemplate[templateId].data.img = GameCanvas.loadImage(text + "/img.png");
            }
            else
            {
                Service.gI().requestModTemplate(templateId);
            }
            if (lastMob.size() > 15)
            {
                arrMobTemplate[int.Parse((string)lastMob.elementAt(0))].data = null;
                lastMob.removeElementAt(0);
            }
            lastMob.addElement(templateId + string.Empty);
        }
        else
        {
            w = arrMobTemplate[templateId].data.width;
            h = arrMobTemplate[templateId].data.height;
        }
    }

    public virtual void setBody(short id)
    {
        changBody = true;
        smallBody = id;
    }

    public virtual void clearBody()
    {
        changBody = false;
    }

    public static bool isExistNewMob(string id)
    {
        for (int i = 0; i < newMob.size(); i++)
        {
            string text = (string)newMob.elementAt(i);
            if (text.Equals(id))
            {
                return true;
            }
        }
        return false;
    }

    public void checkData()
    {
        int num = 0;
        for (int i = 0; i < arrMobTemplate.Length; i++)
        {
            if (arrMobTemplate[i].data != null)
            {
                num++;
            }
        }
        if (num < 10)
        {
            return;
        }
        for (int j = 0; j < arrMobTemplate.Length; j++)
        {
            if (arrMobTemplate[j].data != null && num > 5)
            {
                arrMobTemplate[j].data = null;
            }
        }
    }

    public void checkFrameTick(int[] array)
    {
        if (tick > array.Length - 1)
        {
            tick = 0;
        }
        frame = array[tick];
        tick++;
    }


    private void paintShadow(mGraphics g)
    {
        int num = TileMap.size;
        if (TileMap.tileTypeAt(xSd + num / 2, ySd + 1, 4))
        {
            g.setClip(xSd / num * num, (ySd - 30) / num * num, num, 100);
        }
        else if (TileMap.tileTypeAt((xSd - num / 2) / num, (ySd + 1) / num) == 0)
        {
            g.setClip(xSd / num * num, (ySd - 30) / num * num, 100, 100);
        }
        else if (TileMap.tileTypeAt((xSd + num / 2) / num, (ySd + 1) / num) == 0)
        {
            g.setClip(xSd / num * num, (ySd - 30) / num * num, num, 100);
        }
        else if (TileMap.tileTypeAt(xSd - num / 2, ySd + 1, 8))
        {
            g.setClip(xSd / 24 * num, (ySd - 30) / num * num, num, 100);
        }
        g.drawImage(TileMap.bong, xSd, ySd, 3);
        g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
    }



    public void setInjure()
    {
        if (hp > 0 && status != 3 && status != 7)
        {
            timeStatus = 4;
            status = 7;
            if (getTemplate().type != 0 && Res.abs(x - xFirst) < 30)
            {
                x -= 10 * dir;
            }
        }
    }

    public static BigBoss getBigBoss()
    {
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = (Mob)GameScr.vMob.elementAt(i);
            if (mob is BigBoss)
            {
                return (BigBoss)mob;
            }
        }
        return null;
    }

    public static BigBoss2 getBigBoss2()
    {
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = (Mob)GameScr.vMob.elementAt(i);
            if (mob is BigBoss2)
            {
                return (BigBoss2)mob;
            }
        }
        return null;
    }

    public static BachTuoc getBachTuoc()
    {
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = (Mob)GameScr.vMob.elementAt(i);
            if (mob is BachTuoc)
            {
                return (BachTuoc)mob;
            }
        }
        return null;
    }

    public static NewBoss getNewBoss(sbyte idBoss)
    {
        Mob mob = (Mob)GameScr.vMob.elementAt(idBoss);
        if (mob is NewBoss)
        {
            return (NewBoss)mob;
        }
        return null;
    }

    public static void removeBigBoss()
    {
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = (Mob)GameScr.vMob.elementAt(i);
            if (mob is BigBoss)
            {
                GameScr.vMob.removeElement(mob);
                break;
            }
        }
    }

    public void setAttack(Char cFocus)
    {
        isBusyAttackSomeOne = true;
        mobToAttack = null;
        this.cFocus = cFocus;
        p1 = 0;
        p2 = 0;
        status = 3;
        tick = 0;
        dir = ((cFocus.cx > x) ? 1 : (-1));
        int cx = cFocus.cx;
        int cy = cFocus.cy;
        if (Res.abs(cx - x) < w * 2 && Res.abs(cy - y) < h * 2)
        {
            p3 = 0;
        }
        else
        {
            p3 = 1;
        }
    }

    private bool isSpecial()
    {
        if ((templateId >= 58 && templateId <= 65) || templateId == 67 || templateId == 68)
        {
            return true;
        }
        return false;
    }

    private bool isNewModStand()
    {
        return templateId == 76;
    }

    private bool isNewMod()
    {
        if (templateId >= 73 && !isNewModStand())
        {
            return true;
        }
        return false;
    }





    public MobTemplate getTemplate()
    {
        return arrMobTemplate[templateId];
    }

    public bool isPaint()
    {
        if (x < GameScr.cmx)
        {
            return false;
        }
        if (x > GameScr.cmx + GameScr.gW)
        {
            return false;
        }
        if (y < GameScr.cmy)
        {
            return false;
        }
        if (y > GameScr.cmy + GameScr.gH + 30)
        {
            return false;
        }
        if (arrMobTemplate[templateId] == null)
        {
            return false;
        }
        if (arrMobTemplate[templateId].data == null)
        {
            return false;
        }
        if (arrMobTemplate[templateId].data.img == null)
        {
            return false;
        }
        if (status == 0)
        {
            return false;
        }
        return true;
    }

    public bool isUpdate()
    {
        if (arrMobTemplate[templateId] == null)
        {
            return false;
        }
        if (arrMobTemplate[templateId].data == null)
        {
            return false;
        }
        if (status == 0)
        {
            return false;
        }
        return true;
    }

    public bool checkIsBoss()
    {
        if (isBoss || levelBoss > 0)
        {
            return true;
        }
        return false;
    }


    public virtual void paint(mGraphics g)
    {
        if (isHide)
        {
            return;
        }
        if (isMafuba)
        {
            if (!changBody)
            {
                arrMobTemplate[templateId].data.paintFrame(g, frame, xMFB, yMFB, (dir != 1) ? 1 : 0, 2);
            }
            else
            {
                SmallImage.drawSmallImage(g, smallBody, xMFB, yMFB, (dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
            }
            return;
        }
        if (isShadown && status != 0)
        {
            paintShadow(g);
        }
        if (!isPaint() || (status == 1 && p3 > 0 && GameCanvas.gameTick % 3 == 0))
        {
            return;
        }
        g.translate(0, GameCanvas.transY);
        if (!changBody)
        {
            arrMobTemplate[templateId].data.paintFrame(g, frame, x, y + fy, (dir != 1) ? 1 : 0, 2);
        }
        else
        {
            SmallImage.drawSmallImage(g, smallBody, x, y + fy - 9, (dir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER);
        }
        g.translate(0, -GameCanvas.transY);
        if (Char.myCharz().mobFocus == null || !Char.myCharz().mobFocus.Equals(this) || status == 1 || hp <= 0 || imgHPtem == null)
        {
            return;
        }
        int imageWidth = mGraphics.getImageWidth(imgHPtem);
        int imageHeight = mGraphics.getImageHeight(imgHPtem);
        int num = imageWidth * per / 100;
        int num2 = num;
        if (per_tem >= per)
        {
            num2 = imageWidth * (per_tem -= ((GameCanvas.gameTick % 6 <= 3) ? offset : offset++)) / 100;
            if (per_tem <= 0)
            {
                per_tem = 0;
            }
            if (per_tem < per)
            {
                per_tem = per;
            }
            if (offset >= 3)
            {
                offset = 3;
            }
        }
        g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - h - 5, mGraphics.TOP | mGraphics.LEFT);
        g.setColor(16777215);
        g.fillRect(x - (imageWidth >> 1), y - h - 5, num2, 2);
        g.drawRegion(imgHPtem, 0, 0, num, imageHeight, 0, x - (imageWidth >> 1), y - h - 5, mGraphics.TOP | mGraphics.LEFT);
    }

    public int getHPColor()
    {
        return 16711680;
    }

    public void startDie()
    {
        hp = 0;
        injureThenDie = true;
        hp = 0;
        status = 1;
        Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
        p1 = -3;
        p2 = -dir;
        p3 = 0;
    }

    public void attackOtherMob(Mob mobToAttack)
    {
        this.mobToAttack = mobToAttack;
        isBusyAttackSomeOne = true;
        cFocus = null;
        p1 = 0;
        p2 = 0;
        status = 3;
        tick = 0;
        dir = ((mobToAttack.x > x) ? 1 : (-1));
        int num = mobToAttack.x;
        int num2 = mobToAttack.y;
        if (Res.abs(num - x) < w * 2 && Res.abs(num2 - y) < h * 2)
        {
            if (x < num)
            {
                x = num - w;
            }
            else
            {
                x = num + w;
            }
            p3 = 0;
        }
        else
        {
            p3 = 1;
        }
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public int getH()
    {
        return h;
    }

    public int getW()
    {
        return w;
    }

    public void stopMoving()
    {
        if (status == 5)
        {
            status = 2;
            p1 = (p2 = (p3 = 0));
            forceWait = 50;
        }
    }

    public bool isInvisible()
    {
        return status == 0 || status == 1;
    }

    public void removeHoldEff()
    {
        if (holdEffID != 0)
        {
            holdEffID = 0;
        }
    }

    public void removeBlindEff()
    {
        blindEff = false;
    }

    public void removeSleepEff()
    {
        sleepEff = false;
    }

    public void GetFrame()
    {
        if (isGetFr && isTypeNewMod() && arrMobTemplate[templateId].data != null)
        {
            frameArr = (int[][])Controller.frameHT_NEWBOSS.get(templateId + string.Empty);
            stand = frameArr[0];
            move = frameArr[1];
            moveFast = frameArr[2];
            attack1 = frameArr[3];
            attack2 = frameArr[4];
            hurt = frameArr[5];
            isGetFr = false;
        }
    }

    private bool isTypeNewMod()
    {
        if (arrMobTemplate[templateId].data != null && arrMobTemplate[templateId].data.typeData == 2)
        {
            return true;
        }
        return false;
    }
}
