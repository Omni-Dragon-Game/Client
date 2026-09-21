using System;
using Assets.src.e;
using Assets.src.g;

public partial class Char
{
    // --- bagBoxSort_useItem ---
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

    // --- defaultParts ---
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

    // --- inventoryUtils_potions ---
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

    // injure_die_revive extracted to Char.Combat.cs

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

    // --- checkLuong ---
    public int checkLuong()
    {
        return luong + luongKhoa;
    }

    // --- containsCaiTrang ---
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
}
