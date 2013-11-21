using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace iMeta.Windows.Commands
{
    public static class Click
    {
        private static readonly DependencyProperty ClickCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "ClickCommandBehavior", typeof(ButtonClickCommandBehavior), typeof(Click), null);


        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof(ICommand), typeof(Click), new PropertyMetadata(OnSetCommandCallback));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
            "CommandParameter", typeof(object), typeof(Click), new PropertyMetadata(OnSetCommandParameterCallback));
       
        public static void SetCommand(ButtonBase buttonBase, ICommand command)
        {
            buttonBase.SetValue(CommandProperty, command);
        }
       
        public static ICommand GetCommand(ButtonBase buttonBase)
        {
            return buttonBase.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommandParameter(ButtonBase buttonBase, object parameter)
        {
            buttonBase.SetValue(CommandParameterProperty, parameter);
        }

        public static object GetCommandParameter(ButtonBase buttonBase)
        {
            return buttonBase.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var buttonBase = dependencyObject as ButtonBase;
            if (buttonBase != null)
            {
                var behavior = GetOrCreateBehavior(buttonBase);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var buttonBase = dependencyObject as ButtonBase;
            if (buttonBase != null)
            {
                var behavior = GetOrCreateBehavior(buttonBase);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static ButtonClickCommandBehavior GetOrCreateBehavior(ButtonBase buttonBase)
        {
            var behavior = buttonBase.GetValue(ClickCommandBehaviorProperty) as ButtonClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new ButtonClickCommandBehavior(buttonBase);
                buttonBase.SetValue(ClickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
}