--------------------------------------------------------------------------------
Specification of DDE drivers for Orbitron                           www.stoff.pl
http://www.stoff.pl/downloads.php
--------------------------------------------------------------------------------

Contents:
  1. Introduction
  2. Technical data
  3. Data format
       3.1. TrackingData item
       3.2. TrackingDataEx item
  4. My DDE Client
  5. Let Orbitron support your driver
  6. Your driver for all!
  7. WiSP DDE Client


-- 1. Introduction -------------------------------------------------------------

Orbitron is working as server in DDE conversations, so you can write your own
DDE client that can e.g. control rotor and radio devices. Such programs are 
called by me 'Drivers'.

-- 2. Technical data -----------------------------------------------------------

Driver's EXE file:
  Name of driver's EXE file (without .EXE) is displayed on drivers list in
  Orbitron. Driver's main window title or application title must be the same
  as EXE file name to let Orbitron check is driver already running.

DDE conversation parameters:
  Source application : Orbitron
  Link topic         : Tracking
  Link items         : TrackingData
                         item for communication with WiSP DDE Client with basic 
			 data (Satscape format)
                       TrackingDataEx
		         item with extended data set
'Link item' is changed by Orbitron each time when current data is recalculated
(e.g. 1s, 5s...) and when downlink/uplink frequencies or mode are changed.

-- 3. Data format --------------------------------------------------------------

Please note:
  - all angles are given as decimal degrees,
  - decimal separator is "." (dot),
  - TrackingDataEx and TrackingData can be an EMPTY string If NO satellite 
    are tracked,
  - TrackingDataEx can contain only AOS field if NO satellite are tracked.

-- 3.1. TrackingData item ------------------------------------------------------

Format of string:
  SNxxx AZxxx.x ELxx.x DNxxxx UPxxxx DMxxxx UMxxxx
Fields:
  SN  - tracked satellite name (spaces converted to "_")
  AZ  - azimuth in range form 0.0 to 360.0
  EL  - elevation in range from -90.0 to 90.0
        please note possibility of negative values
  DN  - downlink frequency with doppler, [Hz] in range from 0 to N
  UP  - uplink frequency with doppler, [Hz] in range from 0 to N
  DM  - downlink mode (optional field)
  UM  - uplink mode (optional field)
Examples:
  SNISS_ZARYA AZ182.1 EL36.3 DN145800000 UP145800000 UMFM-N
  SNISS_ZARYA AZ180.3 EL-10.1 DN145800000 UP145800000

-- 3.2. TrackingDataEx item ----------------------------------------------------

Format of string:
  SN"xxx" AZxxx.x ELxx.x DNxxxx UPxxxx DMxxxx UMxxxx AOS"xxx"
Fields:
  SN  - tracked satellite name in quotes
  RA  - range [km]
  RR  - range rate [km/s]
  LO  - longitude in range from -180.0000 to 180.000 (W to E)
  LA  - latitude in range from -90.0000 to 90.000 (S to N)
  AL  - altitude [km]
  TU  - UTC time (Format: YYYYMMDDhhmmss)
  TL  - local time (Format: YYYYMMDDhhmmss)
  AOS - AOS notification for satellite "xxx" (name in quotes; AOS notification
        is provided for all tracked objects, not only for active one)
  Other fields same as in TrackingData item.
Examples:
  SN"ISS ZARYA" AZ182.1 EL36.3 DN145800000 UP145800000 (...)
  SN"ISS ZARYA" AZ182.1 EL36.3 DN145800000 UP145800000 (...) AOS"HST"
  AOS"HST"

-- 4. My DDE Client ------------------------------------------------------------

My DDE Client is an example of user's custom driver for Orbitron with full
source code in Delphi 5. It should helps you to write your own driver for rotor 
or radio hardware. Further information you will find in MAIN.PAS file.

-- 5. Let Orbitron support your driver -----------------------------------------

Please set specific name for your driver program, e.g. SuperRotor.exe. Then you
can edit '{Orbitron}\Config\Setup.cfg' file (*) by adding line to the [Drivers] 
section (please create it if doesn't exist) with your driver information. 
Example:
  [Drivers]
  SuperRotor=d:\My files\SuperRotor.exe
  SuperRotorTwo=
Next time you run Orbitron, your driver will be listed on 'Rotor/Radio' panel. 
You can launch it there. If no path specified, Orbitron will ask you about your 
driver's location (drivers located in {Orbitron} directory will be found 
automatically).

*) {Orbitron} - replace it with directory where Orbitron is installed to, 
     e.g. 'c:\Program Files\Orbitron'. This path is available in Windows 
     registry:
       HKEY_CURRENT_USER\Software\Stoff\Orbitron\Path
     WARNING: User can define his own location where the 'Config\' directory is.
     This value is stored in registry:
       HKEY_CURRENT_USER\Software\Stoff\Orbitron\UserPath
     So, if this value is specified, you have to use it instead of previous one.
     HINT: You can check are files definied by:
       HKEY_CURRENT_USER\Software\Stoff\Orbitron\Path + '\Config\Setup.cfg'
       HKEY_CURRENT_USER\Software\Stoff\Orbitron\UserPath + '\Config\Setup.cfg'
     exists and then add your config line to both.

-- 6. Your driver for all! -----------------------------------------------------

Have you already done your driver? Please, send me it. I'll put it on Orbitron 
website to share it with all people with same hardware as yours.

-- 7. WiSP DDE Client ----------------------------------------------------------

WiSP DDE Client is an application written by Fernando Mederos, CX6DD and 
available on http://www.laboratoriomederos.com/CX6DD/wispdde/. It works as DDE 
Client and support many kinds of hardware and software. To use it with Orbitron, 
following settings are required:
  Receive DDE from   : Orbitron (or Satscape)
  Source application : Orbitron
  Link Topic         : Tracking
  Link Item          : TrackingData
  Query Interval     : 1 sec.
Orbitron will set these values when you launch WiSP DDE Client from 
'Rotor/Radio' panel.

-- Last updated 2005.02.26 -----------------------------------------------------
