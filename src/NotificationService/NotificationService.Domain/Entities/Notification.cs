using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entities
{

    public enum NotificationStatus
    {
        Success,
        Failure
    }
    public class Notification
    {

        public Guid Id { get; private set; }
        public string eventType { get; private set; } = null!;
        public string recipientEmail { get; private set; } = null!;
        public string subject { get; private set; } = null!;
        public NotificationStatus status { get; private set; }
        public string? errorMessages { get; private set; }
        public DateTime sentAt { get; private set; }


        private Notification() { }


        public static Notification Create(string EventType, string RecipientEmail, string Subject)
        {
            return new Notification
            {
                Id = Guid.NewGuid(),
                eventType = EventType,
                recipientEmail = RecipientEmail,
                subject = Subject,
                status = NotificationStatus.Success,
                sentAt = DateTime.UtcNow

            };
        }


        public static Notification CreateError(string EventType, string RecipientEmail, string Subject, string? ErrorMessages)
        {
            return new Notification
            {

                Id = Guid.NewGuid(),
                eventType = EventType,
                recipientEmail = RecipientEmail,
                subject = Subject,
                status=NotificationStatus.Failure,
                errorMessages = ErrorMessages,
                sentAt=DateTime.UtcNow
            };
        }


    }
}
