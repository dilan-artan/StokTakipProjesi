using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StokTakipProjesi.Module.BusinessObjects.SISTEM
{
    [DefaultClassOptions]
    [NavigationItem("SİSTEM")]
    [DefaultProperty("Adi")]

    public class SISTEM_KartTanimlari : BaseObject
    {
        public SISTEM_KartTanimlari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private string _kodu;
        public string Kodu
        {
            get
            {
                return _kodu;
            }
            set
            {
                SetPropertyValue(nameof(Kodu), ref _kodu, value);
            }
        }

        private string _adi;
        public string Adi
        {
            get
            {
                return _adi;
            }
            set
            {
                SetPropertyValue(nameof(Adi), ref _adi, value);
            }
        }
    }
}