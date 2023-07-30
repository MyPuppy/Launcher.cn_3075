﻿using KartRider.Common.Network;
using KartRider.Common.Utilities;
using KartRider.IO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using KartRider_PacketName;
using KartRider_TrackName;
using KartRider_SN;
using ExcData;
using Set_Data;
using RiderData;

namespace KartRider
{
	public class ClientSession : Session
	{
		public SessionGroup Parent
		{
			get;
			set;
		}

		public ClientSession(SessionGroup parent, System.Net.Sockets.Socket socket) : base(socket)
		{
			this.Parent = parent;
		}

		public override void OnDisconnect()
		{
            this.Parent.Client.Disconnect();
        }

		public override void OnPacket(InPacket iPacket)
        {
            int ALLnum;
            lock (this.Parent.m_lock)
			{
				iPacket.Position = 0;
				uint hash = iPacket.ReadUInt();
                if ((hash == Adler32Helper.GenerateAdler32(Encoding.ASCII.GetBytes("PcReportRaidOccur"), 0) ? false : hash != 1340475309))
				{
                    if (hash == Adler32Helper.GenerateAdler32_ASCII("GrRiderTalkPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqEnterMagicHatPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("LoPingRequestPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRiderQuestUX2ndData", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqAddTimeEventInitPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqCountdownBoxPeriodPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqServerSideUdpBindCheck", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqVipGradeCheck", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("SpRqGetMaxGiftIdPacket", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqUpdateRiderSchoolDataPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("RmRiderTalkPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqNeedTimerGiftEvent", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PcReportStateInGame", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("GameBoosterAddPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("LoRqCheckReplayItemPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRecommandChatServerInfo", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("LoCheckLoginEvent", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqBlockWordLogPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqWriteActionLogPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqMissionAttendPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqEnterShopPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqAddTimeEventTimerPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqTimeShopOpenTimePacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqItemPresetSlotDataList", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("VipPlaytimeCheck", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("LoRqEventRewardPacket", 0))
                    {
                        //PqGetRecommandChatServerInfo = 라이더 챗
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqAddRacingTimePacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("LoRpAddRacingTimePacket"))
                        {
                            outPacket.WriteHexString("FF FF FF FF 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqUploadFilePacket", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqStartSinglePacket", 0))
                    {
                        int TimeAttackStartTicks = iPacket.ReadInt();
                        this.Parent.TimeAttackStartTicks = TimeAttackStartTicks;
                        this.Parent.PlaneCheck1 = (byte)this.Parent.TimeAttackStartTicks;
                        uint key = CryptoConstants.GetKey(CryptoConstants.GetKey((uint)this.Parent.TimeAttackStartTicks)) % 5 + 6;
                        ALLnum = (int)key;
                        this.Parent.SendPlaneCount = (int)key;
                        this.Parent.TotalSendPlaneCount = 0;
                        Console.WriteLine("PlaneCheckMax: {0}", this.Parent.SendPlaneCount);
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("GameSlotPacket", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("GameReportPacket", 0))
                    {
                        byte[] DateTime1 = iPacket.ReadBytes(18);
                        int GetItem = iPacket.ReadInt();
                        int UseItem = iPacket.ReadInt();
                        int UseBooster = iPacket.ReadEncodedInt();
                        int[][] hashArray1 = new int[20][];
                        for (int k = 0; k < 20; k++)
                        {
                            hashArray1[k] = new int[3];
                            hashArray1[k][0] = iPacket.ReadEncodedInt();
                            hashArray1[k][1] = iPacket.ReadEncodedInt();
                            hashArray1[k][2] = iPacket.ReadEncodedInt();
                        }
                        int hash1 = iPacket.ReadEncodedInt();
                        int hash2 = iPacket.ReadEncodedInt();
                        int hash3 = iPacket.ReadEncodedInt();
                        float single1 = iPacket.ReadEncodedFloat();
                        float single2 = iPacket.ReadEncodedFloat();
                        float single3 = iPacket.ReadEncodedFloat();
                        int PlaneCheck = iPacket.ReadInt();
                        byte[] hashArray2 = iPacket.ReadBytes(20);
                        int hash4 = iPacket.ReadInt();
                        byte[] hashArray3 = iPacket.ReadBytes(16);
                        this.Parent.TotalSendPlaneCount += PlaneCheck;
                        Console.WriteLine("PlaneCheck: {0}, Total: {1}, Max: {2}, Dist: {3}", PlaneCheck, this.Parent.TotalSendPlaneCount, this.Parent.SendPlaneCount, single3);
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqRenameRidPacket", 0))
                    {
                        SetRider.Nickname = iPacket.ReadString(false);
                        using (OutPacket outPacket = new OutPacket("SpRpRenameRidPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        SetGameData.Save_Nickname();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRider", 0))
                    {
                        NewRider.LoadItemData();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqGetRiderItemPacket", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqSetRiderItemOnPacket", 0))
                    {
                        SetRiderItem.Set_Character = iPacket.ReadShort();
                        SetRiderItem.Set_Paint = iPacket.ReadShort();
                        SetRiderItem.Set_Kart = iPacket.ReadShort();
                        SetRiderItem.Set_Plate = iPacket.ReadShort();
                        SetRiderItem.Set_Goggle = iPacket.ReadShort();
                        SetRiderItem.Set_Balloon = iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_HeadBand = iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_HandGearL = iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_Uniform = iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_Pet = iPacket.ReadShort();
                        SetRiderItem.Set_FlyingPet = iPacket.ReadShort();
                        SetRiderItem.Set_Aura = iPacket.ReadShort();
                        SetRiderItem.Set_SkidMark = iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_RidColor = iPacket.ReadShort();
                        SetRiderItem.Set_BonusCard = iPacket.ReadShort();
                        iPacket.ReadShort();
                        short Set_KartPlant1 = iPacket.ReadShort();
                        short Set_KartPlant2 = iPacket.ReadShort();
                        short Set_KartPlant3 = iPacket.ReadShort();
                        short Set_KartPlant4 = iPacket.ReadShort();
                        iPacket.ReadShort();
                        iPacket.ReadShort();
                        SetRiderItem.Set_Tachometer = iPacket.ReadShort();
                        SetRiderItem.Set_Dye = iPacket.ReadShort();
                        SetRiderItem.Set_KartSN = iPacket.ReadShort();
                        short Set_KartEffect = iPacket.ReadShort();
                        short Set_KartBoosterEffect = iPacket.ReadShort();
                        Launcher.KartSN = SetRiderItem.Set_KartSN;
                        SetRiderItem.Save_SetRiderItem();
                        KartSN_Parts.KartSN_Data();
                        TuneSpec.Use_TuneSpec();
                        TuneSpec.Use_PlantSpec();
                        TuneSpec.Use_KartLevelSpec();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRiderInfo", 0))
                    {
                        iPacket.ReadInt();
                        iPacket.ReadInt();
                        string Nickname = iPacket.ReadString(false);
                        if (Nickname == SetRider.Nickname)
                        {
                            GameSupport.PrGetRiderInfo();
                        }
                        else
                        {
                            using (OutPacket outPacket = new OutPacket("PrGetRiderInfo"))
                            {
                                outPacket.WriteByte(0);
                                this.Parent.Client.Send(outPacket);
                            }
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUpdateRiderIntro", 0))
                    {
                        SetRider.RiderIntro = iPacket.ReadString(false);
                        SetGameData.Save_RiderIntro();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUpdateRiderSchoolLevelPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrUpdateRiderSchoolLevelPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSetPlaytimeEventTick", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSetPlaytimeEventTick"))
                        {
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUpdateGameOption", 0))
                    {
                        SetGameOption.Set_BGM = iPacket.ReadFloat();
                        SetGameOption.Set_Sound = iPacket.ReadFloat();
                        SetGameOption.Main_BGM = iPacket.ReadByte();
                        SetGameOption.Sound_effect = iPacket.ReadByte();
                        SetGameOption.Full_screen = iPacket.ReadByte();
                        SetGameOption.Unk1 = iPacket.ReadByte();
                        SetGameOption.Unk2 = iPacket.ReadByte();
                        SetGameOption.Unk3 = iPacket.ReadByte();
                        SetGameOption.Unk4 = iPacket.ReadByte();
                        SetGameOption.Unk5 = iPacket.ReadByte();
                        SetGameOption.Unk6 = iPacket.ReadByte();
                        SetGameOption.Unk7 = iPacket.ReadByte();
                        SetGameOption.Unk8 = iPacket.ReadByte();//오토 레디
                        SetGameOption.Unk9 = iPacket.ReadByte();//아이템 설명
                        SetGameOption.Unk10 = iPacket.ReadByte();//녹화 품질
                        SetGameOption.Unk11 = iPacket.ReadByte();
                        SetGameOption.BGM_Check = iPacket.ReadByte();//배경음
                        SetGameOption.Sound_Check = iPacket.ReadByte();//효과음
                        SetGameOption.Unk12 = iPacket.ReadByte();
                        SetGameOption.Unk13 = iPacket.ReadByte();
                        SetGameOption.GameType = iPacket.ReadByte();//팀전 개인전 여부
                        SetGameOption.SetGhost = iPacket.ReadByte();//고스트 사용여부
                        SetGameOption.SpeedType = iPacket.ReadByte();//채널 속도
                        SetGameOption.Unk14 = iPacket.ReadByte();
                        SetGameOption.Unk15 = iPacket.ReadByte();
                        SetGameOption.Unk16 = iPacket.ReadByte();
                        SetGameOption.Unk17 = iPacket.ReadByte();
                        SetGameOption.Set_screen = iPacket.ReadByte();
                        SetGameOption.Unk18 = iPacket.ReadByte();
                        SetGameOption.Save_SetGameOption();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetGameOption", 0))
                    {
                        GameSupport.PrGetGameOption();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqVipInfo", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrVipInfo"))
                        {
                            outPacket.WriteInt(1);
                            for (int i = 0; i < 10; i++)
                            {
                                outPacket.WriteInt(0);
                            }
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqLoginVipInfo", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrLoginVipInfo"))
                        {
                            outPacket.WriteInt(SetRider.Premium);
                            outPacket.WriteByte(1);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        using (OutPacket outPacket = new OutPacket("LoRpEventRewardPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("ChReRqEnterMyRoomPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("ChRqEnterRandomMyRoomPacket", 0))
                    {
                        if (hash == 1733888222)//ChReRqEnterMyRoomPacket
                        {
                            GameType.EnterMyRoomType = 0;
                        }
                        else if (hash == 2423851656)//ChRqEnterRandomMyRoomPacket
                        {
                            GameType.EnterMyRoomType = 5;
                        }
                        GameSupport.ChRpEnterMyRoomPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("ChRqEnterMyRoomPacket", 0))
                    {
                        string Nickname = iPacket.ReadString(false);
                        if (Nickname == SetRider.Nickname)
                        {
                            GameType.EnterMyRoomType = 0;
                        }
                        else
                        {
                            GameType.EnterMyRoomType = 3;
                        }
                        GameSupport.ChRpEnterMyRoomPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("RmFirstRequestPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("RmSlotDataPacket"))
                        {
                            outPacket.WriteUInt(SetRider.UserNO);
                            outPacket.WriteBytes(new byte[12]);
                            outPacket.WriteString(SetRider.Nickname);
                            outPacket.WriteBytes(new byte[65]);
                            outPacket.WriteInt(SetRider.RP);
                            outPacket.WriteBytes(new byte[910]);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("RmNotiMyRoomInfoPacket", 0))
                    {
                        SetMyRoom.MyRoom = iPacket.ReadShort();
                        SetMyRoom.MyRoomBGM = iPacket.ReadByte();
                        SetMyRoom.UseRoomPwd = iPacket.ReadByte();
                        iPacket.ReadByte();
                        SetMyRoom.UseItemPwd = iPacket.ReadByte();
                        SetMyRoom.TalkLock = iPacket.ReadByte();
                        SetMyRoom.RoomPwd = iPacket.ReadString(false);
                        iPacket.ReadString(false);
                        SetMyRoom.ItemPwd = iPacket.ReadString(false);
                        SetMyRoom.MyRoomKart1 = iPacket.ReadShort();
                        SetMyRoom.MyRoomKart2 = iPacket.ReadShort();
                        SetMyRoom.Save_SetMyRoom();
                        GameSupport.RmNotiMyRoomInfoPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("ChRqSecedeMyRoomPacket", 0))
                    {
                        //마이룸 나갈때
                        using (OutPacket outPacket = new OutPacket("ChRpSecedeMyRoomPacket"))
                        {
                            outPacket.WriteByte(1);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqStartScenario", 0))
                    {
                        GameType.ScenarioType = iPacket.ReadInt();
                        using (OutPacket outPacket = new OutPacket("PrStartScenario"))
                        {
                            outPacket.WriteInt(GameType.ScenarioType);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCompleteScenarioSingle", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrCompleteScenarioSingle"))
                        {
                            outPacket.WriteInt(GameType.ScenarioType);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqKartSpec", 0))
                    {
                        StartGameData.StartTimeAttack_SpeedType = iPacket.ReadByte();
                        StartGameData.Kart_id = iPacket.ReadShort();
                        StartGameData.FlyingPet_id = iPacket.ReadShort();
                        GameType.StartType = 1;
                        SpeedType.SpeedTypeData();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqChapterInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrChapterInfoPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqChallengerInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrChallengerInfoPacket"))
                        {
                            int stage = 40;
                            outPacket.WriteInt(stage);
                            for (int i = 0; i < stage; i++)
                            {
                                outPacket.WriteShort(55);
                            }
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(1);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqStartChallenger", 0))
                    {
                        int stage_id = iPacket.ReadInt();
                        int GameType = iPacket.ReadInt();
                        short Kart = iPacket.ReadShort();
                        byte Unk1 = iPacket.ReadByte();
                        byte Unk2 = iPacket.ReadByte();
                        byte Unk3 = iPacket.ReadByte();
                        using (OutPacket outPacket = new OutPacket("PrStartChallenger"))
                        {
                            outPacket.WriteInt(stage_id);
                            outPacket.WriteInt(GameType);
                            outPacket.WriteByte(0);
                            outPacket.WriteByte(1);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqchallengerKartSpec", 0))
                    {
                        StartGameData.StartTimeAttack_SpeedType = iPacket.ReadByte();
                        StartGameData.Kart_id = iPacket.ReadShort();
                        StartGameData.FlyingPet_id = iPacket.ReadShort();
                        GameType.StartType = 2;
                        SpeedType.SpeedTypeData();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCompleteChallenger", 0))
                    {
                        int stage = 40;
                        byte StageType = iPacket.ReadByte();
                        iPacket.ReadInt();
                        int EndType = iPacket.ReadInt();
                        using (OutPacket outPacket = new OutPacket("PrCompleteChallenger"))
                        {
                            outPacket.WriteByte(StageType);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(EndType);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(stage);
                            for (int i = 0; i < stage; i++)
                            {
                                outPacket.WriteShort(55);
                            }
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqDisassembleXPartsItem", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqExChangePacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqKartLevelUpProbText", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("PqKartLevelPointClear", 0))
                    {
                        GameSupport.OnDisconnect();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqKartLevelSpecialSlotUpdate", 0))
                    {
                        short Kart = iPacket.ReadShort();
                        short SN = iPacket.ReadShort();
                        short Effect = iPacket.ReadShort();
                        using (OutPacket outPacket = new OutPacket("PrKartLevelSpecialSlotUpdate"))
                        {
                            outPacket.WriteInt(1);
                            outPacket.WriteShort(Kart);
                            outPacket.WriteShort(SN);
                            outPacket.WriteShort(5);
                            outPacket.WriteShort(0);
                            outPacket.WriteShort(10);
                            outPacket.WriteShort(5);
                            outPacket.WriteShort(10);
                            outPacket.WriteShort(10);
                            outPacket.WriteShort(Effect);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUseSocketItem", 0))
                    {
                        short Item = iPacket.ReadShort();
                        short Item_Id = iPacket.ReadShort();
                        short Kart = iPacket.ReadShort();
                        iPacket.ReadShort();
                        short KartSN = iPacket.ReadShort();
                        using (OutPacket outPacket = new OutPacket("PrUseSocketItem"))
                        {
                            outPacket.WriteInt(1);
                            outPacket.WriteShort(Item);
                            outPacket.WriteShort(Item_Id);
                            outPacket.WriteShort(Kart);
                            outPacket.WriteShort(1);
                            outPacket.WriteShort(KartSN);
                            outPacket.WriteShort(KartSN);
                            outPacket.WriteHexString("00 00 00 00 FF FF 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUnequipXPartsItem", 0))
                    {
                        short Kart = iPacket.ReadShort();
                        short SN = iPacket.ReadShort();
                        short item = iPacket.ReadShort();
                        using (OutPacket outPacket = new OutPacket("PrUnequipXPartsItem"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteShort(Kart);
                            outPacket.WriteShort(SN);
                            outPacket.WriteShort(item);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqEquipXPartsItem", 0))
                    {
                        short Kart = iPacket.ReadShort();
                        Launcher.KartSN = iPacket.ReadShort();
                        PartSpec.Item_Cat_Id = iPacket.ReadShort();
                        PartSpec.Item_Id = iPacket.ReadShort();
                        short Quantity = iPacket.ReadShort();
                        short Unk1 = iPacket.ReadShort();
                        PartSpec.Grade = iPacket.ReadByte();
                        byte Unk2 = iPacket.ReadByte();
                        PartSpec.PartsValue = iPacket.ReadShort();
                        short Unk3 = iPacket.ReadShort();
                        using (OutPacket outPacket = new OutPacket("PrEquipXPartsItem"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteShort(Kart);
                            outPacket.WriteShort(Launcher.KartSN);
                            outPacket.WriteShort(PartSpec.Item_Cat_Id);
                            outPacket.WriteShort(PartSpec.Item_Id);
                            outPacket.WriteShort(Quantity);
                            outPacket.WriteShort(Unk1);
                            outPacket.WriteByte(PartSpec.Grade);
                            outPacket.WriteByte(Unk2);
                            outPacket.WriteShort(PartSpec.PartsValue);
                            outPacket.WriteShort(Unk3);
                            this.Parent.Client.Send(outPacket);
                        }
                        if (PartSpec.PartsValue != 0)
                        {
                            Console.WriteLine("ClientSession : Kart: {0}, KartSN: {1}, Item: {2}:{3}, Quantity: {4}, Grade: {5}, PartsValue: {6}", Kart, Launcher.KartSN, PartSpec.Item_Cat_Id, PartSpec.Item_Id, Quantity, PartSpec.Grade, PartSpec.PartsValue);
                            PartSpec.PartSpecData();
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetTrainingMission", 0))
                    {
                        int type = iPacket.ReadInt();
                        uint track = iPacket.ReadUInt();
                        //PrGetTrainingMission 00 08 B7 51 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
                        using (OutPacket outPacket = new OutPacket("PrGetTrainingMission"))
                        {
                            outPacket.WriteInt(type);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetDuelMissionBulk", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetDuelMissionBulk"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteHexString(Program.DataTime);
                            outPacket.WriteHexString("0F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqAdjustDuelMissionDifficulty", 0))
                    {
                        Console.WriteLine("PqAdjustDuelMissionDifficulty: {0}", iPacket);
                        int type = iPacket.ReadInt();
                        int unk = iPacket.ReadInt();
                        using (OutPacket outPacket = new OutPacket("PrAdjustDuelMissionDifficulty"))
                        {
                            outPacket.WriteInt(type);
                            outPacket.WriteInt(unk);
                            outPacket.WriteInt(0);
                            outPacket.WriteShort(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqBlueMarble", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrBlueMarble"))
                        {
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqGetTrackRankPacket", 0))
                    {
                        uint track = iPacket.ReadUInt();
                        byte SpeedType = iPacket.ReadByte();
                        byte GameType = iPacket.ReadByte();
                        using (OutPacket outPacket = new OutPacket("LoRpGetTrackRankPacket"))
                        {
                            outPacket.WriteUInt(track);
                            outPacket.WriteByte(SpeedType);
                            outPacket.WriteByte(GameType);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqFavoriteTrackUpdate", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqDecLucciPacket", 0))
                    {
                        iPacket.ReadByte();
                        uint Lucci = iPacket.ReadUInt();
                        SetRider.Lucci -= Lucci;
                        SetGameData.Save_TimeAttackDecLucci();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqStartTimeAttack", 0))
                    {
                        StartGameData.StartTimeAttack_Unk1 = iPacket.ReadInt();
                        StartGameData.StartTimeAttack_Unk2 = iPacket.ReadInt();
                        StartGameData.StartTimeAttack_Track = iPacket.ReadUInt();
                        StartGameData.StartTimeAttack_SpeedType = iPacket.ReadByte();
                        StartGameData.StartTimeAttack_GameType = iPacket.ReadByte();
                        StartGameData.Kart_id = iPacket.ReadShort();
                        StartGameData.FlyingPet_id = iPacket.ReadShort();
                        StartGameData.StartTimeAttack_StartType = iPacket.ReadByte();
                        StartGameData.StartTimeAttack_Unk3 = iPacket.ReadInt();
                        StartGameData.StartTimeAttack_Unk4 = iPacket.ReadInt();
                        StartGameData.StartTimeAttack_Unk5 = iPacket.ReadByte();
                        StartGameData.StartTimeAttack_RankingTimaAttackType = iPacket.ReadByte();
                        StartGameData.StartTimeAttack_TimaAttackMpdeType = iPacket.ReadByte();
                        StartGameData.StartTimeAttack_TimaAttackMpde = iPacket.ReadInt();
                        StartGameData.StartTimeAttack_RandomTrackGameType = iPacket.ReadByte();
                        TrackName trackName = (TrackName)StartGameData.StartTimeAttack_Track;
                        if (StartGameData.StartTimeAttack_TimaAttackMpdeType == 1)
                        {
                            SetRider.Lucci -= 1000;
                            SetGameData.Save_TimeAttackDecLucci();
                        }
                        Console.WriteLine("StartTimeAttack: {0} / {1} / {2} / {3} / {4} / {5} / {6} / {7}", StartGameData.StartTimeAttack_SpeedType, StartGameData.StartTimeAttack_GameType, StartGameData.Kart_id, StartGameData.FlyingPet_id, trackName, StartGameData.StartTimeAttack_StartType, StartGameData.StartTimeAttack_RankingTimaAttackType, StartGameData.StartTimeAttack_TimaAttackMpdeType);
                        GameType.StartType = 3;
                        RandomTrack.SetGameType();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqFinishTimeAttack", 0))
                    {
                        int type = iPacket.ReadInt();
                        iPacket.ReadInt();
                        GameType.RewardType = iPacket.ReadByte();
                        iPacket.ReadInt();
                        iPacket.ReadInt();
                        iPacket.ReadInt();
                        iPacket.ReadInt();
                        int Time = iPacket.ReadInt();
                        GameType.min = Time / 60000;
                        int sec = Time - GameType.min * 60000;
                        GameType.sec = sec / 1000;
                        GameType.mil = Time % 1000;
                        if (GameType.RewardType == 0)
                        {
                            GameType.TimeAttack_RP = 10;
                            GameType.TimeAttack_Lucci = 20;
                        }
                        else if (GameType.RewardType == 1)
                        {
                            GameType.TimeAttack_RP = 20;
                            GameType.TimeAttack_Lucci = 50;
                        }
                        SetRider.RP += GameType.TimeAttack_RP;
                        SetRider.Lucci += GameType.TimeAttack_Lucci;
                        TrackName trackName = (TrackName)StartGameData.StartTimeAttack_Track;
                        Console.WriteLine("FinishTimeAttack: {0} / {1} / {2} / {3} / {4}:{5}:{6}", GameType.RewardType, GameType.TimeAttack_RP, GameType.TimeAttack_Lucci, trackName, GameType.min, GameType.sec, GameType.mil);
                        using (OutPacket outPacket = new OutPacket("PrFinishTimeAttack"))
                        {
                            outPacket.WriteInt(type);
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00");
                            outPacket.WriteInt(GameType.TimeAttack_RP);//RP
                            outPacket.WriteUInt(GameType.TimeAttack_Lucci);//LUCCI
                            this.Parent.Client.Send(outPacket);
                        }
                        SetGameData.Save_RewardTimeAttack();
                        SetGameData.Save_RecordTimeAttack();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRewardTimeAttack", 0))
                    {
                        byte RewardType = iPacket.ReadByte();
                        int RP = iPacket.ReadInt();
                        int Lucci = iPacket.ReadInt();
                        int TimeAttack_StartTicks = iPacket.ReadInt();
                        uint Track = iPacket.ReadUInt();
                        TrackName trackName = (TrackName)Track;
                        Console.WriteLine("RewardTimeAttack : ResultType: {0}, RP: {1}, Lucci: {2}, Track: {3}", RewardType, RP, Lucci, trackName);
                        if (RewardType == 0)
                        {
                            SetRider.RP += 10;
                            SetRider.Lucci += 20;
                        }
                        else if (RewardType == 1)
                        {
                            SetRider.RP += 20;
                            SetRider.Lucci += 50;
                        }
                        SetGameData.Save_RewardTimeAttack();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqUseItemPacket", 0))
                    {
                        short ItemType = iPacket.ReadShort();
                        short Type = iPacket.ReadShort();
                        SetRider.SlotChanger = iPacket.ReadShort();
                        if (Type == 1)
                        {
                            SetGameData.Save_SlotChanger();
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqQuestUX2ndPacket", 0))
                    {
                        GameSupport.PrQuestUX2ndPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGameOutRunUX2ndClearPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGameOutRunUX2ndClearPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetTrainingMissionReward", 0))
                    {
                        Console.WriteLine("PqGetTrainingMissionReward: {0}", iPacket);
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqBingoGachaPacket", 0))
                    {
                        int BingoType = iPacket.ReadInt();
                        if (BingoType == 0)
                        {
                            using (OutPacket outPacket = new OutPacket("SpRpBingoGachaPacket"))
                            {
                                outPacket.WriteInt(BingoType);
                                for (int i = 0; i < 5; i++)
                                {
                                    outPacket.WriteInt(0);
                                }
                                outPacket.WriteByte(0);
                                outPacket.WriteByte(0);
                                outPacket.WriteByte(0);
                                this.Parent.Client.Send(outPacket);
                            }
                        }
                        else if (BingoType == 4)
                        {
                            using (OutPacket outPacket = new OutPacket("SpRpBingoGachaPacket"))
                            {
                                outPacket.WriteInt(BingoType);
                                for (int i = 0; i < 5; i++)
                                {
                                    outPacket.WriteInt(0);
                                }
                                outPacket.WriteByte(0);
                                outPacket.WriteByte(0);
                                outPacket.WriteByte(0);
                                this.Parent.Client.Send(outPacket);
                            }
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCheckMyClubStatePacket", 0))
                    {
                        GameSupport.PrCheckMyClubStatePacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCheckMyLeaveDatePacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrCheckMyLeaveDatePacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetUserWaitingJoinClubPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetUserWaitingJoinClubPacket"))
                        {
                            outPacket.WriteInt(1);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCheckCreateClubConditionPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrCheckCreateClubConditionPacket"))
                        {
                            outPacket.WriteInt(3);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqClubChannelSwitch", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrInitClubPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqInitClubInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrInitClubInfoPacket"))
                        {
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 04 00 00 FE 02 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSearchClubListPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSearchClubListPacket"))
                        {
                            outPacket.WriteHexString("0C 00 00 00 7F 3D 00 00 04 00 00 00 1C 75 C7 8F 1D 52 4B 60 05 F4 01 00 00 FF FF FF FF 00 00 00 00 40 4B 4C 00 07 00 00 00 00 00 00 00 07 00 00 00 0B 4E E8 96 56 00 52 00 61 00 69 00 6E 00 64 00 00 00 E7 AA 50 46 0F 00 00 00 A1 6C C0 4E 48 4E 79 72 7F 95 2C 00 31 5C 2F 66 65 55 FD 90 1A 4F B9 70 2E 00 2E 00 2E 00 00 05 C4 D6 6E 01 5D 37 00 00 0A 00 00 00 D9 88 37 00 39 00 30 00 30 00 33 00 33 00 36 00 31 00 37 00 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 1C 01 00 00 00 00 00 00 06 00 00 00 13 4E 1A 4E 37 52 5A 80 50 4E E8 90 C3 00 00 00 A3 AA B7 2A 24 00 00 00 D9 88 3A 00 37 00 39 00 30 00 30 00 33 00 33 00 36 00 31 00 37 00 20 00 5C 00 5C 00 20 00 66 8F 1F 96 E0 65 61 67 F6 4E 33 75 F7 8B 2C 00 7F 95 1F 67 2A 67 0A 4E BF 7E EF 53 FD 80 1A 4F AB 88 FB 79 FA 51 4D 4F 6E 7F 01 04 8F 1C 4E 00 04 00 00 00 07 00 00 00 59 00 78 00 36 4E 54 00 65 00 61 00 6D 00 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 92 01 00 00 00 00 00 00 05 00 00 00 59 00 78 00 36 4E 1F 96 7F 95 27 00 00 00 33 AA 99 2A 46 00 00 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 28 4E E0 65 50 96 66 8F 1F 96 28 4E 20 00 66 8F 1F 96 03 80 38 68 36 65 BA 4E 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 A5 63 85 5F 2F 00 03 80 38 68 71 00 A4 7F 3B 00 34 00 39 00 39 00 33 00 33 00 33 00 33 00 31 00 31 00 20 00 00 04 DA 93 27 00 DF 21 00 00 04 00 00 00 28 4E 18 7F 2F 54 28 4E 05 F4 01 00 00 FF FF FF FF 00 00 00 00 40 4B 4C 00 70 01 00 00 00 00 00 00 04 00 00 00 CC 51 75 70 36 4E 63 00 13 00 00 00 47 AA EF 2E 2F 00 00 00 2E 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 3C 00 20 00 20 00 20 00 22 6B 20 00 11 7B 20 00 C5 60 20 00 82 59 20 00 E7 65 20 00 2D 00 20 00 27 84 20 00 8F 75 20 00 13 9B 20 00 F2 5D 20 00 91 65 20 00 20 00 3E 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 00 05 50 6B 20 00 64 3E 00 00 04 00 00 00 52 00 69 00 63 00 68 00 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 71 01 00 00 00 00 00 00 07 00 00 00 52 00 69 00 63 00 68 00 CE 8F A8 60 65 67 60 00 00 00 F4 AA CF 4E 5E 00 00 00 20 00 20 00 20 00 20 00 20 00 52 00 69 00 63 00 68 00 94 4E A7 7E 66 8F 1F 96 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 22 6B CE 8F A8 60 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 3B 4E A9 73 E0 65 50 96 20 00 20 00 20 00 20 00 20 00 20 00 66 8F 1F 96 03 80 38 68 A5 63 85 5F A4 7F 3A 00 35 00 32 00 37 00 31 00 32 00 36 00 39 00 38 00 37 00 00 04 3C 45 14 00 7A 49 00 00 05 00 00 00 08 54 A6 7E 36 65 CF 85 B6 5B 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 8B 01 00 00 00 00 00 00 06 00 00 00 48 00 79 00 54 59 D1 8D 11 54 33 96 30 00 00 00 5D AB AB 36 05 00 00 00 5E 97 F7 8B FF 52 65 51 2E 00 00 04 D4 1E 11 00 8F 55 00 00 06 00 00 00 28 4E 5D 4E 29 59 FD 63 08 67 28 4E 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 8F 01 00 00 00 00 00 00 06 00 00 00 3D 63 1F 66 B8 82 50 6C E5 82 36 4E 3C 00 00 00 A4 AB 44 52 08 00 00 00 66 8F 1F 96 82 66 F6 65 0D 4E 36 65 16 59 BA 4E 00 04 90 6D 10 00 AD 0B 00 00 06 00 00 00 28 4E A2 7E D7 65 66 8F 1F 96 28 4E 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 75 00 00 00 00 00 00 00 06 00 00 00 A2 7E D7 65 85 8D A7 7E 66 5B 38 97 18 00 00 00 33 AA 4C 4A 25 00 00 00 65 51 1F 96 81 89 42 6C 3A 00 66 8F 93 5E E5 62 09 67 A2 7E D7 65 68 51 FB 7C 66 8F 86 8F 2C 00 3A 7F 00 4E 0D 4E EF 53 2C 00 26 7B 08 54 81 89 42 6C 84 76 A0 52 A4 7F 31 00 32 00 39 00 30 00 34 00 33 00 33 00 37 00 32 00 00 04 EC 39 10 00 75 01 00 00 06 00 00 00 28 4E 0D 54 1F 66 66 8F 1F 96 28 4E 05 F4 01 00 00 FF FF FF FF 00 00 00 00 40 4B 4C 00 8D 01 00 00 00 00 00 00 05 00 00 00 0D 54 1F 66 01 80 05 5E E5 54 37 01 00 00 33 AA 6B 2B 54 00 00 00 0D 54 1F 66 F1 4F 50 4E E8 90 2F 66 CC 53 94 4E A7 7E CC 53 EE 4F F1 4F 50 4E E8 90 20 00 DE 7A 1F 90 3B 4E 53 62 53 00 32 00 20 00 53 90 77 51 3A 4E 4E 4F 1F 90 CC 53 81 79 8C 54 26 5E 01 95 20 00 16 59 A4 4E A4 7F A4 7F F7 53 3A 4E 33 00 31 00 36 00 36 00 34 00 31 00 32 00 30 00 30 00 20 00 22 6B CE 8F FF 7E 72 82 A9 73 B6 5B A0 52 65 51 20 00 81 89 42 6C 39 65 0D 54 20 00 D1 53 B0 73 28 75 85 8F A9 52 D1 8D 66 8F 20 00 05 6E 06 74 20 00 0C 5E 1B 67 27 59 B6 5B 92 4E F8 76 D1 76 63 77 3E 4E A5 62 00 05 06 82 09 00 31 15 00 00 06 00 00 00 28 4E 64 8D DA 8B 4B 4E C3 5F 28 4E 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 71 01 00 00 00 00 00 00 06 00 00 00 64 8D DA 8B 28 4E 6B 84 A6 82 28 4E 1C 00 00 00 36 AA 63 24 4C 00 00 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 00 4E 47 72 64 8D DA 8B 4B 4E C3 5F 20 00 B3 7E B0 65 A4 7F 31 00 30 00 35 00 36 00 39 00 31 00 39 00 38 00 36 00 31 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 2A 67 39 65 0D 54 00 4E 8B 5F 0D 4E 88 4E 1A 90 C7 8F 20 00 20 00 20 00 3C 68 0F 5F 20 00 20 00 20 00 64 8D DA 8B 28 4E 78 00 78 00 28 4E 00 04 C5 1A 05 00 F7 32 00 00 08 00 00 00 50 00 6C 00 61 00 79 00 43 00 6C 00 75 00 62 00 05 2C 01 00 00 FF FF FF FF 00 00 00 00 C0 C6 2D 00 75 00 00 00 00 00 00 00 06 00 00 00 50 00 6C 00 61 00 79 00 1C 4E CE 98 A0 00 00 00 81 AA 71 25 49 00 00 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 50 00 6C 00 61 00 79 00 20 00 2D 00 20 00 43 00 6C 00 75 00 62 00 20 00 2D 00 20 00 46 00 6F 00 72 00 65 00 76 00 65 00 72 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 20 00 00 04 9A E7 03 00 6B 20 00 00 03 00 00 00 28 4E 6A 8C 28 4E 05 F4 01 00 00 FF FF FF FF 00 00 00 00 40 4B 4C 00 03 00 00 00 00 00 00 00 02 00 00 00 E4 4E 77 83 42 00 00 00 43 AA E0 01 23 00 00 00 53 90 77 51 66 8F 1F 96 20 00 20 00 3C 68 0F 5F 24 4E 57 5B 20 00 20 00 A5 63 85 5F A4 7F 20 00 31 00 20 00 31 00 20 00 35 00 20 00 39 00 20 00 30 00 20 00 35 00 20 00 33 00 20 00 35 00 20 00 32 00 20 00 36 00 00 05 CF 9C 03 00 01");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetClubListCountPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetClubListCountPacket"))
                        {
                            outPacket.WriteHexString("7F F7 00 00 01 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetClubWaitingCrewCountPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetClubWaitingCrewCountPacket"))
                        {
                            outPacket.WriteHexString("32 00 00 00 32 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqLotteryPacket", 0))
                    {
                        short Lottery_Item = iPacket.ReadShort();
                        byte Unk = iPacket.ReadByte();
                        int Type = iPacket.ReadInt();
                        GameSupport.SpRpLotteryPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqEventBuyCount", 0))
                    {
                        EventBuyCount.BuyCount = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 1) EventBuyCount.ShopItem1 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 2) EventBuyCount.ShopItem2 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 3) EventBuyCount.ShopItem3 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 4) EventBuyCount.ShopItem4 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 5) EventBuyCount.ShopItem5 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 6) EventBuyCount.ShopItem6 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 7) EventBuyCount.ShopItem7 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 8) EventBuyCount.ShopItem8 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 9) EventBuyCount.ShopItem9 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 10) EventBuyCount.ShopItem10 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 11) EventBuyCount.ShopItem11 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 12) EventBuyCount.ShopItem12 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 13) EventBuyCount.ShopItem13 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 14) EventBuyCount.ShopItem14 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 15) EventBuyCount.ShopItem15 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 16) EventBuyCount.ShopItem16 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 17) EventBuyCount.ShopItem17 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 18) EventBuyCount.ShopItem18 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 19) EventBuyCount.ShopItem19 = iPacket.ReadInt();
                        if (EventBuyCount.BuyCount >= 20) EventBuyCount.ShopItem20 = iPacket.ReadInt();
                        EventBuyCount.PrEventBuyCount();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRiderTaskContext", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetRiderTaskContext"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqVersusModeRankOnePacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrVersusModeRankOnePacket"))
                        {
                            outPacket.WriteHexString("00 FF FF FF FF FF FF FF FF");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRiderSchoolDataPacket", 0))
                    {
                        RiderSchool.PrRiderSchoolData();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRiderSchoolProPacket", 0))
                    {
                        RiderSchool.PrRiderSchoolPro();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqStartRiderSchool", 0))
                    {
                        RiderSchool.PrStartRiderSchool();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRiderSchoolExpiredCheck", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrRiderSchoolExpiredCheck"))
                        {
                            outPacket.WriteBytes(new byte[10]);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRankerInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrRankerInfoPacket"))
                        {
                            outPacket.WriteByte(0);
                            outPacket.WriteByte(SetRider.Ranker);
                            outPacket.WriteHexString("00 00 C8 42 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRequestExtradata", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrRequestExtradata"))
                        {
                            outPacket.WriteShort(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("ChRequestChStaticRequestPacket", 0))
                    {
                        GameSupport.ChRequestChStaticReplyPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqDynamicCommand", 0))
                    {
                        GameSupport.PrDynamicCommand();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqPubCommandPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrPubCommandPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqWebEventCompleteCheckPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrWebEventCompleteCheckPacket"))
                        {
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqKoinBalance", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpKoinBalance"))
                        {
                            outPacket.WriteUInt(SetRider.Koin);
                            outPacket.WriteUInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqFavoriteTrackMapGet", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrFavoriteTrackMapGet"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetFavoriteChannel", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetFavoriteChannel"))
                        {
                            outPacket.WriteHexString("02 00 00 00 00 00 00 00 00 00 01 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqKartPassInitPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrKartPassInitPacket"))
                        {
                            outPacket.WriteInt(3);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqGetCashInventoryPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpGetCashInventoryPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqRemainCashPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpRemainCashPacket"))
                        {
                            outPacket.WriteUInt(0);
                            outPacket.WriteUInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqRemainTcCashPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpRemainTcCashPacket"))
                        {
                            outPacket.WriteUInt(99);
                            outPacket.WriteUInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpReqNormalShopBuyItemPacket", 0) || hash == Adler32Helper.GenerateAdler32_ASCII("SpReqItemPresetShopBuyItemPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRepBuyItemPacket"))
                        {
                            outPacket.WriteHexString("01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetCurrentRid", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetCurrentRid"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetMyCouponList", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetMyCouponList"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqDisassembleFeeInfo", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrDisassembleFeeInfo"))
                        {
                            outPacket.WriteHexString("00 00 00 00 06 00 00 00 00 00 E8 03 01 00 F4 01 00 00 E8 03 01 00 F4 01 00 00 E8 03 01 00 F4 01");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRequestExchangeInitPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrRequestExchangeInitPacket"))
                        {
                            outPacket.WriteHexString("01 03 00 00 00 F4 01 00 00 01 00 00 00 02 00 00 00 03 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqRequestPeriodExchangeInitPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrRequestPeriodExchangeInitPacket"))
                        {
                            outPacket.WriteBytes(new byte[22]);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqEnterRewardBoxStage", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpEnterRewardBoxStage"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(1);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqExitRewardBoxStage", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqGetGiftListIncomingPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpGetGiftListIncomingPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqGetGiftListReceivedPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpGetGiftListReceivedPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetCompetitiveRankInfo", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetCompetitiveRankInfo"))
                        {
                            outPacket.WriteHexString("01 00 00 00 00 FF 00 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetCompetitiveSlotInfo", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetCompetitiveSlotInfo"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetCompetitiveCount", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetCompetitiveCount"))
                        {
                            outPacket.WriteHexString("B3 02 52 1B 00 00 B4 02 54 1B 00 00 B9 02 82 1B 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSearchCompetitiveRankPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSearchCompetitiveRankPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetCompetitivePreRankPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetCompetitivePreRankPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("RmRequestEmblemsPacket", 0))
                    {
                        Emblem.RmOwnerEmblemPacket();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("RmRqUpdateMainEmblemPacket", 0))
                    {
                        SetRider.Emblem1 = iPacket.ReadShort();
                        SetRider.Emblem2 = iPacket.ReadShort();
                        using (OutPacket outPacket = new OutPacket("RmRpUpdateMainEmblemPacket"))
                        {
                            outPacket.WriteByte(1);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        SetGameData.Save_Emblem();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSyncDictionaryInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSyncDictionaryInfoPacket"))
                        {
                            outPacket.WriteInt(1);
                            outPacket.WriteInt(1);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetDictionaryRewardInfoPacket", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqNewCareerListPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrNewCareerListPacket"))
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                outPacket.WriteInt(0);
                            }
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("LoRqDeleteItemPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("LoRpDeleteItemPacket"))
                        {
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqQueryCoupon", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpQueryCoupon"))
                        {
                            outPacket.WriteInt(1);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqShopCashPage", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrShopCashPage"))
                        {
                            outPacket.WriteString("https://ripay.nexon.com/Payment/Index");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqShopURLPage", 0))
                    {
                        int URLPageType = iPacket.ReadInt();
                        using (OutPacket outPacket = new OutPacket("PrShopURLPage"))
                        {
                            outPacket.WriteInt(URLPageType);
                            outPacket.WriteString("https://pay.tiancity.com/InnerGame/IndexII.aspx");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqBingoSync", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrBingoSync"))
                        {
                            outPacket.WriteByte(0);
                            outPacket.WriteUShort(0);
                            outPacket.WriteUShort(0);
                            for (int i = 0; i < 15; i++)
                            {
                                outPacket.WriteByte(0);
                            }
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqEnterKartPassPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrEnterKartPassPacket"))
                        {
                            outPacket.WriteHexString("00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqKartPassPlayTimePacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrKartPassPlayTimePacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqKartPassRewardPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrKartPassRewardPacket"))
                        {
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqEnterSeasonPassPacket", 0))
                    {
                        byte SeasonPassType = iPacket.ReadByte();
                        using (OutPacket outPacket = new OutPacket("PrEnterSeasonPassPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(SeasonPassType);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSeasonPassRewardPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSeasonPassRewardPacket"))
                        {
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqCheckPassword", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrCheckPassword"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqUnLockedItem", 0))
                    {
                        int useType = iPacket.ReadInt();
                        int stringType = iPacket.ReadInt();
                        for (int i = 0; i < useType; i++)
                        {
                            iPacket.ReadString(false);
                        }
                        byte Type = iPacket.ReadByte();
                        using (OutPacket outPacket = new OutPacket("PrUnLockedItem"))
                        {
                            outPacket.WriteByte(Type);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqFavoriteItemGet", 0)) //즐겨 찾기 목록
                    {
                        FavoriteItem.Favorite_Item();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqFavoriteItemUpdate", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqLockedItemGet", 0))//아이템 보호
                    {
                        using (OutPacket outPacket = new OutPacket("PrLockedItemGet"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqLockedItemUpdate", 0))
                    {
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSimGameSimpleInfoAndRankPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSimGameSimpleInfoAndRankPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSimGameEnterPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSimGameEnterPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqChannelSwitch", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("ChGetCurrentGpReplyPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("ChCreateRoomRequestPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("ChCreateRoomReplyPacket"))
                        {
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("SpRqTimeShopPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("SpRpTimeShopPacket"))
                        {
                            outPacket.WriteHexString("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF 47 00 00 00 00 00 47 00 00 00 00 00 00 00 02 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqSecretShopEnterPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrSecretShopEnterPacket"))
                        {
                            outPacket.WriteHexString("00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqEnterUpgradeGearPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrEnterUpgradeGearPacket"))
                        {
                            outPacket.WriteHexString("05 00 00 00 03 00 00 00 05 00 00 00 00 00 00 00 00 00 00 00");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqBlockCatchEnterPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrBlockCatchEnterPacket"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(3);
                            outPacket.WriteInt(3);
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(5);
                            outPacket.WriteInt(1);
                            outPacket.WriteInt(7);
                            outPacket.WriteInt(2);
                            outPacket.WriteInt(600);
                            outPacket.WriteInt(300);
                            outPacket.WriteInt(200);
                            outPacket.WriteInt(100);
                            outPacket.WriteInt(-100);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("RqEnterFishingStagePacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("RpEnterFishingStagePacket"))
                        {
                            outPacket.WriteByte(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqPcCafeShowcaseCoupon", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrPcCafeShowcaseCoupon"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqGetRiderCareerSummary", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrGetRiderCareerSummary"))
                        {
                            outPacket.WriteInt(0);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqTcCashEventUserInfoPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrTcCashEventUserInfoPacket"))
                        {
                            outPacket.WriteHexString("47 85 03 00 00 00 00 00 00 00 00 00 02");
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("checkSecondAuthenPacket", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("checkSecondAuthenPacket"))
                        {
                            outPacket.WriteInt(2);
                            outPacket.WriteInt(0);
                            this.Parent.Client.Send(outPacket);
                        }
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqTestServerAddItemPacket", 0))
                    {
                        TestServer.Type = iPacket.ReadShort();
                        TestServer.ItemID = iPacket.ReadShort();
                        TestServer.Amount = iPacket.ReadShort();
                        TestServer.TestServerAddItem();
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqServerTime", 0))
                    {
                        using (OutPacket outPacket = new OutPacket("PrServerTime"))
                        {
                            outPacket.WriteHexString(Program.DataTime);
                            this.Parent.Client.Send(outPacket);
                        } 
                        return;
                    }
                    else if (hash == Adler32Helper.GenerateAdler32_ASCII("PqLogin", 0))
                    {
                        GameDataReset.DataReset();
                        return;
                    }
                }
            }
        }
    }
}