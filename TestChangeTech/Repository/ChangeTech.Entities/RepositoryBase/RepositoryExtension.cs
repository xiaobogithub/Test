using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects.DataClasses;

namespace ChangeTech.Entities
{
    public static class RepositoryExtension
    {
        public static KeyValuePair<int, string> CreateKeyValuePair<TEntity>(this TEntity entity, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, string>> valueSelector) where TEntity : EntityObject
        {
            return CreateKeyValuePair<TEntity, int, string>(entity, keySelector, valueSelector);
        }

        public static KeyValuePair<TKey, TValue> CreateKeyValuePair<TEntity, TKey, TValue>(this TEntity entity, Expression<Func<TEntity, TKey>> ketSelector, Expression<Func<TEntity, TValue>> valueSelector) where TEntity : EntityObject
        {
            return new KeyValuePair<TKey, TValue>(ketSelector.Compile().Invoke(entity), valueSelector.Compile().Invoke(entity));
        }
    }
}
