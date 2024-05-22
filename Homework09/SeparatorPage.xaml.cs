using Homework05Task01;
using System.Windows;
using System.Windows.Controls;

namespace Homework09
{
    /// <summary>
    /// Interaction logic for SeparatorPage.xaml
    /// </summary>
    public partial class SeparatorPage : Page
    {
        public SeparatorPage()
        {
            InitializeComponent();
        }

        private void OnSeparateButtonCkicked(object sender, RoutedEventArgs e)
        {
            string sentence = TextFromUser.Text;
            string[] words = HomeWork05Task01.SplitSentence(sentence);

            SeparatingResultList.Items.Clear();
           
            foreach (string word in words)
            {
                if(string.IsNullOrEmpty(word))
                {
                    continue;
                }

                SeparatingResultList.Items.Add(word);
            }
        }
    }
}
