//------------------------------------------------------------------------------
// <copyright file="ResizeError.cs" company="Brice Lambson">
//     Copyright (c) 2011-2013 Brice Lambson. All rights reserved.
//
//     The use of this software is governed by the Microsoft Public License
//     which is included with this distribution.
// </copyright>
//------------------------------------------------------------------------------

namespace BriceLambson.ImageResizer.Models
{
    using System;
    using System.Diagnostics;
    using System.IO;

    internal class ResizeError
    {
        private readonly string _filename;
        private readonly string _message;

        public ResizeError(string path, Exception ex)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(path), "path is null or empty.");
            Debug.Assert(ex != null, "ex is null.");

            _filename = Path.GetFileName(path);
            _message = ex.Message;
        }

        public string Filename
        {
            get { return _filename; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}