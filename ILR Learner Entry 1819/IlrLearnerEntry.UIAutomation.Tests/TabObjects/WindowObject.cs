using System;

using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TabItems;


namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public abstract class WindowObject
    {
        protected readonly Window _window;

        protected WindowObject(Window window)
        {
            _window = window;
        }

        protected Button Button(string title)
        {
            return _window.Get<Button>(SearchCriteria.ByText(title));
        }

        protected TextBox TextBox(int index = 0)
        {
            return _window.Get<TextBox>(SearchCriteria.Indexed(index));
        }

        protected TextBox TextBox(string name)
        {
            return _window.Get<TextBox>(SearchCriteria.ByAutomationId(name));
        }

        protected ListView ListView()
        {
            return _window.Get<ListView>();
        }

        protected DateTimePicker DateTimePicker(string name)
        {
            return _window.Get<DateTimePicker>(SearchCriteria.ByAutomationId(name));
        }

        protected ListBox ListBox()
        {
            return _window.Get<ListBox>();
        }

        protected Label Label(string automationId)
        {
            return _window.Get<Label>(SearchCriteria.ByAutomationId(automationId));
        }

        protected RadioButton RadioButton(int index = 0)
        {
            return _window.Get<RadioButton>(SearchCriteria.Indexed(index));
        }

        public ComboBox ComboBox()
        {
            return _window.Get<ComboBox>();
        }
        public ComboBox ComboBox(string name)
        {
            return _window.Get<ComboBox>(SearchCriteria.ByAutomationId(name));
        }

        public CheckBox CheckBox(int index = 0)
        {
            return _window.Get<CheckBox>(SearchCriteria.Indexed(index));
        }
        public Tab Tab(string caption)
        {
            return _window.Get<Tab>(caption);
        }
        
    }
}
