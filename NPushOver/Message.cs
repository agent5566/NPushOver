﻿using System;

namespace NPushOver
{
    public class Message
    {
        public Priority Priority { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public SupplementaryURL SupplementaryUrl { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Sound { get; set; }

        public RetryOptions RetryOptions { get; set; }
        
        //public Message SetPriority(Priority priority)
        //{
        //    this.Priority = priority;
        //    return this;
        //}

        //public Message SetTitle(string title)
        //{
        //    this.Title = title;
        //    return this;
        //}

        //public Message SetBody(string body)
        //{
        //    this.Body = body;
        //    return this;
        //}

        //public Message SetSupplementaryURL(SupplementaryURL url)
        //{
        //    this.SupplementaryURL = url;
        //    return this;
        //}

        //public Message SetSupplementaryURL(Uri uri)
        //{
        //    return this.SetSupplementaryURL(uri, null);
        //}

        //public Message SetSupplementaryURL(Uri uri, string title)
        //{
        //    this.SupplementaryURL = new SupplementaryURL { Uri = uri, Title = title };
        //    return this;
        //}

        //public Message SetTimestamp(DateTime timestamp)
        //{
        //    this.Timestamp = timestamp;
        //    return this;
        //}

        //public Message SetSound(Sound sound)
        //{
        //    if (sound == NPushOver.Sound.None)
        //        this.Sound = null;
        //    else
        //        this.Sound = Enum.GetName(typeof(Sound), sound).ToLowerInvariant();
        //    return this;
        //}
    }
}
