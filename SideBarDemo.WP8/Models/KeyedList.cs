//*****************************************************************//
//  Copyright (C) HamGuy 2013 All rights reserved.
//
//  The information contained herein is confidential, proprietary
//  to HamGuy. Use of this information by anyone other than 
//  authorized employees of HamGuy is granted only under a 
//  written non-disclosure agreement, expressly prescribing the 
//  scope and manner of such use.
//
//****************************************************************//
//  Group Create by HamGuy at 2013/4/9 21:28:02
//  Version 1.0
//  wangrui15@gmail.com
//  http://www.hamguy.info
//****************************************************************//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideBarDemo.WP8.Models
{

    public class KeyedList<TKey, TItem> : List<TItem>
    {
        public TKey Key { protected set; get; }

        public KeyedList(TKey key, IEnumerable<TItem> items)
            : base(items)
        {
            Key = key;
        }

        public KeyedList(IGrouping<TKey, TItem> grouping)
            : base(grouping)
        {
            Key = grouping.Key;
        }
    }
}
