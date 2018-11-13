using System;

namespace CWiz.DomainDrivenDesign
{
    public class Entity<TEntity, TKey> : IEquatable<TEntity>
        where TEntity : Entity<TEntity, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Key { get; }

        public Entity(TKey key) => Key = key == null ? throw new ArgumentNullException() : key;

        public bool Equals(TEntity other) => other != null && Key.Equals(other.Key);

        public sealed override bool Equals(object obj) => Equals(obj as TEntity);

        public sealed override int GetHashCode() => Key.GetHashCode();

        public static bool operator ==(Entity<TEntity, TKey> a, Entity<TEntity, TKey> b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);

            if (ReferenceEquals(b, null))
                return false;

            return a.Key.Equals(b.Key);
        }

        public static bool operator !=(Entity<TEntity, TKey> a, Entity<TEntity, TKey> b)
        {
            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            if (ReferenceEquals(b, null))
                return true;

            return !a.Key.Equals(b.Key);
        }

    }
}