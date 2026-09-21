using JetBrains.Annotations;
using Mod;
using Mod.XMAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Networking;

public partial class ModFunc
{
    public void MoveTo(int x, int y)
    {
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
        Char.myCharz().cx = x;
        Char.myCharz().cy = y + 1;
        Service.gI().charMove();
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
    }

    public void GotoNpc(int npcID)
    {
        for (int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
            if (npc.template.npcTemplateId == npcID && Math.abs(npc.cx - Char.myCharz().cx) >= 50)
            {
                MoveTo(npc.cx, npc.cy - 1);
                Char.myCharz().FocusManualTo(npc);
                return;
            }
        }
    }

    public int FindItemIndex(int idItem)
    {
        if (Char.myCharz().arrItemBag == null)
        {
            return -1;
        }
        for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
        {
            if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == idItem)
            {
                return Char.myCharz().arrItemBag[i].indexUI;
            }
        }
        return -1;
    }

    private void AttackChar()
    {
        try
        {
            MyVector myVector = new();
            myVector.addElement(Char.myCharz().charFocus);
            Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
        }
        catch
        {
        }
    }

    public void AttackMob(Mob mob)
    {
        try
        {
            MyVector myVector = new();
            myVector.addElement(mob);
            Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
        }
        catch
        {
        }
    }

    public void AutoAttack()
    {
        Char @char = Char.myCharz();
        if (!Char.isLoadingMap
            && !@char.stone
            && !@char.meDead
            && @char.statusMe != 14
            && @char.statusMe != 5
            && @char.myskill.template.type == 1
            && @char.myskill.template.id != 10
            && @char.myskill.template.id != 11
            && !@char.myskill.paintCanNotUseSkill)
        {
            if (mSystem.currentTimeMillis() - lastAutoAttack > 500)
            {
                if (GameScr.gI().isMeCanAttackMob(@char.mobFocus) && Res.abs(@char.mobFocus.xFirst - @char.cx) < @char.myskill.dx * 2)
                {
                    AttackMob(@char.mobFocus);
                    SetUsedSkill(@char.myskill);
                }
                else if (@char.isMeCanAttackOtherPlayer(@char.charFocus) && Res.abs(@char.charFocus.cx - @char.cx) < @char.myskill.dx * 2)
                {
                    AttackChar();
                    SetUsedSkill(@char.myskill);
                }
                lastAutoAttack = mSystem.currentTimeMillis();
            }
        }
    }

    public void SetUsedSkill(Skill skill)
    {
        skill.paintCanNotUseSkill = true;
        skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
    }

    public void UsePorata()
    {
        int[] ids = new int[] { 454, 921, 2064, 2113, 1324 };
        foreach (int num in ids)
        {
            int index = FindItemIndex(num);
            if (index != -1)
            {
                Service.gI().useItem(0, 1, (sbyte)index, -1);
                Service.gI().petStatus(3);
                return;
            }
        }
        GameScr.info1.addInfo("Bạn không có bông tai", 0);
    }

    public void AutoFocusBoss()
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = (Char)GameScr.vCharInMap.elementAt(i);
            if (@char != null && @char.charID < 0 && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
            {
                Char.myCharz().FocusManualTo(@char);
                return;
            }
        }
    }

    public int GetMapID(string mapName)
    {
        int result = -1;
        for (int i = 0; i < XmapController.mapNames.Length; i++)
        {
            if (XmapController.mapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
            {
                result = i;
            }
        }
        return result;
    }

    private string CharGender(Char @char)
    {
        string result;
        if (@char.cTypePk == 5)
        {
            result = "BOSS";
        }
        else if (@char.cgender == 0)
        {
            result = "TĐ";
        }
        else if (@char.cgender == 1)
        {
            result = "NM";
        }
        else if (@char.cgender == 2)
        {
            result = "XD";
        }
        else
        {
            result = "";
        }
        return result;
    }

    public void UseItem(int itemId)
    {
        int index = FindItemIndex(itemId);
        if (index != -1)
        {
            Service.gI().useItem(0, 1, (sbyte)index, -1);
            return;
        }
        GameScr.info1.addInfo("Không tìm thấy vật phẩm", 0);
    }

    public void UseItemAuto()
    {
        if (!startAutoItem)
        {
            System.Threading.Tasks.Task.Delay(10000).ContinueWith(t => startAutoItem = true);
            return;
        }
        if (listItemAuto.Count > 0 && startAutoItem)
        {
            for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
            {
                Item item = Char.myCharz().arrItemBag[i];
                foreach (ItemAuto itemAuto in listItemAuto)
                {
                    if (item != null && item.template.iconID == itemAuto.iconID && item.template.id == itemAuto.id && !ItemTime.isExistItem(item.template.iconID))
                    {
                        Service.gI().useItem(0, 1, (sbyte)FindItemIndex(item.template.id), -1);
                        break;
                    }
                }
            }
        }
    }

    private void AutoHoiSinh()
    {
        if (Char.myCharz().cHP <= 0 || Char.myCharz().meDead || Char.myCharz().statusMe == 14)
        {
            Service.gI().wakeUpFromDead();
        }
    }

    public static int GetCurrPhaLe(Item item)
    {
        for (int i = 0; i < item.itemOption.Length; i++)
        {
            if (item.itemOption[i].optionTemplate.id == 107)
            {
                return item.itemOption[i].param;
            }
        }
        return 0;
    }

    public void AutoPhaLe()
    {
        while (isAutoPhaLe)
        {
            if (TileMap.mapID != 5)
            {
                GameScr.info1.addInfo("Cần đến Đảo Kame để sử dụng Tự động Pha lê hóa", 0);
                Thread.Sleep(500);
                break;
            }
            if (currPhale >= maxPhale && itemPhale != null && currPhale >= 0 && maxPhale > 0)
            {
                Sound.start(1f, Sound.l1);
                GameScr.info1.addInfo("Đã đạt đến số sao yêu cầu", 0);
                maxPhale = -1;
                itemPhale = null;
            }
            if (Char.myCharz().xu > 10000000000L)
            {
                GotoNpc(21);
                if (itemPhale != null && maxPhale > 0)
                {
                    while (!GameCanvas.menu.showMenu)
                    {
                        Service.gI().combine(1, GameCanvas.panel.vItemCombine);
                        Thread.Sleep(100);
                    }
                    Service.gI().confirmMenu(21, 0);
                    GameCanvas.menu.doCloseMenu();
                    GameCanvas.panel.currItem = null;
                    GameCanvas.panel.chatTField.isShow = false;
                }
            }
            else if (itemPhale != null)
            {
                BanVang();
            }
            Thread.Sleep(500);
        }
    }

    private void BanVang()
    {
        if (TileMap.mapID != 5)
        {
            GameScr.info1.addInfo("Cần đến Đảo Kame để Tự động bán vàng", 0);
            Thread.Sleep(1000);
            return;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GameScr.info1.addInfo("Dừng bán vàng", 0);
            return;
        }
        while (Char.myCharz().xu <= 60000000000L && !Input.GetKey(KeyCode.Q))
        {
            if (FindItemIndex(457) == -1)
            {
                GameScr.info1.addInfo("Không tìm thấy thỏi vàng", 0);
                if (isAutoPhaLe)
                {
                    isAutoPhaLe = false;
                    GameScr.info1.addInfo("Vàng không đủ, đã tắt Tự động Pha lê hóa", 0);
                }
                return;
            }
            Service.gI().useItem(0, 1, (sbyte)FindItemIndex(457), -1);
            GameScr.info1.addInfo("Đang bán thỏi vàng", 0);
            Thread.Sleep(500);
        }
        GameScr.info1.addInfo("Đã bán xong", 0);
        Thread.Sleep(500);
    }

    public static Item FindItemBagWithIndexUI(int index)
    {
        foreach (Item item in Char.myCharz().arrItemBag)
        {
            if (item != null && item.indexUI == index)
            {
                return item;
            }
        }
        return null;
    }

    public void CollectAllThuongDe()
    {
        isCollectAll = true;
        Service.gI().openMenu(19);
        Service.gI().confirmMenu(19, 2);
        Service.gI().confirmMenu(19, 1);
        Service.gI().buyItem(2, 0, 0);
        Thread.Sleep(2000);
        isCollectAll = false;
    }

    private void OpenMenuThuongDe()
    {
        isOpenThuongDe = true;
        Service.gI().openMenu(19);
        Service.gI().confirmMenu(19, 2);
        Service.gI().confirmMenu(19, 0);
        isOpenThuongDe = false;
    }

    public void quayThuongDe()
    {
        if (!isCollectAll && !isOpenThuongDe)
        {
            if (!isPaintThuongDe && TileMap.mapID == 45)
            {
                OpenMenuThuongDe();
                return;
            }
            if (TileMap.mapID == 45)
            {
                if (Input.GetKey("q") || Char.myCharz().xu <= 200000000L)
                {
                    GameScr.info1.addInfo("Đã tắt Auto VQMM (2)", 0);
                    isAutoVQMM = false;
                    return;
                }
                Service.gI().openMenu(19);
                Service.gI().SendCrackBall(2, 7);
            }
        }
    }

    public void AutoBuyItem(int num, Item itemBuy)
    {
        new Thread(() =>
        {
            for (int i = 0; i < num; i++)
            {
                Service.gI().buyItem(3, itemBuy.template.id, 0);
                Thread.Sleep(200);
            }
            GameScr.info1.addInfo("Đã mua xong " + num + " " + itemBuy.template.name, 0);
        }).Start();
    }

    private void AddOrRemoveAutoItem(Item item, bool isAdd)
    {
        if (isAdd)
        {
            listItemAuto.Add(new ItemAuto(item.template.iconID, item.template.id));
            GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào Auto Item", 0);
        }
        else
        {
            foreach (ItemAuto itemAuto in listItemAuto)
            {
                if (itemAuto.iconID == item.template.iconID && itemAuto.id == item.template.id)
                {
                    listItemAuto.Remove(itemAuto);
                    GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi Auto Item", 0);
                    break;
                }
            }
        }
    }

    public void DoDoubleClickToObj(IMapObject obj)
    {
        if ((obj.Equals(Char.myCharz().npcFocus) || GameScr.gI().mobCapcha == null) && !GameScr.gI().checkClickToBotton(obj))
        {
            GameScr.gI().checkEffToObj(obj, false);
            Char.myCharz().cancelAttack();
            Char.myCharz().currentMovePoint = null;
            Char.myCharz().cvx = (Char.myCharz().cvy = 0);
            obj.stopMoving();
            GameScr.gI().auto = 10;
            GameScr.gI().doFire(isFireByShortCut: false, skipWaypoint: true);
            GameScr.gI().clickToX = obj.getX();
            GameScr.gI().clickToY = obj.getY();
            GameScr.gI().clickOnTileTop = false;
            GameScr.gI().clickMoving = true;
            GameScr.gI().clickMovingRed = true;
            GameScr.gI().clickMovingTimeOut = 20;
            GameScr.gI().clickMovingP1 = 30;
        }
    }

    public void ChangeGameSpeed(string strSpeed)
    {
        bool success = int.TryParse(strSpeed, out int speed);
        if (success && speed > 0 && speed <= 10)
        {
            Time.timeScale = speed;
            GameScr.info1.addInfo("Tốc độ game: " + speed, 0);
        }
        else
        {
            GameScr.info1.addInfo("Chỉ nhập số từ 1 đến 10", 0);
        }
    }

    public void TeleportToPlayer(int charID)
    {
        Service.gI().gotoPlayer(charID);
    }

    public void SetAutoIntrinsic(int param)
    {
        if (curSelectIntrinsic.Length > 0)
        {
            bool success = int.TryParse(curSelectIntrinsic.Split("đến ")[1].Split("%")[0], out int maxParam);
            if (success && param > 0 && param <= maxParam)
            {
                paramIntrinsic = param;
                if (curSelectIntrinsic.Contains("+"))
                {
                    curSelectIntrinsic = curSelectIntrinsic.Split("+")[0].Trim();
                }
                else if (curSelectIntrinsic.Contains("dưới"))
                {
                    curSelectIntrinsic = curSelectIntrinsic.Split("dưới ")[0].Trim();
                }
                else
                {
                    paramIntrinsic = -1;
                    curSelectIntrinsic = "";
                    GameScr.info1.addInfo("Có lỗi xảy ra, vui lòng liên hệ ADMIN!", 0);
                    return;
                }
                new Thread(new ThreadStart(DoAutoIntrinsic)).Start();
            }
            else
            {
                GameScr.info1.addInfo("Chỉ số đã chọn không đúng! (0)", 0);
            }
        }
        else
        {
            GameScr.info1.addInfo("Chỉ số đã chọn không đúng! (1)", 0);
        }
    }

    private void DoAutoIntrinsic()
    {
        while (paramIntrinsic != -1)
        {
            // Open Intrinsic
            Service.gI().speacialSkill(0);
            Thread.Sleep(500);
            Service.gI().confirmMenu(5, 2);
            Thread.Sleep(500);
            Service.gI().confirmMenu(5, 0);
            Thread.Sleep(500);
        }
    }

    public void CheckAutoIntrinsic(string info)
    {
        if (info.Contains("+"))
        {
            string[] recvInfo = info.Split("+");
            string recvName = recvInfo[0].Trim();
            int recvParam;
            if (int.TryParse(recvInfo[1].Split("%")[0], out recvParam))
            {
                if (curSelectIntrinsic == recvName && recvParam >= paramIntrinsic)
                {
                    GameScr.info1.addInfo("Mở nội tại " + curSelectIntrinsic + " " + paramIntrinsic + "% thành công!", 0);
                    paramIntrinsic = -1;
                    curSelectIntrinsic = "";
                    GameCanvas.menu.menuSelectedItem = GameCanvas.menu.menuItems.size() - 1;
                    GameCanvas.menu.performSelect();
                    GameCanvas.menu.doCloseMenu();
                }
            }
        }
        else if (info.Contains("dưới"))
        {
            string[] recvInfo = info.Split("dưới ");
            string recvName = recvInfo[0].Trim();
            int recvParam;
            if (int.TryParse(recvInfo[1].Split("%")[0], out recvParam))
            {
                if (curSelectIntrinsic == recvName && recvParam >= paramIntrinsic)
                {
                    GameScr.info1.addInfo("Mở nội tại " + curSelectIntrinsic + " " + paramIntrinsic + "% thành công!", 0);
                    paramIntrinsic = -1;
                    curSelectIntrinsic = "";
                    GameCanvas.menu.menuSelectedItem = GameCanvas.menu.menuItems.size() - 1;
                    GameCanvas.menu.performSelect();
                    GameCanvas.menu.doCloseMenu();
                }
            }
        }
        else
        {
            paramIntrinsic = -1;
            curSelectIntrinsic = "";
            GameCanvas.menu.doCloseMenu();
        }
    }

    public void SetIncreasePoint(string strPoint)
    {
        bool success = int.TryParse(strPoint, out int point);
        if (success && indexAutoPoint != -1 && point > 0)
        {
            pointIncrease = point;
            new Thread(new ThreadStart(DoAutoIncreasePoint)).Start();
            GameScr.info1.addInfo("Tự động tăng " + strPointTypes[indexAutoPoint] + " đến " + point, 0);

        }
        else
        {
            GameScr.info1.addInfo("Có lỗi xảy ra (100)", 0);
        }
    }

    private void DoAutoIncreasePoint()
    {
        while (indexAutoPoint != -1 && pointIncrease > 0)
        {
            Char @char = autoPointForPet ? Char.myPetz() : Char.myCharz();
            var currentPoint = indexAutoPoint switch
            {
                0 => @char.cHPGoc,
                1 => @char.cMPGoc,
                2 => @char.cDamGoc,
                3 => @char.cDefGoc,
                4 => @char.cCriticalGoc,
                _ => 0,
            };
            if (currentPoint >= pointIncrease)
            {
                indexAutoPoint = -1;
                pointIncrease = 0;
                GameScr.info1.addInfo("Đã đạt chỉ số yêu cầu", 0);
                break;
            }
            Service.gI().upPotential(autoPointForPet, indexAutoPoint, 100);
            Thread.Sleep(500);
        }
    }

    public void LoadAcc()
    {
        string text = Rms.loadRMSString("accManager");
        if (text == null || text.Trim('|') == string.Empty) return;

        accounts.Clear();
        cmdsChooseAcc.Clear();
        cmdsDelAcc.Clear();

        string[] accs = text.Trim('|').Split('|');
        for (int i = 0; i < accs.Length; i++)
        {
            string[] acc = accs[i].Split('$');

            Account account = new(acc[0], acc[1]);
            accounts.Add(account);

            Command cmd = new(account.getUsername(), this, 102, account);
            cmd.setType();
            cmdsChooseAcc.Add(cmd);

            Command cmdDel = new("Xoá", this, 103, account);
            cmdDel.setTypeDelete();
            cmdsDelAcc.Add(cmdDel);
        }
    }

    public void AddAccount(string user, string pass)
    {
        Account account = new(user, pass);

        int index = accounts.IndexOf(account);
        if (index != -1)
        {
            accounts.RemoveAt(index);
        }
        accounts.Insert(0, account);

        for (int i = 5; i < accounts.Count; i++)
        {
            accounts.RemoveAt(i);
        }
        SaveAcc();
    }

    private void SaveAcc()
    {
        string text = "";
        foreach (Account acc in accounts)
        {
            text += string.Join('$', acc.getUsername(), acc.getPassword());
            text += "|";
        }
        Rms.saveRMSString("accManager", text.Trim('|'));
    }

    private void AutoChat()
    {
        if (string.IsNullOrEmpty(textAutoChat))
        {
            GameScr.info1.addInfo("Chưa cài nội dung tự động chat", 0);
        }
        else
        {
            Service.gI().chat(textAutoChat);
        }
    }

    private void AutoChatTG()
    {
        if (string.IsNullOrEmpty(textAutoChatTG))
        {
            GameScr.info1.addInfo("Chưa cài nội dung tự động chat thế giới", 0);
        }
        else
        {
            Service.gI().chatGlobal(textAutoChatTG);
        }
    }

    public void GoToBoss(int mapId)
    {
        MyVector myVector = new();
        myVector.addElement(new Command("Đi tới\nMAP " + mapId, this, 1, mapId.ToString()));
        myVector.addElement(new Command("Huỷ", this, 2, null));

        GameCanvas.menu.startAt(myVector, 4);
    }

}
