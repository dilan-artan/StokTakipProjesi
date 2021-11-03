using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    [Appearance("Kırmızı", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Miktar=0", Context = "ListView", BackColor = "red", FontColor = "black", Priority = 2)]
    [Appearance("sarı", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Miktar<50", Context = "ListView", BackColor = "yellow", FontColor = "black", Priority = 2)]
    [Appearance("Yeşil", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Miktar>5", Context = "ListView", BackColor = "green", FontColor = "black", Priority = 2)]

    public class HAREKETLER_Depo_StokMiktari : BaseObject
    { 
        public HAREKETLER_Depo_StokMiktari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

       
        private TANIMLAR_DepoKarti _depoID;
      //  [Association("Depo-DepoStok")]
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

        private TANIMLAR_StokKarti _stokID;
        //[Association("Stok-DepoStok")]
        public TANIMLAR_StokKarti StokID
        {
            get
            {
                return _stokID;
            }
            set
            {
                SetPropertyValue(nameof(StokID), ref _stokID, value);
            }
        }

        

        private decimal _miktar;
        public decimal Miktar
        {
            get
            {
                return _miktar;
            }
            set
            {
                SetPropertyValue(nameof(Miktar), ref _miktar, value);
            }
        }


    }
}