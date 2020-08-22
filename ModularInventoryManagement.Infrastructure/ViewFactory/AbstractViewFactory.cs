using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace ModularInventoryManagement.Infrastructure.ViewFactory
{
    public abstract class AbstractViewFactory : IViewFactory
    {
        private IDictionary<string, Func<Page>> mPageCreators;
        protected IDictionary<string, Func<Page>> PageCreators
        {
            get => mPageCreators;
            set
            {
                mPageCreators = value;
                NotifyPropertyChanged();
            }
        }
        public abstract string Title { get; }
        public virtual Func<Page> MasterPageFactory { get; protected set; }
        private IEnumerable<string> mAllowedPages;
        public virtual IEnumerable<string> AllowedPages
        {
            protected get => mAllowedPages;
            set
            {
                if (mAllowedPages == value) return;
                mAllowedPages = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AbstractViewFactory()
        {
            PropertyChanged += (s, e) => UpdateMasterPageFactory();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateMasterPageFactory()
        {
            if (mPageCreators is null) return;
            IEnumerable<Func<Page>> allowedPageCreators;
            if (mAllowedPages is null || !mAllowedPages.Any())
            {
                allowedPageCreators = mPageCreators.AsEnumerable()
                                    .Select(pair => pair.Value)
                                    .ToList();
            } else
            {
                allowedPageCreators = mPageCreators.AsEnumerable()
                                    .Where(pair => mAllowedPages.Contains(pair.Key))
                                    .Select(pair => pair.Value)
                                    .ToList();
            }
            MasterPageFactory = () => CreateTabbedPage(allowedPageCreators
                                                    .Select(func => func()));
        }

        private TabItem CreateTab(Page page)
        {
            return new TabItem()
            {
                Header = page.Title,
                Content = new Frame()
                {
                    Content = page
                }
            };
        }

        protected Page CreateTabbedPage(IEnumerable<Page> pages)
        {
            if (pages is null || !pages.Any()) return new Page();
            if (pages.Count() == 1) return pages.FirstOrDefault();
            var tabItems = pages.Select(page => CreateTab(page));
            var tabControl = new TabControl()
            {
                ItemsSource = tabItems
            };
            return new Page()
            {
                Content = tabControl
            };
        }
    }
}
