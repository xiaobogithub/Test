using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace ChangeTech.Entities
{
    public interface IDynamicObjectContext
    {
        // The members of this interface should be keep as minimal as posssible
        // So try to generalize you operations from your repository class, and extract it into the RepositoryBase class, where you 
        // have the full access to the DynamicObjectContext by its ObjectContext property.
        // Reviewed by Jesse

        ObjectQuery<T> GetObjectQuery<T>() where T : EntityObject;
        ObjectQuery<object> GetObjectQuery(string type);
        void DeleteObject<T>(T entity) where T : IEntityWithKey;
        void Attach<TEntity>(TEntity entity) where TEntity : IEntityWithKey;
        void Detach<TEntity>(TEntity entity) where TEntity : IEntityWithKey;
        T LoadByKey<T>(String propertyName, Object keyValue);
    }
}
