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
    public bool Chat(string text)
    {
        if (text == "debug")
        {
            isDebugEnable = !isDebugEnable;
            GameScr.info1.addInfo("Debugging Mode: " + (isDebugEnable ? "ON" : "OFF"), 0);
            return true;
        }
        if (text == "loadskill")
        {
            perform(57, null);
            return true;
        }
        if (text == "ak")
        {
            perform(42, null);
            return true;
        }
        if (text == "ts")
        {
            perform(44, null);
            return true;
        }
        if (text == "tsnguoi")
        {
            perform(48, null);
            return true;
        }
        if (text == "vqmm")
        {
            isPaintThuongDe = false;
            isAutoVQMM = !isAutoVQMM;
            GameScr.info1.addInfo("Auto VQMM: " + (isAutoVQMM ? "Bật" : "Tắt"), 0);
            return true;
        }
        if (text == "ukhu")
        {
            isUpdateZones = !isUpdateZones;
            GameScr.info1.addInfo("Tự động cập nhật khu: " + (isUpdateZones ? "Bật" : "Tắt"), 0);
            return true;
        }
        // Start With
        if (text.StartsWith("k "))
        {
            bool success = int.TryParse(text.Replace("k ", ""), out int khu);
            if (success && khu >= 0)
            {
                Service.gI().requestChangeZone(khu, -1);
            }
            return true;
        }
        if (text.StartsWith("s "))
        {
            ChangeGameSpeed(text.Replace("s ", ""));
            return true;
        }
        if (text.StartsWith("atc "))
        {
            textAutoChat = text.Replace("atc ", "");
            return true;
        }
        if (text.StartsWith("atctg "))
        {
            textAutoChatTG = text.Replace("atctg ", "");
            return true;
        }

        //if (text == "attnl")
        //{
        //    Mod.isAutoTTNL = !Mod.isAutoTTNL;
        //    GameScr.info1.addInfo("Auto TTNL: " + (Mod.isAutoTTNL ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "abfdt")
        //{
        //    Mod.aDauDeTu = !Mod.aDauDeTu;
        //    GameScr.info1.addInfo("Auto buff đậu theo chỉ số đệ tử: " + (Mod.aDauDeTu ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text.StartsWith("bhpdt "))
        //{
        //    Mod.csHPDeTu = int.Parse(text.Split(new char[]
        //    {
        //        ' '
        //    })[1]);
        //    GameScr.info1.addInfo("HP buff đậu đệ tử: " + NinjaUtil.getMoneys((long)Mod.csHPDeTu), 0);
        //    text = "";
        //}
        //if (text.StartsWith("bkidt "))
        //{
        //    Mod.csKIDeTu = int.Parse(text.Split(new char[]
        //    {
        //        ' '
        //    })[1]);
        //    GameScr.info1.addInfo("KI buff đậu đệ tử: " + NinjaUtil.getMoneys((long)Mod.csKIDeTu), 0);
        //    text = "";
        //}
        //if (text == "abf")
        //{
        //    Mod.aBuffDau = !Mod.aBuffDau;
        //    GameScr.info1.addInfo("Auto buff đậu theo chỉ số: " + (Mod.aBuffDau ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text.StartsWith("bhp "))
        //{
        //    Mod.csHP = int.Parse(text.Split(new char[]
        //    {
        //        ' '
        //    })[1]);
        //    GameScr.info1.addInfo("HP buff đậu: " + NinjaUtil.getMoneys((long)Mod.csHP), 0);
        //    text = "";
        //}
        //if (text.StartsWith("bki "))
        //{
        //    Mod.csKI = int.Parse(text.Split(new char[]
        //    {
        //        ' '
        //    })[1]);
        //    GameScr.info1.addInfo("KI buff đậu: " + NinjaUtil.getMoneys((long)Mod.csKI), 0);
        //    text = "";
        //}
        //if (text == "akhu")
        //{
        //    Mod.isAutoVeKhu = !Mod.isAutoVeKhu;
        //    GameScr.info1.addInfo((Mod.isAutoVeKhu ? "Auto về khu cũ khi Login: Bật" : "Auto về khu cũ khi Login: Tắt") ?? "", 0);
        //    text = "";
        //}
        //if (text == "kk")
        //{
        //    Mod.khoakhu = !Mod.khoakhu;
        //    GameScr.info1.addInfo("Khóa chuyển khu: " + (Mod.khoakhu ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "kmap")
        //{
        //    Mod.khoamap = !Mod.khoamap;
        //    GameScr.info1.addInfo("Khóa map: " + (Mod.khoamap ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "nmt")
        //{
        //    if (Mod.getX(0) > 0 && Mod.getY(0) > 0)
        //    {
        //        Mod.GotoXY(Mod.getX(0), Mod.getY(0));
        //    }
        //    else
        //    {
        //        Mod.GotoXY(30, PickMobController.GetYsd(30));
        //    }
        //    text = "";
        //}
        //if (text == "nmp")
        //{
        //    if (Mod.getX(2) > 0 && Mod.getY(2) > 0)
        //    {
        //        Mod.GotoXY(Mod.getX(2), Mod.getY(2));
        //    }
        //    else
        //    {
        //        Mod.GotoXY(TileMap.pxw - 30, PickMobController.GetYsd(TileMap.pxw - 30));
        //    }
        //    text = "";
        //}
        //if (text == "nmg")
        //{
        //    if (Mod.getX(1) > 0 && Mod.getY(1) > 0)
        //    {
        //        Mod.GotoXY(Mod.getX(1), Mod.getY(1));
        //        Service.gI().getMapOffline();
        //        Service.gI().requestChangeMap();
        //    }
        //    else
        //    {
        //        Mod.GotoXY(TileMap.pxw / 2, PickMobController.GetYsd(TileMap.pxw / 2));
        //    }
        //    text = "";
        //}
        //if (text == "nmtr")
        //{
        //    if (Mod.getX(3) > 0 && Mod.getY(3) > 0)
        //    {
        //        Mod.GotoXY(Mod.getX(3), Mod.getY(3));
        //    }
        //    text = "";
        //}
        //if (text.StartsWith("do "))
        //{
        //    Mod.bossCanDo = text.Replace("do ", "");
        //    GameScr.info1.addInfo("Boss cần dò: " + Mod.bossCanDo, 0);
        //    text = "";
        //}
        //if (text.StartsWith("dk "))
        //{
        //    Mod.zoneMacDinh = int.Parse(text.Replace("dk ", ""));
        //    GameScr.info1.addInfo("Dò boss từ khu " + Mod.zoneMacDinh, 0);
        //    text = "";
        //}
        //if (text == "clrz")
        //{
        //    Mod.zoneMacDinh = 0;
        //    GameScr.info1.addInfo("Reset khu dò boss xuống", 0);
        //    text = "";
        //}
        //if (text == "doall")
        //{
        //    Mod.doBoss = !Mod.doBoss;
        //    GameScr.info1.addInfo("Dò boss: " + (Mod.doBoss ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "ksbs5")
        //{
        //    Mod.isKSBoss = false;
        //    Mod.isKSBossBangSkill5 = !Mod.isKSBossBangSkill5;
        //    GameScr.info1.addInfo((Mod.isKSBossBangSkill5 ? "KS Boss Bằng Skill 5: Bật" : "KS Boss Bằng Skill 5: Tắt") ?? "", 0);
        //    text = "";
        //}
        //if (text == "ksb")
        //{
        //    Mod.isKSBossBangSkill5 = false;
        //    Mod.isKSBoss = !Mod.isKSBoss;
        //    GameScr.info1.addInfo((Mod.isKSBoss ? "KS Boss bằng đấm thường: Bật" : "KS Boss bằng đấm thường: Tắt") ?? "", 0);
        //    text = "";
        //}
        //if (text.StartsWith("hpboss "))
        //{
        //    Mod.HPKSBoss = int.Parse(text.Replace("hpboss ", ""));
        //    GameScr.info1.addInfo("HP Boss khi đạt " + NinjaUtil.getMoneys((long)Mod.HPKSBoss) + " sẽ oánh bỏ con mẹ boss", 0);
        //    text = "";
        //}
        //if (text == "ttsp")
        //{
        //    Mod.isThongTinSuPhu = !Mod.isThongTinSuPhu;
        //    GameScr.info1.addInfo("Thông Tin Sư Phụ: " + (Mod.isThongTinSuPhu ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "ttdt")
        //{
        //    Mod.isThongTinDeTu = !Mod.isThongTinDeTu;
        //    GameScr.info1.addInfo("Thông Tin Đệ Tử: " + (Mod.isThongTinDeTu ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "xtb")
        //{
        //    Mod.xoaTauBay = !Mod.xoaTauBay;
        //    GameScr.info1.addInfo("Xóa tàu bay: " + (Mod.xoaTauBay ? "Tắt" : "Bật"), 0);
        //    text = "";
        //}
        //if (text == "xht")
        //{
        //    Mod.xoaHieuUngHopThe = !Mod.xoaHieuUngHopThe;
        //    GameScr.info1.addInfo("Hiệu ứng hợp thể: " + (Mod.xoaHieuUngHopThe ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "kvt")
        //{
        //    Mod.ghimX = Char.myCharz().cx;
        //    Mod.ghimY = Char.myCharz().cy;
        //    Mod.isKhoaViTri = !Mod.isKhoaViTri;
        //    GameScr.info1.addInfo("Khóa vị trí: " + (Mod.isKhoaViTri ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "ttnv")
        //{
        //    Mod.isBossM = false;
        //    Mod.isPKM = false;
        //    Mod.trangThai = !Mod.trangThai;
        //    GameScr.info1.addInfo("Trạng thái nhân vật đang trỏ: " + (Mod.trangThai ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}

        //if (text == "pkm")
        //{
        //    Mod.isPKM = !Mod.isPKM;
        //    Mod.isBossM = false;
        //    Mod.trangThai = false;
        //    GameScr.info1.addInfo("Bọn đấm nhau được trong khu: " + (Mod.isPKM ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "gdl")
        //{
        //    Mod.giamDungLuong = !Mod.giamDungLuong;
        //    GameScr.info1.addInfo("Giảm dung lượng: " + (Mod.giamDungLuong ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "xoamap")
        //{
        //    Mod.xoamap = !Mod.xoamap;
        //    GameScr.info1.addInfo("Xóa map: " + (Mod.xoamap ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "gmt")
        //{
        //    Mod.isGMT = false;
        //    text = "";
        //}
        //if (text.StartsWith("gmt "))
        //{
        //    int num = int.Parse(text.Remove(0, 4));
        //    if (num < GameScr.vCharInMap.size())
        //    {
        //        Mod.isGMT = true;
        //        Mod.charMT = (Char)GameScr.vCharInMap.elementAt(num);
        //    }
        //    text = "";
        //}
        //if (text == "abt")
        //{
        //    Mod.isAutoBT = !Mod.isAutoBT;
        //    GameScr.info1.addInfo("Auto bông tai: " + (Mod.isAutoBT ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text.StartsWith("bt "))
        //{
        //    Mod.timeBT = int.Parse(text.Replace("bt ", ""));
        //    GameScr.info1.addInfo("Delay auto bông tai: " + Mod.timeBT + "s", 0);
        //    text = "";
        //}
        //if (text == "anz")
        //{
        //    Mod.isAutoNhatXa = !Mod.isAutoNhatXa;
        //    if (Mod.isAutoNhatXa)
        //    {
        //        Mod.xNhatXa = Char.myCharz().cx;
        //        Mod.yNhatXa = Char.myCharz().cy;
        //        GameScr.info1.addInfo(string.Concat(new object[]
        //        {
        //            "Tọa Độ : ",
        //            Char.myCharz().cx,
        //            "|",
        //            Char.myCharz().cy
        //        }), 0);
        //    }
        //    GameScr.info1.addInfo("Auto Nhặt Xa : " + (Mod.isAutoNhatXa ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text.StartsWith("ndc "))
        //{
        //    Mod.textAutoChat = text.Replace("ndc ", "");
        //    GameScr.info1.addInfo("Nội dung auto chat : " + Mod.textAutoChat, 0);
        //    text = "";
        //}
        //if (text.StartsWith("ndctg "))
        //{
        //    Mod.textAutoChatTG = text.Replace("ndc ", "");
        //    GameScr.info1.addInfo("Nội dung auto chat thế giới : " + Mod.textAutoChatTG, 0);
        //    text = "";
        //}
        //if (text == "atchattg")
        //{
        //    Mod.isAutoCTG = !Mod.isAutoCTG;
        //    GameScr.info1.addInfo("Auto Chat Thế Giới: " + (Mod.isAutoCTG ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text == "atc")
        //{
        //    Mod.achat = !Mod.achat;
        //    GameScr.info1.addInfo("Auto chat : " + (Mod.achat ? "Bật" : "Tắt"), 0);
        //    text = string.Empty;
        //}
        //if (text.StartsWith("go "))
        //{
        //    int num2 = int.Parse(text.Remove(0, 3));
        //    if (num2 < GameScr.vCharInMap.size())
        //    {
        //        Char @char = (Char)GameScr.vCharInMap.elementAt(num2);
        //        Mod.GotoXY(@char.cx, @char.cy);
        //        Char.myCharz().focusManualTo(@char);
        //    }
        //    text = "";
        //}
        //if (text == "showhp")
        //{
        //    Mod.nvat = !Mod.nvat;
        //    GameScr.info1.addInfo("Thông tin người chơi trong map: " + (Mod.nvat ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        //if (text.StartsWith("kx "))
        //{
        //    new Thread(new ParameterizedThreadStart(Mod.VaoKhu)).Start(int.Parse(text.Remove(0, 3)));
        //    text = "";
        //}
        //if (text.StartsWith("tdc "))
        //{
        //    Mod.tocdochay = int.Parse(text.Replace("tdc ", ""));
        //    GameScr.info1.addInfo("Tốc độ phóng: " + Mod.tocdochay, 0);
        //    text = "";
        //}
        //if (text == "dapdo")
        //{
        //    Mod.isDapDo = !Mod.isDapDo;
        //    new Thread(new ThreadStart(Mod.AutoDapDo)).Start();
        //    GameScr.info1.addInfo("Đập đồ: " + (Mod.isDapDo ? "Bật" : "Tắt"), 0);
        //    text = "";
        //}
        return false;
    }

    public void MyChatTextField(ChatTextField chatTField, string strChat, string strName)
    {
        chatTField.strChat = strChat;
        chatTField.tfChat.name = strName;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.isFocus = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
        chatTField.tfChat.setMaxTextLenght(10);
        if (!Main.isPC)
        {
            chatTField.startChat(GameCanvas.panel, string.Empty);
        }
        else if (GameCanvas.isTouch)
        {
            chatTField.tfChat.doChangeToTextBox();
        }
    }

    private void LoadSkillToScreen()
    {
        for (int i = 0; i < Char.myCharz().vSkill.size(); i++)
        {
            Skill skill = (Skill)Char.myCharz().vSkill.elementAt(i);

            if (GameCanvas.isTouch && !Main.isPC)
            {
                for (int j = 0; j < GameScr.onScreenSkill.Length; j++)
                {
                    if (GameScr.onScreenSkill[j] == skill)
                    {
                        GameScr.onScreenSkill[j] = null;
                    }
                }
                GameScr.onScreenSkill[i] = skill;
                GameScr.gI().saveonScreenSkillToRMS();
            }
            else
            {
                for (int k = 0; k < GameScr.keySkill.Length; k++)
                {
                    if (GameScr.keySkill[k] == skill)
                    {
                        GameScr.keySkill[k] = null;
                    }
                }
                GameScr.keySkill[i] = skill;
                GameScr.gI().saveKeySkillToRMS();
            }
        }
    }

    public static void DoChatGlobal()
    {
        GameCanvas.endDlg();
        if (Char.myCharz().checkLuong() < 5)
        {
            GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
            return;
        }
        if (GameCanvas.panel.chatTField == null)
        {
            GameCanvas.panel.chatTField = new ChatTextField();
            GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
            GameCanvas.panel.chatTField.initChatTextField();
            GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
        }
        GameCanvas.panel.chatTField.strChat = mResources.world_channel_5_luong;
        GameCanvas.panel.chatTField.tfChat.name = mResources.CHAT;
        GameCanvas.panel.chatTField.to = string.Empty;
        GameCanvas.panel.chatTField.isShow = true;
        GameCanvas.panel.chatTField.tfChat.isFocus = true;
        GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
        if (Main.isWindowsPhone)
        {
            GameCanvas.panel.chatTField.tfChat.strInfo = GameCanvas.panel.chatTField.strChat;
        }
        if (!Main.isPC)
        {
            GameCanvas.panel.chatTField.startChat(GameCanvas.panel, string.Empty);
        }
        else if (GameCanvas.isTouch)
        {
            GameCanvas.panel.chatTField.tfChat.doChangeToTextBox();
        }
    }

}
