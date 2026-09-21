using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using Mod.XMAP;
using UnityEngine;

public partial class Controller
{
    private void onMessagePart3(Message msg)
    {
        Char @char = null;
        Mob mob = null;
        MyVector myVector = new MyVector();
        int num = 0;
        switch (msg.command)
        {
                case 1:
                    {
                        bool flag9 = msg.reader().readBool();
                        Res.outz("isRes= " + flag9);
                        if (!flag9)
                        {
                            GameCanvas.startOKDlg(msg.reader().readUTF());
                            break;
                        }
                        GameCanvas.loginScr.isLogin2 = false;
                        Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
                        GameCanvas.endDlg();
                        GameCanvas.loginScr.doLogin();
                        break;
                    }
                case 2:
                    Char.isLoadingMap = false;
                    LoginScr.isLoggingIn = false;
                    if (!GameScr.isLoadAllData)
                    {
                        GameScr.gI().initSelectChar();
                    }
                    BgItem.clearHashTable();
                    GameCanvas.endDlg();
                    CreateCharScr.isCreateChar = true;
                    CreateCharScr.gI().switchToMe();
                    break;
                case -107:
                    {
                        sbyte b21 = msg.reader().readByte();
                        if (b21 == 0)
                        {
                            Char.myCharz().havePet = false;
                        }
                        if (b21 == 1)
                        {
                            Char.myCharz().havePet = true;
                        }
                        if (b21 != 2)
                        {
                            break;
                        }
                        InfoDlg.hide();
                        Char.myPetz().head = msg.reader().readShort();
                        Char.myPetz().setDefaultPart();
                        int num40 = msg.reader().readUnsignedByte();
                        Char.myPetz().arrItemBody = new Item[num40];
                        for (int num41 = 0; num41 < num40; num41++)
                        {
                            short num42 = msg.reader().readShort();
                            if (num42 == -1)
                            {
                                continue;
                            }
                            Char.myPetz().arrItemBody[num41] = new Item
                            {
                                template = ItemTemplates.get(num42)
                            };
                            int num43 = Char.myPetz().arrItemBody[num41].template.type;
                            Char.myPetz().arrItemBody[num41].quantity = msg.reader().readInt();
                            Char.myPetz().arrItemBody[num41].info = msg.reader().readUTF();
                            Char.myPetz().arrItemBody[num41].content = msg.reader().readUTF();
                            int num44 = msg.reader().readUnsignedByte();
                            if (num44 != 0)
                            {
                                Char.myPetz().arrItemBody[num41].itemOption = new ItemOption[num44];
                                for (int num45 = 0; num45 < Char.myPetz().arrItemBody[num41].itemOption.Length; num45++)
                                {
                                    int num46 = msg.reader().readUnsignedByte();
                                    int param3 = msg.reader().readUnsignedShort();
                                    if (num46 != -1)
                                    {
                                        Char.myPetz().arrItemBody[num41].itemOption[num45] = new ItemOption(num46, param3);
                                    }
                                }
                            }
                            switch (num43)
                            {
                                case 0:
                                    Char.myPetz().body = Char.myPetz().arrItemBody[num41].template.part;
                                    break;
                                case 1:
                                    Char.myPetz().leg = Char.myPetz().arrItemBody[num41].template.part;
                                    break;
                            }
                        }
                        Char.myPetz().cHP = msg.readLong();
                        Char.myPetz().cHPFull = msg.readLong();
                        Char.myPetz().cMP = msg.readLong();
                        Char.myPetz().cMPFull = msg.readLong();
                        Char.myPetz().cDamFull = msg.readLong();
                        Char.myPetz().cName = msg.reader().readUTF();
                        Char.myPetz().currStrLevel = msg.reader().readUTF();
                        Char.myPetz().cPower = msg.reader().readLong();
                        Char.myPetz().cTiemNang = msg.reader().readLong();
                        Char.myPetz().petStatus = msg.reader().readByte();
                        Char.myPetz().cStamina = msg.reader().readShort();
                        Char.myPetz().cMaxStamina = msg.reader().readShort();
                        Char.myPetz().cCriticalFull = msg.reader().readByte();
                        Char.myPetz().cDefull = msg.reader().readInt();
                        Char.myPetz().arrPetSkill = new Skill[msg.reader().readByte()];
                        for (int num47 = 0; num47 < Char.myPetz().arrPetSkill.Length; num47++)
                        {
                            short num48 = msg.reader().readShort();
                            if (num48 != -1)
                            {
                                Char.myPetz().arrPetSkill[num47] = Skills.get(num48);
                                continue;
                            }
                            Char.myPetz().arrPetSkill[num47] = new Skill();
                            Char.myPetz().arrPetSkill[num47].template = null;
                            Char.myPetz().arrPetSkill[num47].moreInfo = msg.reader().readUTF();
                        }
                        if (!ModFunc.userOpenPet)
                        {
                            return;
                        }
                        if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
                        {
                            GameCanvas.panel2 = new Panel();
                            GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
                            GameCanvas.panel2.setTypeBodyOnly();
                            GameCanvas.panel2.show();
                            GameCanvas.panel.setTypePetMain();
                            GameCanvas.panel.show();
                            ModFunc.userOpenPet = false;
                        }
                        else
                        {
                            GameCanvas.panel.tabName[21] = mResources.petMainTab;
                            GameCanvas.panel.setTypePetMain();
                            GameCanvas.panel.show();
                            ModFunc.userOpenPet = false;
                        }
                        break;
                    }
                case 3:
                    {
                        sbyte type = msg.reader().readByte();
                        if (type == 0)
                        {
                            Char.myCharz().havePet2 = false;
                        }
                        if (type == 1)
                        {
                            Char.myCharz().havePet2 = true;
                        }
                        if (type != 2)
                        {
                            break;
                        }
                        InfoDlg.hide();
                        Char.MyPet2z().head = msg.reader().readShort();
                        Char.MyPet2z().setDefaultPart();
                        int arrBodySz = msg.reader().readUnsignedByte();
                        Char.MyPet2z().arrItemBody = new Item[arrBodySz];
                        for (int i = 0; i < arrBodySz; i++)
                        {
                            short tempId = msg.reader().readShort();
                            if (tempId == -1)
                            {
                                continue;
                            }
                            Char.MyPet2z().arrItemBody[i] = new Item
                            {
                                template = ItemTemplates.get(tempId)
                            };
                            int num43 = Char.MyPet2z().arrItemBody[i].template.type;
                            Char.MyPet2z().arrItemBody[i].quantity = msg.reader().readInt();
                            Char.MyPet2z().arrItemBody[i].info = msg.reader().readUTF();
                            Char.MyPet2z().arrItemBody[i].content = msg.reader().readUTF();
                            int num44 = msg.reader().readUnsignedByte();
                            if (num44 != 0)
                            {
                                Char.MyPet2z().arrItemBody[i].itemOption = new ItemOption[num44];
                                for (int num45 = 0; num45 < Char.MyPet2z().arrItemBody[i].itemOption.Length; num45++)
                                {
                                    int num46 = msg.reader().readUnsignedByte();
                                    int param3 = msg.reader().readUnsignedShort();
                                    if (num46 != -1)
                                    {
                                        Char.MyPet2z().arrItemBody[i].itemOption[num45] = new ItemOption(num46, param3);
                                    }
                                }
                            }
                            switch (num43)
                            {
                                case 0:
                                    Char.MyPet2z().body = Char.MyPet2z().arrItemBody[i].template.part;
                                    break;
                                case 1:
                                    Char.MyPet2z().leg = Char.MyPet2z().arrItemBody[i].template.part;
                                    break;
                            }
                        }
                        Char.MyPet2z().cHP = msg.readLong();
                        Char.MyPet2z().cHPFull = msg.readLong();
                        Char.MyPet2z().cMP = msg.readLong();
                        Char.MyPet2z().cMPFull = msg.readLong();
                        Char.MyPet2z().cDamFull = msg.readLong();
                        Char.MyPet2z().cName = msg.reader().readUTF();
                        Char.MyPet2z().currStrLevel = msg.reader().readUTF();
                        Char.MyPet2z().cPower = msg.reader().readLong();
                        Char.MyPet2z().cTiemNang = msg.reader().readLong();
                        Char.MyPet2z().petStatus = msg.reader().readByte();
                        Char.MyPet2z().cStamina = msg.reader().readShort();
                        Char.MyPet2z().cMaxStamina = msg.reader().readShort();
                        Char.MyPet2z().cCriticalFull = msg.reader().readByte();
                        Char.MyPet2z().cDefull = msg.reader().readInt();
                        Char.MyPet2z().arrPetSkill = new Skill[msg.reader().readByte()];
                        for (int num47 = 0; num47 < Char.MyPet2z().arrPetSkill.Length; num47++)
                        {
                            short num48 = msg.reader().readShort();
                            if (num48 != -1)
                            {
                                Char.MyPet2z().arrPetSkill[num47] = Skills.get(num48);
                                continue;
                            }
                            Char.MyPet2z().arrPetSkill[num47] = new Skill();
                            Char.MyPet2z().arrPetSkill[num47].template = null;
                            Char.MyPet2z().arrPetSkill[num47].moreInfo = msg.reader().readUTF();
                        }
                        if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
                        {
                            GameCanvas.panel2 = new Panel();
                            GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
                            GameCanvas.panel2.setTypeBodyOnly();
                            GameCanvas.panel2.show();
                            GameCanvas.panel.setTypePet2Main();
                            GameCanvas.panel.show();
                        }
                        else
                        {
                            GameCanvas.panel.tabName[21] = mResources.petMainTab;
                            GameCanvas.panel.setTypePet2Main();
                            GameCanvas.panel.show();
                        }
                        break;
                    }
                case -109:
                    Char.myPetz().cHPGoc = msg.readLong();
                    Char.myPetz().cMPGoc = msg.readLong();
                    Char.myPetz().cDamGoc = msg.readLong();
                    Char.myPetz().cDefGoc = msg.reader().readInt();
                    Char.myPetz().cCriticalGoc = msg.reader().readInt();
                    break;
                case -37:
                    {
                        sbyte b20 = msg.reader().readByte();
                        if (b20 != 0)
                        {
                            break;
                        }
                        Char.myCharz().head = msg.reader().readShort();
                        Char.myCharz().setDefaultPart();
                        int num33 = msg.reader().readUnsignedByte();
                        Res.outz("num body = " + num33);
                        Char.myCharz().arrItemBody = new Item[num33];
                        for (int num34 = 0; num34 < num33; num34++)
                        {
                            short num35 = msg.reader().readShort();
                            if (num35 == -1)
                            {
                                continue;
                            }
                            Char.myCharz().arrItemBody[num34] = new Item();
                            Char.myCharz().arrItemBody[num34].template = ItemTemplates.get(num35);
                            int num36 = Char.myCharz().arrItemBody[num34].template.type;
                            Char.myCharz().arrItemBody[num34].quantity = msg.reader().readInt();
                            Char.myCharz().arrItemBody[num34].info = msg.reader().readUTF();
                            Char.myCharz().arrItemBody[num34].content = msg.reader().readUTF();
                            int num37 = msg.reader().readUnsignedByte();
                            if (num37 != 0)
                            {
                                Char.myCharz().arrItemBody[num34].itemOption = new ItemOption[num37];
                                for (int num38 = 0; num38 < Char.myCharz().arrItemBody[num34].itemOption.Length; num38++)
                                {
                                    int num39 = msg.reader().readUnsignedByte();
                                    int param2 = msg.reader().readUnsignedShort();
                                    if (num39 != -1)
                                    {
                                        Char.myCharz().arrItemBody[num34].itemOption[num38] = new ItemOption(num39, param2);
                                    }
                                }
                            }
                            switch (num36)
                            {
                                case 0:
                                    Char.myCharz().body = Char.myCharz().arrItemBody[num34].template.part;
                                    break;
                                case 1:
                                    Char.myCharz().leg = Char.myCharz().arrItemBody[num34].template.part;
                                    break;
                            }
                        }
                        break;
                    }
                case -36:
                    {
                        sbyte b7 = msg.reader().readByte();
                        Res.outz("cAction= " + b7);
                        if (b7 == 0)
                        {
                            int num11 = msg.reader().readUnsignedByte();
                            Char.myCharz().arrItemBag = new Item[num11];
                            GameScr.hpPotion = 0;
                            Res.outz("numC=" + num11);
                            for (int j = 0; j < num11; j++)
                            {
                                short num12 = msg.reader().readShort();
                                if (num12 == -1)
                                {
                                    continue;
                                }
                                Char.myCharz().arrItemBag[j] = new Item();
                                Char.myCharz().arrItemBag[j].template = ItemTemplates.get(num12);
                                Char.myCharz().arrItemBag[j].quantity = msg.reader().readInt();
                                Char.myCharz().arrItemBag[j].info = msg.reader().readUTF();
                                Char.myCharz().arrItemBag[j].content = msg.reader().readUTF();
                                Char.myCharz().arrItemBag[j].indexUI = j;
                                int num13 = msg.reader().readUnsignedByte();
                                if (num13 != 0)
                                {
                                    Char.myCharz().arrItemBag[j].itemOption = new ItemOption[num13];
                                    for (int k = 0; k < Char.myCharz().arrItemBag[j].itemOption.Length; k++)
                                    {
                                        int num14 = msg.reader().readUnsignedByte();
                                        int param = msg.reader().readUnsignedShort();
                                        if (num14 != -1)
                                        {
                                            Char.myCharz().arrItemBag[j].itemOption[k] = new ItemOption(num14, param);
                                        }
                                    }
                                    Char.myCharz().arrItemBag[j].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemBag[j]);
                                }
                                if (Char.myCharz().arrItemBag[j].template.type == 11)
                                {
                                }
                                if (Char.myCharz().arrItemBag[j].template.type == 6)
                                {
                                    GameScr.hpPotion += Char.myCharz().arrItemBag[j].quantity;
                                }
                            }
                        }
                        if (b7 == 2)
                        {
                            sbyte b8 = msg.reader().readByte();
                            int quantity = msg.reader().readInt();
                            int quantity2 = Char.myCharz().arrItemBag[b8].quantity;
                            Char.myCharz().arrItemBag[b8].quantity = quantity;
                            if (Char.myCharz().arrItemBag[b8].quantity < quantity2 && Char.myCharz().arrItemBag[b8].template.type == 6)
                            {
                                GameScr.hpPotion -= quantity2 - Char.myCharz().arrItemBag[b8].quantity;
                            }
                            if (Char.myCharz().arrItemBag[b8].quantity == 0)
                            {
                                Char.myCharz().arrItemBag[b8] = null;
                            }
                        }
                        break;
                    }
                case -35:
                    {
                        sbyte b63 = msg.reader().readByte();
                        Res.outz("cAction= " + b63);
                        if (b63 == 0)
                        {
                            int num154 = msg.reader().readUnsignedByte();
                            Char.myCharz().arrItemBox = new Item[num154];
                            GameCanvas.panel.hasUse = 0;
                            for (int num155 = 0; num155 < num154; num155++)
                            {
                                short num156 = msg.reader().readShort();
                                if (num156 == -1)
                                {
                                    continue;
                                }
                                Char.myCharz().arrItemBox[num155] = new Item();
                                Char.myCharz().arrItemBox[num155].template = ItemTemplates.get(num156);
                                Char.myCharz().arrItemBox[num155].quantity = msg.reader().readInt();
                                Char.myCharz().arrItemBox[num155].info = msg.reader().readUTF();
                                Char.myCharz().arrItemBox[num155].content = msg.reader().readUTF();
                                int num157 = msg.reader().readUnsignedByte();
                                if (num157 != 0)
                                {
                                    Char.myCharz().arrItemBox[num155].itemOption = new ItemOption[num157];
                                    for (int num158 = 0; num158 < Char.myCharz().arrItemBox[num155].itemOption.Length; num158++)
                                    {
                                        int num159 = msg.reader().readUnsignedByte();
                                        int param6 = msg.reader().readUnsignedShort();
                                        if (num159 != -1)
                                        {
                                            Char.myCharz().arrItemBox[num155].itemOption[num158] = new ItemOption(num159, param6);
                                        }
                                    }
                                }
                                GameCanvas.panel.hasUse++;
                            }
                        }
                        if (b63 == 1)
                        {
                            bool isBoxClan = false;
                            try
                            {
                                sbyte b64 = msg.reader().readByte();
                                if (b64 == 1)
                                {
                                    isBoxClan = true;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            GameCanvas.panel.setTypeBox();
                            GameCanvas.panel.isBoxClan = isBoxClan;
                            GameCanvas.panel.show();
                        }
                        if (b63 == 2)
                        {
                            sbyte b65 = msg.reader().readByte();
                            int quantity3 = msg.reader().readInt();
                            Char.myCharz().arrItemBox[b65].quantity = quantity3;
                            if (Char.myCharz().arrItemBox[b65].quantity == 0)
                            {
                                Char.myCharz().arrItemBox[b65] = null;
                            }
                        }
                        break;
                    }
                case -45:
                    {
                        sbyte type = msg.reader().readByte();
                        int playerId = msg.reader().readInt();
                        short skillId = msg.reader().readShort();
                        if (type == 20)
                        {
                            sbyte b50 = msg.reader().readByte();
                            sbyte dir = msg.reader().readByte();
                            short timeGong = msg.reader().readShort();
                            bool isFly = msg.reader().readByte() != 0;
                            sbyte typePaint = msg.reader().readByte();
                            sbyte typeItem = -1;
                            try
                            {
                                typeItem = msg.reader().readByte();
                            }
                            catch (Exception)
                            {
                            }
                            sbyte level = -1;
                            try
                            {
                                level = msg.reader().readByte();
                            }
                            catch (Exception)
                            {
                            }
                            @char = ((Char.myCharz().charID != playerId) ? GameScr.findCharInMap(playerId) : Char.myCharz());
                            @char.SetSkillPaint_NEW(skillId, isFly, b50, typePaint, dir, timeGong, typeItem, level);
                        }
                        if (type == 21)
                        {
                            Point point = new()
                            {
                                x = msg.reader().readShort(),
                                y = msg.reader().readShort()
                            };
                            short timeDame = msg.reader().readShort();
                            short rangeDame = msg.reader().readShort();
                            sbyte typePaint2 = 0;
                            sbyte typeItem2 = -1;
                            Point[] targets = null;
                            @char = (Char.myCharz().charID != playerId) ? GameScr.findCharInMap(playerId) : Char.myCharz();
                            try
                            {
                                typePaint2 = msg.reader().readByte();
                                sbyte targetSz = msg.reader().readByte();
                                targets = new Point[targetSz];
                                for (int i = 0; i < targets.Length; i++)
                                {
                                    targets[i] = new Point
                                    {
                                        type = msg.reader().readByte()
                                    };
                                    if (targets[i].type == 0)
                                    {
                                        targets[i].id = msg.reader().readByte();
                                    }
                                    else
                                    {
                                        targets[i].id = msg.reader().readInt();
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                typeItem2 = msg.reader().readByte();
                            }
                            catch (Exception)
                            {
                            }
                            sbyte level = -1;
                            try
                            {
                                level = msg.reader().readByte();
                            }
                            catch (Exception)
                            {
                            }
                            @char.SetSkillPaint_STT(1, skillId, point, timeDame, rangeDame, typePaint2, targets, typeItem2, level);
                        }
                        if (type == 0)
                        {
                            Res.outz("id use= " + playerId);
                            if (Char.myCharz().charID != playerId)
                            {
                                @char = GameScr.findCharInMap(playerId);
                                if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
                                {
                                    @char.setSkillPaint(GameScr.sks[skillId], 0);
                                }
                                else
                                {
                                    @char.setSkillPaint(GameScr.sks[skillId], 1);
                                    @char.delayFall = 20;
                                }
                            }
                            else
                            {
                                Char.myCharz().saveLoadPreviousSkill();
                                Res.outz("LOAD LAST SKILL");
                            }
                            sbyte b52 = msg.reader().readByte();
                            Res.outz("npc size= " + b52);
                            for (int num120 = 0; num120 < b52; num120++)
                            {
                                sbyte b53 = msg.reader().readByte();
                                sbyte b54 = msg.reader().readByte();
                                Res.outz("index= " + b53);
                                if (skillId >= 42 && skillId <= 48)
                                {
                                    ((Mob)GameScr.vMob.elementAt(b53)).isFreez = true;
                                    ((Mob)GameScr.vMob.elementAt(b53)).seconds = b54;
                                    ((Mob)GameScr.vMob.elementAt(b53)).last = (((Mob)GameScr.vMob.elementAt(b53)).cur = mSystem.currentTimeMillis());
                                }
                            }
                            sbyte b55 = msg.reader().readByte();
                            for (int num121 = 0; num121 < b55; num121++)
                            {
                                int num122 = msg.reader().readInt();
                                sbyte b56 = msg.reader().readByte();
                                Res.outz("player ID= " + num122 + " my ID= " + Char.myCharz().charID);
                                if (skillId < 42 || skillId > 48)
                                {
                                    continue;
                                }
                                if (num122 == Char.myCharz().charID)
                                {
                                    if (!Char.myCharz().isFlyAndCharge && !Char.myCharz().isStandAndCharge)
                                    {
                                        GameScr.gI().isFreez = true;
                                        Char.myCharz().isFreez = true;
                                        Char.myCharz().freezSeconds = b56;
                                        Char.myCharz().lastFreez = (Char.myCharz().currFreez = mSystem.currentTimeMillis());
                                        Char.myCharz().isLockMove = true;
                                    }
                                }
                                else
                                {
                                    @char = GameScr.findCharInMap(num122);
                                    if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
                                    {
                                        @char.isFreez = true;
                                        @char.seconds = b56;
                                        @char.freezSeconds = b56;
                                        @char.lastFreez = (GameScr.findCharInMap(num122).currFreez = mSystem.currentTimeMillis());
                                    }
                                }
                            }
                        }
                        if (type == 1 && playerId != Char.myCharz().charID)
                        {
                            GameScr.findCharInMap(playerId).isCharge = true;
                        }
                        if (type == 3)
                        {
                            if (playerId == Char.myCharz().charID)
                            {
                                Char.myCharz().isCharge = false;
                                SoundMn.gI().taitaoPause();
                                Char.myCharz().saveLoadPreviousSkill();
                            }
                            else
                            {
                                GameScr.findCharInMap(playerId).isCharge = false;
                            }
                        }
                        if (type == 4)
                        {
                            if (playerId == Char.myCharz().charID)
                            {
                                Char.myCharz().seconds = msg.reader().readShort() - 1000;
                                Char.myCharz().last = mSystem.currentTimeMillis();
                                Res.outz("second= " + Char.myCharz().seconds + " last= " + Char.myCharz().last);
                            }
                            else if (GameScr.findCharInMap(playerId) != null)
                            {
                                switch (GameScr.findCharInMap(playerId).cgender)
                                {
                                    case 0:
                                        GameScr.findCharInMap(playerId).useChargeSkill(isGround: false);
                                        break;
                                    case 1:
                                        GameScr.findCharInMap(playerId).useChargeSkill(isGround: true);
                                        break;
                                }
                                GameScr.findCharInMap(playerId).skillTemplateId = skillId;
                                GameScr.findCharInMap(playerId).isUseSkillAfterCharge = true;
                                GameScr.findCharInMap(playerId).seconds = msg.reader().readShort();
                                GameScr.findCharInMap(playerId).last = mSystem.currentTimeMillis();
                            }
                        }
                        if (type == 5)
                        {
                            if (playerId == Char.myCharz().charID)
                            {
                                Char.myCharz().stopUseChargeSkill();
                            }
                            else if (GameScr.findCharInMap(playerId) != null)
                            {
                                GameScr.findCharInMap(playerId).stopUseChargeSkill();
                            }
                        }
                        if (type == 6)
                        {
                            if (playerId == Char.myCharz().charID)
                            {
                                Char.myCharz().setAutoSkillPaint(GameScr.sks[skillId], 0);
                            }
                            else if (GameScr.findCharInMap(playerId) != null)
                            {
                                GameScr.findCharInMap(playerId).setAutoSkillPaint(GameScr.sks[skillId], 0);
                                SoundMn.gI().gong();
                            }
                        }
                        if (type == 7)
                        {
                            if (playerId == Char.myCharz().charID)
                            {
                                Char.myCharz().seconds = msg.reader().readShort();
                                Res.outz("second = " + Char.myCharz().seconds);
                                Char.myCharz().last = mSystem.currentTimeMillis();
                            }
                            else if (GameScr.findCharInMap(playerId) != null)
                            {
                                GameScr.findCharInMap(playerId).useChargeSkill(isGround: true);
                                GameScr.findCharInMap(playerId).seconds = msg.reader().readShort();
                                GameScr.findCharInMap(playerId).last = mSystem.currentTimeMillis();
                                SoundMn.gI().gong();
                            }
                        }
                        if (type == 8 && playerId != Char.myCharz().charID && GameScr.findCharInMap(playerId) != null)
                        {
                            GameScr.findCharInMap(playerId).setAutoSkillPaint(GameScr.sks[skillId], 0);
                        }
                        break;
                    }
                case -44:
                    {
                        bool flag5 = false;
                        if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
                        {
                            flag5 = true;
                        }
                        sbyte type_shop = msg.reader().readByte();
                        int tabSz = msg.reader().readUnsignedByte();
                        Char.myCharz().arrItemShop = new Item[tabSz][];
                        GameCanvas.panel.shopTabName = new string[tabSz + ((!flag5) ? 1 : 0)][];
                        for (int num58 = 0; num58 < GameCanvas.panel.shopTabName.Length; num58++)
                        {
                            GameCanvas.panel.shopTabName[num58] = new string[2];
                        }
                        if (type_shop == 2)
                        {
                            GameCanvas.panel.maxPageShop = new int[tabSz];
                            GameCanvas.panel.currPageShop = new int[tabSz];
                        }
                        if (!flag5)
                        {
                            GameCanvas.panel.shopTabName[tabSz] = mResources.inventory;
                        }
                        for (int i = 0; i < tabSz; i++)
                        {
                            string[] name = Res.split(msg.reader().readUTF(), "\n", 0);
                            if (type_shop == 2)
                            {
                                GameCanvas.panel.maxPageShop[i] = msg.reader().readUnsignedByte();
                            }
                            if (name.Length == 2)
                            {
                                GameCanvas.panel.shopTabName[i] = name;
                            }
                            if (name.Length == 1)
                            {
                                GameCanvas.panel.shopTabName[i][0] = name[0];
                                GameCanvas.panel.shopTabName[i][1] = string.Empty;
                            }
                            int itemSz = msg.reader().readUnsignedByte();
                            Char.myCharz().arrItemShop[i] = new Item[itemSz];
                            Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
                            if (type_shop == 1)
                            {
                                Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
                            }
                            for (int num61 = 0; num61 < itemSz; num61++)
                            {
                                short itemId = msg.reader().readShort();
                                if (itemId == -1)
                                {
                                    continue;
                                }
                                Char.myCharz().arrItemShop[i][num61] = new Item();
                                Char.myCharz().arrItemShop[i][num61].template = ItemTemplates.get(itemId);
                                Res.outz("name " + i + " = " + Char.myCharz().arrItemShop[i][num61].template.name + " id templat= " + Char.myCharz().arrItemShop[i][num61].template.id);
                                if (type_shop == 8)
                                {
                                    Char.myCharz().arrItemShop[i][num61].buyCoin = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].buyGold = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].quantity = msg.reader().readInt();
                                }
                                else if (type_shop == 4) // Reward
                                {
                                    Char.myCharz().arrItemShop[i][num61].reason = msg.reader().readUTF();
                                }
                                else if (type_shop == 0) // Normal
                                {
                                    Char.myCharz().arrItemShop[i][num61].buyCoin = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].buyGold = msg.reader().readInt();

                                }
                                else if (type_shop == 1)
                                {
                                    Char.myCharz().arrItemShop[i][num61].powerRequire = msg.reader().readLong();
                                }
                                else if (type_shop == 2) // Ky gui
                                {
                                    Char.myCharz().arrItemShop[i][num61].itemId = msg.reader().readShort();
                                    Char.myCharz().arrItemShop[i][num61].buyCoin = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].buyGold = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].buyType = msg.reader().readByte();
                                    Char.myCharz().arrItemShop[i][num61].quantity = msg.reader().readInt();
                                    Char.myCharz().arrItemShop[i][num61].isMe = msg.reader().readByte();
                                }
                                else if (type_shop == 3) // Special
                                {
                                    Char.myCharz().arrItemShop[i][num61].isBuySpec = true;
                                    Char.myCharz().arrItemShop[i][num61].iconSpec = msg.reader().readShort();
                                    Char.myCharz().arrItemShop[i][num61].buySpec = msg.reader().readInt();
                                }
                                int optSz = msg.reader().readUnsignedByte();
                                if (optSz != 0)
                                {
                                    Char.myCharz().arrItemShop[i][num61].itemOption = new ItemOption[optSz];
                                    for (int j = 0; j < Char.myCharz().arrItemShop[i][num61].itemOption.Length; j++)
                                    {
                                        int optId = msg.reader().readUnsignedByte();
                                        int param = msg.reader().readUnsignedShort();
                                        if (optId != -1)
                                        {
                                            Char.myCharz().arrItemShop[i][num61].itemOption[j] = new ItemOption(optId, param);
                                            Char.myCharz().arrItemShop[i][num61].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[i][num61]);
                                        }
                                    }
                                }
                                sbyte isNew = msg.reader().readByte();
                                Char.myCharz().arrItemShop[i][num61].newItem = isNew != 0;
                                sbyte isCT = msg.reader().readByte();
                                if (isCT == 1)
                                {
                                    int headTemp = msg.reader().readShort();
                                    int bodyTemp = msg.reader().readShort();
                                    int legTemp = msg.reader().readShort();
                                    int bagTemp = msg.reader().readShort();
                                    Char.myCharz().arrItemShop[i][num61].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
                                }
                            }
                        }
                        if (flag5)
                        {
                            if (type_shop != 2)
                            {
                                GameCanvas.panel2 = new Panel();
                                GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
                                GameCanvas.panel2.setTypeBodyOnly();
                                GameCanvas.panel2.show();
                            }
                            else
                            {
                                GameCanvas.panel2 = new Panel();
                                GameCanvas.panel2.setTypeKiGuiOnly();
                                GameCanvas.panel2.show();
                            }
                        }
                        GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
                        if (type_shop == 2)
                        {
                            string[][] array5 = GameCanvas.panel.tabName[1];
                            if (flag5)
                            {
                                GameCanvas.panel.tabName[1] = new string[4][]
                                {
                            array5[0],
                            array5[1],
                            array5[2],
                            array5[3]
                                };
                            }
                            else
                            {
                                GameCanvas.panel.tabName[1] = new string[5][]
                                {
                            array5[0],
                            array5[1],
                            array5[2],
                            array5[3],
                            array5[4]
                                };
                            }
                        }
                        GameCanvas.panel.setTypeShop(type_shop);
                        GameCanvas.panel.show();
                        break;
                    }
        }
    }
}
