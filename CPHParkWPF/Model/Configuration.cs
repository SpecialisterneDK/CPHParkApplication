using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace CPHParkWPF.Model;
public sealed class Configuration {
    private static string configurationFilePath = Directory.GetCurrentDirectory() + "/config.txt";
    private static string keyEndString = "%&?¤$";

    private static Configuration? _singleton;
    public static Configuration Singleton {
        get {
            if(_singleton == null) {
                _singleton = new Configuration();
            }
            return _singleton;
        }
    }

    private Dictionary<string, string> CachePaths { get; set; }

    private Configuration() {
        CachePaths = new Dictionary<string, string>();

        //Get the locations used previously from configuration file.
        if(File.Exists(configurationFilePath)) {
            using (StreamReader configFile = new StreamReader(configurationFilePath)) {
                foreach (string line in configFile.ReadToEnd().Split(Environment.NewLine)) {
                    if(line.Contains(keyEndString)) {
                        CachePaths.Add(line.Split(keyEndString)[0], line.Split(keyEndString)[1]);
                    }
                }
            }
        }
    }

    public string GetPath(string key) {
        if(key != null && CachePaths.ContainsKey(key)) {
            return CachePaths[key];
        }
        return "";
    }

    public string? SetPath(string key, string value) {
        string? previousValue = null;
        if (CachePaths.ContainsKey(key)) {
             previousValue = CachePaths[key];
        }
        CachePaths[key] = value;
        Save();
        return previousValue;
    }

    private void Save() {
        using (StreamWriter configFile = new StreamWriter(configurationFilePath)) {
            foreach (string line in CachePaths.Keys)
                configFile.WriteLine(line + keyEndString + CachePaths[line]);
        }
    }
}
