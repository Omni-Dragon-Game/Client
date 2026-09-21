using System;
using UnityEngine;

public partial class Effect_End
{
    private void updListObj_Mafuba(bool ismafuba)
    {
        if (listObj == null)
        {
            return;
        }
        for (int i = 0; i < listObj.Length; i++)
        {
            if (listObj[i] == null)
            {
                continue;
            }
            if (listObj[i].type == 0)
            {
                Mob mob = GameScr.findMobInMap(listObj[i].id);
                if (mob != null)
                {
                    mob.isMafuba = ismafuba;
                    mob.isHide = false;
                    mob.xMFB = xDotS[i];
                    mob.yMFB = yDotS[i];
                }
                continue;
            }
            Char @char = null;
            @char = ((Char.myCharz().charID != listObj[i].id) ? GameScr.findCharInMap(listObj[i].id) : Char.myCharz());
            if (@char != null)
            {
                @char.isMafuba = ismafuba;
                @char.isHide = false;
                @char.xMFB = xDotS[i];
                @char.yMFB = yDotS[i];
            }
        }
    }

    private void hideListObj_Mafuba(bool ishide)
    {
        if (listObj == null)
        {
            return;
        }
        for (int i = 0; i < listObj.Length; i++)
        {
            if (listObj[i] == null)
            {
                continue;
            }
            if (listObj[i].type == 0)
            {
                Mob mob = GameScr.findMobInMap(listObj[i].id);
                if (mob != null)
                {
                    mob.isHide = ishide;
                }
                continue;
            }
            Char @char = null;
            @char = ((Char.myCharz().charID != listObj[i].id) ? GameScr.findCharInMap(listObj[i].id) : Char.myCharz());
            if (@char != null)
            {
                @char.isHide = ishide;
            }
        }
    }

    private void get_Img_Skill()
    {
        int skill_id = 0;
        int[] skill_arr_1 = null;
        int[] skill_arr_2 = null;
        switch (typeEffect)
        {
            // Skill 24
            case 18:
                skill_id = 24;
                skill_arr_1 = new int[1];
                skill_arr_2 = new int[1] { 9 };
                break;
            case 21:
                skill_id = 24;
                skill_arr_1 = new int[1] { 1 };
                skill_arr_2 = new int[1] { 10 };
                break;
            case 24:
                skill_id = 24;
                skill_arr_1 = new int[3] { 2, 3, 4 };
                skill_arr_2 = new int[3] { 11, 12, 13 };
                break;

            // Skill 25
            case 19:
                skill_id = 25;
                skill_arr_1 = new int[1];
                skill_arr_2 = new int[1] { 14 };
                break;
            case 22:
                skill_id = 25;
                skill_arr_1 = new int[1] { 1 };
                skill_arr_2 = new int[1] { 15 };
                break;
            case 17:
                skill_id = 25;
                skill_arr_1 = new int[1] { 2 };
                skill_arr_2 = new int[1] { 16 };
                break;
            case 25:
                skill_id = 25;
                skill_arr_1 = new int[4] { 3, 4, 5, 6 };
                skill_arr_2 = new int[4] { 17, 18, 19, 20 };
                break;

            // Skill 26
            case 20:
                skill_id = 26;
                skill_arr_1 = new int[1];
                skill_arr_2 = new int[1] { 21 };
                break;
            case 23:
                skill_id = 26;
                skill_arr_1 = new int[1] { 1 };
                skill_arr_2 = new int[1] { 22 };
                break;
            case 16:
                skill_id = 26;
                if (typeSub == 0)
                {
                    skill_arr_1 = new int[1] { 7 };
                    skill_arr_2 = new int[1] { 28 };
                }
                if (typeSub == 1)
                {
                    skill_arr_1 = new int[1] { 2 };
                    skill_arr_2 = new int[1] { 23 };
                }
                break;
            case 26:
                {
                    skill_id = 26;
                    int num2 = 0;
                    int num3 = 0;
                    if (typeSub == 0)
                    {
                        num2 = 4;
                        num3 = 25;
                    }
                    else if (typeSub == 1)
                    {
                        num2 = 5;
                        num3 = 26;
                    }
                    else if (typeSub == 2)
                    {
                        num2 = 6;
                        num3 = 27;
                    }
                    skill_arr_1 = new int[2] { num2, 3 };
                    skill_arr_2 = new int[2] { num3, 24 };
                    break;
                }
        }
        if (skill_arr_1 == null || skill_arr_2 == null)
        {
            return;
        }
        fra_skill = new FrameImage[skill_arr_1.Length];
        for (int i = 0; i < skill_arr_1.Length; i++)
        {
            string nameImg = "Skills_" + skill_id + "_" + typePaint + "_" + skill_arr_1[i];
            Debug.Log("NAME: " + nameImg);
            FrameImage frameImage = mSystem.getFraImage(nameImg);
            frameImage ??= new FrameImage(skill_arr_2[i]);
            if (frameImage != null)
            {
                fra_skill[i] = frameImage;
            }
        }
    }

    private void set_Gong()
    {
        if (charUse != null)
        {
            if (typeEffect == 21)
            {
                x = charUse.cx - 3 * charUse.cdir;
                y = charUse.cy;
                SoundMn.playSound(x, y, SoundMn.KAMEX10_0, SoundMn.volume);
            }
            else if (typeEffect == 22)
            {
                x = charUse.cx + 20 * charUse.cdir;
                y = charUse.cy - 4;
                SoundMn.playSound(x, y, SoundMn.DESTROY_2, SoundMn.volume);
            }
            else if (typeEffect == 23)
            {
                x = charUse.cx;
                y = charUse.cy - 50;
                SoundMn.playSound(x, y, SoundMn.MAFUBA_2, SoundMn.volume);
            }
            else
            {
                x = charUse.cx;
                y = charUse.cy;
            }
        }
    }

    private void upd_Gong()
    {
        if (charUse != null)
        {
            if (typeEffect == 21)
            {
                x = charUse.cx - 3 * charUse.cdir;
                y = charUse.cy;
            }
            else if (typeEffect == 22)
            {
                x = charUse.cx + 20 * charUse.cdir;
                y = charUse.cy - 4;
            }
            else if (typeEffect == 23)
            {
                x = charUse.cx;
                y = charUse.cy - 50;
            }
            else
            {
                x = charUse.cx;
                y = charUse.cy;
            }
        }
        if (timeRemove > 0)
        {
            if (GameCanvas.timeNow - time >= timeRemove)
            {
                removeEff();
            }
        }
        else if (f >= fra_skill[0].nFrame * n_frame)
        {
            removeEff();
        }
    }

    private void pnt_Gong(mGraphics g, int anchor)
    {
        if (fra_skill[0] != null)
        {
            fra_skill[0].drawFrame(f / n_frame % fra_skill[0].nFrame, x, y, dir_nguoc, anchor, g);
        }
    }

    private void set_Pow()
    {
        nFrame = null;
        n_frame = 3;
        if (typeEffect == 18)
        {
            if (typeSub == 0)
            {
                nFrame = new byte[9] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            }
            else
            {
                nFrame = new byte[12]
                {
                    3, 3, 3, 4, 4, 4, 5, 5, 5, 6,
                    6, 6
                };
            }
        }
    }

    private void upd_Pow()
    {
        if (charUse != null)
        {
            x = charUse.cx;
            y = charUse.cy + 13;
        }
        if (timeRemove > 0)
        {
            if (GameCanvas.timeNow - time >= timeRemove)
            {
                removeEff();
            }
        }
        else if (nFrame != null)
        {
            if (f > nFrame.Length)
            {
                removeEff();
            }
        }
        else if (f >= fra_skill[0].nFrame * n_frame)
        {
            removeEff();
        }
    }

    private void pnt_Pow(mGraphics g, int anchor)
    {
        if (fra_skill[0] != null)
        {
            if (nFrame != null)
            {
                fra_skill[0].drawFrame(nFrame[f % nFrame.Length], x, y, dir_nguoc, anchor, g);
            }
            else
            {
                fra_skill[0].drawFrame(f / n_frame % fra_skill[0].nFrame, x, y, dir_nguoc, anchor, g);
            }
        }
    }

    private void set_Sub()
    {
        if (typeEffect == 17)
        {
            x += ((dir != 0) ? (-fra_skill[0].frameWidth) : 0);
        }
    }

    private void upd_Sub()
    {
        if (timeRemove > 0)
        {
            if (GameCanvas.timeNow - time >= timeRemove)
            {
                removeEff();
            }
        }
        else if (f >= fra_skill[0].nFrame * n_frame)
        {
            removeEff();
        }
    }

    private void pnt_Sub(mGraphics g, int anchor)
    {
        fra_skill[0].drawFrame(f / n_frame % fra_skill[0].nFrame, x, y, dir, anchor, g);
    }

    private void set_()
    {
    }

    private void upd_()
    {
    }

    private void pnt_(mGraphics g)
    {
    }

}
