using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.DIYListBox.ListBox;
using ChangLiao.DIYListBox.ListItem;

namespace ChangLiao.DIYListBox.ItemCollection
{
    [System.ComponentModel.ListBindable(false)]
    class ConversationListItemCollection : IList, ICollection, IEnumerable
    {
        private ConversationListBox m_owner;
        private ConversationListItem[] m_arr;
        public ConversationListItemCollection(ConversationListBox owner)
        {
            m_owner = owner;
        }
        private int count;
        internal ConversationListBox Owner { get { return m_owner; } }

        public ConversationListItem this[int index] { get { return m_arr[index]; } set => m_arr[index] = value; }

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public int Count => count;

        public object SyncRoot => this;

        public bool IsSynchronized => false;

        object IList.this[int index] { get => ((IList)m_arr)[index]; set => ((IList)m_arr)[index] = value; }

        public void Add(ConversationListItem value)
        {
            if (value == null)
            {
                throw new ArgumentException("item is null");
            }
            EnsureSpace(1);
            if (-1 == Array.IndexOf(m_arr, value))
            {
                m_arr[count++] = value;
                m_owner.Invalidate();
            }
        }

        public void Add(List<ConversationListItem> listItems)
        {
            count = 0;
            m_arr = null;
            m_arr = listItems.ToArray();
            count = listItems.Count;
            m_owner.Invalidate();
        }

        public void Clear()
        {
            count = 0;
            m_arr = null;
            m_owner.Invalidate();
        }

        public bool Contains(object value)
        {
            bool res = false;
            ConversationListItem listItem = (ConversationListItem)value;
            foreach (ConversationListItem item in m_arr)
            {
                if (item.conversation.conversationId() == listItem.conversation.conversationId())
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public void CopyTo(Array array, int index)
        {
            m_arr.CopyTo(array as object[], index);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0, Len = this.Count; i < Len; i++)
                yield return m_arr[i];
        }

        public int IndexOf(object value)
        {
            ConversationListItem item = (ConversationListItem)value;
            int i = 0;
            bool ishave = false;
            foreach (var m in m_arr)
            {
                if (m.conversation.conversationId() == item.conversation.conversationId())
                {
                    ishave = true;
                    break;
                }
                i++;
            }
            if (!ishave)
            {
                throw new AggregateException("not have value");
            }
            return i;
        }

        public void Insert(int index, object value)
        {
            if (value.GetType() != typeof(ConversationListItem))
                throw new AggregateException("this object is not ConversationListItem");
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            if (value == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = this.Count; i > index; i--)
                m_arr[i] = m_arr[i - 1];
            m_arr[index] = (ConversationListItem)value;
            count++;
            this.m_owner.Invalidate();
        }
        private void EnsureSpace(int elements)
        {
            if (m_arr == null)
                m_arr = new ConversationListItem[Math.Max(elements, 4)];
            else if (this.Count + elements > m_arr.Length)
            {
                ConversationListItem[] arrTemp = new ConversationListItem[Math.Max(this.Count + elements, m_arr.Length * 2)];
                m_arr.CopyTo(arrTemp, 0);
                m_arr = arrTemp;
            }
        }

        public void Remove(object value)
        {
            if(!(value is ConversationListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            int index = this.IndexOf(value);
            if (-1 != index)        //如果存在元素 那么根据索引移除
                this.RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            this.count--;
            for (int i = index, Len = this.count; i < Len; i++)
                m_arr[i] = m_arr[i + 1];
            this.m_owner.Invalidate();
        }

        public int Add(object value)
        {
            Add((ConversationListItem)value);
            return IndexOf(value);
        }
    }
}
