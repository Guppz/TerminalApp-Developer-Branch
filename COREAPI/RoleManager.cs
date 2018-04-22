using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace COREAPI
{
    public class RoleManager
    {
        private RoleCrud CrudFactory;
        private ViewCrud VCrud;
        private ValueListCrud VLCrud;

        public RoleManager()
        {
            CrudFactory = new RoleCrud();
            VCrud = new ViewCrud();
            VLCrud = new ValueListCrud();

        }

        public void Create(Role role)
        {
            try
            {
                if (!String.IsNullOrEmpty(role.Name))
                {

                    var rol = CrudFactory.CreateRole(role);
                    var valueList = new ValueListSelect
                    {
                        IdList = "Role",
                        Value = rol.IdRole.ToString(),
                        Description = rol.Name

                    };
                    VLCrud.Create(valueList);

                    foreach (View RolxV in role.ViewsPerRole)
                    {
                        CrudFactory.CreateRolexView(RolxV, role);
                    }
                }
                else {

                    throw new BusinessException(53);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Role role)
        {
            try
            {
                CrudFactory.Delete(role);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Role> RetrieveAll()
        {
            List<Role> lstRole = CrudFactory.RetrieveAll<Role>();
            foreach (Role u in lstRole)
            {
                u.ViewsPerRole = VCrud.RetrieveRolesByView<View>(u);
            }

            return lstRole;
        }

        public void Update(Role role)
        {
            try
            {
                List<View> list = new List<View>();
                list = role.ViewsPerRole;
                VCrud.DelteRolexView(role);
                CrudFactory.Update(role);

                var valueList = new ValueListSelect
                {
                    IdList = "Role",
                    Value = role.IdRole.ToString(),
                    Description = role.Name

                };
                VLCrud.Update(valueList);

                foreach (View RolxV in list)
                {

                    CrudFactory.CreateRolexView(RolxV, role);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Role RetrieveById(Role idRole)
        {
            Role r = null;
            try
            {
                r = CrudFactory.Retrieve<Role>(idRole);
                if (r == null)
                {
                    throw new BusinessException(6);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return r;
        }

        public void UpdateActivacion(Role role)
        {
            try
            {
                role.Status = ActivationValidarior(role);
                CrudFactory.ActivateRole(role);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateDesactivation(Role role)
        {
            try
            {
                role.Status = DesactiveValidarior(role);
                CrudFactory.DesactiveRole(role);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void ValidateNonExistingRole(Role role)
        {
            List<Role> ListRol = CrudFactory.RetrieveRolesByUser<Role>(role);

            if (ListRol != null)
            {
                throw new BusinessException(3);
            }
        }

        public void ValidateFields(Role role)
        {
            PropertyInfo[] props = role.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(role, null);

                if (valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }
        }

        public int ActivationValidarior(Role role)
        {
            if (role.Status == 1)
            {
                throw new BusinessException(35);

            }
            return 1;
        }

        public int DesactiveValidarior(Role role)
        {
            if (role.Status == 0)
            {
                throw new BusinessException(37);

            }
            return 1;
        }

    }

}
