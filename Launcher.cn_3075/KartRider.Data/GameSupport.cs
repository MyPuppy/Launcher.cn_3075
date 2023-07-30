using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartRider.IO;
using System.Net;
using Set_Data;

namespace KartRider
{
    public static class GameSupport
    {
        public static void PcFirstMessage()
        {
            uint first_val = 3595571486;
            uint second_val = 2168420743;
            using (OutPacket outPacket = new OutPacket("PcFirstMessage"))
            {
                outPacket.WriteUShort(SessionGroup.usLocale);
                outPacket.WriteUShort(1);
                outPacket.WriteUShort(Program.Version);
                outPacket.WriteString("http://kartupdate.tiancity.cn/patch/ORZNEHDJGILEVRO");
                outPacket.WriteUInt(first_val);
                outPacket.WriteUInt(second_val);
                outPacket.WriteByte(SessionGroup.nClientLoc);
                outPacket.WriteString("+B1K8NAOvJd3cXFieRWTkRNj2rlv2qVmALSUdXFpNl0=");
                outPacket.WriteHexString("00 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 2E 31 2E 31 37 2E 36 00 00 00 00 00 00 00");
                outPacket.WriteString("TwKtPFLx+3AuKg5PFa021r3hKyFDK2sFBzQJJCI26wA=");
                RouterListener.MySession.Client.Send(outPacket);
            }
            RouterListener.MySession.Client._RIV = first_val ^ second_val;
            RouterListener.MySession.Client._SIV = first_val ^ second_val;
        }

        public static void OnDisconnect()
        {
            RouterListener.MySession.Client.Disconnect();
        }

        public static void SpRpLotteryPacket()
        {
            using (OutPacket outPacket = new OutPacket("SpRpLotteryPacket"))
            {
                outPacket.WriteHexString("05 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00");
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrGetGameOption()
        {
            using (OutPacket outPacket = new OutPacket("PrGetGameOption"))
            {
                outPacket.WriteFloat(SetGameOption.Set_BGM);
                outPacket.WriteFloat(SetGameOption.Set_Sound);
                outPacket.WriteByte(SetGameOption.Main_BGM);
                outPacket.WriteByte(SetGameOption.Sound_effect);
                outPacket.WriteByte(SetGameOption.Full_screen);
                outPacket.WriteByte(SetGameOption.Unk1);
                outPacket.WriteByte(SetGameOption.Unk2);
                outPacket.WriteByte(SetGameOption.Unk3);
                outPacket.WriteByte(SetGameOption.Unk4);
                outPacket.WriteByte(SetGameOption.Unk5);
                outPacket.WriteByte(SetGameOption.Unk6);
                outPacket.WriteByte(SetGameOption.Unk7);
                outPacket.WriteByte(SetGameOption.Unk8);
                outPacket.WriteByte(SetGameOption.Unk9);
                outPacket.WriteByte(SetGameOption.Unk10);
                outPacket.WriteByte(SetGameOption.Unk11);
                outPacket.WriteByte(SetGameOption.BGM_Check);
                outPacket.WriteByte(SetGameOption.Sound_Check);
                outPacket.WriteByte(SetGameOption.Unk12);
                outPacket.WriteByte(SetGameOption.Unk13);
                outPacket.WriteByte(SetGameOption.GameType);
                outPacket.WriteByte(SetGameOption.SetGhost);
                outPacket.WriteByte(SetGameOption.SpeedType);
                outPacket.WriteByte(SetGameOption.Unk14);
                outPacket.WriteByte(SetGameOption.Unk15);
                outPacket.WriteByte(SetGameOption.Unk16);
                outPacket.WriteByte(SetGameOption.Unk17);
                outPacket.WriteByte(SetGameOption.Set_screen);
                outPacket.WriteByte(SetGameOption.Unk18);
                outPacket.WriteBytes(new byte[82]);
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void ChRpEnterMyRoomPacket()
        {
            if (GameType.EnterMyRoomType == 0)
            {
                using (OutPacket outPacket = new OutPacket("ChRpEnterMyRoomPacket"))
                {
                    outPacket.WriteString(SetRider.Nickname);
                    outPacket.WriteByte(0);
                    outPacket.WriteShort(SetMyRoom.MyRoom);
                    outPacket.WriteByte(SetMyRoom.MyRoomBGM);
                    outPacket.WriteByte(SetMyRoom.UseRoomPwd);
                    outPacket.WriteByte(0);
                    outPacket.WriteByte(SetMyRoom.UseItemPwd);
                    outPacket.WriteByte(SetMyRoom.TalkLock);
                    outPacket.WriteString(SetMyRoom.RoomPwd);
                    outPacket.WriteString("");
                    outPacket.WriteString(SetMyRoom.ItemPwd);
                    outPacket.WriteShort(SetMyRoom.MyRoomKart1);
                    outPacket.WriteShort(SetMyRoom.MyRoomKart2);
                    RouterListener.MySession.Client.Send(outPacket);
                }
            }
            else
            {
                using (OutPacket outPacket = new OutPacket("ChRpEnterMyRoomPacket"))
                {
                    outPacket.WriteString("");
                    outPacket.WriteByte(GameType.EnterMyRoomType);
                    outPacket.WriteShort(0);
                    outPacket.WriteByte(0);
                    outPacket.WriteByte(0);
                    outPacket.WriteByte(0);
                    outPacket.WriteByte(0);
                    outPacket.WriteByte(1);
                    outPacket.WriteString("");//RoomPwd
                    outPacket.WriteString("");
                    outPacket.WriteString("");//ItemPwd 
                    outPacket.WriteShort(0);
                    outPacket.WriteShort(0);
                    RouterListener.MySession.Client.Send(outPacket);
                }
            }
        }

        public static void RmNotiMyRoomInfoPacket()
        {
            using (OutPacket outPacket = new OutPacket("RmNotiMyRoomInfoPacket"))
            {
                outPacket.WriteShort(SetMyRoom.MyRoom);
                outPacket.WriteByte(SetMyRoom.MyRoomBGM);
                outPacket.WriteByte(SetMyRoom.UseRoomPwd);
                outPacket.WriteByte(0);
                outPacket.WriteByte(SetMyRoom.UseItemPwd);
                outPacket.WriteByte(SetMyRoom.TalkLock);
                outPacket.WriteString(SetMyRoom.RoomPwd);
                outPacket.WriteString("");
                outPacket.WriteString(SetMyRoom.ItemPwd);
                outPacket.WriteShort(SetMyRoom.MyRoomKart1);
                outPacket.WriteShort(SetMyRoom.MyRoomKart2);
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrGetRiderInfo()
        {
            using (OutPacket outPacket = new OutPacket("PrGetRiderInfo"))
            {
                outPacket.WriteByte(1);
                outPacket.WriteUInt(SetRider.UserNO);
                outPacket.WriteString(SetRider.UserID);
                outPacket.WriteString(SetRider.Nickname);
                outPacket.WriteHexString("3C 9A 17 43");//2008.02.08
                for (int i = 1; i <= Program.MAX_EQP_P; i++)
                {
                    outPacket.WriteShort(0);
                }
                outPacket.WriteByte(0);
                outPacket.WriteString("");
                outPacket.WriteInt(SetRider.RP);
                outPacket.WriteInt(0);
                outPacket.WriteByte(6);//Licenses
                outPacket.WriteHexString(Program.DataTime);
                outPacket.WriteBytes(new byte[17]);
                outPacket.WriteShort(SetRider.Emblem1);
                outPacket.WriteShort(SetRider.Emblem2);
                outPacket.WriteShort(0);
                outPacket.WriteString(SetRider.RiderIntro);
                outPacket.WriteInt(SetRider.Premium);
                outPacket.WriteByte(1);
                if (SetRider.Premium == 0)
                    outPacket.WriteInt(0);
                else if (SetRider.Premium == 1)
                    outPacket.WriteInt(10000);
                else if (SetRider.Premium == 2)
                    outPacket.WriteInt(30000);
                else if (SetRider.Premium == 3)
                    outPacket.WriteInt(60000);
                else if (SetRider.Premium == 4)
                    outPacket.WriteInt(120000);
                else if (SetRider.Premium == 5)
                    outPacket.WriteInt(200000);
                else
                    outPacket.WriteInt(0);
                if (SetRider.ClubMark_LOGO == 0)
                {
                    outPacket.WriteInt(0);
                    outPacket.WriteInt(0);
                    outPacket.WriteInt(0);
                    outPacket.WriteString("");
                }
                else
                {
                    outPacket.WriteInt(SetRider.ClubCode);
                    outPacket.WriteInt(SetRider.ClubMark_LOGO);
                    outPacket.WriteInt(SetRider.ClubMark_LINE);
                    outPacket.WriteString(SetRider.ClubName);
                }
                outPacket.WriteInt(0);
                outPacket.WriteByte(SetRider.Ranker);
                outPacket.WriteInt(0);
                outPacket.WriteInt(0);
                outPacket.WriteInt(0);
                outPacket.WriteInt(0);
                outPacket.WriteInt(0);
                outPacket.WriteByte(0);
                outPacket.WriteByte(0);
                outPacket.WriteByte(0);
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrCheckMyClubStatePacket()
        {
            using (OutPacket outPacket = new OutPacket("PrCheckMyClubStatePacket"))
            {
                if (SetRider.ClubMark_LOGO == 0)
                {
                    outPacket.WriteInt(0);
                    outPacket.WriteString("");
                    outPacket.WriteInt(0);
                    outPacket.WriteInt(0);
                }
                else
                {
                    outPacket.WriteInt(SetRider.ClubCode);
                    outPacket.WriteString(SetRider.ClubName);
                    outPacket.WriteInt(SetRider.ClubMark_LOGO);
                    outPacket.WriteInt(SetRider.ClubMark_LINE);
                }
                outPacket.WriteShort(5);//Grade
                outPacket.WriteString(SetRider.Nickname);
                outPacket.WriteInt(1);//ClubMember
                outPacket.WriteByte(5);//Level
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void ChRequestChStaticReplyPacket()
        {
            using (OutPacket outPacket = new OutPacket("ChRequestChStaticReplyPacket"))
            {
                outPacket.WriteHexString("01 F5 02 00 00 53 01 BB 42 33 1E 08 0D 00 00 78 DA 9D 56 69 73 12 41 10 6D 13 C2 CD 2E 77 24 C6 FB BE 15 6F CD A1 09 87 58 A5 65 25 7E B7 24 6C 30 95 05 52 80 46 FF BD DD 0D 03 BB A4 D9 19 2C AA 76 77 BA 5F BF 3E E8 99 E9 06 00 7C B0 F1 31 80 21 38 F0 03 5C 7C FF 84 06 AF 3A F8 EE 42 0B 8E 20 96 62 C8 09 0A 1D 14 28 71 0D 0D C8 70 89 18 8E C6 36 5F F0 79 3A 61 F8 C6 AC 1D 58 9E 0F 51 6C A1 9C 96 45 39 5C C9 69 D9 14 34 1C F3 85 AE 98 22 31 31 A3 68 4A 44 2B B2 78 3A 50 ED F0 33 91 0E 2C D6 08 94 CC 18 30 95 21 95 31 E0 2A 83 55 D0 96 6E 24 6D 22 CA 01 BB A0 2D 9F 17 9E B6 C5 58 BD 90 8C 2D C6 E9 85 64 A9 2C 6D F8 85 0B 97 95 92 FB 5C DC 07 9A 32 E6 2D 54 1C 60 6B 92 E7 13 54 F7 D0 C5 00 F6 F0 FD 97 DB B6 10 46 40 13 3F 87 F8 73 D1 A6 48 82 43 D4 77 90 CE 45 C5 6A 94 05 F4 D9 9E 24 71 DE 2B 54 61 97 E4 7F 90 DE 87 F8 EE 72 F0 6B 72 2F F8 41 17 E2 9E 4A 2B 48 1D 8D D6 33 82 A2 8A 56 A3 1C BB A8 21 D8 C5 84 E8 84 54 97 EC C0 0E 22 C8 E5 AC 41 93 11 F0 8A C5 65 EF B3 E7 16 0A FA 18 DB 1F F8 3E 89 F0 AA 3D 07 30 65 BF 96 D3 42 54 21 AF EB A1 2A D6 1B 6B C6 AC FE CA DF 2C 69 52 92 FB FE D6 62 66 2A CC DB 19 EE BE 1E 1C E3 93 DA 6D F4 3D CB 7E 27 18 A6 D8 EE 96 44 D8 BE 26 E5 7B 3A 33 B9 47 EF 67 0D BD 3D C8 1A F2 3F 94 4F 83 7D 6E ED 16 1A 9D C2 23 B9 7B BD 90 C7 45 2D 8B 3F 91 27 45 2D A7 DF E0 69 04 0D 5C 2E BE 62 2F 4F 45 CA FE 59 D2 B3 55 3F 8F 8D 15 FE B9 A4 54 96 2F 28 A0 21 9F 7E 7D 54 D2 D9 44 DB 9B 40 6D B1 85 5F 9A 1A 28 0F AF D6 03 0D 82 7B FD F5 FF 18 2B CF 6F 82 8D 5D 86 D1 49 EE 08 9D F4 96 F2 FC CD A6 03 84 0C 90 A0 87 0A 87 BD 8E 20 BB 7C 72 37 79 ED C0 BB CC 1C 83 AF B8 1A C9 28 DC 8D 10 DF 12 2E 2E 9B B0 69 79 16 7B 18 CD 01 3B 98 16 71 2B 25 02 54 DE DB D4 A2 7D F4 43 CD B3 83 B0 1E 02 8E 7D 0C EF C9 45 8B 53 A5 B1 69 5A 06 55 A5 9D F4 99 D9 EA 6C 35 76 B3 C2 5D D0 E0 3B C0 19 9F 45 43 36 A8 78 87 A8 86 58 A8 AA 2D 70 F9 21 35 F9 76 F3 83 EA F2 ED E6 07 7D CC 1B DC 93 F4 4F F5 69 87 E5 0D EE 4B 05 FE 64 79 D2 A8 E0 1F D4 9B 71 4D 69 9E 03 7D B2 55 44 40 0C F4 13 0C E5 0B 4B A0 AF 70 85 80 CB 60 52 9F 3A 41 43 60 52 EF 1A 41 57 C0 64 F0 A0 53 1E C2 60 32 7E D0 E5 09 11 58 6C 3A A4 D2 43 14 16 9B 11 A9 6E 10 07 FD 18 48 1B 19 12 30 3B AD 91 37 48 82 77 FF 6E 90 28 05 BA 5D BC 49 30 0B 82 F7 F2 16 81 6C 90 26 C0 55 52 FD 03 2B 3B 42 BC");
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrDynamicCommand()
        {
            using (OutPacket outPacket = new OutPacket("PrDynamicCommand"))
            {
                outPacket.WriteByte(0);
                outPacket.WriteInt(0);
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrQuestUX2ndPacket()
        {
            //questGroupIndex='2' seasonId='17' kartPassSetId='1' unk='0' id='13'
            //EX) 217010013
            int NormalQuest = 6;
            int KartPassQuest = 0;
            int All_Quest = NormalQuest + KartPassQuest;
            using (OutPacket outPacket = new OutPacket("PrQuestUX2ndPacket"))
            {
                outPacket.WriteInt(1);
                outPacket.WriteInt(1);
                outPacket.WriteInt(All_Quest);
                for (int i = 1393; i <= 1409; i++)
                {
                    if (i == 1393 || i == 1394 || i == 1406 || i == 1407 || i == 1408 || i == 1409)
                    {
                        outPacket.WriteInt(i);
                        outPacket.WriteInt(i);
                        outPacket.WriteInt(0);
                        outPacket.WriteInt(0);
                        outPacket.WriteInt(0);
                        outPacket.WriteInt(0);
                        outPacket.WriteInt(2);
                        outPacket.WriteInt(0);
                        outPacket.WriteByte(0);
                    }
                }
                RouterListener.MySession.Client.Send(outPacket);
            }
        }

        public static void PrLogin()
        {
            using (OutPacket outPacket = new OutPacket("PrLogin"))
            {
                outPacket.WriteInt(0);
                outPacket.WriteHexString(Program.DataTime);
                outPacket.WriteUInt(SetRider.UserNO);
                outPacket.WriteString(SetRider.UserID);
                outPacket.WriteByte(2);
                outPacket.WriteByte(1);
                outPacket.WriteByte(0);
                outPacket.WriteInt(0);
                outPacket.WriteByte(0);
                outPacket.WriteInt(1415577599);
                outPacket.WriteUInt(SetRider.pmap);
                for (int i = 0; i < 11; i++)
                {
                    outPacket.WriteInt(0);
                }
                outPacket.WriteByte(0);
                outPacket.WriteEndPoint(IPAddress.Parse(RouterListener.forceConnect), 39311);
                outPacket.WriteEndPoint(IPAddress.Parse(RouterListener.forceConnect), 39312);
                outPacket.WriteInt(0);
                outPacket.WriteString("");
                outPacket.WriteInt(0);
                outPacket.WriteByte(1);
                outPacket.WriteString("content");
                outPacket.WriteInt(0);
                outPacket.WriteInt(1);
                outPacket.WriteString("cc");
                outPacket.WriteString(SessionGroup.Service);
                outPacket.WriteInt(0);
                outPacket.WriteByte(0);
                outPacket.WriteByte(SetGameOption.Set_screen);
                outPacket.WriteByte(SetRider.IdentificationType);
                RouterListener.MySession.Client.Send(outPacket);
            }
        }
    }
}