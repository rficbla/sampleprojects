using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NHibernateSampleApplication.Domain
{
    //Design of Domain Entity class is based on Sharp Architecture: http://code.google.com/p/sharp-architecture/
    public class DomainEntity<T>
        where T : DomainEntity<T>
    {
        private const int RandomPrimeNumber = 397;
        private int? _hashCode;

        public virtual Guid Id { get; set; }

        public override bool Equals(object other)
        {
            var objectToCompareWith = other as T;
            return Equals(objectToCompareWith);
        }

        public virtual bool Equals(T objectToCompareWith)
        {
            if (ReferenceEquals(this, objectToCompareWith))
            {
                return true;
            }

            if (objectToCompareWith == null)
            {
                return false;
            }

            // this is applicable for nHibernate only to handle nHibernate proxy objects
            if (!GetType().IsAssignableFrom(objectToCompareWith.GetObjectType()))
            {
                return false;
            }

            return IsTransient() && objectToCompareWith.IsTransient()
                       ? CompareNaturalKeys(objectToCompareWith)
                       : objectToCompareWith.Id.Equals(Id);
        }

        // returns the real type to handle nHibernate proxy objects
        public virtual Type GetObjectType()
        {
            return GetType();
        }

        public override int GetHashCode()
        {
            // use the _hashcode if it has already been calculated
            if (_hashCode.HasValue)
            {
                return _hashCode.Value;
            }

            // if the object is transient then calculate hashcode based on natural keys if it exists 
            // or just get the hashcode of the base object
            if (IsTransient())
            {
                _hashCode = GetNaturalKeys().Any()
                                ? CalculateHashCodeBasedOnNaturalKeys(GetType().GetHashCode(), RandomPrimeNumber)
                                : base.GetHashCode();

                return _hashCode.Value;
            }
            // Objects are not transient, so calculate hashcode based on the Id
            _hashCode = (GetType().GetHashCode() * RandomPrimeNumber) ^ Id.GetHashCode();
            return _hashCode.Value;
        }

        private int CalculateHashCodeBasedOnNaturalKeys(int hashCode, int primeNumber)
        {
            foreach (PropertyInfo property in GetNaturalKeys())
            {
                object value = property.GetValue(this, null);
                if (value != null)
                {
                    hashCode = (hashCode * primeNumber) ^ value.GetHashCode();
                }
            }
            return hashCode;
        }

        private bool IsTransient()
        {
            return Id.Equals(Guid.Empty);
        }

        private bool CompareNaturalKeys(DomainEntity<T> model)
        {
            foreach (PropertyInfo property in GetNaturalKeys())
            {
                object x = property.GetValue(this, null);
                object y = property.GetValue(model, null);
                if ((x == null && y == null) || (x != null && x.Equals(y)))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        private IEnumerable<PropertyInfo> GetNaturalKeys()
        {
            if (!NaturalKeys.ContainsKey(GetType()))
            {
                NaturalKeys[GetType()] = this.GetPropertiesWithAttribute(typeof(NaturalKey));
            }
            return NaturalKeys[GetType()];
        }

        [ThreadStatic]
        private static IDictionary<Type, IEnumerable<PropertyInfo>> _naturalKeys;

        private static IDictionary<Type, IEnumerable<PropertyInfo>> NaturalKeys
        {
            get
            {
                if (_naturalKeys == null)
                {
                    _naturalKeys = new Dictionary<Type, IEnumerable<PropertyInfo>>();
                }
                return _naturalKeys;
            }
        }
    }
}