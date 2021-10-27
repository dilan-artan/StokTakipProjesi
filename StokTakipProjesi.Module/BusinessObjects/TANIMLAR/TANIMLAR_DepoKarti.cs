using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.HAREKETLER;
using StokTakipProjesi.Module.BusinessObjects.SISTEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StokTakipProjesi.Module.BusinessObjects.TANIMLAR
{
    [DefaultClassOptions]
    [NavigationItem("TANIMLAR")]

    public class TANIMLAR_DepoKarti : SISTEM_KartTanimlari
    { 
        public TANIMLAR_DepoKarti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        private string _adresi;
        public string Adresi
        {
            get
            {
                return _adresi;
            }
            set
            {
                SetPropertyValue(nameof(Address), ref _adresi, value);
            }
        }

        //[Association("Depo-DepoStok")]
        //public XPCollection<HAREKETLER_Depo_StokMiktari> depoStogu
        //{
        //    get
        //    {
        //        return GetCollection<HAREKETLER_Depo_StokMiktari>(nameof(depoStogu));
        //    }
        //}

    }
}