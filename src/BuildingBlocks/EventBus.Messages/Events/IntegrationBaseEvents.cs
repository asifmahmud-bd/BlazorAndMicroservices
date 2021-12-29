using System;
namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvents
    {
        public Guid Id { get; }
        public DateTime CreateDate { get; }

        public IntegrationBaseEvents()
        {
            Id = new Guid();
            CreateDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvents(Guid id, DateTime createDate) //: this()
        {
            this.Id = id;
            this.CreateDate = createDate;
        }
    }
}
