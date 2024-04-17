using CPHParkWPF.Model;
using System.IO;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;

namespace CPHParkWPF.View.Controls {
    /// <summary>
    /// Interaction logic for PathPickerControl.xaml
    /// </summary>
    public partial class PathPickerControl : UserControl, INotifyPropertyChanged {
        public bool ShouldPickFile { get; set; }

        private string _pathKey;
        public string PathKey {
            get {
                return _pathKey;
            }
            set {
                _pathKey = value;
                NotifyPropertyChanged("PathKey");
                NotifyPropertyChanged("PathValue");
            }
        }

        public string ButtonText { get; set; }

        public string PathValue {
            get {
                return Configuration.Singleton.GetPath(PathKey);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PathPickerControl() {
            if (_pathKey == null) {
                _pathKey = "";
            }
            if (ButtonText == null) {
                ButtonText = "ButtonText";
            }

            InitializeComponent();
            this.DataContext = this;
        }

        private void PickPath(object sender, System.Windows.RoutedEventArgs e) {
            if (ShouldPickFile) {
                Configuration.Singleton.SetPath(PathKey, PickFile());
            } else {
                Configuration.Singleton.SetPath(PathKey, PickFolder());
            }
            NotifyPropertyChanged("PathValue");
        }

        private string PickFile() {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension

            bool? result = dialog.ShowDialog();

            if (result == true) {
                return dialog.FileName;
            }
            return "";
        }

        private string PickFolder() {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "My Title";
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = Directory.GetCurrentDirectory();

            dialog.AddToMostRecentlyUsedList = false;
            dialog.AllowNonFileSystemItems = false;
            dialog.DefaultDirectory = Directory.GetCurrentDirectory();
            dialog.EnsureFileExists = true;
            dialog.EnsurePathExists = true;
            dialog.EnsureReadOnly = false;
            dialog.EnsureValidNames = true;
            dialog.Multiselect = false;
            dialog.ShowPlacesList = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                return dialog.FileName;
            }

            return "";
        }

        public void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
