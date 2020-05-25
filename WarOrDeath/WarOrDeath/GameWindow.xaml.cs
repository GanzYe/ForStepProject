using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
enum Dir
{
    You,II,ThatAll,None
}
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
        int countTurns;

        int IIcountTurns;

        public GameWindow()
        {
            InitializeComponent();
            buttons = new List<Button>();
            youButtons = new List<Button>();
            iiButtons = new List<Button>();
            countTurns = 0;
            IIcountTurns = 0;
            int i = 1;
            Turns.Text = countTurns.ToString();
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
            if (countTurns >= 1&&(string)btn.Tag == "Active")
            {
                btn.Tag = "CLICKED";
                youButtons.Add(btn);
                /// Создание крестика
                RotateTransform transform = new RotateTransform(45);
                Rectangle xRec = new Rectangle { Stroke = Brushes.Maroon, Height = 4, StrokeThickness = 4, RenderTransformOrigin = new Point(0.5, 0.5), Margin = new Thickness(-18, 0, -18, 0), RenderTransform = transform };
                Rectangle xRec2 = new Rectangle { Stroke = Brushes.Maroon, Width = 4, StrokeThickness = 4, RenderTransformOrigin = new Point(0.5, 0.5), Margin = new Thickness(0, -18, 0, -18), RenderTransform = transform };
                Grid grid = new Grid();
                grid.Children.Add(xRec);
                grid.Children.Add(xRec2);
                btn.Content = grid;
                ///

                //Активация соседныч кнопок
                int name = int.Parse(btn.Name.Substring(1));
                GetButton(name,Dir.You);
                //
                countTurns--;
                Units.Text = youButtons.Count.ToString();
                Turns.Text = countTurns.ToString();
                btn.Click -= SetForUnit;
                IiTurnButton();
            }
            var allActive = buttons.Where(x => (string)x.Tag == "Active");
            int count = allActive.Count();
            int countClik = buttons.Where(x => (string)x.Tag == "CLICKED").Count();
            int countBrush = 100-countClik;
            if (0 == count)
            {
                var iibtn = buttons.Where(x => (string)x.Tag == "ActiveForComp");
                for (int i = 0; i < countBrush; i++)
                {
                    UpdateII();
                }
            }
            if (100==buttons.Where(x => (string)x.Tag == "CLICKED").Count())
            {
                if (iiButtons.Count>youButtons.Count)
                {
                   MessageBox.Show($"Компьютер выиграл вас!ХАВХАХВХАХВАХ\nВаш счет:{youButtons.Count}\nСчет компа:{iiButtons.Count}");
                }
                if (iiButtons.Count<youButtons.Count)
                {
                   MessageBox.Show($"А ты хорош! Вы выиргали!\nВаш счет:{youButtons.Count}\nСчет компа:{iiButtons.Count}");
                }
                var win = new MainWindow();
                win.Show();
                this.Close();
            }
        }

        private void IiTurnButton()
        {
            if (countTurns == 0 && !RandCube.IsEnabled)
            {
                Random random = new Random();
                IIcountTurns = random.Next(1, 10);
                CompTurns.Text = IIcountTurns.ToString();
                for (int i = 0; i < IIcountTurns; i++)
                {
                    UpdateII();
                }
                RandCube.IsEnabled = true;
                RandCube.Opacity = 1;
                forYou.Opacity = 1;
            }
        }

        private void UpdateII()
        {
            var actButtons = buttons.Where(x =>(string)x.Tag == "ActiveForComp");
            Random random = new Random();
            var btn = new Button();
            int randIndex = random.Next(0, actButtons.Count());
            int startIndex= 0;
            if (100 == buttons.Where(x => (string)x.Tag == "CLICKED").Count())
            {
                if (iiButtons.Count > youButtons.Count)
                {
                    MessageBox.Show($"Компьютер выиграл вас!ХАВХАХВХАХВАХ\nВаш счет:{youButtons.Count}\nСчет компа:{iiButtons.Count}");
                }
                if (iiButtons.Count < youButtons.Count)
                {
                    MessageBox.Show($"А ты хорош! Вы выиргали!\nВаш счет:{youButtons.Count}\nСчет компа:{iiButtons.Count}");
                }
                var win = new MainWindow();
                win.Show();
                this.Close();
            }
            if (0 == actButtons.Count())
            {
                RandCube.IsEnabled = true;
                RandCube.Opacity = 1;
                forYou.Opacity = 1;
                return;
            }
            foreach (var item in actButtons)
            {
                if (startIndex == randIndex)
                {
                    btn = item;
                    break;
                }
                startIndex++;
            }
            int position = int.Parse(btn.Name.Substring(1));
            Ellipse elip = new Ellipse { Fill = Brushes.Red, Height = 30, Stroke = Brushes.Black, Width = 30 };
            Grid grid = new Grid();
            btn.Content = (elip);
            btn.Tag = "CLICKED";
            iiButtons.Add(btn);
            GetButton(position, Dir.II);
        }

        private void GetButton(int position, Dir dir)
        {
            List<Button> fineButtons = new List<Button>();

            if (position == 1)
            {

                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position + 10));

            }
            else if (position == 10)
            {

                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position + 10));
            }
            else if (position == 91)
            {

                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position - 10));

            }
            else if (position == 100)
            {

                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position - 10));

            }
            else if (position > 1 && position < 10)
            {
                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position + 10));

            }
            else if (position > 91 && position < 100)
            {
                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position - 10));

            }
            else if (position % 10 == 1)
            {
                fineButtons.Add(FineButton(position + 10));
                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position - 10));
            }
            else if (position % 10 == 0)
            {
                fineButtons.Add(FineButton(position + 10));
                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position - 10));
            }
            else
            {
                fineButtons.Add(FineButton(position + 10));
                fineButtons.Add(FineButton(position - 1));
                fineButtons.Add(FineButton(position + 1));
                fineButtons.Add(FineButton(position - 10));
            }
            //ActiveForComp
            foreach (var item in fineButtons)
            {
                if (!((string)item.Tag == "CLICKED"))
                {
                    if (dir == Dir.You)
                    {
                        if (!((string)item.Tag == "Active") && !((string)item.Tag == "ActiveForComp"))
                        {
                            item.Tag = "Active";
                            item.Background = new SolidColorBrush(Colors.AliceBlue);
                            item.Click += SetForUnit;
                        }
                    }
                    if (dir == Dir.II)
                    {
                        if (!((string)item.Tag == "Active") && !((string)item.Tag == "ActiveForComp"))
                        {
                            item.Tag = "ActiveForComp";
                            item.Background = new SolidColorBrush(Colors.Aquamarine);
                            item.Click += SetForUnit;
                        }
                    }
                }
            }
        }

        private Button FineButton(int position)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Name == $"N{position}")
                {
                    return buttons[i];
                }
            }
            return null;
        }

        private void RandCube_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            countTurns = random.Next(1,10);
            Turns.Text = countTurns.ToString();
            RandCube.IsEnabled = false;
            RandCube.Opacity = 0;
            forYou.Opacity = 0;

        }

        
    }
}
