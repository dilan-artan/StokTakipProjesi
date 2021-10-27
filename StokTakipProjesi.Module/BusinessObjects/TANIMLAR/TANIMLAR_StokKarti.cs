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

    public class TANIMLAR_StokKarti : SISTEM_KartTanimlari
    {
        public TANIMLAR_StokKarti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }

        private decimal _kdv;
        public decimal KDV
        {
            get
            {
                return _kdv;
            }
            set
            {
                SetPropertyValue(nameof(KDV), ref _kdv, value);
            }
        }

        private decimal _birimFiyati;
        public decimal BirimFiyati
        {
            get
            {
                return _birimFiyati;
            }
            set
            {
                SetPropertyValue(nameof(BirimFiyati), ref _birimFiyati, value);
            }
        }

        private Boolean _satistaMi;
        public Boolean SatistaMi
        {
            get
            {
                return _satistaMi;
            }
            set
            {
                SetPropertyValue(nameof(SatistaMi), ref _satistaMi, value);
            }
        }

        //[Association("Stok-DepoStok")]
        //public XPCollection<HAREKETLER_Depo_StokMiktari> depoStogu
        //{
        //    get
        //    {
        //        return GetCollection<HAREKETLER_Depo_StokMiktari>(nameof(depoStogu));
        //    }
        //}


    }
}