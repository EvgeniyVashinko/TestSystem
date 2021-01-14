using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestSystem.Pages;

namespace TestSystem.Services
{
    public class PageNavigationService
    {
        private Stack<Type> pages;
        public Action<Page> OnPageChanged;
        public bool CanGoBack => pages.Skip(1).Any();

        public PageNavigationService()
        {
            pages = new Stack<Type>();
        }

        public void Navigate(Page page)
        {
            OnPageChanged?.Invoke(page);
            pages.Push(page.GetType());
        }

        public void ToStartPage()
        {
            pages.Clear();
            Navigate(new StartPage());
        }

        public void GoBack()
        {
            pages.Pop();
            OnPageChanged?.Invoke((Page)Activator.CreateInstance(pages.Peek()));
        }
    }
}
