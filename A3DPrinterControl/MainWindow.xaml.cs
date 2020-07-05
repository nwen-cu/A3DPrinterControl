using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
using System.Xml.Serialization;

namespace A3DPrinterControl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Static handles
		public static MainWindow Instance;

		public static TabControl OptionTabContainer;
		public static Grid CommandOptionContainer;
		public static TabItem MotionOptionTabpage;
		public static Grid MotionOptionContainer;
		public static TabItem InfillOptionTabpage;
		public static Grid InfillOptionContainer;
		public MainWindow()
		{
			Instance = this;
			InitializeComponent();
			OptionTabContainer = FindName("OptionTabs") as TabControl;
			CommandOptionContainer = FindName("CommandOptionPanel") as Grid;
			MotionOptionTabpage = FindName("MotionOptionTab") as TabItem;
			MotionOptionContainer = FindName("MotionOptionPanel") as Grid;
			InfillOptionTabpage = FindName("InfillOptionTab") as TabItem;
			InfillOptionContainer = FindName("InfillOptionPanel") as Grid;
			MainController.Initialize();
			InitializeLateBinding();
			CADCanvas.InitializeCanvas();
		}

		public void InitializeLateBinding()
		{
			CanvasWidthBox.SetBinding(TextBox.TextProperty, new Binding("CanvasWidth"));
			CanvasWidthBox.DataContext = CADCanvas.Binder;

			CanvasHeightBox.SetBinding(TextBox.TextProperty, new Binding("CanvasHeight"));
			CanvasHeightBox.DataContext = CADCanvas.Binder;

			RecipeView.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("CommandList"));
			RecipeView.DataContext = Recipe.Binder;
		}

		public void InitializeExternalEventHandler()
		{ 
			
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

		private void CompileButton_Click(object sender, RoutedEventArgs e)
		{
			MainController.Compile();
		}

		private void TestButton_Click(object sender, RoutedEventArgs e)
		{
			//Serializer
			Debug.Log("");
			DataContractSerializer serializer = new DataContractSerializer(typeof(ActionCommandCollection));
			MemoryStream ms = new MemoryStream();
			StreamReader sr = new StreamReader(ms);
			serializer.WriteObject(ms, Recipe.CommandList);
			ms.Seek(0, SeekOrigin.Begin);
			Debug.Log(sr.ReadToEnd());
			ms.Seek(0, SeekOrigin.Begin);
			//Recipe.ClearCommand();
			sr.Close();

			//Add tree
			Recipe.AddCommand(new LineShapeCommand());
			Recipe.CommandList[0].ChildrenCommands.Add(new RectangleShapeCommand());
			Recipe.CommandList[0].ChildrenCommands.Add(new PolygonShapeCommand()); 
			Recipe.CommandList[0].ChildrenCommands[0].ChildrenCommands.Add(new RectangleShapeCommand());
			Recipe.CommandList[0].ChildrenCommands[0].ChildrenCommands.Add(new PolygonShapeCommand());


		}

		private void CommandMoveUp_Click(object sender, RoutedEventArgs e)
		{
			Recipe.CommandMoveUp(Recipe.SelectedCommand);
		}

		private void CommandMoveDown_Click(object sender, RoutedEventArgs e)
		{
			Recipe.CommandMoveDown(Recipe.SelectedCommand);
		}

		private void CommandLevelUp_Click(object sender, RoutedEventArgs e)
		{
			Recipe.CommandLevelUp(Recipe.SelectedCommand);
		}

		private void CommandLevelDown_Click(object sender, RoutedEventArgs e)
		{
			Recipe.CommandLevelDown(Recipe.SelectedCommand);
		}

		private void PrimitiveLine_Click(object sender, RoutedEventArgs e)
		{
			LineShapeCommand cmd = new LineShapeCommand();
			Recipe.AddCommand(cmd);
		}

		private void PrimitiveRectangle_Click(object sender, RoutedEventArgs e)
		{
			RectangleShapeCommand cmd = new RectangleShapeCommand();
			Recipe.AddCommand(cmd);
		}

		private void PrimitivePolygon_Click(object sender, RoutedEventArgs e)
		{
			PolygonShapeCommand cmd = new PolygonShapeCommand();
			Recipe.AddCommand(cmd);
		}

		private void PrimitiveEllipseArc_Click(object sender, RoutedEventArgs e)
		{
			EllipseArcShapeCommand cmd = new EllipseArcShapeCommand();
			Recipe.AddCommand(cmd);
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

		private void RibbonGallery_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (double.TryParse((sender as RibbonGallery).SelectedValue.ToString().Replace("%", ""), out double result))
			{
				CADCanvas.CanvasZoom = result / 100;
			}
		}

		private void RibbonMenuNew_Click(object sender, RoutedEventArgs e)
		{
			Recipe.ClearCommand(Recipe.CommandList);
		}

		private void RibbonMenuLoad_Click(object sender, RoutedEventArgs e)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(ActionCommandCollection));
			OpenFileDialog fd = new OpenFileDialog() { Filter="Recipe Files(*.rcp)|*.rcp" };
			if (fd.ShowDialog() != true) return;
			if (string.IsNullOrEmpty(fd.FileName)) return;
			FileStream fs = new FileStream(fd.FileName, FileMode.Open);
			ActionCommandCollection rcp = serializer.ReadObject(fs) as ActionCommandCollection;
			fs.Close();
			rcp.ToList().ForEach(cmd => Recipe.AddCommand(cmd));
		}

		private void RibbonMenuSave_Click(object sender, RoutedEventArgs e)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(ActionCommandCollection));
			SaveFileDialog fd = new SaveFileDialog() { Filter = "Recipe Files(*.rcp)|*.rcp", AddExtension = true, DefaultExt = "rcp" };
			if (fd.ShowDialog() != true) return;
			if (string.IsNullOrEmpty(fd.FileName)) return;
			FileStream fs = new FileStream(fd.FileName, FileMode.Create);
			serializer.WriteObject(fs, Recipe.CommandList);
			fs.Close();
		}

		private void RecipeViewItem_Selected(object sender, RoutedEventArgs e)
		{
			Recipe.CommandSelected((sender as TreeViewItem).DataContext as IActionCommand);
			e.Handled = true;
		}

		private void RecipeViewItemContextMenuDelete_Click(object sender, RoutedEventArgs e)
		{
			Recipe.RemoveCommand((((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TreeViewItem).DataContext as IActionCommand);
			e.Handled = true;
		}
	}
}
