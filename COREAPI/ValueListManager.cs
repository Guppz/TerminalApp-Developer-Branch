using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
namespace COREAPI
{
    public class ValueListManager
    {
        private ValueListCrud CrudFactory;

        public ValueListManager()
        {
            CrudFactory = new ValueListCrud();
        }

        public List<ValueListSelect> GetValuesList(ValueListSelect idList)
        {
            return CrudFactory.RetrieveSelect<ValueListSelect>(idList);
        }
    }
}
