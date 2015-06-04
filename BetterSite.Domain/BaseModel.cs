using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
   public class BaseModel
    {
       protected int page;

       public int Page
        {
            get { return page; }
            set { page = value; }
        }
        protected int rows;

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        protected string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; }
        }
        protected string sort;

        public string Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        protected string order;

        public string Order
        {
            get { return order; }
            set { order = value; }
        }
    }
}
