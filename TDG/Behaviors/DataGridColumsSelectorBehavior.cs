using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace TDG.Behaviors
{
    [ContentProperty("ColumnsMaps")]
    public class DataGridColumsSelectorBehavior : Behavior<DataGrid>
    {
        static readonly EventHandler ItemSourceChanged = (sender, args) =>
            {
                if (sender is DataGrid dataGrid)
                {
                    var itemsSource = dataGrid.ItemsSource;
                    var selectorBehavior = Interaction.GetBehaviors(dataGrid)
                        .OfType<DataGridColumsSelectorBehavior>()
                        .FirstOrDefault();
                    if (selectorBehavior != null)
                    {
                        dataGrid.Columns.Clear();
                        var elementType = itemsSource.GetFirstOrDefault()?.GetType();
                        if (elementType != null)
                        {
                            var map = selectorBehavior.ColumnsMaps
                                .FirstOrDefault(item => item.Type.IsAssignableFrom(elementType));
                            if (map != null)
                            {
                                foreach (var column in map.Columns)
                                {                                 
                                    dataGrid.Columns.Add(column);
                                }
                            }
                        }
                    }
                }
            };

        protected override void OnAttached()
        {
            base.OnAttached();
            var dataGrid = AssociatedObject;
            if (dataGrid.IsLoaded)
            {
                Wire(dataGrid);
            }
            else
            {
                RoutedEventHandler lh = null;
                lh = (sender, arg) =>
                    {
                        if (sender is DataGrid dg)
                        {
                            dg.Loaded -= lh;
                            Wire(dg);
                        }
                    };
                dataGrid.Loaded += lh;
            }
        }

        protected override void OnDetaching()
        {
            UnWire(AssociatedObject);
            base.OnDetaching();
        }

        void Wire(DataGrid dataGrid)
        {
            dataGrid.AutoGenerateColumns = false;

            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(dataGrid, ItemSourceChanged);
            }
        }


        void UnWire(DataGrid dataGrid)
        {
            dataGrid.AutoGenerateColumns = false;

            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.RemoveValueChanged(dataGrid, ItemSourceChanged);
            }
        }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<DataGridColumnsMap> ColumnsMaps { get; set; } = new List<DataGridColumnsMap>();
    }
}
