using FRC.NetworkTables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TreeViewTesting
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NetworkTableInstance instance = NetworkTableInstance.Default;
            instance.SetServer("127.0.0.1");
            instance.StartClient();

            instance.GetEntry("/MyEntry").SetBoolean(true);

            instance.GetEntry("/Table/NEwThing").SetBoolean(true);
            instance.GetEntry("/Table/NEwThing2").SetBoolean(true);

            instance.GetEntry("/Table2/NEwThing").SetBoolean(true);
            instance.GetEntry("/Table2/NEwThing2").SetBoolean(true);

            instance.GetEntry("/Table2/T2/Thing").SetBoolean(true);
            instance.GetEntry("/Table2/T3/Thing").SetBoolean(true);

            TableTree.StartNetworking(instance);
        }
    }
}
