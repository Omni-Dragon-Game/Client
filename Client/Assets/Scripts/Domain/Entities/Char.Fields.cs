using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
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

    // CharInfo extracted to Char.Tables.cs

    // CHAR_WEAPON extracted to Char.Tables.cs

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

    // inforClass_Skill extracted to Char.Tables.cs

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

    // Arr_Head_2Fr extracted to Char.Tables.cs

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

    // hatInfo extracted to Char.Tables.cs

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

}
