using System;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class GameScr
{
    // --- checkCharFocus ---
    public void checkCharFocus()
    {
    }

    // --- targetArrows_npcSearch ---
    public void checkEffToObj(IMapObject obj, bool isnew)
    {
        if (obj == null || tDoubleDelay > 0)
        {
            return;
        }
        tDoubleDelay = 10;
        int x = obj.getX();
        int num = 1;
        int num2 = Res.abs(Char.myCharz().cx - x);
        num = ((num2 <= 80) ? 1 : ((num2 > 80 && num2 <= 200) ? 2 : ((num2 <= 200 || num2 > 400) ? 4 : 3)));
        if (!isnew)
        {
            if (obj.Equals(Char.myCharz().mobFocus) || (obj.Equals(Char.myCharz().charFocus) && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus)))
            {
                ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), num);
            }
            else if (obj.Equals(Char.myCharz().npcFocus) || obj.Equals(Char.myCharz().itemFocus) || obj.Equals(Char.myCharz().charFocus))
            {
                ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num);
            }
        }
        else
        {
            ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num);
        }
    }

    private void updateClickToArrow()
    {
        if (tDoubleDelay > 0)
        {
            tDoubleDelay--;
        }
        if (clickMoving)
        {
            clickMoving = false;
            IMapObject mapObject = findClickToItem(clickToX, clickToY);
            if (mapObject == null || (mapObject != null && mapObject.Equals(Char.myCharz().npcFocus) && TileMap.mapID == 51))
            {
                ServerEffect.addServerEffect(134, clickToX, clickToY + GameCanvas.transY / 2, 3);
            }
        }
    }

    private void paintWaypointArrow(mGraphics g)
    {
        int num = 10;
        Task taskMaint = Char.myCharz().taskMaint;
        if (taskMaint != null && taskMaint.taskId == 0 && ((taskMaint.index != 1 && taskMaint.index < 6) || taskMaint.index == 0))
        {
            return;
        }
        for (int i = 0; i < TileMap.vGo.size(); i++)
        {
            Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
            if (waypoint.minY == 0 || waypoint.maxY >= TileMap.pxh - 24)
            {
                if (waypoint.maxY <= TileMap.pxh / 2)
                {
                    int x = waypoint.minX + (waypoint.maxX - waypoint.minX) / 2;
                    int y = waypoint.minY + (waypoint.maxY - waypoint.minY) / 2 + runArrow;
                    if (GameCanvas.isTouch)
                    {
                        y = waypoint.maxY + (waypoint.maxY - waypoint.minY) + runArrow + num;
                    }
                    g.drawRegion(arrow, 0, 0, 13, 16, 6, x, y, StaticObj.VCENTER_HCENTER);
                }
                else if (waypoint.minY >= TileMap.pxh / 2)
                {
                    g.drawRegion(arrow, 0, 0, 13, 16, 4, waypoint.minX + (waypoint.maxX - waypoint.minX) / 2, waypoint.minY - 12 - runArrow, StaticObj.VCENTER_HCENTER);
                }
            }
            else if (waypoint.minX >= 0 && waypoint.minX < 24)
            {
                if (!GameCanvas.isTouch)
                {
                    g.drawRegion(arrow, 0, 0, 13, 16, 2, waypoint.maxX + 12 + runArrow, waypoint.maxY - 12, StaticObj.VCENTER_HCENTER);
                }
                else
                {
                    g.drawRegion(arrow, 0, 0, 13, 16, 2, waypoint.maxX + 12 + runArrow, waypoint.maxY - 32, StaticObj.VCENTER_HCENTER);
                }
            }
            else if (waypoint.minX <= TileMap.tmw * 24 && waypoint.minX >= TileMap.tmw * 24 - 48)
            {
                if (!GameCanvas.isTouch)
                {
                    g.drawRegion(arrow, 0, 0, 13, 16, 0, waypoint.minX - 12 - runArrow, waypoint.maxY - 12, StaticObj.VCENTER_HCENTER);
                }
                else
                {
                    g.drawRegion(arrow, 0, 0, 13, 16, 0, waypoint.minX - 12 - runArrow, waypoint.maxY - 32, StaticObj.VCENTER_HCENTER);
                }
            }
            else
            {
                g.drawRegion(arrow, 0, 0, 13, 16, 4, waypoint.minX + (waypoint.maxX - waypoint.minX) / 2, waypoint.maxY - 48 - runArrow, StaticObj.VCENTER_HCENTER);
            }
        }
    }

    public static Npc findNPCInMap(short id)
    {
        for (int i = 0; i < vNpc.size(); i++)
        {
            Npc npc = (Npc)vNpc.elementAt(i);
            if (npc.template.npcTemplateId == id)
            {
                return npc;
            }
        }
        return null;
    }

    public static Char findCharInMap(int charId)
    {
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char @char = (Char)vCharInMap.elementAt(i);
            if (@char.charID == charId)
            {
                return @char;
            }
        }
        return null;
    }

    public static Mob findMobInMap(sbyte mobIndex)
    {
        return (Mob)vMob.elementAt(mobIndex);
    }

    public static Mob findMobInMap(int mobId)
    {
        for (int i = 0; i < vMob.size(); i++)
        {
            Mob mob = (Mob)vMob.elementAt(i);
            if (mob.mobId == mobId)
            {
                return mob;
            }
        }
        return null;
    }

    public static Npc getNpcTask()
    {
        for (int i = 0; i < vNpc.size(); i++)
        {
            Npc npc = (Npc)vNpc.elementAt(i);
            if (npc.template.npcTemplateId == getTaskNpcId())
            {
                return npc;
            }
        }
        return null;
    }

    private void paintArrowPointToNPC(mGraphics g)
    {
        try
        {
            if (ChatPopup.currChatPopup != null)
            {
                return;
            }
            int num = getTaskNpcId();
            if (num == -1)
            {
                return;
            }
            Npc npc = null;
            for (int i = 0; i < vNpc.size(); i++)
            {
                Npc npc2 = (Npc)vNpc.elementAt(i);
                if (npc2.template.npcTemplateId == num)
                {
                    if (npc == null)
                    {
                        npc = npc2;
                    }
                    else if (Res.abs(npc2.cx - Char.myCharz().cx) < Res.abs(npc.cx - Char.myCharz().cx))
                    {
                        npc = npc2;
                    }
                }
            }
            if (npc == null || npc.statusMe == 15 || (npc.cx > cmx && npc.cx < cmx + gW && npc.cy > cmy && npc.cy < cmy + gH) || GameCanvas.gameTick % 10 < 5)
            {
                return;
            }
            int num2 = npc.cx - Char.myCharz().cx;
            int num3 = npc.cy - Char.myCharz().cy;
            int x = 0;
            int y = 0;
            int arg = 0;
            if (num2 > 0 && num3 >= 0)
            {
                if (Res.abs(num2) >= Res.abs(num3))
                {
                    x = gW - 10;
                    y = gH / 2 + 30;
                    if (GameCanvas.isTouch)
                    {
                        y = gH / 2 + 10;
                    }
                    arg = 0;
                }
                else
                {
                    x = gW / 2;
                    y = gH - 10;
                    arg = 5;
                }
            }
            else if (num2 >= 0 && num3 < 0)
            {
                if (Res.abs(num2) >= Res.abs(num3))
                {
                    x = gW - 10;
                    y = gH / 2 + 30;
                    if (GameCanvas.isTouch)
                    {
                        y = gH / 2 + 10;
                    }
                    arg = 0;
                }
                else
                {
                    x = gW / 2;
                    y = 10;
                    arg = 6;
                }
            }
            if (num2 < 0 && num3 >= 0)
            {
                if (Res.abs(num2) >= Res.abs(num3))
                {
                    x = 10;
                    y = gH / 2 + 30;
                    if (GameCanvas.isTouch)
                    {
                        y = gH / 2 + 10;
                    }
                    arg = 3;
                }
                else
                {
                    x = gW / 2;
                    y = gH - 10;
                    arg = 5;
                }
            }
            else if (num2 <= 0 && num3 < 0)
            {
                if (Res.abs(num2) >= Res.abs(num3))
                {
                    x = 10;
                    y = gH / 2 + 30;
                    if (GameCanvas.isTouch)
                    {
                        y = gH / 2 + 10;
                    }
                    arg = 3;
                }
                else
                {
                    x = gW / 2;
                    y = 10;
                    arg = 6;
                }
            }
            resetTranslate(g);
            g.drawRegion(arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
        }
    }

    // --- injure_vsCombat ---
    public void getInjure()
    {
    }

    public void starVS()
    {
        curr = (last = mSystem.currentTimeMillis());
        secondVS = 180;
    }

    private Char findCharVS1()
    {
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char @char = (Char)vCharInMap.elementAt(i);
            if (@char.cTypePk != 0)
            {
                return @char;
            }
        }
        return null;
    }

    private Char findCharVS2()
    {
        for (int i = 0; i < vCharInMap.size(); i++)
        {
            Char @char = (Char)vCharInMap.elementAt(i);
            if (@char.cTypePk != 0 && @char != findCharVS1())
            {
                return @char;
            }
        }
        return null;
    }

    private void paintInfoBar(mGraphics g)
    {
        resetTranslate(g);
        if (TileMap.mapID == 130 && findCharVS1() != null && findCharVS2() != null)
        {
            g.translate(GameCanvas.w / 2 - 62, 0);
            paintImageBar(g, isLeft: true, findCharVS1());
            g.translate(-(GameCanvas.w / 2 - 65), 0);
            paintImageBarRight(g, findCharVS2());
            findCharVS1().paintHeadWithXY(g, 137, 25, 0);
            findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
        }
        else if (isVS() && Char.myCharz().charFocus != null)
        {
            g.translate(GameCanvas.w / 2 - 62, 0);
            paintImageBar(g, isLeft: true, Char.myCharz().charFocus);
            g.translate(-(GameCanvas.w / 2 - 65), 0);
            paintImageBarRight(g, Char.myCharz());
            Char.myCharz().paintHeadWithXY(g, 137, 25, 0);
            Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
        }
        else if (ispaintPhubangBar() && isSmallScr())
        {
            paintHPBar_NEW(g, 1, 1, Char.myCharz());
        }
        else
        {
            paintImageBar(g, isLeft: true, Char.myCharz());
            if (Char.myCharz().isInEnterOfflinePoint() != null || Char.myCharz().isInEnterOnlinePoint() != null)
            {
                mFont.tahoma_7_green2.drawString(g, mResources.enter, imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
            }
            else if (Char.myCharz().mobFocus != null)
            {
                if (Char.myCharz().mobFocus.getTemplate() != null)
                {
                    mFont.tahoma_7b_green2.drawString(g, Char.myCharz().mobFocus.getTemplate().name, imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                }
                if (Char.myCharz().mobFocus.templateId != 0)
                {
                    mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().mobFocus.hp) + string.Empty, imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                }
            }
            else if (Char.myCharz().npcFocus != null)
            {
                mFont.tahoma_7b_green2.drawString(g, Char.myCharz().npcFocus.template.name, imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                if (Char.myCharz().npcFocus.template.npcTemplateId == 4)
                {
                    mFont.tahoma_7b_green2.drawString(g, gI().magicTree.currPeas + "/" + gI().magicTree.maxPeas, imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                }
            }
            else if (Char.myCharz().charFocus != null)
            {
                mFont.tahoma_7b_green2.drawString(g, Char.myCharz().charFocus.cName, imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().charFocus.cHP) + string.Empty, imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
            }
            else
            {
                mFont.tahoma_7b_green2.drawString(g, Char.myCharz().cName, imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
                mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cPower) + string.Empty, imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
            }
        }
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if (isVS() && secondVS > 0)
        {
            curr = mSystem.currentTimeMillis();
            if (curr - last >= 1000)
            {
                last = mSystem.currentTimeMillis();
                secondVS--;
            }
            mFont.tahoma_7b_white.drawString(g, secondVS + string.Empty, GameCanvas.w / 2, 13, 2, mFont.tahoma_7b_dark);
        }
        if (flareFindFocus)
        {
            g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
            flareTime--;
            if (flareTime < 0)
            {
                flareTime = 0;
                flareFindFocus = false;
            }
        }
    }

    public bool isVS()
    {
        if (TileMap.isVoDaiMap() && (Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && findCharVS1() != null && findCharVS2() != null)))
        {
            return true;
        }
        return false;
    }

    // --- pvp_trade_flags ---
    public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
    {
        Char @char = findCharInMap(playerId);
        if (@char != null)
        {
            if (typePK == 3)
            {
                startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
            }
            if (typePK == 4)
            {
                startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
            }
        }
    }

    public void giaodich(int playerID)
    {
        Char @char = findCharInMap(playerID);
        if (@char != null)
        {
            startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
        }
    }

    public void getFlagImage(int charID, sbyte cflag)
    {
        if (vFlag.size() == 0)
        {
            Service.gI().getFlag(2, cflag);
            return;
        }
        if (charID == Char.myCharz().charID)
        {
            if (Char.myCharz().isGetFlagImage(cflag))
            {
                for (int i = 0; i < vFlag.size(); i++)
                {
                    PKFlag pKFlag = (PKFlag)vFlag.elementAt(i);
                    if (pKFlag != null && pKFlag.cflag == cflag)
                    {
                        Char.myCharz().flagImage = pKFlag.IDimageFlag;
                    }
                }
            }
            else if (!Char.myCharz().isGetFlagImage(cflag))
            {
                Service.gI().getFlag(2, cflag);
            }
            return;
        }
        if (findCharInMap(charID) == null)
        {
            return;
        }
        if (findCharInMap(charID).isGetFlagImage(cflag))
        {
            for (int j = 0; j < vFlag.size(); j++)
            {
                PKFlag pKFlag2 = (PKFlag)vFlag.elementAt(j);
                if (pKFlag2 != null && pKFlag2.cflag == cflag)
                {
                    findCharInMap(charID).flagImage = pKFlag2.IDimageFlag;
                }
            }
        }
        else if (!findCharInMap(charID).isGetFlagImage(cflag))
        {
            Service.gI().getFlag(2, cflag);
        }
    }
}
