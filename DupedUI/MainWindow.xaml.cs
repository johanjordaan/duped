using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DupedUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> roots = new List<String>();
        private List<String> files = new List<String>();
        private bool _wasCancelled = false;

        public MainWindow()
        {
            InitializeComponent();
            Status.XTitle = "Hallo";
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                roots.Add(dialog.SelectedPath);
                SelectedPathLbl.Content = dialog.SelectedPath;
            }

            
        }

        private async Task ProcessFiles(string root)
        {
            foreach (var progress in Duped.DirWalker.Files(root))
            {
                if(_wasCancelled)
                {
                    break;
                }
                await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                this.files.Add(progress.FileName);
                StatusLbl.Content = $"{progress.FileName} - {progress.DiretoriesInQueue}";
                CountLbl.Content = $"{files.Count}";
                Status.SeriesCollection[0].Values.Clear();
                Status.SeriesCollection[0].Values.Add(Convert.ToDouble(files.Count));
            }

            StatusLbl.Content = "Done ...";
            StartBtn.IsEnabled = true;
            AddTargetBtn.IsEnabled = true;
            CancelBtn.IsEnabled = false;

        }

        async private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.IsEnabled = false;
            AddTargetBtn.IsEnabled = false;
            CancelBtn.IsEnabled = true;
            _wasCancelled = false;
            this.files.Clear();
            foreach (String root in roots) {
                await ProcessFiles(root);
            }
            StatusLbl.Content = "Done ...";
            StartBtn.IsEnabled = true;
            AddTargetBtn.IsEnabled = true;
            CancelBtn.IsEnabled = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.IsEnabled = true;
            AddTargetBtn.IsEnabled = true;
            CancelBtn.IsEnabled = false;
            _wasCancelled = true;
        }
    }
}
