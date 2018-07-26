// using System;
//  using System.Collections.Generic;
//  using System.Windows;
//  using System.Windows.Controls.Primitives;
// using System.Windows.Input;
// using System.Windows.Controls;
//  using System.ComponentModel;

//namespace O2.FileManager.Helpers.Behaviors
//{
//    /// <summary>
//    ///     Selector MouseDoubleClick calling ViewModel ICommand Behavior
//    ///     using Blend3 Microsoft.Expression.Interactivity Dll
//    /// </summary>
//    public class InteractionsSelectorDoubleClickCommandAction :
//        TargetedTriggerAction<FrameworkElement>,
//        ICommandSource
//    {
//        #region DPs

//        #region Command DP

//        /// <summary>
//        ///     The actual Command to fire when the
//        ///     EventTrigger occurs, thus firing this
//        ///     InteractionsSelectorDoubleClickCommandAction
//        /// </summary>
//        [Category("Command Properties")]
//        public ICommand Command
//        {
//            get => (ICommand) GetValue(CommandProperty);
//            set => SetValue(CommandProperty, value);
//        }

//        public static readonly DependencyProperty CommandProperty =
//            DependencyProperty.Register(
//                "Command", typeof(ICommand),
//                typeof(InteractionsSelectorDoubleClickCommandAction),
//                new PropertyMetadata(
//                    null, OnCommandChanged));

//        private static void OnCommandChanged(DependencyObject d,
//            DependencyPropertyChangedEventArgs e)
//        {
//            var action =
//                (InteractionsSelectorDoubleClickCommandAction) d;
//            action.OnCommandChanged((ICommand) e.OldValue,
//                (ICommand) e.NewValue);
//        }

//        #region Command implementation

//        /// <summary>
//        ///     This is a strong reference to the Command.
//        ///     CanExecuteChanged event handler.
//        ///     The commanding system uses a weak
//        ///     reference and if we don’t enforce a
//        ///     strong reference then the event
//        ///     handler will be gc’ed.
//        /// </summary>
//        private EventHandler CanExecuteChangedHandler;


//        private void OnCommandChanged(ICommand oldCommand,
//            ICommand newCommand)
//        {
//            if (oldCommand != null)
//                UnhookCommand(oldCommand);
//            if (newCommand != null)
//                HookCommand(newCommand);
//        }

//        private void UnhookCommand(ICommand command)
//        {
//            command.CanExecuteChanged -=
//                CanExecuteChangedHandler;
//            UpdateCanExecute();
//        }

//        private void HookCommand(ICommand command)
//        {
//            // Save a strong reference to the
//            // Command.CanExecuteChanged event handler.
//            // The commanding system uses a weak
//            // reference and if we don’t save a strong
//            // reference then the event handler will be gc’ed.
//            CanExecuteChangedHandler =
//                OnCanExecuteChanged;
//            command.CanExecuteChanged
//                += CanExecuteChangedHandler;
//            UpdateCanExecute();
//        }

//        private void OnCanExecuteChanged(object sender,
//            EventArgs e)
//        {
//            UpdateCanExecute();
//        }

//        private void UpdateCanExecute()
//        {
//            if (Command != null)
//            {
//                var command =
//                    Command as RoutedCommand;
//                if (command != null)
//                    IsEnabled =
//                        command.CanExecute(
//                            CommandParameter, CommandTarget);
//                else
//                    IsEnabled =
//                        Command.CanExecute(CommandParameter);
//                if (Target != null && SyncOwnerIsEnabled)
//                    Target.IsEnabled = IsEnabled;
//            }
//        }

//        #endregion

//        #endregion

//        #region CommandParameter DP

//        /// <summary>
//        ///     For consistency with the Wpf Command pattern
//        /// </summary>
//        [Category("Command Properties")]
//        public object CommandParameter
//        {
//            get => (object) GetValue(
//                CommandParameterProperty);
//            set => SetValue(CommandParameterProperty, value);
//        }

//        public static readonly DependencyProperty
//            CommandParameterProperty =
//                DependencyProperty.Register(
//                    "CommandParameter", typeof(object),
//                    typeof(InteractionsSelectorDoubleClickCommandAction),
//                    new PropertyMetadata());

//        #endregion

//        #region CommandTarget DP

//        /// <summary>
//        ///     For consistency with the Wpf Command pattern
//        /// </summary>
//        [Category("Command Properties")]
//        public IInputElement CommandTarget
//        {
//            get => (IInputElement) GetValue(
//                CommandTargetProperty);
//            set => SetValue(CommandTargetProperty, value);
//        }

//        public static readonly DependencyProperty
//            CommandTargetProperty =
//                DependencyProperty.Register(
//                    "CommandTarget", typeof(IInputElement),
//                    typeof(InteractionsSelectorDoubleClickCommandAction),
//                    new PropertyMetadata());

//        #endregion

//        #region SyncOwnerIsEnabled DP

//        /// <summary>
//        ///     Allows the user to specify that the
//        ///     owner element should be
//        ///     enabled/disabled whenever the
//        ///     action is enabled/disabled.
//        /// </summary>
//        [Category("Command Properties")]
//        public bool SyncOwnerIsEnabled
//        {
//            get => (bool) GetValue(SyncOwnerIsEnabledProperty);
//            set => SetValue(SyncOwnerIsEnabledProperty, value);
//        }

//        /// <summary>
//        ///     When SyncOwnerIsEnabled is true
//        ///     then changing
//        ///     InteractionsSelectorDoubleClickCommandAction.
//        ///     IsEnabled
//        ///     will automatically update the owner
//        ///     (Target) IsEnabled property.
//        /// </summary>
//        public static readonly DependencyProperty
//            SyncOwnerIsEnabledProperty =
//                DependencyProperty.Register(
//                    "SyncOwnerIsEnabled", typeof(bool),
//                    typeof(InteractionsSelectorDoubleClickCommandAction),
//                    new PropertyMetadata());

//        #endregion

//        #endregion

//        #region Overrides

//        /// <summary>
//        ///     On attached hook up our own MouseDoubleClick so we
//        ///     can check we actually double click an item
//        /// </summary>
//        protected override void OnAttached()
//        {
//            base.OnAttached();
//            var s = this.AssociatedObject as Selector;
//            if (s != null) s.MouseDoubleClick += OnMouseDoubleClick;
//        }

//        /// <summary>
//        ///     On attached unhook the previously
//        ///     hooked MouseDoubleClick handler
//        /// </summary>
//        protected override void OnDetaching()
//        {
//            base.OnDetaching();
//            var s = this.AssociatedObject as Selector;
//            if (s != null) s.MouseDoubleClick -= OnMouseDoubleClick;
//        }

//        //Must at least implement abstract member invoke
//        protected override void Invoke(object parameter)
//        {
//            //The logic for this is done in the OnMouseDoubleClick
//            //as we only wanto fire command if we are actually on an
//            //Item in the Selector. If the Selector is a ListView we
//            //may have headers so will not want to fire associated
//            //Command when a header is double clicked
//        }

//        #endregion

//        #region Private Methods

//        /// <summary>
//        ///     Handle Selector.MouseDoubleClick but will
//        ///     only fire the associated ViewModel command
//        ///     if the MouseDoubleClick occurred over an actual
//        ///     ItemsControl item. This is nessecary as if we
//        ///     are using a ListView we may have clicked the
//        ///     headers which are not items, so do not want the
//        ///     associated ViewModel command to be run
//        /// </summary>
//        private static void OnMouseDoubleClick(object sender,
//            MouseButtonEventArgs e)
//        {
//            //Get the ItemsControl and then get the item, and
//            //check there is an actual item, as if we are using
//            //a ListView we may have clicked the
//            //headers which are not items
//            var listView = sender as ItemsControl;
//            var originalSender =
//                e.OriginalSource as DependencyObject;
//            if (listView == null || originalSender == null) return;

//            var container =
//                ItemsControl.ContainerFromElement
//                (sender as ItemsControl,
//                    e.OriginalSource as DependencyObject);

//            if (container == null ||
//                container == DependencyProperty.UnsetValue) return;

//            // found a container, now find the item.
//            var activatedItem =
//                listView.ItemContainerGenerator.ItemFromContainer(container);

//            if (activatedItem != null)
//            {
//                var command =
//                    (ICommand) (sender as DependencyObject).GetValue(TheCommandToRunProperty);

//                if (command != null)
//                    if (command.CanExecute(null))
//                        command.Execute(null);
//            }
//        }

//        #endregion
//    }
//}