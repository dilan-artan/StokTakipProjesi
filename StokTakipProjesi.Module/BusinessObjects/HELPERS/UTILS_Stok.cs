using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.HAREKETLER;
using StokTakipProjesi.Module.BusinessObjects.TANIMLAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StokTakipProjesi.Module.BusinessObjects.HELPERS
{
    class UTILS_Stok
    {

        //stattic yapmazsan bir tane newleme yapman lazım.
        public static void DepoStokYaz(TANIMLAR_DepoKarti girenDepo, TANIMLAR_DepoKarti cikanDepo, TANIMLAR_StokKarti stok, decimal miktar, Session session)
        {
            var girenDepoStogu = session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == stok &&
             i.DepoID == girenDepo).FirstOrDefault();
            if (girenDepoStogu == null)
                girenDepoStogu = session.FindObject<HAREKETLER_Depo_StokMiktari>(PersistentCriteriaEvaluationBehavior.InTransaction, CriteriaOperator.Parse("DepoID.Oid=? and StokID.Oid=?", girenDepo.Oid, stok.Oid));

            var cikanDepoStogu = cikanDepo.Session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.StokID == stok &&
            i.DepoID == cikanDepo).FirstOrDefault();
            if (cikanDepoStogu == null)
                cikanDepoStogu = session.FindObject<HAREKETLER_Depo_StokMiktari>(PersistentCriteriaEvaluationBehavior.InTransaction, CriteriaOperator.Parse("DepoID.Oid=? and StokID.Oid=?", cikanDepo.Oid, stok.Oid));

            if (girenDepoStogu != null && cikanDepoStogu != null)
            {
                girenDepoStogu.Miktar += miktar;
                cikanDepoStogu.Miktar -= miktar;
            }

            if (girenDepoStogu == null && cikanDepoStogu != null)
            {
                var stokHareketi = new HAREKETLER_Depo_StokMiktari(girenDepo.Session)
                {
                    DepoID = girenDepo,
                    StokID = stok,
                    Miktar = miktar
                };

                girenDepo.Session.Save(stokHareketi);
                cikanDepoStogu.Miktar -= miktar;
            }

            if (cikanDepoStogu == null && girenDepoStogu != null)
            {
                var yeniStokHareketi = new HAREKETLER_Depo_StokMiktari(session)
                {
                    DepoID = cikanDepo,
                    StokID = stok,
                    Miktar = 0 - miktar
                };

                cikanDepo.Session.Save(yeniStokHareketi);
                // girenDepoStogu.Miktar += miktar;
            }

        }


        public static void UretimFisiYaz(TANIMLAR_DepoKarti depoID, TANIMLAR_StokKarti girenUrun, TANIMLAR_StokKarti cikanUrun, decimal girenMiktar, decimal cikanMiktar, Session session)
        {
            var girenUrunStogu = session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == depoID &&
            i.StokID == girenUrun).FirstOrDefault();

            if (girenUrunStogu == null)
                girenUrunStogu = session.FindObject<HAREKETLER_Depo_StokMiktari>(PersistentCriteriaEvaluationBehavior.InTransaction, CriteriaOperator.Parse("DepoID.Oid=? and StokID.Oid=?", depoID.Oid, girenUrun.Oid));

            var cikanUrunStogu = session.Query<HAREKETLER_Depo_StokMiktari>().Where(i => i.DepoID == depoID &&
            i.StokID == cikanUrun).FirstOrDefault();
            if (cikanUrunStogu == null)
                cikanUrunStogu = session.FindObject<HAREKETLER_Depo_StokMiktari>(PersistentCriteriaEvaluationBehavior.InTransaction, CriteriaOperator.Parse("DepoID.Oid=? and StokID.Oid=?", depoID.Oid, cikanUrun.Oid));

            //UTILS_Stok.DepoStokYaz(eskiGirenDepoID, eskiCikanDepoID, eskiStokID, eskiMiktar * -1, Session);

            if (girenUrunStogu != null && cikanUrunStogu != null)
            {
                cikanUrunStogu.Miktar += cikanMiktar;
                girenUrunStogu.Miktar -= girenMiktar;

                //girenUrun.Miktar += (decimal)oldValue - GirenMiktar;
                //cikanUrun.Miktar += CikanMiktar - (decimal)oldValue;

            }
            if (girenUrunStogu == null && cikanUrunStogu != null)
            {
                var stokHareketi = new HAREKETLER_Depo_StokMiktari(session)
                {
                    DepoID = depoID,
                    StokID = girenUrun,
                    Miktar = 0 - girenMiktar
                };

                stokHareketi.Session.Save(stokHareketi);
                cikanUrunStogu.Miktar += cikanMiktar;
            }

            if (cikanUrunStogu == null && girenUrunStogu != null)
            {
                var yeniStokHareket = new HAREKETLER_Depo_StokMiktari(session)
                {
                    DepoID = depoID,
                    StokID = cikanUrun,
                    Miktar = cikanMiktar
                };

                yeniStokHareket.Session.Save(yeniStokHareket);
            }
        }
    }
}

