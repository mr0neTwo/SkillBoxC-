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
using System.Windows.Shapes;

namespace Homework09
{
    /// <summary>
    /// Interaction logic for ReverserPage.xaml
    /// </summary>
    public partial class ReverserPage : Page
    {
        public ReverserPage()
        {
            InitializeComponent();
        }

        private void OnReverseButtonClicked(object sender, RoutedEventArgs e)
        {
            string sentence = TextFromUser.Text;
            string[] reverdedSentenceWord = sentence.Split(' ').Reverse().ToArray();
            string reversedSentence = string.Join(" ", reverdedSentenceWord);
            ReversedText.Content = reversedSentence;
        }
    }
}
