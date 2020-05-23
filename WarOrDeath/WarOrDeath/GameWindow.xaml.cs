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

namespace WarOrDeath
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        List<Button> buttons;
        List<Button> iiButtons;
        List<Button> youButtons;
        int i = 1;
        public GameWindow()
        {
            InitializeComponent();
            buttons = new List<Button>();
            youButtons = new List<Button>();
            iiButtons = new List<Button>();
            foreach (var item in gameGrid.Children)
            {
                if (item is Button)
                {
                    var btn =(Button)item;
                    btn.Name = ($"N{i++}");
                    buttons.Add(btn);
                }
            }
        }


        private void SetForUnit(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            int name =int.Parse( btn.Name.Skip(1).ToString());
            var b = buttons.Where(x => int.Parse(x.Name.Skip(1).ToString()) == name + 1);
            ss.Text = b.ToString();
            youButtons.Add(btn);
            btn.Background = new SolidColorBrush(Colors.Black);
            btn.Click -= SetForUnit;
        }
    }
}
