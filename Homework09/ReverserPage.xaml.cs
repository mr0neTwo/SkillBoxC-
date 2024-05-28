using System.Windows;
using System.Windows.Controls;

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
