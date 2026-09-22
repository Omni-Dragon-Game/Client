using System;
using Assets.src.e;
using Assets.src.g;
using UnityEngine;

public partial class Mob : IMapObject
{
    private void updateShadown()
    {
        int num = TileMap.size;
        xSd = x;
        wCount = 0;
        if (ySd <= 0 || TileMap.tileTypeAt(xSd, ySd, 2))
        {
            return;
        }
        if (TileMap.tileTypeAt(xSd / num, ySd / num) == 0)
        {
            isOutMap = true;
        }
        else if (TileMap.tileTypeAt(xSd / num, ySd / num) != 0 && !TileMap.tileTypeAt(xSd, ySd, 2))
        {
            xSd = x;
            ySd = y;
            isOutMap = false;
        }
        while (isOutMap && wCount < 10)
        {
            wCount++;
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

    public void updateSuperEff()
    {
        if (typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0)
        {
            ServerEffect.addServerEffect(114, this, 1);
        }
        if (typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0)
        {
            ServerEffect.addServerEffect(132, this, 1);
        }
        if (typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0)
        {
            ServerEffect.addServerEffect(131, this, 1);
        }
    }

    public virtual void update()
    {
        if (isMafuba)
        {
            return;
        }
        GetFrame();
        if (blindEff && GameCanvas.gameTick % 5 == 0)
        {
            ServerEffect.addServerEffect(113, x, y, 1);
        }
        if (sleepEff && GameCanvas.gameTick % 10 == 0)
        {
            EffecMn.addEff(new Effect(41, x, y, 3, 1, 1));
        }
        if (!GameCanvas.lowGraphic && status != 1 && status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + mobId * 2) == 0)
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char != null && @char.isFlyAndCharge && @char.cf == 32)
                {
                    Char char2 = new Char();
                    char2.cx = @char.cx;
                    char2.cy = @char.cy - @char.ch;
                    if (@char.cgender == 0)
                    {
                        MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), -100, -100, char2, 25);
                    }
                }
            }
            if (Char.myCharz().isFlyAndCharge && Char.myCharz().cf == 32)
            {
                Char char3 = new Char();
                char3.cx = Char.myCharz().cx;
                char3.cy = Char.myCharz().cy - Char.myCharz().ch;
                if (Char.myCharz().cgender == 0)
                {
                    MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), -100, -100, char3, 25);
                }
            }
        }
        if (holdEffID != 0 && GameCanvas.gameTick % 5 == 0)
        {
            EffecMn.addEff(new Effect(holdEffID, x, y + 24, 3, 5, 1));
        }
        if (isFreez)
        {
            if (GameCanvas.gameTick % 5 == 0)
            {
                ServerEffect.addServerEffect(113, x, y, 1);
            }
            long num = mSystem.currentTimeMillis();
            if (num - last >= 1000)
            {
                seconds--;
                last = num;
                if (seconds < 0)
                {
                    isFreez = false;
                    seconds = 0;
                }
            }
            if (isTypeNewMod())
            {
                frame = hurt[GameCanvas.gameTick % hurt.Length];
            }
            else if (isNewModStand())
            {
                frame = attack1[GameCanvas.gameTick % attack1.Length];
            }
            else if (isNewMod())
            {
                if (GameCanvas.gameTick % 20 > 5)
                {
                    frame = 11;
                }
                else
                {
                    frame = 10;
                }
            }
            else if (isSpecial())
            {
                if (GameCanvas.gameTick % 20 > 5)
                {
                    frame = 1;
                }
                else
                {
                    frame = 15;
                }
            }
            else if (GameCanvas.gameTick % 20 > 5)
            {
                frame = 11;
            }
            else
            {
                frame = 10;
            }
        }
        if (!isUpdate())
        {
            return;
        }
        if (isShadown)
        {
            updateShadown();
        }
        if (vMobMove == null && arrMobTemplate[templateId].rangeMove != 0)
        {
            return;
        }
        if (status != 3 && isBusyAttackSomeOne)
        {
            if (cFocus != null)
            {
                cFocus.doInjure(dame, dameMp, isCrit: false, isMob: true);
            }
            else if (mobToAttack != null)
            {
                mobToAttack.setInjure();
            }
            isBusyAttackSomeOne = false;
        }
        if (levelBoss > 0)
        {
            updateSuperEff();
        }
        switch (status)
        {
            case 1:
                isDisable = false;
                isDontMove = false;
                isFire = false;
                isIce = false;
                isWind = false;
                y += p1;
                if (GameCanvas.gameTick % 2 == 0)
                {
                    if (p2 > 1)
                    {
                        p2--;
                    }
                    else if (p2 < -1)
                    {
                        p2++;
                    }
                }
                x += p2;
                if (isTypeNewMod())
                {
                    frame = hurt[GameCanvas.gameTick % hurt.Length];
                }
                else if (isNewModStand())
                {
                    frame = attack1[GameCanvas.gameTick % attack1.Length];
                }
                else if (isNewMod())
                {
                    frame = 11;
                }
                else if (isSpecial())
                {
                    frame = 15;
                }
                else
                {
                    frame = 11;
                }
                if (isDie || y > TileMap.pxh + 50 || p1 > 20)
                {
                    isDie = false;
                    if (isMobMe)
                    {
                        for (int j = 0; j < GameScr.vMob.size(); j++)
                        {
                            if (((Mob)GameScr.vMob.elementAt(j)).mobId == mobId)
                            {
                                GameScr.vMob.removeElementAt(j);
                            }
                        }
                    }
                    p1 = 0;
                    p2 = 0;
                    p3 = 0;
                    x = (y = 0);
                    hp = getTemplate().hp;
                    status = 0;
                    timeStatus = 0;
                    break;
                }
                if ((TileMap.tileTypeAtPixel(x, y) & 2) == 2)
                {
                    p1 = ((p1 <= 4) ? (-p1) : (-4));
                    if (p3 == 0)
                    {
                        p3 = 16;
                    }
                }
                else
                {
                    p1++;
                }
                if (p3 > 0)
                {
                    p3--;
                    if (p3 == 0)
                    {
                        isDie = true;
                    }
                }
                break;
            case 2:
                if (holdEffID == 0 && !isFreez && !blindEff && !sleepEff)
                {
                    timeStatus = 0;
                    updateMobStandWait();
                }
                break;
            case 4:
                if (holdEffID == 0 && !blindEff && !sleepEff && !isFreez)
                {
                    timeStatus = 0;
                    p1++;
                    if (p1 > 40 + mobId % 5)
                    {
                        if (arrMobTemplate[templateId].type == 4 || arrMobTemplate[templateId].type == 5)
                        {
                            y -= 2;
                        }
                        status = 5;
                        p1 = 0;
                    }
                }
                break;
            case 3:
                if (holdEffID == 0 && !blindEff && !sleepEff && !isFreez)
                {
                    updateMobAttack();
                }
                break;
            case 5:
                if (holdEffID != 0 || blindEff || sleepEff)
                {
                    break;
                }
                if (isFreez)
                {
                    if (arrMobTemplate[templateId].type == 4)
                    {
                        ty++;
                        wt++;
                        fy += ((!wy) ? 1 : (-1));
                        if (wt == 10)
                        {
                            wt = 0;
                            wy = !wy;
                        }
                    }
                }
                else
                {
                    timeStatus = 0;
                    updateMobWalk();
                }
                break;
            case 6:
                timeStatus = 0;
                p1++;
                y += p1;
                if (y >= yFirst)
                {
                    y = yFirst;
                    p1 = 0;
                    status = 5;
                }
                break;
            case 7:
                updateInjure();
                break;
        }
    }

    private void updateInjure()
    {
        if (!isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0)
        {
            if (isTypeNewMod())
            {
                frame = hurt[GameCanvas.gameTick % hurt.Length];
            }
            else if (isNewModStand())
            {
                frame = attack1[GameCanvas.gameTick % attack1.Length];
            }
            else if (isNewMod())
            {
                if (frame != 10)
                {
                    frame = 10;
                }
                else
                {
                    frame = 11;
                }
            }
            else if (isSpecial())
            {
                if (frame != 1)
                {
                    frame = 1;
                }
                else
                {
                    frame = 15;
                }
            }
            else if (frame != 10)
            {
                frame = 10;
            }
            else
            {
                frame = 11;
            }
        }
        timeStatus--;
        if (timeStatus <= 0 && (isTypeNewMod() || isNewModStand() || (isNewMod() && frame == 11) || (isSpecial() && frame == 15) || (templateId < 58 && frame == 11)))
        {
            if ((injureBy != null && injureThenDie) || hp == 0)
            {
                status = 1;
                p2 = (injureBy != null) ? (injureBy.cdir << 1) : (-dir);
                p1 = -3;
                p3 = 0;
            }
            else
            {
                status = 5;
                if (injureBy != null)
                {
                    dir = -injureBy.cdir;
                    if (Res.abs(x - injureBy.cx) < 24)
                    {
                        status = 2;
                    }
                }
                p1 = (p2 = (p3 = 0));
                timeStatus = 0;
            }
            injureBy = null;
        }
        else if (arrMobTemplate[templateId].type != 0 && injureBy != null)
        {
            int num = -injureBy.cdir << 1;
            if (x > xFirst - arrMobTemplate[templateId].rangeMove && x < xFirst + arrMobTemplate[templateId].rangeMove)
            {
                x -= num;
            }
        }
    }

    private void updateMobStandWait()
    {
        checkFrameTick(stand);
        switch (arrMobTemplate[templateId].type)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                p1++;
                if (p1 > 10 + mobId % 10 && (cFocus == null || Res.abs(cFocus.cx - x) > 80) && (mobToAttack == null || Res.abs(mobToAttack.x - x) > 80))
                {
                    status = 5;
                }
                break;
            case 4:
            case 5:
                p1++;
                if (p1 > mobId % 3 && (cFocus == null || Res.abs(cFocus.cx - x) > 80) && (mobToAttack == null || Res.abs(mobToAttack.x - x) > 80))
                {
                    status = 5;
                }
                break;
        }
        if (cFocus != null && GameCanvas.gameTick % (10 + p1 % 20) == 0)
        {
            if (cFocus.cx > x)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }
        else if (mobToAttack != null && GameCanvas.gameTick % (10 + p1 % 20) == 0)
        {
            if (mobToAttack.x > x)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }
        if (forceWait > 0)
        {
            forceWait--;
            status = 2;
        }
    }

    public void updateMobAttack()
    {
        int[] array = ((p3 != 0) ? attack2 : attack1);
        if (tick < array.Length)
        {
            checkFrameTick(array);
            if (x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w && p3 == 0 && GameCanvas.gameTick % 2 == 0)
            {
                SoundMn.gI().charPunch(isKick: false, 0.05f);
            }
        }
        if (p1 == 0)
        {
            int num = 0;
            int num2 = 0;
            num = ((cFocus == null) ? mobToAttack.x : cFocus.cx);
            num2 = ((cFocus == null) ? mobToAttack.y : cFocus.cy);
            if (!isNewMod())
            {
                if (x > xFirst + arrMobTemplate[templateId].rangeMove)
                {
                    p1 = 1;
                }
                if (x < xFirst - arrMobTemplate[templateId].rangeMove)
                {
                    p1 = 1;
                }
            }
            if ((arrMobTemplate[templateId].type == 4 || arrMobTemplate[templateId].type == 5) && !isDontMove)
            {
                y += (num2 - y) / 20;
            }
            p2++;
            if (p2 > array.Length - 1 || p1 == 1)
            {
                p1 = 1;
                if (p3 == 0)
                {
                    if (cFocus != null)
                    {
                        cFocus.doInjure(dame, dameMp, isCrit: false, isMob: true);
                    }
                    else
                    {
                        mobToAttack.setInjure();
                    }
                    isBusyAttackSomeOne = false;
                }
                else
                {
                    if (cFocus != null)
                    {
                        MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), (int)dame, dameMp, cFocus, getTemplate().dartType);
                    }
                    else
                    {
                        Char @char = new Char();
                        @char.cx = mobToAttack.x;
                        @char.cy = mobToAttack.y;
                        @char.charID = -100;
                        MonsterDart.addMonsterDart(x + dir * w, y, checkIsBoss(), (int)dame, dameMp, @char, getTemplate().dartType);
                    }
                    isBusyAttackSomeOne = false;
                }
            }
            dir = ((x < num) ? 1 : (-1));
        }
        else if (p1 == 1)
        {
            if (arrMobTemplate[templateId].type == 0 || isDontMove || isIce || !isWind)
            {
            }
            if (tick == array.Length)
            {
                status = 2;
                p1 = 0;
                p2 = 0;
                tick = 0;
            }
        }
        if (tick == 5 && cFocus != null && cFocus.charID == Char.myCharz().charID)
        {
            if (templateId == 88 && p3 != 0)
            {
                GameScr.shock_scr = 2;
            }
            if (templateId == 89)
            {
                GameScr.shock_scr = 2;
            }
        }
    }

    public void updateMobWalk()
    {
        int num = 0;
        try
        {
            if (injureThenDie)
            {
                status = 1;
                p2 = injureBy.cdir << 3;
                p1 = -5;
                p3 = 0;
            }
            num = 1;
            if (isIce)
            {
                return;
            }
            if (isDontMove || isWind)
            {
                checkFrameTick(stand);
                return;
            }
            switch (arrMobTemplate[templateId].type)
            {
                case 0:
                    if (isNewModStand())
                    {
                        frame = stand[GameCanvas.gameTick % stand.Length];
                    }
                    else
                    {
                        frame = 0;
                    }
                    num = 2;
                    break;
                case 1:
                case 2:
                case 3:
                    {
                        num = 3;
                        sbyte b = arrMobTemplate[templateId].speed;
                        if (b == 1)
                        {
                            if (GameCanvas.gameTick % 2 == 1)
                            {
                                break;
                            }
                        }
                        else if (b > 2)
                        {
                            b += (sbyte)(mobId % 2);
                        }
                        else if (GameCanvas.gameTick % 2 == 1)
                        {
                            b--;
                        }
                        int nextX = x + b * dir;
                        if (!TileMap.tileTypeAt(nextX, y + 2, 2) && TileMap.tileTypeAt(x, y + 2, 2))
                        {
                            dir = -dir;
                        }
                        else
                        {
                            x = nextX;
                        }
                        if (x > xFirst + arrMobTemplate[templateId].rangeMove)
                        {
                            dir = -1;
                        }
                        else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
                        {
                            dir = 1;
                        }
                        if (!TileMap.tileTypeAt(x, y, 2) && TileMap.tileTypeAt(x, yFirst, 2))
                        {
                            y = yFirst;
                        }
                        if (Res.abs(x - Char.myCharz().cx) < 40 && Res.abs(x - xFirst) < arrMobTemplate[templateId].rangeMove)
                        {
                            dir = ((x <= Char.myCharz().cx) ? 1 : (-1));
                            if (Res.abs(x - Char.myCharz().cx) < 20)
                            {
                                x -= dir * 10;
                            }
                            status = 2;
                            forceWait = 20;
                        }
                        checkFrameTick((w <= 30) ? moveFast : move);
                        break;
                    }
                case 4:
                    {
                        num = 4;
                        sbyte speed2 = arrMobTemplate[templateId].speed;
                        speed2 += (sbyte)(mobId % 2);
                        x += speed2 * dir;
                        if (GameCanvas.gameTick % 10 > 2)
                        {
                            y += speed2 * dirV;
                        }
                        speed2 += (sbyte)((GameCanvas.gameTick + mobId) % 2);
                        if (x > xFirst + arrMobTemplate[templateId].rangeMove)
                        {
                            dir = -1;
                            status = 2;
                            forceWait = GameCanvas.gameTick % 20 + 20;
                            p1 = 0;
                        }
                        else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
                        {
                            dir = 1;
                            status = 2;
                            forceWait = GameCanvas.gameTick % 20 + 20;
                            p1 = 0;
                        }
                        if (y > yFirst + 24)
                        {
                            dirV = -1;
                        }
                        else if (y < yFirst - (20 + GameCanvas.gameTick % 10))
                        {
                            dirV = 1;
                        }
                        checkFrameTick(move);
                        break;
                    }
                case 5:
                    {
                        num = 5;
                        sbyte speed = arrMobTemplate[templateId].speed;
                        speed += (sbyte)(mobId % 2);
                        x += speed * dir;
                        speed += (sbyte)((GameCanvas.gameTick + mobId) % 2);
                        if (GameCanvas.gameTick % 10 > 2)
                        {
                            y += speed * dirV;
                        }
                        if (x > xFirst + arrMobTemplate[templateId].rangeMove)
                        {
                            dir = -1;
                            status = 2;
                            forceWait = GameCanvas.gameTick % 20 + 20;
                            p1 = 0;
                        }
                        else if (x < xFirst - arrMobTemplate[templateId].rangeMove)
                        {
                            dir = 1;
                            status = 2;
                            forceWait = GameCanvas.gameTick % 20 + 20;
                            p1 = 0;
                        }
                        if (y > yFirst + 24)
                        {
                            dirV = -1;
                        }
                        else if (y < yFirst - (20 + GameCanvas.gameTick % 10))
                        {
                            dirV = 1;
                        }
                        if (TileMap.tileTypeAt(x, y, 2))
                        {
                            if (GameCanvas.gameTick % 10 > 5)
                            {
                                y = TileMap.tileYofPixel(y);
                                status = 4;
                                p1 = 0;
                                dirV = -1;
                            }
                            else
                            {
                                dirV = -1;
                            }
                        }
                        break;
                    }
            }
        }
        catch (Exception)
        {
            Cout.println("lineee: " + num);
        }
    }

    public void updateHp_bar()
    {
        len = (int)((long)hp * 100L / maxHp * w_hp_bar) / 100;
        per = (int)((long)hp * 100L / maxHp);
        if (per == 100)
        {
            per_tem = per;
        }
        if (per >= 100)
        {
            per_tem = per;
        }
        offset = 0;
        if (per < 30)
        {
            color = 15473700;
            imgHPtem = GameScr.imgHP_tm_do;
        }
        else if (per < 60)
        {
            color = 16744448;
            imgHPtem = GameScr.imgHP_tm_vang;
        }
        else
        {
            color = 11992374;
            imgHPtem = GameScr.imgHP_tm_xanh;
        }
    }

}
