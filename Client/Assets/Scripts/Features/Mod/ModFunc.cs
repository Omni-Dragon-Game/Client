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

public partial class ModFunc : IActionListener
{

















    //public static void AutoLogin()
    //{
    //    Thread.Sleep(1000);
    //    while (ServerListScreen.testConnect != 2)
    //    {
    //        GameCanvas.serverScreen.switchToMe();
    //        Thread.Sleep(1000);
    //    }
    //    GameCanvas.loginScr ??= new LoginScr();
    //    GameCanvas.loginScr.switchToMe();
    //    Thread.Sleep(500);

    //    Account account = FindAccWithUsername(accAutoLogin);
    //    if (account.getUsername().Length > 0)
    //    {
    //        Rms.saveRMSString("acc", account.getUsername());
    //        Rms.saveRMSString("pass", account.getPassword());
    //        if (GameCanvas.currentScreen == GameCanvas.loginScr)
    //        {
    //            GameCanvas.loginScr.setUserPass();
    //        }
    //    }

    //    Thread.Sleep(500);
    //    GameCanvas.loginScr.doLogin();
    //}









    private long currTimeLogin;
    //public IEnumerator AutoLogin2()
    //{
    //    yield return new WaitForSecondsRealtime(10f);
    //    if (GameCanvas.currentScreen is ServerListScreen)
    //    {
    //        if (GameCanvas.loginScr == null)
    //        {
    //            GameCanvas.loginScr = new LoginScr();
    //        }
    //        GameCanvas.loginScr.switchToMe();
    //    }
    //    else if (GameCanvas.currentScreen is LoginScr)
    //    {
    //        yield return new WaitForSecondsRealtime(2f);
    //        GameCanvas.loginScr = new LoginScr();
    //        GameCanvas.loginScr.doLogin();
    //        Main.main.StartCoroutine(AutoLogin2());
    //    }

    //}

    public static bool AutoLogin()
    {
        if (autoLogin == null)
        {
            return false;
        }

        if (autoLogin.waitToNextLogin)
        {
            return true;
        }

        if (!Util.CanDoWithTime(autoLogin.lastTimeWait, 500))
        {
            return false;
        }

        if (ServerListScreen.testConnect != 2)
        {
            GameCanvas.serverScreen ??= new ServerListScreen();
            GameCanvas.serverScreen.switchToMe();
            autoLogin.lastTimeWait = mSystem.currentTimeMillis();
            return false;
        }

        if (GameCanvas.currentScreen != GameCanvas.loginScr)
        {
            GameCanvas.loginScr ??= new LoginScr();
            GameCanvas.loginScr.switchToMe();
            autoLogin.lastTimeWait = mSystem.currentTimeMillis();
            return false;
        }

        if (!autoLogin.hasSetUserPass)
        {
            Account account = autoLogin.GetAccWithUsername(accounts);
            if (account.getUsername().Length > 0)
            {
                Rms.saveRMSString("acc", account.getUsername());
                Rms.saveRMSString("pass", account.getPassword());
                if (GameCanvas.currentScreen == GameCanvas.loginScr)
                {
                    GameCanvas.loginScr.setUserPass();
                }
                autoLogin.hasSetUserPass = true;
            }
            autoLogin.lastTimeWait = mSystem.currentTimeMillis();
        }

        GameCanvas.loginScr.doLogin();
        autoLogin.waitToNextLogin = true;
        return true;
    }

    public void Update()
    {
        UpdateTouch();
        
        if (isPeanPet && mSystem.currentTimeMillis() - lastPeanPet >= 3000L)
        {
            Char pet = Char.myPetz();
            if (!pet.isDie && (Char.myPetz().cStamina <= Char.myPetz().cMaxStamina * 20 / 100
                || Char.myPetz().cHP < Char.myPetz().cHPFull * 20 / 100
                || Char.myPetz().cMP < Char.myPetz().cMPFull * 20 / 100))
            {
                GameScr.gI().doUseHP();
                lastPeanPet = mSystem.currentTimeMillis();
            }
        }

        //if (Input.GetKey("q") && SpecialSkill.gI().isnoitai)
        //{
        //    SpecialSkill.gI().isnoitai = false;
        //    GameScr.info1.addInfo("Đã Dừng", 0);
        //    if (isPaintCrackBall)
        //    {
        //        isThuongDeVip = false;
        //        isThuongDeVip = false;
        //        isThuongDeThuong = false;
        //        GameScr.info1.addInfo("Đã Dừng", 0);
        //    }
        //}

        if (isAutoPhaLe && itemPhale != null)
        {
            currPhale = GetCurrPhaLe(FindItemBagWithIndexUI(itemPhale.indexUI));
        }
        else
        {
            currPhale = -1;
        }

        if (isAutoChat && mSystem.currentTimeMillis() - lastAutoChat >= 4000L)
        {
            AutoChat();
            lastAutoChat = mSystem.currentTimeMillis();
        }

        if (isAutoChatTG && mSystem.currentTimeMillis() - lastAutoChatTG >= 30000L)
        {
            AutoChatTG();
            lastAutoChatTG = mSystem.currentTimeMillis();
        }

        //if (Mod.doBoss && mSystem.currentTimeMillis() - Mod.currDoBoss >= 1000L)
        //{
        //    Mod.DoBoss();
        //    Mod.currDoBoss = mSystem.currentTimeMillis();
        //}

        if (!TileMap.isOfflineMap() && mSystem.currentTimeMillis() - lastUpdateZones >= 1000L)
        {
            UseItemAuto();
            if (isUpdateZones)
            {
                Service.gI().openUIZone();
            }
            lastUpdateZones = mSystem.currentTimeMillis();
        }

        if (isAutoVQMM && mSystem.currentTimeMillis() - lastVQMM >= 1000L)
        {
            quayThuongDe();
            lastVQMM = mSystem.currentTimeMillis();
        }

        //Mod.AutoTTNL();

        if (autoWakeUp && mSystem.currentTimeMillis() - lastAutoWakeUp >= 1000)
        {
            AutoHoiSinh();
            lastAutoWakeUp = mSystem.currentTimeMillis();
        }

        //Mod.xd();
        //Mod.cd();
        //Mod.UseSkillAuto();

        if (focusBoss && mSystem.currentTimeMillis() - lastFocusBoss >= 500L)
        {
            AutoFocusBoss();
            lastFocusBoss = mSystem.currentTimeMillis();
        }

        //Mod.KSBoss();
        //Mod.KSBossBangSkill5();
        //Mod.khoaViTri();
        //Mod.gmt();
        //Mod.AutoBT();
        //Mod.AutoCTG();
        //Mod.AutoNhatXa();

        //if (Mod.isAutoVeKhu && mSystem.currentTimeMillis() - Mod.currVeKhuCu >= 20000L)
        //{
        //    Mod.currVeKhuCu = mSystem.currentTimeMillis();
        //    Mod.khuVeLai = TileMap.zoneID;
        //}

        //if (Mod.isAutoAnNho && Char.myCharz().cStamina <= 5 && mSystem.currentTimeMillis() - Mod.currAnNho >= 1000L)
        //{
        //    Mod.AnNho();
        //    Mod.currAnNho = mSystem.currentTimeMillis();
        //}

        if (autoAttack)
        {
            AutoAttack();
        }

        //if (Mod.isPKM && !Mod.isGMT && (Char.myCharz().charFocus == null || (Char.myCharz().charFocus != null && !Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))))
        //{
        //    for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        //    {
        //        Char @char = (Char)GameScr.vCharInMap.elementAt(i);
        //        if (@char != null && Char.myCharz().isMeCanAttackOtherPlayer(@char) && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("$") && !@char.cName.StartsWith("#") && @char.charID >= 0)
        //        {
        //            Char.myCharz().focusManualTo(@char);
        //            return;
        //        }
        //    }
        //}

        //if (Mod.thudau && mSystem.currentTimeMillis() - Mod.currThuDau >= 500L)
        //{
        //    Mod.td();
        //    Mod.currThuDau = mSystem.currentTimeMillis();
        //}

        //if (Mod.isAutoNeBoss && mSystem.currentTimeMillis() - Mod.currNeBoss >= 5000L)
        //{
        //    Mod.NeBoss();
        //    Mod.currNeBoss = mSystem.currentTimeMillis();
        //}

        UpdateNotifTichXanh();
    }





















    //private static Account FindAccWithUsername(string username)
    //{
    //    foreach (Account acc in accounts)
    //    {
    //        if (acc.getUsername().Equals(username))
    //        {
    //            return acc;
    //        }
    //    }

    //    return new Account("", "");
    //}





















}