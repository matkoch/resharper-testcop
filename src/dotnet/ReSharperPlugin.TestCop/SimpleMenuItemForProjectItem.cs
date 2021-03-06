﻿// --
// -- TestCop http://github.com/testcop
// -- License http://github.com/testcop/license
// -- Copyright 2015
// --
using System;
using JetBrains.Annotations;
using JetBrains.Application.UI.Controls.JetPopupMenu;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.Icons;
using JetBrains.UI.RichText;

namespace TestCop.Plugin
{
    public class SimpleMenuItemForProjectItem : SimpleMenuItem
    {
        public IProjectItem AssociatedProjectItem { get; private set; }
        public IDeclaredElement DeclaredElement { get; private set; }

        public SimpleMenuItemForProjectItem([NotNull] RichText text, [CanBeNull] IconId icon,
            [CanBeNull] Action FOnExecute
            , IProjectItem associatedProjectItem, IDeclaredElement declaredElement
            )
            : base(text, icon, FOnExecute)
        {
            AssociatedProjectItem = associatedProjectItem;
            DeclaredElement = declaredElement;
        }
    }
}
