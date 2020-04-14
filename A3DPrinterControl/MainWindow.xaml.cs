using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Static handles
		public static MainWindow Instance;
		public static Grid CommandOptionContainer;
		public MainWindow()
		{
			Instance = this;
			InitializeComponent();
			CommandOptionContainer = FindName("CommandOptionPanel") as Grid;
			PyScriptManager.InitializeScriptEngine();
			CanvasWidthBox.Text = 300.ToString();
			CanvasHeightBox.Text = 300.ToString();
		}

		public void ShapeHighlight(object sender, MouseEventArgs e)
		{
			if (e == null || sender == null) return;
			if (e.RoutedEvent == Mouse.MouseEnterEvent)
			{
				(sender as Shape).Stroke = new SolidColorBrush(Colors.Red);
			}
			else if (e.RoutedEvent == Mouse.MouseLeaveEvent)
			{
				(sender as Shape).Stroke = new SolidColorBrush(Colors.Black);
			}


		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			//((sender as Canvas).ToolTip as ToolTip).StaysOpen = true;
			
		}

		private void PrimitiveRectangle_Click(object sender, RoutedEventArgs e)
		{
			RectangleShapeCommand cmd = new RectangleShapeCommand();
			Recipe.AddCommand(cmd);
		}

		private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
		{
			
		}

		private void CanvasWidth_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (double.TryParse((sender as RibbonTextBox).Text, out double result))
			{
				CADCanvas.CanvasWidth = result;
			}
		}
		private void CanvasHeight_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (double.TryParse((sender as RibbonTextBox).Text, out double result))
			{
				CADCanvas.CanvasHeight = result;
			}
		}

		private void RecipeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			e.Handled = true;
		}

		private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(CADCanvas.Container.Children.Count.ToString());
		}

		private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show((sender as Polygon).Points.ToString());
		}

		private void RibbonGallery_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (double.TryParse((sender as RibbonGallery).SelectedValue.ToString().Replace("%", ""), out double result))
			{
				CADCanvas.CanvasZoom = result / 100;
			}
		}
	}
}
