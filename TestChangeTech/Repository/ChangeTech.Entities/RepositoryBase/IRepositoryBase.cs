using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;

namespace ChangeTech.Entities
{
    public interface IRepositoryBase
    {
        void Load<TEntity, TReference>(TEntity entity, Expression<Func<TEntity, EntityReference<TReference>>> referenceSelector)
            where TEntity : class
            where TReference : EntityObject;

        void Load<TEntity, TCollection>(TEntity entity, Expression<Func<TEntity, EntityCollection<TCollection>>> referenceSelector)
            where TEntity : class
            where TCollection : EntityObject;
    }
}
