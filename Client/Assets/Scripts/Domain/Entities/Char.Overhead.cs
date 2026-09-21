using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- isOutX_createShadow ---
    public bool isOutX()
    {
        if (cx < GameScr.cmx)
        {
            return true;
        }
        if (cx > GameScr.cmx + GameScr.gW)
        {
            return true;
        }
        return false;
    }

    public bool isPaint()
    {
        if (cy < GameScr.cmy)
        {
            return false;
        }
        if (cy > GameScr.cmy + GameScr.gH + 30)
        {
            return false;
        }
        if (isOutX())
        {
            return false;
        }
        if (isSetPos)
        {
            return false;
        }
        if (isFusion)
        {
            return false;
        }
        return true;
    }

    public void createShadow(int x, int y, int life)
    {
        shadowX = x;
        shadowY = y;
        shadowLife = life;
    }

    // --- paint_map_line ---
    private void paint_map_line(mGraphics g)
    {
        if (isPaintNewSkill || x_hint == 0 || y_hint == 0 || statusMe == 14)
        {
            return;
        }
        int arg = 0;
        int x = cx - 30;
        int y = cy - 15;
        int num = -30;
        int num2 = 5;
        if (Res.abs(cy - y_hint) > 150)
        {
            if (cy > y_hint)
            {
                arg = 7;
                x = cx;
                y = cy - 15 - 60;
            }
            else
            {
                arg = 5;
                x = cx;
                y = cy - 15 + 60;
            }
        }
        else if (cx > x_hint)
        {
            arg = 2;
        }
        else if (cx <= x_hint)
        {
            x = cx + 30;
        }
        if (GameCanvas.gameTick % 10 >= 5)
        {
            if (Res.abs(cx - x_hint) > 100)
            {
                g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
            }
            else
            {
                g.drawImage(Panel.imgBantay, x_hint + num, y_hint - 60 + num2, 0);
            }
        }
    }

    // --- hp_name_shadow ---
    private void paintArrowAttack(mGraphics g)
    {
    }

    public void paintHp(mGraphics g, int x, int y)
    {
        long num = cHP * 100 / cHPFull / 10 - 1;
        if (num < 0)
        {
            num = 0;
        }
        if (num > 9)
        {
            num = 9;
        }
        if (!me)
        {
            g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y, 3);
        }
        if (cTypePk == 0 && (myCharz().cFlag == 0 || cFlag == 0 || (cFlag != 8 && myCharz().cFlag != 8 && cFlag == myCharz().cFlag)))
        {
            return;
        }
        len = (int)(cHP * 100L / cHPFull * w_hp_bar) / 100;
        num = (int)(cHP * 100L / cHPFull);
        if (num < 30)
        {
            imgHPtem = GameScr.imgHP_tm_do;
        }
        else if (num < 60)
        {
            imgHPtem = GameScr.imgHP_tm_vang;
        }
        else
        {
            imgHPtem = GameScr.imgHP_tm_xanh;
        }
        int imageWidth = mGraphics.getImageWidth(GameScr.imgHP_tm_do);
        int imageHeight = mGraphics.getImageHeight(GameScr.imgHP_tm_do);
        long w = imageWidth * num / 100;
        g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
        if (len < 5)
        {
            if (GameCanvas.gameTick % 6 < 3)
            {
                g.drawRegion(imgHPtem, 0, 0, (int)w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
            }
        }
        else
        {
            g.drawRegion(imgHPtem, 0, 0, (int)w, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
        }
    }

    public int getClassColor()
    {
        int result = 9145227;
        if (nClass.classId == 1 || nClass.classId == 2)
        {
            result = 16711680;
        }
        else if (nClass.classId == 3 || nClass.classId == 4)
        {
            result = 33023;
        }
        else if (nClass.classId == 5 || nClass.classId == 6)
        {
            result = 7443811;
        }
        return result;
    }

    public void paintNameInSameParty(mGraphics g)
    {
        if (cTypePk != 3 && cTypePk != 5 && isPaint())
        {
            if (myCharz().charFocus == null || !myCharz().charFocus.Equals(this))
            {
                mFont.tahoma_7_yellow.drawString(g, cName, cx, cy - ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
            }
            else if (myCharz().charFocus != null && myCharz().charFocus.Equals(this))
            {
                mFont.tahoma_7_yellow.drawString(g, cName, cx, cy - ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
            }
        }
    }

    private void PaintCharName_HP_MP_Overhead(mGraphics g)
    {
        Part part = GameScr.parts[getFHead(head)];
        int num = CharInfo[cf][0][2] - part.pi[CharInfo[cf][0][0]].dy + 5;
        if ((isInvisiblez && !me) || (!me && TileMap.mapID == 113 && cy >= 360))
        {
            return;
        }
        if (me)
        {
            num += 5;
            paintHp(g, cx, cy - num + 3);
            if (fraDanhHieu != null)
            {
                int x = cx - fraDanhHieu.frameWidth / 2;
                int y = cy - num + 3 - mFont.tahoma_7.getHeight() - (fraDanhHieu.frameHeight + 5);
                if (GameCanvas.gameTick % 5 == 0)
                {
                    danhHieuFramme++;
                }
                if (danhHieuFramme >= fraDanhHieu.nFrame)
                {
                    danhHieuFramme = 0;
                }
                fraDanhHieu.drawFrame(danhHieuFramme, x, y, 0, mGraphics.TOP | mGraphics.LEFT, g);
            }
            return;
        }
        bool isSameClan = myChar.clan != null && clanID == myChar.clan.ID;
        bool isPK = cTypePk == 3 || cTypePk == 5;
        bool isTrainning = cTypePk == 4;
        if (cName.StartsWith("$"))
        {
            cName = cName[1..];
            isPet = true;
        }
        if (cName.StartsWith("#"))
        {
            cName = cName[1..];
            isMiniPet = true;
        }
        if (myCharz().charFocus != null && myCharz().charFocus.Equals(this))
        {
            num += 5;
            paintHp(g, cx, cy - num + 3);
            if (fraDanhHieu != null)
            {
                int x2 = cx - fraDanhHieu.frameWidth / 2;
                int y2 = cy - num + 3 - mFont.tahoma_7.getHeight() - (fraDanhHieu.frameHeight + 5);
                if (GameCanvas.gameTick % 5 == 0)
                {
                    danhHieuFramme++;
                }
                if (danhHieuFramme >= fraDanhHieu.nFrame)
                {
                    danhHieuFramme = 0;
                }
                fraDanhHieu.drawFrame(danhHieuFramme, x2, y2, 0, mGraphics.TOP | mGraphics.LEFT, g);
            }
        }
        num += mFont.tahoma_7b_white.getHeight();
        mFont mFont2 = mFont.tahoma_7b_white;
        if (isPet)
        {
            mFont2 = mFont.tahoma_7_blue1Small;
        }
        else if (isMiniPet)
        {
            mFont2 = mFont.number_orange;
        }
        else if (isPK)
        {
            mFont2 = mFont.tahoma_7b_red;
        }
        else if (isTrainning)
        {
            mFont2 = mFont.tahoma_7b_yellow;
        }
        else if (isSameClan)
        {
            mFont2 = mFont.tahoma_7b_green;
        }
        int strLiength = mFont2.getWidth(cName);
        if ((paintName || isPK || isTrainning) && !isSameClan)
        {
            if (mSystem.clientType == 1)
            {
                mFont2.drawStringBorder(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
            }
            else if (charID == -83)
            {
                mFont2.drawStringBorder(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
            }
            else
            {
                mFont2.drawStringBorder(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
            }
            if (isTichXanh)
            {
                ModFunc.PaintTicks(g, cx + strLiength / 2, cy - num + 1);
            }
            num += mFont.tahoma_7.getHeight();
        }
        if (isSameClan) // Same clan
        {
            if (myCharz().charFocus != null && myCharz().charFocus.Equals(this))
            {
                mFont2.drawStringBorder(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
                if (isTichXanh)
                {
                    ModFunc.PaintTicks(g, cx + strLiength / 2, cy - num + 1);
                }
            }
            else if (charFocus == null)
            {
                mFont2.drawStringBorder(g, cName, cx - 10, cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
                if (isTichXanh)
                {
                    ModFunc.PaintTicks(g, cx + strLiength / 2 + 4, cy - num + 4);
                }
                //paintHp(g, cx - 16, cy - num + 10);
            }
        }
    }

    public void paintShadow(mGraphics g)
    {
        if (isMabuHold || head == 377 || leg == 471 || isTeleport || isFlyUp)
        {
            return;
        }
        int num = TileMap.size;
        if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128 && !TileMap.tileTypeAt(xSd + num / 2, ySd + 1, 4))
        {
            if (TileMap.tileTypeAt((xSd - num / 2) / num, (ySd + 1) / num) == 0)
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
        }
        g.drawImage(TileMap.bong, xSd, ySd, 3);
        g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
    }

    public void updateShadown()
    {
        int num = 0;
        xSd = cx;
        if (TileMap.tileTypeAt(cx, cy, 2))
        {
            ySd = cy;
            return;
        }
        ySd = cy;
        while (num < 30)
        {
            num++;
            ySd += 24;
            if (TileMap.tileTypeAt(xSd, ySd, 2))
            {
                if (ySd % 24 != 0)
                {
                    ySd -= ySd % 24;
                }
                break;
            }
        }
    }

    // --- isMagicTree ---
    public bool isMagicTree()
    {
        if (GameScr.gI().magicTree != null)
        {
            int x = GameScr.gI().magicTree.x;
            int y = GameScr.gI().magicTree.y;
            if (cx > x - 30 && cx < x + 30 && cy > y - 30 && cy < y + 30)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    // --- flagPK ---
    public bool isGetFlagImage(sbyte getFlag)
    {
        bool result = true;
        for (int i = 0; i < GameScr.vFlag.size(); i++)
        {
            PKFlag pKFlag = (PKFlag)GameScr.vFlag.elementAt(i);
            if (pKFlag != null)
            {
                if (pKFlag.cflag == getFlag)
                {
                    return true;
                }
                result = false;
            }
        }
        return result;
    }

    private void paintPKFlag(mGraphics g)
    {
        if (cdir == 1)
        {
            if (cFlag != 0 && cFlag != -1)
            {
                SmallImage.drawSmallImage(g, flagImage, cx - 10, cy - ch - ((!me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 2, 0);
            }
        }
        else if (cFlag != 0 && cFlag != -1)
        {
            SmallImage.drawSmallImage(g, flagImage, cx, cy - ch - ((!me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 0, 0);
        }
    }

    // --- setDanhHieu ---
    public void setDanhHieu(int smallDanhHieu, int frame)
    {
        smallDanhHieu = 0;
        frame = 1;
        if (mainImg == null)
        {
            mainImg = ImgByName.getImagePath("banner_" + 0, ImgByName.hashImagePath);
        }
        if (mainImg.img != null)
        {
            int num = mainImg.img.getHeight() / mainImg.nFrame;
            if (num < 1)
            {
                num = 1;
            }
            fraDanhHieu = new FrameImage(mainImg.img, mainImg.img.getWidth(), num);
        }
    }
}
