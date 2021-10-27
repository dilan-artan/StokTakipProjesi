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
    public class HAREKETLER_UretimFisi : BaseObject
    { 
        public HAREKETLER_UretimFisi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
        }

        private TANIMLAR_DepoKarti _depoID;
        private TANIMLAR_StokKarti _girenUrun;
        private TANIMLAR_StokKarti _cikanUrun;
        private decimal _girenMiktar;
        private decimal _cikanMiktar;
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

        public TANIMLAR_StokKarti GirenUrun
        {
            get
            {
                return _girenUrun;
            }
            set
            {
                SetPropertyValue(nameof(GirenUrun), ref _girenUrun, value);
            }
        }

        public TANIMLAR_StokKarti CikanUrun
        {
            get
            {
                return _cikanUrun;
            }
            set
            {
                SetPropertyValue(nameof(CikanUrun), ref _cikanUrun, value);
            }
        }

        public decimal GirenMiktar
        {
            get
            {
                return _girenMiktar;
            }
            set
            {
                SetPropertyValue(nameof(GirenMiktar), ref _girenMiktar, value);
            }
        }

        public decimal CikanMiktar
        {
            get
            {
                return _cikanMiktar;
            }
            set
            {
                SetPropertyValue(nameof(CikanMiktar), ref _cikanMiktar, value);
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if(propertyName == "GirenMiktar" && propertyName == "CikanMiktar" && GirenUrun != null && CikanUrun != null)
            {
                var girenUrun = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == this.DepoID && i.StokID == this.GirenUrun).FirstOrDefault();

                var cikanUrun = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == this.DepoID && i.StokID == this.CikanUrun).FirstOrDefault();

                if(GirenUrun != null && CikanUrun != null)
                {
                   // girenUrun.Miktar =
                }



            }
        }

    }
}