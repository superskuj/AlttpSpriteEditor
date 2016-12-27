using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlttpSpriteEditor
{
    public static class Conversion
    {
        //receives a 3 byte array, little endian snes address, returns 3 byte array, big endian pc address
        //This is a weird algorithm but it should work
        public static byte[] snesToPc(byte[] snesAddress)
        {
            byte[] pcAddress = new byte[3];
            double temp, mod;
            mod = snesAddress[2] % 2;
            temp = snesAddress[2] / 2; temp = Math.Floor(temp);
            pcAddress[2] = (byte)temp;

            if (mod != 0)
            {
                pcAddress[1] = snesAddress[1];
            }
            else
            {
                temp = snesAddress[1] - 0x80;
                pcAddress[1] = (byte)temp;
            }
            pcAddress[0] = snesAddress[0];
            Array.Reverse(pcAddress);

            return pcAddress;
        }

        //receives a 3 byte array, big endian pc address, returns 3 byte array, little endian snes address
        public static byte[] pcToSnes(byte[] pcAddress)
        {
            byte[] snesAddress = new byte[3];
            double temp;
            temp = pcAddress[0] * 2;
            snesAddress[0] = (byte)temp;

            temp = pcAddress[1];

            if (temp >= 0x80)
            {
                snesAddress[0]++;
            }
            else
            {
                temp = temp + 0x80;
            }
            snesAddress[1] = (byte)temp;
            snesAddress[2] = pcAddress[2];
            Array.Reverse(snesAddress);

            return snesAddress;
        }

        public static int ParseHexString(string s)
        {
            s = "0x" + s;
            int i = Convert.ToInt32(s, 16);
            return i;
        }

        public static int GetListIndexFromNonsequentialStringIndex(List<string> list, int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int stringIndex = ParseHexString(list[i].ToString().Substring(1, 2));
                if (stringIndex == index)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
