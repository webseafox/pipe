using System;
using System.Collections.Generic;
using System.Linq;

namespace MiraeDigital.BffMobile.Domain.SeedWork
{
    public abstract class ValueObject
    {
        public List<string> Errors { get; } = new List<string>();
        public void AddErrors(IList<string> errors) => Errors.AddRange(errors);
        public void AddError(string error) => Errors.Add(error);
        public bool IsValid() => Errors.Count == 0;
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left is not null && left.Equals(right);
        }
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}
