using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LetsGraph.Model.Base;
using LetsGraph.Model.Exporter;
using LetsGraph.Model.Parser;

namespace LetsGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository _repository;


        public MainWindow()
        {
            InitializeComponent();

            _repository = new Repository();
            RootPath.Text = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

            this.DataContext = this;
            updateSelectedItem(null);
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = RootPath.Text;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RootPath.Text = dialog.SelectedPath;
            }
        }


        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _repository = new Repository();

            var path = RootPath.Text;

            var rootItem = ParseFileSystem.ParsePathRecursive(_repository, path);

            updateSelectedItem(rootItem);
        }


        private void updateSelectedItem(Item item)
        {
            SelectedItem.DataContext = item;
            Predecessors.ItemsSource = _repository.GetIncomingRelations(item);
            Successors.ItemsSource = _repository.GetOutgoingRelations(item);
        }


        private void PredecessorsStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var stackPanel = sender as StackPanel;
            var relation = stackPanel.DataContext as Relation;
            updateSelectedItem(relation.Source);
        }


        private void SuccessorsStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var stackPanel = sender as StackPanel;
            var relation = stackPanel.DataContext as Relation;
            updateSelectedItem(relation.Target);
        }


        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = ".graphml";
            dialog.FileName = "LetsGraph-Export";
            dialog.Filter = "GraphML (.graphml)|*.graphml|All Files (*.*)|*.*";
            dialog.InitialDirectory = RootPath.Text;

            var result = dialog.ShowDialog();
            if (result == true)
            {
                ExportToGraphML.ExportToFile(_repository, dialog.FileName);
            }
        }
    }
}
