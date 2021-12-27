using System;
namespace Ordering.Domain.Common
{
    public class EntityBase
    {
        public int Id { get; protected set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string LastModefiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
