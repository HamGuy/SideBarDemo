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

namespace SideBarDemo.Models
{

    public class Group<T> : IEnumerable<T>
    {
        public Group(string name, IEnumerable<T> items)
        {
            this.Title = name;
            this.Items = new List<T>(items);
        }

        public override bool Equals(object obj)
        {
            Group<T> that = obj as Group<T>;

            return (that != null) && (this.Title.Equals(that.Title));
        }

        public string Title
        {
            get;
            set;
        }

        public IList<T> Items
        {
            get;
            set;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion
    }
}
