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

    }
}
