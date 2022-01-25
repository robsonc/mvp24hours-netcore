//=====================================================================================
// Developed by Kallebe Lins (kallebe.santos@outlook.com)
// Teacher, Architect, Consultant and Project Leader
// Virtual Card: https://www.linkedin.com/in/kallebelins
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Core.Contract.Infrastructure.Contexts;
using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.ValueObjects.Infrastructure;
using Mvp24Hours.Core.ValueObjects.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mvp24Hours.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class BusinessExtensions
    {
        /// <summary>
        /// Transform a message into a business object
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this IPipelineMessage message, string key = null, string tokenDefault = null)
        {
            if (message != null)
            {
                return new BusinessResult<T>(
                    token: message.Token ?? tokenDefault,
                    data: key.HasValue() ? message.GetContent<T>(key) : message.GetContent<T>(),
                    messages: new ReadOnlyCollection<IMessageResult>(message.Messages ?? new List<IMessageResult>())
                );
            }
            return new BusinessResult<T>(token: tokenDefault);
        }

        /// <summary>
        /// Encapsulates notifications for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this IReadOnlyCollection<Notification> notifications, IMessageResult defaultMessage = null, string tokenDefault = null)
        {
            var messages = new List<IMessageResult>();
            if (notifications.AnyOrNotNull())
            {
                foreach (var item in notifications)
                {
                    messages.Add(new MessageResult(item.Key, item.Message, item.Type));
                }
            }
            else if (defaultMessage != null)
            {
                messages.Add(defaultMessage);
            }

            return new BusinessResult<T>(
                token: tokenDefault,
                data: default,
                messages: new ReadOnlyCollection<IMessageResult>(messages)
            );
        }

        /// <summary>
        /// Encapsulates notifications for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this INotificationContext notificationContext, IMessageResult defaultMessage = null, string tokenDefault = null)
        {
            var messages = new List<IMessageResult>();
            if (notificationContext != null && notificationContext.HasNotifications)
            {
                foreach (var item in notificationContext.Notifications)
                {
                    messages.Add(new MessageResult(item.Key, item.Message, item.Type));
                }
            }
            else if (defaultMessage != null)
            {
                messages.Add(defaultMessage);
            }
            return new BusinessResult<T>(
                token: tokenDefault,
                data: default,
                messages: new ReadOnlyCollection<IMessageResult>(messages)
            );
        }

        /// <summary>
        /// Encapsulates object for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this IMessageResult messageResult, string tokenDefault = null)
        {
            if (messageResult != null)
            {
                return ToBusiness<T>(default, new List<IMessageResult> { messageResult }, tokenDefault);
            }
            return new BusinessResult<T>(token: tokenDefault);
        }

        /// <summary>
        /// Encapsulates object for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this IList<IMessageResult> messageResult, string tokenDefault = null)
        {
            if (messageResult != null)
            {
                return ToBusiness<T>(default, messageResult, tokenDefault);
            }
            return new BusinessResult<T>(token: tokenDefault);
        }

        /// <summary>
        /// Encapsulates object for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this T value, IMessageResult messageResult, string tokenDefault = null)
        {
            return new BusinessResult<T>(
                token: tokenDefault,
                data: value,
                messages: new ReadOnlyCollection<IMessageResult>(new List<IMessageResult>() { messageResult })
            );
        }

        /// <summary>
        /// Encapsulates object for business
        /// </summary>
        public static IBusinessResult<T> ToBusiness<T>(this T value, IList<IMessageResult> messageResult = null, string tokenDefault = null)
        {
            return new BusinessResult<T>(
                token: tokenDefault,
                data: value,
                messages: new ReadOnlyCollection<IMessageResult>(messageResult?.ToList() ?? new List<IMessageResult>())
            );
        }

        public static bool HasData<T>(this IBusinessResult<T> value)
        {
            if (value == null || value.Data == null)
            {
                return false;
            }

            if (value.Data.IsList<T>())
            {
                return ((IEnumerable<T>)value.Data).AnyOrNotNull();
            }

            return true;
        }

        public static bool HasMessageKey<T>(this IBusinessResult<T> value, string key)
        {
            if (value == null || value.Messages == null)
            {
                return false;
            }

            return value.Messages.Any(x => x.Key.HasValue() && x.Key.Equals(key, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public static T GetDataValue<T>(this IBusinessResult<T> value)
        {
            if (value.HasData())
            {
                return value.Data;
            }
            return default;
        }

        public static int GetDataCount<T>(this IBusinessResult<T> value)
        {
            if (value.HasData())
            {
                if (value.Data.IsList<T>())
                {
                    return ((IEnumerable<T>)value.Data).Count();
                }
                else
                {
                    return 1;
                }
            }
            return 0;
        }

        public static bool HasDataCount<T>(this IBusinessResult<T> value, int count)
        {
            if (value.HasData())
            {
                if (value.Data.IsList<T>())
                {
                    return ((IEnumerable<T>)value.Data).Count() == count;
                }
            }
            return false;
        }

        public static object GetDataFirstOrDefault<T>(this IBusinessResult<T> value)
        {
            if (value.HasData())
            {
                if (value.Data.IsList<T>())
                {
                    return ((IEnumerable<T>)value.Data).FirstOrDefault();
                }
                else
                {
                    return value.Data;
                }
            }
            return null;
        }
    }
}