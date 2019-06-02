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
using System.Windows.Shapes;

namespace NodeApp
{
    /// <summary>
    /// Логика взаимодействия для WindowForReplace.xaml
    /// </summary>
    public partial class WindowForReplace : Window
    {
        int start = 0;
        bool isFirstTime = true;
        bool isLastWord = false;
        MainWindow mainWindowOne;
        public WindowForReplace(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindowOne = mainWindow;           
        }

        private void ButtonClickRepalceOnly(object sender, RoutedEventArgs e)
        {
            mainWindowOne.mainText.SelectedText = textBoxForWordTwo.Text;
        }

        private void ButtonClickFind(object sender, RoutedEventArgs e)
        {
            if (isFirstTime)
            {
                start = mainWindowOne.mainText.Text.IndexOf(textBoxForWord.Text, start);
                isFirstTime = false;
            }
            else if (start + textBoxForWord.Text.Length <= mainWindowOne.mainText.Text.Length && isLastWord == false)
                start = mainWindowOne.mainText.Text.IndexOf(textBoxForWord.Text, start + textBoxForWord.Text.Length);
            if (start + textBoxForWord.Text.Length <= mainWindowOne.mainText.Text.Length && isLastWord == false && start != -1)
            {
                mainWindowOne.mainText.Select(start, textBoxForWord.Text.Length);
                mainWindowOne.Focus();
                if (start + textBoxForWord.Text.Length == mainWindowOne.mainText.Text.Length)
                    isLastWord = true;
            }
            else if (isLastWord)
            {
                isLastWord = false;
                isFirstTime = true;
                start = 0;
            }
            else
            {
                isFirstTime = true;
                start = 0;
            }
        }

        private void ButtonClickReplaceAll(object sender, RoutedEventArgs e)
        {
            mainWindowOne.mainText.Text = mainWindowOne.mainText.Text.Replace(textBoxForWord.Text, textBoxForWordTwo.Text);
        }
    }
}
