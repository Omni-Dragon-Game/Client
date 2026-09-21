using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char : IMapObject
{
    public string xuStr;

    public string luongStr;

    public string luongKhoaStr;

    public long lastUpdateTime;

    public bool meLive;

    public bool isMask;

    public bool isTeleport;

    public bool isUsePlane;

    public int shadowX;

    public int shadowY;

    public int shadowLife;

    public bool isNhapThe;

    public PetFollow petFollow;

    public int rank;

    public const sbyte A_STAND = 1;

    public const sbyte A_RUN = 2;

    public const sbyte A_JUMP = 3;

    public const sbyte A_FALL = 4;

    public const sbyte A_DEADFLY = 5;

    public const sbyte A_NOTHING = 6;

    public const sbyte A_ATTK = 7;

    public const sbyte A_INJURE = 8;

    public const sbyte A_AUTOJUMP = 9;

    public const sbyte A_FLY = 10;

    public const sbyte SKILL_STAND = 12;

    public const sbyte SKILL_FALL = 13;

    public const sbyte A_DEAD = 14;

    public const sbyte A_HIDE = 15;

    public const sbyte A_RESETPOINT = 16;

    public static ChatPopup chatPopup;

    public long cPower;

    public Info chatInfo;

    public sbyte petStatus;

    public int cx = 24;

    public int cy = 24;

    public int cvx;

    public int cvy;

    public int cp1;

    public int cp2;

    public int cp3;

    public int statusMe = 5;

    public int cdir = 1;

    public int charID;

    public int cgender;

    public int ctaskId;

    public int menuSelect;

    public int cBonusSpeed;

    public int cspeed = 4;

    public int ccurrentAttack;

    public long cDamFull;

    public int cDefull;

    public int cCriticalFull;

    public int clevel;

    public long cMP;

    public long cHP;

    public long cHPNew;

    public int cMaxEXP;

    public long cHPShow;

    public int xReload;

    public int yReload;

    public int cyStartFall;

    public int saveStatus;

    public int eff5BuffHp;

    public int eff5BuffMp;

    public long cHPFull;

    public long cMPFull;

    public int cdameDown;

    public int cStr;

    public long cLevelPercent;

    public long cTiemNang;

    public long cNangdong;

    public long damHP;

    // TODO: Check this
    public int damMP;

    public bool isMob;

    public bool isCrit;

    public bool isDie;

    public int pointUydanh;

    public int pointNon;

    public int pointVukhi;

    public int pointAo;

    public int pointLien;

    public int pointGangtay;

    public int pointNhan;

    public int pointQuan;

    public int pointNgocboi;

    public int pointGiay;

    public int pointPhu;

    public int countFinishDay;

    public int countLoopBoos;

    public int limitTiemnangso;

    public int limitKynangso;

    public short[] potential = new short[4];

    public string cName = string.Empty;

    public int clanID;

    public sbyte ctypeClan;

    public Clan clan;

    public sbyte role;

    public int cw = 22;

    public int ch = 32;

    public int chw = 11;

    public int chh = 16;

    public Command cmdMenu;

    public bool canFly = true;

    public bool cmtoChar;

    public bool me;

    public bool cFinishedAttack;

    public bool cchistlast;

    public bool isAttack;

    public bool isAttFly;

    public int cwpt;

    public int cwplv;

    public int cf;

    public int tick;

    public static bool fallAttack;

    public bool isJump;

    public bool autoFall;

    public bool attack = true;

    public long xu;

    public int xuInBox;

    public int yen;

    public int gold_lock;

    public int luong;

    public int luongKhoa;

    public NClass nClass;

    public Command endMovePointCommand;

    public MyVector vSkill = new MyVector();

    public MyVector vSkillFight = new MyVector();

    public MyVector vEff = new MyVector();

    public Skill myskill;

    public Task taskMaint;

    public bool paintName = true;

    public Archivement[] arrArchive;

    public Item[] arrItemBag;

    public Item[] arrItemBox;

    public Item[] arrItemBody;

    public Skill[] arrPetSkill;

    public Item[][] arrItemShop;

    public string[][] infoSpeacialSkill;

    public short[][] imgSpeacialSkill;

    public short cResFire;

    public short cResIce;

    public short cResWind;

    public short cMiss;

    public short cExactly;

    public short cFatal;

    public sbyte cPk;

    public sbyte cTypePk;

    public short cReactDame;

    public short sysUp;

    public short sysDown;

    public int avatar;

    public int skillTemplateId;

    public Mob mobFocus;

    public Mob mobMe;

    public int tMobMeBorn;

    public Npc npcFocus;

    public Char charFocus;

    public ItemMap itemFocus;

    public MyVector focus = new MyVector();

    public Mob[] attMobs;

    public Char[] attChars;

    public short[] moveFast;

    public int testCharId = -9999;

    public int killCharId = -9999;

    public sbyte resultTest;

    public int countKill;

    public int countKillMax;

    public bool isInvisiblez;

    public bool isShadown = true;

    public const sbyte PK_NORMAL = 0;

    public const sbyte PK_PHE = 1;

    public const sbyte PK_BANG = 2;

    public const sbyte PK_THIDAU = 3;

    public const sbyte PK_LUYENTAP = 4;

    public const sbyte PK_TUDO = 5;

    public MyVector taskOrders = new MyVector();

    public int cStamina;

    public static short[] idHead;

    public static short[] idAvatar;

    public int exp;

    public string[] strLevel;

    public string currStrLevel;

    public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

    public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

    public bool isFreez;

    public bool isCharge;

    public int seconds;

    public int freezSeconds;

    public long last;

    public long cur;

    public long lastFreez;

    public long currFreez;

    public bool isFlyUp;

    public static MyVector vItemTime = new MyVector();

    public static short ID_NEW_MOUNT = 30000;

    public short idMount;

    public bool isHaveMount;

    public bool isMountVip;

    public bool isEventMount;

    public bool isSpeacialMount;

    public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

    public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

    public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

    public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

    public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

    public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

    public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

    public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

    public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

    public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

    public sbyte[] FrameMount = new sbyte[8] { 0, 0, 1, 1, 2, 2, 1, 1 };

    public int frameMount;

    public int frameNewMount;

    public int transMount;

    public int genderMount;

    public int idcharMount;

    public int xMount;

    public int yMount;

    public int dxMount;

    public int dyMount;

    public int xChar;

    public int xdis;

    public int speedMount;

    public bool isStartMount;

    public bool isMount;

    public bool isEndMount;

    public sbyte cFlag;

    public int flagImage;

    public short x_hint;

    public short y_hint;

    public short s_danhHieu1;

    public static int[][][] CharInfo = new int[33][][]
    {
        new int[4][]
        {
            new int[3] { 0, -13, 34 },
            new int[3] { 1, -8, 10 },
            new int[3] { 1, -9, 16 },
            new int[3] { 1, -9, 45 }
        },
        new int[4][]
        {
            new int[3] { 0, -13, 35 },
            new int[3] { 1, -8, 10 },
            new int[3] { 1, -9, 17 },
            new int[3] { 1, -9, 46 }
        },
        new int[4][]
        {
            new int[3] { 1, -10, 33 },
            new int[3] { 2, -10, 11 },
            new int[3] { 2, -8, 16 },
            new int[3] { 1, -12, 49 }
        },
        new int[4][]
        {
            new int[3] { 1, -10, 32 },
            new int[3] { 3, -12, 10 },
            new int[3] { 3, -11, 15 },
            new int[3] { 1, -13, 47 }
        },
        new int[4][]
        {
            new int[3] { 1, -10, 34 },
            new int[3] { 4, -8, 11 },
            new int[3] { 4, -7, 17 },
            new int[3] { 1, -12, 47 }
        },
        new int[4][]
        {
            new int[3] { 1, -10, 34 },
            new int[3] { 5, -12, 11 },
            new int[3] { 5, -9, 17 },
            new int[3] { 1, -13, 49 }
        },
        new int[4][]
        {
            new int[3] { 1, -10, 33 },
            new int[3] { 6, -10, 10 },
            new int[3] { 6, -8, 16 },
            new int[3] { 1, -12, 47 }
        },
        new int[4][]
        {
            new int[3] { 0, -9, 36 },
            new int[3] { 7, -5, 17 },
            new int[3] { 7, -11, 25 },
            new int[3] { 1, -8, 49 }
        },
        new int[4][]
        {
            new int[3] { 0, -7, 35 },
            new int[3] { 0, -18, 22 },
            new int[3] { 7, -10, 25 },
            new int[3] { 1, -7, 48 }
        },
        new int[4][]
        {
            new int[3] { 1, -11, 35 },
            new int[3] { 10, -3, 25 },
            new int[3] { 12, -10, 26 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -11, 37 },
            new int[3] { 11, -3, 25 },
            new int[3] { 12, -11, 27 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -14, 34 },
            new int[3] { 12, -8, 21 },
            new int[3] { 9, -7, 31 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -12, 35 },
            new int[3] { 8, -5, 14 },
            new int[3] { 8, -15, 29 },
            new int[3] { 1, -9, 49 }
        },
        new int[4][]
        {
            new int[3] { 1, -9, 34 },
            new int[3] { 9, -12, 9 },
            new int[3] { 10, -7, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -13, 34 },
            new int[3] { 9, -12, 9 },
            new int[3] { 11, -10, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -8, 32 },
            new int[3] { 9, -12, 9 },
            new int[3] { 2, -6, 15 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -8, 32 },
            new int[3] { 9, -12, 9 },
            new int[3] { 13, -12, 16 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -10, 31 },
            new int[3] { 9, -12, 9 },
            new int[3] { 7, -13, 20 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -11, 32 },
            new int[3] { 9, -12, 9 },
            new int[3] { 8, -15, 26 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -9, 33 },
            new int[3] { 9, -12, 9 },
            new int[3] { 14, -8, 18 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -11, 33 },
            new int[3] { 9, -12, 9 },
            new int[3] { 15, -6, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -16, 31 },
            new int[3] { 9, -12, 9 },
            new int[3] { 9, -8, 28 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -14, 34 },
            new int[3] { 1, -8, 10 },
            new int[3] { 8, -16, 28 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -8, 36 },
            new int[3] { 7, -5, 17 },
            new int[3] { 0, -5, 25 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -9, 31 },
            new int[3] { 9, -12, 9 },
            new int[3] { 0, -6, 20 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 2, -9, 36 },
            new int[3] { 13, -5, 17 },
            new int[3] { 16, -11, 25 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -9, 34 },
            new int[3] { 8, -5, 13 },
            new int[3] { 10, -7, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -13, 34 },
            new int[3] { 8, -5, 13 },
            new int[3] { 11, -10, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -8, 32 },
            new int[3] { 8, -5, 13 },
            new int[3] { 2, -6, 15 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 1, -8, 32 },
            new int[3] { 8, -5, 13 },
            new int[3] { 13, -12, 16 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -9, 33 },
            new int[3] { 8, -5, 13 },
            new int[3] { 14, -8, 18 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -11, 33 },
            new int[3] { 8, -5, 13 },
            new int[3] { 15, -6, 19 },
            new int[3]
        },
        new int[4][]
        {
            new int[3] { 0, -16, 32 },
            new int[3] { 8, -5, 13 },
            new int[3] { 9, -8, 29 },
            new int[3]
        }
    };

    public static int[] CHAR_WEAPONX = new int[11]
    {
        -2, -6, 22, 21, 19, 22, 10, -2, -2, 5,
        19
    };

    public static int[] CHAR_WEAPONY = new int[11]
    {
        9, 22, 25, 17, 26, 37, 36, 49, 50, 52,
        36
    };

    private static Char myChar;

    private static Char myPet;

    private static Char myPet2;

    public static int[] listAttack;

    public static int[][] listIonC;

    public int cvyJump;

    private int indexUseSkill = -1;

    public int cxSend;

    public int cySend;

    public int cdirSend = 1;

    public int cxFocus;

    public int cyFocus;

    public int cactFirst = 5;

    public MyVector vMovePoints = new MyVector();

    public static string[][] inforClass = new string[2][]
    {
        new string[4] { "1", "1", "chiêu 1", "0" },
        new string[4] { "2", "2", "chiêu 2", "5" }
    };

    public static int[][] inforSkill = new int[10][]
    {
        new int[12]
        {
            1, 0, 1, 1000, 40, 1, 0, 20, 0, 0,
            0, 0
        },
        new int[12]
        {
            2, 1, 10, 1000, 100, 1, 0, 40, 0, 0,
            0, 0
        },
        new int[12]
        {
            2, 2, 11, 800, 100, 1, 0, 45, 0, 0,
            0, 0
        },
        new int[12]
        {
            2, 3, 12, 600, 100, 1, 0, 50, 0, 0,
            0, 0
        },
        new int[12]
        {
            2, 4, 13, 500, 100, 1, 0, 55, 0, 0,
            0, 0
        },
        new int[12]
        {
            3, 1, 14, 500, 100, 1, 0, 60, 0, 0,
            0, 0
        },
        new int[12]
        {
            3, 2, 14, 500, 100, 1, 0, 60, 0, 0,
            0, 0
        },
        new int[12]
        {
            3, 3, 14, 500, 100, 1, 0, 60, 0, 0,
            0, 0
        },
        new int[12]
        {
            3, 4, 14, 500, 100, 1, 0, 60, 0, 0,
            0, 0
        },
        new int[12]
        {
            3, 5, 14, 500, 100, 1, 0, 60, 0, 0,
            0, 0
        }
    };

    public static bool flag;

    public static bool ischangingMap;

    public static bool isLockKey;

    public static bool isLoadingMap;

    public bool isLockMove;

    public bool isLockAttack;

    public string strInfo;

    public short powerPoint;

    public short maxPowerPoint;

    public short secondPower;

    public long lastS;

    public long currS;

    public bool havePet = false;

    public bool havePet2 = false;

    public MovePoint currentMovePoint;

    public int bom;

    public int delayFall;

    private bool isSoundJump;

    public int lastFrame;

    private Effect eProtect;

    private Effect eDanhHieu;

    private int twHp;

    public bool isInjureHp;

    public bool changePos;

    public bool isHide;

    private bool wy;

    public int wt;

    public int fy;

    public int ty;

    private int t;

    private int fM;

    public int[] move = new int[15]
    {
        1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
        3, 3, 2, 2, 2
    };

    private string strMount = "mount_";

    public int headICON = -1;

    public int head;

    public int leg;

    public int body;

    public int bag;

    public int wp;

    public int indexEff = -1;

    public int indexEffTask = -1;

    public EffectCharPaint eff;

    public EffectCharPaint effTask;

    public int indexSkill;

    public int i0;

    public int i1;

    public int i2;

    public int dx0;

    public int dx1;

    public int dx2;

    public int dy0;

    public int dy1;

    public int dy2;

    public EffectCharPaint eff0;

    public EffectCharPaint eff1;

    public EffectCharPaint eff2;

    public Arrow arr;

    public PlayerDart dart;

    public bool isCreateDark;

    public SkillPaint skillPaint;

    public SkillPaint skillPaintRandomPaint;

    public EffectPaint[] effPaints;

    public int sType;

    public sbyte isInjure;

    public bool isUseSkillAfterCharge;

    public bool isFlyAndCharge;

    public bool isStandAndCharge;

    private bool isFlying;

    public int posDisY;

    private int chargeCount;

    private bool hasSendAttack;

    public bool isMabuHold;

    private long timeBlue;

    private int tBlue;

    private bool IsAddDust1;

    private bool IsAddDust2;

    public int len = 24;

    public int w_hp_bar = 24;

    private int per = 100;

    private int per_tem = 100;

    private Image imgHPtem;

    public bool isPet;

    public bool isMiniPet;

    private int iiii;

    private int danhHieuFramme;

    public int xSd;

    public int ySd;

    private bool isOutMap;

    private int fBag;

    private Part ph;

    private Part pl;

    private Part pb;

    public int cH_new = 32;

    private int statusBeforeNothing;

    private int timeFocusToMob;

    public static bool isManualFocus = false;

    private Char charHold;

    private Mob mobHold;

    private int nInjure;

    public short wdx;

    public short wdy;

    public bool isDirtyPostion;

    public Skill lastNormalSkill;

    public bool currentFireByShortcut;

    public long cDamGoc;

    public long cHPGoc;

    public long cMPGoc;

    public int cDefGoc;

    public int cCriticalGoc;

    public sbyte hpFrom1000TiemNang;

    public sbyte mpFrom1000TiemNang;

    public sbyte damFrom1000TiemNang;

    public sbyte defFrom1000TiemNang = 1;

    public sbyte criticalFrom1000Tiemnang = 1;

    public short cMaxStamina;

    public short expForOneAdd;

    public sbyte isMonkey;

    public bool isCopy;

    public bool isWaitMonkey;

    private bool isFeetEff;

    public bool meDead;

    public int holdEffID;

    public bool holder;

    public bool protectEff;

    public bool danhHieuEff = true;

    private bool isSetPos;

    private int tpos;

    private short xPos;

    private short yPos;

    private sbyte typePos;

    private bool isMyFusion;

    public bool isFusion;

    public int tFusion;

    public bool huytSao;

    public bool blindEff;

    public bool telePortSkill;

    public bool sleepEff;

    public bool stone;

    public int perCentMp = 100;

    public long dHP;

    public int headTemp = -1;

    public int bodyTemp = -1;

    public int legTemp = -1;

    public int bagTemp = -1;

    public int wpTemp = -1;

    public MyVector vEffChar = new MyVector("vEff");

    public static FrameImage fraRedEye;

    private int fChopmat;

    private bool isAddChopMat;

    private long timeAddChopmat;

    private int[] frChopNhanh = new int[34]
    {
        -1, -1, -1, -1, 0, 0, 1, 1, 0, 0,
        1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
        0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
        -1, -1, -1, -1
    };

    private int[] frChopCham = new int[23]
    {
        -1, -1, -1, -1, 0, 0, 1, 1, 1, 0,
        0, 1, 1, 1, 0, 0, 1, 1, 1, -1,
        -1, -1, -1
    };

    private int[] frEye = new int[30]
    {
        -1, -1, 0, 0, 1, 1, 0, 0, 1, 1,
        0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
        1, 1, 0, 0, 1, 1, 0, 0, -1, -1
    };

    public static int[][] Arr_Head_2Fr = new int[1][] { new int[2] { 542, 543 } };

    private int fHead;

    private string strEffAura = "aura_";

    public short idAuraEff = -1;

    public static bool isPaintAura = true;

    public static bool isPaintAura2 = true;

    private FrameImage fraEff;

    private FrameImage fraEffSub;

    private string strEff_Set_Item = "set_eff_";

    public short idEff_Set_Item = -1;

    private FrameImage fraHat_behind;

    private FrameImage fraHat_font;

    private FrameImage fraHat_behind_2;

    private FrameImage fraHat_font_2;

    private string strHat_behind = "hat_sau_";

    private string strHat_font = "hat_truoc_";

    private string strNgang = "ngang_";

    public short idHat = -1;

    public static int[][] hatInfo = new int[32][]
    {
        new int[2] { 5, -7 },
        new int[2] { 5, -7 },
        new int[2] { 5, -8 },
        new int[2] { 5, -7 },
        new int[2] { 5, -6 },
        new int[2] { 5, -8 },
        new int[2] { 5, -7 },
        new int[2] { 9, 0 },
        new int[2] { 11, 1 },
        new int[2] { 4, 0 },
        new int[2] { 4, -1 },
        new int[2] { 4, 8 },
        new int[2] { 6, 5 },
        new int[2] { 6, -6 },
        new int[2] { 2, -5 },
        new int[2] { 7, -8 },
        new int[2] { 7, -6 },
        new int[2] { 8, 0 },
        new int[2] { 7, 5 },
        new int[2] { 9, -7 },
        new int[2] { 7, -3 },
        new int[2] { 2, 8 },
        new int[2] { 4, 5 },
        new int[2] { 10, -5 },
        new int[2] { 9, -5 },
        new int[2] { 9, -5 },
        new int[2] { 6, -6 },
        new int[2] { 2, -5 },
        new int[2] { 7, -8 },
        new int[2] { 7, -6 },
        new int[2] { 9, -7 },
        new int[2] { 7, -3 }
    };

    public const byte TYPE_SKILL_KAMEX10 = 1;

    public const byte TYPE_SKILL_FINAL = 2;

    public const byte TYPE_SKILL_MAFUBA = 3;

    public const byte TYPE_SKILL_GENKI = 4;

    public bool isPaintNewSkill;

    private bool isFly;

    private long timeReset_newSkill;

    private sbyte typeFrame;

    private short idskillPaint;

    private byte[] fr_start;

    private byte[] fr_atk;

    private byte[] fr_end;

    private int count_NEW;

    private int stt;

    private short rangeDame;

    private sbyte typePaint;

    private sbyte typeItem;

    private Point targetDame;

    private long timeDame;

    public bool isMafuba;

    private short countMafuba;

    public int xMFB;

    public int yMFB;

    public int timeGongSkill;

    private FrameImage fraDanhHieu;

    private MainImage mainImg;

    public bool isTichXanh;

    #region NewInfo

    public int tlDef;

    public int tlPst;

    public int tlNeDon;

    public int tlHutHp;

    public int tlHutMp;

    public int tileGiamTDHS;

    public int timeGiamTDHS;

    public bool khangTDHS;

    public bool isKhongLanh;

    public bool wearingVoHinh;

    public bool teleport;

    #endregion


    public Char()
    {
        statusMe = 6;
    }

    public void applyCharLevelPercent()
    {
        try
        {
            long num = 1L;
            long num2 = 0L;
            int num3 = 0;
            for (int num4 = GameScr.exps.Length - 1; num4 >= 0; num4--)
            {
                if (cPower >= GameScr.exps[num4])
                {
                    num = ((num4 != GameScr.exps.Length - 1) ? (GameScr.exps[num4 + 1] - GameScr.exps[num4]) : 1);
                    num2 = cPower - GameScr.exps[num4];
                    num3 = num4;
                    break;
                }
            }
            clevel = num3;
            cLevelPercent = num2 * 10000 / num;
            if (cLevelPercent > 10000)
            {
                cLevelPercent = 10000;
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi char level percent: " + ex.ToString());
        }
    }

    // getdx_dySkill extracted to Char.Skills.cs

    public static void taskAction(bool isNextStep)
    {
        Task task = myCharz().taskMaint;
        if (task.index > task.contentInfo.Length - 1)
        {
            task.index = task.contentInfo.Length - 1;
        }
        string text = task.contentInfo[task.index];
        if (text != null && !text.Equals(string.Empty))
        {
            if (text.StartsWith("#"))
            {
                text = NinjaUtil.Replace(text, "#", string.Empty);
                Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[myCharz().cgender][2]);
                npc.cx = (npc.cy = -100);
                npc.avatar = GameScr.info1.charId[myCharz().cgender][2];
                npc.charID = 5;
                if (GameCanvas.currentScreen == GameScr.instance)
                {
                    ChatPopup.addNextPopUpMultiLine(text, npc);
                }
            }
            else if (isNextStep)
            {
                GameScr.info1.addInfo(text, 0);
            }
        }
        GameScr.isHaveSelectSkill = true;
        Cout.println("TASKx " + myCharz().taskMaint.taskId);
        if (myCharz().taskMaint.taskId <= 2)
        {
            myCharz().canFly = false;
        }
        else
        {
            myCharz().canFly = true;
        }
        GameScr.gI().left = GameScr.gI().cmdMenu;
        GameScr.gI().right = GameScr.gI().cmdFocus;
        GameScr.isHaveSelectSkill = true;
        GameScr.isPaintRada = 1;
        MagicTree.isPaint = true;
        Hint.isViewMap = true;
        Hint.isViewPotential = true;
        if (task.taskId >= 0)
        {
            Panel.isPaintMap = true;
        }
        else
        {
            Panel.isPaintMap = false;
        }
        if (task.taskId < 12)
        {
            GameCanvas.panel.mainTabName = mResources.mainTab1;
        }
        else
        {
            GameCanvas.panel.mainTabName = mResources.mainTab2;
        }
        GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
        if (myChar.taskMaint.taskId > 10)
        {
            Rms.saveRMSString("fake", "aa");
        }
    }

    public string getStrLevel()
    {
        if (clevel >= strLevel.Length)
        {
            clevel = strLevel.Length - 1;
        }
        string text = strLevel[clevel] + "+" + cLevelPercent / 100 + "." + cLevelPercent % 100 + "%";
        if (text.Length > 23 && text.IndexOf("cấp ") >= 0)
        {
            text = Res.replace(text, "cấp ", "c");
        }
        return text;
    }

    public int avatarz()
    {
        return getAvatar(head);
    }

    public int getAvatar(int headId)
    {
        if (idHead == null || idAvatar == null)
        {
            return -1;
        }
        for (int i = 0; i < idHead.Length && i < idAvatar.Length; i++)
        {
            if (headId == idHead[i])
            {
                return idAvatar[i];
            }
        }
        return -1;
    }

    public void setPowerInfo(string info, short p, short maxP, short sc)
    {
        powerPoint = p;
        strInfo = info;
        maxPowerPoint = maxP;
        secondPower = sc;
        lastS = (currS = mSystem.currentTimeMillis());
    }

    public void addInfo(string info)
    {
        if (chatInfo == null)
        {
            chatInfo = new Info();
        }
        Char cInfo = null;
        chatInfo.addInfo(info, 0, cInfo, isChatServer: false);
    }

    public int getSys()
    {
        if (nClass.classId == 1 || nClass.classId == 2)
        {
            return 1;
        }
        if (nClass.classId == 3 || nClass.classId == 4)
        {
            return 2;
        }
        if (nClass.classId == 5 || nClass.classId == 6)
        {
            return 3;
        }
        return 0;
    }

    public static Char myCharz()
    {
        if (myChar == null)
        {
            myChar = new Char();
            myChar.me = true;
            myChar.cmtoChar = true;
        }
        return myChar;
    }

    public static Char myPetz()
    {
        if (myPet == null)
        {
            myPet = new Char();
            myPet.me = false;
        }
        return myPet;
    }

    public static Char MyPet2z()
    {
        myPet2 ??= new Char
            {
                me = false
            };
        return myPet2;
    }

    public static void clearMyChar()
    {
        myChar = null;
    }

    public void bagSort()
    {
        try
        {
            MyVector myVector = new MyVector();
            for (int i = 0; i < arrItemBag.Length; i++)
            {
                Item item = arrItemBag[i];
                if (item != null && item.template.isUpToUp && !item.isExpires)
                {
                    myVector.addElement(item);
                }
            }
            for (int j = 0; j < myVector.size(); j++)
            {
                Item item2 = (Item)myVector.elementAt(j);
                if (item2 == null)
                {
                    continue;
                }
                for (int k = j + 1; k < myVector.size(); k++)
                {
                    Item item3 = (Item)myVector.elementAt(k);
                    if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
                    {
                        item2.quantity += item3.quantity;
                        arrItemBag[item3.indexUI] = null;
                        myVector.setElementAt(null, k);
                    }
                }
            }
            for (int l = 0; l < arrItemBag.Length; l++)
            {
                if (arrItemBag[l] == null)
                {
                    continue;
                }
                for (int m = 0; m <= l; m++)
                {
                    if (arrItemBag[m] == null)
                    {
                        arrItemBag[m] = arrItemBag[l];
                        arrItemBag[m].indexUI = m;
                        arrItemBag[l] = null;
                        break;
                    }
                }
            }
        }
        catch (Exception)
        {
            Cout.println("Char.bagSort()");
        }
    }

    public void boxSort()
    {
        try
        {
            MyVector myVector = new MyVector();
            for (int i = 0; i < arrItemBox.Length; i++)
            {
                Item item = arrItemBox[i];
                if (item != null && item.template.isUpToUp && !item.isExpires)
                {
                    myVector.addElement(item);
                }
            }
            for (int j = 0; j < myVector.size(); j++)
            {
                Item item2 = (Item)myVector.elementAt(j);
                if (item2 == null)
                {
                    continue;
                }
                for (int k = j + 1; k < myVector.size(); k++)
                {
                    Item item3 = (Item)myVector.elementAt(k);
                    if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
                    {
                        item2.quantity += item3.quantity;
                        arrItemBox[item3.indexUI] = null;
                        myVector.setElementAt(null, k);
                    }
                }
            }
            for (int l = 0; l < arrItemBox.Length; l++)
            {
                if (arrItemBox[l] == null)
                {
                    continue;
                }
                for (int m = 0; m <= l; m++)
                {
                    if (arrItemBox[m] == null)
                    {
                        arrItemBox[m] = arrItemBox[l];
                        arrItemBox[m].indexUI = m;
                        arrItemBox[l] = null;
                        break;
                    }
                }
            }
        }
        catch (Exception)
        {
            Cout.println("Char.boxSort()");
        }
    }

    public void useItem(int indexUI)
    {
        Item item = arrItemBag[indexUI];
        if (!item.isTypeBody())
        {
            return;
        }
        item.isLock = true;
        item.typeUI = 5;
        Item item2 = arrItemBody[item.template.type];
        arrItemBag[indexUI] = null;
        if (item2 != null)
        {
            item2.typeUI = 3;
            arrItemBody[item.template.type] = null;
            item2.indexUI = indexUI;
            arrItemBag[indexUI] = item2;
        }
        item.indexUI = item.template.type;
        arrItemBody[item.indexUI] = item;
        for (int i = 0; i < arrItemBody.Length; i++)
        {
            Item item3 = arrItemBody[i];
            if (item3 != null)
            {
                if (item3.template.type == 0)
                {
                    body = item3.template.part;
                }
                else if (item3.template.type == 1)
                {
                    leg = item3.template.part;
                }
            }
        }
    }

    // getSkill_isPunchKick extracted to Char.Skills.cs

    public void soundUpdate()
    {
        if (me && statusMe == 10 && cf == 8 && ty > 20 && GameCanvas.gameTick % 20 == 0)
        {
            SoundMn.gI().charFly();
        }
        if (skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length && isPunchKickSkill() && (me || (!me && cx >= GameScr.cmx && cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
        {
            if (cf == 9 || cf == 10 || cf == 11)
            {
                SoundMn.gI().charPunch(isKick: true, (!me) ? 0.05f : 0.1f);
            }
            else
            {
                SoundMn.gI().charPunch(isKick: false, (!me) ? 0.05f : 0.1f);
            }
        }
    }

    public void updateChargeSkill()
    {
    }

    public virtual void update()
    {
        if (isMafuba)
        {
            cf = 23;
            countMafuba++;
            if (countMafuba > 150)
            {
                isMafuba = false;
            }
            return;
        }
        countMafuba = 0;
        if (isHide || isMabuHold)
        {
            return;
        }
        if ((!isCopy && clevel < 14) || statusMe == 1 || statusMe == 6)
        {
        }
        if (petFollow != null)
        {
            if (GameCanvas.gameTick % 3 == 0)
            {
                if (myCharz().cdir == 1)
                {
                    petFollow.cmtoX = cx - 20;
                }
                if (myCharz().cdir == -1)
                {
                    petFollow.cmtoX = cx + 20;
                }
                petFollow.cmtoY = cy - 40;
                if (petFollow.cmx > cx)
                {
                    petFollow.dir = -1;
                }
                else
                {
                    petFollow.dir = 1;
                }
                if (petFollow.cmtoX < 100)
                {
                    petFollow.cmtoX = 100;
                }
                if (petFollow.cmtoX > TileMap.pxw - 100)
                {
                    petFollow.cmtoX = TileMap.pxw - 100;
                }
            }
            petFollow.update();
        }
        if (!me && cHP <= 0 && clanID != -100 && statusMe != 14 && statusMe != 5)
        {
            startDie((short)cx, (short)cy);
        }
        if (isInjureHp)
        {
            twHp++;
            if (twHp == 20)
            {
                twHp = 0;
                isInjureHp = false;
            }
        }
        else if (dHP > cHP)
        {
            long num = dHP - cHP >> 1;
            if (num < 1)
            {
                num = 1;
            }
            dHP -= num;
        }
        else
        {
            dHP = cHP;
        }
        if (secondPower != 0)
        {
            currS = mSystem.currentTimeMillis();
            if (currS - lastS >= 1000)
            {
                lastS = mSystem.currentTimeMillis();
                secondPower--;
            }
        }
        if (isPaintNewSkill)
        {
            if (GameCanvas.timeNow > timeReset_newSkill || statusMe == 14 || statusMe == 5)
            {
                timeReset_newSkill = 0L;
                isPaintNewSkill = false;
            }
            UpdSkillPaint_NEW();
            if (isShadown)
            {
                updateShadown();
            }
        }
        else
        {
            if (!me && GameScr.notPaint)
            {
                return;
            }
            if (sleepEff && GameCanvas.gameTick % 10 == 0)
            {
                EffecMn.addEff(new Effect(41, cx, cy, 3, 1, 1));
            }
            if (huytSao)
            {
                huytSao = false;
                EffecMn.addEff(new Effect(39, cx, cy, 3, 3, 1));
            }
            if (blindEff && GameCanvas.gameTick % 5 == 0)
            {
                ServerEffect.addServerEffect(113, this, 1);
            }
            if (protectEff)
            {
                int y = cH_new + 73;
                if (GameCanvas.gameTick % 5 == 0)
                {
                    eProtect = new Effect(33, cx, y, 3, 3, 1);
                }
                if (eProtect != null)
                {
                    eProtect.update();
                    eProtect.x = cx;
                    eProtect.y = y;
                }
            }
            if (danhHieuEff)
            {
                if (eDanhHieu == null)
                {
                    string text = (string)GameCanvas.danhHieu.get(charID + string.Empty);
                    if (text != null)
                    {
                        string[] array = Res.split(text.Trim(), ",", 0);
                        short id = short.Parse(array[0]);
                        short num2 = short.Parse(array[1]);
                        eDanhHieu = new Effect(id, cx, cH_new + 73, 1, -1, -1);
                        eDanhHieu.timeExist = num2 * 1000 + mSystem.currentTimeMillis();
                    }
                }
                if (eDanhHieu != null)
                {
                    eDanhHieu.update();
                    eDanhHieu.x = cx;
                    eDanhHieu.y = cH_new;
                    if (eDanhHieu.timeExist <= mSystem.currentTimeMillis())
                    {
                        eDanhHieu = null;
                        GameCanvas.danhHieu.remove(charID + string.Empty);
                    }
                }
            }
            if (charFocus != null && charFocus.cy < 0)
            {
                charFocus = null;
            }
            if (isFusion)
            {
                tFusion++;
            }
            if (isNhapThe)
            {
                int num3 = 0;
                if (GameCanvas.gameTick % 25 == 0)
                {
                    num3 = 114;
                    ServerEffect.addServerEffect(num3, this, 1);
                }
            }
            if (isSetPos)
            {
                tpos++;
                if (tpos != 1)
                {
                    return;
                }
                tpos = 0;
                isSetPos = false;
                cx = xPos;
                cy = yPos;
                cp1 = (cp2 = (cp3 = 0));
                if (typePos == 1)
                {
                    if (me)
                    {
                        cxSend = cx;
                        cySend = cy;
                    }
                    currentMovePoint = null;
                    telePortSkill = false;
                    ServerEffect.addServerEffect(173, cx, cy, 1);
                }
                else
                {
                    ServerEffect.addServerEffect(60, cx, cy, 1);
                }
                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                {
                    statusMe = 1;
                }
                else
                {
                    statusMe = 4;
                }
                return;
            }
            soundUpdate();
            if (stone)
            {
                return;
            }
            if (isFreez)
            {
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(113, cx, cy, 1);
                }
                cf = 23;
                long num4 = mSystem.currentTimeMillis();
                if (num4 - lastFreez >= 1000)
                {
                    freezSeconds--;
                    lastFreez = num4;
                    if (freezSeconds < 0)
                    {
                        isFreez = false;
                        seconds = 0;
                        if (me)
                        {
                            myCharz().isLockMove = false;
                            GameScr.gI().dem = 0;
                            GameScr.gI().isFreez = false;
                        }
                    }
                }
                if (TileMap.tileTypeAt(cx / TileMap.size, cy / TileMap.size) == 0)
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
                return;
            }
            if (isWaitMonkey)
            {
                isLockMove = true;
                cf = 17;
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(154, cx, cy - 10, 2);
                }
                if (GameCanvas.gameTick % 5 == 0)
                {
                    ServerEffect.addServerEffect(1, cx, cy + 10, 1);
                }
                chargeCount++;
                if (chargeCount == 500)
                {
                    isWaitMonkey = false;
                    isLockMove = false;
                }
                return;
            }
            if (isStandAndCharge)
            {
                chargeCount++;
                bool flag = !TileMap.tileTypeAt(myCharz().cx, myCharz().cy, 2);
                updateEffect();
                updateSkillPaint();
                moveFast = null;
                currentMovePoint = null;
                cf = 17;
                if (flag && cgender != 2)
                {
                    cf = 12;
                }
                if (cgender == 2)
                {
                    if (GameCanvas.gameTick % 3 == 0)
                    {
                        ServerEffect.addServerEffect(154, cx, cy - ch / 2 + 10, 1);
                    }
                    if (GameCanvas.gameTick % 5 == 0)
                    {
                        ServerEffect.addServerEffect(114, cx + Res.random(-20, 20), cy + Res.random(-20, 20), 1);
                    }
                }
                if (cgender == 1)
                {
                    if (GameCanvas.gameTick % 4 == 0)
                    {
                    }
                    if (GameCanvas.gameTick % 2 == 0)
                    {
                        if (cdir == 1)
                        {
                            ServerEffect.addServerEffect(70, cx - 18, cy - ch / 2 + 8, 1);
                            ServerEffect.addServerEffect(70, cx + 23, cy - ch / 2 + 15, 1);
                        }
                        else
                        {
                            ServerEffect.addServerEffect(70, cx + 18, cy - ch / 2 + 8, 1);
                            ServerEffect.addServerEffect(70, cx - 23, cy - ch / 2 + 15, 1);
                        }
                    }
                }
                cur = mSystem.currentTimeMillis();
                if (cur - last > seconds || cur - last > 10000)
                {
                    stopUseChargeSkill();
                    if (me)
                    {
                        GameScr.gI().auto = 0;
                        if (cgender == 2)
                        {
                            myCharz().setAutoSkillPaint(GameScr.sks[myCharz().myskill.skillId], flag ? 1 : 0);
                            Service.gI().skill_not_focus(8);
                        }
                        if (cgender == 1)
                        {
                            isCreateDark = true;
                            myCharz().setSkillPaint(GameScr.sks[myCharz().myskill.skillId], flag ? 1 : 0);
                        }
                    }
                    else if (cgender == 2)
                    {
                        setAutoSkillPaint(GameScr.sks[skillTemplateId], flag ? 1 : 0);
                    }
                    if (cgender == 2 && statusMe != 14 && statusMe != 5)
                    {
                        GameScr.gI().activeSuperPower(cx, cy);
                    }
                }
                chargeCount++;
                if (chargeCount == 500)
                {
                    stopUseChargeSkill();
                }
                return;
            }
            if (isFlyAndCharge)
            {
                updateEffect();
                updateSkillPaint();
                moveFast = null;
                currentMovePoint = null;
                posDisY++;
                if (TileMap.tileTypeAt(cx, cy - ch, 8192))
                {
                    stopUseChargeSkill();
                    return;
                }
                if (posDisY == 20)
                {
                    last = mSystem.currentTimeMillis();
                }
                if (posDisY > 20)
                {
                    cur = mSystem.currentTimeMillis();
                    if (cur - last > seconds || cur - last > 10000)
                    {
                        isFlyAndCharge = false;
                        if (me)
                        {
                            isCreateDark = true;
                            bool flag2 = TileMap.tileTypeAt(myCharz().cx, myCharz().cy, 2);
                            isUseSkillAfterCharge = true;
                            myCharz().setSkillPaint(GameScr.sks[myCharz().myskill.skillId], (!flag2) ? 1 : 0);
                        }
                        return;
                    }
                    cf = 32;
                    if (cgender == 0 && GameCanvas.gameTick % 3 == 0)
                    {
                        ServerEffect.addServerEffect(153, cx, cy - ch, 2);
                    }
                    chargeCount++;
                    if (chargeCount == 500)
                    {
                        stopUseChargeSkill();
                    }
                }
                else
                {
                    if (statusMe != 14)
                    {
                        statusMe = 3;
                    }
                    cvy = -3;
                    cy += cvy;
                    cf = 7;
                }
                return;
            }
            if (me && GameCanvas.isTouch)
            {
                if (charFocus != null && charFocus.charID >= 0 && charFocus.cx > 100 && charFocus.cx < TileMap.pxw - 100 && isInEnterOnlinePoint() == null && isInEnterOfflinePoint() == null && !isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
                {
                    int num5 = Math.abs(cx - charFocus.cx);
                    int num6 = Math.abs(cy - charFocus.cy);
                    if (num5 < 60 && num6 < 40)
                    {
                        if (cmdMenu == null)
                        {
                            cmdMenu = new Command(mResources.MENU, 11111);
                            cmdMenu.isPlaySoundButton = false;
                        }
                        cmdMenu.x = charFocus.cx - GameScr.cmx;
                        cmdMenu.y = charFocus.cy - charFocus.ch - 30 - GameScr.cmy;
                    }
                    else
                    {
                        cmdMenu = null;
                    }
                }
                else
                {
                    cmdMenu = null;
                }
            }
            if (isShadown)
            {
                updateShadown();
            }
            if (isTeleport)
            {
                return;
            }
            if (chatInfo != null)
            {
                chatInfo.update();
            }
            if (shadowLife > 0)
            {
                shadowLife--;
            }
            if (resultTest > 0 && GameCanvas.gameTick % 2 == 0)
            {
                resultTest--;
                if (resultTest == 30 || resultTest == 60)
                {
                    resultTest = 0;
                }
            }
            updateSkillPaint();
            if (mobMe != null)
            {
                updateMobMe();
            }
            if (arr != null)
            {
                arr.update();
            }
            if (dart != null)
            {
                dart.update();
            }
            updateEffect();
            if (holdEffID != 0)
            {
                if (GameCanvas.gameTick % 5 == 0)
                {
                    EffecMn.addEff(new Effect(32, cx, cy + 24, 3, 5, 1));
                }
            }
            else
            {
                if (blindEff || sleepEff)
                {
                    return;
                }
                if (holder)
                {
                    if (charHold != null && (charHold.statusMe == 14 || charHold.statusMe == 5))
                    {
                        removeHoleEff();
                    }
                    if (mobHold != null && mobHold.status == 1)
                    {
                        removeHoleEff();
                    }
                    if (me && statusMe == 2 && currentMovePoint != null)
                    {
                        holder = false;
                        charHold = null;
                        mobHold = null;
                    }
                    if (TileMap.tileTypeAt(cx, cy, 2))
                    {
                        cf = 16;
                    }
                    else
                    {
                        cf = 31;
                    }
                    return;
                }
                if (cHP > 0)
                {
                    for (int i = 0; i < vEff.size(); i++)
                    {
                        EffectChar effectChar = (EffectChar)vEff.elementAt(i);
                        if (effectChar.template.type == 0 || effectChar.template.type == 12)
                        {
                            if (GameCanvas.isEff1)
                            {
                                cHP += effectChar.param;
                                cMP += effectChar.param;
                            }
                        }
                        else if (effectChar.template.type == 4 || effectChar.template.type == 17)
                        {
                            if (GameCanvas.isEff1)
                            {
                                cHP += effectChar.param;
                            }
                        }
                        else if (effectChar.template.type == 13 && GameCanvas.isEff1)
                        {
                            cHP -= cHPFull * 3 / 100;
                            if (cHP < 1)
                            {
                                cHP = 1;
                            }
                        }
                    }
                    if (eff5BuffHp > 0 && GameCanvas.isEff2)
                    {
                        cHP += eff5BuffHp;
                    }
                    if (eff5BuffMp > 0 && GameCanvas.isEff2)
                    {
                        cMP += eff5BuffMp;
                    }
                    if (cHP > cHPFull)
                    {
                        cHP = cHPFull;
                    }
                    if (cMP > cMPFull)
                    {
                        cMP = cMPFull;
                    }
                }
                if (cmtoChar)
                {
                    GameScr.cmtoX = cx - GameScr.gW2;
                    GameScr.cmtoY = cy - GameScr.gH23;
                    if (!GameCanvas.isTouchControl)
                    {
                        GameScr.cmtoX += GameScr.gW6 * cdir;
                    }
                }
                tick = (tick + 1) % 100;
                if (me)
                {
                    if (charFocus != null && !GameScr.vCharInMap.contains(charFocus))
                    {
                        charFocus = null;
                    }
                    if (cx < 10)
                    {
                        cvx = 0;
                        cx = 10;
                    }
                    else if (cx > TileMap.pxw - 10)
                    {
                        cx = TileMap.pxw - 10;
                        cvx = 0;
                    }
                    if (me && !ischangingMap && isInWaypoint())
                    {
                        Service.gI().charMove();
                        if (TileMap.isTrainingMap())
                        {
                            Service.gI().getMapOffline();
                            ischangingMap = true;
                        }
                        else
                        {
                            Service.gI().requestChangeMap();
                        }
                        isLockKey = true;
                        ischangingMap = true;
                        GameCanvas.clearKeyHold();
                        GameCanvas.clearKeyPressed();
                        InfoDlg.showWait();
                        return;
                    }
                    if (statusMe != 4 && Res.abs(cx - cxSend) + Res.abs(cy - cySend) >= 70 && cy - cySend <= 0 && me)
                    {
                        Service.gI().charMove();
                    }
                    if (isLockMove)
                    {
                        currentMovePoint = null;
                    }
                    if (currentMovePoint != null)
                    {
                        if (abs(cx - currentMovePoint.xEnd) <= 16 && abs(cy - currentMovePoint.yEnd) <= 16)
                        {
                            cx = (currentMovePoint.xEnd + cx) / 2;
                            cy = currentMovePoint.yEnd;
                            currentMovePoint = null;
                            GameScr.instance.clickMoving = false;
                            checkPerformEndMovePointAction();
                            cvx = (cvy = 0);
                            if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                            {
                                statusMe = 1;
                            }
                            else
                            {
                                setCharFallFromJump();
                            }
                            Service.gI().charMove();
                        }
                        else
                        {
                            cdir = ((currentMovePoint.xEnd > cx) ? 1 : (-1));
                            if (TileMap.tileTypeAt(cx, cy, 2))
                            {
                                statusMe = 2;
                                if (currentMovePoint != null)
                                {
                                    cvx = cspeed * cdir;
                                    cvy = 0;
                                }
                                if (abs(cx - currentMovePoint.xEnd) <= 10)
                                {
                                    if (currentMovePoint.yEnd > cy)
                                    {
                                        bool flag3 = false;
                                        sbyte b = 1;
                                        b = (sbyte)((cdir == 1) ? 1 : (-1));
                                        for (int j = 0; j < 2; j++)
                                        {
                                            if (TileMap.tileTypeAt(currentMovePoint.xEnd + chw * b, cy + chh * j, 2))
                                            {
                                                flag3 = true;
                                                break;
                                            }
                                        }
                                        if (flag3)
                                        {
                                            currentMovePoint = null;
                                            GameScr.instance.clickMoving = false;
                                            statusMe = 1;
                                            cvx = (cvy = 0);
                                            checkPerformEndMovePointAction();
                                        }
                                        else
                                        {
                                            SoundMn.gI().charJump();
                                            cx = currentMovePoint.xEnd;
                                            statusMe = 10;
                                            cvy = -5;
                                            cvx = 0;
                                        }
                                    }
                                    else
                                    {
                                        SoundMn.gI().charJump();
                                        cx = currentMovePoint.xEnd;
                                        statusMe = 10;
                                        cvy = -5;
                                        cvx = 0;
                                    }
                                }
                                if (cdir == 1)
                                {
                                    if (TileMap.tileTypeAt(cx + chw, cy - chh, 4))
                                    {
                                        cvx = cspeed * cdir;
                                        statusMe = 10;
                                        cvy = -5;
                                    }
                                }
                                else if (TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8))
                                {
                                    cvx = cspeed * cdir;
                                    statusMe = 10;
                                    cvy = -5;
                                }
                            }
                            else
                            {
                                if (currentMovePoint.yEnd < cy + 10)
                                {
                                    statusMe = 10;
                                    cvy = -5;
                                    if (abs(cy - currentMovePoint.yEnd) <= 10)
                                    {
                                        cy = currentMovePoint.yEnd;
                                        cvy = 0;
                                    }
                                    if (abs(cx - currentMovePoint.xEnd) <= 10)
                                    {
                                        cvx = 0;
                                    }
                                    else
                                    {
                                        cvx = cspeed * cdir;
                                    }
                                }
                                else if (TileMap.tileTypeAt(cx, cy, 2))
                                {
                                    currentMovePoint = null;
                                    GameScr.instance.clickMoving = false;
                                    statusMe = 1;
                                    cvx = (cvy = 0);
                                    checkPerformEndMovePointAction();
                                }
                                else
                                {
                                    if (statusMe == 10 || statusMe == 2)
                                    {
                                        cvy = 0;
                                    }
                                    statusMe = 4;
                                }
                                if (currentMovePoint.yEnd > cy)
                                {
                                    if (cdir == 1)
                                    {
                                        if (TileMap.tileTypeAt(cx + chw, cy - chh, 4))
                                        {
                                            cvx = (cvy = 0);
                                            statusMe = 4;
                                            currentMovePoint = null;
                                            GameScr.instance.clickMoving = false;
                                            checkPerformEndMovePointAction();
                                        }
                                    }
                                    else if (TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8))
                                    {
                                        cvx = (cvy = 0);
                                        statusMe = 4;
                                        currentMovePoint = null;
                                        GameScr.instance.clickMoving = false;
                                        checkPerformEndMovePointAction();
                                    }
                                }
                            }
                        }
                    }
                    searchFocus();
                }
                else
                {
                    checkHideCharName();
                    if (statusMe == 1 || statusMe == 6)
                    {
                        bool flag4 = false;
                        if (currentMovePoint != null)
                        {
                            if (abs(currentMovePoint.xEnd - cx) < 17 && abs(currentMovePoint.yEnd - cy) < 25)
                            {
                                cx = currentMovePoint.xEnd;
                                cy = currentMovePoint.yEnd;
                                currentMovePoint = null;
                                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                                {
                                    statusMe = 1;
                                    cp3 = 0;
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                }
                                else
                                {
                                    statusMe = 4;
                                    cvy = 0;
                                    cp1 = 0;
                                }
                                flag4 = true;
                            }
                            else if ((statusBeforeNothing == 10 || cf == 8) && vMovePoints.size() > 0)
                            {
                                flag4 = true;
                            }
                            else if (cy == currentMovePoint.yEnd)
                            {
                                if (cx != currentMovePoint.xEnd)
                                {
                                    cx = (cx + currentMovePoint.xEnd) / 2;
                                    cf = GameCanvas.gameTick % 5 + 2;
                                }
                            }
                            else if (cy < currentMovePoint.yEnd)
                            {
                                cf = 12;
                                cx = (cx + currentMovePoint.xEnd) / 2;
                                if (cvy < 0)
                                {
                                    cvy = 0;
                                }
                                cy += cvy;
                                if ((TileMap.tileTypeAtPixel(cx, cy) & 2) == 2)
                                {
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                }
                                cvy++;
                                if (cvy > 16)
                                {
                                    cy = (cy + currentMovePoint.yEnd) / 2;
                                }
                            }
                            else
                            {
                                cf = 7;
                                cx = (cx + currentMovePoint.xEnd) / 2;
                                cy = (cy + currentMovePoint.yEnd) / 2;
                            }
                        }
                        else
                        {
                            flag4 = true;
                        }
                        if (flag4 && vMovePoints.size() > 0)
                        {
                            currentMovePoint = (MovePoint)vMovePoints.firstElement();
                            vMovePoints.removeElementAt(0);
                            if (currentMovePoint.status == 2)
                            {
                                if ((TileMap.tileTypeAtPixel(cx, cy + 12) & 2) != 2)
                                {
                                    statusMe = 10;
                                    cp1 = 0;
                                    cp2 = 0;
                                    cvx = -(cx - currentMovePoint.xEnd) / 10;
                                    cvy = -(cy - currentMovePoint.yEnd) / 10;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                }
                                else
                                {
                                    statusMe = 2;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                    cvx = cspeed * cdir;
                                    cvy = 0;
                                }
                            }
                            else if (currentMovePoint.status == 3)
                            {
                                if ((TileMap.tileTypeAtPixel(cx, cy + 23) & 2) != 2)
                                {
                                    statusMe = 10;
                                    cp1 = 0;
                                    cp2 = 0;
                                    cvx = -(cx - currentMovePoint.xEnd) / 10;
                                    cvy = -(cy - currentMovePoint.yEnd) / 10;
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                }
                                else
                                {
                                    statusMe = 3;
                                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                                    GameCanvas.gI().startDust(1, cx - 8, cy);
                                    if (cx - currentMovePoint.xEnd > 0)
                                    {
                                        cdir = -1;
                                    }
                                    else if (cx - currentMovePoint.xEnd < 0)
                                    {
                                        cdir = 1;
                                    }
                                    cvx = abs(cx - currentMovePoint.xEnd) / 10 * cdir;
                                    cvy = -10;
                                }
                            }
                            else if (currentMovePoint.status == 4)
                            {
                                statusMe = 4;
                                if (cx - currentMovePoint.xEnd > 0)
                                {
                                    cdir = -1;
                                }
                                else if (cx - currentMovePoint.xEnd < 0)
                                {
                                    cdir = 1;
                                }
                                cvx = abs(cx - currentMovePoint.xEnd) / 9 * cdir;
                                cvy = 0;
                            }
                            else
                            {
                                cx = currentMovePoint.xEnd;
                                cy = currentMovePoint.yEnd;
                                currentMovePoint = null;
                            }
                        }
                    }
                }
                switch (statusMe)
                {
                    case 1:
                        updateCharStand();
                        break;
                    case 2:
                        updateCharRun();
                        break;
                    case 3:
                        updateCharJump();
                        break;
                    case 4:
                        updateCharFall();
                        break;
                    case 5:
                        updateCharDeadFly();
                        break;
                    case 16:
                        updateResetPoint();
                        break;
                    case 9:
                        updateCharAutoJump();
                        break;
                    case 10:
                        updateCharFly();
                        break;
                    case 12:
                        updateSkillStand();
                        break;
                    case 13:
                        updateSkillFall();
                        break;
                    case 14:
                        cp1++;
                        if (cp1 > 30)
                        {
                            cp1 = 0;
                        }
                        if (cp1 % 15 < 5)
                        {
                            cf = 0;
                        }
                        else
                        {
                            cf = 1;
                        }
                        break;
                    case 6:
                        if (isInjure <= 0)
                        {
                            cf = 0;
                        }
                        else if (statusBeforeNothing == 10)
                        {
                            cx += cvx;
                        }
                        else if (cf <= 1)
                        {
                            cp1++;
                            if (cp1 > 6)
                            {
                                cf = 0;
                            }
                            else
                            {
                                cf = 1;
                            }
                            if (cp1 > 10)
                            {
                                cp1 = 0;
                            }
                        }
                        if (cf != 7 && cf != 12 && (TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2)
                        {
                            cvx = 0;
                            cvy = 0;
                            statusMe = 4;
                            cf = 7;
                        }
                        if (me)
                        {
                            break;
                        }
                        cp3++;
                        if (cp3 > 10)
                        {
                            if ((TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2)
                            {
                                cy += 5;
                            }
                            else
                            {
                                cf = 0;
                            }
                        }
                        if (cp3 > 50)
                        {
                            cp3 = 0;
                            currentMovePoint = null;
                        }
                        break;
                }
                if (isInjure > 0)
                {
                    cf = 23;
                    isInjure--;
                }
                if (wdx != 0 || wdy != 0)
                {
                    startDie(wdx, wdy);
                    wdx = 0;
                    wdy = 0;
                }
                if (moveFast != null)
                {
                    if (moveFast[0] == 0)
                    {
                        moveFast[0]++;
                        ServerEffect.addServerEffect(60, this, 1);
                    }
                    else if (moveFast[0] < 10)
                    {
                        moveFast[0]++;
                    }
                    else
                    {
                        cx = moveFast[1];
                        cy = moveFast[2];
                        moveFast = null;
                        ServerEffect.addServerEffect(60, this, 1);
                        if (me)
                        {
                            if ((TileMap.tileTypeAtPixel(cx, cy) & 2) != 2)
                            {
                                statusMe = 4;
                                //myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
                            }
                            else
                            {
                                Service.gI().charMove();
                                //myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
                            }
                        }
                    }
                }
                if (statusMe != 10)
                {
                    fy = 0;
                }
                if (isCharge)
                {
                    cf = 17;
                    if (GameCanvas.gameTick % 4 == 0)
                    {
                        ServerEffect.addServerEffect(1, cx, cy + GameCanvas.transY, 1);
                    }
                    if (me)
                    {
                        long num7 = mSystem.currentTimeMillis();
                        if (num7 - last >= 1000)
                        {
                            last = num7;
                            cHP += cHPFull * myskill.damage / 100;
                            cMP += cMPFull * myskill.damage / 100;
                            if (cHP < cHPFull)
                            {
                                GameScr.startFlyText("+" + cHPFull * myskill.damage / 100 + " " + mResources.HP, cx, cy - ch - 20, 0, -1, mFont.HP);
                            }
                            if (cMP < cMPFull)
                            {
                                GameScr.startFlyText("+" + cMPFull * myskill.damage / 100 + " " + mResources.KI, cx, cy - ch - 20, 0, -2, mFont.MP);
                            }
                            Service.gI().skill_not_focus(2);
                        }
                    }
                }
                if (isFlyUp)
                {
                    if (me)
                    {
                        isLockKey = true;
                        statusMe = 3;
                        cvy = -8;
                        if (cy <= TileMap.pxh - 240)
                        {
                            isFlyUp = false;
                            isLockKey = false;
                            statusMe = 4;
                        }
                    }
                    else
                    {
                        statusMe = 3;
                        cvy = -8;
                        if (cy <= TileMap.pxh - 240)
                        {
                            cvy = 0;
                            isFlyUp = false;
                            cvy = 0;
                            statusMe = 1;
                        }
                    }
                }
                updateMount();
                updEffChar();
                updateEye();
                updateFHead();
            }
        }
    }

    private void updateEffect()
    {
        if (effPaints != null)
        {
            for (int i = 0; i < effPaints.Length; i++)
            {
                if (effPaints[i] == null)
                {
                    continue;
                }
                if (effPaints[i].eMob != null)
                {
                    if (!effPaints[i].isFly)
                    {
                        effPaints[i].eMob.setInjure();
                        effPaints[i].eMob.injureBy = this;
                        if (me)
                        {
                            effPaints[i].eMob.hpInjure = myCharz().cDamFull / 2 - myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
                        }
                        int num = effPaints[i].eMob.h >> 1;
                        if (effPaints[i].eMob.isBigBoss())
                        {
                            num = effPaints[i].eMob.getY() + 20;
                        }
                        GameScr.startSplash(effPaints[i].eMob.x, effPaints[i].eMob.y - num, cdir);
                        effPaints[i].isFly = true;
                    }
                }
                else if (effPaints[i].eChar != null && !effPaints[i].isFly)
                {
                    if (effPaints[i].eChar.charID >= 0)
                    {
                        effPaints[i].eChar.doInjure();
                    }
                    GameScr.startSplash(effPaints[i].eChar.cx, effPaints[i].eChar.cy - (effPaints[i].eChar.ch >> 1), cdir);
                    effPaints[i].isFly = true;
                }
                effPaints[i].index++;
                if (effPaints[i].index >= effPaints[i].effCharPaint.arrEfInfo.Length)
                {
                    effPaints[i] = null;
                }
            }
        }
        if (indexEff >= 0 && eff != null && GameCanvas.gameTick % 2 == 0)
        {
            indexEff++;
            if (indexEff >= eff.arrEfInfo.Length)
            {
                indexEff = -1;
                eff = null;
            }
        }
        if (indexEffTask >= 0 && effTask != null && GameCanvas.gameTick % 2 == 0)
        {
            indexEffTask++;
            if (indexEffTask >= effTask.arrEfInfo.Length)
            {
                indexEffTask = -1;
                effTask = null;
            }
        }
    }

    // checkPerformEndMovePointAction extracted to Char.Navigation.cs

    private void checkHideCharName()
    {
        if (GameCanvas.gameTick % 20 != 0 || charID < 0)
        {
            return;
        }
        paintName = true;
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = null;
            try
            {
                @char = (Char)GameScr.vCharInMap.elementAt(i);
            }
            catch (Exception)
            {
            }
            if (@char != null && !@char.Equals(this) && ((@char.cy == cy && Res.abs(@char.cx - cx) < 35) || (cy - @char.cy < 32 && cy - @char.cy > 0 && Res.abs(@char.cx - cx) < 24)))
            {
                paintName = false;
            }
        }
        for (int j = 0; j < GameScr.vNpc.size(); j++)
        {
            Npc npc = null;
            try
            {
                npc = (Npc)GameScr.vNpc.elementAt(j);
            }
            catch (Exception)
            {
            }
            if (npc != null && npc.cy == cy && Res.abs(npc.cx - cx) < 24)
            {
                paintName = false;
            }
        }
    }

    private void updateMobMe()
    {
        if (tMobMeBorn != 0)
        {
            tMobMeBorn--;
        }
        if (tMobMeBorn == 0)
        {
            mobMe.xFirst = ((cdir != 1) ? (cx + 30) : (cx - 30));
            mobMe.yFirst = cy - 60;
            int num = mobMe.xFirst - mobMe.x;
            int num2 = mobMe.yFirst - mobMe.y;
            mobMe.x += num / 4;
            mobMe.y += num2 / 4;
            mobMe.dir = cdir;
        }
    }

    // updateSkillPaint extracted to Char.Skills.cs

    // resetPoints_autoJump extracted to Char.Navigation.cs


    public void updateSuperEff()
    {
        if (GameCanvas.panel.isShow || isCopy || isFusion || isSetPos || isPet || isMiniPet || isMonkey == 1)
        {
            return;
        }
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        ty++;
        if (clevel >= 14)
        {
            return;
        }
        if (clevel >= 9 && !GameCanvas.lowGraphic && (ty == 40 || ty == 50))
        {
            GameCanvas.gI().startDust(-1, cx - -8, cy);
            GameCanvas.gI().startDust(1, cx - 8, cy);
            addDustEff(1);
        }
        if (ty <= 50 || clevel < 9)
        {
            return;
        }
        int num = 0;
        if (cgender == 0)
        {
            if (GameCanvas.gameTick % 25 == 0)
            {
                num = 114;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 4 == 0)
            {
                num = 132;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
        if (cgender == 1)
        {
            if (GameCanvas.gameTick % 4 == 0)
            {
                num = 132;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 7 == 0)
            {
                num = 131;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
        if (cgender == 2)
        {
            if (GameCanvas.gameTick % 7 == 0)
            {
                num = 131;
                ServerEffect.addServerEffect(num, this, 1);
            }
            if (clevel >= 13 && GameCanvas.gameTick % 25 == 0)
            {
                num = 114;
                ServerEffect.addServerEffect(num, this, 1);
            }
        }
    }

    public float getSoundVolumn()
    {
        if (me)
        {
            return 0.1f;
        }
        int num = Res.abs(myChar.cx - cx);
        if (num >= 0 && num <= 50)
        {
            return 0.1f;
        }
        return 0.05f;
    }


    public void setDefaultPart()
    {
        setDefaultWeapon();
        setDefaultBody();
        setDefaultLeg();
    }

    public void setDefaultWeapon()
    {
        if (cgender == 0)
        {
            wp = 0;
        }
    }

    public void setDefaultBody()
    {
        if (cgender == 0)
        {
            body = 57;
        }
        else if (cgender == 1)
        {
            body = 59;
        }
        else if (cgender == 2)
        {
            body = 57;
        }
    }

    public void setDefaultLeg()
    {
        if (cgender == 0)
        {
            leg = 58;
        }
        else if (cgender == 1)
        {
            leg = 60;
        }
        else if (cgender == 2)
        {
            leg = 58;
        }
    }

    // skillSelection_ChargeSkills extracted to Char.Skills.cs

    public void setAttack()
    {
        if (me)
        {
            SkillPaint skillPaint = skillPaintRandomPaint;
            if (dart != null)
            {
                skillPaint = dart.skillPaint;
            }
            if (skillPaint == null)
            {
                return;
            }
            MyVector myVector = new MyVector();
            MyVector myVector2 = new MyVector();
            if (charFocus != null)
            {
                myVector2.addElement(charFocus);
            }
            else if (mobFocus != null)
            {
                myVector.addElement(mobFocus);
            }
            effPaints = new EffectPaint[myVector.size() + myVector2.size()];
            for (int i = 0; i < myVector.size(); i++)
            {
                effPaints[i] = new EffectPaint();
                effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
                if (!isSelectingSkillUseAlone())
                {
                    effPaints[i].eMob = (Mob)myVector.elementAt(i);
                }
            }
            for (int j = 0; j < myVector2.size(); j++)
            {
                effPaints[j + myVector.size()] = new EffectPaint();
                effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
                effPaints[j + myVector.size()].eChar = (Char)myVector2.elementAt(j);
            }
            int type = 0;
            if (mobFocus != null)
            {
                type = 1;
            }
            else if (charFocus != null)
            {
                type = 2;
            }
            if (myVector.size() == 0 && myVector2.size() == 0)
            {
                stopUseChargeSkill();
            }
            if (me && !isSelectingSkillUseAlone() && !hasSendAttack)
            {
                Service.gI().sendPlayerAttack(myVector, myVector2, type);
                hasSendAttack = true;
            }
            return;
        }
        SkillPaint skillPaint2 = skillPaintRandomPaint;
        if (dart != null)
        {
            skillPaint2 = dart.skillPaint;
        }
        if (skillPaint2 == null)
        {
            return;
        }
        if (attMobs != null)
        {
            effPaints = new EffectPaint[attMobs.Length];
            for (int k = 0; k < attMobs.Length; k++)
            {
                effPaints[k] = new EffectPaint();
                effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
                effPaints[k].eMob = attMobs[k];
            }
            attMobs = null;
        }
        else if (attChars != null)
        {
            effPaints = new EffectPaint[attChars.Length];
            for (int l = 0; l < attChars.Length; l++)
            {
                effPaints[l] = new EffectPaint();
                effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
                effPaints[l].eChar = attChars[l];
            }
            attChars = null;
        }
    }

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

    public void setMabuHold(bool m)
    {
        isMabuHold = m;
    }

    public virtual void paint(mGraphics g)
    {
        if (isHide)
        {
            return;
        }
        if (isMafuba)
        {
            paintCharWithoutSkill(g);
        }
        else if (isMabuHold)
        {
            if (cmtoChar)
            {
                GameScr.cmtoX = cx - GameScr.gW2;
                GameScr.cmtoY = cy - GameScr.gH23;
                if (!GameCanvas.isTouchControl)
                {
                    GameScr.cmtoX += GameScr.gW6 * cdir;
                }
            }
        }
        else
        {
            if (!isPaint() || (!me && GameScr.notPaint))
            {
                return;
            }
            if (petFollow != null)
            {
                petFollow.paint(g);
            }
            paintMount1(g);
            if ((TileMap.isInAirMap() && cy >= TileMap.pxh - 48) || isTeleport)
            {
                return;
            }
            if (holder && GameCanvas.gameTick % 2 == 0)
            {
                g.setColor(16185600);
                if (charHold != null)
                {
                    g.drawLine(cx, cy - ch / 2, charHold.cx, charHold.cy - charHold.ch / 2);
                }
                if (mobHold != null)
                {
                    g.drawLine(cx, cy - ch / 2, mobHold.x, mobHold.y - mobHold.h / 2);
                }
            }
            paintSuperEffBehind(g);
            paintAuraBehind(g);
            paintEffBehind(g);
            paintEff_Lvup_behind(g);
            paintEff_Pet(g);
            if (shadowLife > 0)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    paintCharBody(g, shadowX, shadowY, cdir, 25, isPaintBag: true);
                }
                else if (shadowLife > 5)
                {
                    paintCharBody(g, shadowX, shadowY, cdir, 7, isPaintBag: true);
                }
            }
            if (!isPaint() && skillPaint != null && (skillPaint.id < 70 || skillPaint.id > 76) && (skillPaint.id < 77 || skillPaint.id > 83))
            {
                if (skillPaint != null)
                {
                    indexSkill = skillInfoPaint().Length;
                    skillPaint = null;
                }
                effPaints = null;
                eff = null;
                effTask = null;
                indexEff = -1;
                indexEffTask = -1;
            }
            else if (statusMe != 15 && (moveFast == null || moveFast[0] <= 0))
            {
                PaintCharName_HP_MP_Overhead(g);
                if (skillPaint == null || skillInfoPaint() == null || indexSkill >= skillInfoPaint().Length)
                {
                    paintCharWithoutSkill(g);
                }
                if (arr != null)
                {
                    arr.paint(g);
                }
                if (dart != null)
                {
                    dart.paint(g);
                }
                paintEffect(g);
                if (mobMe != null)
                {
                }
                paintMount2(g);
                paintEff_Lvup_front(g);
                paintSuperEffFront(g);
                paintAuraFront(g);
                paintEffFront(g);
                paint_map_line(g);
            }
        }
    }

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

    private void paintEff_Pet(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.effId >= 201)
            {
                effect.paint(g);
            }
        }
    }

    private void paintSuperEffBehind(mGraphics g)
    {
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        if (!isPaintAura2 || (statusMe != 1 && statusMe != 6) || GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0 || isCopy || clevel < 16)
        {
            return;
        }
        int num = 7598;
        int num2 = 4;
        if (clevel >= 19)
        {
            num = 7676;
        }
        if (clevel >= 22)
        {
            num = 7677;
        }
        if (clevel >= 25)
        {
            num = 7678;
        }
        if (num != -1)
        {
            Small small = SmallImage.imgNew[num];
            if (small == null)
            {
                SmallImage.createImage(num);
                return;
            }
            int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
            g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
        }
    }

    private void paintSuperEffFront(mGraphics g)
    {
        if (me)
        {
            if (!isPaintAura && idAuraEff > -1)
            {
                return;
            }
        }
        else if (idAuraEff > -1)
        {
            return;
        }
        if (!isPaintAura2)
        {
            return;
        }
        if (statusMe == 1 || statusMe == 6)
        {
            if (GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0)
            {
                return;
            }
            if (isCopy)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
                return;
            }
            if (clevel >= 14 && !GameCanvas.lowGraphic)
            {
                bool flag = false;
                if (mSystem.currentTimeMillis() - timeBlue > -1000 && IsAddDust1)
                {
                    flag = true;
                    IsAddDust1 = false;
                }
                if (mSystem.currentTimeMillis() - timeBlue > -500 && IsAddDust2)
                {
                    flag = true;
                    IsAddDust2 = false;
                }
                if (flag)
                {
                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                    GameCanvas.gI().startDust(1, cx - 8, cy);
                    addDustEff(1);
                }
            }
            if (clevel == 14)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
            }
            else if (clevel == 15)
            {
                if (GameCanvas.gameTick % 2 == 0)
                {
                    tBlue++;
                }
                if (tBlue > 6)
                {
                    tBlue = 0;
                }
            }
            else
            {
                if (clevel < 16)
                {
                    return;
                }
                int num = -1;
                int num2 = 4;
                if (clevel >= 16 && clevel < 22)
                {
                    num = 7599;
                    num2 = 4;
                }
                if (num != -1)
                {
                    Small small = SmallImage.imgNew[num];
                    if (small == null)
                    {
                        SmallImage.createImage(num);
                        return;
                    }
                    int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
                    g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
            }
        }
        else
        {
            timeBlue = mSystem.currentTimeMillis() + 1500;
            IsAddDust1 = true;
            IsAddDust2 = true;
        }
    }

    private void paintEffect(mGraphics g)
    {
        if (effPaints != null)
        {
            for (int i = 0; i < effPaints.Length; i++)
            {
                if (effPaints[i] == null)
                {
                    continue;
                }
                if (effPaints[i].eMob != null)
                {
                    int y = effPaints[i].eMob.y;
                    if (effPaints[i].eMob is BigBoss)
                    {
                        y = effPaints[i].eMob.y - 60;
                    }
                    if (effPaints[i].eMob is BigBoss2)
                    {
                        y = effPaints[i].eMob.y - 50;
                    }
                    if (effPaints[i].eMob is BachTuoc)
                    {
                        y = effPaints[i].eMob.y - 40;
                    }
                    SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
                else if (effPaints[i].eChar != null)
                {
                    SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eChar.cx, effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
                }
            }
        }
        if (indexEff >= 0 && eff != null)
        {
            SmallImage.drawSmallImage(g, eff.arrEfInfo[indexEff].idImg, cx + eff.arrEfInfo[indexEff].dx, cy + eff.arrEfInfo[indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
        }
        if (indexEffTask >= 0 && effTask != null)
        {
            SmallImage.drawSmallImage(g, effTask.arrEfInfo[indexEffTask].idImg, cx + effTask.arrEfInfo[indexEffTask].dx, cy + effTask.arrEfInfo[indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
        }
    }

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

    private void paintCharWithoutSkill(mGraphics g)
    {
        try
        {
            if (isMafuba)
            {
                paintCharBody(g, xMFB, yMFB, cdir, cf, isPaintBag: false);
                return;
            }
            if (isInvisiblez)
            {
                if (me)
                {
                    if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
                    {
                        SmallImage.drawSmallImage(g, 1196, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                    else
                    {
                        SmallImage.drawSmallImage(g, 1195, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                }
            }
            else
            {
                paintCharBody(g, cx, cy + fy, cdir, cf, isPaintBag: true);
            }
            if (isLockAttack)
            {
                SmallImage.drawSmallImage(g, 290, cx, cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
            }
        }
        catch (Exception ex)
        {
            Cout.LogError("Loi paint char without skill: " + ex.ToString());
        }
    }

    public void paintBag(mGraphics g, int[] id, int x, int y, int dir, bool isPaintChar)
    {
        int num = 0;
        int num2 = 0;
        if (statusMe == 6)
        {
            num = 8;
            num2 = 17;
        }
        if (statusMe == 1)
        {
            if (cp1 % 15 < 5)
            {
                num = 8;
                num2 = 17;
            }
            else
            {
                num = 8;
                num2 = 18;
            }
        }
        if (statusMe == 2)
        {
            if (cf <= 3)
            {
                num = 7;
                num2 = 17;
            }
            else
            {
                num = 7;
                num2 = 18;
            }
        }
        if (statusMe == 3 || statusMe == 9)
        {
            num = 5;
            num2 = 20;
        }
        if (statusMe == 4)
        {
            if (cf == 8)
            {
                num = 5;
                num2 = 16;
            }
            else
            {
                num = 5;
                num2 = 20;
            }
        }
        if (statusMe == 10)
        {
            if (cf == 8)
            {
                num = 0;
                num2 = 23;
            }
            else
            {
                num = 5;
                num2 = 22;
            }
        }
        if (isInjure > 0)
        {
            num = 5;
            num2 = 18;
        }
        if (skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length)
        {
            num = -1;
            num2 = 17;
        }
        fBag++;
        if (fBag > 10000)
        {
            fBag = 0;
        }
        sbyte b = (sbyte)(fBag / 4 % id.Length);
        if (!isPaintChar)
        {
            if (id.Length == 2)
            {
                b = 1;
            }
            if (id.Length == 3)
            {
                if (id[2] >= 0)
                {
                    b = 2;
                    if (GameCanvas.gameTick % 10 > 5)
                    {
                        b = 1;
                    }
                }
                else
                {
                    b = 1;
                }
            }
        }
        else if (id.Length > 1 && (b == 0 || b == 1) && statusMe != 1 && statusMe != 6)
        {
            fBag = 0;
            b = 0;
            if (GameCanvas.gameTick % 10 > 5)
            {
                b = 1;
            }
        }
        SmallImage.drawSmallImage(g, id[b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
    }

    public bool isCharBodyImageID(int id)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || leg < 0 || leg >= GameScr.parts.Length || body < 0 || body >= GameScr.parts.Length)
        {
            return false;
        }
        Part part = GameScr.parts[head];
        Part part2 = GameScr.parts[leg];
        Part part3 = GameScr.parts[body];
        if (part == null || part2 == null || part3 == null)
        {
            return false;
        }
        for (int i = 0; i < CharInfo.Length; i++)
        {
            if (id == part.pi[CharInfo[i][0][0]].id)
            {
                return true;
            }
            if (id == part2.pi[CharInfo[i][1][0]].id)
            {
                return true;
            }
            if (id == part3.pi[CharInfo[i][2][0]].id)
            {
                return true;
            }
        }
        return false;
    }

    public void paintHead(mGraphics g, int cx, int cy, int look)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || GameScr.parts[head] == null)
        {
            return;
        }
        Part part = GameScr.parts[head];
        SmallImage.drawSmallImage(g, part.pi[CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
    }

    public void paintHeadWithXY(mGraphics g, int x, int y, int look)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || GameScr.parts[head] == null)
        {
            return;
        }
        Part part = GameScr.parts[head];
        SmallImage.drawSmallImage(g, part.pi[CharInfo[0][0][0]].id, x + CharInfo[0][0][1] + part.pi[CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
    }

    public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
    {
        if (GameScr.parts == null)
        {
            GameScr.gI().readPart();
        }
        if (GameScr.parts == null || head < 0 || head >= GameScr.parts.Length || leg < 0 || leg >= GameScr.parts.Length || body < 0 || body >= GameScr.parts.Length)
        {
            return;
        }
        ph = GameScr.parts[head];
        pl = GameScr.parts[leg];
        pb = GameScr.parts[body];
        if (ph == null || pl == null || pb == null)
        {
            return;
        }
        if (bag >= 0 && statusMe != 14)
        {
            if (!ClanImage.idImages.containsKey(bag + string.Empty))
            {
                ClanImage.idImages.put(bag + string.Empty, new ClanImage());
                Service.gI().requestBagImage((sbyte)bag);
            }
            else
            {
                ClanImage clanImage = (ClanImage)ClanImage.idImages.get(bag + string.Empty);
                if (clanImage.idImage != null && isPaintBag)
                {
                    paintBag(g, clanImage.idImage, cx, cy, cdir, isPaintChar: true);
                }
            }
        }
        int num = 2;
        int anchor = 24;
        int anchor2 = StaticObj.TOP_RIGHT;
        int num2 = -1;
        if (cdir == 1)
        {
            num = 0;
            anchor = 0;
            anchor2 = 0;
            num2 = 1;
        }
        if (statusMe == 14)
        {
            if (GameCanvas.gameTick % 4 > 0)
            {
                g.drawImage(ItemMap.imageFlare, cx, cy - ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            int num3 = 0;
            if (head == 89 || head == 457 || head == 460 || head == 461 || head == 462 || head == 463 || head == 464 || head == 465 || head == 466)
            {
                num3 = 15;
            }
            SmallImage.drawSmallImage(g, 834, cx, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
            SmallImage.drawSmallImage(g, 79, cx, cy - ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
            SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            paintHat_behind(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            if (isHead_2Fr(head))
            {
                Part part = GameScr.parts[getFHead(head)];
                SmallImage.drawSmallImage(g, part.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + part.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + part.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            else
            {
                SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            paintHat_front(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            paintRedEye(g, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
        }
        else
        {
            paintHat_behind(g, cf, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy);
            if (isHead_2Fr(head))
            {
                Part part2 = GameScr.parts[getFHead(head)];
                SmallImage.drawSmallImage(g, part2.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + part2.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + part2.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            else
            {
                SmallImage.drawSmallImage(g, ph.pi[CharInfo[cf][0][0]].id, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
            }
            SmallImage.drawSmallImage(g, pl.pi[CharInfo[cf][1][0]].id, cx + (CharInfo[cf][1][1] + pl.pi[CharInfo[cf][1][0]].dx) * num2, cy - CharInfo[cf][1][2] + pl.pi[CharInfo[cf][1][0]].dy, num, anchor);
            SmallImage.drawSmallImage(g, pb.pi[CharInfo[cf][2][0]].id, cx + (CharInfo[cf][2][1] + pb.pi[CharInfo[cf][2][0]].dx) * num2, cy - CharInfo[cf][2][2] + pb.pi[CharInfo[cf][2][0]].dy, num, anchor);
            paintRedEye(g, cx + (CharInfo[cf][0][1] + ph.pi[CharInfo[cf][0][0]].dx) * num2, cy - CharInfo[cf][0][2] + ph.pi[CharInfo[cf][0][0]].dy, num, anchor);
        }
        ch = ((isMonkey != 1 && !isFusion) ? (CharInfo[0][0][2] + ph.pi[CharInfo[0][0][0]].dy + 10) : 60);
        int num4 = ((Res.abs(ph.pi[CharInfo[cf][0][0]].dy) < 22) ? ph.pi[CharInfo[cf][0][0]].dy : ((ph.pi[CharInfo[cf][0][0]].dy >= 0) ? (ph.pi[CharInfo[cf][0][0]].dy - 5) : (ph.pi[CharInfo[cf][0][0]].dy + 5)));
        cH_new = cy - CharInfo[cf][0][2] + num4;
        if (statusMe == 1 && charID > 0 && !isMask && !isUseChargeSkill() && !isWaitMonkey && skillPaint == null && cf != 23 && bag < 0 && ((GameCanvas.gameTick + charID) % 30 == 0 || isFreez))
        {
            g.drawImage((cgender != 1) ? eyeTraiDat : eyeNamek, cx + -((cgender != 1) ? 2 : 2) * num2, cy - 32 + ((cgender != 1) ? 11 : 10) - cf, anchor2);
        }
        if (eProtect != null)
        {
            eProtect.paint(g);
        }
        if (eDanhHieu != null)
        {
            eDanhHieu.paint(g);
        }
        paintPKFlag(g);
    }

    public void paintCharWithSkill(mGraphics g)
    {
        ty = 0;
        SkillInfoPaint[] array = skillInfoPaint();
        cf = array[indexSkill].status;
        paintCharWithoutSkill(g);
        if (cdir == 1)
        {
            if (eff0 != null)
            {
                if (dx0 == 0)
                {
                    dx0 = array[indexSkill].e0dx;
                }
                if (dy0 == 0)
                {
                    dy0 = array[indexSkill].e0dy;
                }
                SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx + dx0 + eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i0++;
                if (i0 >= eff0.arrEfInfo.Length)
                {
                    eff0 = null;
                    i0 = (dx0 = (dy0 = 0));
                }
            }
            if (eff1 != null)
            {
                if (dx1 == 0)
                {
                    dx1 = array[indexSkill].e1dx;
                }
                if (dy1 == 0)
                {
                    dy1 = array[indexSkill].e1dy;
                }
                SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx + dx1 + eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i1++;
                if (i1 >= eff1.arrEfInfo.Length)
                {
                    eff1 = null;
                    i1 = (dx1 = (dy1 = 0));
                }
            }
            if (eff2 != null)
            {
                if (dx2 == 0)
                {
                    dx2 = array[indexSkill].e2dx;
                }
                if (dy2 == 0)
                {
                    dy2 = array[indexSkill].e2dy;
                }
                SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx + dx2 + eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                i2++;
                if (i2 >= eff2.arrEfInfo.Length)
                {
                    eff2 = null;
                    i2 = (dx2 = (dy2 = 0));
                }
            }
        }
        else
        {
            if (eff0 != null)
            {
                if (dx0 == 0)
                {
                    dx0 = array[indexSkill].e0dx;
                }
                if (dy0 == 0)
                {
                    dy0 = array[indexSkill].e0dy;
                }
                SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx - dx0 - eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i0++;
                if (i0 >= eff0.arrEfInfo.Length)
                {
                    eff0 = null;
                    i0 = 0;
                    dx0 = 0;
                    dy0 = 0;
                }
            }
            if (eff1 != null)
            {
                if (dx1 == 0)
                {
                    dx1 = array[indexSkill].e1dx;
                }
                if (dy1 == 0)
                {
                    dy1 = array[indexSkill].e1dy;
                }
                SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx - dx1 - eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i1++;
                if (i1 >= eff1.arrEfInfo.Length)
                {
                    eff1 = null;
                    i1 = 0;
                    dx1 = 0;
                    dy1 = 0;
                }
            }
            if (eff2 != null)
            {
                if (dx2 == 0)
                {
                    dx2 = array[indexSkill].e2dx;
                }
                if (dy2 == 0)
                {
                    dy2 = array[indexSkill].e2dy;
                }
                SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx - dx2 - eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
                i2++;
                if (i2 >= eff2.arrEfInfo.Length)
                {
                    eff2 = null;
                    i2 = 0;
                    dx2 = 0;
                    dy2 = 0;
                }
            }
        }
        indexSkill++;
    }

    public static int getIndexChar(int ID)
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char @char = (Char)GameScr.vCharInMap.elementAt(i);
            if (@char.charID == ID)
            {
                return i;
            }
        }
        return -1;
    }

    // moveTo extracted to Char.Navigation.cs

    public static void getcharInjure(int cID, int dx, int dy, long HP)
    {
        Char @char = (Char)GameScr.vCharInMap.elementAt(cID);
        if (@char.vMovePoints.size() != 0)
        {
            MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
            int xEnd = movePoint.xEnd + dx;
            int yEnd = movePoint.yEnd + dy;
            Char char2 = (Char)GameScr.vCharInMap.elementAt(cID);
            char2.cHP -= HP;
            if (char2.cHP < 0)
            {
                char2.cHP = 0;
            }
            char2.cHPShow = ((Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
            char2.statusMe = 6;
            char2.cp3 = 0;
            char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
        }
    }

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

    // searchItem() extracted to Char.Targeting.cs

    // searchFocus() extracted to Char.Targeting.cs

    // ClearFocus() extracted to Char.Targeting.cs

    // isCharInScreen() extracted to Char.Targeting.cs

    // isAttacPlayerStatus() extracted to Char.Targeting.cs

    // setHoldChar() extracted to Char.Targeting.cs

    // setHoldMob() extracted to Char.Targeting.cs

    // findNextFocusByKey() extracted to Char.Targeting.cs

    // deFocusNPC() extracted to Char.Targeting.cs

    // updateCharInBridge extracted to Char.Navigation.cs

    public static void sort(int[] data)
    {
        int num = 5;
        for (int i = 0; i < num - 1; i++)
        {
            for (int j = i + 1; j < num; j++)
            {
                if (data[i] < data[j])
                {
                    int num2 = data[j];
                    data[j] = data[i];
                    data[i] = num2;
                }
            }
        }
    }

    public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
    {
        if (x > cmWx || x < cmX || y > cmyH || y < cmy)
        {
            return false;
        }
        return true;
    }

    public void kickOption(Item item, int maxKick)
    {
        int num = 0;
        if (item == null || item.options == null)
        {
            return;
        }
        for (int i = 0; i < item.options.size(); i++)
        {
            ItemOption itemOption = (ItemOption)item.options.elementAt(i);
            itemOption.active = 0;
            if (itemOption.optionTemplate.type == 2)
            {
                if (num < maxKick)
                {
                    itemOption.active = 1;
                    num++;
                }
            }
            else if (itemOption.optionTemplate.type == 3 && item.upgrade >= 4)
            {
                itemOption.active = 1;
            }
            else if (itemOption.optionTemplate.type == 4 && item.upgrade >= 8)
            {
                itemOption.active = 1;
            }
            else if (itemOption.optionTemplate.type == 5 && item.upgrade >= 12)
            {
                itemOption.active = 1;
            }
            else if (itemOption.optionTemplate.type == 6 && item.upgrade >= 14)
            {
                itemOption.active = 1;
            }
            else if (itemOption.optionTemplate.type == 7 && item.upgrade >= 16)
            {
                itemOption.active = 1;
            }
        }
    }

    public void doInjure(long HPShow, int MPShow, bool isCrit, bool isMob)
    {
        this.isCrit = isCrit;
        this.isMob = isMob;
        cHP -= HPShow;
        cMP -= MPShow;
        GameScr.gI().isInjureHp = true;
        GameScr.gI().twHp = 0;
        GameScr.gI().isInjureMp = true;
        GameScr.gI().twMp = 0;
        if (cHP < 0)
        {
            cHP = 0;
        }
        if (cMP < 0)
        {
            cMP = 0;
        }
        if (isMob || (!isMob && cTypePk != 4 && damMP != -100))
        {
            if (HPShow <= 0)
            {
                if (me)
                {
                    GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS_ME);
                }
                else
                {
                    GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS);
                }
            }
            else
            {
                GameScr.startFlyText("-" + HPShow, cx, cy - ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
            }
        }
        if (HPShow > 0)
        {
            isInjure = 6;
        }
        ServerEffect.addServerEffect(80, this, 1);
        if (isDie)
        {
            isDie = false;
            isLockKey = false;
            startDie((short)xSd, (short)ySd);
        }
    }

    public void doInjure()
    {
        GameScr.gI().isInjureHp = true;
        GameScr.gI().twHp = 0;
        GameScr.gI().isInjureMp = true;
        GameScr.gI().twMp = 0;
        isInjure = 6;
        ServerEffect.addServerEffect(8, this, 1);
        isInjureHp = true;
        twHp = 0;
    }

    public void startDie(short toX, short toY)
    {
        isMonkey = 0;
        isWaitMonkey = false;
        if (me && isDie)
        {
            return;
        }
        if (me)
        {
            isLockMove = true;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                @char.killCharId = -9999;
            }
            if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
            {
                GameCanvas.panel.cp = null;
            }
            if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
            {
                GameCanvas.panel2.cp = null;
            }
        }
        statusMe = 5;
        cp2 = toX;
        cp3 = toY;
        cp1 = 0;
        cHP = 0;
        testCharId = -9999;
        killCharId = -9999;
        if (me && myskill != null && myskill.template.id != 14)
        {
            stopUseChargeSkill();
        }
        cTypePk = 0;
    }

    public void waitToDie(short toX, short toY)
    {
        wdx = toX;
        wdy = toY;
    }

    public void liveFromDead()
    {
        cHP = cHPFull;
        cMP = cMPFull;
        statusMe = 1;
        cp1 = (cp2 = (cp3 = 0));
        ServerEffect.addServerEffect(109, this, 2);
        GameScr.gI().center = null;
        GameScr.isHaveSelectSkill = true;
    }

    public bool doUsePotion()
    {
        if (arrItemBag == null)
        {
            return false;
        }
        for (int i = 0; i < arrItemBag.Length; i++)
        {
            if (arrItemBag[i] != null && arrItemBag[i].template.type == 6)
            {
                Service.gI().useItem(0, 1, -1, arrItemBag[i].template.id);
                return true;
            }
        }
        return false;
    }

    public bool isLang()
    {
        if (TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48)
        {
            return true;
        }
        return false;
    }

    public bool isMeCanAttackOtherPlayer(Char cAtt)
    {
        if (cAtt == null || myCharz().myskill == null || myCharz().myskill.template.type == 2 || (myCharz().myskill.template.type == 4 && cAtt.statusMe != 14 && cAtt.statusMe != 5))
        {
            return false;
        }
        return ((cAtt.cTypePk == 3 && myCharz().cTypePk == 3) || myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (myCharz().cTypePk == 1 && cAtt.cTypePk == 1) || (myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (myCharz().testCharId >= 0 && myCharz().testCharId == cAtt.charID) || (myCharz().killCharId >= 0 && myCharz().killCharId == cAtt.charID && !isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == myCharz().charID && !isLang()) || (myCharz().cFlag == 8 && cAtt.cFlag != 0) || (myCharz().cFlag != 0 && cAtt.cFlag == 8) || (myCharz().cFlag != cAtt.cFlag && myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14 && cAtt.statusMe != 5;
    }

    public void clearTask()
    {
        myCharz().taskMaint = null;
        for (int i = 0; i < myCharz().arrItemBag.Length; i++)
        {
            if (myCharz().arrItemBag[i] != null && myCharz().arrItemBag[i].template.type == 8)
            {
                myCharz().arrItemBag[i] = null;
            }
        }
        Npc.clearEffTask();
    }

    public int getX()
    {
        return cx;
    }

    public int getY()
    {
        return cy;
    }

    public int getH()
    {
        return 32;
    }

    public int getW()
    {
        return 24;
    }

    // FocusManualTo() extracted to Char.Targeting.cs

    // stopMoving extracted to Char.Navigation.cs

    public void cancelAttack()
    {
    }

    public bool isInvisible()
    {
        return false;
    }

    // focusToAttack() extracted to Char.Targeting.cs

    public void addDustEff(int type)
    {
        if (GameCanvas.lowGraphic)
        {
            return;
        }
        switch (type)
        {
            case 1:
                if (clevel >= 9)
                {
                    Effect effect3 = new Effect(19, cx - 5, cy + 20, 2, 1, -1);
                    EffecMn.addEff(effect3);
                }
                break;
            case 2:
                if ((!me || isMonkey != 1) && isNhapThe && GameCanvas.gameTick % 5 == 0)
                {
                    Effect effect2 = new Effect(22, cx - 5, cy + 35, 2, 1, -1);
                    EffecMn.addEff(effect2);
                }
                break;
            case 3:
                if (clevel >= 9 && ySd - cy <= 5)
                {
                    Effect effect = new Effect(19, cx - 5, ySd + 20, 2, 1, -1);
                    EffecMn.addEff(effect);
                }
                break;
        }
    }

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

    public void removeHoleEff()
    {
        if (holder)
        {
            holder = false;
            charHold = null;
            mobHold = null;
        }
        else
        {
            holdEffID = 0;
            charHold = null;
            mobHold = null;
        }
    }

    public void removeProtectEff()
    {
        protectEff = false;
        eProtect = null;
    }

    public void removeBlindEff()
    {
        blindEff = false;
    }

    public void removeEffect()
    {
        if (holdEffID != 0)
        {
            holdEffID = 0;
        }
        if (holder)
        {
            holder = false;
        }
        if (protectEff)
        {
            protectEff = false;
        }
        eProtect = null;
        charHold = null;
        mobHold = null;
        blindEff = false;
        sleepEff = false;
    }

    // setPos extracted to Char.Navigation.cs

    public void removeHuytSao()
    {
        huytSao = false;
    }

    public void fusionComplete()
    {
        isFusion = false;
        isLockKey = false;
        tFusion = 0;
    }

    public void setFusion(sbyte fusion)
    {
        tFusion = 0;
        if (fusion == 4 || fusion == 5)
        {
            if (me)
            {
                Service.gI().funsion(fusion);
            }
            EffecMn.addEff(new Effect(34, cx, cy + 12, 2, 1, -1));
        }
        if (fusion == 6)
        {
            EffecMn.addEff(new Effect(38, cx, cy + 12, 2, 1, -1));
        }
        if (me)
        {
            GameCanvas.panel.hideNow();
            isLockKey = true;
        }
        isFusion = true;
        if (fusion == 1)
        {
            isNhapThe = false;
        }
        else
        {
            isNhapThe = true;
        }
    }

    public void removeSleepEff()
    {
        sleepEff = false;
    }

    public void setPartOld()
    {
        headTemp = head;
        bodyTemp = body;
        legTemp = leg;
        bagTemp = bag;
    }

    public void setPartTemp(int head, int body, int leg, int bag)
    {
        if (head != -1)
        {
            this.head = head;
        }
        if (body != -1)
        {
            this.body = body;
        }
        if (leg != -1)
        {
            this.leg = leg;
        }
        if (bag != -1)
        {
            this.bag = bag;
        }
    }

    public void resetPartTemp()
    {
        if (headTemp != -1)
        {
            head = headTemp;
            headTemp = -1;
        }
        if (bodyTemp != -1)
        {
            body = bodyTemp;
            bodyTemp = -1;
        }
        if (legTemp != -1)
        {
            leg = legTemp;
            legTemp = -1;
        }
        if (bagTemp != -1)
        {
            bag = bagTemp;
            bagTemp = -1;
        }
    }

    public Effect getEffById(int id)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.effId == id)
            {
                return effect;
            }
        }
        return null;
    }

    public void addEffChar(Effect e)
    {
        removeEffChar(0, e.effId);
        vEffChar.addElement(e);
    }

    public void removeEffChar(int type, int id)
    {
        if (type == -1)
        {
            vEffChar.removeAllElements();
        }
        else if (getEffById(id) != null)
        {
            vEffChar.removeElement(getEffById(id));
        }
    }

    public void paintEffBehind(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.layer == 0)
            {
                bool flag = true;
                if (effect.isStand == 0)
                {
                    flag = ((statusMe == 1 || statusMe == 6) ? true : false);
                }
                if (flag)
                {
                    effect.paint(g);
                }
            }
        }
    }

    public void paintEffFront(mGraphics g)
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            Effect effect = (Effect)vEffChar.elementAt(i);
            if (effect.layer == 1)
            {
                bool flag = true;
                if (effect.isStand == 0)
                {
                    flag = ((statusMe == 1 || statusMe == 6) ? true : false);
                }
                if (flag)
                {
                    effect.paint(g);
                }
            }
        }
    }

    public void updEffChar()
    {
        for (int i = 0; i < vEffChar.size(); i++)
        {
            ((Effect)vEffChar.elementAt(i)).update();
        }
    }

    public int checkLuong()
    {
        return luong + luongKhoa;
    }

    public void updateEye()
    {
        if (head != 934)
        {
            return;
        }
        if (GameCanvas.timeNow - timeAddChopmat > 0)
        {
            fChopmat++;
            if (fChopmat > frEye.Length - 1)
            {
                fChopmat = 0;
                timeAddChopmat = GameCanvas.timeNow + Res.random(2000, 3500);
                frEye = frChopCham;
                if (Res.random(2) == 0)
                {
                    frEye = frChopNhanh;
                }
            }
        }
        else
        {
            fChopmat = 0;
        }
    }

    private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
    {
        if (head != 934 || (statusMe != 1 && statusMe != 6))
        {
            return;
        }
        if (fraRedEye == null || fraRedEye.imgFrame == null)
        {
            Image img = mSystem.loadImage("/redeye.png");
            fraRedEye = new FrameImage(img, 14, 10);
        }
        else if (frEye[fChopmat] != -1)
        {
            int num = 8;
            int num2 = 15;
            if (trans == 2)
            {
                num = -8;
            }
            fraRedEye.drawFrame(frEye[fChopmat], xx + num, yy + num2, trans, anchor, g);
        }
    }

    public bool isHead_2Fr(int idHead)
    {
        for (int i = 0; i < Arr_Head_2Fr.Length; i++)
        {
            if (Arr_Head_2Fr[i][0] == idHead)
            {
                return true;
            }
        }
        return false;
    }

    private void updateFHead()
    {
        if (isHead_2Fr(head))
        {
            fHead++;
            if (fHead > 10000)
            {
                fHead = 0;
            }
        }
        else
        {
            fHead = 0;
        }
    }

    private int getFHead(int idHead)
    {
        for (int i = 0; i < Arr_Head_2Fr.Length; i++)
        {
            if (Arr_Head_2Fr[i][0] == idHead)
            {
                return Arr_Head_2Fr[i][fHead / 4 % Arr_Head_2Fr[i].Length];
            }
        }
        return idHead;
    }

    public void paintAuraBehind(mGraphics g)
    {
        if ((!me || !isPaintAura) && idAuraEff > -1 && (statusMe == 1 || statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - timeBlue > 0)
        {
            string nameImg = strEffAura + idAuraEff + "_0";
            FrameImage fraImage = mSystem.getFraImage(nameImg);
            fraImage?.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
        }
    }

    public void paintAuraFront(mGraphics g)
    {
        if ((me && !isPaintAura) || idAuraEff <= -1)
        {
            return;
        }
        if (statusMe == 1 || statusMe == 6)
        {
            if (!GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
            {
                bool flag = false;
                if (mSystem.currentTimeMillis() - timeBlue > -1000 && IsAddDust1)
                {
                    flag = true;
                    IsAddDust1 = false;
                }
                if (mSystem.currentTimeMillis() - timeBlue > -500 && IsAddDust2)
                {
                    flag = true;
                    IsAddDust2 = false;
                }
                if (flag)
                {
                    GameCanvas.gI().startDust(-1, cx - -8, cy);
                    GameCanvas.gI().startDust(1, cx - 8, cy);
                    addDustEff(1);
                }
                if (mSystem.currentTimeMillis() - timeBlue > 0)
                {
                    string nameImg = strEffAura + idAuraEff + "_1";
                    FrameImage fraImage = mSystem.getFraImage(nameImg);
                    fraImage?.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy + 2, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
            }
        }
        else
        {
            timeBlue = mSystem.currentTimeMillis() + 1500;
            IsAddDust1 = true;
            IsAddDust2 = true;
        }
    }

    public void paintEff_Lvup_behind(mGraphics g)
    {
        if (idEff_Set_Item != -1)
        {
            if (fraEff != null)
            {
                fraEff.drawFrame(GameCanvas.gameTick / 4 % fraEff.nFrame, cx, cy + 3, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraEff = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_0");
            }
        }
    }

    public void paintEff_Lvup_front(mGraphics g)
    {
        if (idEff_Set_Item != -1)
        {
            if (fraEffSub != null)
            {
                fraEffSub.drawFrame(GameCanvas.gameTick / 4 % fraEffSub.nFrame, cx, cy + 8, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraEffSub = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_1");
            }
        }
    }

    public void paintHat_behind(mGraphics g, int cf, int yh)
    {
        try
        {
            if (idHat == -1)
            {
                return;
            }
            if (isFrNgang(cf))
            {
                if (fraHat_behind_2 != null)
                {
                    fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind_2.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
                else
                {
                    fraHat_behind_2 = mSystem.getFraImage(strHat_behind + strNgang + idHat);
                }
            }
            else if (fraHat_behind != null)
            {
                fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraHat_behind = mSystem.getFraImage(strHat_behind + idHat);
            }
        }
        catch (Exception)
        {
        }
    }

    public void paintHat_front(mGraphics g, int cf, int yh)
    {
        try
        {
            if (idHat == -1)
            {
                return;
            }
            if (isFrNgang(cf))
            {
                if (fraHat_font_2 != null)
                {
                    fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_font_2.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
                }
                else
                {
                    fraHat_font_2 = mSystem.getFraImage(strHat_font + strNgang + idHat);
                }
            }
            else if (fraHat_font != null)
            {
                fraHat_font.drawFrame(GameCanvas.gameTick / 4 % fraHat_font.nFrame, cx + hatInfo[cf][0] * ((cdir == 1) ? 1 : (-1)), yh + hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
            }
            else
            {
                fraHat_font = mSystem.getFraImage(strHat_font + idHat);
            }
        }
        catch (Exception)
        {
        }
    }

    public bool isFrNgang(int fr)
    {
        if (fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29)
        {
            return true;
        }
        return false;
    }

    public void sendNewAttack(short idTemplateSkill)
    {
        short x = -1;
        short y = -1;
        if (mobFocus != null)
        {
            x = (short)mobFocus.x;
            y = (short)mobFocus.y;
        }
        if (charFocus != null && !charFocus.isPet && !charFocus.isMiniPet)
        {
            x = (short)charFocus.cx;
            y = (short)charFocus.cy;
        }
        Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)cdir, x, y);
    }

    // skillPaint_NEW extracted to Char.Skills.cs

    public Char clone()
    {
        Char @char = new Char();
        @char.charID = charID;
        @char.cx = cx;
        @char.cy = cy;
        @char.cdir = cdir;
        if (arrItemBody != null)
        {
            @char.arrItemBody = new Item[arrItemBody.Length];
            for (int i = 0; i < arrItemBody.Length; i++)
            {
                if (arrItemBody[i] == null)
                {
                    @char.arrItemBody[i] = null;
                }
                else
                {
                    @char.arrItemBody[i] = arrItemBody[i].clone();
                }
            }
        }
        return @char;
    }

    public bool containsCaiTrang(int v)
    {
        if (arrItemBody != null)
        {
            for (int i = 0; i < arrItemBody.Length; i++)
            {
                if (arrItemBody[i] != null && arrItemBody[i].template != null && arrItemBody[i].template.id == v)
                {
                    return true;
                }
            }
        }
        Res.err("tim kiem id cai trang " + v + " ko tim thay");
        return false;
    }

    public void printlog()
    {
        string empty = string.Empty;
        string text = empty;
        empty = text + "isInjure " + isInjure + "\n";
        text = empty;
        empty = text + "isInjure " + isMonkey + "\n";
        text = empty;
        empty = text + "isInjure " + isAddChopMat + "\n";
        text = empty;
        empty = text + "isInjure " + isAttack + "\n";
        text = empty;
        empty = text + "isInjure " + isAttFly + "\n";
        text = empty;
        empty = text + "isInjure " + ischangingMap + "\n";
        text = empty;
        empty = text + "isInjure " + isCharge + "\n";
        text = empty;
        empty = text + "isInjure " + isCopy + "\n";
        text = empty;
        empty = text + "isInjure " + isCreateDark + "\n";
        text = empty;
        empty = text + "isInjure " + isCrit + "\n";
        text = empty;
        empty = text + "isInjure " + isDirtyPostion + "\n";
        text = empty;
        empty = text + "isInjure " + isEndMount + "\n";
        text = empty;
        empty = text + "isInjure " + isEventMount + "\n";
        text = empty;
        empty = text + "isInjure " + isMafuba + "\n";
        text = empty;
        empty = text + "isInjure " + isFusion + "\n";
        text = empty;
        empty = text + "isInjure " + isFeetEff + "\n";
        text = empty;
        empty = text + "isInjure " + isFlying + "\n";
        text = empty;
        empty = text + "isInjure " + isWaitMonkey + "\n";
        text = empty;
        empty = text + "isInjure " + isUseSkillSpec() + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
        text = empty;
        empty = text + "isInjure " + isDie + "\n";
    }

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
