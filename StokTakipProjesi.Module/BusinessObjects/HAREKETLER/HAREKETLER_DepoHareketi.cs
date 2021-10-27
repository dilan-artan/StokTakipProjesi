using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.TANIMLAR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StokTakipProjesi.Module.BusinessObjects.HAREKETLER
{
    [DefaultClassOptions]
    [NavigationItem("HAREKETLER")]

    public class HAREKETLER_DepoHareketi : BaseObject
    { 
        public HAREKETLER_DepoHareketi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        private DateTime _faturaTarihi;
        public DateTime FaturaTarihi
        {
            get
            {
                return _faturaTarihi;
            }
            set
            {
                SetPropertyValue(nameof(FaturaTarihi), ref _faturaTarihi, value);
            }
        }

        private TANIMLAR_DepoKarti _depoID;
        public TANIMLAR_DepoKarti DepoID
        {
            get
            {
                return _depoID;
            }
            set
            {
                SetPropertyValue(nameof(DepoID), ref _depoID, value);
            }
        }

        [Association("Fatura-Detay")]
        public XPCollection<HAREKETLER_DepoHareket_Detayi> _hareketDetaylari
        {
            get
            {
                return GetCollection<HAREKETLER_DepoHareket_Detayi>(nameof(_hareketDetaylari));
            }
        }

     

    }
}