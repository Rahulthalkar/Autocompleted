﻿using System.Runtime.Serialization;

namespace ANM.Domain
{
    public class APIResult<T>
    {
        public APIResult() { }
        [DataMember]
        public T? Value { get; set; }

        /// <summary>
        /// This is what helps us decide whether the business logic executed successfully or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// This value needs to be translated, hence they need to be present in the resource files.
        /// </summary>
        public string ErrorMessageKey { get; set; } = string.Empty;

        /// <summary>
        /// Serialized Exception Information.
        /// </summary>
        public string ExceptionInfo { get; set; } = string.Empty;


    }

}
