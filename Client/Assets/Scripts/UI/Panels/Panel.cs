using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Assets.src.g;
using Mod;
using Mod.XMAP;
using UnityEngine;

public partial class Panel : IActionListener, IChatable
{
    // PlayerChat() extracted to Panel.Social.cs

    public bool isShow;

    public int X;

    public int Y;

    public int W;

    public int H;

    public int ITEM_HEIGHT;

    public int TAB_W;

    public int TAB_W_NEW;

    public int cmtoY;

    public int cmy;

    public int cmdy;

    public int cmvy;

    public int cmyLim;

    public int xc;

    public int[] cmyLast;

    public int cmtoX;

    public int cmx;

    public int cmxLim;

    public int cmxMap;

    public int cmyMap;

    public int cmxMapLim;

    public int cmyMapLim;

    public int cmyQuest;

    public static Image imgBantay;

    public static Image imgX;

    public static Image imgMap;

    public TabClanIcon tabIcon;

    public MyVector vItemCombine = new MyVector();

    public int moneyGD;

    public int friendMoneyGD;

    public bool isLock;

    public bool isFriendLock;

    public bool isAccept;

    public bool isFriendAccep;

    public string topName;

    public ChatTextField chatTField;

    public static string specialInfo;

    public static short spearcialImage;

    public static Image imgStar;

    public static Image imgMaxStar;

    public static Image imgStar8;

    public static Image imgNew;

    public static Image imgXu;

    public static Image imgThoivang;

    public static Image imgTicket;

    public static Image imgLuong;

    public static Image imgLuongKhoa;

    private static Image imgUp;

    private static Image imgDown;

    private int pa1;

    private int pa2;

    private bool trans;

    private int pX;

    private int pY;

    private Command left = new Command(mResources.SELECT, 0);

    public int type;

    public int currentTabIndex;

    public int startTabPos;

    public int[] lastTabIndex;

    public string[][] currentTabName;

    private int[] currClanOption;

    public int mainTabPos = 4;

    public int shopTabPos = 50;

    public int boxTabPos = 50;

    public string[][] mainTabName;

    public string[] mapNames;

    public string[] planetNames;

    public static string[] strTool = new string[7]
    {
        mResources.gameInfo,
        mResources.change_flag,
        mResources.change_zone,
        mResources.chat_world,
        mResources.account,
        mResources.option,
        mResources.change_account
    };

    public static string[] strCauhinh = new string[4]
    {
        (!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
        mResources.increase_vga,
        mResources.analog,
        (mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
    };

    public static string[] strModFunc = new string[1]
    {
        ""
    };

    public static string[] strAccount = new string[5]
    {
        mResources.inventory_Pass,
        mResources.friend,
        mResources.enemy,
        mResources.msg,
        mResources.charger
    };

    public static string[] strAuto = new string[1] { mResources.useGem };

    public static int graphics = 0;

    public string[][] shopTabName;

    public int[] maxPageShop;

    public int[] currPageShop;

    private static string[][] boxTabName = new string[2][]
    {
        mResources.chestt,
        mResources.inventory
    };

    private static string[][] boxCombine = new string[2][]
    {
        mResources.combine,
        mResources.inventory
    };

    private static string[][] boxZone = new string[1][] { mResources.zonee };

    private static string[][] boxMap = new string[1][] { mResources.mapp };

    private static string[][] boxGD = new string[3][]
    {
        mResources.inventory,
        mResources.item_give,
        mResources.item_receive
    };

    private static string[][] boxPet = mResources.petMainTab;

    public string[][][] tabName = new string[29][][]
    {
        null,
        null,
        boxTabName,
        boxZone,
        boxMap,
        null,
        null,
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        boxCombine,
        boxGD,
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        boxPet,
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
        new string[1][] { new string[1] { string.Empty } },
         new string[1][] { new string[1] { string.Empty } },
         boxPet
    };

    private static readonly sbyte BOX_BAG = 0;

    private static readonly sbyte BAG_BOX = 1;

    private static readonly sbyte BODY_BOX = 3;

    private static readonly sbyte BAG_BODY = 4;

    private static readonly sbyte BODY_BAG = 5;

    private static readonly sbyte BAG_PET = 6;

    private static readonly sbyte PET_BAG = 7;

    private static readonly sbyte BAG_PET2 = 8;

    private static readonly sbyte PET2_BAG = 9;

    public int hasUse;

    public int hasUseBag;

    public int currentListLength;

    private int[] lastSelect;

    public static int[] mapIdTraidat = new int[16]
    {
        21, 0, 1, 2, 24, 3, 4, 5, 6, 27,
        28, 29, 30, 42, 47, 46
    };

    public static int[] mapXTraidat = new int[16]
    {
        39, 42, 105, 93, 61, 93, 142, 165, 210, 100,
        165, 220, 233, 10, 125, 125
    };

    public static int[] mapYTraidat = new int[16]
    {
        28, 60, 48, 96, 88, 131, 136, 95, 32, 200,
        189, 167, 120, 110, 20, 20
    };

    public static int[] mapIdNamek = new int[14]
    {
        22, 7, 8, 9, 25, 11, 12, 13, 10, 31,
        32, 33, 34, 43
    };

    public static int[] mapXNamek = new int[14]
    {
        55, 30, 93, 80, 24, 149, 219, 220, 233, 170,
        148, 195, 148, 10
    };

    public static int[] mapYNamek = new int[14]
    {
        136, 84, 69, 34, 25, 42, 32, 110, 192, 70,
        106, 156, 210, 57
    };

    public static int[] mapIdSaya = new int[14]
    {
        23, 14, 15, 16, 26, 17, 18, 20, 19, 35,
        36, 37, 38, 44
    };

    public static int[] mapXSaya = new int[14]
    {
        90, 95, 144, 234, 231, 122, 176, 158, 205, 54,
        105, 159, 231, 27
    };

    public static int[] mapYSaya = new int[14]
    {
        10, 43, 20, 36, 69, 87, 112, 167, 160, 151,
        173, 207, 194, 29
    };

    public static int[][] mapId = new int[3][] { mapIdTraidat, mapIdNamek, mapIdSaya };

    public static int[][] mapX = new int[3][] { mapXTraidat, mapXNamek, mapXSaya };

    public static int[][] mapY = new int[3][] { mapYTraidat, mapYNamek, mapYSaya };

    public Item currItem;

    public Clan currClan;

    public ClanMessage currMess;

    public Member currMem;

    public Clan[] clans;

    public MyVector member;

    public MyVector myMember;

    public MyVector logChat = new MyVector();

    public MyVector vPlayerMenu = new MyVector();

    public MyVector vFriend = new MyVector();

    public MyVector vMyGD = new MyVector();

    public MyVector vFriendGD = new MyVector();

    public MyVector vTop = new MyVector();

    public MyVector vEnemy = new MyVector();

    public MyVector vFlag = new MyVector();

    public MyVector vPlayerMenu_id = new MyVector();

    public Command cmdClose;

    public static bool CanNapTien = false;

    public static int WIDTH_PANEL = 240;

    private int position;

    public string playerChat;

    public Dictionary<string, PlayerChat> chats = new Dictionary<string, PlayerChat>();

    public Char charMenu;

    private bool isThachDau;

    public int typeShop = -1;

    public int xScroll;

    public int yScroll;

    public int wScroll;

    public int hScroll;

    public ChatPopup cp;

    public int idIcon;

    public int[] partID;

    private int timeShow;

    public bool isBoxClan;

    public int w;

    private int pa;

    public int selected;

    private int cSelected;

    private int newSelected;

    private bool isClanOption;

    public bool isSearchClan;

    public bool isMessage;

    public bool isViewMember;

    public const int TYPE_MAIN = 0;

    public const int TYPE_SHOP = 1;

    public const int TYPE_BOX = 2;

    public const int TYPE_ZONE = 3;

    public const int TYPE_MAP = 4;

    public const int TYPE_CLANS = 5;

    public const int TYPE_INFOMATION = 6;

    public const int TYPE_BODY = 7;

    public const int TYPE_MESS = 8;

    public const int TYPE_ARCHIVEMENT = 9;

    public const int PLAYER_MENU = 10;

    public const int TYPE_FRIEND = 11;

    public const int TYPE_COMBINE = 12;

    public const int TYPE_GIAODICH = 13;

    public const int TYPE_MAPTRANS = 14;

    public const int TYPE_TOP = 15;

    public const int TYPE_ENEMY = 16;

    public const int TYPE_KIGUI = 17;

    public const int TYPE_FLAG = 18;

    public const int TYPE_OPTION = 19;

    public const int TYPE_ACCOUNT = 20;

    public const int TYPE_PET_MAIN = 21;

    public const int TYPE_AUTO = 22;

    public const int TYPE_GAMEINFO = 23;

    public const int TYPE_GAMEINFOSUB = 24;

    public const int TYPE_SPEACIALSKILL = 25;

    private int pointerDownTime;

    private int pointerDownFirstX;

    private int[] pointerDownLastX = new int[3];

    private bool pointerIsDowning;

    private bool isDownWhenRunning;

    private bool wantUpdateList;

    private int waitToPerform;

    private int cmRun;

    private int keyTouchLock = -1;

    private int keyToundGD = -1;

    private int keyTouchCombine = -1;

    private int keyTouchMapButton = -1;

    public int indexMouse = -1;

    private bool justRelease;

    private int keyTouchTab = -1;

    private int nTableItem;

    public string[][] clansOption = new string[2][]
    {
        mResources.findClan,
        mResources.createClan
    };

    public string clanInfo = string.Empty;

    public string clanReport = string.Empty;

    private bool isHaveClan;

    private Scroll scroll;

    private int cmvx;

    private int cmdx;

    private bool isSelectPlayerMenu;

    private string[] strStatus = new string[6]
    {
        mResources.follow,
        mResources.defend,
        mResources.attack,
        mResources.gohome,
        mResources.fusion,
        mResources.fusionForever
    };

    private static string log;

    private int tt;

    private int currentButtonPress;

    public static long[] t_tiemnang = new long[14]
    {
        50000000L, 250000000L, 1250000000L, 5000000000L, 15000000000L, 30000000000L, 45000000000L, 60000000000L, 75000000000L, 90000000000L,
        110000000000L, 130000000000L, 150000000000L, 170000000000L
    };

    private int[] zoneColor = new int[3] { 43520, 14743570, 14155776 };

    public string[] combineInfo;

    public string[] combineTopInfo;

    public static int[] color1 = new int[3] { 2327248, 8982199, 16713222 };

    public static int[] color2 = new int[3] { 4583423, 16719103, 16714764 };

    private int sellectInventory;

    private Item itemInvenNew;

    private Effect eBanner;

    private static FrameImage screenTab6;

    private bool isUp;

    private int compare;

    public static string strWantToBuy = string.Empty;

    public int xstart;

    public int ystart;

    public int popupW = 140;

    public int popupH = 160;

    public int cmySK;

    public int cmtoYSK;

    public int cmdySK;

    public int cmvySK;

    public int cmyLimSK;

    public int popupY;

    public int popupX;

    public int isborderIndex;

    public int isselectedRow;

    public int indexSize = 28;

    public int indexTitle;

    public int indexSelect;

    public int indexRow = -1;

    public int indexRowMax;

    public int indexMenu;

    public int columns = 6;

    public int rows;

    public int inforX;

    public int inforY;

    public int inforW;

    public int inforH;

    private int yPaint;

    private int xMap;

    private int yMap;

    private int xMapTask;

    private int yMapTask;

    private int xMove;

    private int yMove;

    public static bool isPaintMap = true;

    public bool isClose;

    private int infoSelect;

    public static MyVector vGameInfo = new MyVector(string.Empty);

    public static string[] contenInfo;

    public bool isViewChatServer;

    private int currInfoItem;

    public Char charInfo;

    private bool isChangeZone;

    private bool isKiguiXu;

    private bool isKiguiLuong;

    private int delayKigui;

    public sbyte combineSuccess = -1;

    public int idNPC;

    public int xS;

    public int yS;

    private int rS;

    private int angleS;

    private int angleO;

    private int iAngleS;

    private int iDotS;

    private int speed;

    private int[] xArgS;

    private int[] yArgS;

    private int[] xDotS;

    private int[] yDotS;

    private int time;

    private int typeCombine;

    private int countUpdate;

    private int countR;

    private int countWait;

    private bool isSpeedCombine;

    private bool isCompleteEffCombine = true;

    private bool isPaintCombine;

    public bool isDoneCombine = true;

    public short iconID1;

    public short iconID2;

    public short iconID3;

    public short[] iconID;

    public string[][] speacialTabName;

    public static int[] sizeUpgradeEff = new int[3] { 2, 1, 1 };

    public static int nsize = 1;

    public const sbyte COLOR_WHITE = 0;

    public const sbyte COLOR_GREEN = 1;

    public const sbyte COLOR_PURPLE = 2;

    public const sbyte COLOR_ORANGE = 3;

    public const sbyte COLOR_BLUE = 4;

    public const sbyte COLOR_YELLOW = 5;

    public const sbyte COLOR_RED = 6;

    public const sbyte COLOR_BLACK = 7;

    public static int[][] colorUpgradeEffect = new int[7][]
    {
        new int[6] { 16777215, 15000805, 13487823, 11711155, 9671828, 7895160 },
        new int[6] { 61952, 58624, 52224, 45824, 39168, 32768 },
        new int[6] { 13500671, 12058853, 10682572, 9371827, 7995545, 6684800 },
        new int[6] { 16744192, 15037184, 13395456, 11753728, 10046464, 8404992 },
        new int[6] { 37119, 33509, 28108, 24499, 21145, 17536 },
        new int[6] { 16776192, 15063040, 12635136, 11776256, 10063872, 8290304 },
        new int[6] { 16711680, 15007744, 13369344, 11730944, 10027008, 8388608 }
    };

    public const int color_item_white = 15987701;

    public const int color_item_green = 2786816;

    public const int color_item_purple = 7078041;

    public const int color_item_orange = 12537346;

    public const int color_item_blue = 1269146;

    public const int color_item_yellow = 13279744;

    public const int color_item_red = 11599872;

    public const int color_item_black = 2039326;

    private Image imgo_0;
    private Image imgo_00;

    private Image imgo_1;

    private Image imgo_2;

    private Image imgo_3;

    private Image imgo_4;

    private Image imgo_5;

    private Image imgo_6;
    private Image imgo_7;
    private Image imgo_8;
    private Image imgo_10;
    private Image imgo_11;
    private Image imgo_12;
    private Image imgo_13;
    private Image imgo_14;
    private Image imgo_15;
    private Image imgo_16;


    private Image imgo_17;
    private Image imgo_18; 
    private Image imgo_19;

    public const int numItem = 20;

    public const sbyte INVENTORY_TAB = 1;

    public sbyte size_tab;

    private bool isnewInventory;

    private static Image[] bgcam = new Image[8], bgdo = new Image[8], bgxanhla = new Image[8], bgtim = new Image[8], bgxanhnhat = new Image[8], bgxanhdam = new Image[8];
    private static Image[] effcam = new Image[8], effdo = new Image[8], effxanhla = new Image[8], efftim = new Image[8], effxanhnhat = new Image[8], effxanhdam = new Image[8];

    public Panel()
    {
        init();
        cmdClose = new Command(string.Empty, this, 1003, null);
        cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
        cmdClose.cmdClosePanel = true;
        currItem = null;
    }

    public static void loadBg()
    {
        imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID + ".png");
        imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
        imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
        imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
        imgThoivang = GameCanvas.loadImage("/mainImage/thoivang.png");
        imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
        imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
        imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
        imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
        imgStar = GameCanvas.loadImage("/mainImage/star.png");
        imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
        imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
        imgNew = GameCanvas.loadImage("/mainImage/new.png");
        imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
        for(int i = 0; i < 8; i++)
        {
            if (bgcam[i] == null) bgcam[i] = GameCanvas.loadEffect("/cam/bg/" + i);
            if (bgdo[i] == null) bgdo[i] = GameCanvas.loadEffect("/do/bg/" + i);
            if (bgtim[i] == null) bgtim[i] = GameCanvas.loadEffect("/tim/bg/" + i );
            if (bgxanhdam[i] == null) bgxanhdam[i] = GameCanvas.loadEffect("/xanhdam/bg/" + i );
            if (bgxanhla[i] == null) bgxanhla[i] = GameCanvas.loadEffect("/xanhla/bg/" + i );
            if (bgxanhnhat[i] == null) bgxanhnhat[i] = GameCanvas.loadEffect("/xanhnhat/bg/" + i);


            if (effcam[i] == null) effcam[i] = GameCanvas.loadEffect("/cam/eff/" + i );
            if (effdo[i] == null) effdo[i] = GameCanvas.loadEffect("/do/eff/" + i );
            if (efftim[i] == null) efftim[i] = GameCanvas.loadEffect("/tim/eff/" + i);
            if (effxanhdam[i] == null) effxanhdam[i] = GameCanvas.loadEffect("/xanhdam/eff/" + i);
            if (effxanhla[i] == null)effxanhla[i] = GameCanvas.loadEffect("/xanhla/eff/" + i );
            if (effxanhnhat[i] == null) effxanhnhat[i] = GameCanvas.loadEffect("/xanhnhat/eff/" + i);
        }
    }
    // paintEffectItem() extracted to Panel.ItemRender.cs
    public void init()
    {
        pX = GameCanvas.pxLast + cmxMap;
        pY = GameCanvas.pyLast + cmyMap;
        lastTabIndex = new int[tabName.Length];
        for (int i = 0; i < lastTabIndex.Length; i++)
        {
            lastTabIndex[i] = -1;
        }
    }

    // getXMap() extracted to Panel.ZoneMap.cs

    // getYMap() extracted to Panel.ZoneMap.cs

    // getXMapTask() extracted to Panel.ZoneMap.cs

    // getYMapTask() extracted to Panel.ZoneMap.cs

    private void setType(int position)
    {
        typeShop = -1;
        W = WIDTH_PANEL;
        H = GameCanvas.h;
        X = 0;
        Y = 0;
        ITEM_HEIGHT = 24;
        this.position = position;
        switch (position)
        {
            case 0:
                xScroll = 2;
                yScroll = 80;
                wScroll = W - 4;
                hScroll = H - 96;
                cmx = wScroll;
                cmtoX = 0;
                X = 0;
                break;
            case 1:
                wScroll = W - 4;
                xScroll = GameCanvas.w - wScroll;
                yScroll = 80;
                hScroll = H - 96;
                X = xScroll - 2;
                cmx = -(GameCanvas.w + W);
                cmtoX = GameCanvas.w - W;
                break;
        }
        currentTabIndex = 0;
        currentTabName = tabName[type];
        TAB_W = (wScroll - 2) / ((currentTabName != null && currentTabName.Length > 0) ? currentTabName.Length : 5);
        startTabPos = xScroll + wScroll / 2 - currentTabName.Length * TAB_W / 2;
        lastSelect = new int[currentTabName.Length];
        cmyLast = new int[currentTabName.Length];
        for (int i = 0; i < currentTabName.Length; i++)
        {
            lastSelect[i] = (GameCanvas.isTouch ? (-1) : 0);
        }
        if (lastTabIndex[type] != -1)
        {
            currentTabIndex = lastTabIndex[type];
        }
        if (currentTabIndex < 0)
        {
            currentTabIndex = 0;
        }
        if (currentTabIndex > currentTabName.Length - 1)
        {
            currentTabIndex = currentTabName.Length - 1;
        }
        scroll = null;
    }

    // setTypeMapTrans() extracted to Panel.ZoneMap.cs

    public void setTypeInfomatioin()
    {
        type = 6;
        cmx = wScroll;
        cmtoX = 0;
    }

    // setTypeMap() extracted to Panel.ZoneMap.cs

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

    // setTypeKiGuiOnly() extracted to Panel.Shop.cs

    // setTabChatManager() extracted to Panel.Social.cs

    // setTabChatPlayer() extracted to Panel.Social.cs

    // setTypeChatPlayer() extracted to Panel.Social.cs

    // setTabKiGui() extracted to Panel.Shop.cs

    public void setTypeBodyOnly()
    {
        type = 7;
        setType(1);
        setTabBody(resetSelect: true);
        currentTabIndex = 0;
    }

    // addChatMessage() extracted to Panel.Social.cs

    // IsNewMessage() extracted to Panel.Social.cs

    // IsHaveNewMessage() extracted to Panel.Social.cs

    // ClearNewMessage() extracted to Panel.Social.cs

    // addPlayerMenu() extracted to Panel.Social.cs

    // setTabPlayerMenu() extracted to Panel.Social.cs

    public void setTypeFlag()
    {
        type = 18;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabFlag();
    }

    public void setTabFlag()
    {
        currentListLength = vFlag.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        if (selected > currentListLength - 1)
        {
            selected = currentListLength - 1;
        }
        cmx = (cmtoX = 0);
    }

    // setTypePlayerMenu() extracted to Panel.Social.cs

    // setTypeFriend() extracted to Panel.Social.cs

    // setTypeEnemy() extracted to Panel.Social.cs

    public void setTypeTop(sbyte t)
    {
        type = 15;
        setType(0);
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        setTabTop();
        isThachDau = t != 0;
    }

    public void setTabTop()
    {
        currentListLength = vTop.size();
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
        if (selected > currentListLength - 1)
        {
            selected = currentListLength - 1;
        }
        cmx = (cmtoX = 0);
    }

    // setTabFriend() extracted to Panel.Social.cs

    // setTabEnemy() extracted to Panel.Social.cs

    // setTypeMessage() extracted to Panel.Social.cs

    // setTypeShop() extracted to Panel.Shop.cs

    // setTypeBox() extracted to Panel.Box.cs

    // setTypeCombine() và setTabCombine() được tách sang Panel.Upgrade.cs


    public void setTypeAuto()
    {
        type = 22;
        setType(0);
        setTabAuto();
        cmx = (cmtoX = 0);
    }

    private void setTabAuto()
    {
        currentListLength = strAuto.Length;
        ITEM_HEIGHT = 29;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
    }

    // setTypePetMain() extracted to Panel.Pet.cs

    // setTypePet2Main() extracted to Panel.Pet.cs

    public void setTypeMain()
    {
        subTabInventory = 0;
        type = 0;
        setType(0);
        if (currentTabIndex == 0)
        {
            setTabTask();
        }
        if (currentTabIndex == 1)
        {
            setTabInventory(resetSelect: true);
        }
        if (currentTabIndex == 2)
        {
            setTabSkill();
        }
        if (currentTabIndex == 3)
        {
            if (mainTabName.Length == 4)
            {
                setTabTool();
            }
            else
            {
                setTabClans();
            }
        }
        if (currentTabIndex == 4)
        {
            setTabTool();
        }
    }

    // setTypeZone() extracted to Panel.ZoneMap.cs

    // Các hàm addDetail (addItemDetail, addSkillDetail, addClanDetail...) được tách sang Panel.Detail.cs


    public void show()
    {
        if (GameCanvas.isTouch)
        {
            cmdClose.x = 156;
            cmdClose.y = 3;
        }
        else
        {
            cmdClose.x = GameCanvas.w - 19;
            cmdClose.y = GameCanvas.h - 19;
        }
        cmdClose.isPlaySoundButton = false;
        ChatPopup.currChatPopup = null;
        InfoDlg.hide();
        timeShow = 20;
        isShow = true;
        isClose = false;
        SoundMn.gI().panelOpen();
        if (isTypeShop())
        {
            Char.myCharz().setPartOld();
        }
    }

    // chatTFUpdateKey() extracted to Panel.Social.cs

    public void updateKey()
    {
        if ((chatTField != null && chatTField.isShow) || !GameCanvas.panel.isDoneCombine || InfoDlg.isShow)
        {
            return;
        }
        if (tabIcon != null && tabIcon.isShow)
        {
            tabIcon.updateKey();
        }
        else
        {
            if (isClose || !isShow)
            {
                return;
            }
            if (cmdClose.isPointerPressInside())
            {
                cmdClose.performAction();
                return;
            }
            if (GameCanvas.keyPressed[13])
            {
                if (type != 4)
                {
                    hide();
                    return;
                }
                setTypeMain();
                cmx = (cmtoX = 0);
            }
            if (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
            {
                if (left.idAction > 0)
                {
                    perform(left.idAction, left.p);
                }
                else
                {
                    waitToPerform = 2;
                }
            }
            if (Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(X, Y, W, H) && !pointerIsDowning)
            {
                hide();
                return;
            }
            if (!isClanOption)
            {
                updateKeyInTabBar();
            }
            switch (type)
            {
                case 3:
                case 8:
                case 9:
                case 10:
                case 11:
                case 14:
                case 15:
                case 16:
                case 18:
                case 23:
                case 24:
                case 26:
                case 27:
                    updateKeyScrollView();
                    break;
                case 21:
                case 28:
                    if (currentTabIndex == 0 || currentTabIndex == 2)
                    {
                        updateKeyScrollView();
                    }
                    else if (currentTabIndex == 1)
                    {
                        updateKeyPetStatus();
                    }
                    break;
                case 0:
                    if (currentTabIndex == 0)
                    {
                        updateKeyQuest();
                        GameCanvas.clearKeyPressed();
                        return;
                    }
                    if (currentTabIndex == 1)
                    {
                        updateKeyInventory();
                    }
                    if (currentTabIndex == 2)
                    {
                        updateKeySkill();
                    }
                    if (currentTabIndex == 3)
                    {
                        if (mainTabName.Length == 4)
                        {
                            updateKeyTool();
                        }
                        else
                        {
                            updateKeyClans();
                        }
                    }
                    if (currentTabIndex == 4)
                    {
                        updateKeyTool();
                    }
                    break;
                case 2:
                    updateKeyInventory();
                    break;
                case 1:
                case 17:
                case 25:
                    if (currentTabIndex < currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && type != 17)
                    {
                        updateKeyScrollView();
                    }
                    else if (typeShop == 0)
                    {
                        updateKeyInventory();
                    }
                    else
                    {
                        updateKeyScrollView();
                    }
                    break;
                case 4:
                    updateKeyMap();
                    GameCanvas.clearKeyPressed();
                    return;
                case 7:
                    updateKeyInventory();
                    break;
                case 12:
                    updateKeyCombine();
                    break;
                case 13:
                    updateKeyGiaoDich();
                    break;
                case 19:
                    updateKeyOption();
                    break;
                case 20:
                    updateKeyOption();
                    break;
                case 22:
                    updateKeyAuto();
                    break;
            }
            GameCanvas.clearKeyHold();
            for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
            {
                GameCanvas.keyPressed[i] = false;
            }
        }
    }

    private void updateKeyAuto()
    {
    }

    // updateKeyPetStatus() extracted to Panel.Pet.cs

    // updateKeyPetSkill() extracted to Panel.Pet.cs

    // keyGiaodich() extracted to Panel.Trade.cs

    // updateKeyGiaoDich() extracted to Panel.Trade.cs

    private void updateKeyTool()
    {
        updateKeyScrollView();
    }

    // updateKeySkill() extracted to Panel.Skill.cs

    // updateKeyClanIcon() extracted to Panel.Clan.cs

    // setTabGiaoDich() extracted to Panel.Trade.cs

    // setTypeGiaoDich() extracted to Panel.Trade.cs

    // paintGiaoDich() extracted to Panel.Trade.cs

    // updateKeyMap() extracted to Panel.ZoneMap.cs

    // updateKeyCombine() được tách sang Panel.Upgrade.cs


    private void updateKeyQuest()
    {
        if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
        {
            cmyQuest -= 5;
        }
        if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
        {
            cmyQuest += 5;
        }
        if (cmyQuest < 0)
        {
            cmyQuest = 0;
        }
        int num = indexRowMax * 12 - (hScroll - 60);
        if (num < 0)
        {
            num = 0;
        }
        if (cmyQuest > num)
        {
            cmyQuest = num;
        }
        if (scroll != null)
        {
            if (!GameCanvas.isTouch)
            {
                scroll.cmy = cmyQuest;
            }
            scroll.updateKey();
        }
        int num2 = xScroll + wScroll / 2 - 35;
        int num3 = ((GameCanvas.h <= 300) ? 15 : 20);
        int num4 = yScroll + hScroll - num3 - 15;
        int px = GameCanvas.px;
        int py = GameCanvas.py;
        keyTouchMapButton = -1;
        if (isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30 && (scroll == null || !scroll.pointerIsDowning))
        {
            keyTouchMapButton = 1;
            if (GameCanvas.isPointerJustRelease)
            {
                SoundMn.gI().buttonClick();
                waitToPerform = 2;
                GameCanvas.clearAllPointerEvent();
            }
        }
    }

    // getCurrClanOtion() extracted to Panel.Clan.cs

    // updateKeyClansOption() extracted to Panel.Clan.cs

    // updateKeyClans() extracted to Panel.Clan.cs

    // checkOptionSelect() extracted to Panel.Clan.cs

    public void updateScroolMouse(int a)
    {
        bool flag = false;
        if (GameCanvas.pxMouse > wScroll)
        {
            return;
        }
        if (indexMouse == -1)
        {
            indexMouse = selected;
        }
        if (a > 0)
        {
            indexMouse -= a;
            flag = true;
        }
        else if (a < 0)
        {
            indexMouse += -a;
            flag = true;
        }
        if (indexMouse < 0)
        {
            indexMouse = 0;
        }
        if (flag)
        {
            cmtoY = indexMouse * 12;
            if (cmtoY > cmyLim)
            {
                cmtoY = cmyLim;
            }
            if (cmtoY < 0)
            {
                cmtoY = 0;
            }
        }
    }

    private void updateKeyScrollView()
    {
        if (currentListLength <= 0)
        {
            return;
        }
        bool flag = false;
        if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
        {
            flag = true;
            if (isTabInven() && isnewInventory)
            {
                if (selected > 0 && sellectInventory == 0)
                {
                    selected--;
                }
            }
            else
            {
                selected--;
                if (type == 24 || type == 27)
                {
                    selected -= 2;
                    if (selected < 0)
                    {
                        selected = 0;
                    }
                }
                else if (selected < (isTabInven() ? 1 : 0))
                {
                    if (Equals(GameCanvas.panel) && typeShop == 2 && currentTabIndex <= 3 && maxPageShop[currentTabIndex] > 1)
                    {
                        InfoDlg.showWait();
                        if (currPageShop[currentTabIndex] <= 0)
                        {
                            Service.gI().kigui(4, -1, (sbyte)currentTabIndex, maxPageShop[currentTabIndex] - 1, -1);
                        }
                        else
                        {
                            Service.gI().kigui(4, -1, (sbyte)currentTabIndex, currPageShop[currentTabIndex] - 1, -1);
                        }
                        return;
                    }
                    selected = currentListLength - 1;
                    if (isClanOption)
                    {
                        selected = -1;
                    }
                    if (size_tab > 0 && !isTabInven())
                    {
                        selected = -1;
                    }
                }
                lastSelect[currentTabIndex] = selected;
                cSelected = 0;
                getCurrClanOtion();
            }
        }
        else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
        {
            flag = true;
            if (isTabInven() && isnewInventory)
            {
                if (selected < 1 && sellectInventory == 0)
                {
                    selected++;
                }
            }
            else
            {
                selected++;
                if (type == 24 || type == 27)
                {
                    selected += 2;
                    if (selected > currentListLength - 1)
                    {
                        selected = currentListLength - 1;
                    }
                }
                else if (selected > currentListLength - 1)
                {
                    if (Equals(GameCanvas.panel) && typeShop == 2 && currentTabIndex <= 3 && maxPageShop[currentTabIndex] > 1)
                    {
                        InfoDlg.showWait();
                        if (currPageShop[currentTabIndex] >= maxPageShop[currentTabIndex] - 1)
                        {
                            Service.gI().kigui(4, -1, (sbyte)currentTabIndex, 0, -1);
                        }
                        else
                        {
                            Service.gI().kigui(4, -1, (sbyte)currentTabIndex, currPageShop[currentTabIndex] + 1, -1);
                        }
                        return;
                    }
                    selected = (isTabInven() ? 1 : 0);
                }
                lastSelect[currentTabIndex] = selected;
                cSelected = 0;
                getCurrClanOtion();
            }
        }
        if (isnewInventory && GameCanvas.keyPressed[5] && itemInvenNew != null)
        {
            pointerDownTime = 0;
            waitToPerform = 2;
        }
        int numScrollY = ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1)) ? (yScroll + 22) : yScroll;
        int scrollAreaH = ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1)) ? (hScroll - 22) : hScroll;
        if (flag)
        {
            cmtoY = (isTabInven() ? (selected - 1) : selected) * ITEM_HEIGHT - scrollAreaH / 2;
            if (cmtoY > cmyLim)
            {
                cmtoY = cmyLim;
            }
            if (cmtoY < 0)
            {
                cmtoY = 0;
            }
            cmy = cmtoY;
        }
        if (GameCanvas.isPointerDown)
        {
            justRelease = false;
            if (!pointerIsDowning && GameCanvas.isPointer(xScroll, numScrollY, wScroll, scrollAreaH))
            {
                for (int i = 0; i < pointerDownLastX.Length; i++)
                {
                    pointerDownLastX[0] = GameCanvas.py;
                }
                pointerDownFirstX = GameCanvas.py;
                pointerIsDowning = true;
                isDownWhenRunning = cmRun != 0;
                cmRun = 0;
            }
            else if (pointerIsDowning)
            {
                pointerDownTime++;
                if (pointerDownTime > 5 && pointerDownFirstX == GameCanvas.py && !isDownWhenRunning)
                {
                    pointerDownFirstX = -1000;
                    if (isTabInven())
                    {
                        selected = (GameCanvas.py >= H - 15 - 18) ? (-1) : ((cmtoY + GameCanvas.py - numScrollY) / ITEM_HEIGHT + 1);
                    }
                    else
                    {
                        selected = (cmtoY + GameCanvas.py - numScrollY) / ITEM_HEIGHT;
                    }
                    if (selected >= currentListLength)
                    {
                        selected = -1;
                    }
                    checkOptionSelect();
                }
                else
                {
                    indexMouse = -1;
                }
                int num = GameCanvas.py - pointerDownLastX[0];
                if (num != 0 && selected != -1)
                {
                    selected = -1;
                    cSelected = -1;
                }
                for (int num2 = pointerDownLastX.Length - 1; num2 > 0; num2--)
                {
                    pointerDownLastX[num2] = pointerDownLastX[num2 - 1];
                }
                pointerDownLastX[0] = GameCanvas.py;
                cmtoY -= num;
                if (cmtoY < 0)
                {
                    cmtoY = 0;
                }
                if (cmtoY > cmyLim)
                {
                    cmtoY = cmyLim;
                }
                if (cmy < 0 || cmy > cmyLim)
                {
                    num /= 2;
                }
                cmy -= num;
                if (cmy < -(GameCanvas.h / 3))
                {
                    wantUpdateList = true;
                }
                else
                {
                    wantUpdateList = false;
                }
                if (isnewInventory)
                {
                    int num3 = GameCanvas.px - xScroll;
                    int num4 = GameCanvas.py - yScroll;
                    sellectInventory = num4 / 34 * 5 + num3 / 34;
                }
            }
        }
        if (!GameCanvas.isPointerJustRelease || !pointerIsDowning)
        {
            return;
        }
        justRelease = true;
        int i2 = GameCanvas.py - pointerDownLastX[0];
        GameCanvas.isPointerJustRelease = false;
        if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - pointerDownFirstX) < 20 && !isDownWhenRunning)
        {
            cmRun = 0;
            cmtoY = cmy;
            pointerDownFirstX = -1000;
            if (isTabInven())
            {
                selected = (cmtoY + GameCanvas.py - numScrollY) / ITEM_HEIGHT + 1;
            }
            else
            {
                selected = (cmtoY + GameCanvas.py - numScrollY) / ITEM_HEIGHT;
            }
            if (selected >= currentListLength)
            {
                selected = -1;
            }
            checkOptionSelect();
            pointerDownTime = 0;
            waitToPerform = 10;
            if (isnewInventory)
            {
                waitToPerform = -1;
            }
            SoundMn.gI().panelClick();
        }
        else if (selected != -1 && pointerDownTime > 5)
        {
            pointerDownTime = 0;
            waitToPerform = 1;
        }
        else if (selected == -1 && !isDownWhenRunning)
        {
            if (cmy < 0)
            {
                cmtoY = 0;
            }
            else if (cmy > cmyLim)
            {
                cmtoY = cmyLim;
            }
            else
            {
                int num5 = GameCanvas.py - pointerDownLastX[0] + (pointerDownLastX[0] - pointerDownLastX[1]) + (pointerDownLastX[1] - pointerDownLastX[2]);
                num5 = ((num5 > 10) ? 10 : ((num5 < -10) ? (-10) : 0));
                cmRun = -num5 * 100;
            }
        }
        int num6 = 0;
        if (type == 13 && GameCanvas.py < yScroll + ITEM_HEIGHT)
        {
            selected = 0;
            updateKeyInvenTab();
        }
        pointerIsDowning = false;
        pointerDownTime = 0;
        GameCanvas.isPointerJustRelease = false;
    }

    public string subArray(string[] str)
    {
        return null;
    }

    private void updateKeyInTabBar()
    {
        if ((scroll != null && scroll.pointerIsDowning) || pointerIsDowning)
        {
            return;
        }
        int num = currentTabIndex;
        if (isTabInven() && isnewInventory)
        {
            if (selected == -1)
            {
                if (GameCanvas.keyPressed[6])
                {
                    currentTabIndex++;
                    if (currentTabIndex >= currentTabName.Length)
                    {
                        if (GameCanvas.panel2 != null)
                        {
                            currentTabIndex = currentTabName.Length - 1;
                            GameCanvas.isFocusPanel2 = true;
                        }
                        else
                        {
                            currentTabIndex = 0;
                        }
                    }
                    selected = lastSelect[currentTabIndex];
                    lastTabIndex[type] = currentTabIndex;
                }
                if (GameCanvas.keyPressed[4])
                {
                    currentTabIndex--;
                    if (currentTabIndex < 0)
                    {
                        currentTabIndex = currentTabName.Length - 1;
                    }
                    if (GameCanvas.isFocusPanel2)
                    {
                        GameCanvas.isFocusPanel2 = false;
                    }
                    selected = lastSelect[currentTabIndex];
                    lastTabIndex[type] = currentTabIndex;
                }
            }
            else if (selected > 0)
            {
                if (GameCanvas.keyPressed[8])
                {
                    if (newSelected == 0)
                    {
                        sellectInventory++;
                    }
                    else
                    {
                        sellectInventory += 5;
                    }
                }
                else if (GameCanvas.keyPressed[2])
                {
                    if (newSelected == 0)
                    {
                        sellectInventory--;
                    }
                    else
                    {
                        sellectInventory -= 5;
                    }
                }
                else if (GameCanvas.keyPressed[4])
                {
                    if (newSelected == 0)
                    {
                        sellectInventory -= 5;
                    }
                    else
                    {
                        sellectInventory--;
                    }
                }
                else if (GameCanvas.keyPressed[6])
                {
                    if (newSelected == 0)
                    {
                        sellectInventory += 5;
                    }
                    else
                    {
                        sellectInventory++;
                    }
                }
            }
            if (sellectInventory < 0)
            {
            }
            if (sellectInventory == nTableItem)
            {
                sellectInventory = 0;
            }
        }
        else if (!IsTabOption())
        {
            if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
            {
                if (isTabInven())
                {
                    if (selected >= 0)
                    {
                        updateKeyInvenTab();
                    }
                    else
                    {
                        currentTabIndex++;
                        if (currentTabIndex >= currentTabName.Length)
                        {
                            if (GameCanvas.panel2 != null)
                            {
                                currentTabIndex = currentTabName.Length - 1;
                                GameCanvas.isFocusPanel2 = true;
                            }
                            else
                            {
                                currentTabIndex = 0;
                            }
                        }
                        selected = lastSelect[currentTabIndex];
                        lastTabIndex[type] = currentTabIndex;
                    }
                }
                else
                {
                    currentTabIndex++;
                    if (currentTabIndex >= currentTabName.Length)
                    {
                        if (GameCanvas.panel2 != null)
                        {
                            currentTabIndex = currentTabName.Length - 1;
                            GameCanvas.isFocusPanel2 = true;
                        }
                        else
                        {
                            currentTabIndex = 0;
                        }
                    }
                    selected = lastSelect[currentTabIndex];
                    lastTabIndex[type] = currentTabIndex;
                }
            }
            if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
            {
                currentTabIndex--;
                if (currentTabIndex < 0)
                {
                    currentTabIndex = currentTabName.Length - 1;
                }
                if (GameCanvas.isFocusPanel2)
                {
                    GameCanvas.isFocusPanel2 = false;
                }
                selected = lastSelect[currentTabIndex];
                lastTabIndex[type] = currentTabIndex;
            }
        }
        keyTouchTab = -1;
        for (int i = 0; i < currentTabName.Length; i++)
        {
            if (!GameCanvas.isPointer(startTabPos + i * TAB_W, 52, TAB_W - 1, 25))
            {
                continue;
            }
            keyTouchTab = i;
            if (GameCanvas.isPointerJustRelease)
            {
                if (type == 8)
                {
                    ModFunc.DoChatGlobal();
                    break;
                }
                currentTabIndex = i;
                lastTabIndex[type] = i;
                GameCanvas.isPointerJustRelease = false;
                selected = lastSelect[currentTabIndex];
                if (num == currentTabIndex && cmRun == 0)
                {
                    cmtoY = 0;
                    selected = GameCanvas.isTouch ? (-1) : 0;
                }
                break;
            }
        }
        if (num == currentTabIndex)
        {
            return;
        }
        size_tab = 0;
        SoundMn.gI().panelClick();
        switch (type)
        {
            case 21:
            case 28:
                if (currentTabIndex == 0)
                {
                    setTabPetInventory(type == 28);
                }
                else if (currentTabIndex == 1)
                {
                    setTabPetSkill(type == 28);
                }
                else if (currentTabIndex == 2)
                {
                    setTabPetStatus();
                }
                else if (currentTabIndex == 3)
                {
                    setTabInventory(resetSelect: true);
                }
                break;
            case 0:
                if (currentTabIndex == 0)
                {
                    setTabTask();
                }
                if (currentTabIndex == 1)
                {
                    setTabInventory(resetSelect: true);
                }
                if (currentTabIndex == 2)
                {
                    setTabSkill();
                }
                if (currentTabIndex == 3)
                {
                    if (mainTabName.Length == 4)
                    {
                        setTabTool();
                    }
                    else
                    {
                        setTabClans();
                    }
                }
                if (currentTabIndex == 4)
                {
                    setTabTool();
                }
                break;
            case 2:
                if (currentTabIndex == 0)
                {
                    setTabBox();
                }
                if (currentTabIndex == 1)
                {
                    setTabInventory(resetSelect: true);
                }
                break;
            case 3:
                setTabZone();
                break;
            case 1:
                setTabShop();
                break;
            case 25:
                setTabSpeacialSkill();
                break;
            case 12:
                if (currentTabIndex == 0)
                {
                    setTabCombine();
                }
                if (currentTabIndex == 1)
                {
                    setTabInventory(resetSelect: true);
                }
                break;
            case 13:
                if (currentTabIndex == 0)
                {
                    if (Equals(GameCanvas.panel))
                    {
                        setTabInventory(resetSelect: true);
                    }
                    else if (Equals(GameCanvas.panel2))
                    {
                        setTabGiaoDich(isMe: false);
                    }
                }
                if (currentTabIndex == 1)
                {
                    setTabGiaoDich(isMe: true);
                }
                if (currentTabIndex == 2)
                {
                    setTabGiaoDich(isMe: false);
                }
                break;
        }
        selected = lastSelect[currentTabIndex];
    }

    // setTabPetStatus() extracted to Panel.Pet.cs

    // setTabPetSkill() extracted to Panel.Pet.cs

    private void setTabTool()
    {
        SoundMn.gI().getSoundOption();
        currentListLength = strTool.Length;
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        cmy = (cmtoY = cmyLast[currentTabIndex]);
        if (cmy < 0)
        {
            cmy = (cmtoY = 0);
        }
        if (cmy > cmyLim)
        {
            cmy = (cmtoY = cmyLim);
        }
    }

    // initTabClans() extracted to Panel.Clan.cs

    // setTabClans() extracted to Panel.Clan.cs

    // initLogMessage() extracted to Panel.Social.cs

    // setTabMessage() extracted to Panel.Social.cs

    // setTabShop() extracted to Panel.Shop.cs

    // setTabSkill() extracted to Panel.Skill.cs

    // setTabMapTrans() extracted to Panel.ZoneMap.cs

    // setTabZone() extracted to Panel.ZoneMap.cs

    // setTabBox() extracted to Panel.Box.cs

    // setTabPetInventory() extracted to Panel.Pet.cs

    private void setTabBody(bool resetSelect)
    {
        currentListLength = checkCurrentListLength(Char.myCharz().arrItemBody.Length);
        ITEM_HEIGHT = 29;
        cmyLim = (currentListLength - 1) * ITEM_HEIGHT - hScroll;
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
        if (resetSelect)
        {
            selected = (GameCanvas.isTouch ? (-1) : 1);
        }
    }

    private void setTabInventory(bool resetSelect)
    {
        if (isnewInventory)
        {
            int num = Char.myCharz().arrItemBag.Length;
            currentListLength = checkCurrentListLength(num);
            currentListLength = 3;
            newSelected = 0;
            size_tab = (sbyte)(num / 20 + ((num % 20 > 0) ? 1 : 0));
            Res.outz("sizeTab = " + size_tab);
            return;
        }
        int len = isCurrentTabBody() ? Char.myCharz().arrItemBody.Length : Char.myCharz().arrItemBag.Length;
        currentListLength = checkCurrentListLength(len);
        ITEM_HEIGHT = 29;
        int numScrollH = ((type == 0 || type == 2) ? (hScroll - 22) : hScroll);
        cmyLim = (currentListLength - 1) * ITEM_HEIGHT - numScrollH;
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
        if (resetSelect)
        {
            selected = (GameCanvas.isTouch ? (-1) : 1);
        }
    }

    // setTabMap() extracted to Panel.ZoneMap.cs

    private void setTabTask()
    {
        cmyQuest = 0;
    }

    public void moveCamera()
    {
        if (timeShow > 0)
        {
            timeShow--;
        }
        if (justRelease && Equals(GameCanvas.panel) && typeShop == 2 && maxPageShop[currentTabIndex] > 1)
        {
            if (cmy < -50)
            {
                InfoDlg.showWait();
                justRelease = false;
                if (currPageShop[currentTabIndex] <= 0)
                {
                    Service.gI().kigui(4, -1, (sbyte)currentTabIndex, maxPageShop[currentTabIndex] - 1, -1);
                }
                else
                {
                    Service.gI().kigui(4, -1, (sbyte)currentTabIndex, currPageShop[currentTabIndex] - 1, -1);
                }
            }
            else if (cmy > cmyLim + 50)
            {
                justRelease = false;
                InfoDlg.showWait();
                if (currPageShop[currentTabIndex] >= maxPageShop[currentTabIndex] - 1)
                {
                    Service.gI().kigui(4, -1, (sbyte)currentTabIndex, 0, -1);
                }
                else
                {
                    Service.gI().kigui(4, -1, (sbyte)currentTabIndex, currPageShop[currentTabIndex] + 1, -1);
                }
            }
        }
        if (cmx != cmtoX && !pointerIsDowning)
        {
            cmvx = cmtoX - cmx << 2;
            cmdx += cmvx;
            cmx += cmdx >> 3;
            cmdx &= 15;
        }
        if (Math.abs(cmtoX - cmx) < 10)
        {
            cmx = cmtoX;
        }
        if (isClose)
        {
            isClose = false;
            cmtoX = wScroll;
        }
        if (cmtoX >= wScroll - 10 && cmx >= wScroll - 10 && position == 0)
        {
            isShow = false;
            cleanCombine();
            if (isChangeZone)
            {
                isChangeZone = false;
                if (Char.myCharz().cHP > 0 && Char.myCharz().statusMe != 14)
                {
                    InfoDlg.showWait();
                    if (type == 3)
                    {
                        Service.gI().requestChangeZone(selected, -1);
                    }
                    else if (type == 14)
                    {
                        AutoXmap.SelectMapTrans(selected);
                    }
                }
            }
            if (isSelectPlayerMenu)
            {
                isSelectPlayerMenu = false;
                int num = vPlayerMenu.size() - vPlayerMenu_id.size();
                if (Char.myCharz().charFocus != null)
                {
                    if (selected - num < 0)
                    {
                        Char.myCharz().charFocus.menuSelect = selected;
                    }
                    else
                    {
                        Char.myCharz().charFocus.menuSelect = short.Parse((string)vPlayerMenu_id.elementAt(selected - num));
                    }
                }
                Command command = (Command)vPlayerMenu.elementAt(selected);
                command.performAction();
            }
            vPlayerMenu.removeAllElements();
            charMenu = null;
        }
        if (cmRun != 0 && !pointerIsDowning)
        {
            cmtoY += cmRun / 100;
            if (cmtoY < 0)
            {
                cmtoY = 0;
            }
            else if (cmtoY > cmyLim)
            {
                cmtoY = cmyLim;
            }
            else
            {
                cmy = cmtoY;
            }
            cmRun = cmRun * 9 / 10;
            if (cmRun < 100 && cmRun > -100)
            {
                cmRun = 0;
            }
        }
        if (cmy != cmtoY && !pointerIsDowning)
        {
            cmvy = cmtoY - cmy << 2;
            cmdy += cmvy;
            cmy += cmdy >> 4;
            cmdy &= 15;
        }
        cmyLast[currentTabIndex] = cmy;
    }

    // paintDetail(mGraphics g) được tách và quản lý trong Panel.Detail.cs


    public void paintTop(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        if (currentListLength == 0)
        {
            return;
        }
        int num = (cmy + hScroll) / 24 + 1;
        if (num < hScroll / 24 + 1)
        {
            num = hScroll / 24 + 1;
        }
        if (num > currentListLength)
        {
            num = currentListLength;
        }
        int num2 = cmy / 24;
        if (num2 >= num)
        {
            num2 = num - 1;
        }
        if (num2 < 0)
        {
            num2 = 0;
        }
        for (int i = num2; i < num; i++)
        {
            int num3 = xScroll;
            int num4 = yScroll + i * ITEM_HEIGHT;
            int num5 = 29;
            int h = ITEM_HEIGHT - 1;
            int num6 = xScroll + num5;
            int num7 = yScroll + i * ITEM_HEIGHT;
            int num8 = wScroll - num5;
            int num9 = ITEM_HEIGHT - 1;
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num6, num7, num8, num9);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num3, num4, num5, h);
            TopInfo topInfo = (TopInfo)vTop.elementAt(i);
            if (topInfo.headICON != -1)
            {
                SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
            }
            else
            {
                Part part = GameScr.parts[topInfo.headID];
                SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, num3 + part.pi[Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
            }
            g.setClip(xScroll, yScroll + cmy, wScroll, hScroll);
            if (topInfo.pId != Char.myCharz().charID)
            {
                mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
            }
            else
            {
                mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
            }
            mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
            mFont.tahoma_7_green2.drawString(g, mResources.rank + ": " + topInfo.rank + string.Empty, num6 + 5, num7 + 11, 0);
        }
        paintScrollArrow(g);
    }

    public void paint(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
        g.translate(-cmx, 0);
        g.translate(X, Y);
        if (GameCanvas.panel.combineSuccess != -1)
        {
            if (Equals(GameCanvas.panel))
            {
                paintCombineEff(g);
            }
            return;
        }
        GameCanvas.paintz.paintFrameSimple(X, Y, W, H, g);
        try
        {
            paintTopInfo(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTopInfo: " + ex.ToString());
        }
        try
        {
            paintBottomMoneyInfo(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintBottomMoneyInfo: " + ex.ToString());
        }
        try
        {
            paintTab(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTab: " + ex.ToString());
        }
        try
        {
            switch (type)
            {
            case 9:
                paintArchivement(g);
                break;
            case 21:
            case 28:
                if (currentTabIndex == 0)
                {
                    paintPetInventory(g, type == 28);
                }
                else if (currentTabIndex == 1)
                {
                    paintPetSkill(g, type == 28);
                }
                else if (currentTabIndex == 2)
                {
                    paintPetStatus(g);
                }
                else if (currentTabIndex == 3)
                {
                    paintInventory(g);
                }
                break;
            case 24:
                paintGameSubInfo(g);
                break;
            case 23:
                paintGameInfo(g);
                break;
            case 0:
                if (currentTabIndex == 0)
                {
                    paintTask(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                if (currentTabIndex == 2)
                {
                    paintSkill(g);
                }
                if (currentTabIndex == 3)
                {
                    if (mainTabName.Length == 4)
                    {
                        paintTools(g);
                    }
                    else
                    {
                        paintClans(g);
                    }
                }
                if (currentTabIndex == 4)
                {
                    paintTools(g);
                }
                break;
            case 2:
                if (currentTabIndex == 0)
                {
                    paintBox(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                break;
            case 3:
                paintZone(g);
                break;
            case 1:
                paintShop(g);
                break;
            case 25:
                paintSpeacialSkill(g);
                break;
            case 4:
                paintMap(g);
                break;
            case 7:
                paintInventory(g);
                break;
            case 17:
                paintShop(g);
                break;
            case 8:
                paintLogChat(g);
                break;
            case 10:
                paintPlayerMenu(g);
                break;
            case 11:
                paintFriend(g);
                break;
            case 16:
                paintEnemy(g);
                break;
            case 15:
                paintTop(g);
                break;
            case 12:
                if (currentTabIndex == 0)
                {
                    paintCombine(g);
                }
                if (currentTabIndex == 1)
                {
                    paintInventory(g);
                }
                break;
            case 13:
                if (currentTabIndex == 0)
                {
                    if (Equals(GameCanvas.panel))
                    {
                        paintInventory(g);
                    }
                    else
                    {
                        paintGiaoDich(g, isMe: false);
                    }
                }
                if (currentTabIndex == 1)
                {
                    paintGiaoDich(g, isMe: true);
                }
                if (currentTabIndex == 2)
                {
                    paintGiaoDich(g, isMe: false);
                }
                break;
            case 14:
                paintMapTrans(g);
                break;
            case 18:
                paintFlagChange(g);
                break;
            case 19:
                paintOption(g);
                break;
            case 20:
                paintAccount(g);
                break;
            case 22:
                paintAuto(g);
                break;
            case 26:
                PaintModFunc(g);
                break;
            case 27:
                paintPlayerInfo(g);
                break;
        }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paint type " + type + ": " + ex.ToString());
        }
        GameScr.resetTranslate(g);
        try
        {
            paintDetail(g);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintDetail: " + ex.ToString());
        }
        if (cmx == cmtoX)
        {
            cmdClose.paint(g);
        }
        if (tabIcon != null && tabIcon.isShow)
        {
            tabIcon.paint(g);
        }
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.translate(X, Y);
        g.translate(-cmx, 0);
    }

    // paintShop() extracted to Panel.Shop.cs

    private void paintAuto(mGraphics g)
    {
    }

    // paintPetStatus() extracted to Panel.Pet.cs

    // paintPetSkill() extracted to Panel.Pet.cs

    // paintPetInventory() extracted to Panel.Pet.cs

    private void paintScrollArrow(mGraphics g)
    {
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        if ((cmy > 24 && currentListLength > 0) || (Equals(GameCanvas.panel) && typeShop == 2 && maxPageShop[currentTabIndex] > 1))
        {
            g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, xScroll + wScroll - 12, yScroll + 3, 0);
        }
        if ((cmy < cmyLim && currentListLength > 0) || (Equals(GameCanvas.panel) && typeShop == 2 && maxPageShop[currentTabIndex] > 1))
        {
            int arrowY = yScroll + hScroll - 8;
            g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, xScroll + wScroll - 12, arrowY, 0);
        }
    }

    private void paintTools(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < strTool.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, h);
            mFont.tahoma_7b_dark.drawString(g, strTool[i], xScroll + wScroll / 2, num2 + 6, mFont.CENTER);
            if (!strTool[i].Equals(mResources.gameInfo))
            {
                continue;
            }
            for (int j = 0; j < vGameInfo.size(); j++)
            {
                GameInfo gameInfo = (GameInfo)vGameInfo.elementAt(j);
                if (!gameInfo.hasRead)
                {
                    if (GameCanvas.gameTick % 20 > 10)
                    {
                        g.drawImage(imgNew, num + 10, num2 + 10, 3);
                    }
                    break;
                }
            }
        }
        paintScrollArrow(g);
    }

    private void paintGameSubInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < contenInfo.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * 15;
            int num3 = wScroll - 1;
            int num4 = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                mFont.tahoma_7b_dark.drawString(g, contenInfo[i], xScroll + 5, num2 + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }

    private void paintPlayerInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < contenInfo.Length; i++)
        {
            int num = xScroll;
            int num2 = yScroll + i * 15;
            int num3 = wScroll - 1;
            int num4 = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                mFont.tahoma_7b_dark.drawString(g, contenInfo[i], xScroll + 5, num2 + 6, mFont.LEFT);
            }
        }
        paintScrollArrow(g);
    }

    private void paintGameInfo(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        for (int i = 0; i < vGameInfo.size(); i++)
        {
            GameInfo gameInfo = (GameInfo)vGameInfo.elementAt(i);
            int num = xScroll;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 1;
            int h = ITEM_HEIGHT - 1;
            if (num2 - cmy <= yScroll + hScroll && num2 - cmy >= yScroll - ITEM_HEIGHT)
            {
                g.setColor((i != selected) ? 15196114 : 16383818);
                g.fillRect(num, num2, num3, h);
                mFont.tahoma_7b_dark.drawString(g, gameInfo.main, xScroll + wScroll / 2, num2 + 6, mFont.CENTER);
                if (!gameInfo.hasRead && GameCanvas.gameTick % 20 > 10)
                {
                    g.drawImage(imgNew, num + 10, num2 + 10, 3);
                }
            }
        }
        paintScrollArrow(g);
    }

    // paintSkill() extracted to Panel.Skill.cs

    // paintMapTrans() extracted to Panel.ZoneMap.cs

    // paintZone() extracted to Panel.ZoneMap.cs

    // paintSpeacialSkill() extracted to Panel.Skill.cs

    private void paintPageBar(mGraphics g, int pageBarY, int pageBarH, int totalPages)
    {
    }

    // paintBox() extracted to Panel.Box.cs

    public Member getCurrMember()
    {
        if (selected < 2)
        {
            return null;
        }
        if (selected > ((member == null) ? myMember.size() : member.size()) + 1)
        {
            return null;
        }
        return (member == null) ? ((Member)myMember.elementAt(selected - 2)) : ((Member)member.elementAt(selected - 2));
    }

    public ClanMessage getCurrMessage()
    {
        if (selected < 2)
        {
            return null;
        }
        if (selected > ClanMessage.vMessage.size() + 1)
        {
            return null;
        }
        return (ClanMessage)ClanMessage.vMessage.elementAt(selected - 2);
    }

    public Clan getCurrClan()
    {
        if (selected < 2)
        {
            return null;
        }
        if (selected > clans.Length + 1)
        {
            return null;
        }
        return clans[selected - 2];
    }

    // paintLogChat() extracted to Panel.Social.cs

    private void paintFlagChange(mGraphics g)
    {
        g.setClip(xScroll, yScroll, wScroll, hScroll);
        g.translate(0, -cmy);
        g.setColor(0);
        for (int i = 0; i < currentListLength; i++)
        {
            int num = xScroll + 26;
            int num2 = yScroll + i * ITEM_HEIGHT;
            int num3 = wScroll - 26;
            int h = ITEM_HEIGHT - 1;
            int num4 = xScroll;
            int num5 = yScroll + i * ITEM_HEIGHT;
            int num6 = 24;
            int num7 = ITEM_HEIGHT - 1;
            if (num2 - cmy > yScroll + hScroll || num2 - cmy < yScroll - ITEM_HEIGHT)
            {
                continue;
            }
            g.setColor((i != selected) ? 15196114 : 16383818);
            g.fillRect(num, num2, num3, h);
            g.setColor((i != selected) ? 9993045 : 9541120);
            g.fillRect(num4, num5, num6, num7);
            Item item = (Item)vFlag.elementAt(i);
            if (item == null)
            {
                continue;
            }
            mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
            string text = string.Empty;
            if (item.itemOption != null && item.itemOption.Length >= 1)
            {
                if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
                {
                    text += item.itemOption[0].getOptionString();
                }
                mFont tahoma_7_blue = mFont.tahoma_7_blue;
                tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
                SmallImage.drawSmallImage(g, item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
            }
        }
        paintScrollArrow(g);
    }

    // paintEnemy() extracted to Panel.Social.cs

    // paintFriend() extracted to Panel.Social.cs

    // paintPlayerMenu() extracted to Panel.Social.cs

    // paintClans() extracted to Panel.Clan.cs

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

    // paintCombine(mGraphics g) được tách sang Panel.Upgrade.cs


    private void paintInventory(mGraphics g)
    {
        bool flag = true;
        if (flag && isnewInventory)
        {
            Item[] arrItemBody = Char.myCharz().arrItemBody;
            Item[] arrItemBag = Char.myCharz().arrItemBag;
            g.setColor(16711680);
            int num = arrItemBody.Length + arrItemBag.Length;
            int num2 = num / 20 + ((num % 20 > 0) ? 1 : 0) + 1;
            int num3 = 0;
            num3 = 0;
            int num6 = xScroll;
            int num7 = yScroll + num3 * ITEM_HEIGHT;
            int num8 = 34;
            int num9 = ITEM_HEIGHT - 1;
            for (int j = 0; j < 4; j++)
            {
                num6 = xScroll;
                num7 = yScroll + (j + num3) * ITEM_HEIGHT;
                bool flag2 = true;
                for (int k = 0; k < 5; k++)
                {
                    Item item = null;
                    int num10 = 0;
                    if (newSelected > 0)
                    {
                        num10 = (newSelected - 1) * 20;
                        if (j * 5 + k + num10 < arrItemBag.Length)
                        {
                            item = arrItemBag[j * 5 + k + num10];
                            num6 = xScroll + num8 * k;
                            int num11 = sellectInventory % 5;
                            int num12 = sellectInventory / 5;
                            if (newSelected > 0)
                            {
                                g.setColor(15196114);
                            }
                            else
                            {
                                g.setColor(9993045);
                            }
                            g.drawRect(num6, num7, num8, num9);
                            if (j == num12 && k == num11 && selected > 0)
                            {
                                g.setColor(16383818);
                                itemInvenNew = item;
                            }
                            g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
                            if (item != null)
                            {
                                int x2 = num6 + imgNew.getWidth() / 2;
                                int y = num7;
                                int num13 = 34;
                                int h = ITEM_HEIGHT - 1;
                                SmallImage.drawSmallImage(g, item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
                                if (item.quantity > 1)
                                {
                                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num6, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
                                }
                                if (item.newItem && GameCanvas.gameTick % 10 > 5)
                                {
                                    g.drawImage(imgNew, x2, y, 3);
                                }
                                for (int l = 0; l < item.itemOption.Length; l++)
                                {
                                    paintOptSlotItem(g, item.itemOption[l].optionTemplate.id, item.itemOption[l].param, x2, y, num13, h);
                                }
                            }
                            if (!flag2)
                            {
                                break;
                            }
                            continue;
                        }
                        flag2 = false;
                        break;
                    }
                    if (j * 5 + k < arrItemBody.Length)
                    {
                        item = arrItemBody[j * 5 + k];
                        flag2 = false;
                    }
                    else
                    {
                        flag2 = false;
                    }
                    break;
                }
            }
            num3 = ((newSelected != 0) ? 5 : 3);
            int num14 = yScroll + num3 * ITEM_HEIGHT + 5;
            int num15 = 2;
            if (newSelected == 0)
            {
                num15 = 4;
            }
            num6 = xScroll;
            num7 = yScroll + num3 * ITEM_HEIGHT;
            num8 = 34;
            num9 = ITEM_HEIGHT - 1;
            if (newSelected == 0)
            {
                g.setColor(15196114);
                num3 = 1;
                nTableItem = 10;
                int num16 = 5;
                if (eBanner != null)
                {
                    eBanner.paint(g);
                    eBanner.x = num6 + 34 + 34;
                    eBanner.y = num7 + num9 - 25;
                }
                for (int m = 0; m < 10; m++)
                {
                    Item item2 = null;
                    item2 = arrItemBody[m];
                    if (m < 5)
                    {
                        num16 = 0;
                        num6 = xScroll;
                        num7 = yScroll + (m + num3) * ITEM_HEIGHT;
                    }
                    else
                    {
                        num16 = 5;
                        num6 = xScroll + 4 * num8;
                        num7 = yScroll + (m - num16 + num3) * ITEM_HEIGHT;
                    }
                    g.setColor(15196114);
                    g.drawRect(num6, num7, num8, num9);
                    if (sellectInventory == m)
                    {
                        itemInvenNew = item2;
                        g.setColor(16383818);
                    }
                    else
                    {
                        g.setColor(9993045);
                    }
                    g.fillRect(num6 + 2, num7 + 2, num8 - 3, num9 - 3);
                    if (item2 == null)
                    {
                        screenTab6.drawFrame(m, num6 + num8 / 2 - 8, num7 + num9 / 2 - 8, 0, mGraphics.TOP | mGraphics.LEFT, g);
                    }
                    if (item2 != null)
                    {
                        SmallImage.drawSmallImage(g, item2.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
                        if (item2.quantity > 1)
                        {
                            mFont.tahoma_7_yellow.drawString(g, string.Empty + item2.quantity, num6 + 4 * num8, num7 - mFont.tahoma_7_yellow.getHeight(), 1);
                        }
                    }
                }
                num3 = 1;
                num6 = xScroll + 34;
                num7 = yScroll + num3 * ITEM_HEIGHT;
                num8 = 102;
                num9 = 4 * (ITEM_HEIGHT - 1);
                Char.myCharz().paintCharBody(g, num6 + 34 + 17, num7 + num9 - 25, 1, 0, isPaintBag: true);
                num3 = 3;
                num15 = 2;
                num6 = xScroll + 34;
                num7 = yScroll + (1 + num3) * ITEM_HEIGHT - 1;
                num8 = 102;
                num9 = ITEM_HEIGHT * num15;
                g.setColor(15196114);
                g.drawRect(num6, num7, num8, num9);
                g.setColor(9993045);
                g.fillRect(num6 + 1, num7 + 1, num8 - 2, num9 - 2);
                paintItemBodyBagInfo(g, num6 + 3, num7 - 2);
                num3 = ((newSelected != 0) ? 5 : 6);
                num14 = yScroll + num3 * ITEM_HEIGHT;
                g.setColor(15196114);
                if (newSelected == 0)
                {
                    num15 = 1;
                }
                g.drawRect(xScroll, num14, wScroll, ITEM_HEIGHT * num15);
                g.setColor(16777215);
                g.fillRect(xScroll + 1, num14 + 1, wScroll - 2, ITEM_HEIGHT * num15 - 2);
            }
            if (itemInvenNew != null && itemInvenNew.itemOption != null)
            {
                string text = string.Empty;
                mFont mFont2 = mFont.tahoma_7_green2;
                if (itemInvenNew.itemOption != null)
                {
                    for (int n = 0; n < itemInvenNew.itemOption.Length; n++)
                    {
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 72)
                        {
                            text = " [+" + itemInvenNew.itemOption[n].param + "]";
                        }
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 225)
                        {
                            text = " [+" + itemInvenNew.itemOption[n].param + "]";
                        }
                     
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 225)
                        {
                            if (itemInvenNew.itemOption[n].param >= 1 && itemInvenNew.itemOption[n].param <= 5)
                            {
                                mFont2 = GetFont(2);
                            }                          
                            else if (itemInvenNew.itemOption[n].param >= 6 && itemInvenNew.itemOption[n].param <= 7)
                            {
                                mFont2 = GetFont(8);
                            }
                            else if (itemInvenNew.itemOption[n].param >= 8 && itemInvenNew.itemOption[n].param <= 10)
                            {
                                mFont2 = GetFont(7);
                            }
                        }
                        if (itemInvenNew.itemOption[n].optionTemplate.id == 72)
                        {
                            if (itemInvenNew.itemOption[n].param >= 1 && itemInvenNew.itemOption[n].param <= 5)
                            {
                                mFont2 = GetFont(2);
                            }

                            else if (itemInvenNew.itemOption[n].param >= 6 && itemInvenNew.itemOption[n].param <= 7)
                            {
                                mFont2 = GetFont(8);
                            }
                            else if (itemInvenNew.itemOption[n].param >= 8 && itemInvenNew.itemOption[n].param <= 10)
                            {
                                mFont2 = GetFont(7);
                            }
                        }
                    }
                }
                mFont2.drawString(g, "[" + itemInvenNew.template.id + "] " + itemInvenNew.template.name + text, xScroll + 5, num14 + 1, 0);
                string text2 = string.Empty;
                if (itemInvenNew.itemOption != null)
                {
                    if (itemInvenNew.itemOption.Length > 0 && itemInvenNew.itemOption[0] != null
                          && itemInvenNew.itemOption[0].optionTemplate.id != 72
                          && itemInvenNew.itemOption[0].optionTemplate.id != 206
                            && itemInvenNew.itemOption[0].optionTemplate.id != 225
                              && itemInvenNew.itemOption[0].optionTemplate.id != 34
                                && itemInvenNew.itemOption[0].optionTemplate.id != 35
                                && itemInvenNew.itemOption[0].optionTemplate.id != 36
                        && itemInvenNew.itemOption[0].optionTemplate.id != 102 
                        && itemInvenNew.itemOption[0].optionTemplate.id != 107)
                    {
                        text2 += itemInvenNew.itemOption[0].getOptionString();
                    }
                    mFont mFont3 = mFont.tahoma_7_blue;
                    if (itemInvenNew.compare < 0 && itemInvenNew.template.type != 5)
                    {
                        mFont3 = mFont.tahoma_7_red;
                    }
                    if (itemInvenNew.itemOption.Length > 1)
                    {
                        for (int num17 = 1; num17 < Math.min(itemInvenNew.itemOption.Length,3); num17++)
                        {
                            if (itemInvenNew.itemOption[num17] != null
                                  && itemInvenNew.itemOption[num17].optionTemplate.id != 72
                          && itemInvenNew.itemOption[num17].optionTemplate.id != 206
                            && itemInvenNew.itemOption[num17].optionTemplate.id != 225
                              && itemInvenNew.itemOption[num17].optionTemplate.id != 34
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 35
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 36
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 102
                                && itemInvenNew.itemOption[num17].optionTemplate.id != 107)
                            {
                                text2 = text2 + "| " + itemInvenNew.itemOption[num17].getOptionString();
                            }
                        }
                    }
                    try
                    {
                        if (mFont3.getWidth(text2) > wScroll)
                        {
                            text2 = mFont3.splitFontArray(text2, wScroll)[0];
                        }
                    }
                    catch (Exception)
                    {
                    }
                    mFont3.drawString(g, text2, xScroll + 5, num14 + 10, mFont.LEFT);
                }
            }
        }
        if (flag && isnewInventory)
        {
            return;
        }
        g.setColor(16711680);
        Item[] arrItemBody2 = Char.myCharz().arrItemBody;
        Item[] arrItemBag2 = Char.myCharz().arrItemBag;
        bool isBody = isCurrentTabBody();
        Item[] currentItems = isBody ? arrItemBody2 : arrItemBag2;
        currentListLength = checkCurrentListLength(currentItems.Length);
        bool hasSubTabs = (type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1);
        int startY = hasSubTabs ? (yScroll + 22) : yScroll;
        int scrollH = hasSubTabs ? (hScroll - 22) : hScroll;
        if (hasSubTabs)
        {
            int subTabY = yScroll;
            int subTabH = UITheme.SUB_TAB_HEIGHT;
            int subTabW = (wScroll - 2) / 2;
            int subTab0_x = xScroll + 1;
            int subTab1_x = xScroll + 1 + subTabW;
            // Tab 0: Túi đồ
            g.setColor((subTabInventory == 0) ? UITheme.COLOR_TAB_INACTIVE : UITheme.COLOR_SUBTAB_INACTIVE);
            g.fillRect(subTab0_x, subTabY, subTabW, subTabH);
            if (subTabInventory == 0)
            {
                g.setColor(UITheme.COLOR_TAB_ACTIVE_GREEN);
                g.fillRect(subTab0_x, subTabY + subTabH - 2, subTabW, 2);
            }
            mFont font0 = (subTabInventory == 0) ? mFont.tahoma_7_green2 : mFont.tahoma_7_grey;
            font0.drawString(g, "Túi đồ", subTab0_x + subTabW / 2, subTabY + 4, mFont.CENTER);
            // Tab 1: Trang bị
            g.setColor((subTabInventory == 1) ? UITheme.COLOR_TAB_INACTIVE : UITheme.COLOR_SUBTAB_INACTIVE);
            g.fillRect(subTab1_x, subTabY, subTabW, subTabH);
            if (subTabInventory == 1)
            {
                g.setColor(UITheme.COLOR_TAB_ACTIVE_GREEN);
                g.fillRect(subTab1_x, subTabY + subTabH - 2, subTabW, 2);
            }
            mFont font1 = (subTabInventory == 1) ? mFont.tahoma_7_green2 : mFont.tahoma_7_grey;
            font1.drawString(g, "Trang bị", subTab1_x + subTabW / 2, subTabY + 4, mFont.CENTER);
        }
        g.setClip(xScroll, startY, wScroll, scrollH);
        g.translate(0, -cmy);
        try
        {
            for (int num22 = 1; num22 < currentListLength; num22++)
            {
                int num23 = xScroll + 29;
                int num24 = startY + (num22 - 1) * ITEM_HEIGHT;
                int num25 = wScroll - 29;
                int h2 = ITEM_HEIGHT - 1;
                int num26 = xScroll;
                int num27 = startY + (num22 - 1) * ITEM_HEIGHT;
                int num28 = ITEM_HEIGHT - 1;
                int num29 = ITEM_HEIGHT - 1;
                if (num24 - cmy > startY + scrollH || num24 - cmy < startY - ITEM_HEIGHT)
                {
                    continue;
                }
                int itemIndex = num22 - 1;
                Item item3 = (itemIndex >= 0 && itemIndex < currentItems.Length) ? currentItems[itemIndex] : null;
                g.setColor((num22 == selected) ? 16383818 : ((!isBody) ? 15723751 : 15196114));
                g.fillRect(num23, num24, num25, h2);
                g.setColor((num22 == selected) ? 9541120 : ((!isBody) ? 11837316 : 9993045));
                if (item3 != null)
                {
                    for (int num30 = 0; num30 < item3.itemOption.Length; num30++)
                    {
                        if (item3.itemOption[num30].optionTemplate.id == 72 && item3.itemOption[num30].param > 0)
                        {
                            byte id = (byte)GetColor_Item_Upgrade(item3.itemOption[num30].param);
                            int color_ItemBg = GetColor_ItemBg(id);
                            if (color_ItemBg != -1)
                            {
                                g.setColor((num22 != selected) ? GetColor_ItemBg(id) : GetColor_ItemBg(id));
                            }
                        }
                    }
                    foreach (ItemAuto itemAuto in ModFunc.GI().listItemAuto)
                    {
                        if (item3.template.id == itemAuto.id && item3.template.iconID == itemAuto.iconID)
                        {
                            g.setColor((num22 != selected) ? color1[1] : color2[1]);
                        }
                    }
                }
                
                g.setColor(6047789,0.5f);
                g.fillRect(num26, num27, num28, num29);paintEffectItem(g, item3, num26, num27);
                if (item3 != null && item3.isSelect && GameCanvas.panel.type == 12)
                {
                    g.setColor((num22 != selected) ? 6047789 : 7040779);
                    g.fillRect(num26, num27, num28, num29);
                }
                if (item3 == null)
                {
                    if (isBody)
                    {
                        mFont.tahoma_7_grey.drawString(g, "[" + getBodySlotName(itemIndex) + "]", num23 + 5, num24 + 7, 0);
                    }
                    continue;
                }
                string text3 = string.Empty;
                mFont mFont4 = mFont.tahoma_7_green2;
                if (item3.itemOption != null)
                {
                    for (int num31 = 0; num31 < item3.itemOption.Length; num31++)
                    {
                        if (item3.itemOption[num31].optionTemplate.id == 72)
                        {
                            text3 = " [+" + item3.itemOption[num31].param + "]";
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 225)
                        {
                            text3 = " [+" + item3.itemOption[num31].param + "]";
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 225)
                        {
                            if (item3.itemOption[num31].param >= 1 && item3.itemOption[num31].param <= 2)
                            {
                                mFont4 = GetFont(0);
                            }
                            else if (item3.itemOption[num31].param >= 3 && item3.itemOption[num31].param <= 4)
                            {
                                mFont4 = GetFont(2);
                            }
                            else if (item3.itemOption[num31].param >= 5 && item3.itemOption[num31].param <= 6)
                            {
                                mFont4 = GetFont(8);
                            }
                            else if (item3.itemOption[num31].param >= 7 && item3.itemOption[num31].param <= 10)
                            {
                                mFont4 = GetFont(7);
                            }
                        }
                        if (item3.itemOption[num31].optionTemplate.id == 72)
                        {
                            if (item3.itemOption[num31].param >= 1 && item3.itemOption[num31].param <= 5)
                            {
                                mFont4 = GetFont(2);
                            }
                            
                            else if (item3.itemOption[num31].param >= 6 && item3.itemOption[num31].param <= 7)
                            {
                                mFont4 = GetFont(8);
                            }
                            else if (item3.itemOption[num31].param >= 8 && item3.itemOption[num31].param <= 10)
                            {
                                mFont4 = GetFont(7);
                            }
                        }
                    }
                }
                string itemNamePrefix = isBody ? ("[" + getBodySlotName(itemIndex) + "] ") : ("[" + item3.template.id + "] ");
                mFont4.drawString(g, itemNamePrefix + item3.template.name + text3, num23 + 5, num24 + 1, 0);
                string text4 = string.Empty;
                if (item3.itemOption != null)
                {
                    if (item3.itemOption.Length > 0 && item3.itemOption[0] != null
                        && item3.itemOption[0].optionTemplate.id != 72
                          && item3.itemOption[0].optionTemplate.id != 206
                            && item3.itemOption[0].optionTemplate.id != 225
                              && item3.itemOption[0].optionTemplate.id != 34
                                && item3.itemOption[0].optionTemplate.id != 35 
                                && item3.itemOption[0].optionTemplate.id != 36
                                  && item3.itemOption[0].optionTemplate.id != 102
                        && item3.itemOption[0].optionTemplate.id != 107)
                    {
                        text4 += item3.itemOption[0].getOptionString();
                    }
                    mFont mFont5 = mFont.tahoma_7_blue;
                    if (item3.compare < 0 && item3.template.type != 5)
                    {
                        mFont5 = mFont.tahoma_7_red;
                    }
                    if (item3.itemOption.Length > 1)
                    {
                        for (int num32 = 1; num32 < Math.min(item3.itemOption.Length , 3); num32++)
                        {
                            if (item3.itemOption[num32] != null
                                 && item3.itemOption[num32].optionTemplate.id != 72
                          && item3.itemOption[num32].optionTemplate.id != 206
                            && item3.itemOption[num32].optionTemplate.id != 225
                              && item3.itemOption[num32].optionTemplate.id != 34
                                && item3.itemOption[num32].optionTemplate.id != 35
                                && item3.itemOption[num32].optionTemplate.id != 36
                                && item3.itemOption[num32].optionTemplate.id != 102 
                                && item3.itemOption[num32].optionTemplate.id != 107)
                            {
                                text4 = text4 + "| " + item3.itemOption[num32].getOptionString();
                            }
                        }
                    }
                    mFont5.drawString(g, text4, num23 + 5, num24 + 10, mFont.LEFT);
                }
                SmallImage.drawSmallImage(g, item3.template.iconID, num26 + num28 / 2, num27 + num29 / 2, 0, 3);
                if (item3.itemOption != null)
                {
                    for (int num33 = 0; num33 < item3.itemOption.Length; num33++)
                    {
                        paintOptItemInventory(g, item3.itemOption[num33].optionTemplate.id, item3.itemOption[num33].param, num26, num27, num28, num29, item3);
                    }
                    for (int num34 = 0; num34 < item3.itemOption.Length; num34++)
                    {
                        paintOptSlotItem(g, item3.itemOption[num34].optionTemplate.id, item3.itemOption[num34].param, num26, num27, num28, num29);
                    }
                }
                if (item3.quantity > 1)
                {
                    mFont.tahoma_7_yellow.drawString(g, string.Empty + item3.quantity, num26 + num28, num27 + num29 - mFont.tahoma_7_yellow.getHeight(), 1);
                }
            }
        }
        catch (Exception)
        {
        }
        g.translate(0, cmy);
        paintScrollArrow(g);
    }

    // paintTab(mGraphics g) được tách và quản lý trong Panel.Tabs.cs


    private void paintBottomMoneyInfo(mGraphics g)
    {
        try
        {
            if (type != 13 || (currentTabIndex != 2 && !Equals(GameCanvas.panel2)))
            {
                g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
                g.setColor(11837316);
                g.fillRect(X + 1, H - 15, W - 2, 14);
                g.setColor(13524492);
                g.fillRect(X + 1, H - 15, W - 2, 1);
                if (imgXu != null)
                {
                    g.drawImage(imgXu, X + 11, H - 7, 3);
                }
                if (imgLuong != null)
                {
                    g.drawImage(imgLuong, X + 75, H - 8, 3);
                }
                if (Char.myCharz() != null)
                {
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().xuStr + string.Empty, X + 24, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongStr + string.Empty, X + 85, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                    if (imgLuongKhoa != null)
                    {
                        g.drawImage(imgLuongKhoa, X + 130, H - 8, 3);
                    }
                    mFont.tahoma_7_yellow.drawString(g, Char.myCharz().luongKhoaStr + string.Empty, X + 140, H - 13, mFont.LEFT, mFont.tahoma_7_grey);
                }
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintBottomMoneyInfo: " + ex.ToString());
        }
    }

    // paintClanInfo() extracted to Panel.Clan.cs

    private void paintToolInfo(mGraphics g)
    {
        mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        mFont.tahoma_7_yellow.drawString(g, (Char.myCharz().isTichXanh ? "     " : string.Empty) + Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
        if (Char.myCharz().isTichXanh)
        {
            ModFunc.PaintTicks(g, 58, 17);
        }
        string text = ((!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet);
        mFont.tahoma_7_yellow.drawString(g, mResources.account_server + " " + ServerListScreen.nameServer[ServerListScreen.ipSelect] + ": " + text, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
    }

    // paintGiaoDichInfo() extracted to Panel.Trade.cs

    private void paintMyInfo(mGraphics g)
    {
        paintCharInfo(g, Char.myCharz());
    }

    // paintPetInfo() extracted to Panel.Pet.cs

    // paintPetSkillInfo() extracted to Panel.Pet.cs

    private void paintCharInfo(mGraphics g, Char c)
    {
        if (c == null)
        {
            return;
        }
        try
        {
            mFont.tahoma_7b_white.drawString(g, (c.isTichXanh ? "     " : string.Empty) + c.cName, X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
            if (c.isTichXanh)
            {
                ModFunc.PaintTicks(g, X + 60, 5);
            }
            if (c.cMaxStamina > 0 && GameScr.imgMP != null)
            {
                mFont.tahoma_7_yellow.drawString(g, mResources.vitality, X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
                if (GameScr.imgMPLost != null)
                {
                    g.drawImage(GameScr.imgMPLost, X + 95, 19, 0);
                }
                int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / c.cMaxStamina;
                g.setClip(95, X + 19, num, 20);
                g.drawImage(GameScr.imgMP, X + 95, 19, 0);
            }
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            if (c.cPower > 0)
            {
                mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
            }
            mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintCharInfo: " + ex.ToString());
        }
    }

    // paintZoneInfo() extracted to Panel.ZoneMap.cs

    public int getCompare(Item item)
    {
        if (item == null)
        {
            return -1;
        }
        if (item.isTypeBody())
        {
            if (item.itemOption == null)
            {
                return -1;
            }
            ItemOption itemOption = item.itemOption[0];
            if (itemOption.optionTemplate.id == 22)
            {
                itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
                itemOption.param *= 1000;
            }
            if (itemOption.optionTemplate.id == 23)
            {
                itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
                itemOption.param *= 1000;
            }
            Item item2 = null;
            for (int i = 0; i < Char.myCharz().arrItemBody.Length; i++)
            {
                Item item3 = Char.myCharz().arrItemBody[i];
                if (itemOption.optionTemplate.id == 22)
                {
                    itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
                    itemOption.param *= 1000;
                }
                if (itemOption.optionTemplate.id == 23)
                {
                    itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
                    itemOption.param *= 1000;
                }
                if (item3 != null && item3.itemOption != null && item3.template.type == item.template.type)
                {
                    item2 = item3;
                    break;
                }
            }
            if (item2 == null)
            {
                isUp = true;
                return itemOption.param;
            }
            int num = 0;
            num = ((item2 == null || item2.itemOption == null) ? itemOption.param : (itemOption.param - item2.itemOption[0].param));
            if (num < 0)
            {
                isUp = false;
            }
            else
            {
                isUp = true;
            }
            return num;
        }
        return 0;
    }

    // paintMapInfo() extracted to Panel.ZoneMap.cs

    // paintShopInfo() extracted to Panel.Shop.cs

    // paintItemBoxInfo() extracted to Panel.Box.cs

    // paintSkillInfo() extracted to Panel.Skill.cs
    // Hàm định dạng số nếu trên 2 tỷ
    // Hàm định dạng số nếu trên 2 tỷ
    // Hàm định dạng số lớn theo đơn vị Tỷ, Tỷ Tỷ...
    public static String formatLargeNumber(long value)
    {
        if (value >= 1_000_000_000_000_000_000L)
        {
            return (value / 1_000_000_000_000_000_000L) + " B Tỷ";
        }else
        if (value >= 1_000_000_000_000_000L)
        {
            return (value / 1_000_000_000_000_000L) + " M Tỷ";
        }
        else if (value >= 1_000_000_000_000L)
        {
            return (value / 1_000_000_000_000L) + " K Tỷ";
        }
        //else if (value >= 1_000_000_000L)
        //{
        //    return (value / 1_000_000_000L) + " Tỷ";
        //}
        else
        {
            return value + ""; // Chuyển đổi số thành chuỗi nếu dưới 1 tỷ
        }
    }



    private void paintItemBodyBagInfo(mGraphics g)
    {
        //mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + Char.myCharz().cHP + " / " + Char.myCharz().cHPFull, X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + Char.myCharz().cMP + " / " + Char.myCharz().cMPFull, X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + Char.myCharz().cDamFull, X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + Char.myCharz().cDefull + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
        // Sử dụng hàm định dạng trong việc vẽ chuỗi
        mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + formatLargeNumber(Char.myCharz().cHP) + " / " + formatLargeNumber(Char.myCharz().cHPFull), X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + formatLargeNumber(Char.myCharz().cMP) + " / " + formatLargeNumber(Char.myCharz().cMPFull), X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + formatLargeNumber(Char.myCharz().cDamFull), X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + formatLargeNumber(Char.myCharz().cDefull) + ", " + mResources.critical + ": " + formatLargeNumber(Char.myCharz().cCriticalFull) + "%", X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
    }

    private void paintItemBodyBagInfo(mGraphics g, int x, int y)
    {
        //mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + Char.myCharz().cHP + " / " + Char.myCharz().cHPFull, x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + Char.myCharz().cMP + " / " + Char.myCharz().cMPFull, x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + Char.myCharz().cDamFull, x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
        //mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + Char.myCharz().cDefull + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);

        mFont.tahoma_7_yellow.drawString(g, mResources.HP + ": " + formatLargeNumber(Char.myCharz().cHP) + " / " + formatLargeNumber(Char.myCharz().cHPFull), x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.KI + ": " + formatLargeNumber(Char.myCharz().cMP) + " / " + formatLargeNumber(Char.myCharz().cMPFull), x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + formatLargeNumber(Char.myCharz().cDamFull), x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_yellow.drawString(g, mResources.armor + ": " + formatLargeNumber(Char.myCharz().cDefull) + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
    }

    private void paintTopInfo(mGraphics g)
    {
        try
        {
            g.setClip(X + 1, Y, W - 2, yScroll - 2);
            g.setColor(9993045);
            g.fillRect(X, Y, W - 2, 50);
            switch (type)
            {
            case 13:
                if (currentTabIndex == 0 || currentTabIndex == 1)
                {
                    if (Equals(GameCanvas.panel))
                    {
                        SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                        paintGiaoDichInfo(g);
                    }
                    if (Equals(GameCanvas.panel2) && charMenu != null)
                    {
                        SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                        paintCharInfo(g, charMenu);
                    }
                }
                if (currentTabIndex == 2 && charMenu != null)
                {
                    SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                    paintCharInfo(g, charMenu);
                }
                break;
            case 12:
                if (currentTabIndex == 0)
                {
                    int id = 1410;
                    for (int i = 0; i < GameScr.vNpc.size(); i++)
                    {
                        Npc npc = (Npc)GameScr.vNpc.elementAt(i);
                        if (npc.template.npcTemplateId == idNPC)
                        {
                            id = npc.avatar;
                        }
                    }
                    SmallImage.drawSmallImage(g, id, X + 25, 50, 0, 33);
                    paintCombineInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintMyInfo(g);
                }
                break;
            case 11:
            case 16:
            case 23:
            case 24:
            case 27:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 15:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 9:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 21:
            case 28:
                Char pet = type == 28 ? Char.MyPet2z() : Char.myPetz();
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetInfo(g, type == 28);
                }
                else if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetSkillInfo(g, type == 28);
                }
                else if (currentTabIndex == 2)
                {
                    SmallImage.drawSmallImage(g, pet.avatarz(), X + 25, 50, 0, 33);
                    paintPetStatusInfo(g, type == 28);
                }
                else if (currentTabIndex == 3)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintItemBodyBagInfo(g);
                }
                break;
            case 0:
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintMyInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    if (isnewInventory)
                    {
                        paintCharInfo(g, Char.myCharz());
                    }
                    else
                    {
                        paintItemBodyBagInfo(g);
                    }
                }
                if (currentTabIndex == 2)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintSkillInfo(g);
                }
                if (currentTabIndex == 3)
                {
                    if (mainTabName.Length == 5)
                    {
                        paintClanInfo(g);
                    }
                    else
                    {
                        SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                        paintToolInfo(g);
                    }
                }
                if (currentTabIndex == 4)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintToolInfo(g);
                }
                break;
            case 25:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 2:
                if (currentTabIndex == 0)
                {
                    SmallImage.drawSmallImage(g, 526, X + 25, 50, 0, 33);
                    paintItemBoxInfo(g);
                }
                if (currentTabIndex == 1)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                    paintItemBodyBagInfo(g);
                }
                break;
            case 3:
                SmallImage.drawSmallImage(g, 561, X + 25, 50, 0, 33);
                paintZoneInfo(g);
                break;
            case 1:
                if (currentTabIndex == currentTabName.Length - 1 && GameCanvas.panel2 == null)
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                }
                else
                {
                    SmallImage.drawSmallImage(g, Char.myCharz().npcFocus.avatar, X + 25, 50, 0, 33);
                }
                paintShopInfo(g);
                break;
            case 4:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMapInfo(g);
                break;
            case 7:
            case 17:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 8:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 10:
                if (charMenu != null)
                {
                    SmallImage.drawSmallImage(g, charMenu.avatarz(), X + 25, 50, 0, 33);
                    paintCharInfo(g, charMenu);
                }
                break;
            case 14:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMapInfo(g);
                break;
            case 18:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintMyInfo(g);
                break;
            case 19:
            case 26:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 20:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 22:
                SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), X + 25, 50, 0, 33);
                paintToolInfo(g);
                break;
            case 5:
            case 6:
                break;
        }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paintTopInfo inner: " + ex.ToString());
        }
    }

    // paintChatManager() extracted to Panel.Social.cs

    // paintChatPlayer() extracted to Panel.Social.cs

    private string getStatus(int status)
    {
        return status switch
        {
            0 => mResources.follow,
            1 => mResources.defend,
            2 => mResources.attack,
            3 => mResources.gohome,
            _ => "aaa",
        };
    }

    // paintPetStatusInfo() extracted to Panel.Pet.cs

    // paintCombineInfo(mGraphics g) được tách sang Panel.Upgrade.cs


    private void paintInfomation(mGraphics g)
    {
    }

    // paintMap() extracted to Panel.ZoneMap.cs

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

    public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            string text = arr[i];
            if (text.StartsWith("c"))
            {
                if (text.StartsWith("c0"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_dark;
                }
                else if (text.StartsWith("c1"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_yellow;
                }
                else if (text.StartsWith("c2"))
                {
                    text = text.Substring(2);
                    f = mFont.tahoma_7b_green;
                }
            }
            if (i == 0)
            {
                f.drawString(g, text, x, y, align);
                continue;
            }
            if (i < indexRow + 30 && i > indexRow - 30)
            {
                f.drawString(g, text, x, y += 12, align);
            }
            else
            {
                y += 12;
            }
            yPaint += 12;
            indexRowMax++;
        }
    }

    public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
    {
        int num = ((!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20);
        string[] array = f.splitFontArray(str, inforW - num);
        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0)
            {
                f.drawString(g, array[i], x, y, align);
                continue;
            }
            if (i < indexRow + 15 && i > indexRow - 15)
            {
                f.drawString(g, array[i], x, y += 12, align);
            }
            else
            {
                y += 12;
            }
            yPaint += 12;
            indexRowMax++;
        }
    }

    // cleanCombine() được tách sang Panel.Upgrade.cs


    public void hideNow()
    {
        if (timeShow > 0)
        {
            isClose = false;
            return;
        }
        if (isTypeShop())
        {
            Char.myCharz().resetPartTemp();
        }
        if (chatTField != null && type == 13 && chatTField.isShow)
        {
            chatTField = null;
        }
        if (type == 13 && !isAccept)
        {
            Service.gI().giaodich(3, -1, -1, -1);
        }
        SoundMn.gI().buttonClose();
        GameScr.isPaint = true;
        TileMap.lastPlanetId = -1;
        imgMap = null;
        mSystem.gcc();
        isClanOption = false;
        isClose = true;
        cleanCombine();
        Hint.clickNpc();
        GameCanvas.panel2 = null;
        GameCanvas.clearAllPointerEvent();
        GameCanvas.clearKeyPressed();
        pointerDownTime = (pointerDownFirstX = 0);
        pointerIsDowning = false;
        isShow = false;
        if ((Char.myCharz().cHP <= 0 || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5) && Char.myCharz().meDead)
        {
            Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
            GameScr.gI().center = center;
            Char.myCharz().cHP = 0;
        }
    }

    public void hide()
    {
        if (timeShow > 0)
        {
            isClose = false;
            return;
        }
        if (isTypeShop())
        {
            Char.myCharz().resetPartTemp();
        }
        if (chatTField != null && type == 13 && chatTField.isShow)
        {
            chatTField = null;
        }
        if (type == 13 && !isAccept)
        {
            Service.gI().giaodich(3, -1, -1, -1);
        }
        if (type == 15)
        {
            Service.gI().sendThachDau(-1);
        }
        SoundMn.gI().buttonClose();
        GameScr.isPaint = true;
        TileMap.lastPlanetId = -1;
        if (imgMap != null)
        {
            imgMap.texture = null;
            imgMap = null;
        }
        mSystem.gcc();
        isClanOption = false;
        if (type != 4)
        {
            if (type == 24)
            {
                setTypeGameInfo();
            }
            else if (type == 23)
            {
                setTypeMain();
            }
            else if (type == 3 || type == 14)
            {
                if (isChangeZone)
                {
                    isClose = true;
                }
                else
                {
                    setTypeMain();
                    cmx = cmtoX = 0;
                }
            }
            else if (type == 18 || type == 19 || type == 20 || type == 21 || type == 26 || type == 27 || type == 28)
            {
                setTypeMain();
                cmx = cmtoX = 0;
            }
            else if (type == 8 || type == 11 || type == 16)
            {
                setTypeAccount();
                cmx = (cmtoX = 0);
            }
            else
            {
                isClose = true;
            }
        }
        else
        {
            setTypeMain();
            cmx = (cmtoX = 0);
        }
        Hint.clickNpc();
        GameCanvas.panel2 = null;
        GameCanvas.clearAllPointerEvent();
        GameCanvas.clearKeyPressed();
        GameCanvas.isFocusPanel2 = false;
        pointerDownTime = (pointerDownFirstX = 0);
        pointerIsDowning = false;
        if ((Char.myCharz().cHP <= 0 || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5) && Char.myCharz().meDead)
        {
            Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
            GameScr.gI().center = center;
            Char.myCharz().cHP = 0;
        }
    }

    public void update()
    {
        if (chatTField != null && chatTField.isShow)
        {
            chatTField.update();
            return;
        }
        if (isKiguiXu)
        {
            delayKigui++;
            if (delayKigui == 10)
            {
                delayKigui = 0;
                isKiguiXu = false;
                chatTField.tfChat.setText(string.Empty);
                chatTField.strChat = mResources.kiguiXuchat + " ";
                chatTField.tfChat.name = mResources.input_money;
                chatTField.to = string.Empty;
                chatTField.isShow = true;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
                chatTField.tfChat.setMaxTextLenght(9);
                if (GameCanvas.isTouch)
                {
                    chatTField.tfChat.doChangeToTextBox();
                }
                if (Main.isWindowsPhone)
                {
                    chatTField.tfChat.strInfo = chatTField.strChat;
                }
                if (!Main.isPC)
                {
                    chatTField.startChat(this, string.Empty);
                }
            }
            return;
        }
        if (isKiguiLuong)
        {
            delayKigui++;
            if (delayKigui == 10)
            {
                delayKigui = 0;
                isKiguiLuong = false;
                chatTField.tfChat.setText(string.Empty);
                chatTField.strChat = mResources.kiguiLuongchat + "  ";
                chatTField.tfChat.name = mResources.input_money;
                chatTField.to = string.Empty;
                chatTField.isShow = true;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
                chatTField.tfChat.setMaxTextLenght(9);
                if (GameCanvas.isTouch)
                {
                    chatTField.tfChat.doChangeToTextBox();
                }
                if (Main.isWindowsPhone)
                {
                    chatTField.tfChat.strInfo = chatTField.strChat;
                }
                if (!Main.isPC)
                {
                    chatTField.startChat(this, string.Empty);
                }
            }
            return;
        }
        if (scroll != null)
        {
            scroll.updatecm();
        }
        if (tabIcon != null && tabIcon.isShow)
        {
            tabIcon.update();
            return;
        }
        moveCamera();
        if (isTabInven() && isnewInventory)
        {
            if (eBanner == null)
            {
                eBanner = new Effect(205, 0, 0, 3, 10, -1);
                eBanner.typeEff = 2;
            }
            if (eBanner != null)
            {
                eBanner.update();
            }
        }
        if (waitToPerform > 0)
        {
            waitToPerform--;
            if (waitToPerform == 0)
            {
                lastSelect[currentTabIndex] = selected;
                switch (type)
                {
                    case 23:
                        doFireGameInfo();
                        break;
                    case 21:
                        doFirePetMain();
                        break;
                    case 28:
                        DoFirePet2Main();
                        break;
                    case 0:
                        doFireMain();
                        break;
                    case 2:
                        doFireBox();
                        break;
                    case 3:
                        doFireZone();
                        break;
                    case 1:
                    case 17:
                        doFireShop();
                        break;
                    case 25:
                        doSpeacialSkill();
                        break;
                    case 4:
                        doFireMap();
                        break;
                    case 14:
                        doFireMapTrans();
                        break;
                    case 7:
                        if (Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2)
                        {
                            doFireBox();
                            return;
                        }
                        doFireInventory();
                        break;
                    case 8:
                        doFireLogMessage();
                        break;
                    case 9:
                        doFireArchivement();
                        break;
                    case 10:
                        doFirePlayerMenu();
                        break;
                    case 11:
                        doFireFriend();
                        break;
                    case 16:
                        doFireEnemy();
                        break;
                    case 15:
                        doFireTop();
                        break;
                    case 12:
                        doFireCombine();
                        break;
                    case 13:
                        doFireGiaoDich();
                        break;
                    case 18:
                        doFireChangeFlag();
                        break;
                    case 19:
                        doFireOption();
                        break;
                    case 20:
                        doFireAccount();
                        break;
                    case 22:
                        doFireAuto();
                        break;
                    case 26:
                        DoFireModFunc();
                        break;
                }
            }
        }
        for (int i = 0; i < ClanMessage.vMessage.size(); i++)
        {
            ((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
        }
        updateCombineEff();
    }

    // doSpeacialSkill() extracted to Panel.Skill.cs

    private void doFireGameInfo()
    {
        if (selected != -1)
        {
            infoSelect = selected;
            ((GameInfo)vGameInfo.elementAt(infoSelect)).hasRead = true;
            Rms.saveRMSInt(((GameInfo)vGameInfo.elementAt(infoSelect)).id + string.Empty, 1);
            setTypeGameSubInfo();
        }
    }

    private void doFireAuto()
    {
    }

    // DoFirePet2Main() extracted to Panel.Pet.cs

    // doFirePetMain() extracted to Panel.Pet.cs

    // doFirePetStatus() extracted to Panel.Pet.cs

    private void doFireTop()
    {
        if (selected >= -1)
        {
            if (isThachDau)
            {
                Service.gI().sendTop(topName, (sbyte)selected);
                return;
            }
            MyVector myVector = new(string.Empty);
            myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)vTop.elementAt(selected)));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
            addThachDauDetail((TopInfo)vTop.elementAt(selected));
        }
    }

    // doFireMapTrans() extracted to Panel.ZoneMap.cs

    // doFireGiaoDich() extracted to Panel.Trade.cs

    // doFireCombine() được tách sang Panel.Upgrade.cs


    // doFirePlayerMenu() extracted to Panel.Social.cs

    // doFireShop() extracted to Panel.Shop.cs

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

    private void doFireInventory()
    {
        if (Char.myCharz().statusMe == 14)
        {
            GameCanvas.startOKDlg(mResources.can_not_do_when_die);
        }
        else
        {
            if (selected == -1)
            {
                return;
            }
            if (selected == 0)
            {
                setNewSelected(Char.myCharz().arrItemBody.Length + Char.myCharz().arrItemBag.Length, resetSelect: false);
                return;
            }
            currItem = null;
            MyVector myVector = new MyVector();
            if (isnewInventory && isnewInventory)
            {
                currItem = itemInvenNew;
                if (newSelected == 0)
                {
                    myVector.addElement(new Command(mResources.GETOUT, this, 2002, currItem));
                }
                else if (GameCanvas.panel.type == 12)
                {
                    myVector.addElement(new Command(mResources.use_for_combine, this, 6000, currItem));
                }
                else if (GameCanvas.panel.type == 13)
                {
                    myVector.addElement(new Command(mResources.use_for_trade, this, 7000, currItem));
                }
                else if (currItem.isTypeBody())
                {
                    myVector.addElement(new Command(mResources.USE, this, 2000, currItem));
                    if (Char.myCharz().havePet)
                    {
                        myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                    }
                    if (Char.myCharz().havePet2)
                    {
                        myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                    }
                }
                else
                {
                    myVector.addElement(new Command(mResources.USE, this, 2001, currItem));
                    if (Char.myCharz().havePet)
                    {
                        myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                    }
                    if (Char.myCharz().havePet2)
                    {
                        myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                    }
                }
            }
            else if (!GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody))
            {
                Item item = Char.myCharz().arrItemBag[GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)];
                if (item != null)
                {
                    currItem = item;
                    if (GameCanvas.panel.type == 12)
                    {
                        myVector.addElement(new Command(mResources.use_for_combine, this, 6000, currItem));
                    }
                    else if (GameCanvas.panel.type == 13)
                    {
                        myVector.addElement(new Command(mResources.use_for_trade, this, 7000, currItem));
                    }
                    else if (item.isTypeBody())
                    {
                        myVector.addElement(new Command(mResources.USE, this, 2000, currItem));
                        if (Char.myCharz().havePet)
                        {
                            myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                        }
                        if (Char.myCharz().havePet2)
                        {
                            myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                        }
                    }
                    else
                    {
                        myVector.addElement(new Command(mResources.USE, this, 2001, currItem));
                        if (Char.myCharz().havePet)
                        {
                            myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, currItem));
                        }
                        if (Char.myCharz().havePet2)
                        {
                            myVector.addElement(new Command(ModFunc.strUseForPet2, this, 2007, currItem));
                        }
                    }
                }
            }
            else
            {
                Item item2 = Char.myCharz().arrItemBody[GetInventorySelect_body(selected, newSelected)];
                if (item2 != null)
                {
                    currItem = item2;
                    myVector.addElement(new Command(mResources.GETOUT, this, 2002, currItem));
                }
            }
            if (currItem != null)
            {
                Char.myCharz().setPartTemp(currItem.headTemp, currItem.bodyTemp, currItem.legTemp, currItem.bagTemp);
                if (GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13)
                {
                    if (position == 0)
                    {
                        myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, currItem));
                        if (this.currItem.template.type == 29 || this.currItem.template.type == 33 || this.currItem.template.id == 380 || this.currItem.quantity >= 2)
                        {
                            if (ModFunc.GI().listItemAuto.Exists(i => i.id == currItem.template.id))
                            {
                                myVector.addElement(new Command(ModFunc.strRemoveAutoItem, ModFunc.GI(), 501, currItem));
                            }
                            else
                            {
                                myVector.addElement(new Command(ModFunc.strAddAutoItem, ModFunc.GI(), 500, currItem));
                            }
                        }
                    }
                    if (position == 1)
                    {
                        myVector.addElement(new Command(mResources.SALE, this, 3002, currItem));
                    }
                }
                int numScrollY = ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1)) ? (yScroll + 22) : yScroll;
                GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + numScrollY);
                addItemDetail(currItem);
            }
            else
            {
                cp = null;
            }
        }
    }

    private void doRada()
    {
        hide();
        if (RadarScr.list == null || RadarScr.list.size() == 0)
        {
            Service.gI().SendRada(0, -1);
            RadarScr.gI().switchToMe();
        }
        else
        {
            RadarScr.gI().switchToMe();
        }
    }

    private void doFireTool()
    {
        if (selected < 0)
        {
            return;
        }
        if (SoundMn.IsDelAcc && selected == strTool.Length - 1)
        {
            Service.gI().sendDelAcc();
            return;
        }
        if (!Char.myCharz().havePet && !Char.myCharz().havePet2)
        {
            switch (selected)
            {
                case 0:
                    setTypeGameInfo();
                    break;
                case 1:
                    SetTypeModFunc();
                    break;
                case 2:
                    SetTypePlayerInfo();
                    break;
                case 3:
                    doRada();
                    break;
                case 4:
                    Service.gI().getFlag(0, -1);
                    InfoDlg.showWait();
                    break;
                case 5:
                    if (Char.myCharz().statusMe == 14)
                    {
                        GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                    }
                    else
                    {
                        ModFunc.GI().userOpenZones = true;
                        Service.gI().openUIZone();
                    }
                    break;
                case 6:
                    ModFunc.DoChatGlobal();
                    break;
                case 7:
                    setTypeAccount();
                    break;
                case 8:
                    setTypeOption();
                    break;
                case 9:
                    GameCanvas.loginScr.backToRegister();
                    break;
                case 10:
                    if (GameCanvas.loginScr.isLogin2)
                    {
                        SoundMn.gI().backToRegister();
                    }
                    break;
            }
            return;
        }
        if (Char.myCharz().havePet && Char.myCharz().havePet2)
        {
            switch (selected)
            {
                case 0:
                    setTypeGameInfo();
                    break;
                case 1:
                    SetTypeModFunc();
                    break;
                case 2:
                    SetTypePlayerInfo();
                    break;
                case 3:
                    doRada();
                    break;
                case 4:
                    doFirePet();
                    break;
                case 5:
                    doFirePet2();
                    break;
                case 6:
                    Service.gI().getFlag(0, -1);
                    InfoDlg.showWait();
                    break;
                case 7:
                    if (Char.myCharz().statusMe == 14)
                    {
                        GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                    }
                    else
                    {
                        ModFunc.GI().userOpenZones = true;
                        Service.gI().openUIZone();
                    }
                    break;
                case 8:
                    ModFunc.DoChatGlobal();
                    break;
                case 9:
                    setTypeAccount();
                    break;
                case 10:
                    setTypeOption();
                    break;
                case 11:
                    GameCanvas.loginScr.backToRegister();
                    break;
                case 12:
                    if (GameCanvas.loginScr.isLogin2)
                    {
                        SoundMn.gI().backToRegister();
                    }
                    break;
            }
            return;
        }
        switch (selected)
        {
            case 0:
                setTypeGameInfo();
                break;
            case 1:
                SetTypeModFunc();
                break;
            case 2:
                SetTypePlayerInfo();
                break;
            case 3:
                doRada();
                break;
            case 4:
                if (Char.myCharz().havePet)
                {
                    doFirePet();
                }
                else
                {
                    doFirePet2();
                }
                break;
            case 5:
                Service.gI().getFlag(0, -1);
                InfoDlg.showWait();
                break;
            case 6:
                if (Char.myCharz().statusMe == 14)
                {
                    GameCanvas.startOKDlg(mResources.can_not_do_when_die);
                }
                else
                {
                    ModFunc.GI().userOpenZones = true;
                    Service.gI().openUIZone();
                }
                break;
            case 7:
                ModFunc.DoChatGlobal();
                break;
            case 8:
                setTypeAccount();
                break;
            case 9:
                setTypeOption();
                break;
            case 10:
                GameCanvas.loginScr.backToRegister();
                break;
            case 11:
                if (GameCanvas.loginScr.isLogin2)
                {
                    SoundMn.gI().backToRegister();
                }
                break;
        }
    }

    private void setTypeGameSubInfo()
    {
        string content = ((GameInfo)vGameInfo.elementAt(infoSelect)).content;
        contenInfo = mFont.tahoma_7_grey.splitFontArray(content, wScroll - 40);
        currentListLength = contenInfo.Length;
        ITEM_HEIGHT = 16;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
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
            cmy = (cmtoY = cmyLim);
        }
        type = 24;
        setType(0);
    }

    private void SetTypePlayerInfo()
    {
        string content = "Tộc: " + (Char.myCharz().cgender == 0 ? "Trái Đất" : Char.myCharz().cgender == 1 ? "Namek" : "Xayda") + "\n" +
            "HP: " + NinjaUtil.getMoneys(Char.myCharz().cHP) + " / " + NinjaUtil.getMoneys(Char.myCharz().cHPFull) + "\n" +
            "KI: " + NinjaUtil.getMoneys(Char.myCharz().cMP) + " / " + NinjaUtil.getMoneys(Char.myCharz().cMPFull) + "\n" +
            "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull) + "\n" +
            "Chí mạng: " + Char.myCharz().cCriticalFull + "%\n" +
            "Giảm sát thương: " + Char.myCharz().tlDef + "%\n" +
            "Phản sát thương: " + Char.myCharz().tlPst + "%\n" +
            "Né đòn: " + Char.myCharz().tlNeDon + "%\n" +
            "Hút HP: " + Char.myCharz().tlHutHp + "%\n" +
            "Hút KI: " + Char.myCharz().tlHutMp + "%\n" +

            "Giảm TDHS: " + Char.myCharz().tileGiamTDHS + "%\n" +
            "Giảm TDHS: " + Char.myCharz().timeGiamTDHS + " giây\n" +
            "Kháng TDHS: " + (Char.myCharz().khangTDHS ? "Có" : "Không") + "\n" +
            "Kháng lạnh: " + (Char.myCharz().isKhongLanh ? "Có" : "Không") + "\n" +
            "Vô hình: " + (Char.myCharz().wearingVoHinh ? "Có" : "Không") + "\n" +
            "Dịch chuyển: " + (Char.myCharz().teleport ? "Có" : "Không") + "\n";
        contenInfo = mFont.tahoma_7_grey.splitFontArray(content, wScroll - 40);
        currentListLength = contenInfo.Length;
        ITEM_HEIGHT = 16;
        selected = GameCanvas.isTouch ? (-1) : 0;
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
        if (cmyLim < 0)
        {
            cmyLim = 0;
        }
        if (cmy < 0)
        {
            cmy = cmtoY = 0;
        }
        if (cmy > cmyLim)
        {
            cmy = cmtoY = cmyLim;
        }
        type = 27;
        setType(0);
    }

    private void setTypeGameInfo()
    {
        currentListLength = vGameInfo.size();
        ITEM_HEIGHT = 24;
        selected = (GameCanvas.isTouch ? (-1) : 0);
        cmyLim = currentListLength * ITEM_HEIGHT - hScroll;
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
            cmy = (cmtoY = cmyLim);
        }
        type = 23;
        setType(0);
    }

    // doFirePet() extracted to Panel.Pet.cs

    // doFirePet2() extracted to Panel.Pet.cs

    // searchClan() extracted to Panel.Clan.cs

    // chatClan() extracted to Panel.Clan.cs

    // creatClan() extracted to Panel.Clan.cs

    public void putMoney()
    {
        if (chatTField == null)
        {
            chatTField = new ChatTextField();
            chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
            chatTField.initChatTextField();
            chatTField.parentScreen = GameCanvas.panel;
        }
        chatTField.strChat = mResources.input_money_to_trade;
        chatTField.tfChat.name = mResources.input_money;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
        chatTField.tfChat.setMaxTextLenght(9);
        if (GameCanvas.isTouch)
        {
            chatTField.tfChat.doChangeToTextBox();
        }
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }

    public void putQuantily()
    {
        if (chatTField == null)
        {
            chatTField = new ChatTextField();
            chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
            chatTField.initChatTextField();
            chatTField.parentScreen = GameCanvas.panel;
        }
        chatTField.strChat = mResources.input_quantity_to_trade;
        chatTField.tfChat.name = mResources.input_quantity;
        chatTField.to = string.Empty;
        chatTField.isShow = true;
        chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
        if (GameCanvas.isTouch)
        {
            chatTField.tfChat.doChangeToTextBox();
        }
        if (Main.isWindowsPhone)
        {
            chatTField.tfChat.strInfo = chatTField.strChat;
        }
        if (!Main.isPC)
        {
            chatTField.startChat(this, string.Empty);
        }
    }

    // chagenSlogan() extracted to Panel.Clan.cs

    // changeIcon() extracted to Panel.Clan.cs

    // addFriend() extracted to Panel.Social.cs

    // doFireEnemy() extracted to Panel.Social.cs

    // doFireFriend() extracted to Panel.Social.cs

    private void doFireChangeFlag()
    {
        if (selected >= 0)
        {
            MyVector myVector = new MyVector();
            currInfoItem = selected;
            myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
            myVector.addElement(new Command(mResources.BACK, this, 10031, null));
            GameCanvas.menu.startAt(myVector, X, (selected + 1) * ITEM_HEIGHT - cmy + yScroll);
        }
    }

    // doFireLogMessage() extracted to Panel.Social.cs

    // doFireClanOption() extracted to Panel.Clan.cs

    private void doFireMain()
    {
        try
        {
            if (currentTabIndex == 0)
            {
                setTypeMap();
            }
            if (currentTabIndex == 1)
            {
                doFireInventory();
            }
            if (currentTabIndex == 2)
            {
                doFireSkill();
            }
            if (currentTabIndex == 3)
            {
                if (mainTabName.Length == 4)
                {
                    doFireTool();
                }
                else
                {
                    doFireClanOption();
                }
            }
            if (currentTabIndex == 4)
            {
                doFireTool();
            }
        }
        catch (Exception ex)
        {
            Res.outz("Throw ex " + ex.StackTrace);
        }
    }

    // doFireSkill() extracted to Panel.Skill.cs

    // DoFirePetSkill() extracted to Panel.Pet.cs

    // addLogMessage() extracted to Panel.Social.cs

    // addSkillDetail2() extracted to Panel.Pet.cs

    // doFireClanIcon() extracted to Panel.Clan.cs

    // doFireMap() extracted to Panel.ZoneMap.cs

    // doFireZone() extracted to Panel.ZoneMap.cs

    public void updateRequest(int recieve, int maxCap)
    {
        cp.says[cp.says.Length - 1] = mResources.received + " " + recieve + "/" + maxCap;
    }

    // doFireBox() extracted to Panel.Box.cs

    public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
    {
        GameCanvas.endDlg();
        ItemObject itemObject = new ItemObject();
        itemObject.type = itemAction;
        itemObject.id = index;
        itemObject.where = where;
        GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
    }

    public void saleRequest(sbyte type, string info, short id)
    {
        ItemObject itemObject = new ItemObject();
        itemObject.type = type;
        itemObject.id = id;
        GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
    }

    public void perform(int idAction, object p)
    {
        if (idAction == 8010)
        {
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = this;
            }
            ModFunc.GI().MyChatTextField(chatTField, "Nhập số sao cần đập", "Chỉ được nhập số");
        }
        else if (idAction == 8011)
        {
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = this;
            }
            string infoIntrinsic = (string)p;
            ModFunc.GI().curSelectIntrinsic = infoIntrinsic;
            ModFunc.GI().MyChatTextField(chatTField, "Nhập chỉ số mong muốn", "Chỉ nhập số");
        }
        if (idAction == 9999)
        {
            TopInfo topInfo = (TopInfo)p;
            Service.gI().sendThachDau(topInfo.pId);
        }
        if (idAction == 170391)
        {
            Rms.clearAll();
            if (mGraphics.zoomLevel > 1)
            {
                Rms.saveRMSInt("levelScreenKN", 1);
            }
            else
            {
                Rms.saveRMSInt("levelScreenKN", 0);
            }
            GameMidlet.instance.exit();
        }
        if (idAction == 6001)
        {
            Item item = (Item)p;
            item.isSelect = false;
            GameCanvas.panel.vItemCombine.removeElement(item);
            if (GameCanvas.panel.currentTabIndex == 0)
            {
                GameCanvas.panel.setTabCombine();
            }
            if (ModFunc.GI().isAutoPhaLe)
            {
                ModFunc.GI().itemPhale = null;
                ModFunc.GI().maxPhale = -1;
                ModFunc.GI().currPhale = -1;
            }
        }
        if (idAction == 6000)
        {
            Item item2 = (Item)p;
            for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
            {
                Item item3 = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
                if (item3.template.id == item2.template.id)
                {
                    GameCanvas.startOKDlg(mResources.already_has_item);
                    return;
                }
            }
            item2.isSelect = true;
            GameCanvas.panel.vItemCombine.addElement(item2);
            if (GameCanvas.panel.currentTabIndex == 0)
            {
                GameCanvas.panel.setTabCombine();
            }
            if (ModFunc.GI().isAutoPhaLe)
            {
                ModFunc.GI().itemPhale = item2;
            }
        }
        if (idAction == 7000)
        {
            if (isLock)
            {
                GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
                return;
            }
            Item item4 = (Item)p;
            for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
            {
                Item item5 = (Item)GameCanvas.panel.vMyGD.elementAt(j);
                if (item5.indexUI == item4.indexUI)
                {
                    GameCanvas.startOKDlg(mResources.already_has_item);
                    return;
                }
            }
            if (item4.quantity > 1)
            {
                putQuantily();
                return;
            }
            item4.isSelect = true;
            Item item6 = new Item();
            item6.template = item4.template;
            item6.itemOption = item4.itemOption;
            item6.indexUI = item4.indexUI;
            GameCanvas.panel.vMyGD.addElement(item6);
            Service.gI().giaodich(2, -1, (sbyte)item6.indexUI, item6.quantity);
        }
        if (idAction == 7001)
        {
            Item item7 = (Item)p;
            item7.isSelect = false;
            GameCanvas.panel.vMyGD.removeElement(item7);
            if (GameCanvas.panel.currentTabIndex == 1)
            {
                GameCanvas.panel.setTabGiaoDich(isMe: true);
            }
            Service.gI().giaodich(4, -1, (sbyte)item7.indexUI, -1);
        }
        if (idAction == 7002)
        {
            isAccept = true;
            GameCanvas.endDlg();
            Service.gI().giaodich(7, -1, -1, -1);
            hide();
        }
        if (idAction == 8003)
        {
            InfoItem infoItem = (InfoItem)p;
            Service.gI().friend(1, infoItem.charInfo.charID);
            if (type != 8)
            {
            }
        }
        if (idAction == 8002)
        {
            InfoItem infoItem2 = (InfoItem)p;
            Service.gI().friend(2, infoItem2.charInfo.charID);
        }
        if (idAction == 8004)
        {
            int charID = (int)p;
            ModFunc.GI().TeleportToPlayer(charID);
        }
        if (idAction == 8001)
        {
            InfoItem infoItem4 = (InfoItem)p;
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = GameCanvas.panel;
            }
            chatTField.strChat = mResources.chat_player;
            chatTField.tfChat.name = mResources.chat_with + " " + infoItem4.charInfo.cName;
            chatTField.to = string.Empty;
            chatTField.isShow = true;
            chatTField.tfChat.isFocus = true;
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
            if (Main.isWindowsPhone)
            {
                chatTField.tfChat.strInfo = chatTField.strChat;
            }
            if (!Main.isPC)
            {
                chatTField.startChat(this, string.Empty);
            }
        }
        if (idAction == 1000)
        {
            Service.gI().getItem(BOX_BAG, (sbyte)GetInventorySelect_body(selected, newSelected));
        }
        if (idAction == 1001)
        {
            sbyte id = (sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody);
            Service.gI().getItem(BAG_BOX, id);
        }
        if (idAction == 1003)
        {
            hide();
        }
        if (idAction == 1002)
        {
            Service.gI().getItem(BODY_BOX, (sbyte)GetInventorySelect_body(selected, newSelected));
        }
        if (idAction == 2011)
        {
            Service.gI().useItem(1, 2, (sbyte)GetInventorySelect_body(selected, newSelected), -1);
        }
        if (idAction == 2010)
        {
            Service.gI().useItem(0, 2, (sbyte)GetInventorySelect_body(selected, newSelected), -1);
            Item item8 = (Item)p;
            if (item8 != null && (item8.template.id == 193 || item8.template.id == 194))
            {
                GameCanvas.panel.hide();
            }
        }
        if (idAction == 2000)
        {
            Item[] arrItemBody = Char.myCharz().arrItemBody;
            sbyte id2 = (sbyte)GetInventorySelect_bag(selected, newSelected, arrItemBody);
            if (isnewInventory)
            {
                id2 = (sbyte)currItem.indexUI;
            }
            Service.gI().getItem(BAG_BODY, id2);
        }
        if (idAction == 2001)
        {
            Res.outz("use item");
            Item item9 = (Item)p;
            bool inventorySelect_isbody = GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody);
            sbyte b = 0;
            b = (inventorySelect_isbody ? ((sbyte)GetInventorySelect_body(selected, newSelected)) : ((sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)));
            if (isnewInventory)
            {
                b = (sbyte)currItem.indexUI;
                sbyte where = 0;
                if (newSelected != 0)
                {
                    where = 1;
                }
                Service.gI().useItem(0, where, b, -1);
            }
            else
            {
                Service.gI().useItem(0, (sbyte)((!inventorySelect_isbody) ? 1 : 0), b, -1);
            }
            if (item9.template.id == 193 || item9.template.id == 194)
            {
                GameCanvas.panel.hide();
            }
        }
        if (idAction == 2002)
        {
            if (isnewInventory)
            {
                Service.gI().getItem(BODY_BAG, (sbyte)sellectInventory);
            }
            else
            {
                Service.gI().getItem(BODY_BAG, (sbyte)GetInventorySelect_body(selected, newSelected));
            }
        }
        if (idAction == 2003)
        {
            Res.outz("remove item");
            bool inventorySelect_isbody2 = GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody);
            sbyte b2 = 0;
            b2 = (inventorySelect_isbody2 ? ((sbyte)GetInventorySelect_body(selected, newSelected)) : ((sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)));
            Service.gI().useItem(1, (sbyte)((!inventorySelect_isbody2) ? 1 : 0), b2, -1);
        }
        if (idAction == 2004)
        {
            GameCanvas.endDlg();
            ItemObject itemObject = (ItemObject)p;
            sbyte where2 = (sbyte)itemObject.where;
            sbyte index = (sbyte)itemObject.id;
            Service.gI().useItem((sbyte)((itemObject.type != 0) ? 2 : 3), where2, index, -1);
        }
        if (idAction == 2005)
        {
            sbyte id3 = (sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody);
            Service.gI().getItem(BAG_PET, id3);
        }
        if (idAction == 2006)
        {
            sbyte id4 = (sbyte)selected;
            Service.gI().getItem(PET_BAG, id4);
        }
        if (idAction == 2007)
        {
            sbyte id3 = (sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody);
            Service.gI().getItem(BAG_PET2, id3);
        }
        if (idAction == 2008)
        {
            sbyte id4 = (sbyte)selected;
            Service.gI().getItem(PET2_BAG, id4);
        }
        if (idAction == 30001)
        {
            Res.outz("nhan do");
            Service.gI().buyItem(0, selected, 0);
        }
        if (idAction == 30002)
        {
            Res.outz("xoa do");
            Service.gI().buyItem(1, selected, 0);
        }
        if (idAction == 30003)
        {
            Res.outz("nhan tat");
            Service.gI().buyItem(2, selected, 0);
        }
        if (idAction == 3000)
        {
            Res.outz("mua do");
            Item item10 = (Item)p;
            Service.gI().buyItem(0, item10.template.id, 0);
        }
        if (idAction == 3001)
        {
            Item item11 = (Item)p;
            GameCanvas.msgdlg.pleasewait();
            Service.gI().buyItem(1, item11.template.id, 0);
        }
        if (idAction == 3002)
        {
            GameCanvas.endDlg();
            bool inventorySelect_isbody3 = GetInventorySelect_isbody(selected, newSelected, Char.myCharz().arrItemBody);
            sbyte b3 = 0;
            b3 = (inventorySelect_isbody3 ? ((sbyte)GetInventorySelect_body(selected, newSelected)) : ((sbyte)GetInventorySelect_bag(selected, newSelected, Char.myCharz().arrItemBody)));
            Service.gI().saleItem(0, (sbyte)((!inventorySelect_isbody3) ? 1 : 0), b3);
        }
        if (idAction == 3003)
        {
            GameCanvas.endDlg();
            ItemObject itemObject2 = (ItemObject)p;
            Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
        }
        if (idAction == 3004)
        {
            Item item12 = (Item)p;
            Service.gI().buyItem(3, item12.template.id, 0);
        }
        if (idAction == 3005)
        {
            // Mua do
            Item item13 = (Item)p;
            Service.gI().buyItem(3, item13.template.id, 0);
        }
        if (idAction == 3006)
        {
            Item item13 = (Item)p;
            ModFunc.GI().AutoBuyItem(20, item13);
        }
        if (idAction == 4000)
        {
            Clan clan = (Clan)p;
            if (clan != null)
            {
                GameCanvas.endDlg();
                Service.gI().clanMessage(2, null, clan.ID);
            }
        }
        if (idAction == 4001)
        {
            Clan clan2 = (Clan)p;
            if (clan2 != null)
            {
                InfoDlg.showWait();
                clanReport = mResources.PLEASEWAIT;
                Service.gI().clanMember(clan2.ID);
            }
        }
        if (idAction == 4005)
        {
            GameCanvas.endDlg();
        }
        if (idAction == 4007)
        {
            GameCanvas.endDlg();
        }
        if (idAction == 4006)
        {
            ClanMessage clanMessage = (ClanMessage)p;
            Service.gI().clanDonate(clanMessage.id);
        }
        if (idAction == 5001)
        {
            Member member = (Member)p;
            Service.gI().clanRemote(member.ID, 0);
        }
        if (idAction == 5002)
        {
            Member member2 = (Member)p;
            Service.gI().clanRemote(member2.ID, 1);
        }
        if (idAction == 5003)
        {
            Member member3 = (Member)p;
            Service.gI().clanRemote(member3.ID, 2);
        }
        if (idAction == 5004)
        {
            Member member4 = (Member)p;
            Service.gI().clanRemote(member4.ID, -1);
        }
        if (idAction == 9000)
        {
            bool forPet = p != null && (bool)p;
            Service.gI().upPotential(forPet, selected, 1);
            GameCanvas.endDlg();
            InfoDlg.showWait();
        }
        if (idAction == 9006)
        {
            bool forPet = p != null && (bool)p;
            Service.gI().upPotential(forPet, selected, 10);
            GameCanvas.endDlg();
            InfoDlg.showWait();
        }
        if (idAction == 9007)
        {
            bool forPet = p != null && (bool)p;
            Service.gI().upPotential(forPet, selected, 100);
            GameCanvas.endDlg();
            InfoDlg.showWait();
        }
        if (idAction == 9002)
        {
            Skill skill = (Skill)p;
            if (skill.template.isSkillSpec())
            {
                GameCanvas.startOKDlg(mResources.updSkill);
            }
            else
            {
                GameCanvas.startOKDlg(mResources.can_buy_from_Uron1 + skill.powRequire + mResources.can_buy_from_Uron2 + skill.moreInfo + mResources.can_buy_from_Uron3);
            }
        }
        if (idAction == 9003)
        {
            if (GameCanvas.isTouch && !Main.isPC)
            {
                GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
            }
            else
            {
                GameScr.gI().doSetKeySkill((SkillTemplate)p);
            }
        }
        if (idAction == 9004)
        {
            Skill skill2 = (Skill)p;
            if (skill2.template.isSkillSpec())
            {
                GameCanvas.startOKDlg(mResources.learnSkill);
            }
            else
            {
                GameCanvas.startOKDlg(mResources.can_buy_from_Uron1 + skill2.powRequire + mResources.can_buy_from_Uron2 + skill2.moreInfo + mResources.can_buy_from_Uron3);
            }
        }
        if (idAction == 10000)
        {
            InfoItem infoItem5 = (InfoItem)p;
            Service.gI().enemy(1, infoItem5.charInfo.charID);
            GameCanvas.panel.hideNow();
        }
        if (idAction == 10001)
        {
            InfoItem infoItem6 = (InfoItem)p;
            Service.gI().enemy(2, infoItem6.charInfo.charID);
            InfoDlg.showWait();
        }
        if (idAction == 10021)
        {
        }
        if (idAction == 10012)
        {
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
            }
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
            chatTField.tfChat.setText(string.Empty);
            if (currItem.quantity == 1)
            {
                chatTField.strChat = mResources.kiguiXuchat;
                chatTField.tfChat.name = mResources.input_money;
            }
            else
            {
                chatTField.strChat = mResources.input_quantity + " ";
                chatTField.tfChat.name = mResources.input_quantity;
            }
            chatTField.tfChat.setMaxTextLenght(9);
            chatTField.to = string.Empty;
            chatTField.isShow = true;
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
            if (GameCanvas.isTouch)
            {
                chatTField.tfChat.doChangeToTextBox();
            }
            if (Main.isWindowsPhone)
            {
                chatTField.tfChat.strInfo = chatTField.strChat;
            }
            if (!Main.isPC)
            {
                chatTField.startChat(this, string.Empty);
            }
        }
        if (idAction == 10013)
        {
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - global::ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
            }
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
            chatTField.tfChat.setText(string.Empty);
            if (currItem.quantity == 1)
            {
                chatTField.strChat = mResources.kiguiLuongchat;
                chatTField.tfChat.name = mResources.input_money;
            }
            else
            {
                chatTField.strChat = mResources.input_quantity + "  ";
                chatTField.tfChat.name = mResources.input_quantity;
            }
            chatTField.to = string.Empty;
            chatTField.isShow = true;
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
            if (GameCanvas.isTouch)
            {
                chatTField.tfChat.doChangeToTextBox();
            }
            if (Main.isWindowsPhone)
            {
                chatTField.tfChat.strInfo = chatTField.strChat;
            }
            if (!Main.isPC)
            {
                chatTField.startChat(this, string.Empty);
            }
        }
        if (idAction == 10014)
        {
            Item item14 = (Item)p;
            Service.gI().kigui(1, item14.itemId, -1, -1, -1);
            InfoDlg.showWait();
        }
        if (idAction == 10015)
        {
            Item item15 = (Item)p;
            Service.gI().kigui(2, item15.itemId, -1, -1, -1);
            InfoDlg.showWait();
        }
        if (idAction == 10016)
        {
            Item item16 = (Item)p;
            Service.gI().kigui(3, item16.itemId, 0, item16.buyCoin, -1);
            InfoDlg.showWait();
        }
        if (idAction == 10017)
        {
            Item item17 = (Item)p;
            Service.gI().kigui(3, item17.itemId, 1, item17.buyGold, -1);
            InfoDlg.showWait();
        }
        if (idAction == 10018)
        {
            Item item18 = (Item)p;
            Service.gI().kigui(5, item18.itemId, -1, -1, -1);
            InfoDlg.showWait();
        }
        if (idAction == 10019)
        {
            Session_ME.gI().close();
            Rms.saveRMSString("acc", string.Empty);
            Rms.saveRMSString("pass", string.Empty);
            GameCanvas.loginScr.tfPass.setText(string.Empty);
            GameCanvas.loginScr.tfUser.setText(string.Empty);
            GameCanvas.loginScr.isLogin2 = false;
            GameCanvas.loginScr.switchToMe();
            GameCanvas.endDlg();
            hide();
        }
        if (idAction == 10020)
        {
            GameCanvas.endDlg();
        }
        if (idAction == 10030)
        {
            Service.gI().getFlag(1, (sbyte)selected);
            GameCanvas.panel.hideNow();
        }
        if (idAction == 10031)
        {
            Session_ME.gI().close();
        }
        if (idAction == 11000)
        {
            Service.gI().kigui(0, currItem.itemId, 1, currItem.buyRuby, 1);
            GameCanvas.endDlg();
        }
        if (idAction == 11001)
        {
            Service.gI().kigui(0, currItem.itemId, 1, currItem.buyRuby, currItem.quantilyToBuy);
            GameCanvas.endDlg();
        }
        if (idAction == 11002)
        {
            chatTField.isShow = false;
            GameCanvas.endDlg();
        }
    }

    // onChatFromMe() extracted to Panel.Social.cs

    // onCancelChat() extracted to Panel.Social.cs

    // Toàn bộ logic hiệu ứng và đối thoại NPC nâng cấp (setCombineEff, updateCombineEff, paintCombineEff, paintCombineNPC, addTextCombineNPC) được tách sang Panel.Upgrade.cs


    // setTypeOption() extracted to Panel.ModOptions.cs

    // SetTypeModFunc() extracted to Panel.ModOptions.cs

    // SetTabModFunc() extracted to Panel.ModOptions.cs

    // setTabOption() extracted to Panel.ModOptions.cs

    // paintOption() extracted to Panel.ModOptions.cs

    // PaintModFunc() extracted to Panel.ModOptions.cs

    // doFireOption() extracted to Panel.ModOptions.cs

    // DoFireModFunc() extracted to Panel.ModOptions.cs

    // setTypeAccount() extracted to Panel.ModOptions.cs

    // setTabAccount() extracted to Panel.ModOptions.cs

    // paintAccount() extracted to Panel.ModOptions.cs

    // doFireAccount() extracted to Panel.ModOptions.cs

    // updateKeyOption() extracted to Panel.ModOptions.cs

    // setTypeSpeacialSkill() extracted to Panel.Skill.cs

    // setTabSpeacialSkill() extracted to Panel.Skill.cs

    // isTypeShop() extracted to Panel.Shop.cs

    // doNotiRuby() extracted to Panel.Shop.cs

    // paintUpgradeEffect() extracted to Panel.ItemRender.cs

    // upgradeEffectX() extracted to Panel.ItemRender.cs

    // upgradeEffectY() extracted to Panel.ItemRender.cs

    // GetColor_ItemBg() extracted to Panel.ItemRender.cs

    public static sbyte GetColor_Item_Upgrade(int lv)
    {
        if (lv < 0)
        {
            return 0;
        }
        switch (lv)
        {
            case 0:
            case 1: 
                return 4;
            case 3:
            case 2:
                return 1;
            case 4:
            case 5:
                return 2;   
            case 6:
            case 7:
                return 3;
            case 8:
                return 5;
            case 9:
                return 6;
            case 10:
                return 0;
            default:
                return 0;
        }
    }

    public static mFont GetFont(int color)
    {
        mFont result = mFont.tahoma_7;
        switch (color)
        {
            case -1:
                result = mFont.tahoma_7;
                break;
            case 0:
                result = mFont.tahoma_7b_dark;
                break;
            case 1:
                result = mFont.tahoma_7b_green;
                break;
            case 2:
                result = mFont.tahoma_7b_blue;
                break;
            case 3:
                result = mFont.tahoma_7b_blue;
                break;
            case 4:
                result = mFont.tahoma_7b_blue;
                break;
            case 5:
                result = mFont.tahoma_7b_blue;
                break;
            case 7:
                result = mFont.tahoma_7b_red;
                break;
            case 8:
                result = mFont.tahoma_7b_yellow;
                break;
        }
        return result;
    }

    // paintOptItem() extracted to Panel.ItemRender.cs
    // paintOptItemInventory() extracted to Panel.ItemRender.cs
    // paintOptSlotItem() extracted to Panel.ItemRender.cs

    // setTextColor() extracted to Panel.ItemRender.cs

    public int subTabInventory = 0;

    public bool isCurrentTabBody()
    {
        if (type == 7)
        {
            return true;
        }
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            return subTabInventory == 1;
        }
        return false;
    }

    public static string getBodySlotName(int index)
    {
        return index switch
        {
            0 => "Áo",
            1 => "Quần",
            2 => "Găng tay",
            3 => "Giày",
            4 => "Rada",
            5 => "Cải trang",
            6 => "Giáp tập luyện",
            7 => "Phụ kiện",
            8 => "Bông tai",
            9 => "Thú cưỡi",
            10 => "Pet theo sau",
            11 => "Pet bay",
            12 => "Danh hiệu",
            13 => "Ngọc bội",
            14 => "Hào quang",
            _ => "Trang bị " + index
        };
    }

    private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
    {
        return isCurrentTabBody();
    }

    private int GetInventorySelect_body(int select, int subSelect)
    {
        return select - 1;
    }

    private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
    {
        return select - 1;
    }

    private bool isTabInven()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 7 && currentTabIndex == 0) || (type == 2 && (currentTabIndex == 0 || currentTabIndex == 1)) || (type == 12 && currentTabIndex == 1) || ((type == 21 || type == 28) && currentTabIndex == 3))
        {
            return true;
        }
        return false;
    }

    private void updateKeyInvenTab()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            subTabInventory = (subTabInventory == 0) ? 1 : 0;
            setTabInventory(resetSelect: true);
            SoundMn.gI().panelClick();
        }
    }

    private void updateKeyInventory()
    {
        if ((type == 0 && currentTabIndex == 1) || (type == 2 && currentTabIndex == 1))
        {
            int subTabY = yScroll;
            int subTabH = 20;
            int subTabW = (wScroll - 2) / 2;
            int subTab0_x = xScroll + 1;
            int subTab1_x = xScroll + 1 + subTabW;
            if (GameCanvas.isPointerHoldIn(subTab0_x, subTabY, subTabW, subTabH))
            {
                if (GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.isPointerJustRelease = false;
                    if (subTabInventory != 0)
                    {
                        subTabInventory = 0;
                        setTabInventory(resetSelect: true);
                    }
                    SoundMn.gI().panelClick();
                }
                return;
            }
            if (GameCanvas.isPointerHoldIn(subTab1_x, subTabY, subTabW, subTabH))
            {
                if (GameCanvas.isPointerJustRelease)
                {
                    GameCanvas.isPointerJustRelease = false;
                    if (subTabInventory != 1)
                    {
                        subTabInventory = 1;
                        setTabInventory(resetSelect: true);
                    }
                    SoundMn.gI().panelClick();
                }
                return;
            }
        }
        updateKeyScrollView();
    }

    // IsTabOption() extracted to Panel.ModOptions.cs

    private int checkCurrentListLength(int arrLength)
    {
        newSelected = 0;
        size_tab = 1;
        return arrLength + 1;
    }

    private void setNewSelected(int arrLength, bool resetSelect)
    {
        newSelected = 0;
        setTabInventory(resetSelect);
    }
}
