/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Depressurizer
{
    [TypeDescriptionProvider(typeof(InstantiableClassProvider<AutoCatConfigPanel, UserControl>))]
    public class AutoCatConfigPanel : UserControl
    {
        public virtual void SaveToAutoCat(AutoCat ac) { }

        public virtual void LoadFromAutoCat(AutoCat ac) { }

        public static AutoCatConfigPanel CreatePanel(AutoCat ac, GameList ownedGames, List<AutoCat> autocats)
        {
            AutoCatType t = ac.AutoCatType;
            switch (t)
            {
                case AutoCatType.Genre:
                    return new AutoCatConfigPanel_Genre();
                case AutoCatType.Flags:
                    return new AutoCatConfigPanel_Flags();
                case AutoCatType.Tags:
                    return new AutoCatConfigPanel_Tags(ownedGames);
                case AutoCatType.Year:
                    return new AutoCatConfigPanel_Year();
                case AutoCatType.UserScore:
                    return new AutoCatConfigPanel_UserScore();
                case AutoCatType.Hltb:
                    return new AutoCatConfigPanel_Hltb();
                case AutoCatType.Manual:
                    return new AutoCatConfigPanel_Manual(ownedGames);
                case AutoCatType.DevPub:
                    return new AutoCatConfigPanel_DevPub(ownedGames);
                case AutoCatType.Group:
                    return new AutoCatConfigPanel_Group(autocats);
                case AutoCatType.Name:
                    return new AutoCatConfigPanel_Name();
                case AutoCatType.VrSupport:
                    return new AutoCatConfigPanel_VrSupport();
                case AutoCatType.Language:
                    return new AutoCatConfigPanel_Language();
                default:
                    return null;
            }
        }
    }

    internal class InstantiableClassProvider<TAbstract, TInstantiable> : TypeDescriptionProvider
    {
        public InstantiableClassProvider() : base(TypeDescriptor.GetProvider(typeof(TAbstract))) { }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
            {
                return typeof(TInstantiable);
            }
            return base.GetReflectionType(objectType, instance);
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes,
            object[] args)
        {
            if (objectType == typeof(TAbstract))
            {
                objectType = typeof(TInstantiable);
            }
            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}