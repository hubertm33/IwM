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
            
            List<string> textList = new List<string>();

            foreach (string item in textArray)
            {
                textList.Add(item);
            }
            //textArray.ToList(); nie dziala
            for (int i = 0;i < textArray.Length;i++)
            {
                foreach(String stopWord in stopWords)
                {
                    
                    if (String.Equals(textArray[i], stopWord))
                    {
                        textList.Remove(textArray[i]);
                        
                    }
                }
            }


        }
    }
}
