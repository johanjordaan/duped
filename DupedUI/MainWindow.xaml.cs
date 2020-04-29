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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SelectedPath.Content = dialog.SelectedPath;
            }

            ProcessFiles(dialog.SelectedPath);
        }

        private async Task ProcessFiles(string root)
        {
            var files = new List<string>();
            foreach (var progress in Duped.DirWalker.Files(root))
            {
                await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                files.Add(progress.FileName);
                Status.Content = $"{progress.FileName} - {progress.DiretoriesInQueue}";
                Count.Content = $"{files.Count}";
            }

            Status.Content = "Done ...";
        }
    }
}
