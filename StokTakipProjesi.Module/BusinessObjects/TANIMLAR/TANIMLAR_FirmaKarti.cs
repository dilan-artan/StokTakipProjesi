using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.SISTEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static StokTakipProjesi.Module.BusinessObjects.HELPERS.helpers;

namespace StokTakipProjesi.Module.BusinessObjects.TANIMLAR
{
    [DefaultClassOptions]
    [NavigationItem("TANIMLAR")]
    
    public class TANIMLAR_FirmaKarti : SISTEM_KartTanimlari
    {
        public TANIMLAR_FirmaKarti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        private string _vergiDairesi;
        public string VergiDairesi
        {
            get
            {
                return _vergiDairesi;
            }
            set
            {
                SetPropertyValue(nameof(VergiDairesi), ref _vergiDairesi, value);
            }
        }

        private string _vergiNumarasi;
        public string VergiNumarasi
        {
            get
            {
                return _vergiNumarasi;
            }
            set
            {
                SetPropertyValue(nameof(VergiNumarasi), ref _vergiNumarasi, value);
            }
        }

        private Tur _turu;
        public Tur Turu
        {
            get { return _turu; }
            set { SetPropertyValue(nameof(Turu), ref _turu, value); }
        }

    }
}