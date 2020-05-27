﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(EllipseArcCADShape))]
	public class EllipseArcShapeCommand : IBindable, IShapeCommand
	{
		[DataMember]
		private string _descriptionName = "Ellipse Arc";

		public EllipseArcShapeCommand()
		{
			Shape = new EllipseArcCADShape(this);
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "EllipseArcShape");
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "PolygonShape");
		}

		[DataMember]
		public ICADShape Shape { get; private set; }

		public string DescriptionName
		{
			get
			{
				return _descriptionName;
			}
			set
			{
				_descriptionName = value;
				OnPropertyChanged("DescriptionName");
			}
		}

		public ListViewItem RecipeViewItem { get; private set; }

		public UserControl OptionView => EllipseArcShapeCommandOptionView.Show(this);

		[DataMember]
		public IActionCommand ParentCommand { get; private set; } = null;

		[DataMember]
		public List<IActionCommand> ChildrenCommands { get; private set; } = null;

		public void OnCompile()
		{
		}

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as EllipseArcCADShape).UpdateControl;
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
			CADCanvas.MainCanvas.SizeChanged -= (Shape as EllipseArcCADShape).UpdateControl;
		}

		public void OnMove()
		{
		}

		public void OnSelect()
		{
			Shape.OnSelect();
		}

		public void OnDeselect()
		{
			Shape.OnDeselect();
		}

		public void OnRun()
		{
		}

		public void OnPause()
		{
		}

		public void OnStop()
		{
		}

		public void OnReset()
		{
		}

		public void OnRecipeStart()
		{
		}

		public void OnRecipeStop()
		{
		}

		public void OnRecipeFinish()
		{
		}
	}
}