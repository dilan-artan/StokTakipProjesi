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
    [RuleCriteria("GirenMiktar", DefaultContexts.Save, "GirenMiktar > 0", "Miktar 0 dan büyük olmalı", SkipNullOrEmptyValues = false)]
    [RuleCriteria("CikanMiktar", DefaultContexts.Save, "CikanMiktar > 0", "Miktar 0 dan büyük olmalı", SkipNullOrEmptyValues = false)]


    public class HAREKETLER_DepoHareket_Detayi : BaseObject
    {
        public HAREKETLER_DepoHareket_Detayi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        private HAREKETLER_DepoHareketi _hareketID;
        [Association("Fatura-Detay")]
        public HAREKETLER_DepoHareketi HareketID
        {
            get
            {
                return _hareketID;
            }
            set
            {
                SetPropertyValue(nameof(HareketID), ref _hareketID, value);


            }
        }

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

        private decimal _girenMiktar;
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

        private decimal _cikanMiktar;
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

        [NonPersistent]
        private decimal _toplamTutar;
        public decimal ToplamTutar
        {
            get
            {
                return GirenMiktar - CikanMiktar;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
         
            base.OnChanged(propertyName, oldValue, newValue);
            {
                if ((propertyName=="GirenMiktar" || propertyName == "CikanMiktar" || propertyName == "HareketID" || propertyName == "StokID")
                    && (HareketID !=null && StokID != null))
                {
                    var DepoStok = Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == this.StokID
                     && i.DepoID == this.HareketID.DepoID).FirstOrDefault();

                    //Normalde xaf bunu hafızaya kaydediyor. sadece session yazıldığında kayıt daha database de olmadığından bulamıyor ve otomatik kayıt açıyor. Böylece birden fazla oluşturuyor.
                    //Bu yüzden hem database hem de hafıza kontrolü yapılmalı.
                    if (DepoStok == null)
                        DepoStok = Session.FindObject<HAREKETLER_Depo_StokMiktari>(PersistentCriteriaEvaluationBehavior.InTransaction, CriteriaOperator.Parse("DepoID.Oid=? and StokID.Oid=?", HareketID.DepoID.Oid, StokID.Oid));




                    if (DepoStok != null)
                    {
                        if (propertyName == "CikanMiktar")
                        {
                            DepoStok.Miktar += (decimal)oldValue - CikanMiktar;
                        }
                        if (propertyName == "GirenMiktar")
                        {
                            DepoStok.Miktar += GirenMiktar - (decimal)(oldValue);
                        }
                    }

                    else
                    {

                        var yeniStok = new HAREKETLER_Depo_StokMiktari(Session)
                        {
                            DepoID = this.HareketID.DepoID,
                            StokID = this.StokID,
                            Miktar = (GirenMiktar - CikanMiktar)
                        };

                        Session.Save(yeniStok);
                    }
                   
                }

            }



          }
        }
    }

