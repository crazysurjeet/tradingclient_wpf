﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace EquityTrading.Client.Utils
{
    public static class DataGridExtension
    {
        public static readonly DependencyProperty HideAnnotatedColumnsProperty = DependencyProperty.RegisterAttached(
            "HideAnnotatedColumns",
            typeof(bool),
            typeof(DataGridExtension),
            new UIPropertyMetadata(false, OnHideAnnotatedColumns));

        public static bool GetHideAnnotatedColumns(DependencyObject d)
        {
            return (bool)d.GetValue(HideAnnotatedColumnsProperty);
        }

        public static void SetHideAnnotatedColumns(DependencyObject d, bool value)
        {
            d.SetValue(HideAnnotatedColumnsProperty, value);
        }

        private static void OnHideAnnotatedColumns(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool hideAnnotatedColumns = (bool)e.NewValue;

            DataGrid dataGrid = d as DataGrid;

            if (hideAnnotatedColumns)
            {
                dataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            }
            else
            {
                dataGrid.AutoGeneratingColumn -= dataGrid_AutoGeneratingColumn;
            }
        }

        private static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor != null)
            {
                foreach (var item in propertyDescriptor.Attributes)
                {
                    if (item.GetType() == typeof(HideColumnIfAutoGenerated))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
    public class HideColumnIfAutoGenerated : System.Attribute
    {
        public HideColumnIfAutoGenerated()
        {
        }
    }
}
