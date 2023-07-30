using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartRider.IO;
using KartRider;

namespace RiderData
{
    public static class FavoriteItem
    {
        public static void Favorite_Item()
        {
            int itemCount = 23;
            using (OutPacket outPacket = new OutPacket("PrFavoriteItemGet"))
            {
                if (Program.FavoriteItem)
                {
                    outPacket.WriteInt(itemCount);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(433);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(684);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(854);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1045);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1098);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1247);
                    outPacket.WriteShort(301);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1276);
                    outPacket.WriteShort(61);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1348);
                    outPacket.WriteShort(87);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1359);
                    outPacket.WriteShort(95);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1370);
                    outPacket.WriteShort(102);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1376);
                    outPacket.WriteShort(105);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1379);
                    outPacket.WriteShort(106);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1385);
                    outPacket.WriteShort(112);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1387);
                    outPacket.WriteShort(114);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1401);
                    outPacket.WriteShort(125);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1419);
                    outPacket.WriteShort(138);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1430);
                    outPacket.WriteShort(145);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1435);
                    outPacket.WriteShort(148);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1440);
                    outPacket.WriteShort(150);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1452);
                    outPacket.WriteShort(161);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1459);
                    outPacket.WriteShort(166);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1463);
                    outPacket.WriteShort(168);
                    outPacket.WriteByte(0);

                    outPacket.WriteShort(3);
                    outPacket.WriteShort(1466);
                    outPacket.WriteShort(170);
                    outPacket.WriteByte(0);
                }
                else
                {
                    outPacket.WriteInt(0);
                }
                RouterListener.MySession.Client.Send(outPacket);
            }
        }
    }
}