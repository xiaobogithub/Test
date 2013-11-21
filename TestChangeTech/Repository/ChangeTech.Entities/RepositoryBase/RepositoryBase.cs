using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Configuration;
using System.Diagnostics;
using System.Data.EntityClient;
using System.Reflection;
using System.Web;



namespace ChangeTech.Entities
{
    /// <summary>
    /// The base class of all the repository classes.
    /// </summary>
    public abstract class RepositoryBase : IRepositoryBase
    {
        #region constructors

        protected RepositoryBase()
        {
        }

        #endregion

        #region static methods and properties

        protected static string GetSQLConnectionString(string key)
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings[key].ToString();
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                throw new InvalidOperationException("The applicationsetting key " + key + " is not set.");
            }
            return sqlConnectionString;
        }

        protected static string GetDataEntitiesConnectionName()
        {
            string dataEntitiesConnectionName = ConfigurationManager.AppSettings["DataEntitiesConnectionName"];
            if (string.IsNullOrEmpty(dataEntitiesConnectionName))
            {
                throw new InvalidOperationException("The applicationsetting key 'DataEntitiesConnectionName' is not set.");
            }
            return dataEntitiesConnectionName;
        }

        protected static string GetDataEntitiesContainerName()
        {
            string dataEntitiesContainerName = ConfigurationManager.AppSettings["DataEntitiesContainerName"];
            if (string.IsNullOrEmpty(dataEntitiesContainerName))
            {
                throw new InvalidOperationException("The applicationsetting key 'DataEntitiesContainerName' is not set.");
            }
            return dataEntitiesContainerName;
        }

        protected static string DefaultEntitiesConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[GetDataEntitiesConnectionName()].ConnectionString;
            }
        }

        protected static string DefaultEntitiesContainerName
        {
            get
            {
                return GetDataEntitiesContainerName();
            }
        }

        #endregion

        #region Properties

        private static readonly string ObjectContextItemName = "ObjectContext";

        private static readonly object synclock = new object();

        private static IDictionary<string, object> container;

        private static IDictionary<string, object> Container
        {
            get
            {
                if (container == null)
                {
                    lock (synclock)
                    {
                        if (container == null)
                        {
                            container = new Dictionary<string, object>();
                        }
                    }
                }
                return container;
            }
            set
            { container = value; }
        }

        /// <summary>
        /// Gets or sets the object context.
        /// </summary>
        /// <value>The data context.</value>
        /// <remarks>
        /// 2009-10-27: [Chen Pu]   If HttpContext.Current is null, it is used by console application
        ///                         otherwise, it is used by web application
        /// </remarks>
        private static DynamicObjectContext ObjectContext
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items[ObjectContextItemName] == null)
                    {
                        lock (synclock)
                        {
                            if (HttpContext.Current.Items[ObjectContextItemName] == null)
                            {
                                HttpContext.Current.Items[ObjectContextItemName] = new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName);
                            }
                        }
                    }

                    return (DynamicObjectContext)HttpContext.Current.Items[ObjectContextItemName];
                }
                else
                {
                    if (!Container.ContainsKey(ObjectContextItemName))
                    {
                        lock (synclock)
                        {
                            if (!Container.ContainsKey(ObjectContextItemName))
                            {
                                Container.Add(ObjectContextItemName, new DynamicObjectContext(DefaultEntitiesConnectionString, DefaultEntitiesContainerName));
                            }
                        }
                    }
                    return (DynamicObjectContext)Container[ObjectContextItemName];
                }
            }
            //set
            //{
            //    objectContext = value;
            //}
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        protected IDynamicObjectContext Context
        {
            get
            {
                return ObjectContext as IDynamicObjectContext;
            }
        }

        public static void ResetContainer()
        {
            container = null;
        }
        #endregion

        protected virtual bool IsLogTypeFitPrioritySetting(int logTypeID, ObjectContext objectContext)
        {
            ActivityLogSetting activityLogSetting = GetEntities<ActivityLogSetting>(objectContext).FirstOrDefault();
            ActivityLogType activityLogType = GetEntities<ActivityLogType>(a => a.LogTypeID == logTypeID, objectContext).FirstOrDefault();
            if (activityLogType == null) return false;
            if (activityLogType.ActivityLogPriorityReference == null) return false;
            if (!activityLogType.ActivityLogPriorityReference.IsLoaded)
                activityLogType.ActivityLogPriorityReference.Load();
            if (activityLogSetting.LogPriorityID > activityLogType.ActivityLogPriority.LogPriorityID)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            var query = string.Format("SELECT VALUE Entity FROM {0}.{1} AS Entity ",
                GetDataEntitiesContainerName(), typeof(TEntity).Name);
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                query += " WHERE Entity.IsDeleted = false OR Entity.IsDeleted IS NULL";

            }

            ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(query, ObjectContext, MergeOption.OverwriteChanges);  // Changed by Chen Pu on 2009-04-13 from MergeOption.NoTracking because can not update 

            return objectQuery;
        }

        /// <summary>
        /// Gets the entities. Using objectContext outside
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetEntities<TEntity>(ObjectContext objectContext) where TEntity : class
        {
            var query = string.Format("SELECT VALUE Entity FROM {0}.{1} AS Entity ",
                GetDataEntitiesContainerName(), typeof(TEntity).Name);
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                query += " WHERE Entity.IsDeleted = false OR Entity.IsDeleted IS NULL";

            }

            ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(query, objectContext, MergeOption.OverwriteChanges);  // Changed by Chen Pu on 2009-04-13 from MergeOption.NoTracking because can not update 

            return objectQuery;
        }

        /// <summary>
        /// Gets the entities. Using objectContext outside
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryFunc">The query func.</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> queryFunc) where TEntity : EntityObject
        {
            ObjectContext.CommandTimeout = 3600000;
            var results = from entity in ObjectContext.GetObjectQuery<TEntity>()
                          select entity;

            IQueryable<TEntity> tmp = results.Where<TEntity>(queryFunc);
            results = results.Where<TEntity>(queryFunc);

            (results as ObjectQuery<TEntity>).MergeOption = MergeOption.OverwriteChanges;

            return results;
        }

        /// <summary>
        /// Gets the entities. Using objectContext outside
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryFunc">The query func.</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> queryFunc, ObjectContext objectContext) where TEntity : EntityObject
        {
            objectContext.CommandTimeout = 3600000;
            var results = from entity in objectContext.CreateQuery<TEntity>("[" + typeof(TEntity).Name + "]")
                          select entity;

            IQueryable<TEntity> tmp = results.Where<TEntity>(queryFunc);
            results = results.Where<TEntity>(queryFunc);

            (results as ObjectQuery<TEntity>).MergeOption = MergeOption.OverwriteChanges;

            return results;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryFunc">The query func.</param>
        /// <param name="orderKey">The order key.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        protected IQueryable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> queryFunc,
            Expression<Func<TEntity, object>> orderKey,
            bool ascending, int pageIndex, int pageSize, out int totalCount) where TEntity : EntityObject
        {
            IOrderedQueryable<TEntity> orderedQuery;
            if (ascending)
            {
                orderedQuery = ObjectContext.GetObjectQuery<TEntity>().Where<TEntity>(queryFunc).OrderBy(orderKey);
            }
            else
            {
                orderedQuery = ObjectContext.GetObjectQuery<TEntity>().Where<TEntity>(queryFunc).OrderByDescending(orderKey);
            }

            totalCount = orderedQuery.Count<TEntity>();
            var results = orderedQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return results;
        }

        protected IQueryable<Object> GetEntities(string type)
        {
            var results = from entity in ObjectContext.GetObjectQuery(type)
                          select entity;

            (results as ObjectQuery<object>).MergeOption = MergeOption.OverwriteChanges;

            return results;
        }

        protected IQueryable<TEntity> GetEntities<TEntity>(Expression<Func<TEntity, bool>> queryFunc,
            Expression<Func<TEntity, int>> orderKey,
            bool ascending, int pageIndex, int pageSize, out int totalCount) where TEntity : EntityObject
        {
            IOrderedQueryable<TEntity> orderedQuery;
            if (ascending)
            {
                orderedQuery = ObjectContext.GetObjectQuery<TEntity>().Where<TEntity>(queryFunc).OrderBy(orderKey);
            }
            else
            {
                orderedQuery = ObjectContext.GetObjectQuery<TEntity>().Where<TEntity>(queryFunc).OrderByDescending(orderKey);
            }

            totalCount = orderedQuery.Count<TEntity>();
            var results = orderedQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return results;
        }


        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        protected TEntity GetEntityById<TEntity>(object id)
        {
            TEntity entity = default(TEntity);

            var query = string.Format("SELECT VALUE Entity FROM {0}.{1} AS Entity "
                + " WHERE Entity.Id = @Id", GetDataEntitiesContainerName(), typeof(TEntity).Name);

            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                query += " AND Entity.IsDeleted = false OR Entity.IsDeleted IS NULL";
            }

            ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(query, ObjectContext, MergeOption.OverwriteChanges);

            objectQuery.Parameters.Add(new ObjectParameter("Id", id));

            entity = objectQuery.FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void InsertEntity(EntityObject entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            //To cross cutting the DateEntered, DataUpdated
            entity = RefreshEntityColumn(entity, "Created", DateTime.UtcNow);
            entity = RefreshEntityColumn(entity, "LastUpdated", DateTime.UtcNow);

            ObjectContext.AddObject(entity.GetType().Name, entity);
            ObjectContext.SaveChanges();
        }

        /// <summary>
        /// Inserts the entity. Using objectContext outside
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void InsertEntity(EntityObject entity, ObjectContext objectContext)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            //To cross cutting the DateEntered, DataUpdated
            entity = RefreshEntityColumn(entity, "Created", DateTime.UtcNow);
            entity = RefreshEntityColumn(entity, "LastUpdated", DateTime.UtcNow);

            objectContext.AddObject(entity.GetType().Name, entity);
            objectContext.SaveChanges();
        }

        protected void InsertEntities<TEntity>(List<TEntity> entities, Guid userId) where TEntity : EntityObject
        {
            //TEntity[] entityArrary = entities.ToArray();
            foreach (TEntity entity in entities)
            {
                ObjectContext.AddObject(entity.GetType().Name, entity);
            }
            SaveChanges();
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user id.</param>
        protected void UpdateEntity(EntityObject entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity = RefreshEntityColumn(entity, "LastUpdated", DateTime.UtcNow);

            EntityKey key;
            object originalEntity;

            key = ObjectContext.CreateEntityKey(entity.EntityKey.EntitySetName, entity);

            if (ObjectContext.TryGetObjectByKey(key, out originalEntity))
            {
                if (entity.EntityState != EntityState.Added)
                {
                    ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
                }
                else
                {
                    ObjectContext.AcceptAllChanges();
                }

                ObjectContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the entity.Using the objectContext from outside
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="objectContext">The objuctContext from outside</param>
        protected void UpdateEntity(EntityObject entity, ObjectContext objectContext)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity = RefreshEntityColumn(entity, "LastUpdated", DateTime.UtcNow);

            EntityKey key;
            object originalEntity;

            key = objectContext.CreateEntityKey(entity.EntityKey.EntitySetName, entity);

            if (objectContext.TryGetObjectByKey(key, out originalEntity))
            {
                if (entity.EntityState != EntityState.Added)
                {
                    objectContext.ApplyCurrentValues(key.EntitySetName, entity);
                }
                else
                {
                    objectContext.AcceptAllChanges();
                }

                objectContext.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entityId">The entity id.</param>
        /// <param name="userId">The user id.</param>
        protected void DeleteEntity<TEntity>(object entityId, Guid userId) where TEntity : EntityObject
        {
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                TEntity entity = this.GetEntityById<TEntity>(entityId);
                this.DeleteEntityWithDeletedColumn(entity, userId);

                ObjectContext.SaveChanges();
            }
            else
            {
                TEntity entity = this.GetEntityWithReferenceLoadedById<TEntity>(entityId);

                if (entity.EntityState == EntityState.Detached)
                {
                    ObjectContext.Attach(entity);
                }

                ObjectContext.DeleteObject(entity);

                this.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryFunc">The query func.</param>
        /// <param name="userId">The user id.</param>
        protected void DeleteEntity<TEntity>(Expression<Func<TEntity, bool>> queryFunc, Guid userId) where TEntity : EntityObject
        {
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                TEntity entity = this.GetEntities<TEntity>(queryFunc).FirstOrDefault();
                this.DeleteEntityWithDeletedColumn(entity, userId);
                ObjectContext.SaveChanges();
            }
            else
            {
                TEntity entity = this.GetEntityWithReferenceLoadedById<TEntity>(queryFunc);
                if (entity != null)
                {

                    if (entity.EntityState == EntityState.Detached)
                    {
                        ObjectContext.Attach(entity);
                    }

                    ObjectContext.DeleteObject(entity);

                    SaveChanges();
                }
            }
        }

        protected void DeleteEntities<TEntity>(Expression<Func<TEntity, bool>> queryFunc, Guid userId) where TEntity : EntityObject
        {
            List<TEntity> list = this.GetEntities<TEntity>(queryFunc).ToList<TEntity>();
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                foreach (TEntity entity in list)
                {
                    this.DeleteEntityWithDeletedColumn(entity, userId);
                }
                ObjectContext.SaveChanges();
            }
            else
            {
                for (int index = 0; index < list.Count; index++)
                {
                    if (list[index].EntityState == EntityState.Detached)
                    {
                        ObjectContext.Attach(list[index]);
                    }

                    ObjectContext.DeleteObject(list[index]);
                }
                SaveChanges();
            }
        }

        protected void DeleteEntities<TEntity>(EntityCollection<TEntity> entities, Guid userId) where TEntity : EntityObject
        {
            if (this.HasDeletedColumn(typeof(TEntity)))
            {
                foreach (TEntity entity in entities)
                {
                    this.DeleteEntityWithDeletedColumn(entity, userId);
                }
                ObjectContext.SaveChanges();
            }
            else
            {
                TEntity[] entityArrary = entities.ToArray();
                for (int index = 0; index < entityArrary.Length; index++)
                {
                    if (entityArrary[index].EntityState == EntityState.Detached)
                    {
                        ObjectContext.Attach(entityArrary[index]);
                    }

                    ObjectContext.DeleteObject(entityArrary[index]);
                }
                SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entity with reference loaded by id.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        private TEntity GetEntityWithReferenceLoadedById<TEntity>(object entityId)
        {
            TEntity entity = default(TEntity);

            var query = string.Format("SELECT VALUE Entity FROM {0}.{1} AS Entity "
                + " WHERE Entity.Id = @Id", GetDataEntitiesContainerName(), typeof(TEntity).Name);


            ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(query, ObjectContext, MergeOption.NoTracking);

            objectQuery.Parameters.Add(new ObjectParameter("Id", entityId));

            List<string> includePathList = this.GetIncludePathList(typeof(TEntity));
            foreach (string path in includePathList)
            {
                objectQuery = objectQuery.Include(path);
            }

            entity = objectQuery.FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// Gets the entity with reference loaded by id.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="queryFunc">The query func.</param>
        /// <returns></returns>
        private TEntity GetEntityWithReferenceLoadedById<TEntity>(Expression<Func<TEntity, bool>> queryFunc) where TEntity : EntityObject
        {
            TEntity entity = default(TEntity);

            ObjectQuery<TEntity> objectQuery = this.GetEntities<TEntity>(queryFunc) as ObjectQuery<TEntity>;

            List<string> includePathList = this.GetIncludePathList(typeof(TEntity));
            foreach (string path in includePathList)
            {
                objectQuery = objectQuery.Include(path);
            }

            entity = objectQuery.FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// Gets the include path list.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        private List<string> GetIncludePathList(Type entityType)
        {
            List<string> returnList = new List<string>();

            foreach (PropertyInfo property in entityType.GetProperties())
            {
                if (property.PropertyType.BaseType.Equals(typeof(EntityReference)))
                {
                    returnList.Add(property.Name.Replace("Reference", ""));
                }
            }

            return returnList;
        }

        /// <summary>
        /// Deletes the entity with deleted column.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user id.</param>
        private void DeleteEntityWithDeletedColumn(EntityObject entity, Guid userId)
        {
            if (entity != null)
            {
                ObjectStateEntry entry = null;
                if (!ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity.EntityKey, out entry))
                {
                    ObjectContext.Attach(entity);
                    entry = ObjectContext.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
                }
                CurrentValueRecord record = entry.CurrentValues;

                for (int i = 0; i < record.FieldCount; i++)
                {
                    if (record.GetName(i).Equals("IsDeleted"))
                    {
                        record.SetBoolean(i, true);
                    }
                    //else if (record.GetName(i).Equals("LastUpdatedBy") && userId != Guid.NewGuid())
                    //else if (record.GetName(i).Equals("LastUpdatedBy"))
                    //{
                    //    record.SetGuid(i, userId);
                    //}
                    else if (record.GetName(i).Equals("LastUpdated"))
                    {
                        record.SetDateTime(i, DateTime.UtcNow);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        protected void SaveChanges()
        {
            ObjectContext.SaveChanges();
        }

        /// <summary>
        /// Determines whether [has mark for delete].
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// 	<c>true</c> if [has mark for delete]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasDeletedColumn(EntityObject entity)
        {
            //return (entity.GetType().GetProperty("IsDeleted") == null ? false : true);
            return false;
        }

        /// <summary>
        /// Determines whether [has deleted column] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [has deleted column] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasDeletedColumn(Type type)
        {
            return (type.GetProperty("IsDeleted") == null ? false : true);
            //return false;
        }


        private EntityObject RefreshEntityColumn(EntityObject entity, string columnName, object value)
        {
            if (entity.GetType().GetProperty(columnName) != null)
            {
                entity.GetType().GetProperty(columnName).SetValue(entity, value, null);
            }

            return entity;
        }

        #region Load Methods

        /// <summary>
        /// Load the reference entity from the giving entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of giving entity.</typeparam>
        /// <typeparam name="TReference">Type of the reference entity.</typeparam>
        /// <param name="entity">Giving entity.</param>
        /// <param name="referenceSelector">Indicates how to retrieve the reference entity.</param>
        public void Load<TEntity, TReference>(TEntity entity, Expression<Func<TEntity, EntityReference<TReference>>> referenceSelector)
            where TEntity : class
            where TReference : EntityObject
        {
            var reference = referenceSelector.Compile().Invoke(entity);
            if (!reference.IsLoaded)
            {
                reference.Load();
            }
        }

        /// <summary>
        /// Load the collection entity from the giving entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of giving entity.</typeparam>
        /// <typeparam name="TReference">Type of the collection entity.</typeparam>
        /// <param name="entity">Giving entity.</param>
        /// <param name="referenceSelector">Indicates how to retrieve the collection entity.</param>
        public void Load<TEntity, TCollection>(TEntity entity, Expression<Func<TEntity, EntityCollection<TCollection>>> referenceSelector)
            where TEntity : class
            where TCollection : EntityObject
        {
            var reference = referenceSelector.Compile().Invoke(entity);
            if (!reference.IsLoaded)
            {
                reference.Load();
            }
        }

        public T LoadByKey<T>(String propertyName, Object keyValue)
        {
            return Context.LoadByKey<T>(propertyName, keyValue);
        }
        #endregion
    }
}
