// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.ComponentModel;
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    [ToolboxItem(false)]
    public class TabListBox : ListBox
    {
        /// <summary>
        /// For public use only.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        /// <summary>
        /// For public use only.
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected override bool IsInputChar(char charCode)
        {
            return true;
        }
    }
}
