using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class StringEntryControl : UserControl
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public StringEntryControl(string key, string value)
        {
            Key = key;
            Value = value;
            this.InitializeComponent();
        }
    }
}
