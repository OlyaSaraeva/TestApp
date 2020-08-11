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
using System.Data;
using System.Threading;
using System.Windows.Threading;

namespace TestApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            isStart = false;
        }

        public bool isStart { get; set; } // свойство, определяющее состояние кнопки
        public string errorText { get; set; }

        public bool Validator() // метод, проверяющий входное число на правильность ввода
        {
            int dig;
            if (!int.TryParse(inputValue.Text.ToString(), out dig))
            {
                errorText = "Неверный формат строки";
                progressBar.Value = 0;
            }
            else
            {
                if (dig >= 0 && dig <= 100) return true;
                else
                {
                    errorText = "Число должно быть в диапазоне от 0 до 100";
                    progressBar.Value = 0;
                }
            }
            return false;
        }

        public void RandomValue() // метод, вставляющий рандомные числа в ProgressBar
        {
            if (isStart)
            {
                if (Validator())
                {
                    Random random = new Random();
                    progressBar.Value = random.Next(Convert.ToInt32(inputValue.Text));
                    lName.Content = "Максимальное число = " + inputValue.Text;
                }
                else lName.Content = errorText;
            }
        } 

        public void Timer() // метод, отвечающий за вывод случайного числа в ProgressBar пока кнопка в состоянии "Start"
        {
            if (isStart)
            {
                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(disp_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RandomValue();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btn.Content == "Start")
            {
                isStart = true;
                btn.Content = "Stop";
                RandomValue();
            }
            else
            {
                isStart = false;
                btn.Content = "Start";
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Timer();
        }

        private void disp_Tick(object sender, EventArgs e)
        {
            RandomValue();
        }
    }
}
