﻿using System;
using System.Runtime.Serialization;

namespace SalesforceSharp
{
    #region Enum
    /// <summary>
    /// The Salesforce REST API's error.
    /// http://www.salesforce.com/us/developer/docs/api/Content/sforce_api_calls_concepts_core_data_objects.htm
    /// </summary>
    public enum SalesforceError
    {
        /// <summary>
        /// Unknown error.
        /// </summary>
        Unknown,

        /// <summary>
        /// Invalid client.
        /// </summary>
        InvalidClient,

        /// <summary>
        /// Unsupported grant type
        /// </summary>
        UnsupportedGrantType,

        /// <summary>
        /// Invalid grant.
        /// </summary>
        InvalidGrant,

        /// <summary>
        /// Authentication failure.
        /// </summary>
        AuthenticationFailure,

        /// <summary>
        /// Invalid password.
        /// </summary>
        InvalidPassword,

        /// <summary>
        /// Client identifier invalid.
        /// </summary>
        ClientIdentifierInvalid,

        /// <summary>
        /// The resource or record was not found.
        /// </summary>
        NotFound,

        /// <summary>
        /// An invalid query string was specified. For example, the query string was longer than 20,000 characters.
        /// </summary>
        MalFormedQuery,

        /// <summary>
        /// Some field validation has throw an exception.
        /// </summary>
        FieldCustomValidationException,

        /// <summary>
        /// Some field used on update or insert was invalid.
        /// </summary>
        InvalidFieldForInsertUpdate,

        /// <summary>
        /// Client id used to authenticate is invalid.
        /// </summary>
        InvalidClientId,

        /// <summary>
        /// Invalid field.
        /// </summary>
        InvalidField,

        /// <summary>
        /// Required field missing.
        /// </summary>
        RequiredFieldMissing,

        /// <summary>
        /// String is too long.
        /// </summary>
        StringTooLong,

        /// <summary>
        /// You can't reference an object that has been deleted.
        /// </summary>
        EntityIsDeleted,

        /// <summary>
        /// An ID must be either 15 characters, or 18 characters with a valid case-insensitive extension.
        /// </summary>
        MalFormedId,

        /// <summary>
        /// An invalid operator was used in the query() filter clause, at least for that field.
        /// </summary>
        InvalidQueryFilterOperator
    }
    #endregion

    /// <summary>
    /// An exception generated by Salesforce REST API.
    /// </summary>
    [Serializable]
    public class SalesforceException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesforceException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="description">The description.</param>
        public SalesforceException(SalesforceError error, string description)
            : base(description)
        {
            Error = error;
            Fields = new string[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesforceException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="description">The description.</param>
        internal SalesforceException(string error, string description) : this(ParseError(error), description)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesforceException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="description">The description.</param>
        /// <param name="fields">The fields.</param>
        internal SalesforceException(string error, string description, string[] fields)
            : this(error, description)
        {
            Fields = fields;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public SalesforceError Error { get; private set; }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public string[] Fields { get; private set; }
        #endregion       

        #region Methods
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        ///</PermissionSet>        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Error", Error);
            info.AddValue("Fields", Fields);
        }

        /// <summary>
        /// Parses the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private static SalesforceError ParseError(string error)
        {
            SalesforceError value;

            if (Enum.TryParse<SalesforceError>(error.Replace("_", ""), true, out value))
            {
                return value;
            }
            else
            {
                return SalesforceError.Unknown;
            }            
        }
        #endregion        
    }
}
