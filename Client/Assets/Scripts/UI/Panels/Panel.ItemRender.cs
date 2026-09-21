using System;
using UnityEngine;

public partial class Panel
{
    // ==================== paintEffectItem ====================
    private void paintEffectItem(mGraphics g, Item item, int x, int y)
    {
        try
        {
            Image[] bg = null;
            Image[] eff = null;
            if (item != null && item.itemOption != null)
            {
                foreach (var option in item.itemOption)
                {
                    if (option != null && (option.optionTemplate.id == 72|| option.optionTemplate.id == 225|| (option.optionTemplate.id >= 241 && option.optionTemplate.id <= 247)))
                    {
                        switch (option.param)
                        {
                            case 1:
                                eff = effxanhnhat;
                                break;
                            case 2:
                                bg = bgxanhnhat;
                                eff = effxanhnhat;
                                break;
                            case 3:
                                eff = effxanhla;
                                break;
                            case 4:
                                bg = bgxanhla;
                                eff = effxanhla;
                                break;
                            case 5:
                                eff = efftim;
                                break;
                            case 6:
                                bg = bgtim;
                                eff = efftim;
                                break;
                            case 7:
                                eff = effcam;
                                break;
                            case 8:
                                bg = bgcam;
                                eff = effcam;
                                break;
                            case 9:
                                eff = effdo;
                                break;
                            case 10:
                                bg = bgdo;
                                eff = effdo;
                                break;
                        }
                        //Debug.LogError(eff != null ? "non null" : "null");
                        if(eff != null)
                        g.drawImage(eff[GameCanvas.gameTick / 4 % 7], x + 2, y + 2);
                        if (bg != null)
                            g.drawImage(bg[GameCanvas.gameTick / 4 % 7], x - 1, y - 1);
                        //Debug.LogError(x + " " + y);
                    }
                    //if (option != null && option.optionTemplate.id == 225)
                    //{
                    //    switch (option.param)
                    //    {
                    //        case 0:
                    //        case 1:
                    //        case 2:
                    //        case 3:
                    //            bg = bgxanhnhat;
                    //            break;
                    //        case 4:
                    //        case 5:
                    //            bg = bgxanhnhat;
                    //            eff = effxanhnhat;
                    //            break;
                    //        case 6:
                    //        case 7:
                    //        case 8:
                    //            bg = bgxanhla;
                    //            break;
                    //        case 9:                               
                    //        case 10:
                    //            bg = bgxanhla;
                    //            eff = effxanhla;
                    //            break;
                    //        case 11:
                    //        case 12:
                    //        case 13:
                    //            bg = bgtim;
                    //            break;
                    //        case 14:
                    //        case 15:
                    //            bg = bgtim;
                    //            eff = efftim;
                    //            break;
                    //        case 16:
                    //        case 17:
                    //        case 18:
                    //        case 19:
                    //            bg = bgcam;
                    //            break;
                    //        case 20:
                    //            bg = bgcam;
                    //            eff = effcam;
                    //            break;
                    //    }
                    //    //Debug.LogError(eff != null ? "non null" : "null");
                    //    if (eff != null)
                    //        g.drawImage(eff[GameCanvas.gameTick / 4 % 7], x + 2, y + 2);
                    //    if (bg != null)
                    //        g.drawImage(bg[GameCanvas.gameTick / 4 % 7], x - 1, y - 1);
                    //    //Debug.LogError(x + " " + y);
                    //}
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }


    // ==================== paintUpgradeEffect ====================
    public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
    {
        try
        {
            int num = (wItem << 1) + (hItem << 1);
            int num2 = num / nline;
            nsize = sizeUpgradeEff.Length;
            if (nline > 4)
            {
                nsize = 2;
            }
            for (int i = 0; i < nline; i++)
            {
                for (int j = 0; j < nsize; j++)
                {
                    int wSize = ((sizeUpgradeEff[j] <= 1) ? 1 : ((sizeUpgradeEff[j] >> 1) + 1));
                    int x2 = x + upgradeEffectX(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
                    int y2 = y + upgradeEffectY(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
                    g.setColor(colorUpgradeEffect[cl][j]);
                    g.fillRect(x2, y2, sizeUpgradeEff[j], sizeUpgradeEff[j]);
                }
            }
        }
        catch (Exception)
        {
        }
    }


    // ==================== upgradeEffectX ====================
    private static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
    {
        int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
        if (0 <= num && num < wItem)
        {
            return num % wItem;
        }
        if (wItem <= num && num < wItem + hitem)
        {
            return wItem - wSize;
        }
        if (wItem + hitem <= num && num < (wItem << 1) + hitem)
        {
            return wItem - (num - hitem) % wItem - wSize;
        }
        return 0;
    }


    // ==================== upgradeEffectY ====================
    private static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
    {
        int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
        if (0 <= num && num < wItem)
        {
            return 0;
        }
        if (wItem <= num && num < wItem + hitem)
        {
            return num % wItem;
        }
        if (wItem + hitem <= num && num < (wItem << 1) + hitem)
        {
            return hitem - wSize;
        }
        return hitem - (num - (wItem << 1)) % hitem - wSize;
    }


    // ==================== GetColor_ItemBg ====================
    public static int GetColor_ItemBg(int id)
    {
        return id switch
        {
            4 => 1269146,
            1 => 2786816,
            5 => 13279744,
            3 => 12537346,
            2 => 7078041,
            6 => 11599872,
            _ => -1,
        };
    }


    // ==================== paintOptItem ====================
    public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
    {
        switch (idOpt)
        {
            case 34:
                if (imgo_0 != null)
                {
                    g.drawImage(imgo_0, x, y + h - imgo_0.getHeight());
                }
                else
                {
                    imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
                }
                if (imgo_1 != null)
                {
                    g.drawImage(imgo_1, x, y + h - imgo_1.getHeight());
                }
                else
                {
                    imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
                }
                break;
            case 35:
                if (imgo_0 != null)
                {
                    g.drawImage(imgo_0, x, y + h - imgo_0.getHeight());
                }
                else
                {
                    imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
                }
                if (imgo_2 != null)
                {
                    g.drawImage(imgo_2, x, y + h - imgo_2.getHeight());
                }
                else
                {
                    imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
                }
                break;
            case 36:
                if (imgo_0 != null)
                {
                    g.drawImage(imgo_0, x, y + h - imgo_0.getHeight());
                }
                else
                {
                    imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
                }
                if (imgo_3 != null)
                {
                    g.drawImage(imgo_3, x, y + h - imgo_3.getHeight());
                }
                else
                {
                    imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
                }
                break;
            case 225:
                if (imgo_0 != null)
                {
                    g.drawImage(imgo_0, x, y + h - imgo_0.getHeight());
                }
                else
                {
                    imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
                }
                if (imgo_4 != null)
                {
                    g.drawImage(imgo_4, x, y + h - imgo_4.getHeight());
                }
                else
                {
                    imgo_4 = mSystem.loadImage("/mainImage/o_4.png");
                }
                break;
            case 206:
                if (imgo_0 != null)
                {
                    g.drawImage(imgo_0, x, y + h - imgo_0.getHeight());
                }
                else
                {
                    imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
                }
                if (imgo_5 != null)
                {
                    g.drawImage(imgo_5, x, y + h - imgo_5.getHeight());
                }
                else
                {
                    imgo_5 = mSystem.loadImage("/mainImage/o_5.png");
                }
                break;
        }
    }


    // ==================== paintOptItemInventory ====================
    public void paintOptItemInventory(mGraphics g, int idOpt, int param, int x, int y, int w, int h, Item item)
    {
        int num1 = 156;// vi tri x tam giac trai
        int num3 = 145;// vi tri y tam giac phai
        int num4 = 12;// vi tri x tam giac trai
        int num2 = 19;// vi tri y tam giac phai
        try
        {
            switch (idOpt)
            {
                case 102:
                     if (imgo_17 != null && param > 0)
                    {
                        for (int l = param; l < 10; l++)
                        {
                            g.drawImage(imgo_19, x + w - imgo_19.getWidth() + num2 + 2 + l * 3, y + h - imgo_19.getHeight());
                        }
                        for (int i = 0; i < Math.min(param, 7); i++)
                        {
                            g.drawImage(imgo_17, x + w - imgo_17.getWidth() + num2 + 2 + i * 3, y + h - imgo_17.getHeight());
                        }
                        if (param > 7)
                        {
                            for (int j = 7; j < param; j++)
                            {
                                g.drawImage(imgo_18, x + w - imgo_18.getWidth() + num2 + 2 + j * 3, y + h - imgo_18.getHeight());
                            }
                        }
                        mFont.tahoma_7b_dark.drawString(g, string.Empty + param, x + w - imgo_17.getWidth() + num2 + 1, y + h - imgo_17.getHeight() - 1, 1);
                    }
                    else
                    {
                       
                        imgo_19 = mSystem.loadImage("/mainImage/starE.png");
                        imgo_17 = mSystem.loadImage("/mainImage/star.png");
                        imgo_18 = mSystem.loadImage("/mainImage/star8.png");
                    }
                    break;             

                case 34:
                    if (imgo_0 != null)
                    {
                        g.drawImage(imgo_0, x + w - imgo_0.getWidth() + num3, y + h - imgo_0.getHeight() - num4);
                    }
                    else
                    {
                        imgo_0 = mSystem.loadImage("/mainImage/o_00.png");
                    }
                    if (imgo_1 != null)
                    {
                        g.drawImage(imgo_1, x + w - imgo_1.getWidth() + num3, y + h - imgo_1.getHeight() - num2);
                    }
                    else
                    {
                        imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
                    }
                    break;
                case 35:
                    if (imgo_0 != null)
                    {
                        g.drawImage(imgo_0, x + w - imgo_0.getWidth() + num3, y + h - imgo_0.getHeight()-num4);
                    }
                    else
                    {
                        imgo_0 = mSystem.loadImage("/mainImage/o_00.png");
                    }
                    if (imgo_2 != null)
                    {
                        g.drawImage(imgo_2, x + w - imgo_2.getWidth() + num3, y + h - imgo_2.getHeight()- num2);
                    }
                    else
                    {
                        imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
                    }
                    break;
                case 36:
                    if (imgo_0 != null)
                    {
                        g.drawImage(imgo_0, x + w - imgo_0.getWidth() + num3, y + h - imgo_0.getHeight()-num4);
                    }
                    else
                    {
                        imgo_0 = mSystem.loadImage("/mainImage/o_00.png");
                    }
                    if (imgo_3 != null)
                    {
                        g.drawImage(imgo_3, x + w - imgo_3.getWidth() + num3, y + h - imgo_3.getHeight()-num2);
                    }
                    else
                    {
                        imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
                    }
                    break;
                case 225:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }
                    if (imgo_4 != null)
                    {
                        g.drawImage(imgo_4, x + w - imgo_4.getWidth() + num3, y + h - imgo_4.getHeight() - num2);
                    }
                    else
                    {
                        imgo_4 = mSystem.loadImage("/mainImage/o_4.png");
                    }
                    break;
                case 241:
                    //if (imgo_00 != null)
                    //{
                    //    g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    //}
                    //else
                    //{
                    //    imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    //}

                    if (imgo_10 != null)
                    {
                        g.drawImage(imgo_10, x + w - imgo_10.getWidth() + num3, y + h - imgo_10.getHeight() - num2);
                    }
                    else
                    {
                        imgo_10 = mSystem.loadImage("/mainImage/22641.png");
                    }

                    break;
                case 242:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_11 != null)
                    {
                        g.drawImage(imgo_11, x + w - imgo_11.getWidth() + num3, y + h - imgo_11.getHeight() - num2);
                    }
                    else
                    {
                        imgo_11 = mSystem.loadImage("/mainImage/22642.png");
                    }

                    break;
                case 243:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_12 != null)
                    {
                        g.drawImage(imgo_12, x + w - imgo_12.getWidth() + num3, y + h - imgo_12.getHeight() - num2);
                    }
                    else
                    {
                        imgo_12 = mSystem.loadImage("/mainImage/22643.png");
                    }

                    break;
                case 244:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_13 != null)
                    {
                        g.drawImage(imgo_13, x + w - imgo_13.getWidth() + num3, y + h - imgo_13.getHeight() - num2);
                    }
                    else
                    {
                        imgo_13 = mSystem.loadImage("/mainImage/22644.png");
                    }

                    break;
                case 245:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_14 != null)
                    {
                        g.drawImage(imgo_14, x + w - imgo_14.getWidth() + num3, y + h - imgo_14.getHeight() - num2);
                    }
                    else
                    {
                        imgo_14 = mSystem.loadImage("/mainImage/22645.png");
                    }

                    break;
                case 246:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_15 != null)
                    {
                        g.drawImage(imgo_15, x + w - imgo_15.getWidth() + num3, y + h - imgo_15.getHeight() - num2);
                    }
                    else
                    {
                        imgo_15 = mSystem.loadImage("/mainImage/22646.png");
                    }

                    break;
                case 247:
                    if (imgo_00 != null)
                    {
                        g.drawImage(imgo_00, x + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() - num4);
                    }
                    else
                    {
                        imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    }

                    if (imgo_16 != null)
                    {
                        g.drawImage(imgo_16, x + w - imgo_16.getWidth() + num3, y + h - imgo_16.getHeight() - num2);
                    }
                    else
                    {
                        imgo_16 = mSystem.loadImage("/mainImage/22647.png");
                    }

                    break;
                   
                case 206:
                    //if (imgo_00 != null)
                    //{
                    //    g.drawImage(imgo_00, x  + w - imgo_00.getWidth() + num3, y + h - imgo_00.getHeight() -num4);
                    //}
                    //else
                    //{
                    //    imgo_00 = mSystem.loadImage("/mainImage/o_00.png");
                    //}
                    if (item.template.type == 0)
                    {
                        if (imgo_5 != null)
                        {
                            g.drawImage(imgo_5, x + w - imgo_5.getWidth() + num3, y + h - imgo_5.getHeight() + 1);
                        }
                        else
                        {
                            imgo_5 = mSystem.loadImage("/mainImage/o_5.png");
                        }
                        mFont.tahoma_7b_red.drawString(g, string.Empty + param, x + w - imgo_5.getWidth() + num3, y + h - imgo_5.getHeight(), 1);
                    }
                    if (item.template.type == 1)
                    {
                        if (imgo_6 != null)
                        {
                            g.drawImage(imgo_6, x + w - imgo_6.getWidth() + num3, y + h - imgo_6.getHeight() + 1);
                        }
                        else
                        {
                            imgo_6 = mSystem.loadImage("/mainImage/o_6.png");
                        }
                        mFont.tahoma_7b_blue.drawString(g, string.Empty + param, x + w - imgo_6.getWidth() + num3, y + h - imgo_6.getHeight(), 1);
                    }
                    if (item.template.type == 2)
                    {
                        if (imgo_7 != null)
                        {
                            g.drawImage(imgo_7, x + w - imgo_7.getWidth() + num3, y + h - imgo_7.getHeight() + 1);
                        }
                        else
                        {
                            imgo_7 = mSystem.loadImage("/mainImage/o_7.png");
                        }
                        mFont.tahoma_7b_green.drawString(g, string.Empty + param, x + w - imgo_7.getWidth() + num3, y + h - imgo_7.getHeight(), 1);
                    }
                    if (item.template.type == 3)
                    {
                        if (imgo_8 != null)
                        {
                            g.drawImage(imgo_8, x + w - imgo_8.getWidth() + num3, y + h - imgo_8.getHeight() + 1);
                        }
                        else
                        {
                            imgo_8 = mSystem.loadImage("/mainImage/o_8.png");
                        }
                        mFont.tahoma_7b_yellow.drawString(g, string.Empty + param, x + w - imgo_8.getWidth() + num3, y + h - imgo_8.getHeight(), 1);
                    }
                    
                    break;
            }
        }
        catch(Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }


    // ==================== paintOptSlotItem ====================
    public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
    {
        if (idOpt == 102 && param > ChatPopup.numSlot)
        {
            sbyte color_Item_Upgrade = GetColor_Item_Upgrade(param);
            int nline = param - ChatPopup.numSlot;
            paintUpgradeEffect(x, y, w, h, nline, color_Item_Upgrade, g);
        }
    }


    // ==================== setTextColor ====================
    public static mFont setTextColor(int id, int type)
    {
        if (type == 0)
        {
            return id switch
            {
                0 => mFont.bigNumber_While,
                1 => mFont.bigNumber_green,
                3 => mFont.bigNumber_orange,
                4 => mFont.bigNumber_blue,
                5 => mFont.bigNumber_yellow,
                6 => mFont.bigNumber_red,
                _ => mFont.bigNumber_While,
            };
        }
        return id switch
        {
            0 => mFont.tahoma_7b_white,
            1 => mFont.tahoma_7b_green,
            3 => mFont.tahoma_7b_yellowSmall2,
            4 => mFont.tahoma_7b_blue,
            5 => mFont.tahoma_7b_yellow,
            6 => mFont.tahoma_7b_red,
            7 => mFont.tahoma_7b_dark,
            _ => mFont.tahoma_7b_white,
        };
    }


}
