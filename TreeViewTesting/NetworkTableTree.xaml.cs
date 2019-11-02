using FRC.NetworkTables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TreeViewTesting
{
    public sealed partial class NetworkTableTree : UserControl
    {
        private ObservableCollection<NetworkTableTreeEntry> DataSource = new ObservableCollection<NetworkTableTreeEntry>();
        public NetworkTableTree()
        {
            this.InitializeComponent();

            DataSource.Add(new NetworkTableTreeEntry($"{NetworkTable.PathSeparator}Root")); 
        }

        public void StartNetworking(NetworkTableInstance instance)
        {
            instance.AddEntryListener("", (in RefEntryNotification notification) =>
            {
                string name = notification.Name.ToString();
                var entry = notification.Entry;
                var flags = notification.Flags;
                object value = notification.Value.ToValue().Value.GetValue();
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                {
                    Accept(name, entry, flags, value);
                });
                
            }, (NotifyFlags)0xFF);
        }

        private void Accept(string name, NetworkTableEntry ntEntry, NotifyFlags flags, object value)
        {
            var pathElements = name.ToString().Split(NetworkTable.PathSeparator).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            var finalPathElement = pathElements[pathElements.Length - 1];

            StringBuilder path = new StringBuilder();
            path.Append(NetworkTable.PathSeparator);

            var current = DataSource[0]; // Root will only be a single element

            foreach (var node in pathElements)
            {
                if (node == pathElements[pathElements.Length - 1]) break;
                path.Append(node).Append(NetworkTable.PathSeparator);

                var pt = path.ToString();

                var normalized = current.Children.Select(x => Normalize(x.FullName)).ToArray();

                var entry = current.Children.Where(x => Normalize(x.FullName) == path.ToString()).FirstOrDefault();
                
                if (entry != null)
                {
                    current = entry;
                }
                else
                {
                    // Does not exist
                    var newRow = new NetworkTableTreeEntry(path.ToString(), ntEntry, value);
                    current.Children.Add(newRow);
                    current = newRow;
                }

            }

            path.Append(finalPathElement);

            var row = current.Children.Where(x => Normalize(x.FullName) == path.ToString()).FirstOrDefault();

            if (flags.HasFlag(NotifyFlags.Delete))
            {
                if (row != null)
                {
                    current.Children.Remove(row);
                }
            } 
            else if (row != null)
            {
                // Update TODO
                ;
            }
            else
            {
                current.Children.Add(new NetworkTableTreeEntry(path.ToString(), ntEntry, value));
            }

        }

        private string Normalize(string key)
        {
            string tmp = NetworkTable.PathSeparator + key;
            var sep = NetworkTable.PathSeparator;
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] == sep)
                {
                    // Add it
                    newString.Append(tmp[i]);
                    i++;
                    // Advance to first not sep character
                    while (true)
                    {
                        if (i >= tmp.Length) goto end;
                        if (tmp[i] == sep) i++;
                        else
                        {
                            i--;
                            break;
                        }
                    }
                }
                else
                {
                    newString.Append(tmp[i]);
                }
            }
        end:
            return newString.ToString();
        }

    }
}
