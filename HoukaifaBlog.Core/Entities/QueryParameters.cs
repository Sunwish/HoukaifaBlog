using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using HoukaifaBlog.Core.Interfaces;

namespace HoukaifaBlog.Core.Entities
{
    public abstract class QueryParameters /*: INotifyPropertyChanged*/
    {
        private const int DefaultPageSize = 10;
        private const int DefaultMaxPageSize = 100;

        private int _pageIndex;
        public int PageIndex {
            get => _pageIndex;
            set => _pageIndex = value >= 0 ? value : 0;
        }

        private int _pageSize = DefaultPageSize;
        public virtual int PageSize {
            get => _pageSize;
            set => _pageSize = value; // SetField(ref _pageSize, value);
        }

        private string _orderBy;
        public string OrderBy {
            get => _orderBy;
            set => _orderBy = _orderBy ?? nameof(IEntity.Id);
        }

        private int _maxPageSize = DefaultMaxPageSize;
        protected internal int MaxPageSize {
            get => _maxPageSize;
            set => _maxPageSize = value; // (ref _maxPageSize, value);
        }

        public string Fields { get; set; }

        /*
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetFiled<T>(ref T filed, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(filed, value)) return false;

            filed = value;

            OnPropertyChanged(propertyName);

            if(propertyName == nameof(PageSize) || propertyName == nameof(MaxPageSize))
            {
                SetPageSize();
            }

            return true;
        }
        */
    }
}
