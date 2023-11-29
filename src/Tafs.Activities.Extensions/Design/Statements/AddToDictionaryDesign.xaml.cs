//
//  AddToDictionaryDesign.xaml.cs
//
//  Author:
//       Devin Duanne <dduanne@tafs.com>
//
//  Copyright (c) TAFS, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Windows;
using Tafs.Activities.Extensions.Statements;
using DependencyProp = System.Windows.DependencyProperty;

namespace Tafs.Activities.Extensions.Design.Statements
{
    /// <summary>
    /// Interaction logic for AddToDictionaryDesign.xaml.
    /// </summary>
    public partial class AddToDictionaryDesign
    {
        /// <summary>
        /// Gets the binding property for the dictionary type.
        /// </summary>
        public static readonly DependencyProp DictionaryTypeProperty = DependencyProp.Register(nameof(DictionaryType), typeof(Type), typeof(AddToDictionaryDesign), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the binding property for the key type.
        /// </summary>
        public static readonly DependencyProp KeyTypeProperty = DependencyProp.Register(nameof(KeyType), typeof(Type), typeof(AddToDictionaryDesign), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the binding property for the value type.
        /// </summary>
        public static readonly DependencyProp ValueTypeProperty = DependencyProp.Register(nameof(ValueType), typeof(Type), typeof(AddToDictionaryDesign), new UIPropertyMetadata(null));

        private Type DictionaryType
        {
            get => (Type)GetValue(DictionaryTypeProperty);
            set => SetValue(DictionaryTypeProperty, value);
        }

        private Type KeyType
        {
            get => (Type)GetValue(KeyTypeProperty);
            set => SetValue(KeyTypeProperty, value);
        }

        private Type ValueType
        {
            get => (Type)GetValue(ValueTypeProperty);
            set => SetValue(ValueTypeProperty, value);
        }

        /// <inheritdoc/>
        protected override void OnModelItemChanged(object newItem)
        {
            base.OnModelItemChanged(newItem);
            Type[] genericArguments = ModelItem.ItemType.GetGenericArguments();
            KeyType = genericArguments[0];
            ValueType = genericArguments[1];
            DictionaryType = typeof(Dictionary<,>).MakeGenericType(genericArguments);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToDictionaryDesign"/> class.
        /// </summary>
        public AddToDictionaryDesign()
        {
            InitializeComponent();
        }
    }
}
