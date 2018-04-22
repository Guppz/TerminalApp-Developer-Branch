using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}
