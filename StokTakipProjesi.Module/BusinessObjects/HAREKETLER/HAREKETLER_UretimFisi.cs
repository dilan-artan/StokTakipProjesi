using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.HELPERS;
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
            //eskiDepoID = null;
            //eskiGirenUrun = null;
            //eskiCikanUrun = null;
            //eskiGirenMiktar = GirenMiktar;
            //eskiCikanMiktar = CikanMiktar;

        }

        private TANIMLAR_DepoKarti _depoID;
        private TANIMLAR_StokKarti _girenUrun;
        private TANIMLAR_StokKarti _cikanUrun;
        private decimal _girenMiktar;
        private decimal _cikanMiktar;


        private TANIMLAR_DepoKarti eskiDepoID;
        private TANIMLAR_StokKarti eskiGirenUrun;
        private TANIMLAR_StokKarti eskiCikanUrun;
        private decimal eskiGirenMiktar;
        private decimal eskiCikanMiktar;

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

        protected override void OnLoaded()
        {
            base.OnLoaded();
            eskiDepoID = DepoID;
            eskiGirenUrun = GirenUrun;
            eskiCikanUrun = CikanUrun;
            eskiGirenMiktar = GirenMiktar;
            eskiCikanMiktar = CikanMiktar;
           

        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (eskiDepoID != null && eskiGirenUrun != null && eskiCikanUrun != null)
            {
                UTILS_Stok.UretimFisiYaz(eskiDepoID, eskiGirenUrun, eskiCikanUrun, eskiCikanMiktar*-1, eskiGirenMiktar * -1, Session);
            }

            if (GirenUrun != null && CikanUrun != null && DepoID != null)
            {
                UTILS_Stok.UretimFisiYaz(DepoID, GirenUrun, CikanUrun, GirenMiktar, CikanMiktar, Session);
            }

        }

        protected override void OnDeleting()
        {
            base.OnDeleting();

            if (eskiDepoID != null && eskiGirenUrun != null && eskiCikanUrun != null)
            {
                UTILS_Stok.UretimFisiYaz(eskiDepoID, eskiGirenUrun, eskiCikanUrun, eskiCikanMiktar * -1, eskiGirenMiktar * -1, Session);
            }
        }
        /*  protected override void OnChanged(string propertyName, object oldValue, object newValue)
          {
              base.OnChanged(propertyName, oldValue, newValue);

              if ((propertyName == "GirenMiktar" || propertyName == "CikanMiktar" || propertyName == "StokID")
                      && (GirenUrun != null && CikanUrun != null))
              {
                  var girenUrun = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == this.DepoID && i.StokID == this.GirenUrun).FirstOrDefault();

                  var cikanUrun = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == this.DepoID && i.StokID == this.CikanUrun).FirstOrDefault();

                  //UTILS_Stok.DepoStokYaz(eskiGirenDepoID, eskiCikanDepoID, eskiStokID, eskiMiktar * -1, Session);

                  if (GirenUrun != null && CikanUrun != null)
                  {
                      cikanUrun.Miktar += CikanMiktar;
                      girenUrun.Miktar -= GirenMiktar;

                      //girenUrun.Miktar += (decimal)oldValue - GirenMiktar;
                      //cikanUrun.Miktar += CikanMiktar - (decimal)oldValue;

                  }
                  if (girenUrun == null && cikanUrun != null)
                  {
                      var stokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
                      {
                          DepoID = DepoID,
                          StokID = GirenUrun,
                          Miktar = 0- GirenMiktar
                      };

                      girenUrun.Session.Save(stokHareketi);
                      cikanUrun.Miktar -= CikanMiktar;
                  }

                  if (cikanUrun == null && girenUrun != null)
                  {
                      var yeniStokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
                      {
                          DepoID = this.DepoID,
                          StokID = CikanUrun,
                          Miktar = CikanMiktar
                      };

                      cikanUrun.Session.Save(yeniStokHareketi);
                  }
              }

          }*/


    }
}
