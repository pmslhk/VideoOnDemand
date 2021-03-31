using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VOD.Common.Extensions
{
   public static class ListExtensions
    {
      public static SelectList ToSelectList<TEntity>(this List<TEntity>
                 items, string valueField, string textField) where TEntity : class
      {
          return new SelectList(items, valueField, textField);
      }
    }
}
