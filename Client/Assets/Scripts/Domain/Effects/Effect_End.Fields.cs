using System;
using UnityEngine;

public partial class Effect_End
{
    public const sbyte Lvlpaint_All = -1;

    public const sbyte Lvlpaint_Front = 0;

    public const sbyte Lvlpaint_Mid = 1;

    public const sbyte Lvlpaint_Mid_2 = 2;

    public const sbyte Lvlpaint_Behind = 3;

    public const short End_String_Lose = 0;

    public const short End_String_Win = 1;

    public const short End_String_Draw = 2;

    public const short End_FireWork = 3;

    public const short End_line_in = 9;

    public const short End_e8_rock = 10;

    public const short End_e8_ice = 11;

    public const short End_SUB_MaFuBa = 16;

    public const short End_SUB_Destroy = 17;

    public const short End_POW_Kamex10 = 18;

    public const short End_POW_Destroy = 19;

    public const short End_POW_MaFuBa = 20;

    public const short End_GONG_Kamex10 = 21;

    public const short End_GONG_Destroy = 22;

    public const short End_GONG_MaFuBa = 23;

    public const short End_Skill_Kamex10 = 24;

    public const short End_Skill_Destroy = 25;

    public const short End_Skill_MaFuBa = 26;

    private readonly MyVector VecEffEnd = new("EffectEnd VecEffEnd");

    public FrameImage fraImgEff;

    public byte[] nFrame = new byte[10];

    public byte[] nFrame_2 = new byte[10];

    public int typePaint;

    public sbyte skillLevel;

    public int typeEffect;

    public int typeSub;

    public int range;

    public short idEndeff;

    public int fRemove;

    public int fMove;

    public int n_frame;

    public int x;

    public int y;

    public int w;

    public int h;

    public int dir;

    public int dir_nguoc;

    public int levelPaint;

    public int f;

    public int frame;

    public int fSpeed;

    public int vx;

    public int vy;

    public int x1000;

    public int y1000;

    public int vx1000;

    public int vy1000;

    public int dy_throw;

    public int vMax;

    public int toX;

    public int toY;

    public int stt;

    public int dx;

    public int dy;

    public short timeRemove;

    public long time;

    public bool isRemove;

    public bool isAddSub;

    public Char charUse;

    public Point[] listObj;

    public Point target;

    public static short[][] arrInfoEff = new short[53][]
    {
        new short[3] { 68, 264, 4 },
        new short[3] { 30, 120, 4 },
        new short[3] { 66, 280, 4 },
        new short[3] { 0, 0, 1 },
        new short[3] { 111, 68, 2 },
        new short[3] { 90, 68, 2 },
        new short[3] { 125, 68, 2 },
        new short[3] { 47, 282, 6 },
        new short[3] { 10, 40, 4 },
        new short[3] { 92, 525, 7 }, // 9
        new short[3] { 62, 372, 6 },
        new short[3] { 80, 352, 4 },
        new short[3] { 80, 352, 4 },
        new short[3] { 80, 352, 4 }, // 13
        new short[3] { 72, 240, 3 }, // 14
        new short[3] { 20, 42, 3 },
        new short[3] { 65, 160, 4 },
        new short[3] { 50, 300, 6 },
        new short[3] { 84, 168, 2 },
        new short[3] { 90, 540, 6 },
        new short[3] { 180, 900, 6 }, // 20
        new short[3] { 62, 186, 3 }, // 21
        new short[3] { 34, 80, 4 }, // 22
        new short[3] { 140, 560, 4 }, // 23
        new short[3] { 64, 600, 6 }, // 24
        new short[3] { 36, 200, 5 },
        new short[3] { 35, 200, 5 },
        new short[3] { 50, 250, 5 },
        new short[3] { 50, 240, 6 }, // 28
        new short[3] { 68, 264, 4 },
        new short[3] { 30, 120, 4 },
        new short[3] { 92, 525, 7 }, // 31
        new short[3] { 62, 372, 6 },
        new short[3] { 80, 352, 4 },
        new short[3] { 80, 352, 4 },
        new short[3] { 80, 352, 4 }, // 35
        new short[3] { 72, 240, 3 }, // 36
        new short[3] { 20, 42, 3 },
        new short[3] { 65, 160, 4 },
        new short[3] { 50, 300, 6 }, // 39
        new short[3] { 50, 300, 6 }, // 40
        new short[3] { 84, 168, 2 }, // 41
        new short[3] { 84, 168, 2 }, // 42
        new short[3] { 90, 540, 6 },
        new short[3] { 180, 900, 6 }, // 44
        new short[3] { 62, 186, 3 }, // 45
        new short[3] { 34, 80, 4 }, // 46
        new short[3] { 140, 560, 4 },
        new short[3] { 140, 560, 4 }, // 48
        new short[3] { 64, 600, 6 }, // 49
        new short[3] { 35, 200, 5 },
        new short[3] { 50, 250, 5 },
        new short[3] { 50, 240, 6 }, // 52
    };

    public int life;

    public int goc_Arc;

    public int va;

    public int gocT_Arc;

    public byte[] mpaintone_Arrow = new byte[24]
    {
        12, 11, 10, 9, 8, 7, 6, 5, 4, 3,
        2, 1, 0, 23, 22, 21, 20, 19, 18, 17,
        16, 15, 14, 13
    };

    public byte[] mImageArrow = new byte[24]
    {
        0, 0, 2, 1, 1, 2, 0, 0, 2, 1,
        1, 2, 0, 0, 2, 1, 1, 2, 0, 0,
        2, 1, 1, 2
    };

    public byte[] mXoayArrow = new byte[24]
    {
        2, 2, 3, 3, 3, 4, 5, 5, 5, 5,
        5, 1, 0, 0, 0, 0, 0, 7, 6, 6,
        6, 6, 6, 2
    };

    private int rS;

    private int angleS;

    private int angleO;

    private int iAngleS;

    private int iDotS;

    private int[] xArgS;

    private int[] yArgS;

    private int[] xDotS;

    private int[] yDotS;

    public static int[][] colorStar = new int[3][]
    {
        new int[3] { 16310304, 16298056, 16777215 },
        new int[3] { 7045120, 12643960, 16777215 },
        new int[3] { 2407423, 11987199, 16777215 }
    };

    private int[] colorpaint;

    private int indexColorStar;

    private int xline;

    private int yline;

    private FrameImage[] fra_skill;

    public Effect_End(int type, int typeSub, int typePaint, Char charUse, Point target, int levelPaint, short timeRemove, short range, sbyte level)
    {
        f = 0;
        stt = 0;
        typeEffect = type;
        this.typeSub = typeSub;
        this.typePaint = typePaint;
        this.charUse = charUse;
        this.skillLevel = level;
        if (charUse.containsCaiTrang(1265))
        {
            if (typeEffect == 21 || typeEffect == 22 || typeEffect == 23)
            {
                this.charUse.cx += 10 * this.charUse.cdir;
            }
            else if (typeEffect == 18 || typeEffect == 19 || typeEffect == 20)
            {
                this.charUse.cx += -15 * this.charUse.cdir;
            }
            else
            {
                this.charUse.cx += 15 * this.charUse.cdir;
            }
        }
        x = this.charUse.cx;
        y = this.charUse.cy;
        dir = this.charUse.cdir;
        dir_nguoc = ((dir == -1) ? 2 : 0);
        this.target = target;
        this.levelPaint = levelPaint;
        time = mSystem.currentTimeMillis();
        this.timeRemove = timeRemove;
        this.range = range;
        isRemove = (isAddSub = false);
        n_frame = 4;
        get_Img_Skill();
        create_Effect();
    }

    public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
    {
        f = 0;
        stt = 0;
        typeEffect = type;
        this.typeSub = typeSub;
        this.typePaint = typePaint;
        this.x = x;
        this.y = y;
        this.levelPaint = levelPaint;
        this.dir = dir;
        dir_nguoc = ((dir == -1) ? 2 : 0);
        time = mSystem.currentTimeMillis();
        this.timeRemove = timeRemove;
        isRemove = (isAddSub = false);
        n_frame = 4;
        if (listObj != null)
        {
            this.listObj = new Point[listObj.Length];
            for (int i = 0; i < this.listObj.Length; i++)
            {
                this.listObj[i] = listObj[i];
            }
        }
        get_Img_Skill();
        create_Effect();
    }

    public Effect_End(int type, int typeSub, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj, sbyte level)
    {
        f = 0;
        stt = 0;
        typeEffect = type;
        this.typeSub = typeSub;
        this.typePaint = typePaint;
        this.x = x;
        this.y = y;
        this.levelPaint = levelPaint;
        this.skillLevel = level;
        this.dir = dir;
        dir_nguoc = ((dir == -1) ? 2 : 0);
        time = mSystem.currentTimeMillis();
        this.timeRemove = timeRemove;
        isRemove = (isAddSub = false);
        n_frame = 4;
        if (listObj != null)
        {
            this.listObj = new Point[listObj.Length];
            for (int i = 0; i < this.listObj.Length; i++)
            {
                this.listObj[i] = listObj[i];
            }
        }
        get_Img_Skill();
        create_Effect();
    }

}
