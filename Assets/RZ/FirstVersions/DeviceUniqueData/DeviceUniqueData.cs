namespace RZ
{

  using UnityEngine;

  public class DeviceUniqueData : SingletonMonoBehaviour<DeviceUniqueData>
  {
    public static string DATE_TIME_FORMAT = "yyyy-MM-dd_HH:mm:ss";
    public static string P_PREFS_KEY = "RZ.DeviceUniqueData";
    public static int minimalHashLength = 20;
    public static int preferredHashLength = 40;

    private static bool _updated;

    private static string _app_hash = "unknown";
    private static string _device_data = "unknown";
    private static bool _app_hash_ok = false;


    public static string app_hash
    {
      get
      {
        if (!_updated) UpdateDeviceInfo();
        return _app_hash;
      }

      set { _app_hash = value; }
    }


    public static string device_data
    {
      get
      {
        if (!_updated) UpdateDeviceInfo();
        return _device_data;
      }

      set { _device_data = value; }
    }


    public static bool app_hash_ok
    {
      get
      {
        if (!_updated) UpdateDeviceInfo();
        return _app_hash_ok;
      }

      set { _app_hash_ok = value; }
    }


    private void Reset()
    {
      _app_hash = "unknown";
      _device_data = "unknown";
      _app_hash_ok = false;

      _updated = false;

      UpdateDeviceInfo();
    }


    private void Awake()
    {
      _app_hash = "unknown";
      _device_data = "unknown";
      _app_hash_ok = false;

      _updated = false;

      UpdateDeviceInfo();
    }


    public static void UpdateDeviceInfo()
    {
      string I = "\"";


      // string date =
      //     System.DateTime.Now.ToString(DateTimeController.DATE_TIME_FORMAT);

      _device_data =
      "{"
      + I + "cpu" + I + ":" + I + SystemInfo.processorType + I + ","
      + I + "os" + I + ":" + I + SystemInfo.operatingSystem + I + ","
      + I + "app_ver" + I + ":" + I + Application.version + I + ","
      // + I + "user_date_time" + I + ":" + I + date + I
      + "}";

      _app_hash = "";

      _app_hash = SystemInfo.deviceUniqueIdentifier;

      if (string.IsNullOrEmpty(_app_hash) || _app_hash.Length < minimalHashLength)
      {
        if (PlayerPrefs.HasKey(P_PREFS_KEY))
        { _app_hash = PlayerPrefs.GetString(P_PREFS_KEY); }
      }

      if (string.IsNullOrEmpty(_app_hash) || _app_hash.Length < minimalHashLength)
      {
        if (PlayerPrefs.HasKey(P_PREFS_KEY))
        { _app_hash = GetRandomHash(); }
      }

      if (!string.IsNullOrEmpty(_app_hash) && _app_hash.Length >= minimalHashLength)
      { PlayerPrefs.SetString(P_PREFS_KEY, _app_hash); }

      _updated = true;
    }


    public static string GetRandomHash()
    {
      return RZ.Crypto.GetRangomHash(preferredHashLength);
    }

  }

}