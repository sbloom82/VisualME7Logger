
ME7Logger: Data Logger for ME7 (c) mki, 11/2010-03/2013 
=======================================================

HISTORY:
--------
Changes in ME7Logger v1.20, 24.03.2013, 10.07.2013:
- Read additional ecu characteristics file "my_<ecufile>" if it exists in same directory as <ecufile>.
  -> my_<ecufile> should be used to keep manually added definitions.
- Read additional log config file my_<ecu>_template.cfg from same directory as log config file.
  - my_<ecu>_template.cfg can be used to keep log entries for manually added definitions
- Optimization of logged data, e.g.:
    - logging multiple bitvariables which use the same byte will request only one byte from ecu.
    - logging two byte variables from neighbouring memory addresses will request only one merged location from ecu.
    The limits are now: 
      - up to 254 bytes, 
      - up to 127 different memory locations (1 or 2 bytes), 
      - up to 254 variables.

- Linux port added (using libftdi1 and libusb1 for FTDI cables)
   -> see directory bin-linux

- Shared library (DLL) added (.so/.dll) for those who want to implement own logging solutions
   -> see directories lib and lib-linux for description and example program
   A driver for WinLog (http://www.devtechnics.com/winlog.htm) using the ME7L shared library
   is available as example implementation as well.


Changes in ME7Info v1.20, 24.03.2013:
- Linux port added 
   -> see directory bin-linux
- Internal cleanup work



Changes in ME7Logger v1.17, 02.04.2012:
- Reworked general timing calculations, sync to full second
- Fixed local time in absolute timestamps (option -a)
- Supress logfile writing when output goes to stdout (option -R)
- Added option -m: Timestamp format milliseconds
- Added option -h: supress header in logfiles
- Added option -b: allow to set communication speed on command line
- Double slowinit supported (KWP1281 -> KWP2000), helps with "instrument cluster" problem

Changes in ME7Info v1.17, 02.04.2012:
- Reverted back to zwbasar_0 .. zwbasar_3
  (Array size NOT dependent on number of cylinders)
- Some variables added:
   + zwoutar_0 .. zwoutar_3
   + zwsol
   + redist / evz_austot
   + lamfaws_w / lamfawkr_w / lamfaw_w / lamfwl_w / lamrlmn_w
   + perffilt_w /perfmax_w    (CPU-Load, helps in finding out the max. logging capabilities
                               of your ECU. Around 90% CPU-Load might be a limit for the ECU.)


Changes in ME7Logger/ME7Info v1.10, 08.09.2011:
- Single sample option added, result is output just to the window, option -1
- More variables determined for images with code variant (missing tans(IAT) for one image)
- zwbasar_<xx> now really available for all available cylinders
- Some more variables added
- Some alias names added, some changed


Changes in ME7Logger v1.09, 12.08.2011:
- Experimental FTDI native driver support added, option -f
- Logger start time synchronization added, option -t
- Logger absolute timestamp added, option -a
- Allow TAB's also in trace config files

Changes in ME7Info v1.09, 12.08.2011:
- zwbasar_<xx> for all available cylinders
- Limit wkraa_<xx> to available cylinders
- Minor changes, some variables added



Main Features:
==============
o The logger works with (dumb) Serial-to-K-Line cables for connection with the car.
o Also supports FTDI based USB cables (using FTD2XX.DLL / libftdi1.so).
  USB cables (in dumb mode) can be used as serial interface also by 
    - using a virtual comport (VCP) driver under Windows
    - using kernel module ftdi_sio (/dev/ttyUSB0) under Linux.

o Allows to log a high number of variables at high samplerates (minimal usage of ecu's cpu load and datarates on K-Line for logging).

o A maximum of 254 freely selectable variables can be logged in parallel.
o A maximum of 254 bytes can be logged in parallel.
o A maximum of 127 different memory locations (1 or 2 bytes) can be logged in parallel.
    In practice, 254 different variables could be logged only if they are all single Byte variables
    and always two of them are located at neighbour addresses (to keep the limit of 127 different memory locations).

o The sample rate can be selected between 1 and 50 samples/second.
  The reachable sample rate depends on number of logged bytes and the used communication speed.
  Also the ECU's performance has an impacts, there exist ME7 with different CPU frequencies.
  I recommend to log the CPU load of your ecu to find out which logging load is possible without problems.

o Open system: it is possible to modify existing definitions or to add own definitions in ecu characteristics (properties).

o With the logger comes a tool to derive the initial ecu characteristics from the ecu image, this works for ME7.1/ME7.5 images.
  You should have a copy of your ecu image available in advance to logging to generate the ecu characteristics.

o The most important variables for engine logging/tuning are instantly available for logging.

o Ecu identification is checked automatically at connection time. Helps to avoids useless logging sessions due to erroneous use of wrong ecu characteristics.

o The logger is a small tool. No GUI, no online value display, no gauges, just logging for offline analysis.
  But therefore also small cpu requirements.

o Was developed under WinXP, but runs under Win95 .. Win7.
o Also a Linux port is available now (tested with Kernels 2.6.31, 3.7.10).

o The logger is implemented as command line based tool, one-click logging sessions are possible by use of shortcuts on the desktop.

o A new Logfile is opened automatically for every logging session.
  Output is appended if using an already existing logfile, no accidental overwriting of older logs.

o Output format of the logger is CSV.

o The names of logging variables are internal ME7 names, e.g. 'nmot' (engineSpeed) or 'ldtvm' (N75 duty cycle).
  The logged values can be easily mapped to the official ME7 documentation.
  For those not familiar with the internal variable names, also a more readable alias name is present or 
  can be added (adaptable to your own preferences).


o Logging continues until you stop it by pressing Control-C in the terminal/stop the application or until you interrupt
  communication with the car (turn off/on ignition).
o After each full second of logging, the logdata will be flushed to file.
  If you stop the logging, data up to the last complete logged second is contained in the logfile
  (Use 'realtime' logging options to have each log record written immediately to disk).
o To make visible the logging progress, the logger prints a '.' on the terminal after each full second as long as
  sample timing can be kept. After each full minute you will see a '|' and a new line is started on the terminal.
  If the sample rate is set too high, you will see a 'o' or 'O' to indicate an increasingly smaller time budget.
  A probable loss of acurate timing due to too high requested sampling rate/number of variables is indicated by a '!'.


o A shared library (DLL) of the logger is available also. The usage of this library is described in a short example program.
  Plase read LICENSE.txt and DISCLAIMER.txt before using the shared library.  

Please read also DISCLAIMER.txt before you start logging with a car!!!


Offline logfile evaluation/analysis
-----------------------------------
For logfile evaluation I recommend to use nyet's EcuXPlot (see http://nyet.org/cars/ECUxPlot)!
This tool parses (amongst many other formats) logfiles of ME7Logger and performs analysis of your logs.


Connection method "double slowinit"
-----------------------------------
The connection method "double slowinit" is useful if the instrument cluster blocks/disturbs
communication between logger and ECU  when using normal slowinit/fastinit.
The "double slowinit" works only if the engine is not running.  You need first to connect the logger,
then start engine with this connect method.
To use "double slowinit", set the following in the [Communication] section of the .ecu file:
Connect      = SLOW-0x00 
DoubleDelay  = 12           ; Possible values 1 .. 100 (delay in 100ms)



Contents of the directories:
============================
doc     - contains the documentation
bin     - the binaries, ME7Info and ME7Logger
defs    - ME7 map file (me7_std.map), alias names definition (me7_alias.map)
ecus    - ECU characteristics files (.ecu), generated by tool ME7Info
logs    - log config files (.cfg) and the logfiles (.csv)
lib     - Shared library, header file, example program

Linux/* - same directories for Linux port


Quickstart:
===========
Unpack the zip file including subdirectories to a directory of your choice, e.g. C:\Program Files\ME7.
You should have the five subdirectories as described above.
All tools are command line tools, so you should open a command window (cmd.exe or a cygwin shell window)
and type the name of the command to run the below described tools. 
If you add the bin directory to the path variable, you don't need to specify the full path of commands,
e.g. put "set PATH=%PATH%;C:\Program Files\ME7\bin" into a batch file that you can execute once.

1. Have your ECU binary available.
   Read it out of the ecu (use the Nefmoto Flasher!) or get a copy from somewhere else. 
   The stock binary is sufficient (as long as you don't change variable addresses in the code, which is normally NOT done).

2. Create a characteristics file for your ECU.
   Feed your ecu's binary to the ME7Info tool, this will generate your characteristics file in the ecus-directory, e.g.:
     ME7Info  C:\Binaries\my_binary.bin 

   The characteristics file for your ecu (my_binary.ecu) is created out of the binary file,
   a generic map file "me7_std.map" is used during this process (found in the defs directory).
   It contains basic information about many variables of the ECU for logging, already including the most important ones.
   If something is missing or does not fit, you can hand-edit this file lateron, e.g. to add more variables,
   to fix conversion formulas/units, or to change alias names.
   The created characteristics file is stored automatically in the "ecus" directory, allowing the logger to find the file
   just by specifying the filename without having to give a full path lateron.

3. Create a log config file for your ECU and your special logging needs.
    Make a copy of the example log config file from the logs directory:
      cd C:\Program Files\ME7\logs
      copy example_log.cfg my_log.cfg
    Edit my_log.cfg:
      - Reference your ECU's characteristics file (my_binary.ecu):
          ECUCharacteristics = my_binary.ecu
      - Change the default sampling speed as you need (1-50 samples per second):
          SamplesPerSecond = 10
      - Uncomment the variable names you want to log (it may be max. 127), comment out unneeded variables by adding a ';' in the beginning of the line
        or add more variables (names to be found in the characteristics file of your ecu in the "ecus" directory).
      That's all to define a logging session.
  Alternative:
    Create a template logging config which contains all possible variables:
      ME7Info -t  C:\Binaries\my_binary.bin     -> this generates "my_binary_template.cfg"
    Make a copy of the template log config and uncomment the variables you want to log in the copy.

4. Connect your PC/Laptop with a Serial->K adapter or a FTDI USB cable to the car, turn on ignition.
   => See DISCLAIMER.txt for safety instructions!

5. Start the ME7Logger tool, giving your log config file as argument, probably need to specify also the COMport of your cable:
     ME7Logger  -p COM4  my_log.cfg   (for serial cables)
     ME7Logger  -f  my_log.cfg         (for FTDI cables)
   -> Logging will start after connection and preparation has finished,
   the logger prints a '.' after each complete second of logging.
   Press Control-C to stop logging at anytime.

6. Look at the contents of your logfile in the logs directory with an editor or import it into Excel:
     edit my_binary_<date>_<time>.csv 
   When importing into excel, be aware of possible problems with '.' and ',' for decimal numbers
   due to language settings.
   For analysis of bigger amounts of data, you better use a postprocessing tool like EcuXPlot.


Shortcut for one-click logging session:
---------------------------------------
To start a fixed logging session with one mouse-click, you can create a shortcut on the desktop.
Put something similar to the following example command in the shortcut as Target:
%SystemRoot%\system32\cmd.exe /K C:\Program Files\ME7\bin\ME7Logger.exe my_log.cfg

This will start logging with a fixed logging configuration 'my_log.cfg'



Search path for files:
-----------------------
- The map file is searched in the defs directory.
  To use a different file than the default file me7_std.map,
  use the command line option '-m <mapfile>'.

- Ecu characteristics files are searched in ecus directory if only the filename was given.
  To find them somewhere else, specify also the path to the file in log configs.



Usage of ME7Info:
-----------------
ME7Info v1.20 (c) mki, 11/2010-03/2013
Usage: ME7Info [-c | -g | -t]  [-n] [-m <mapfile>] [-a <aliasfile>] [-o <outfile>]  <binfile>

Options:
  -c              : generate communication parameters overview on stdout
  -g              : generate measgroup variables overview on stdout
  -t              : generate logging config template in logs-directory

  -n              : sort characteristics file numerically (by address),
                    default is to sort by variable name
  -m <mapfile>    : map file to use, default is me7_std.map out of defs-directory
  -a <aliasfile>  : alias file to use, default is me7_alias.map out of defs-directory
  -o <outfile>    : output file to generate,
                    default is to create <binfile>.ecu in ecus-directory
                    or <binfile>.cfg in logs-directory when creating a logging config template (-t)

  <binfile>       : the input file, a ME7 image



Usage of ME7Logger:
-------------------
ME7Logger v1.20 (c) mki, 12/2010-03/2013
Usage: ME7Logger  [-p <comport> | -f [-S<serno>|-D<desc>|-L<loc>]]
           [-s <sps>] [-b <baudrate>] [-t] [-a|-m] [-h] [-1] [-R] [-r] [-o <logfile>]  <logconfig>
Options:
    -p <comport>  : interface is connected to <comport> (default = COM2)
    -f            : use FTDI based interface
      -S<serial>  : open FTDI interface with serial number <serial>
      -D<desc>    : open FTDI interface with description <desc>
      -L<loc>     : open FTDI interface with USB location <loc> (in hex)

    -s <sps>      : set a samplerate (samples/second), overrides rate from log config file
    -b <baudrate> : set a logging baudrate for communication, overrides baudrate from ecu file
    -t            : time synchronization: logging starts with next full second of system time
    -a            : print absolute timestamps (default: seconds.milliseconds since start)
    -m            : print millisecond timestamps (default: seconds.milliseconds since start)
    -h            : supress printing of header information in logfile
    -1            : just read one measurement and then stop
    -r            : write logged data to file immediately (for realtime postproc),
                    default is to flush only every full second to disk
    -R            : show logged data on screen (realtime display), this turns off
                    writing to logfile if not requested explicitly via -o <logfile>
    -o <logfile>  : write log to file <logfile>, output is appended if logfile already exists
                    default logfile is '<logconfig>_<date>_<time>.csv' in logs-directory
    <logconfig>   : use log configuration file <logconfig> (from logs-directory)



Description of the log config file:
-------------------------------------
In the directory 'logs' you find an example log config file 'example_log.cfg', have a look at it.
With the log config you specify three things for a logging session:
1. the ecu characteristics file to be used 
    Provide the name of the ecu characteristics file for your ecu.
2. the samplerate 
    Specify the number of samples to be taken per second, can be 1..50.
    This can be overridden by the command line parameter "-s <sps>" of the logger.
3. the list of variables which shall be logged 
    Use the variable names as found in the ecu characteristics file, one variable per line.
    Optionally you can add an alias name for each variable which will override the default alias name
    out of the ecu characteristics file.  All aliases will be printed on a headerline in the resulting logfile.

A semicolon (;) starts a comment line.
You can just comment out the variables you don't want to get logged and easily re-insert them lateron.



Description of ecu characteristics file:
----------------------------------------
In directory ecus you find an example ecu characteristics file, example.ecu. Viewing the example file
in parallel to reading here makes it easier to follow the description.
The ecu characteristics file for your specific ecu is automatically generated (tool ME7Info) in a first step,
but you can hand-edit it lateron.
It contains four sections: [Version], [Communication], [Identification], and [Measurements].

In the [Version] section, the used tool version is stored. Normally you should not make changes here.

In the [Communication] section is specified how the logger connects to and communicates with your ecu.
This section gets filled automatically for your specific ecu image during creation of the charactericstics file.
In rare cases it could happen that the values can't be derived, you need to manually adapt in this section
if the logger can't connect to your ecu.
I did not implement an automatic connection mode which tries different addresses/connect modes, 
since I think it would be annoying to wait several seconds until the logger tries with the right
address/mode combination for your ecu.
Per default slow init is used, but you can change here to fast init and try if this also works for your ecu,
if you want to save some time at logger startup.
You can adapt in this section also the baudrate which will be used during logging, an automatic speed setting
is not yet implemented. The default baudrate of 56000 should work with almost all ecus/interfaces.
Some of the possible baudrates are only working on specific ecus/interfaces. If you face communication problems
during logging, you can try to lower the baudrate here.

In the [Identification] section several strings are defined which the logger will compare against the ecu's 
identification when connecting. You can comment out these strings to circumvent the comparisons if you want.
Normally they should be left present to asure the right characteristics file is used during logging.
Only in case you change some identification strings in your ecu (e.g. during tuning process), you should comment
out the corresponding string in the characteristics file.
If the identification strings don't match the ones from the ecu, the logger will stop.

Finally, in the [Measurements] section there is one line for each measurement variable that is prepared to be logged.
Each line contains:
- the variable name (ME7 internal name)
- an alias name, surrounded by braces '{}' (more readable name)
- the address of the variable in the ecu's ram (can also be in flash memory, if you want to log constants)
- the size of the variable in bytes (only 1 or 2 bytes are allowed currently)
- a bitmask (0x0000 for normal variables or something unequal zero for condition variables, e.g. see B_bl (brake pedal engaged))
- a physical unit, surrounded by braces '{}'
- conversion parameters (S, I, A, B) to convert from internal to physical values, see below
- a comment, surrounded by braces '{}' (this is currently in german, it was derived from damos/asap2 files).

The conversion parameters (S, I, A, B) describe how a variable value is converted into a physical value:
S:    0 = internal value is unsigned, 1 = internal value is signed
I:    0 = normal conversion, 1 = inverted conversion
A:    conversion factor
B:    conversion offset

    <physical> = (A * <internal>) - B   for normal conversion
    <physical> = A / (<internal> - B)   for inverted conversion

You can change the conversion parameters at your needs to adapt the values for different units
(e.g. to change from g/s to kg/h or from km/h to mph).

The conversion values have been derived from damos files and are stored in the generic mapfile me7_std.map.
Most values have the same conversion factors across all ME7 models, but for some variables there are 
different factors. For injection timing parameters the ME7Info tool automatically cares to select the correct
conversion factors!
In case there are conversions which you need to change, you can create a copy of the generic ME7 map file (me7_std.map) 
and fix conversions in this copy. This makes sense if you want to create lateron multiple characteristics files 
from different ecu images with these changed conversions.
Use "ME7Info -m <my-mapfile>" when generating new characteristics files to make use of a new mapfile.

On the other hand you can edit the conversion parameters just in a single ecu's characteristics file.

Additionally, you can add new variables to a characteristics file. For example, if you find an interesting variable
in your ecu's image which you want to log, you can add an additional descriptor line (using the same format as the 
existing ones). Just take care, that the variable name is unique.
After you have added a variable to the descriptor file, you can use the name of this new variable immediately in your
log config files.



Description of the standard map file:
-------------------------------------
This is derived from information in damos/asap2 files, therefore the comments are in german!!
This file lists all variables that are prepared in ME7 for logging and that can be automatically recognized.
The table 1 in section "TKMWL - Testerkommunikation, Messwerte lesen" (Tester communication, reading of measurement values)
in the Funktionsrahmen document lists most of these variables.
The ME7Info tool searches in the examined ecu binary for each variable the address, size and bitmask.
Mapping to a name, conversion formula and unit is done by using the standard map file.

The conversion from internal to physical values depends on the cpu frequency of the ecu for certain variable types
(injection timing parameters).  There exist four different cpu speeds (20, 24, 32, 40 MHz) for which the internal 
tick time is different, this influences the conversion from internal ticks to milliseconds. The ME7Info tool automatically
determines the correct conversion factor for all injection timing parameters and writes them to generated ecu characteristics files.



Description of the standard alias file:
---------------------------------------
This defines default aliases for the ME7 variables. They are used during the automatic ecu characteristic file creation.
To map alias names for cylinder dependent values (e.g. KnockVoltageCyl3), the ME7Info tool automatically determines
the number of cylinders and the ignition order in the image. In alias names ending with '$1' to '$8', the correct 
cylinder number gets inserted when the information is written into an ecu characteristics file.
Therefore a single alias map file can be used for 4-, 6-, or 8-cylinder engines.



APPENDIX
========
Load comparison of different mechanisms to read ecu variables:
--------------------------------------------------------------
CPU cycles of different message handling functions have been measured.  The actual message transfer via serial K-Line
is handled in background, using DMA transfer (PECC). Therefore the total message size is not relevant for CPU load.
Number of message bytes has been counted without header and checksum.

Mechanism:                                          CPU cycles:     MsgBytes TX/RX:     Remarks:
----------                                          -----------     ---------------     --------
readDataByLocalIdent (8 variables)                       560            2/26            (1)

readMemoryByAddress (memory block,   2 bytes)            153            5/3             (!)
readMemoryByAddress (memory block, 126 bytes)          2,385            5/127           (!)
readMemoryByAddress (memory block, 254 bytes)          4,689            5/255           (!)

readDataByDynDefLocalIdent (  1 variable,    2 bytes)    227            2/4             (2)
readDataByDynDefLocalIdent ( 63 variables, 126 bytes)  9,500            2/128           (2)
readDataByDynDefLocalIdent (126 variables, 252 bytes) 19,000            2/254           (2) (*)

ME7Logger request(  1 variable,    2 bytes)               58            1/3
ME7Logger request( 63 variables, 126 bytes)            1,112            1/127
ME7Logger request(127 variables, 254 bytes)            2,200            2/255           (*)

(1) This service is used by VCDS (KWP2000 mode), it is present only on a small number of ecus
    which don't offer KWP1285 anymore.
(2) This service is used by EcuExplorer.
(!) Only a contiguous block can be read with one request. Variables spreaded over memory have to
    be read by multiple readMemoryByAddress requests.
(*) This is a worst-case scenario with two-byte variables only.
    Normally you have a mixture of one-byte and two-byte variables, leading to a lesser total data size.
    A realistic mixture results in about 220 bytes, giving 1900 clock cycles for the ME7Logger request.

As you can see from the above figures, ME7Logger uses an efficient mechanism to collect variables.
Compared to other mechanisms this needs a much lower number of cpu cycles per log request, as well 
as a low number of bytes transfered on the K-line.
Even a high number of logged variables will not cause problems with the timing deadlines of the ecu.


