# WaymarkLibrarian
![Screenshot](WaymarkLibrarianScreenshot.png)

## Purpose
This is a tool for swapping an unlimited number of waymark presets into and out of the extremely limited number of slots that the game provides.  While I've tried to make everything function correctly (I wrote this for myself to use, after all), use of this program is at your own risk (please read the [IMPORTANT](#important) section below).

## Basic Use
1. Select the folder in which FFXIV stores user configuration files.  The program assumes "...\Documents\My Games\FINAL FANTASY XIV - A Realm Reborn" for the current user when it first starts up, but this can be changed by clicking the "..." button if it is incorrect (or if you have another folder that you would like to use (i.e., a copy made for testing)).
2. Select the character for which you would like to view/edit presets.  See the [Aliases](#aliases) section below for more information on character names.  Also note that if the character character hasn't been logged into recently, the program will pop up a message that it can't read the waymark presets.  Just log in and back out on that character in order to have a file that the program can read.
3. You can now select any of the five preset slots for that character in the list on the left and view the preset info, delete it, or copy it to the library.
4. The library is completely separate from the game.  You can store an unlimited number of waymark presets in it, and those presets can have descriptive names (i.e., e7s PUG Strats, e7s Static Strats, e7s Uptime, UCoB Inverted, UCoB Upright, etc.).  The library is stored completely separately from the game in your user's application data folder.
5. After you have presets in your library, you can copy them to the selected character using the "<- Copy to Game" button.  This will copy the selected preset from the library to the selected game slot.  Please note that this will not yet affect the game.
6. Once you are satisfied with the five preset slots for the character, you must click the "Write Game File" button for the updated presets to be reflected in-game.  Also note that the file must be written when the selected character is logged out.  If the character is logged in when you click the write button, the game will overwrite any changes that you have made.

## Importing, Exporting, and Editing Presets
The program allows you to import presets that other people have created, as well as export your own presets for others.  To import, click the "Import/New" button below the library and paste in the formatted data.  The import function also accepts Paisley Park presets, although you will have to manually set and save the time and zone fields, as that program does not store or provide that information.  To export a preset, simply click the "Export" button beneath the library and copy the provided text.

You can also edit the name, zone, and waymark positions of any preset in your library.  To do this, select the desired preset in the library, enter/edit the fields on the righthand third of the program, and click the "Update" button at the bottom.

## IMPORTANT
This program should be considered as being in beta.  It's only been tested with a handful of characters so far, and since the waymark format was reverse-engineered, there's no way to know that it is being written flawlessly and undetectably.  That being said, the format of the data that the game uses is very simple, so there's not much that could be incorrect.
Additionally, while this program does not affect the game executable, memory, etc., it is technically possible that SE could detect the use of this program in one of two ways:
1) They could audit where your character was at the time that the waymark preset says it was saved, and see that you were not in the corresponding zone then.
2) They could be saving a parallel server-side preset for each slot on your character and checking it against the client-side preset saved in your user configuration.

Both of these seem unlikely though, as they would give false positives for anyone that copies user configuration data from a main character to alts, and they are methods that seem like more work than SE would put in for something like this when they have already made the preset data a part of character configuration.

## Aliases
By default, these will be a string of numbers and letters, such as "FFIXV_CHR...".  Clicking "Set Alias" and entering something that you will easily remember (i.e. "Character Name (Server)" will make a friendly name appear in the list for that character instead.  Unfortunately, you will have to determine which folder corresponds to which character manually, as it appears to be something that cannot be determined directly without hooking the game.  The simplest way to determine which folder belongs to a character is to log in to that character, change your item order in your inventory, change a user macro, a hotbar, etc., log back out, and then find which folder contains a file modified at the time you logged out.

## Game Data Format
The five waymark presets that the game provides are saved by the game per-character in UISAVE.DAT under the corresponding character config folder.  As of patch 5.25, the format is as follows:
* Valid regions of the file are XOR-encoded by 0x31.
* Each data field is little-endian.
* Waymark preset data starts at 0x6C97, and is 104 bytes per slot (second slot starts at 0x6CFF, etc.).
* There are 12 bytes per waymark (96 bytes total for eight waymarks), with a 4-byte signed integer each for the X, Y, and Z coordinates (in that order).  Divide the integer by 1000 to get the actual decimal value.
* The final eight bytes of each preset are as follows:
  * One byte containing a bitmask of which markers are enabled.
  * One byte that appears to be reserved (always 0).
  * Two bytes for the territory ID in which the preset is valid.
  * Four bytes containing the Unix timestamp of when the preset was created.
