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
    [RuleCriteria("TransferMiktari", DefaultContexts.Save, "TransferMiktari > 0", "Miktar 0 dan büyük olmalı", SkipNullOrEmptyValues = false)]
    
    public class HAREKETLER_DepoTransfer : BaseObject
    {
        public HAREKETLER_DepoTransfer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            eskiGirenDepoID = null;
            eskiCikanDepoID = null;
            eskiStokID = null;
            eskiMiktar = TransferMiktari;
        }
        private TANIMLAR_DepoKarti eskiGirenDepoID;
        private TANIMLAR_DepoKarti eskiCikanDepoID;
        private TANIMLAR_StokKarti eskiStokID;
        private decimal eskiMiktar;

        private TANIMLAR_StokKarti _stokID;
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

        private TANIMLAR_DepoKarti _ciktigiDepoID;
        public TANIMLAR_DepoKarti CiktigiDepoID
        {
            get
            {
                return _ciktigiDepoID;
            }
            set
            {
                SetPropertyValue(nameof(CiktigiDepoID), ref _ciktigiDepoID, value);
            }
        }

        private TANIMLAR_DepoKarti _geldigiDepoID;
        public TANIMLAR_DepoKarti GeldigiDepoID
        {
            get
            {
                return _geldigiDepoID;
            }
            set
            {
                SetPropertyValue(nameof(GeldigiDepoID), ref _geldigiDepoID, value);
            }
        }



        private decimal _transferMiktari;

        public decimal TransferMiktari
        {
            get
            {
                return _transferMiktari;
            }
            set
            {
                SetPropertyValue(nameof(TransferMiktari), ref _transferMiktari, value);
            }
        }

        protected override void OnLoaded()
        {
            eskiGirenDepoID = GeldigiDepoID;
            eskiCikanDepoID = CiktigiDepoID;
            eskiStokID = StokID;
            eskiMiktar = TransferMiktari;
            base.OnLoaded();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
             // asdasd

            if (eskiStokID != null && eskiGirenDepoID != null && eskiCikanDepoID != null)
            {
                UTILS_Stok.DepoStokYaz(eskiGirenDepoID, eskiCikanDepoID, eskiStokID, eskiMiktar*-1, Session);
            }

            if (StokID != null && GeldigiDepoID != null && CiktigiDepoID != null)
            {
                UTILS_Stok.DepoStokYaz(GeldigiDepoID, CiktigiDepoID, StokID, TransferMiktari, Session);
            }
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();

            if (eskiGirenDepoID != null && eskiCikanDepoID != null && eskiStokID != null)
            {
                UTILS_Stok.DepoStokYaz(eskiGirenDepoID, eskiCikanDepoID, eskiStokID, -1 * eskiMiktar, Session);
            }
        }
  


        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{

        //    base.OnChanged(propertyName, oldValue, newValue);

        //    //if (propertyName == "TransferMiktari" && StokID != null && GeldigiDepoID != null && CiktigiDepoID != null)
        //    //{
        //    //    if (oldValue != null) { }


        //    //        UTILS_Stok eskiVeri = new UTILS_Stok();
        //    //        eskiVeri.DepoStokYaz(eskiGirenDepoID,eskiCikanDepoID, eskiStokID, eskiMiktar);
        //    //    }
        //    //    else
        //    //    {
        //    //        UTILS_Stok veri = new UTILS_Stok();
        //    //        veri.DepoStokYaz(GeldigiDepoID, CiktigiDepoID, StokID, TransferMiktari);
        //    //    }

        //    //}


        //    /* Eski Kodlarım
        //     if (propertyName == "TransferMiktari" && StokID != null && GeldigiDepoID != null && CiktigiDepoID != null)
        //     {
        //         var girenDepoStogu = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //        i.DepoID == this.GeldigiDepoID).FirstOrDefault();

        //         var cikanDepoStogu = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //         i.DepoID == this.CiktigiDepoID).FirstOrDefault();

        //         if (girenDepoStogu != null && cikanDepoStogu != null)
        //         {
        //             girenDepoStogu.Miktar += TransferMiktari - (decimal)oldValue;
        //             cikanDepoStogu.Miktar += (decimal)oldValue - TransferMiktari;
        //         }

        //         if (girenDepoStogu == null && cikanDepoStogu != null)
        //         {
        //             var stokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
        //             {
        //                 DepoID = GeldigiDepoID,
        //                 StokID = this.StokID,
        //                 Miktar = TransferMiktari
        //             };

        //             Session.Save(stokHareketi);
        //             cikanDepoStogu.Miktar -= TransferMiktari;
        //         }

        //         if (cikanDepoStogu == null && girenDepoStogu != null)
        //         {
        //             var yeniStokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
        //             {
        //                 DepoID = CiktigiDepoID,
        //                 StokID = this.StokID,
        //                 Miktar = 0 - TransferMiktari
        //             };

        //             Session.Save(yeniStokHareketi);
        //             girenDepoStogu.Miktar += TransferMiktari;
        //         }

        //         if (propertyName == "StokID" && StokID != null && CiktigiDepoID != null && GeldigiDepoID != null)
        //         {
        //             var EskiGirenDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == oldValue &&
        //             i.DepoID == this.GeldigiDepoID).FirstOrDefault();

        //             var EskiCikanDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == oldValue &&
        //             i.DepoID == this.CiktigiDepoID).FirstOrDefault();

        //             var YeniGirenDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == newValue &&
        //             i.DepoID == this.GeldigiDepoID).FirstOrDefault();

        //             var yeniCikanDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == newValue &&
        //             i.DepoID == this.CiktigiDepoID).FirstOrDefault();

        //             if (girenDepoStogu != null && cikanDepoStogu != null && YeniGirenDepo != null && yeniCikanDepo != null)
        //             {
        //                 EskiGirenDepo.Miktar -= TransferMiktari;
        //                 EskiCikanDepo.Miktar += TransferMiktari;

        //                 YeniGirenDepo.Miktar += TransferMiktari;
        //                 yeniCikanDepo.Miktar -= TransferMiktari;
        //             }

        //             if (girenDepoStogu != null && cikanDepoStogu != null && YeniGirenDepo == null && yeniCikanDepo != null)
        //             {

        //                 var stokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
        //                 {
        //                     DepoID = GeldigiDepoID,
        //                     StokID = this.StokID,
        //                     Miktar = TransferMiktari
        //                 };

        //                 Session.Save(stokHareketi);
        //                 EskiGirenDepo.Miktar -= TransferMiktari;
        //                 EskiCikanDepo.Miktar += TransferMiktari;
        //                 yeniCikanDepo.Miktar -= TransferMiktari;
        //             }
        //         }

        //         if (propertyName == "GeldigiDepoID" && (StokID != null && CiktigiDepoID != null && GeldigiDepoID != null))
        //         {
        //             var EskiGirenDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //             i.DepoID == oldValue);

        //             var YeniGirenDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //             i.DepoID == GeldigiDepoID);

        //             if (EskiGirenDepo.Any() && YeniGirenDepo.Any() )
        //             {
        //                 EskiGirenDepo.FirstOrDefault().Miktar -= TransferMiktari;
        //                 YeniGirenDepo.FirstOrDefault().Miktar += TransferMiktari;
        //             }

        //             if (!YeniGirenDepo.Any())
        //             {
        //                 var stokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
        //                 {
        //                     DepoID = GeldigiDepoID,
        //                     StokID = this.StokID,
        //                     Miktar = TransferMiktari
        //                 };

        //                 Session.Save(stokHareketi);
        //                 EskiGirenDepo.FirstOrDefault().Miktar -= TransferMiktari;
        //             }
        //         }

        //         if (propertyName == "CiktigiDepoID" && (StokID != null && CiktigiDepoID != null && GeldigiDepoID != null))
        //         {
        //             var EskiCiktigiDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //             i.DepoID == oldValue);

        //             var YeniCiktigiDepo = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID &&
        //             i.DepoID == CiktigiDepoID);

        //             if (EskiCiktigiDepo.Any() && YeniCiktigiDepo.Any())
        //             {
        //                 EskiCiktigiDepo.FirstOrDefault().Miktar += TransferMiktari;
        //                 YeniCiktigiDepo.FirstOrDefault().Miktar -= TransferMiktari;
        //             }

        //             if (!YeniCiktigiDepo.Any())
        //             {
        //                 var stokHareketi = new HAREKETLER_Depo_StokMiktari(Session)
        //                 {
        //                     DepoID = CiktigiDepoID,
        //                     StokID = this.StokID,
        //                     Miktar = 0-TransferMiktari
        //                 };

        //                 Session.Save(stokHareketi);
        //                 EskiCiktigiDepo.FirstOrDefault().Miktar += TransferMiktari;
        //             }
        //         }
        //     }*/
        //}
    }
}