using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace VisualME7Logger.Output
{
    public class EEProm
    {
        public string ApplicationPath = string.Empty;
        public string BinPath = string.Empty;
        public string COMPort = "1";
        public string Baudrate = string.Empty;

        private EEProm() { }
        public EEProm(string ME7LoggerDirectory) : this()
        {
            ApplicationPath = Path.Combine(ME7LoggerDirectory, "ME7_95040.exe");
        }

        public void Read(XElement element)
        {
            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "ApplicationPath":
                        this.ApplicationPath = att.Value;
                        break;
                    case "BinPath":
                        this.BinPath = att.Value;
                        break;
                    case "COMPort":
                        this.COMPort = att.Value;
                        break;
                    case "Baudrate":
                        this.Baudrate = att.Value;
                        break;
                }
            }
        }

        public XElement Write()
        {
            XElement ele = new XElement("EEProm");
            ele.Add(new XAttribute("ApplicationPath", this.ApplicationPath));
            ele.Add(new XAttribute("BinPath", this.BinPath));
            ele.Add(new XAttribute("COMPort", this.COMPort));
            ele.Add(new XAttribute("Baudrate", this.Baudrate));
            return ele;
        }

        public EEProm Clone()
        {
            EEProm clone = new EEProm();
            clone.ApplicationPath = this.ApplicationPath;
            clone.BinPath = this.BinPath;
            clone.COMPort = this.COMPort;
            clone.Baudrate = this.Baudrate;
            return clone;
        }

        #region emprom output
        /* 
         * Allows to program ME7 ECU's EEPROM (95040).
This software needs a dumb serial to k-line cable for connection with the ecu.

Usage: me7_95040 [OPTIONS]... [FILE]...
 -p, --comport COMPORT     Set COMPORT.
 -b, --baudrate BAUDRATE   Set BAUDRATE, default: 10400.
                           Allowed baud rates: 9600, 10400, 19200, 57600.
     --OBD                 Use this option to read the EEPROM over OBD port.
                           Currently, only read is supported in OBD mode.
     --bootmode            Use this option to program the EEPROM in boot mode.
 -r, --read                Save EEPROM contents to file.
 -w, --write               Write file to EEPROM.
 -s, --screen              Print EEPROM contents to screen.
     --help                Display this help and exit.

Usage example:
   Read EEPROM over OBD port:         $ me7_95040 --OBD -r -p 1 95040.bin
   Print EEPROM contents to srcreen:  $ me7_95040 --OBD -p1 --screen
   Write file to EEPROM in bootmode:  $ me7_95040 --bootmode -wp1 95040.bin

It's free for hobby use.
This software is provided "as is", with NO WARRANTY.
email: agv.tuning@gmail.com
         */
        #endregion

        public EEPromResult ReadEEProm(bool bootmode)
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(
                   this.ApplicationPath,
                   string.Format("{0} -r -p {1}{2} ", bootmode ? "--bootmode" : "--OBD", this.COMPort, string.IsNullOrEmpty(this.Baudrate) ? "" : " -b " + this.Baudrate) +
                   "\"" + this.BinPath + "\"");
                //p.StartInfo.UseShellExecute = false;
                //p.StartInfo.CreateNoWindow = true;
                //p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.RedirectStandardError = true;
                if (p.Start())
                {
                    p.WaitForExit();
                    return new EEPromResult() { Success = p.ExitCode == 0, Output = p.ExitCode != 0 ? "Exit Code " + p.ExitCode : "Success" };
                }
                else
                {
                    return new EEPromResult() { Success = false, Output = "Unable to start process" };
                }
            }
            catch (Exception e)
            {
                return new EEPromResult() { Success = false, Output = e.Message };
            }
        }

        public EEPromResult WriteEEProm()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(
                   this.ApplicationPath,
                   string.Format("--bootmode -w -p {0}{1} ", COMPort, string.IsNullOrEmpty(Baudrate) ? "" : " -b " + Baudrate) +
                   "\"" + this.BinPath + "\"");
                p.StartInfo.UseShellExecute = false;
                //p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                if (p.Start())
                {
                    p.WaitForExit();
                    return new EEPromResult() { Success = p.ExitCode == 0, Output = p.StandardOutput.ReadToEnd() + "\r\n" + p.StandardError.ReadToEnd() };
                }
                else
                {
                    return new EEPromResult() { Success = false, Output = "Unable to start process" };
                }
            }
            catch (Exception e)
            {
                return new EEPromResult() { Success = false, Output = "Error. " + e.Message };
            }
        }

        public class EEPromResult
        {
            public bool Success = true;
            public string Output = "";
        }

        public string GetSKC()
        {
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        if (fileBytes[0x32] == 0xFF && fileBytes[0x33] == 0xFF)
                        {
                            return "NO SKC";
                        }
                        string hex1 = BitConverter.ToString(new byte[] { fileBytes[0x32] });
                        string hex2 = BitConverter.ToString(new byte[] { fileBytes[0x33] });
                        return string.Format("0{0:0000}", int.Parse(hex2 + hex1, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        return "Incorrect File Size.  Expected 512 bytes.";
                    }
                }
                return "File not found";
            }
            catch (Exception e)
            {
                return "Error. " + e.Message;
            }
        }

        public EEPromResult SetSKC(byte[] fileBytes, string SKC)
        {
            if (!string.IsNullOrEmpty(SKC))
            {
                if (SKC.Length != 5)
                {
                    return new EEPromResult() { Success = false, Output = "Incorrect SKC Length, expected 5 characters." };
                }
                if (!SKC.StartsWith("0"))
                {
                    return new EEPromResult() { Success = false, Output = "SKC must begin with '0'" };
                }
                short skcInt;
                if (!short.TryParse(SKC, out skcInt))
                {
                    return new EEPromResult() { Success = false, Output = "Invalid SKC" };
                }
                byte[] skcBytes = BitConverter.GetBytes(skcInt);

                fileBytes[0x32] = skcBytes[0];
                fileBytes[0x33] = skcBytes[1];
                fileBytes[0x42] = skcBytes[0];
                fileBytes[0x43] = skcBytes[1];

                return new EEPromResult() { Success = true, Output = string.Format("SKC set as {0}", SKC) };
            }
            return new EEPromResult();
        }

        #region immo notes
        //pin-skc = 0x32-0x33 & 0x42-0x43. To decode flip bytes, convert to dec, add leading zero
        //VIN = 0xB5-0xB9, 0xD0-0xDB & 0xC5-0xC9, 0xE0-0xEB
        //IMMO Key (key shared w/ cluster) = 0x34-0x3A && 0x44-0x4A
        //IMMO ID = 0xDC, 0xF0-0xFC & 0xEC, 0x100-x10C
        //IMMO ON/OFF=  0x12 & 0x22 (01 on, 02 off)

        /*immo off is very easy . 12 and 22 are 01, change them to 02. Then, subtract 1 from the checksum of that line. You'll know that's them because they will be sequential

05 01 01 xx xx xx xx xx xx xx FC AA
05 01 01 xx xx xx xx xx xx xx FB AA

change to 

05 01 02 xx xx xx xx xx xx FB AA
05 01 02 xx xx xx xx xx xx FA AA*/
        #endregion

        public string GetVIN()
        {
            //VIN = 0xB5-0xB9, 0xD0-0xDB & 0xC5-0xC9, 0xE0-0xEB
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        byte[] vinBytes = new byte[] { fileBytes[0xB5], fileBytes[0xB6], fileBytes[0xB7], fileBytes[0xB8], fileBytes[0xB9], fileBytes[0xD0], fileBytes[0xD1], fileBytes[0xD2], fileBytes[0xD3], fileBytes[0xD4], fileBytes[0xD5], fileBytes[0xD6], fileBytes[0xD7], fileBytes[0xD8], fileBytes[0xD9], fileBytes[0xDA], fileBytes[0xDB] };
                        byte[] vinBytes2 = new byte[] { fileBytes[0xC5], fileBytes[0xC6], fileBytes[0xC7], fileBytes[0xC8], fileBytes[0xC9], fileBytes[0xE0], fileBytes[0xE1], fileBytes[0xE2], fileBytes[0xE3], fileBytes[0xE4], fileBytes[0xE5], fileBytes[0xE6], fileBytes[0xE7], fileBytes[0xE8], fileBytes[0xE9], fileBytes[0xEA], fileBytes[0xEB] };
                        string s = "";
                        for (int i = 0; i < vinBytes.Length; ++i)
                        {
                            s += (char)vinBytes[i];
                            if (vinBytes[i] != vinBytes2[i])
                            {
                                return "Error VIN mirror mismatch";
                            }
                        }
                        return s;
                    }
                    else
                    {
                        return "Incorrect File Size.  Expected 512 bytes.";
                    }
                }
                return "File not found";
            }
            catch (Exception e)
            {
                return "Error. " + e.Message;
            }
        }

        public EEPromResult SetVIN(byte[] fileBytes, string VIN)
        {
            if (!string.IsNullOrEmpty(VIN))
            {
                int[] vinBytes = new int[] { 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB };
                int[] vinBytes2 = new int[] { 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB };

                if (VIN.Length != 17)
                {
                    return new EEPromResult() { Success = false, Output = "VIN must be 17 characters in length" };
                }

                for (int i = 0; i < VIN.Length; ++i)
                {
                    fileBytes[vinBytes[i]] = (byte)VIN[i];
                    fileBytes[vinBytes2[i]] = (byte)VIN[i];
                }
                return new EEPromResult() { Success = true, Output = string.Format("VIN set as {0}", VIN) };
            }
            return new EEPromResult();
        }

        public string GetImmoID()
        {
            //IMMO ID = 0xDC, 0xF0-0xFC & 0xEC, 0x100-x10C
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        byte[] immoIDBytes = new byte[] { fileBytes[0xDC], fileBytes[0xF0], fileBytes[0xF1], fileBytes[0xF2], fileBytes[0xF3], fileBytes[0xF4], fileBytes[0xF5], fileBytes[0xF6], fileBytes[0xF7], fileBytes[0xF8], fileBytes[0xF9], fileBytes[0xFA], fileBytes[0xFB], fileBytes[0xFC] };
                        byte[] immoIDBytes2 = new byte[] { fileBytes[0xEC], fileBytes[0x100], fileBytes[0x101], fileBytes[0x102], fileBytes[0x103], fileBytes[0x104], fileBytes[0x105], fileBytes[0x106], fileBytes[0x107], fileBytes[0x108], fileBytes[0x109], fileBytes[0x10A], fileBytes[0x10B], fileBytes[0x10C] };
                        string s = "";
                        for (int i = 0; i < immoIDBytes.Length; ++i)
                        {
                            s += (char)immoIDBytes[i];
                            if (immoIDBytes[i] != immoIDBytes2[i])
                            {
                                return "Error ImmoID mirror mismatch";
                            }
                        }
                        return s;
                    }
                    else
                    {
                        return "Incorrect File Size.  Expected 512 bytes.";
                    }
                }
                return "File not found";
            }
            catch (Exception e)
            {
                return "Error. " + e.Message;
            }
        }

        public EEPromResult SetImmoID(byte[] fileBytes, string immoID)
        {
            if (!string.IsNullOrEmpty(immoID))
            {
                int[] immoIDBytes = new int[] { 0xDC, 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC };
                int[] immoIDBytes2 = new int[] { 0xEC, 0x100, 0x101, 0x102, 0x103, 0x104, 0x105, 0x106, 0x107, 0x108, 0x109, 0x10A, 0x10B, 0x10C };

                if (immoID.Length != 14)
                {
                    return new EEPromResult() { Success = false, Output = "IMMO ID must be 14 characters in length" };
                }

                for (int i = 0; i < immoID.Length; ++i)
                {
                    fileBytes[immoIDBytes[i]] = (byte)immoID[i];
                    fileBytes[immoIDBytes2[i]] = (byte)immoID[i];
                }
                return new EEPromResult() { Success = true, Output = string.Format("ImmoID set as {0}", immoID) };
            }
            return new EEPromResult();
        }

        public byte[] GetImmoData()
        {
            //IMMO Key (key shared w/ cluster) = 0x34-0x3A && 0x44-0x4A
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        byte[] immoDataBytes = new byte[] { fileBytes[0x34], fileBytes[0x35], fileBytes[0x36], fileBytes[0x37], fileBytes[0x38], fileBytes[0x39], fileBytes[0x3A] };
                        byte[] immoDataBytes2 = new byte[] { fileBytes[0x44], fileBytes[0x45], fileBytes[0x46], fileBytes[0x47], fileBytes[0x48], fileBytes[0x49], fileBytes[0x4A] };

                        for (int i = 0; i < immoDataBytes.Length; ++i)
                        {
                            if (immoDataBytes[i] != immoDataBytes2[i])
                            {
                                return null;
                            }
                        }
                        return immoDataBytes;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public EEPromResult SetImmoData(byte[] fileBytes, byte[] immoData)
        {
            if (immoData != null)
            {
                int[] immoDataBytes = new int[] { 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A };
                int[] immoDataBytes2 = new int[] { 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A };

                if (immoData.Length != 7)
                {
                    return new EEPromResult() { Success = false, Output = "IMMO Data must be 7 bytes in length" };
                }

                for (int i = 0; i < immoData.Length; ++i)
                {
                    fileBytes[immoDataBytes[i]] = immoData[i];
                    fileBytes[immoDataBytes2[i]] = immoData[i];
                }
                return new EEPromResult() { Success = true, Output = "Immo Data set" };
            }
            return new EEPromResult();
        }

        public bool? ImmoEnabled()
        {
            /*immo off is very easy . 12 and 22 are 01, change them to 02. Then, subtract 1 from the checksum of that line. You'll know that's them because they will be sequential

05 01 01 xx xx xx xx xx xx xx FC AA
05 01 01 xx xx xx xx xx xx xx FB AA

change to 

05 01 02 xx xx xx xx xx xx FB AA
05 01 02 xx xx xx xx xx xx FA AA*/
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        byte immoFlag = fileBytes[0x12];
                        byte immoFlag2 = fileBytes[0x22];
                        if (immoFlag != immoFlag2)
                        {
                            return null;
                        }
                        if (immoFlag != 0x01 && immoFlag != 0x02)
                        {
                            return null;
                        }
                        return immoFlag == 0x01;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public bool? DeathCodeOn()
        {
            try
            {
                FileInfo file = new FileInfo(this.BinPath);
                if (file.Exists)
                {
                    if (file.Length == 512)
                    {
                        byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                        byte deathCodeByte = fileBytes[0x1C];
                        byte deathCodeByte2 = fileBytes[0x2C];
                        if (deathCodeByte != deathCodeByte2)
                        {
                            return null;
                        }
                        if (deathCodeByte != 0x33 && deathCodeByte != 0x00)
                        {
                            return null;
                        }
                        return deathCodeByte == 0x33;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public EEPromResult EnableImmo(byte[] fileData)
        {
            fileData[0x12] = 0x01;
            fileData[0x22] = 0x01;
            return new EEPromResult() { Output = "Immo Enabled"};
        }
       
        public EEPromResult DisableImmo(byte[] fileData)
        {
            fileData[0x12] = 0x02;
            fileData[0x22] = 0x02;
            return new EEPromResult() { Output = "Immo Disabled" };
        }
       
        public EEPromResult FixDeathCode(byte[] fileBytes)
        {
            fileBytes[0x1C] = 0x00;
            fileBytes[0x2C] = 0x00;
            return new EEPromResult() { Output = "Death Code Fixed" };
        }

        public EEPromResult CalculateChecksum(byte[] fileData)
        {
            try
            {
                int pageNumber = 0;
                int CSMask = 0x01;
                int CBMask = 0x40;
                int totalChecksums = 0;
                int checksumsCorrected = 0;

                int[] pageDesc = new int[32] { 
                    0xFF18, 0x0017, 0x0117, 0x0207, 0x0307, 0x0437, 0x0533, 0x06B7, 
                    0x06F7, 0x07B3, 0x07F3, 0x08B7, 0x08F7, 0x09B3, 0x09F3, 0x0AB3, 
                    0x0AF3, 0x0B32, 0x0B10, 0x0B10, 0x0B10, 0x0C37, 0x0D33, 0x0E33, 
                    0x0F33, 0x1033, 0x1133, 0x1233, 0x1235, 0x1235, 0x13B7, 0x13F7 };

                while (true)
                {
                    if (pageNumber == pageDesc.Length)
                        break;

                    byte[] dataIn = new byte[16];
                    byte[] dataOut = new byte[16];
                    Array.Copy(fileData, pageNumber * 16, dataIn, 0, 16);

                    totalChecksums++;

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

                        if (dataOut[14] != CRCbytes[0] || dataOut[15] != CRCbytes[1])
                        {
                            checksumsCorrected++;
                            dataOut[14] = CRCbytes[0];
                            dataOut[15] = CRCbytes[1];
                        }
                    }
                    Array.Copy(dataOut, 0, fileData, pageNumber * 16, 16);
                    pageNumber++;
                }

                return new EEPromResult() { Success = true, Output = string.Format("{0} of {1} Checksums corrected.", checksumsCorrected, totalChecksums) };
            }
            catch (Exception e)
            {
                return new EEPromResult() { Success = false, Output = e.ToString() };
            }
        }

        public EEPromResult WriteFile(string filePath, string vin, string skc, string immoID, byte[] immoData, bool? enableImmo, bool fixDeathCode, bool correctChecksum)
        {
            try
            {
                FileInfo fi = new FileInfo(this.BinPath);
                if (fi.Length != 512)
                {
                    throw new ApplicationException("EEPROM file must be 512 bytes in size. Yours is: " + fi.Length + " long.");
                }

                byte[] fileBytes = File.ReadAllBytes(this.BinPath);
                byte[] newFileBytes = new byte[fileBytes.Length];
                Array.Copy(fileBytes, newFileBytes, fileBytes.Length);

                EEPromResult retval = new EEPromResult() { Success = true, Output = string.Empty };

                EEPromResult localResult = this.SetVIN(newFileBytes, vin);
                retval.Success = retval.Success && localResult.Success;
                retval.Output += localResult.Output + "\r\n";

                localResult = this.SetSKC(newFileBytes, skc);
                retval.Success = retval.Success && localResult.Success;
                retval.Output += localResult.Output + "\r\n";

                localResult = this.SetImmoID(newFileBytes, immoID);
                retval.Success = retval.Success && localResult.Success;
                retval.Output += localResult.Output + "\r\n";

                localResult = this.SetImmoData(newFileBytes, immoData);
                retval.Success = retval.Success && localResult.Success;
                retval.Output += localResult.Output + "\r\n";

                if (enableImmo.HasValue)
                {
                    if (enableImmo.Value)
                    {
                        localResult = this.EnableImmo(newFileBytes);
                    }
                    else
                    {
                        localResult = this.DisableImmo(newFileBytes);
                    }
                    retval.Success = retval.Success && localResult.Success;
                    retval.Output += localResult.Output + "\r\n";
                }

                if (fixDeathCode)
                {
                    localResult = this.FixDeathCode(newFileBytes);
                    retval.Success = retval.Success && localResult.Success;
                    retval.Output += localResult.Output + "\r\n";
                }

                if (correctChecksum)
                {
                    localResult = this.CalculateChecksum(newFileBytes);
                    retval.Success = retval.Success && localResult.Success;
                    retval.Output += localResult.Output + "\r\n";
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(newFileBytes, 0, newFileBytes.Length);
                    retval.Output += string.Format("\r\nFile saved successfully as {0}", filePath);
                }

                return retval;
            }
            catch (Exception e)
            {
                return new EEPromResult() { Success = false, Output = e.ToString() };
            }
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
