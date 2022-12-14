# VisualAlarm
A simple periodic visual (i.e. silent but flashing) alarm that can be used to remind yourself to move around occasionally / be mindful / etc.

Usage:
- Open a terminal somewhere on your desktop
- Run the excutable
- The terminal window with flash at the set interval

Edit "AlarmSetting.json" to change the alarms

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
