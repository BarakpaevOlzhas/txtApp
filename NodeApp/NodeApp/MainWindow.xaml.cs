using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        static string text = "";

        static string path = "";

        bool isStat = false;

        System.Threading.Timer timer = new System.Threading.Timer(SaveText, text, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

        

        public MainWindow()
        {
            InitializeComponent();           
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(SaveInBufText);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 9);
            dispatcherTimer.Start();

            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd, MenuItemClickSave));

            RoutedCommand newCmdTow = new RoutedCommand();
            newCmdTow.InputGestures.Add(new KeyGesture(Key.F5));
            CommandBindings.Add(new CommandBinding(newCmdTow, MenuItemClickDataAndTime));

            RoutedCommand newCmdThree = new RoutedCommand();
            newCmdThree.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmdThree, MenuItemClickNew));

            RoutedCommand newCmdFour = new RoutedCommand();
            newCmdFour.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmdFour, MenuItemClickOpen));

            RoutedCommand newCmdFive = new RoutedCommand();
            newCmdFive.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmdFive, MenuItemClickFind));

            RoutedCommand newCmdSix = new RoutedCommand();
            newCmdSix.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmdSix, MenuItemClickReplace));
        }

        private void ReStat()
        {
            int line = mainText.GetLineIndexFromCharacterIndex(mainText.SelectionStart);

            int numberChar = mainText.GetCharacterIndexFromLineIndex(line);

            int column = mainText.SelectionStart - numberChar;

            lableStat.Content = $"Стр {line}, стлб {mainText.SelectionStart}";
        }

        ~MainWindow()
        {
            timer.Dispose();
        }

        private void SaveInBufText(object sender, EventArgs e)
        {
            text = mainText.Text;           
        }

        private static void SaveText(object textBox)
        {
            if (path != "") {
                using (StreamWriter fileStream = new StreamWriter(path))
                {
                    fileStream.Write(text);
                }
            }
        }

        private void LoadText(object textBox)
        {
            using (StreamReader fileStream = new StreamReader(path))
            {
                text = fileStream.ReadToEnd();
            }            
        }

        private void MenuItemClickOpen(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                mainText.Text = File.ReadAllText(dialog.FileName);
                path = dialog.FileName;
            }
        }

        private void MenuItemClickFont(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();            

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mainText.FontFamily = new FontFamily(fd.Font.Name);
                mainText.FontSize = fd.Font.Size;
            }
        }

        private void MenuItemClickSave(object sender, RoutedEventArgs e)
        {
            if (path != "")
            {
                using (StreamWriter fileStream = new StreamWriter(path))
                {
                    fileStream.Write(mainText.Text);
                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    using (StreamWriter fileStream = new StreamWriter(saveFileDialog.FileName))
                    {
                        path = saveFileDialog.FileName;
                        fileStream.Write(mainText.Text);
                    }
                }
            }
        }

        private void MenuItemClickSaveHow(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter fileStream = new StreamWriter(saveFileDialog.FileName))
                {
                    path = saveFileDialog.FileName;
                    fileStream.Write(mainText.Text);
                }
            }
        }

        private void MenuItemClickNew(object sender, RoutedEventArgs e)
        {
            path = "";
            text = "";
            mainText.Text = "";
        }

        private void MenuItemClickBack(object sender, RoutedEventArgs e)
        {
            mainText.Undo();
        }

        private void MenuItemClickCut(object sender, RoutedEventArgs e)
        {
            mainText.Cut();
        }

        private void MenuItemClickCopy(object sender, RoutedEventArgs e)
        {
            mainText.Copy();
        }

        private void MenuItemClickPaste(object sender, RoutedEventArgs e)
        {
            mainText.Paste();
        }

        private void MenuItemClickDelete(object sender, RoutedEventArgs e)
        {
            mainText.Text = mainText.Text.Remove(mainText.SelectionStart, mainText.SelectionLength);
        }

        private void MenuItemClickFind(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(this);
            window.Show();
        }

        private void MenuItemClickSelectAll(object sender, RoutedEventArgs e)
        {
            mainText.SelectAll();
            mainText.Focus();
        }

        private void MenuItemClickDataAndTime(object sender, RoutedEventArgs e)
        {
            mainText.SelectedText = DateTime.Now.ToString("hh:mm dd:MM:yyyy");
        }

        private void MenuItemClickReplace(object sender, RoutedEventArgs e)
        {
            WindowForReplace window = new WindowForReplace(this);
            window.Show();
        }

        private void MenuItemClickStat(object sender, RoutedEventArgs e)
        {
            isStat = !isStat;

            if (isStat)
            {
                Thickness thickness = new Thickness();
                thickness = mainText.Margin;
                thickness.Bottom = 19;
                mainText.Margin = thickness;
            }
            else
            {
                Thickness thickness = new Thickness();
                thickness = mainText.Margin;
                thickness.Bottom = 0;
                mainText.Margin = thickness;
            }
        }

        private void MainTextKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ReStat();
        }

        private void MainTextMouseDown(object sender, MouseButtonEventArgs e)
        {
            ReStat();
        }       
    }
}
