using Diary.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Diary.Utilities
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush FalseColor { get; set; } = Brushes.Black;
        public Brush TrueColor { get; set; } = Brushes.Gray;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bvalue = (bool)value;
            return !bvalue ? FalseColor : TrueColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PriorityToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TaskPriority priority = (TaskPriority)value;
            return SingleTaskModel.PriorityDescription(priority);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PriorityToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(TaskPriority)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TaskPriority)(int)value;
        }
    }
    public class PriorityToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TaskPriority priority = (TaskPriority)value;
            switch(priority)
            {
                case TaskPriority.LessImportant:
                    return Brushes.Olive;
                case TaskPriority.Important:
                    return Brushes.Orange;
                case TaskPriority.Critical:
                    return Brushes.OrangeRed;
                default:
                    throw new Exception("Unknown task priority");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bvalue = (bool)value;
            return bvalue ? TextDecorations.Strikethrough: null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TaskConverter : IMultiValueConverter
    {
        PriorityToInt pzti = new PriorityToInt();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string description = (string)values[0];
            DateTime creationDate = DateTime.Now;
            DateTime? realizationDate = (DateTime?)values[1];
            TaskPriority priority = (TaskPriority)pzti.ConvertBack(values[2], typeof(TaskPriority), null, CultureInfo.CurrentCulture);

            if (!string.IsNullOrWhiteSpace(description) && realizationDate.HasValue)
                return new ViewModel.SingleTaskViewModel(description, creationDate, realizationDate.Value, priority, false);
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
