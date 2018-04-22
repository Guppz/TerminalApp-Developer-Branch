using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public interface ISqlStaments
    {
        SqlOperation GetCreateStatement(BaseEntity entity);
        SqlOperation GetUpdateStatement(BaseEntity entity);
        SqlOperation GetRetrieveAllStatement();
        SqlOperation GetDeleteStatement(BaseEntity entity);
        SqlOperation GetRetriveStatement(BaseEntity entity);
    }
}
