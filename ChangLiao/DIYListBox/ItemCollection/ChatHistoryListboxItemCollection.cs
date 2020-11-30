using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangLiao.DIYListBox.ListItem;
using ChangLiao.ChildView;
using System.Collections;

namespace ChangLiao.DIYListBox.ItemCollection
{
    [System.ComponentModel.ListBindable(false)]
    class ChatHistoryListboxItemCollection : IList, ICollection, IEnumerable
    {
        private ChatHistoryListBox m_owner;
        private ArrayList m_item;
        private int count;
        public ChatHistoryListBox Owner { get => m_owner; }

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public int Count => m_item.Count;

        public object SyncRoot => this;

        public bool IsSynchronized => false;
        public object this[int index] { get => m_item[index]; set => m_item[index] = value; }

        public ChatHistoryListboxItemCollection(ChatHistoryListBox chatHistoryListBox)
        {
            m_owner = chatHistoryListBox;
        }

        public int Add(object value)
        {
            if (value == null)
            {
                throw new ArgumentException("item is null");
            }
            if(m_item == null)
            {
                m_item = new ArrayList();
            }
           return m_item.Add(value);
        }
        public void Add(ArrayList listItems)
        {
            m_item = listItems;
            m_owner.Invalidate();
        }

        public void Clear()
        {
            count = 0;
            m_item = null;
            m_owner.Invalidate();
        }

        public bool Contains(object value)
        {
            string time= null;
            ChatHistoryListItem model = null;
            if(value is string)
            {
                time = (string)value;
            }
            else
            {

                model = (ChatHistoryListItem)value;
            }
            foreach (object item in m_item)
            {
                if(item is string)
                {
                    if((string)item== time)
                    {
                        return true;
                    }
                }
                else
                {
                    ChatHistoryListItem chatHistoryListItem = (ChatHistoryListItem)item;
                    if (chatHistoryListItem.messageId == model.messageId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int IndexOf(object value)
        {
            return m_item.IndexOf(value);
        }

        public void Insert(int index, ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                m_item.Insert(index + i, list[i]);
            }
        }

        public void Insert(int index, object value)
        {
            m_item.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_item.Remove(value);
        }

        public void RemoveAt(int index)
        {
            m_item.RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            m_item.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return m_item.GetEnumerator();
        }
    }
}
