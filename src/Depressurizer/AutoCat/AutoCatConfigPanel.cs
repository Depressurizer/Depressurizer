using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Depressurizer.Core.Enums;

namespace Depressurizer
{
    [TypeDescriptionProvider(typeof(InstantiableClassProvider<AutoCatConfigPanel, UserControl>))]
    public class AutoCatConfigPanel : UserControl
    {
        #region Public Methods and Operators

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
                case AutoCatType.Curator:
                    return new AutoCatConfigPanel_Curator();
                case AutoCatType.Platform:
                    return new AutoCatConfigPanel_Platform();
                default:
                    return null;
            }
        }

        public virtual void LoadFromAutoCat(AutoCat autoCat) { }

        public virtual void SaveToAutoCat(AutoCat autoCat) { }

        #endregion
    }

    internal class InstantiableClassProvider<TAbstract, TInstantiable> : TypeDescriptionProvider
    {
        #region Constructors and Destructors

        public InstantiableClassProvider() : base(TypeDescriptor.GetProvider(typeof(TAbstract))) { }

        #endregion

        #region Public Methods and Operators

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
            {
                objectType = typeof(TInstantiable);
            }

            return base.CreateInstance(provider, objectType, argTypes, args);
        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
            {
                return typeof(TInstantiable);
            }

            return base.GetReflectionType(objectType, instance);
        }

        #endregion
    }
}
