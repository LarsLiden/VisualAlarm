# VisualAlarm
A simple visual (i.e. silent but flashing) alarm that can be used to remind yourself to move around occasionally / be mindful / etc.

Usage:
- Open a terminal somewhere on your desktop
- Run the excutable
- The terminal window will change the console's background color based on alarm settings

#### Types of Alarms
##### Transient Alarm
When a transient alarm goes off, the console will flash between the alarm's set color and the background color (black) for the duration set in the alarm and then turn off automatically.  When more than one transient alarm is triggered the console will flash between the colors of all the active alarms.

##### Wait Alarm
When a wait alarm goes off, it will change the background color of the console to the set color.  The background color will not revert back to black until the user presses a key.  If a transient alarm is also going off, the console color will flash between the transient alarm's color and the wait alarm's color.   If more than one wait alarm has been triggered, the background color will be set to the color of most recently triggerd wait alarm.

Edit "AlarmSetting.json" to change the alarms

##### Example 1 
The sample will create two alarms.  One that flashes red every 60 seconds for one second and another that flashes yellow every 30 seconds for 2 seconds.   When multiple alarms are active at the same time it will flash between the colors of the active alarms.

```
[
    {
        "targetConsoleColor": "Red",
        "frequency": 60.0,
        "duration": 1.0
    },
    {
        "targetConsoleColor": "Yellow",
        "frequency": 30.0,
        "duration": 2.0
    }
]
```

##### Example 2 
This sample will create two alarms.  A transient alarm that flashes red every 10 minutes for 2 seconds.  The other, a wait alarm, will change the background color to blue every 100 minutes and requires the user to press a key to change it back to black.   When both alarms are active at the same time the console will flash between the red and blue.

```
[
    {
        "targetConsoleColor": "Red",
        "frequency": 600,
        "duration": 2.0
    },
    {
        "targetConsoleColor": "Blue",
        "frequency": 6000,
        "wait": true
    }
] 
```
