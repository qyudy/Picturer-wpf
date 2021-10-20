using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Picturer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler folderTree_Expanded = null;
            folderTree_Expanded = (o, es) =>
            {
                es.Handled = true;
                var thiz = o as TreeViewItem;
                foreach (TreeViewItem expandedFolder in thiz.Items)
                {
                    expandedFolder.Items.Clear();
                    expandedFolder.IsExpanded = false;
                    buildChildTree(folderTree_Expanded, expandedFolder, expandedFolder.Header as string, false);
                }
            };
            buildChildTree(folderTree_Expanded, MainListBox, null, true);
            string[] args = (Application.Current as App).args;
            if (args != null && args.Length > 0 && (File.Exists(args[0]) || Directory.Exists(args[0])))
            {
                string filename = null, path = null;
                if (Directory.Exists(args[0]))
                {
                    filename = null;
                    path = args[0];
                }
                else if (File.Exists(args[0]))
                {
                    filename = args[0];
                    path = Directory.GetParent(filename).FullName;
                }
                show(path, filename, true);
            }
        }

        private void buildChildTree(RoutedEventHandler folderTree_Expanded, ItemsControl parentTree, String path, bool recurOnce)
        {
            try
            {
                var paths = path != null ? Directory.GetDirectories(path) : Directory.GetLogicalDrives();
                foreach (string folder in paths)
                {
                    var folderTree = new TreeViewItem();
                    folderTree.Header = folder;
                    folderTree.Expanded += folderTree_Expanded;
                    folderTree.MouseRightButtonDown += (o, ev) =>
                    {
                        ev.Handled = true;
                        Button_Click(null,null);
                    };
                    folderTree.KeyDown += (o, ev) =>
                    {
                        if (ev.Key == System.Windows.Input.Key.Enter)
                        {
                            Button_Click(null, null);
                        }
                    };
                    parentTree.Items.Add(folderTree);
                    folderTree.IsExpanded = false;
                    if (recurOnce)
                    {
                        buildChildTree(folderTree_Expanded, folderTree, folder, false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var thiz = MainListBox.SelectedItem as TreeViewItem;
            var folderPath = thiz.Header as string;
            show(folderPath);
        }

        private void show(string folderPath, String filename = null, bool fromFile = false)
        {
            try
            {
                var tree = new DirTree(folderPath, null, Directory.GetFiles(folderPath));
                if (tree.HasSeed && Tools.IsPicImageContainer(tree, PicMode.AllSupport))
                {
                    new Show(tree, randomMode.IsChecked == true ? ChangeMode.Random : ChangeMode.Sequence, filename).Show();
                    if (fromFile)
                    {
                        expand(MainListBox.Items, folderPath);
                        //Application.Current.MainWindow.Close();
                    }
                    Application.Current.MainWindow.Hide();
                }
                else
                {
                    //Application.Current.MainWindow.Close();
                    expand(MainListBox.Items, folderPath);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void expand(ItemCollection items, string folderPath)
        {
            if (items == null || items.IsEmpty)
            {
                return;
            }
            foreach (TreeViewItem i in items)
            {
                var path = i.Header as string;
                if (path == folderPath)
                {
                    i.IsSelected = true;
                    return;
                }
                else if (folderPath.StartsWith(path + (path.EndsWith("\\")?"":"\\")))
                {
                    i.IsExpanded = true;
                    expand(i.Items, folderPath);
                    return;
                }
            }
        }

        private void MainListBox_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (data != null && data.Length > 0)
            {
                string filename = data[0], path = null;
                if (Directory.Exists(filename))
                {
                    path = filename;
                }
                else if (File.Exists(filename))
                {
                    path = Directory.GetParent(filename).FullName;
                }
                show(path);
            }
        }
    }
}
