namespace VisualAlarm 
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    class AlarmSetting {

        static public List<AlarmSetting> alarmSettings = new List<AlarmSetting>();

        [JsonConverter(typeof(StringEnumConverter))]
        public ConsoleColor targetConsoleColor;

        public double frequency;
        public double duration;

        public bool wait;

        public AlarmSetting(ConsoleColor targetConsoleColor, double frequency, double duration, bool wait = false) {
            this.targetConsoleColor = targetConsoleColor;
            this.frequency = frequency;
            this.duration = duration;
            this.wait = wait;
            alarmSettings.Add(this);
        }

        public static void SaveAlarmSettings() {
            var json = JsonConvert.SerializeObject(alarmSettings, new StringEnumConverter());
            File.WriteAllText("alarmSettings.json", json);
        }

        public static void LoadAlarmSettings() {
            var json = File.ReadAllText("alarmSettings.json");

            List<AlarmSetting>? settings = JsonConvert.DeserializeObject<List<AlarmSetting>>(json, new StringEnumConverter());
            if (settings == null) {
                alarmSettings = new List<AlarmSetting>();
            }
            else {
                alarmSettings = settings;
            }
            
            foreach (var alarmSetting in alarmSettings) {
                if (alarmSetting.wait) {
                    AlarmManager.AddAlarm(new WaitAlarm(alarmSetting));
                }
                else {
                    AlarmManager.AddAlarm(new TransientAlarm(alarmSetting));    
                }
            }
        }
    }
}