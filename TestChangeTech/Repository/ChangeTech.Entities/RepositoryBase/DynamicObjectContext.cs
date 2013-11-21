using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;

namespace ChangeTech.Entities
{
    public partial class DynamicObjectContext : ObjectContext, IDynamicObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initialize a new DynamicObjectContext object.
        /// </summary>
        public DynamicObjectContext(string connectionString, string entitesContainerName) :
            base(connectionString, entitesContainerName)
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new DynamicObjectContext object.
        /// </summary>
        public DynamicObjectContext(global::System.Data.EntityClient.EntityConnection connection, string entitesContainerName) :
            base(connection, entitesContainerName)
        {
            this.OnContextCreated();
        }

        partial void OnContextCreated();

        #endregion

        /// <summary>
        /// Generic method for getting ObjectQuery
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns></returns>
        public ObjectQuery<T> GetObjectQuery<T>() where T : EntityObject
        {
            if (!_ObjectQueries.ContainsKey(typeof(T).ToString()))
            {
                _ObjectQueries[typeof(T).ToString()] = base.CreateQuery<T>("[" + typeof(T).Name + "]");
            }
            return (ObjectQuery<T>)_ObjectQueries[typeof(T).ToString()];

        }

        public ObjectQuery<object> GetObjectQuery(string type)
        {
            if (!_ObjectQueries.ContainsKey(type))
            {
                _ObjectQueries[type] = base.CreateQuery<object>("[" + type + "]");
            }

            return (ObjectQuery<object>)_ObjectQueries[type];
        }

        Dictionary<string, ObjectQuery> _ObjectQueries = new Dictionary<string, ObjectQuery>();

        /// <summary>
        /// Adds a typed entity object to the object context
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity object</param>
        public void AddTo<T>(T entity) where T : EntityObject
        {
            base.AddObject(typeof(T).Name, entity);
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void DeleteObject<T>(T entity) where T : IEntityWithKey
        {
            base.DeleteObject(entity);
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Attach<TEntity>(TEntity entity) where TEntity : IEntityWithKey
        {
            base.Attach(entity);
        }

        /// <summary>
        /// Detach the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Detach<TEntity>(TEntity entity) where TEntity : IEntityWithKey
        {
            base.Detach(entity);
        }

        public T LoadByKey<T>(String propertyName, Object keyValue)
        {
            IEnumerable<KeyValuePair<string, object>> entityKeyValues =
             new KeyValuePair<string, object>[] {
            new KeyValuePair<string, object>(propertyName, keyValue) };

            // Create the  key for a specific SalesOrderHeader object. 
            EntityKey key = new EntityKey(this.GetType().Name + "." + typeof(T).Name, entityKeyValues);
            return (T)base.GetObjectByKey(key);
        }
    }
}
