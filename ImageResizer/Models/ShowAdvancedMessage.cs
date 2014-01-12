//------------------------------------------------------------------------------
// <copyright file="ShowAdvancedEvent.cs" company="Brice Lambson">
//     Copyright (c) 2011-2013 Brice Lambson. All rights reserved.
//
//     The use of this software is governed by the Microsoft Public License
//     which is included with this distribution.
// </copyright>
//------------------------------------------------------------------------------

namespace BriceLambson.ImageResizer.Models
{
    using System.Diagnostics;
    using BriceLambson.ImageResizer.ViewModels;
    using GalaSoft.MvvmLight.Messaging;

    internal class ShowAdvancedMessage : GenericMessage<AdvancedViewModel>
    {
        public ShowAdvancedMessage(AdvancedViewModel content)
            : base(content)
        {
            Debug.Assert(content != null, "content is null.");
        }
    }
}