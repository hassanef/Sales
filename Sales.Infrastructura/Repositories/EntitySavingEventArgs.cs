using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructura.Repositories
{
    public class EntitySavingEventArgs<T> : EventArgs
    {
        public T SavedEntity;

    }

  
}

