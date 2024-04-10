using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[,] xPlaySpace = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public int[,] oPlaySpace = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public MainWindow()
        {
            InitializeComponent();
        }

        //right click event
        private void RightUp(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Grid parentGrid = clickedButton.Parent as Grid;
                if(parentGrid != null)
                {
                    parentGrid.Children.Remove(clickedButton);
                    CreateEllipse(clickedButton.Tag as string);
                    int check = CheckWin();
                    if (check != 0)
                    {
                        if (check == 1)
                        {
                            //xwin
                            xWin();

                        }
                        else
                        {
                            //owin
                            oWin();
                        }
                    }

                }
            }
        }

        //left click event
        private void LeftUp(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Grid parentGrid = clickedButton.Parent as Grid;
                if (parentGrid != null)
                {
                    parentGrid.Children.Remove(clickedButton);
                    CreateX(clickedButton.Tag as string);
                    int check = CheckWin();
                    if(check != 0)
                    {
                        if(check == 1)
                        {
                            //xwin
                            xWin();

                        } else
                        {
                            //owin
                            oWin();
                        }
                    }
                }
            }
        }

        //create the circle in the required row/column which is gathered from the tag -- eg. tag="00" or tag="01" for row/col respectively
        private void CreateEllipse(string tag)
        {
            Ellipse circ = new Ellipse();

            circ.Width = 80;
            circ.Height = 80;
            circ.Fill = Brushes.White;
            circ.Stroke = Brushes.Black;
            circ.StrokeThickness = 5;
            
            //parse tag into row and column
            int row = int.Parse(tag.Substring(0, 1));
            int col = int.Parse(tag.Substring(1, 1));

            //add o to oPlaySpace
            oPlaySpace[row, col] = 1;

            //create circle
            mainGrid.Children.Add(circ);
            Grid.SetRow(circ, row);
            Grid.SetColumn(circ, col);
        }

        //create X
        private void CreateX(string tag)
        {
            Line leftslant = new Line();
            Line rightslant = new Line();

            //create both lines for the x
            leftslant.X1 = 10;
            leftslant.Y1 = 10;
            leftslant.X2 = 90;
            leftslant.Y2 = 90;

            rightslant.X1 = 90;
            rightslant.Y1 = 10;
            rightslant.X2 = 10;
            rightslant.Y2 = 90;

            //create brush as black and thickness of 5 pixels
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            leftslant.StrokeThickness = 5;
            leftslant.Stroke = brush;
            rightslant.StrokeThickness= 5;
            rightslant.Stroke = brush;

            //parse tag into x,y coords
            int row = int.Parse(tag.Substring(0, 1));
            int col = int.Parse(tag.Substring(1, 1));

            //add x to xPlaySpace
            xPlaySpace[row, col] = 1;

            //add lines to the designated grid
            mainGrid.Children.Add(leftslant);
            Grid.SetRow(leftslant, row);
            Grid.SetColumn(leftslant, col);

            mainGrid.Children.Add(rightslant);
            Grid.SetRow(rightslant, row);
            Grid.SetColumn(rightslant, col);
        }

        //probably a better way to do it, but there are only 8 combo's per so whatever
        private int CheckWin()
        {
            if (xPlaySpace[0, 0] != 0 && xPlaySpace[0, 1] != 0 && xPlaySpace[0, 2] != 0)
            {
                //xrow 1
                return 1;
            }
            else if (xPlaySpace[1, 0] != 0 && xPlaySpace[1, 1] != 0 && xPlaySpace[1, 2] != 0)
            {
                //xrow 2
                return 1;
            }
            else if (xPlaySpace[2, 0] != 0 && xPlaySpace[2, 1] != 0 && xPlaySpace[2, 2] != 0)
            {
                //xrow 3
                return 1;
            }
            else if (xPlaySpace[0, 0] != 0 && xPlaySpace[1, 0] != 0 && xPlaySpace[1, 0] != 0)
            {
                //xcol 1
                return 1;
            }
            else if (xPlaySpace[0, 1] != 0 && xPlaySpace[1, 1] != 0 && xPlaySpace[2, 1] != 0)
            {
                //xcol 2
                return 1;
            }
            else if (xPlaySpace[0, 2] != 0 && xPlaySpace[1, 2] != 0 && xPlaySpace[2, 2] != 0)
            {
                //xcol 3
                return 1;
            }
            else if (xPlaySpace[0, 0] != 0 && xPlaySpace[1, 1] != 0 && xPlaySpace[2, 2] != 0)
            {
                //xdiag top left to bot right
                return 1;
            }
            else if (xPlaySpace[0, 2] != 0 && xPlaySpace[1, 1] != 0 && xPlaySpace[2, 0] != 0)
            {
                //xdiag top right to bot left
                return 1;
            }
            else if (oPlaySpace[0, 0] != 0 && oPlaySpace[0, 1] != 0 && oPlaySpace[0, 2] != 0)
            {
                //orow 1
                return 2;
            }
            else if (oPlaySpace[1, 0] != 0 && oPlaySpace[1, 1] != 0 && oPlaySpace[1, 2] != 0)
            {
                //orow 2
                return 2;
            }
            else if (oPlaySpace[2, 0] != 0 && oPlaySpace[2, 1] != 0 && oPlaySpace[2, 2] != 0)
            {
                //orow 3
                return 2;
            }
            else if (oPlaySpace[0, 0] != 0 && oPlaySpace[1, 0] != 0 && oPlaySpace[2, 0] != 0)
            {
                //ocol 1
                return 2;
            }
            else if (oPlaySpace[0, 1] != 0 && oPlaySpace[1, 1] != 0 && oPlaySpace[2, 1] != 0)
            {
                //ocol 2
                return 2;
            }
            else if (oPlaySpace[0, 2] != 0 && oPlaySpace[1, 2] != 0 && oPlaySpace[2, 2] != 0)
            {
                //ocol 3
                return 2;
            }
            else if (oPlaySpace[0, 0] != 0 && oPlaySpace[1, 1] != 0 && oPlaySpace[2, 2] != 0)
            {
                //odiag top left to bot right
                return 2;
            }
            else if (oPlaySpace[0, 2] != 0 && oPlaySpace[1, 1] != 0 && oPlaySpace[2, 0] != 0)
            {
                //odiag top right to bot left
            }
            return 0;
        }

        //xwin show
        private void xWin()
        {
            //winner text show
            TextBox winner = new TextBox();
            winner.Name = "text";
            RegisterName(winner.Name, winner);
            winner.HorizontalAlignment = HorizontalAlignment.Center;
            winner.VerticalAlignment = VerticalAlignment.Top;
            winner.Margin = new Thickness(20,20,20,20);
            winner.FontSize = 40;
            winner.Text = "X Wins!";

            //add elements to window
            gameOver();
            board.Children.Add(winner);
        }

        //owin show
        private void oWin()
        {
            //winner text show
            TextBox winner = new TextBox();
            winner.Name = "text";
            RegisterName(winner.Name, winner);
            winner.HorizontalAlignment = HorizontalAlignment.Center;
            winner.VerticalAlignment = VerticalAlignment.Top;
            winner.Margin = new Thickness(20, 20, 20, 20);
            winner.FontSize = 40;
            winner.Text = "O Wins!";

            //add elements to window
            gameOver();
            board.Children.Add(winner);
        }

        //remove buttons after game is over
        private void gameOver()
        {
            mainGrid.Children.Clear();
            xPlaySpace = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            oPlaySpace = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        }

        //restart game by destroying circles and re-adding the buttons
        private void Restart(object sender, RoutedEventArgs e)
        {
            gameOver();
            TextBox txt = (TextBox)board.FindName("text");
            if(txt != null)
            {
                UnregisterName(txt.Name);
                board.Children.Remove(txt);
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Content = "";
                    string row = i.ToString();
                    string col = j.ToString();
                    button.Tag = row + col;
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;
                    button.PreviewMouseLeftButtonUp += LeftUp;
                    button.MouseRightButtonUp += RightUp;
                    button.Height = 100;
                    button.Width = 100;
                    mainGrid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                }
            }
        }
    }
}
