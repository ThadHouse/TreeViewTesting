using FRC.NetworkTables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewTesting
{
    public class NetworkTableTreeEntry
    {
        public string FullName { get; set; }

        public object EntryContent { get; set; }

        public string EntryName
        {
            get
            {
                var key = FullName.AsSpan();
                if (key[key.Length - 1] == NetworkTable.PathSeparator) 
                {
                    key = key.Slice(0, key.Length - 1);
                }
                key = key.Slice(key.LastIndexOf(NetworkTable.PathSeparator) + 1);
                return key.ToString();
            }
        }

        public NetworkTableEntry? Entry;
        public ObservableCollection<NetworkTableTreeEntry> Children { get; set; } = new ObservableCollection<NetworkTableTreeEntry>();

        public NetworkTableTreeEntry(string key)
        {
            FullName = key;
            EntryContent = EntryName;
        }

        public NetworkTableTreeEntry(string key, NetworkTableEntry ntEntry, object value)
        {
            FullName = key;
            EntryContent = new StringEntryControl(EntryName, ntEntry.GetObjectValue().ToString());
        }
    }
}
