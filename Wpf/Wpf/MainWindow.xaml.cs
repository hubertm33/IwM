using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> stopWords;
        

        public MainWindow()
        {
            stopWords = new List<string>();
            String line;
            
            System.IO.StreamReader file =
               new System.IO.StreamReader("stop_words.txt");
            while ((line = file.ReadLine()) != null)
            {
                stopWords.Add(line);
                
            }
            stopWords.ToArray();
            file.Close();

            InitializeComponent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Query.Clear();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //wyrazenie regularne na poprawne zdanie

            String[] textArray = Query.Text.Split(' ', ',', ':', ';');
            //wyrazenie regularne na alfanumeric
            Regex rgx = new Regex(@"^[A-Za-z0-9]+$");
            
            List<string> textList = new List<string>();
            int a = 0;
            foreach (string item in textArray)
            {
                if (rgx.IsMatch(item))
                {
                    a++;
                    textList.Add(item);
                }
                
            }

            Query.Text = Convert.ToString(a);
            //textArray.ToList(); nie dziala
            for (int i = 0;i < textList.Count;i++)
            {
                foreach(String stopWord in stopWords)
                {
                    
                    if (String.Equals(textList[i], stopWord))
                    {
                        textList.Remove(textList[i]);
                        
                    }
                }
            }
            //The Porter stemming algorithm

            Porter stemer = new Porter();

            for (int i = 0; i < textList.Count; i++)
            {
                textList[i] = stemer.stem(textList[i]);
            }

            //Query.Text = textList[0] + textList[1]+ textList[2];

        }
    }
}
