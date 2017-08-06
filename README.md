# Intel NUC LED Controller
Simple GUI tool to control the front panel LED ring on Intel NUCs.
Requires the 'SW Control' option to be set in the BIOS.

![manual control](http://james-barnett.net/files/intel-nuc-led-controller/screenshots/manual.png) ![auto control](http://james-barnett.net/files/intel-nuc-led-controller/screenshots/auto.png)

## Install
Grab the latest release zip [here](https://github.com/jamesbarnett91/intel-nuc-led-controller/releases/download/v0.2/NucLedController-v0.2.zip) (or from the releases page).
Unzip somewhere, and run NucLedController.exe


## Use
This tool provides options for both manual control and some experimental automatic controls.
In manual mode the desired LED colour and blink/fade rates can be set, or the LED disabled all together.

Currently the automatic control options allow cycling or fading between all the LED colours at a given rate. Lower rates (i.e. quicker colour changes) will consume more CPU resources to manage.
These automatic modes are fully controlled by this software, and so the program must be left running to use these modes.
In manual mode the setting can be applied and the application closed.


Note that this application requests to run as admin in order to use the WMI interface to control the LED.
I'm not a C# developer so some of this may be janky.