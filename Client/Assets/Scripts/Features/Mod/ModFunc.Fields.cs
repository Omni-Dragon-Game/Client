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
    private static readonly ModFunc Instance = new();

    public static string homeUrl = "Omni Dragon Online";

   // public static string ipString = "74-55-43-5C-16-08-0C-08-02-17-04-0B-03-17-04-09-05-17-04-0D-04-03-05-0E-05-0F-0C-09-1A-7B-5A-4C-53-19-04-03-07-0D-18-0B-04-0C-18-0B-06-0A-18-0B-02-0B-0C-0A-01-0A-01-03-06-15-74-55-43-5C-16-0A-0C-08-02-17-04-0B-03-17-04-09-05-17-04-0D-04-03-05-0E-05-01-0C-09-1A-7B-5A-4C-53-19-62-7C-65-6D-0C-08-02-17-04-0B-03-17-04-09-05-17-04-0D-04-03-05-01-03-00-0C-09-1A-09-1A-09";

  /*  public static string ipString = Decrypt("勂勞勩劢勋勄劲勩勀勛劶务劽勛劺勪势勄劥勪劾劵劾勦劾勛勂勪勀勛劾劤劾勫勊劧劾労勩労勓効勇勝劺劵劺劧劾勅勂勦劾勛劺劢劽勛劺勨劾勪劥勪势劵劺劧劾勫勔勫势勫勠勨劽劶劻勤動勈勆勘劾勫勠勩势労劥勪劾勛勆勦劾勛劲勫劽勛劺务劾勛勠勫势勫劾劥勀勛劲勤勂勞勩劢勋勄劳勆勃勇势勆勀勛劶务劽勛劺勪势勄劥勪劾劵劾勦劾勛勂勪勀勛劾劥势勅勜劧劾労勨勨劽劵劲劮", 742001);
    //protected static string IP = "Ngọc Rồng Hà Nội:14.225.213.148:8888:0,0,0";
    public static string Decrypt(string encryptedText, int key)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in encryptedText)
        {
            stringBuilder.Append((char)(c - key));
        }

        byte[] bytes = Convert.FromBase64String(stringBuilder.ToString());
        return Encoding.UTF8.GetString(bytes);
    }*/

    // For debug only
    private static bool isDebugEnable = false;

    private static long lastTimeLog = 0;

    public bool canUpdate = false;

    private long lastAutoAttack = 0;

    private readonly List<Skill> listSkillsAuto = new();

    public List<ItemAuto> listItemAuto = new();

    public static bool notifBoss = false;

    private bool lineToBoss;

    private bool focusBoss = false;

    private long lastFocusBoss;

    public static MyVector bossNotif = new();

    private long lastUpdateZones = 0;

    public bool isUpdateZones = false;

    public bool userOpenZones = false;

    public bool showCharsInMap = true;

    public MyVector charsInMap = new();

    public bool showInfoMap = false;

    public bool showInfoMe = false;

    private long lastUpdateInfoMe = 0;

    public bool autoAttack = false;

    public bool autoWakeUp = false;

    public long lastAutoWakeUp = 0;

    public bool isAutoPhaLe = false;

    public Item itemPhale;

    public int maxPhale = -1;

    public int currPhale = -1;

    public static bool isAutoLogin = false;

    public static AutoLogin autoLogin;

    public bool isAutoVQMM = false;

    public bool isOpenThuongDe = false;

    public bool isCollectAll = false;

    public bool isPaintThuongDe = false;

    public long lastVQMM;

    public bool isShowButton = true;

    public bool isIntroOff = false;

    public bool isHighFps;

    private int paramIntrinsic = -1;

    public string curSelectIntrinsic = "";

    public static string strAddAutoItem = "Thêm vào\nAutoItem";

    public static string strRemoveAutoItem = "Xoá khỏi\nAutoItem";

    public static string strTeleportTo = "Dịch\nchuyển tới";

    public static string strAutoBuy = "Mua 20 lần";

    public static string strChooseIntrinsic = "Chọn chỉ số";

    public static string strInCrease = "Tăng\ntới\nmức";

    public static string[] strPointTypes = { "HP", "MP", "Sức Đánh", "Giáp", "Chí mạng" };

    public static string strAccManager = "Q.L.T.K";

    public static string strModFunc = "Chức Năng MOD";

    public static string strUpdateZones = "Cập Nhật Khu";

    public static string strCharsInMap = "Nhân Vật Trong Khu";

    public static string strInfoMe = "Thông Tin Bản Thân";

    public static string strAutoPhaLe = "Tự Động Pha Lê Hóa";

    public static string strAutoVQMM = "Tự Động VQMM";

    public static string strAutoWakeUp = "Tự Động Hồi Sinh";

    public static string strAutoLogin = "Tự Động Đăng Nhập";

    public static string strShowButton = "Hiện Nút Trợ Năng";

    public static string strIntroOff = "Tắt Intro";

    public static string strHighFps = "FPS Cao";

    public static string strClickToChat = " [Ấn để chat]";

    public static string strPlayerInfo = "Thông tin player";

    public static string strPet2 = "Người iuu";

    public static string strUseForPet2 = "Sử dụng\ncho\nNg.iuu";

    public MyVector listNotifTichXanh = new();

    private bool startChat = false;

    private int xNotif;

    private long lastUpdateNotif;

    public bool isPeanPet = false;

    private long lastPeanPet = 0;

    public static int indexAutoPoint = -1;

    public static int pointIncrease = 0;

    public static bool autoPointForPet = false;

    private static int modKeyPosX;

    private static int modKeyPosY;

    public static Command cmdAccManager;

    public static bool isOpenAccMAnager = false;

    public static List<Account> accounts = new();

    public List<Command> cmdsChooseAcc = new();

    public List<Command> cmdsDelAcc = new();

    public static Command cmdCloseAccManager;

    public static bool startAutoItem = false;

    private static bool isAutoChat = false;

    private static string textAutoChat = string.Empty;

    private static bool isAutoChatTG = false;

    private static string textAutoChatTG = string.Empty;

    private long lastAutoChat = 0;

    private long lastAutoChatTG = 0;

    public static string ipServer = "Đổi IP";

    public static bool userOpenPet = false;

    public static bool isLockFocus = false;

    private long lastUpdateFPS;

    private static int lastFps;

    public static Image[] ticks = new Image[20];
    private static Image logo = new Image();
    private static Image[] logos = new Image[60];

    public static Image imgLogoBig = null;

    public static Image imgBg = null;

    public static bool isReadInt = true;

    public static int musicCount = 0;

    public static bool loadedMusic = false;

    public static bool isPlayingMusic = false;

    public static List<AudioClip> musics = new();

    public static ModFunc GI()
    {
        return Instance ?? new();
    }

    public static void DoEncodeIp()
    {
        //Debug.Log(EncodeStringToByteArrayString("Blue 1:14.225.203.242:3736:0,Blue 2:14.225.203.242:3737:0,Blue 3:14.225.203.242:3738:0,Blue TEST:14.225.203.242:3859:0,0,0", "69"));
    }

}
