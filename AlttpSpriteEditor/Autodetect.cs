using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlttpSpriteEditor
{
    public class Autodetect
    {
        // <Summary>
        // this method confirms if the rom is alttp, and checks the region
        // returns: 0 = not alttp, 1 = US , 2 = JP
        // </Summary>
        public static int CheckRegion(MemoryStream ROMStream)
        {
            int headerOffset = (CheckHeader(ROMStream) == 0) ? 0x00 : 0x0200; 


            int titleOffset = 0x7FC0 + headerOffset;
            byte[] byteArray = new byte[10];
            string usCheck = "544845204C4547454E44";
            string jpCheck = "5A454C44414E4F44454E";
            ROMStream.Position = titleOffset; ROMStream.Read(byteArray, 0, 10);

            string arrayString = "";
            for (int i = 0; i < byteArray.Length; i++)
            {
                arrayString += byteArray[i].ToString("X2");
            }

            Console.WriteLine(arrayString);

            if (arrayString == usCheck)
            {
                Console.WriteLine("US version");//debug
                return 1;
            }
            else if (arrayString == jpCheck)
            {
                Console.WriteLine("JP version");//debug
                return 2;
            }

            return 0;
        }

        // <Summary>
        // Receives a rom as a memorystream and checks if it has a copier header.
        // returns 0 if headerless, 1 if headered
        // </Summary>
        public static int CheckHeader(MemoryStream ROMStream)
        {
            byte[] byteArray = new byte[4];
            string headerCheck = "789C0042";

            ROMStream.Position = 0;
            ROMStream.Read(byteArray, 0, 4);

            string arrayString = "";
            for (int i = 0; i < byteArray.Length; i++)
            {
                arrayString += byteArray[i].ToString("X2");
            }
            Console.WriteLine(arrayString);

            if (arrayString == headerCheck)
            {
                Console.WriteLine("0");
                return 0;
            }
            Console.WriteLine("1");
            return 1;
        }
    }
}