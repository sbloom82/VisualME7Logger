using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace VisualME7Logger.Output
{
    public class EEPROM
    {
        static void CalculateChecksum(string fileIn, string fileOut)
        {
            
           /*
            int result = 255;

            Console.WriteLine("EEPROM 95040 Checksum calculator 1.0");
            Console.WriteLine("");

            if (strArgs.Length < 3)
            {
                Console.WriteLine("Not enough parameters passed to this program.");
                DisplayHelp();
                Environment.ExitCode = 1;
                return 1;
            }

            try
            {
                result = MainImp(System.Environment.GetCommandLineArgs());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing program:\r\n" + ex.Message);
                Environment.ExitCode = 1;
                return 1;
            }


            Environment.ExitCode = result;
            return result;
            */
        }


        static int MainImp(string[] args)
        {
            byte[] dataIn = new byte[16];
            byte[] dataOut = new byte[16];
            System.IO.FileStream fileIn = null;
            System.IO.FileStream fileOut = null;

            int bytesIn = 0;
            int pageNumber = 0;
            int CSMask = 0x01;
            int CBMask = 0x40;

            int[] pageDesc = new int[32]  {
                             0xFF18,
                             0x0017,
                             0x0117,
                             0x0207,
                             0x0307,
                             0x0437,
                             0x0533,
                             0x06B7,
                             0x06F7,
                             0x07B3,
                             0x07F3,
                             0x08B7,
                             0x08F7,
                             0x09B3,
                             0x09F3,
                             0x0AB3,
                             0x0AF3,
                             0x0B32,
                             0x0B10,
                             0x0B10,
                             0x0B10,
                             0x0C37,
                             0x0D33,
                             0x0E33,
                             0x0F33,
                             0x1033,
                             0x1133,
                             0x1233,
                             0x1235,
                             0x1235,
                             0x13B7,
                             0x13F7
                             };

            Int64 fileInLength = new System.IO.FileInfo(args[1]).Length;
            if (fileInLength != 512)
            {
                throw new ApplicationException("EEPROM file must be 512 bytes in size. Yours is: " + fileInLength + " long.");
            }

            if (System.IO.File.Exists(args[2])) System.IO.File.Delete(args[2]);
            fileIn = new System.IO.FileStream(args[1], System.IO.FileMode.Open);
            fileOut = new System.IO.FileStream(args[2], System.IO.FileMode.Create);


            while (true)
            {

                bytesIn = fileIn.Read(dataIn, 0, 16);

                if (bytesIn == 0)
                {
                    break;
                }
                else if (bytesIn != 16)
                {
                    System.Environment.ExitCode = 1;
                    fileIn.Close();
                    fileOut.Close();
                    throw new ApplicationException("Failed reading in input file. Uneven blocks encountered (they must increase by 16 bytes)");
                }


                dataIn.CopyTo(dataOut, 0);

                if ((pageDesc[pageNumber] & CSMask) == CSMask)
                {
                    int offset;
                    int checksum = 0;

                    for (offset = 0; offset <= 13; offset++)
                    {
                        checksum = checksum + (int)dataIn[offset];
                    }

                    checksum = checksum + pageNumber;

                    if ((pageDesc[pageNumber] & CBMask) == CBMask)
                    {
                        checksum = checksum - 1;
                    }

                    checksum = checksum * -1;

                    byte[] CRCbytes = System.BitConverter.GetBytes(checksum);

                    dataOut[14] = CRCbytes[0];
                    dataOut[15] = CRCbytes[1];

                }

                fileOut.Write(dataOut, 0, 16);

                pageNumber++;

            }

            fileIn.Close();
            fileOut.Close();

            return 0;
        }

        static void DisplayHelp()
        {

            string strProgramName = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileName(System.Environment.GetCommandLineArgs()[0])));

            Console.WriteLine();
            Console.WriteLine("Help for " + strProgramName + ":");
            Console.WriteLine();
            Console.WriteLine(strProgramName + " FileIn.ext FileOut.ext");
            Console.WriteLine();

        }

        /*
         * 
static int pageDesc[32] = { 0xFF18,0x0017,0x0117,0x0207,0x0307,0x0437,0x0533,0x06B7,0x06F7,0x07B3,0x07F3,0x08B7,0x08F7,0x09B3,0x09F3,0x0AB3,0x0AF3,0x0B32,0x0B10,0x0B10,0x0B10,0x0C37,0x0D33,0x0E33,0x0F33,0x1033,0x1133,0x1233,0x1235,0x1235,0x13B7,0x13F7 };
static int CSMask = 0x01;
static int CBMask = 0x40;
static unsigned char dataOut[16*32];

int main(int argc, char **argv)
{
  int pageNumber = 0;
  unsigned char *pInPos,*pOutPos,chk_byte1,chk_byte2;
  unsigned char *pIn;
  unsigned char modified;
  int corrected=0;
	size_t filelen;
  int offset,result;
  unsigned short checksum;

		printf("EEPROM 95040 Checksum calculator 1.01\n\n");
    if (argc < 3) { printf("Usage: %s <infile> <outfile>",argv[0]);	return 1; }

		// load then check length is 512 bytes exactly
		pIn = load_file(argv[1],&filelen);

		// validate loaded size is correct
		if(filelen != 512) { printf("file must be 512 bytes\n"); if(pIn != 0) free(pIn); return 2; }

    printf("        : 0-------------------1-------|----|\n");
    printf("        : 0 1 2 3 4 5 6 7 8 9 0 1 2 3 |4 5 |\n");
    for(pageNumber=0;pageNumber < 32;pageNumber++)
    {
        printf("Block %-2.2d: ",pageNumber);
    	// calculate current in and out positions
			pInPos  = pIn     + (pageNumber*16);
			pOutPos = dataOut + (pageNumber*16);

      // copy 16 bytes, in to out
      memcpy(pOutPos,pInPos, 16);
    
    	// calc checksums
     	checksum = 0;
      if ((pageDesc[pageNumber] & CSMask) == CSMask)
      {
        for (offset = 0; offset <= 13; offset++) { 
        	printf("%-2.2x",pInPos[offset]);
        	checksum = checksum + (int)pInPos[offset]; 
        }
        checksum = checksum + pageNumber;
        if ((pageDesc[pageNumber] & CBMask) == CBMask) { checksum = checksum - 1; }
        checksum = checksum * -1;

       	printf("[%-2.2x%-2.2x]",pInPos[14],pInPos[15]);

				// display in ascii
				printf(" ");
        for (offset = 0; offset <= 15; offset++) { 
		    	if(isprint(((unsigned char)pInPos[offset]))) {
 		      	printf("%c", ((unsigned char)pInPos[offset]));
		      } else {
		      	printf(".");
		      }
	       	if(offset == 13) printf(" ");
        }
				chk_byte1 = (unsigned char)((checksum     ) & 0x00ff);
				chk_byte2 = (unsigned char)((checksum >> 8) & 0x00ff);
 	 	   	printf(" Desc %-4.4x, New Chksum [%-1.1x%-1.1x]",pageDesc[pageNumber],chk_byte1,chk_byte2);
 	 	   	
 				// updated corrected checksums and count number of changes
   	   	modified=0;
 	 	   	if(pInPos[14] == (chk_byte1) ) { printf(" "); } else { printf("*"); modified=1; pOutPos[14] = chk_byte1;}
 	   	 	if(pInPos[15] == (chk_byte2) ) { printf(" "); } else { printf("*"); modified=1; pOutPos[15] = chk_byte2;}
				if(modified==1) { corrected++; }
				printf("\n");

     	} else {
				// still display info for blocks we skip to re-calculate
        for (offset = 0; offset <= 13; offset++) { 
        	printf("%-2.2x",pInPos[offset]);
        }
       	printf("[%-2.2x%-2.2x]",pInPos[14],pInPos[15]);

				// display in ascii
				printf(" ");
        for (offset = 0; offset <= 15; offset++) { 
		    	if(isprint(((unsigned char)pInPos[offset]))) {
 		      	printf("%c", ((unsigned char)pInPos[offset]));
		      } else {
		      	printf(".");
		      }
	       	if(offset == 13) printf(" ");
        }
      	printf(" Desc %-4.4x, Checksum Skip\n",pageDesc[pageNumber]);
    	}
    }

		printf("\n");
		if(corrected == 0) {
			printf("No checksums where corrected, file is OK already, skipping save.\n");
		} else {
			printf("%d checksum(s) where corrected in this file.\n",corrected);
	 		// write out new file   
	    result = save_file((char *)argv[2],(unsigned char *)dataOut,(size_t)512);
		}

		// free file buffer
		if(pIn != 0) free(pIn);

		return 0;
}
         * */


    }
}
