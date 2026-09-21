using System;
using System.Collections;
using System.Threading;
using Assets.src.g;
using UnityEngine;

public partial class GameCanvas
{
    public static void showErrorForm(int type, string moreInfo)
    {
    }

    public static void endDlg()
    {
        if (inputDlg != null)
        {
            inputDlg.tfInput.setMaxTextLenght(500);
        }
        currentDialog = null;
        InfoDlg.hide();
    }
    static long time;
    public static void startOKDlg(string info)
    {
        closeKeyBoard();
        InfoDlg.hide();
        msgdlg.setInfo(info, null, new Command(mResources.OK, instance, 8882, null), null);
        currentDialog = msgdlg;
    }

    public static void startWaitDlg(string info)
    {
        closeKeyBoard();
        msgdlg.setInfo(info, null, new Command(mResources.CANCEL, instance, 8882, null), null);
        currentDialog = msgdlg;
        msgdlg.isWait = true;
    }

    public static void startOKDlg(string info, bool isError)
    {
        closeKeyBoard();
        msgdlg.setInfo(info, null, new Command(mResources.CANCEL, instance, 8882, null), null);
        currentDialog = msgdlg;
        msgdlg.isWait = true;
    }

    public static void startWaitDlg()
    {
        closeKeyBoard();
        Char.isLoadingMap = true;
    }

    public void openWeb(string strLeft, string strRight, string url, string str)
    {
        msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
        currentDialog = msgdlg;
    }

    public static void startOK(string info, int actionID, object p)
    {
        closeKeyBoard();
        msgdlg.setInfo(info, null, new Command(mResources.OK, instance, actionID, p), null);
        msgdlg.show();
    }

    public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
    {
        closeKeyBoard();
        msgdlg.setInfo(info, new Command(mResources.YES, instance, iYes, pYes), new Command(string.Empty, instance, iYes, pYes), new Command(mResources.NO, instance, iNo, pNo));
        msgdlg.show();
    }

    public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
    {
        closeKeyBoard();
        msgdlg.setInfo(info, cmdYes, null, cmdNo);
        msgdlg.show();
    }

    public static void startserverThongBao(string msgSv)
    {
        thongBaoTest = msgSv;
        xThongBaoTranslate = w - 60;
        dir_ = -1;
    }

    public static string getMoneys(int m)
    {
        string text = string.Empty;
        int num = m / 1000 + 1;
        for (int i = 0; i < num; i++)
        {
            if (m >= 1000)
            {
                int num2 = m % 1000;
                text = ((num2 != 0) ? ((num2 >= 10) ? ((num2 >= 100) ? ("." + num2 + text) : (".0" + num2 + text)) : (".00" + num2 + text)) : (".000" + text));
                m /= 1000;
                continue;
            }
            text = m + text;
            break;
        }
        return text;
    }

    public static int getX(int start, int w)
    {
        return (px - start) / w;
    }

    public static int getY(int start, int w)
    {
        return (py - start) / w;
    }

    protected void sizeChanged(int w, int h)
    {
    }

    public static bool isGetResourceFromServer()
    {
        return true;
    }

    public static Image loadImageRMS(string path)
    {
        string originalPath = path;
        string fullPath = cutPng(Main.res + "/x" + mGraphics.zoomLevel + originalPath);
        Image result = null;
        try
        {
            result = Image.createImage(fullPath);
        }
        catch (Exception ex)
        {
            try
            {
                string[] array = Res.split(fullPath, "/", 0);
                string filename = "x" + mGraphics.zoomLevel + array[array.Length - 1];
                sbyte[] array2 = Rms.loadRMS(filename);
                if (array2 != null)
                {
                    result = Image.createImage(array2, 0, array2.Length);
                    array2 = null;
                }
            }
            catch (Exception)
            {
                Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
            }
        }
        if (result == null && mGraphics.zoomLevel != 2)
        {
            try
            {
                result = Image.createImage(cutPng(Main.res + "/x2" + originalPath));
            }
            catch (Exception)
            {
                try
                {
                    result = Image.createImage(cutPng(Main.res + "/x2" + originalPath).ToLower());
                }
                catch (Exception)
                {
                }
            }
        }
        if (result == null && mGraphics.zoomLevel != 1)
        {
            try
            {
                result = Image.createImage(cutPng(Main.res + "/x1" + originalPath));
            }
            catch (Exception)
            {
                try
                {
                    result = Image.createImage(cutPng(Main.res + "/x1" + originalPath).ToLower());
                }
                catch (Exception)
                {
                }
            }
        }
        return result;
    }

    public static Image loadImage(string path)
    {
        string originalPath = path;
        string fullPath = cutPng(Main.res + "/x" + mGraphics.zoomLevel + originalPath);
        Image result = null;
        try
        {
            result = Image.createImage(fullPath);
        }
        catch (Exception)
        {
        }
        if (result == null)
        {
            try
            {
                result = Image.createImage(fullPath.ToLower());
            }
            catch (Exception)
            {
            }
        }
        if (result == null && mGraphics.zoomLevel != 2)
        {
            try
            {
                result = Image.createImage(cutPng(Main.res + "/x2" + originalPath));
            }
            catch (Exception)
            {
                try
                {
                    result = Image.createImage(cutPng(Main.res + "/x2" + originalPath).ToLower());
                }
                catch (Exception)
                {
                }
            }
        }
        if (result == null && mGraphics.zoomLevel != 1)
        {
            try
            {
                result = Image.createImage(cutPng(Main.res + "/x1" + originalPath));
            }
            catch (Exception)
            {
                try
                {
                    result = Image.createImage(cutPng(Main.res + "/x1" + originalPath).ToLower());
                }
                catch (Exception)
                {
                }
            }
        }
        return result;
    }

    public static Image LoadImageFromRoot(string path)
    {
        path = Main.res + path;
        path = cutPng(path);
        Image result = null;
        try
        {
            result = Image.createImage(path);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static string cutPng(string str)
    {
        string result = str;
        if (str.Contains(".png"))
        {
            result = str.Replace(".png", string.Empty);
        }
        return result;
    }

    public static int random(int a, int b)
    {
        return a + r.nextInt(b - a);
    }

    public bool startDust(int dir, int x, int y)
    {
        if (lowGraphic)
        {
            return false;
        }
        int num = ((dir != 1) ? 1 : 0);
        if (dustState[num] != -1)
        {
            return false;
        }
        dustState[num] = 0;
        dustX[num] = x;
        dustY[num] = y;
        return true;
    }

    public void loadWaterSplash()
    {
        if (!lowGraphic)
        {
            imgWS = new Image[3];
            for (int i = 0; i < 3; i++)
            {
                imgWS[i] = loadImage("/e/w" + i + ".png");
            }
            wsX = new int[2];
            wsY = new int[2];
            wsState = new int[2];
            wsF = new int[2];
            wsState[0] = (wsState[1] = -1);
        }
    }

    public bool startWaterSplash(int x, int y)
    {
        if (lowGraphic)
        {
            return false;
        }
        int num = ((wsState[0] != -1) ? 1 : 0);
        if (wsState[num] != -1)
        {
            return false;
        }
        wsState[num] = 0;
        wsX[num] = x;
        wsY[num] = y;
        return true;
    }

    public void updateWaterSplash()
    {
        if (lowGraphic)
        {
            return;
        }
        for (int i = 0; i < 2; i++)
        {
            if (wsState[i] == -1)
            {
                continue;
            }
            wsY[i]--;
            if (gameTick % 2 == 0)
            {
                wsState[i]++;
                if (wsState[i] > 2)
                {
                    wsState[i] = -1;
                }
                else
                {
                    wsF[i] = wsState[i];
                }
            }
        }
    }

    public void updateDust()
    {
        if (lowGraphic)
        {
            return;
        }
        for (int i = 0; i < 2; i++)
        {
            if (dustState[i] != -1)
            {
                dustState[i]++;
                if (dustState[i] >= 5)
                {
                    dustState[i] = -1;
                }
                if (i == 0)
                {
                    dustX[i]--;
                }
                else
                {
                    dustX[i]++;
                }
                dustY[i]--;
            }
        }
    }

    public static bool isPaint(int x, int y)
    {
        if (x < GameScr.cmx)
        {
            return false;
        }
        if (x > GameScr.cmx + GameScr.gW)
        {
            return false;
        }
        if (y < GameScr.cmy)
        {
            return false;
        }
        if (y > GameScr.cmy + GameScr.gH + 30)
        {
            return false;
        }
        return true;
    }

    public void paintDust(mGraphics g)
    {
        if (lowGraphic)
        {
            return;
        }
        for (int i = 0; i < 2; i++)
        {
            if (dustState[i] != -1 && isPaint(dustX[i], dustY[i]))
            {
                g.drawImage(imgDust[i][dustState[i]], dustX[i], dustY[i], 3);
            }
        }
    }

    public void loadDust()
    {
        if (lowGraphic)
        {
            return;
        }
        if (imgDust == null)
        {
            imgDust = new Image[2][];
            for (int i = 0; i < imgDust.Length; i++)
            {
                imgDust[i] = new Image[5];
            }
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    imgDust[j][k] = loadImage("/e/d" + j + k + ".png");
                }
            }
        }
        dustX = new int[2];
        dustY = new int[2];
        dustState = new int[2];
        dustState[0] = (dustState[1] = -1);
    }

    public static void paintShukiren(int x, int y, mGraphics g)
    {
        g.drawRegion(imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
    }

    public void resetToLoginScrz()
    {
        resetToLoginScr = true;
    }

}
