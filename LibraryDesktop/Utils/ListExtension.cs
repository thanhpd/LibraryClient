﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDesktop.Models;

namespace LibraryDesktop.Utils
{
    static class ListExtension
    {
        public static T PopAt<T>(this List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }

        public static int count = 0;
        public static List<BookModel> BackupBookModels = new List<BookModel>();
    }
}
