﻿using Bachelor.Models.Interfaces;
using Bachelor.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bachelor.Services
{
    abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
    {
        private List<T> _Items = new List<T>();
        private int _LastId;
        protected RepositoryInMemory() { }
        protected RepositoryInMemory(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            if (_Items.Contains(item)) return;
            item.Id = ++_LastId;
            _Items.Add(item);
        }

        public IEnumerable<T> GetAll() => _Items;
        public bool Remove(T item) => _Items.Remove(item);

        public void Update(int id, T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Індекс не може буди менше 1");
            if (_Items.Contains(item)) return;

            var db_item = ((IRepository<T>)this).Get(id);
            if (db_item is null)
                throw new InvalidOperationException("Редагований елемент не знайдено в репозиторії");

            Update(item, db_item);
        }

        protected abstract void Update(T Source, T Destination);
    }
}
