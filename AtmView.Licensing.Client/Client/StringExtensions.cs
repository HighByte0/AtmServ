﻿//
// Copyright (c) 2016 Repetti Adriano.
//
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AtmView.Licensing.Client
{
    static class StringExtensions
    {
        public static KeyValuePair<string, string> SplitKeyValuePair(this string source, char separator)
        {
            if (String.IsNullOrEmpty(source))
                return new KeyValuePair<string, string>();

            string[] parts = source.Split(new char[] { separator }, 2);

            if (parts.Length == 2)
                return new KeyValuePair<string, string>(parts[0].Trim(), parts[1].Trim());

            return new KeyValuePair<string, string>(parts[0].Trim(), "");
        }

        public static StringBuilder RemoveNewLinesAndTabs(this StringBuilder sb)
        {
            return sb.Replace("\r", "").Replace("\n", "").Replace("\t", " ");
        }
    }
}
