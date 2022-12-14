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

        public AlarmSetting(ConsoleColor targetConsoleColor, double frequency, double duration) {
            this.targetConsoleColor = targetConsoleColor;
            this.frequency = frequency;
            this.duration = duration;
            alarmSettings.Add(this);
        }

        public static void SaveAlarmSettings() {
            var json = JsonConvert.SerializeObject(alarmSettings, new StringEnumConverter());
            File.WriteAllText("alarmSettings.json", json);
        }

        public static void LoadAlarmSettings() {
            var json = File.ReadAllText("alarmSettings.json");
            alarmSettings = JsonConvert.DeserializeObject<List<AlarmSetting>>(json, new StringEnumConverter());
        }
    }
}