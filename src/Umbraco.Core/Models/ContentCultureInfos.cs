﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Umbraco.Core.Exceptions;
using Umbraco.Core.Models.Entities;

namespace Umbraco.Core.Models
{
    /// <summary>
    /// The name of a content variant for a given culture
    /// </summary>
    public class ContentCultureInfos : BeingDirtyBase, IDeepCloneable, IEquatable<ContentCultureInfos>
    {
        private DateTime _date;
        private string _name;
        private static readonly Lazy<PropertySelectors> Ps = new Lazy<PropertySelectors>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentCultureInfos"/> class.
        /// </summary>
        public ContentCultureInfos(string culture)
        {
            if (culture.IsNullOrWhiteSpace()) throw new ArgumentNullOrEmptyException(nameof(culture));
            Culture = culture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentCultureInfos"/> class.
        /// </summary>
        /// <remarks>Used for cloning, without change tracking.</remarks>
        private ContentCultureInfos(string culture, string name, DateTime date)
            : this(culture)
        {
            _name = name;
            _date = date;
        }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        public string Culture { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetPropertyValueAndDetectChanges(value, ref _name, Ps.Value.NameSelector);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set => SetPropertyValueAndDetectChanges(value, ref _date, Ps.Value.DateSelector);
        }

        /// <inheritdoc />
        public object DeepClone()
        {
            return new ContentCultureInfos(Culture, Name, Date);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ContentCultureInfos other && Equals(other);
        }

        /// <inheritdoc />
        public bool Equals(ContentCultureInfos other)
        {
            return other != null && Culture == other.Culture && Name == other.Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = 479558943;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Culture);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        /// <summary>
        /// Deconstructs into culture and name.
        /// </summary>
        public void Deconstruct(out string culture, out string name)
        {
            culture = Culture;
            name = Name;
        }

        /// <summary>
        /// Deconstructs into culture, name and date.
        /// </summary>
        public void Deconstruct(out string culture, out string name, out DateTime date)
        {
            Deconstruct(out culture, out name);
            date = Date;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class PropertySelectors
        {
            public readonly PropertyInfo NameSelector = ExpressionHelper.GetPropertyInfo<ContentCultureInfos, string>(x => x.Name);
            public readonly PropertyInfo DateSelector = ExpressionHelper.GetPropertyInfo<ContentCultureInfos, DateTime>(x => x.Date);
        }
    }
}
